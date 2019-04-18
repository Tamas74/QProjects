<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDM_RulesSetup
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    If (IsNothing(dtEndDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtEndDate)
                        Catch ex As Exception

                        End Try


                        dtEndDate.Dispose()
                        dtEndDate = Nothing
                    End If
                Catch
                End Try

                Try
                    If (IsNothing(dtStartDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtStartDate)
                        Catch ex As Exception

                        End Try


                        dtStartDate.Dispose()
                        dtStartDate = Nothing
                    End If
                Catch
                End Try

                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_RulesSetup))
        Dim CheckBoxProperties7 As PresentationControls.CheckBoxProperties = New PresentationControls.CheckBoxProperties()
        Dim CheckBoxProperties1 As PresentationControls.CheckBoxProperties = New PresentationControls.CheckBoxProperties()
        Dim CheckBoxProperties2 As PresentationControls.CheckBoxProperties = New PresentationControls.CheckBoxProperties()
        Dim CheckBoxProperties3 As PresentationControls.CheckBoxProperties = New PresentationControls.CheckBoxProperties()
        Dim CheckBoxProperties4 As PresentationControls.CheckBoxProperties = New PresentationControls.CheckBoxProperties()
        Dim CheckBoxProperties5 As PresentationControls.CheckBoxProperties = New PresentationControls.CheckBoxProperties()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Panel48 = New System.Windows.Forms.Panel()
        Me.tbCntrl_RuleSetup = New System.Windows.Forms.TabControl()
        Me.tbPg_Triggers = New System.Windows.Forms.TabPage()
        Me.pnlMiddle = New System.Windows.Forms.Panel()
        Me.pnlDemoVitals = New System.Windows.Forms.Panel()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.btnRemoveSelectedInsurance = New System.Windows.Forms.Button()
        Me.lstVw_Insurance = New System.Windows.Forms.ListView()
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearInsurance = New System.Windows.Forms.Button()
        Me.btnInsurance = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedICD10 = New System.Windows.Forms.Button()
        Me.btnClearICD10 = New System.Windows.Forms.Button()
        Me.lstVw_ICD10 = New System.Windows.Forms.ListView()
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnICD10 = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedDrug = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedOrders = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedICD9 = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedLab = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedCPT = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedHistory = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedSnomedCode = New System.Windows.Forms.Button()
        Me.btnClearSnomed = New System.Windows.Forms.Button()
        Me.btnSnomed = New System.Windows.Forms.Button()
        Me.lstVw_SnoMed = New System.Windows.Forms.ListView()
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnClearICD = New System.Windows.Forms.Button()
        Me.btnClearCPT = New System.Windows.Forms.Button()
        Me.lstVw_Orders = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.lstVw_Drugs = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearHistory = New System.Windows.Forms.Button()
        Me.lstVw_Lab = New System.Windows.Forms.ListView()
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.lstVw_ICD9 = New System.Windows.Forms.ListView()
        Me.lstVw_COL_ICDCode = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearOrders = New System.Windows.Forms.Button()
        Me.lstVw_CPT = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearDrugs = New System.Windows.Forms.Button()
        Me.lstVw_History = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearLab = New System.Windows.Forms.Button()
        Me.btnHistory = New System.Windows.Forms.Button()
        Me.btnRadiology = New System.Windows.Forms.Button()
        Me.btnDrugs = New System.Windows.Forms.Button()
        Me.btnLabs = New System.Windows.Forms.Button()
        Me.btnICD9 = New System.Windows.Forms.Button()
        Me.btnCPT = New System.Windows.Forms.Button()
        Me.btnproblemlist = New System.Windows.Forms.Button()
        Me.btnDemographics = New System.Windows.Forms.Button()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.Label202 = New System.Windows.Forms.Label()
        Me.Label204 = New System.Windows.Forms.Label()
        Me.Label214 = New System.Windows.Forms.Label()
        Me.Label236 = New System.Windows.Forms.Label()
        Me.Label241 = New System.Windows.Forms.Label()
        Me.pnlVitals = New System.Windows.Forms.Panel()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.label52 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.label51 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label254 = New System.Windows.Forms.Label()
        Me.Label260 = New System.Windows.Forms.Label()
        Me.Label258 = New System.Windows.Forms.Label()
        Me.Label256 = New System.Windows.Forms.Label()
        Me.Label253 = New System.Windows.Forms.Label()
        Me.Label252 = New System.Windows.Forms.Label()
        Me.Label251 = New System.Windows.Forms.Label()
        Me.Label250 = New System.Windows.Forms.Label()
        Me.Label249 = New System.Windows.Forms.Label()
        Me.Label259 = New System.Windows.Forms.Label()
        Me.Label257 = New System.Windows.Forms.Label()
        Me.Label255 = New System.Windows.Forms.Label()
        Me.Label248 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.txtHeightMin = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtHeightMaxInch = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtHeightMinInch = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPulseOXmax = New System.Windows.Forms.TextBox()
        Me.lblPulseOXMax = New System.Windows.Forms.Label()
        Me.txtPulseOXmin = New System.Windows.Forms.TextBox()
        Me.lblPulseOXMin = New System.Windows.Forms.Label()
        Me.txtPulseMax = New System.Windows.Forms.TextBox()
        Me.lblPulseMax = New System.Windows.Forms.Label()
        Me.txtPulseMin = New System.Windows.Forms.TextBox()
        Me.lblPulseMin = New System.Windows.Forms.Label()
        Me.txtTemperatureMax = New System.Windows.Forms.TextBox()
        Me.lblTempratureMax = New System.Windows.Forms.Label()
        Me.txtBMImax = New System.Windows.Forms.TextBox()
        Me.lblBMImax = New System.Windows.Forms.Label()
        Me.txtBMImin = New System.Windows.Forms.TextBox()
        Me.lblBMImin = New System.Windows.Forms.Label()
        Me.txtWeightMax = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblWeightMax = New System.Windows.Forms.Label()
        Me.txtHeightMax = New System.Windows.Forms.TextBox()
        Me.txtBPstandingMaxTo = New System.Windows.Forms.TextBox()
        Me.txtBPstandingMinTo = New System.Windows.Forms.TextBox()
        Me.txtBPstandingMax = New System.Windows.Forms.TextBox()
        Me.txtBPstandingMin = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMaxTo = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMinTo = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMax = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMin = New System.Windows.Forms.TextBox()
        Me.lblBPStandingMax = New System.Windows.Forms.Label()
        Me.lblBPStandingMin = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblBPSittingMax = New System.Windows.Forms.Label()
        Me.lblBPSittingMin = New System.Windows.Forms.Label()
        Me.txtTemperatureMin = New System.Windows.Forms.TextBox()
        Me.txtWeightMin = New System.Windows.Forms.TextBox()
        Me.lblWeightMin = New System.Windows.Forms.Label()
        Me.lblTempratureMin = New System.Windows.Forms.Label()
        Me.lblHeightMin = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlDemographics = New System.Windows.Forms.Panel()
        Me.cmbChkBoxMaritalSt = New PresentationControls.CheckBoxComboBox()
        Me.cmbChkBoxGender = New PresentationControls.CheckBoxComboBox()
        Me.cmbChkBoxRace = New PresentationControls.CheckBoxComboBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.Label158 = New System.Windows.Forms.Label()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.cmbState = New System.Windows.Forms.ComboBox()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.lblState = New System.Windows.Forms.Label()
        Me.txtZip = New System.Windows.Forms.TextBox()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.lblEmpStatus = New System.Windows.Forms.Label()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.cmbEmpStatus = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMaxMnth = New System.Windows.Forms.ComboBox()
        Me.lblZip = New System.Windows.Forms.Label()
        Me.cmbAgeMinMnth = New System.Windows.Forms.ComboBox()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.cmbAgeMax = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMin = New System.Windows.Forms.ComboBox()
        Me.Label200 = New System.Windows.Forms.Label()
        Me.Label157 = New System.Windows.Forms.Label()
        Me.lblAgeMax = New System.Windows.Forms.Label()
        Me.lblMaritalStatus = New System.Windows.Forms.Label()
        Me.lblGender = New System.Windows.Forms.Label()
        Me.lblRace = New System.Windows.Forms.Label()
        Me.Label156 = New System.Windows.Forms.Label()
        Me.lblAgeMin = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PnlProblemList = New System.Windows.Forms.Panel()
        Me.PnlProblemMiddle = New System.Windows.Forms.Panel()
        Me.Pnlsnomedprb = New System.Windows.Forms.Panel()
        Me.pnltrvSnowmedOff = New System.Windows.Forms.Panel()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.trvSnowmedOff = New System.Windows.Forms.TreeView()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.pnltrvfinprob = New System.Windows.Forms.Panel()
        Me.trvfinprob = New System.Windows.Forms.TreeView()
        Me.Panel31 = New System.Windows.Forms.Panel()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.Label201 = New System.Windows.Forms.Label()
        Me.Label203 = New System.Windows.Forms.Label()
        Me.PnlSrchProb = New System.Windows.Forms.Panel()
        Me.txtsrchprb = New System.Windows.Forms.TextBox()
        Me.Label215 = New System.Windows.Forms.Label()
        Me.Label216 = New System.Windows.Forms.Label()
        Me.btnclrprb = New System.Windows.Forms.Button()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label217 = New System.Windows.Forms.Label()
        Me.Label218 = New System.Windows.Forms.Label()
        Me.Label219 = New System.Windows.Forms.Label()
        Me.Label220 = New System.Windows.Forms.Label()
        Me.Splitter6 = New System.Windows.Forms.Splitter()
        Me.pnltrvsubprb = New System.Windows.Forms.Panel()
        Me.Label221 = New System.Windows.Forms.Label()
        Me.trvsubprb = New System.Windows.Forms.TreeView()
        Me.Panel34 = New System.Windows.Forms.Panel()
        Me.Label222 = New System.Windows.Forms.Label()
        Me.Label223 = New System.Windows.Forms.Label()
        Me.Label224 = New System.Windows.Forms.Label()
        Me.Label225 = New System.Windows.Forms.Label()
        Me.Label226 = New System.Windows.Forms.Label()
        Me.PnlProblemSearch = New System.Windows.Forms.Panel()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.trvprobright = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PnlProbLeft = New System.Windows.Forms.Panel()
        Me.Label179 = New System.Windows.Forms.Label()
        Me.Label197 = New System.Windows.Forms.Label()
        Me.Label198 = New System.Windows.Forms.Label()
        Me.Label199 = New System.Windows.Forms.Label()
        Me.trvproblem = New System.Windows.Forms.TreeView()
        Me.Panel35 = New System.Windows.Forms.Panel()
        Me.Panel36 = New System.Windows.Forms.Panel()
        Me.Label227 = New System.Windows.Forms.Label()
        Me.Label228 = New System.Windows.Forms.Label()
        Me.Label229 = New System.Windows.Forms.Label()
        Me.Label230 = New System.Windows.Forms.Label()
        Me.Label231 = New System.Windows.Forms.Label()
        Me.Panel37 = New System.Windows.Forms.Panel()
        Me.trvselectedhist = New System.Windows.Forms.TreeView()
        Me.Label232 = New System.Windows.Forms.Label()
        Me.Label233 = New System.Windows.Forms.Label()
        Me.trvselectedprob = New System.Windows.Forms.TreeView()
        Me.Label234 = New System.Windows.Forms.Label()
        Me.Label235 = New System.Windows.Forms.Label()
        Me.Panel38 = New System.Windows.Forms.Panel()
        Me.Panel39 = New System.Windows.Forms.Panel()
        Me.cmbhistsnomed = New System.Windows.Forms.ComboBox()
        Me.lblsnohistcat = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label237 = New System.Windows.Forms.Label()
        Me.Label238 = New System.Windows.Forms.Label()
        Me.Label239 = New System.Windows.Forms.Label()
        Me.Label240 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.pnlRadiology = New System.Windows.Forms.Panel()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Label170 = New System.Windows.Forms.Label()
        Me.Label171 = New System.Windows.Forms.Label()
        Me.Label172 = New System.Windows.Forms.Label()
        Me.Label173 = New System.Windows.Forms.Label()
        Me.c1Labs = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.txtLabsSearch = New System.Windows.Forms.TextBox()
        Me.btnLabClear = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.Label168 = New System.Windows.Forms.Label()
        Me.Label169 = New System.Windows.Forms.Label()
        Me.pnlInternalToolStripRadiology = New System.Windows.Forms.Panel()
        Me.Panel41 = New System.Windows.Forms.Panel()
        Me.ToolStrip6 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveRadiology = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_CancelRadiology = New System.Windows.Forms.ToolStripButton()
        Me.pnlHistory = New System.Windows.Forms.Panel()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.Label193 = New System.Windows.Forms.Label()
        Me.Label194 = New System.Windows.Forms.Label()
        Me.trvSelectedHistory = New System.Windows.Forms.TreeView()
        Me.Label195 = New System.Windows.Forms.Label()
        Me.Label196 = New System.Windows.Forms.Label()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Label188 = New System.Windows.Forms.Label()
        Me.Label189 = New System.Windows.Forms.Label()
        Me.Label190 = New System.Windows.Forms.Label()
        Me.Label191 = New System.Windows.Forms.Label()
        Me.Label192 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.trvHistoryRight = New System.Windows.Forms.TreeView()
        Me.pnlHistoryLeft = New System.Windows.Forms.Panel()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.Label121 = New System.Windows.Forms.Label()
        Me.Label122 = New System.Windows.Forms.Label()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.trvHistory = New System.Windows.Forms.TreeView()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.cmbHistoryCategory = New System.Windows.Forms.ComboBox()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.btnHistorySearch = New System.Windows.Forms.Button()
        Me.pnlInternalToolStripHistory = New System.Windows.Forms.Panel()
        Me.Panel33 = New System.Windows.Forms.Panel()
        Me.ToolStrip4 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveHistory = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_CancelHistory = New System.Windows.Forms.ToolStripButton()
        Me.pnlInsurance = New System.Windows.Forms.Panel()
        Me.pnltrvSelectedInsurance = New System.Windows.Forms.Panel()
        Me.Label329 = New System.Windows.Forms.Label()
        Me.Label330 = New System.Windows.Forms.Label()
        Me.trvSelectedInsurance = New System.Windows.Forms.TreeView()
        Me.Label331 = New System.Windows.Forms.Label()
        Me.Label332 = New System.Windows.Forms.Label()
        Me.pnlSelectedInsuranceLabel = New System.Windows.Forms.Panel()
        Me.Panel56 = New System.Windows.Forms.Panel()
        Me.Label333 = New System.Windows.Forms.Label()
        Me.Label334 = New System.Windows.Forms.Label()
        Me.Label335 = New System.Windows.Forms.Label()
        Me.Label336 = New System.Windows.Forms.Label()
        Me.Label337 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.pnlInternalToolStripInsurance = New System.Windows.Forms.Panel()
        Me.Panel58 = New System.Windows.Forms.Panel()
        Me.ToolStrip13 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveInsurance = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_CancelInsurance = New System.Windows.Forms.ToolStripButton()
        Me.pnlICD9 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label132 = New System.Windows.Forms.Label()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.trvselecteICD10s = New System.Windows.Forms.TreeView()
        Me.trvselecteICDs = New System.Windows.Forms.TreeView()
        Me.Label134 = New System.Windows.Forms.Label()
        Me.Label135 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.Label130 = New System.Windows.Forms.Label()
        Me.Label131 = New System.Windows.Forms.Label()
        Me.Splitter4 = New System.Windows.Forms.Splitter()
        Me.pnlInternalToolStripICD = New System.Windows.Forms.Panel()
        Me.Panel29 = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveICD = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_CancelICD = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_SaveICD10 = New System.Windows.Forms.ToolStripButton()
        Me.pnlCPT = New System.Windows.Forms.Panel()
        Me.pnlSelectedCPTs = New System.Windows.Forms.Panel()
        Me.Label205 = New System.Windows.Forms.Label()
        Me.Label206 = New System.Windows.Forms.Label()
        Me.trvselectedCPT = New System.Windows.Forms.TreeView()
        Me.Label207 = New System.Windows.Forms.Label()
        Me.Label208 = New System.Windows.Forms.Label()
        Me.pnlSelecteCPTsLabels = New System.Windows.Forms.Panel()
        Me.Panel28 = New System.Windows.Forms.Panel()
        Me.Label209 = New System.Windows.Forms.Label()
        Me.Label210 = New System.Windows.Forms.Label()
        Me.Label211 = New System.Windows.Forms.Label()
        Me.Label212 = New System.Windows.Forms.Label()
        Me.Label213 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlInternalToolStripCPT = New System.Windows.Forms.Panel()
        Me.Panel30 = New System.Windows.Forms.Panel()
        Me.ToolStrip2 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveCPT = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_CancelCPT = New System.Windows.Forms.ToolStripButton()
        Me.pnlDrugs = New System.Windows.Forms.Panel()
        Me.pnltrvSelectedDrugs = New System.Windows.Forms.Panel()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label180 = New System.Windows.Forms.Label()
        Me.trvSelectedDrugs = New System.Windows.Forms.TreeView()
        Me.Label181 = New System.Windows.Forms.Label()
        Me.Label182 = New System.Windows.Forms.Label()
        Me.pnlSelectedDrugLabel = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label185 = New System.Windows.Forms.Label()
        Me.Label184 = New System.Windows.Forms.Label()
        Me.Label186 = New System.Windows.Forms.Label()
        Me.Label187 = New System.Windows.Forms.Label()
        Me.Label183 = New System.Windows.Forms.Label()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.pnlInternalToolStripDrugs = New System.Windows.Forms.Panel()
        Me.Panel32 = New System.Windows.Forms.Panel()
        Me.ToolStrip3 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveDrugs = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_CancelDrugs = New System.Windows.Forms.ToolStripButton()
        Me.pnlLab = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.C1LabResult = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.txtLabResultSearch = New System.Windows.Forms.TextBox()
        Me.btnLabResultClear = New System.Windows.Forms.Button()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label136 = New System.Windows.Forms.Label()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.pnlInternalToolStripLab = New System.Windows.Forms.Panel()
        Me.Panel40 = New System.Windows.Forms.Panel()
        Me.ToolStrip5 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveLab = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_CancelLab = New System.Windows.Forms.ToolStripButton()
        Me.tbPg_Exceptions = New System.Windows.Forms.TabPage()
        Me.pnlExceptionsMiddle = New System.Windows.Forms.Panel()
        Me.pnlExceptionsDemoVitals = New System.Windows.Forms.Panel()
        Me.Panel45 = New System.Windows.Forms.Panel()
        Me.btnRemoveSelectedInsurance_Ex = New System.Windows.Forms.Button()
        Me.lstExVw_Insurance = New System.Windows.Forms.ListView()
        Me.ColumnHeader18 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearInsuranceEx = New System.Windows.Forms.Button()
        Me.btnInsuranceEx = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedICD10_Ex = New System.Windows.Forms.Button()
        Me.btnClearICD10Ex = New System.Windows.Forms.Button()
        Me.lstExVw_ICD10 = New System.Windows.Forms.ListView()
        Me.ColumnHeader16 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnICD10Ex = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedDrug_Ex = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedOrders_Ex = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedICD9_Ex = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedLab_Ex = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedCPT_Ex = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedHistory_Ex = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedSnomedCodeEx = New System.Windows.Forms.Button()
        Me.btnClearSnomedEx = New System.Windows.Forms.Button()
        Me.btnSnomedEx = New System.Windows.Forms.Button()
        Me.lstExVw_SnoMed = New System.Windows.Forms.ListView()
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.btnClearICD9Ex = New System.Windows.Forms.Button()
        Me.btnClearCPTEx = New System.Windows.Forms.Button()
        Me.lstExVw_Orders = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.lstExVw_Drugs = New System.Windows.Forms.ListView()
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearHistoryEx = New System.Windows.Forms.Button()
        Me.lstExVw_Lab = New System.Windows.Forms.ListView()
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.lstExVw_ICD = New System.Windows.Forms.ListView()
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearOrdersEx = New System.Windows.Forms.Button()
        Me.lstExVw_CPT = New System.Windows.Forms.ListView()
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearDrugsEx = New System.Windows.Forms.Button()
        Me.lstExVw_History = New System.Windows.Forms.ListView()
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.btnClearLabEx = New System.Windows.Forms.Button()
        Me.btnHistoryEx = New System.Windows.Forms.Button()
        Me.btnRadiologyEx = New System.Windows.Forms.Button()
        Me.btnDrugsEx = New System.Windows.Forms.Button()
        Me.btnLabEx = New System.Windows.Forms.Button()
        Me.btnICD9Ex = New System.Windows.Forms.Button()
        Me.btnCPTEx = New System.Windows.Forms.Button()
        Me.btnproblemlist_Ex = New System.Windows.Forms.Button()
        Me.btnDemographics_Ex = New System.Windows.Forms.Button()
        Me.Panel46 = New System.Windows.Forms.Panel()
        Me.Panel47 = New System.Windows.Forms.Panel()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.pnlVitals_Ex = New System.Windows.Forms.Panel()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.Label174 = New System.Windows.Forms.Label()
        Me.Label175 = New System.Windows.Forms.Label()
        Me.Label176 = New System.Windows.Forms.Label()
        Me.Label177 = New System.Windows.Forms.Label()
        Me.Label178 = New System.Windows.Forms.Label()
        Me.Label244 = New System.Windows.Forms.Label()
        Me.Label245 = New System.Windows.Forms.Label()
        Me.Label263 = New System.Windows.Forms.Label()
        Me.Label264 = New System.Windows.Forms.Label()
        Me.Label265 = New System.Windows.Forms.Label()
        Me.Label266 = New System.Windows.Forms.Label()
        Me.Label267 = New System.Windows.Forms.Label()
        Me.Label268 = New System.Windows.Forms.Label()
        Me.Label269 = New System.Windows.Forms.Label()
        Me.Label270 = New System.Windows.Forms.Label()
        Me.Label271 = New System.Windows.Forms.Label()
        Me.txtHeightMin_Ex = New System.Windows.Forms.TextBox()
        Me.Label272 = New System.Windows.Forms.Label()
        Me.txtHeightMaxInch_Ex = New System.Windows.Forms.TextBox()
        Me.Label273 = New System.Windows.Forms.Label()
        Me.Label274 = New System.Windows.Forms.Label()
        Me.txtHeightMinInch_Ex = New System.Windows.Forms.TextBox()
        Me.Label275 = New System.Windows.Forms.Label()
        Me.txtPulseOXmax_Ex = New System.Windows.Forms.TextBox()
        Me.Label276 = New System.Windows.Forms.Label()
        Me.txtPulseOXmin_Ex = New System.Windows.Forms.TextBox()
        Me.Label277 = New System.Windows.Forms.Label()
        Me.txtPulseMax_Ex = New System.Windows.Forms.TextBox()
        Me.Label278 = New System.Windows.Forms.Label()
        Me.txtPulseMin_Ex = New System.Windows.Forms.TextBox()
        Me.Label279 = New System.Windows.Forms.Label()
        Me.txtTemperatureMax_Ex = New System.Windows.Forms.TextBox()
        Me.Label280 = New System.Windows.Forms.Label()
        Me.txtBMImax_Ex = New System.Windows.Forms.TextBox()
        Me.Label281 = New System.Windows.Forms.Label()
        Me.txtBMImin_Ex = New System.Windows.Forms.TextBox()
        Me.Label282 = New System.Windows.Forms.Label()
        Me.txtWeightMax_Ex = New System.Windows.Forms.TextBox()
        Me.Label283 = New System.Windows.Forms.Label()
        Me.txtHeightMax_Ex = New System.Windows.Forms.TextBox()
        Me.txtBPstandingMax_Ex_To = New System.Windows.Forms.TextBox()
        Me.txtBPstandingMin_Ex_To = New System.Windows.Forms.TextBox()
        Me.txtBPstandingMax_Ex = New System.Windows.Forms.TextBox()
        Me.txtBPstandingMin_Ex = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMax_Ex_To = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMin_Ex_To = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMax_Ex = New System.Windows.Forms.TextBox()
        Me.txtBPsettingMin_Ex = New System.Windows.Forms.TextBox()
        Me.Label284 = New System.Windows.Forms.Label()
        Me.Label285 = New System.Windows.Forms.Label()
        Me.Label286 = New System.Windows.Forms.Label()
        Me.Label287 = New System.Windows.Forms.Label()
        Me.txtTemperatureMin_Ex = New System.Windows.Forms.TextBox()
        Me.txtWeightMin_Ex = New System.Windows.Forms.TextBox()
        Me.Label288 = New System.Windows.Forms.Label()
        Me.Label289 = New System.Windows.Forms.Label()
        Me.Label290 = New System.Windows.Forms.Label()
        Me.Panel49 = New System.Windows.Forms.Panel()
        Me.Panel50 = New System.Windows.Forms.Panel()
        Me.Label291 = New System.Windows.Forms.Label()
        Me.Label292 = New System.Windows.Forms.Label()
        Me.Label293 = New System.Windows.Forms.Label()
        Me.Label294 = New System.Windows.Forms.Label()
        Me.Label295 = New System.Windows.Forms.Label()
        Me.pnlDemographics_Ex = New System.Windows.Forms.Panel()
        Me.cmbChkBoxMaritalSt_Ex = New PresentationControls.CheckBoxComboBox()
        Me.cmbGender_Ex = New PresentationControls.CheckBoxComboBox()
        Me.cmbChkBoxRace_Ex = New PresentationControls.CheckBoxComboBox()
        Me.txtCity_Ex = New System.Windows.Forms.TextBox()
        Me.Label296 = New System.Windows.Forms.Label()
        Me.Label297 = New System.Windows.Forms.Label()
        Me.Label298 = New System.Windows.Forms.Label()
        Me.cmbState_Ex = New System.Windows.Forms.ComboBox()
        Me.Label299 = New System.Windows.Forms.Label()
        Me.Label300 = New System.Windows.Forms.Label()
        Me.txtZip_Ex = New System.Windows.Forms.TextBox()
        Me.Label301 = New System.Windows.Forms.Label()
        Me.Label302 = New System.Windows.Forms.Label()
        Me.Label303 = New System.Windows.Forms.Label()
        Me.cmbEmpStatus_Ex = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMaxMnth_Ex = New System.Windows.Forms.ComboBox()
        Me.Label304 = New System.Windows.Forms.Label()
        Me.cmbAgeMinMnth_Ex = New System.Windows.Forms.ComboBox()
        Me.Label305 = New System.Windows.Forms.Label()
        Me.Label306 = New System.Windows.Forms.Label()
        Me.Label307 = New System.Windows.Forms.Label()
        Me.Label308 = New System.Windows.Forms.Label()
        Me.cmbAgeMax_Ex = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMin_Ex = New System.Windows.Forms.ComboBox()
        Me.Label309 = New System.Windows.Forms.Label()
        Me.Label310 = New System.Windows.Forms.Label()
        Me.Label311 = New System.Windows.Forms.Label()
        Me.Label312 = New System.Windows.Forms.Label()
        Me.Label313 = New System.Windows.Forms.Label()
        Me.Label314 = New System.Windows.Forms.Label()
        Me.Label315 = New System.Windows.Forms.Label()
        Me.Label316 = New System.Windows.Forms.Label()
        Me.Panel52 = New System.Windows.Forms.Panel()
        Me.Panel53 = New System.Windows.Forms.Panel()
        Me.Label317 = New System.Windows.Forms.Label()
        Me.Label318 = New System.Windows.Forms.Label()
        Me.Label319 = New System.Windows.Forms.Label()
        Me.Label320 = New System.Windows.Forms.Label()
        Me.Label321 = New System.Windows.Forms.Label()
        Me.pnlExceptionsRadiology = New System.Windows.Forms.Panel()
        Me.Panel18_Ex = New System.Windows.Forms.Panel()
        Me.Label362 = New System.Windows.Forms.Label()
        Me.Label363 = New System.Windows.Forms.Label()
        Me.Label364 = New System.Windows.Forms.Label()
        Me.Label365 = New System.Windows.Forms.Label()
        Me.c1Labs_Ex = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel17_Ex = New System.Windows.Forms.Panel()
        Me.txtLabsSearch_Ex = New System.Windows.Forms.TextBox()
        Me.bntLabClear_Ex = New System.Windows.Forms.Button()
        Me.Label366 = New System.Windows.Forms.Label()
        Me.Label367 = New System.Windows.Forms.Label()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.Label368 = New System.Windows.Forms.Label()
        Me.Label369 = New System.Windows.Forms.Label()
        Me.Label370 = New System.Windows.Forms.Label()
        Me.Label371 = New System.Windows.Forms.Label()
        Me.pnlInternalToolStripRadiology_Ex = New System.Windows.Forms.Panel()
        Me.Panel76 = New System.Windows.Forms.Panel()
        Me.ToolStrip7 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_Btn_SaveRadiology_Ex = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.pnlExceptionsHistory = New System.Windows.Forms.Panel()
        Me.Panel22_Ex = New System.Windows.Forms.Panel()
        Me.Label372 = New System.Windows.Forms.Label()
        Me.Label373 = New System.Windows.Forms.Label()
        Me.trvSelectedHistory_Ex = New System.Windows.Forms.TreeView()
        Me.Label374 = New System.Windows.Forms.Label()
        Me.Label375 = New System.Windows.Forms.Label()
        Me.Panel20_Ex = New System.Windows.Forms.Panel()
        Me.Panel80 = New System.Windows.Forms.Panel()
        Me.Label376 = New System.Windows.Forms.Label()
        Me.Label377 = New System.Windows.Forms.Label()
        Me.Label378 = New System.Windows.Forms.Label()
        Me.Label379 = New System.Windows.Forms.Label()
        Me.Label380 = New System.Windows.Forms.Label()
        Me.Panel16_Ex = New System.Windows.Forms.Panel()
        Me.Panel9_Ex = New System.Windows.Forms.Panel()
        Me.trvHistoryRight_Ex = New System.Windows.Forms.TreeView()
        Me.Panel83 = New System.Windows.Forms.Panel()
        Me.Label381 = New System.Windows.Forms.Label()
        Me.Label382 = New System.Windows.Forms.Label()
        Me.Label383 = New System.Windows.Forms.Label()
        Me.Label384 = New System.Windows.Forms.Label()
        Me.TreeView10 = New System.Windows.Forms.TreeView()
        Me.Panel11_Ex = New System.Windows.Forms.Panel()
        Me.Panel8_Ex = New System.Windows.Forms.Panel()
        Me.cmbHistoryCategory_Ex = New System.Windows.Forms.ComboBox()
        Me.Label385 = New System.Windows.Forms.Label()
        Me.txtSearch_Ex = New System.Windows.Forms.TextBox()
        Me.Label386 = New System.Windows.Forms.Label()
        Me.Label387 = New System.Windows.Forms.Label()
        Me.Label388 = New System.Windows.Forms.Label()
        Me.Label389 = New System.Windows.Forms.Label()
        Me.btnHistorySearch_Ex = New System.Windows.Forms.Button()
        Me.pnlInternalToolStripHistory_Ex = New System.Windows.Forms.Panel()
        Me.Panel87 = New System.Windows.Forms.Panel()
        Me.ToolStrip8 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveHistory_Ex = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.pnlExceptionsInsurance = New System.Windows.Forms.Panel()
        Me.pnltrvSelectedInsurance_Ex = New System.Windows.Forms.Panel()
        Me.Label338 = New System.Windows.Forms.Label()
        Me.Label339 = New System.Windows.Forms.Label()
        Me.trvSelectedInsurance_Ex = New System.Windows.Forms.TreeView()
        Me.Label340 = New System.Windows.Forms.Label()
        Me.Label341 = New System.Windows.Forms.Label()
        Me.pnlSelectedInsuranceLabel_Ex = New System.Windows.Forms.Panel()
        Me.Panel57 = New System.Windows.Forms.Panel()
        Me.Label342 = New System.Windows.Forms.Label()
        Me.Label343 = New System.Windows.Forms.Label()
        Me.Label344 = New System.Windows.Forms.Label()
        Me.Label345 = New System.Windows.Forms.Label()
        Me.Label346 = New System.Windows.Forms.Label()
        Me.Splitter5 = New System.Windows.Forms.Splitter()
        Me.pnlInternalToolStripInsurance_Ex = New System.Windows.Forms.Panel()
        Me.Panel60 = New System.Windows.Forms.Panel()
        Me.ToolStrip14 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveInsurance_Ex = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_CancelInsurance_Ex = New System.Windows.Forms.ToolStripButton()
        Me.pnlExceptionsCPT = New System.Windows.Forms.Panel()
        Me.pnlSelectedCPTs_Ex = New System.Windows.Forms.Panel()
        Me.Label399 = New System.Windows.Forms.Label()
        Me.Label400 = New System.Windows.Forms.Label()
        Me.trvselectedCPT_Ex = New System.Windows.Forms.TreeView()
        Me.Label401 = New System.Windows.Forms.Label()
        Me.Label402 = New System.Windows.Forms.Label()
        Me.pnlSelectedCPTsLabels_Ex = New System.Windows.Forms.Panel()
        Me.Panel97 = New System.Windows.Forms.Panel()
        Me.Label403 = New System.Windows.Forms.Label()
        Me.Label404 = New System.Windows.Forms.Label()
        Me.Label405 = New System.Windows.Forms.Label()
        Me.Label406 = New System.Windows.Forms.Label()
        Me.Label407 = New System.Windows.Forms.Label()
        Me.Splitter8 = New System.Windows.Forms.Splitter()
        Me.pnlInternalToolStripCPT_Ex = New System.Windows.Forms.Panel()
        Me.Panel99 = New System.Windows.Forms.Panel()
        Me.ToolStrip10 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveCPT_Ex = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.pnlExceptionsDrugs = New System.Windows.Forms.Panel()
        Me.pnltrvSelectedDrugs_Ex = New System.Windows.Forms.Panel()
        Me.Label408 = New System.Windows.Forms.Label()
        Me.Label409 = New System.Windows.Forms.Label()
        Me.trvSelectedDrugs_Ex = New System.Windows.Forms.TreeView()
        Me.Label410 = New System.Windows.Forms.Label()
        Me.Label411 = New System.Windows.Forms.Label()
        Me.pnlSelectedDrugLabel_Ex = New System.Windows.Forms.Panel()
        Me.Panel103 = New System.Windows.Forms.Panel()
        Me.Label412 = New System.Windows.Forms.Label()
        Me.Label413 = New System.Windows.Forms.Label()
        Me.Label414 = New System.Windows.Forms.Label()
        Me.Label415 = New System.Windows.Forms.Label()
        Me.Label416 = New System.Windows.Forms.Label()
        Me.Splitter9 = New System.Windows.Forms.Splitter()
        Me.pnlInternalToolStripDrugs_Ex = New System.Windows.Forms.Panel()
        Me.Panel105 = New System.Windows.Forms.Panel()
        Me.ToolStrip11 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveDrugs_Ex = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.pnlExceptionsLab = New System.Windows.Forms.Panel()
        Me.Panel112_Ex = New System.Windows.Forms.Panel()
        Me.Label417 = New System.Windows.Forms.Label()
        Me.C1LabResult_Ex = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label418 = New System.Windows.Forms.Label()
        Me.Label419 = New System.Windows.Forms.Label()
        Me.Label420 = New System.Windows.Forms.Label()
        Me.Panel13_Ex = New System.Windows.Forms.Panel()
        Me.Panel23_Ex = New System.Windows.Forms.Panel()
        Me.txtLabResultSearch_Ex = New System.Windows.Forms.TextBox()
        Me.btnLabResultClear_Ex = New System.Windows.Forms.Button()
        Me.Label421 = New System.Windows.Forms.Label()
        Me.Label422 = New System.Windows.Forms.Label()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.Label423 = New System.Windows.Forms.Label()
        Me.Label424 = New System.Windows.Forms.Label()
        Me.Label425 = New System.Windows.Forms.Label()
        Me.Label426 = New System.Windows.Forms.Label()
        Me.pnlInternalToolStripLab_Ex = New System.Windows.Forms.Panel()
        Me.Panel111 = New System.Windows.Forms.Panel()
        Me.ToolStrip12 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveLab_Ex = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton12 = New System.Windows.Forms.ToolStripButton()
        Me.pnlExceptionsICD9 = New System.Windows.Forms.Panel()
        Me.Panel15_Ex = New System.Windows.Forms.Panel()
        Me.Label390 = New System.Windows.Forms.Label()
        Me.Label391 = New System.Windows.Forms.Label()
        Me.trvselectedICD10s_Ex = New System.Windows.Forms.TreeView()
        Me.trvselectedICDs_Ex = New System.Windows.Forms.TreeView()
        Me.Label392 = New System.Windows.Forms.Label()
        Me.Label393 = New System.Windows.Forms.Label()
        Me.Panel5_Ex = New System.Windows.Forms.Panel()
        Me.Panel91 = New System.Windows.Forms.Panel()
        Me.Label394 = New System.Windows.Forms.Label()
        Me.Label395 = New System.Windows.Forms.Label()
        Me.Label396 = New System.Windows.Forms.Label()
        Me.Label397 = New System.Windows.Forms.Label()
        Me.Label398 = New System.Windows.Forms.Label()
        Me.Splitter7 = New System.Windows.Forms.Splitter()
        Me.pnlInternalToolStripICD_Ex = New System.Windows.Forms.Panel()
        Me.Panel93 = New System.Windows.Forms.Panel()
        Me.ToolStrip9 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsBtn_SaveICD_Ex = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.tsBtn_SaveICD10_Ex = New System.Windows.Forms.ToolStripButton()
        Me.tbPg_QuickOrders = New System.Windows.Forms.TabPage()
        Me.pnlSummaryOthers = New System.Windows.Forms.Panel()
        Me.pnlGuideline = New System.Windows.Forms.Panel()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.trOrderInfo = New System.Windows.Forms.TreeView()
        Me.pnlGuidelineHeader = New System.Windows.Forms.Panel()
        Me.pnl3 = New System.Windows.Forms.Panel()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.sptRight = New System.Windows.Forms.Splitter()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.pnltrvTriggers = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.pnltxtSearchOrder = New System.Windows.Forms.Panel()
        Me.txtSearchOrder = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.pnlbtnLab = New System.Windows.Forms.Panel()
        Me.btnLab = New System.Windows.Forms.Button()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.pnlbtnReferrals = New System.Windows.Forms.Panel()
        Me.btnReferrals = New System.Windows.Forms.Button()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.pnlbtnRx = New System.Windows.Forms.Panel()
        Me.btnRx = New System.Windows.Forms.Button()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.pnlbtnRadiologyTest = New System.Windows.Forms.Panel()
        Me.btnRadiologyTest = New System.Windows.Forms.Button()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.pnlbtnGuideline = New System.Windows.Forms.Panel()
        Me.btnGuideline = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnlIM = New System.Windows.Forms.Panel()
        Me.btnIM = New System.Windows.Forms.Button()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.Label159 = New System.Windows.Forms.Label()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.tbPg_RefInfo = New System.Windows.Forms.TabPage()
        Me.Panel44 = New System.Windows.Forms.Panel()
        Me.Label242 = New System.Windows.Forms.Label()
        Me.Label243 = New System.Windows.Forms.Label()
        Me.Label328 = New System.Windows.Forms.Label()
        Me.Label327 = New System.Windows.Forms.Label()
        Me.Label326 = New System.Windows.Forms.Label()
        Me.Label325 = New System.Windows.Forms.Label()
        Me.Label247 = New System.Windows.Forms.Label()
        Me.Label246 = New System.Windows.Forms.Label()
        Me.txtRevisionDates = New System.Windows.Forms.TextBox()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.txtRelease = New System.Windows.Forms.TextBox()
        Me.txtFundingSource = New System.Windows.Forms.TextBox()
        Me.txtBibliographicCitation = New System.Windows.Forms.TextBox()
        Me.txtInterventionDeveloper = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel43 = New System.Windows.Forms.Panel()
        Me.Label261 = New System.Windows.Forms.Label()
        Me.Label262 = New System.Windows.Forms.Label()
        Me.Label322 = New System.Windows.Forms.Label()
        Me.Label323 = New System.Windows.Forms.Label()
        Me.Label324 = New System.Windows.Forms.Label()
        Me.sptLeft = New System.Windows.Forms.Splitter()
        Me.Panel84 = New System.Windows.Forms.Panel()
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
        Me.cmbMaritalSt = New System.Windows.Forms.ComboBox()
        Me.cmbRace = New System.Windows.Forms.ComboBox()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.pnlMsgTOP = New System.Windows.Forms.Panel()
        Me.pnlMsg = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Panel27 = New System.Windows.Forms.Panel()
        Me.pnlRecurring = New System.Windows.Forms.Panel()
        Me.pnlRecurrenceControls = New System.Windows.Forms.Panel()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.cmbDurationType = New System.Windows.Forms.ComboBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.chckRecurring = New System.Windows.Forms.CheckBox()
        Me.Panel42 = New System.Windows.Forms.Panel()
        Me.lblHealthPlanAlert = New System.Windows.Forms.Label()
        Me.chkSpecialAlert = New System.Windows.Forms.CheckBox()
        Me.chkIsActiveRule = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.pnl_tlstrip = New System.Windows.Forms.Panel()
        Me.pnlgloStreamLogo = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.tlsDM = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsDM_Save = New System.Windows.Forms.ToolStripButton()
        Me.tlsDM_Close = New System.Windows.Forms.ToolStripButton()
        Me.CntConditions = New System.Windows.Forms.ContextMenu()
        Me.mnuDelete = New System.Windows.Forms.MenuItem()
        Me.mnuReferral = New System.Windows.Forms.MenuItem()
        Me.EditReferral = New System.Windows.Forms.MenuItem()
        Me.mnuEditTemplate = New System.Windows.Forms.MenuItem()
        Me.cntFindingsprb = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteInsurance = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteDrugs = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuHistory = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.CmnuStripCPT = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuItem_DeleteCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.CmnustripICD = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuItem_DeleteICD = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cntFindings = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnu_AddFindings = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuProblem = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteProblem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GloUC_trvHistory = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvInsurance = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvICD10 = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvICD9 = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvCPT = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvDrugs = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_TrvHistoryEx = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvInsurance_Ex = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvCPT_Ex = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvDrugs_Ex = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvICD10_Ex = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvICD9_Ex = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvAssociates = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlMain.SuspendLayout
        Me.tbCntrl_RuleSetup.SuspendLayout
        Me.tbPg_Triggers.SuspendLayout
        Me.pnlMiddle.SuspendLayout
        Me.pnlDemoVitals.SuspendLayout
        Me.Panel19.SuspendLayout
        Me.Panel14.SuspendLayout
        Me.Panel25.SuspendLayout
        Me.pnlVitals.SuspendLayout
        Me.Panel3.SuspendLayout
        Me.Panel7.SuspendLayout
        Me.pnlDemographics.SuspendLayout
        Me.Panel2.SuspendLayout
        Me.Panel6.SuspendLayout
        Me.PnlProblemList.SuspendLayout
        Me.PnlProblemMiddle.SuspendLayout
        Me.Pnlsnomedprb.SuspendLayout
        Me.pnltrvSnowmedOff.SuspendLayout
        Me.Panel24.SuspendLayout
        Me.pnltrvfinprob.SuspendLayout
        Me.Panel31.SuspendLayout
        Me.PnlSrchProb.SuspendLayout
        CType(Me.PictureBox3,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnltrvsubprb.SuspendLayout
        Me.Panel34.SuspendLayout
        Me.PnlProblemSearch.SuspendLayout
        Me.Panel26.SuspendLayout
        Me.PnlProbLeft.SuspendLayout
        Me.Panel35.SuspendLayout
        Me.Panel36.SuspendLayout
        Me.Panel37.SuspendLayout
        Me.Panel38.SuspendLayout
        Me.Panel39.SuspendLayout
        Me.pnlRadiology.SuspendLayout
        Me.Panel18.SuspendLayout
        CType(Me.c1Labs,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel17.SuspendLayout
        CType(Me.PictureBox6,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlInternalToolStripRadiology.SuspendLayout
        Me.Panel41.SuspendLayout
        Me.ToolStrip6.SuspendLayout
        Me.pnlHistory.SuspendLayout
        Me.Panel22.SuspendLayout
        Me.Panel20.SuspendLayout
        Me.Panel21.SuspendLayout
        Me.Panel16.SuspendLayout
        Me.Panel9.SuspendLayout
        Me.pnlHistoryLeft.SuspendLayout
        Me.Panel11.SuspendLayout
        Me.Panel8.SuspendLayout
        Me.pnlInternalToolStripHistory.SuspendLayout
        Me.Panel33.SuspendLayout
        Me.ToolStrip4.SuspendLayout
        Me.pnlInsurance.SuspendLayout
        Me.pnltrvSelectedInsurance.SuspendLayout
        Me.pnlSelectedInsuranceLabel.SuspendLayout
        Me.Panel56.SuspendLayout
        Me.pnlInternalToolStripInsurance.SuspendLayout
        Me.Panel58.SuspendLayout
        Me.ToolStrip13.SuspendLayout
        Me.pnlICD9.SuspendLayout
        Me.Panel15.SuspendLayout
        Me.Panel5.SuspendLayout
        Me.Panel10.SuspendLayout
        Me.pnlInternalToolStripICD.SuspendLayout
        Me.Panel29.SuspendLayout
        Me.ToolStrip1.SuspendLayout
        Me.pnlCPT.SuspendLayout
        Me.pnlSelectedCPTs.SuspendLayout
        Me.pnlSelecteCPTsLabels.SuspendLayout
        Me.Panel28.SuspendLayout
        Me.pnlInternalToolStripCPT.SuspendLayout
        Me.Panel30.SuspendLayout
        Me.ToolStrip2.SuspendLayout
        Me.pnlDrugs.SuspendLayout
        Me.pnltrvSelectedDrugs.SuspendLayout
        Me.pnlSelectedDrugLabel.SuspendLayout
        Me.Panel4.SuspendLayout
        Me.pnlInternalToolStripDrugs.SuspendLayout
        Me.Panel32.SuspendLayout
        Me.ToolStrip3.SuspendLayout
        Me.pnlLab.SuspendLayout
        Me.Panel12.SuspendLayout
        CType(Me.C1LabResult,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel13.SuspendLayout
        Me.Panel23.SuspendLayout
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlInternalToolStripLab.SuspendLayout
        Me.Panel40.SuspendLayout
        Me.ToolStrip5.SuspendLayout
        Me.tbPg_Exceptions.SuspendLayout
        Me.pnlExceptionsMiddle.SuspendLayout
        Me.pnlExceptionsDemoVitals.SuspendLayout
        Me.Panel45.SuspendLayout
        Me.Panel46.SuspendLayout
        Me.Panel47.SuspendLayout
        Me.pnlVitals_Ex.SuspendLayout
        Me.Panel49.SuspendLayout
        Me.Panel50.SuspendLayout
        Me.pnlDemographics_Ex.SuspendLayout
        Me.Panel52.SuspendLayout
        Me.Panel53.SuspendLayout
        Me.pnlExceptionsRadiology.SuspendLayout
        Me.Panel18_Ex.SuspendLayout
        CType(Me.c1Labs_Ex,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel17_Ex.SuspendLayout
        CType(Me.PictureBox5,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlInternalToolStripRadiology_Ex.SuspendLayout
        Me.Panel76.SuspendLayout
        Me.ToolStrip7.SuspendLayout
        Me.pnlExceptionsHistory.SuspendLayout
        Me.Panel22_Ex.SuspendLayout
        Me.Panel20_Ex.SuspendLayout
        Me.Panel80.SuspendLayout
        Me.Panel16_Ex.SuspendLayout
        Me.Panel9_Ex.SuspendLayout
        Me.Panel83.SuspendLayout
        Me.Panel11_Ex.SuspendLayout
        Me.Panel8_Ex.SuspendLayout
        Me.pnlInternalToolStripHistory_Ex.SuspendLayout
        Me.Panel87.SuspendLayout
        Me.ToolStrip8.SuspendLayout
        Me.pnlExceptionsInsurance.SuspendLayout
        Me.pnltrvSelectedInsurance_Ex.SuspendLayout
        Me.pnlSelectedInsuranceLabel_Ex.SuspendLayout
        Me.Panel57.SuspendLayout
        Me.pnlInternalToolStripInsurance_Ex.SuspendLayout
        Me.Panel60.SuspendLayout
        Me.ToolStrip14.SuspendLayout
        Me.pnlExceptionsCPT.SuspendLayout
        Me.pnlSelectedCPTs_Ex.SuspendLayout
        Me.pnlSelectedCPTsLabels_Ex.SuspendLayout
        Me.Panel97.SuspendLayout
        Me.pnlInternalToolStripCPT_Ex.SuspendLayout
        Me.Panel99.SuspendLayout
        Me.ToolStrip10.SuspendLayout
        Me.pnlExceptionsDrugs.SuspendLayout
        Me.pnltrvSelectedDrugs_Ex.SuspendLayout
        Me.pnlSelectedDrugLabel_Ex.SuspendLayout
        Me.Panel103.SuspendLayout
        Me.pnlInternalToolStripDrugs_Ex.SuspendLayout
        Me.Panel105.SuspendLayout
        Me.ToolStrip11.SuspendLayout
        Me.pnlExceptionsLab.SuspendLayout
        Me.Panel112_Ex.SuspendLayout
        CType(Me.C1LabResult_Ex,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel13_Ex.SuspendLayout
        Me.Panel23_Ex.SuspendLayout
        CType(Me.PictureBox7,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlInternalToolStripLab_Ex.SuspendLayout
        Me.Panel111.SuspendLayout
        Me.ToolStrip12.SuspendLayout
        Me.pnlExceptionsICD9.SuspendLayout
        Me.Panel15_Ex.SuspendLayout
        Me.Panel5_Ex.SuspendLayout
        Me.Panel91.SuspendLayout
        Me.pnlInternalToolStripICD_Ex.SuspendLayout
        Me.Panel93.SuspendLayout
        Me.ToolStrip9.SuspendLayout
        Me.tbPg_QuickOrders.SuspendLayout
        Me.pnlSummaryOthers.SuspendLayout
        Me.pnlGuideline.SuspendLayout
        Me.pnlGuidelineHeader.SuspendLayout
        Me.pnl3.SuspendLayout
        Me.pnlRight.SuspendLayout
        Me.pnltrvTriggers.SuspendLayout
        Me.pnltxtSearchOrder.SuspendLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.pnlbtnLab.SuspendLayout
        Me.pnlbtnReferrals.SuspendLayout
        Me.pnlbtnRx.SuspendLayout
        Me.pnlbtnRadiologyTest.SuspendLayout
        Me.pnlbtnGuideline.SuspendLayout
        Me.pnlIM.SuspendLayout
        Me.tbPg_RefInfo.SuspendLayout
        Me.Panel44.SuspendLayout
        Me.Panel1.SuspendLayout
        Me.Panel43.SuspendLayout
        Me.pnlMsgTOP.SuspendLayout
        Me.pnlMsg.SuspendLayout
        Me.Panel27.SuspendLayout
        Me.pnlRecurring.SuspendLayout
        Me.pnlRecurrenceControls.SuspendLayout
        Me.Panel42.SuspendLayout
        Me.pnl_tlstrip.SuspendLayout
        Me.pnlgloStreamLogo.SuspendLayout
        CType(Me.PictureBox4,System.ComponentModel.ISupportInitialize).BeginInit
        Me.tlsDM.SuspendLayout
        Me.ContextMenuStrip2.SuspendLayout
        Me.ContextMenuStrip1.SuspendLayout
        Me.ContextMenuHistory.SuspendLayout
        Me.CmnuStripCPT.SuspendLayout
        Me.CmnustripICD.SuspendLayout
        Me.cntFindings.SuspendLayout
        Me.ContextMenuProblem.SuspendLayout
        Me.SuspendLayout
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = true
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.Panel48)
        Me.pnlMain.Controls.Add(Me.tbCntrl_RuleSetup)
        Me.pnlMain.Controls.Add(Me.sptLeft)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 146)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1094, 726)
        Me.pnlMain.TabIndex = 0
        '
        'Panel48
        '
        Me.Panel48.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel48.Location = New System.Drawing.Point(3, 726)
        Me.Panel48.Name = "Panel48"
        Me.Panel48.Size = New System.Drawing.Size(1091, 0)
        Me.Panel48.TabIndex = 115
        '
        'tbCntrl_RuleSetup
        '
        Me.tbCntrl_RuleSetup.Controls.Add(Me.tbPg_Triggers)
        Me.tbCntrl_RuleSetup.Controls.Add(Me.tbPg_Exceptions)
        Me.tbCntrl_RuleSetup.Controls.Add(Me.tbPg_QuickOrders)
        Me.tbCntrl_RuleSetup.Controls.Add(Me.tbPg_RefInfo)
        Me.tbCntrl_RuleSetup.Dock = System.Windows.Forms.DockStyle.Top
        Me.tbCntrl_RuleSetup.Location = New System.Drawing.Point(3, 0)
        Me.tbCntrl_RuleSetup.Name = "tbCntrl_RuleSetup"
        Me.tbCntrl_RuleSetup.SelectedIndex = 0
        Me.tbCntrl_RuleSetup.Size = New System.Drawing.Size(1091, 726)
        Me.tbCntrl_RuleSetup.TabIndex = 334
        '
        'tbPg_Triggers
        '
        Me.tbPg_Triggers.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tbPg_Triggers.Controls.Add(Me.pnlMiddle)
        Me.tbPg_Triggers.Location = New System.Drawing.Point(4, 23)
        Me.tbPg_Triggers.Name = "tbPg_Triggers"
        Me.tbPg_Triggers.Padding = New System.Windows.Forms.Padding(3)
        Me.tbPg_Triggers.Size = New System.Drawing.Size(1083, 699)
        Me.tbPg_Triggers.TabIndex = 0
        Me.tbPg_Triggers.Text = "Triggers  "
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnlMiddle.Controls.Add(Me.pnlDemoVitals)
        Me.pnlMiddle.Controls.Add(Me.PnlProblemList)
        Me.pnlMiddle.Controls.Add(Me.pnlRadiology)
        Me.pnlMiddle.Controls.Add(Me.pnlHistory)
        Me.pnlMiddle.Controls.Add(Me.pnlInsurance)
        Me.pnlMiddle.Controls.Add(Me.pnlICD9)
        Me.pnlMiddle.Controls.Add(Me.pnlCPT)
        Me.pnlMiddle.Controls.Add(Me.pnlDrugs)
        Me.pnlMiddle.Controls.Add(Me.pnlLab)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMiddle.Location = New System.Drawing.Point(3, 3)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Size = New System.Drawing.Size(1077, 693)
        Me.pnlMiddle.TabIndex = 1
        '
        'pnlDemoVitals
        '
        Me.pnlDemoVitals.Controls.Add(Me.Panel19)
        Me.pnlDemoVitals.Controls.Add(Me.btnproblemlist)
        Me.pnlDemoVitals.Controls.Add(Me.btnDemographics)
        Me.pnlDemoVitals.Controls.Add(Me.Panel14)
        Me.pnlDemoVitals.Controls.Add(Me.pnlVitals)
        Me.pnlDemoVitals.Controls.Add(Me.Panel3)
        Me.pnlDemoVitals.Controls.Add(Me.pnlDemographics)
        Me.pnlDemoVitals.Controls.Add(Me.Panel2)
        Me.pnlDemoVitals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDemoVitals.Location = New System.Drawing.Point(0, 0)
        Me.pnlDemoVitals.Name = "pnlDemoVitals"
        Me.pnlDemoVitals.Size = New System.Drawing.Size(1077, 693)
        Me.pnlDemoVitals.TabIndex = 0
        '
        'Panel19
        '
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedInsurance)
        Me.Panel19.Controls.Add(Me.lstVw_Insurance)
        Me.Panel19.Controls.Add(Me.btnClearInsurance)
        Me.Panel19.Controls.Add(Me.btnInsurance)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedICD10)
        Me.Panel19.Controls.Add(Me.btnClearICD10)
        Me.Panel19.Controls.Add(Me.lstVw_ICD10)
        Me.Panel19.Controls.Add(Me.btnICD10)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedDrug)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedOrders)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedICD9)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedLab)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedCPT)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedHistory)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedSnomedCode)
        Me.Panel19.Controls.Add(Me.btnClearSnomed)
        Me.Panel19.Controls.Add(Me.btnSnomed)
        Me.Panel19.Controls.Add(Me.lstVw_SnoMed)
        Me.Panel19.Controls.Add(Me.Label36)
        Me.Panel19.Controls.Add(Me.Label35)
        Me.Panel19.Controls.Add(Me.Label34)
        Me.Panel19.Controls.Add(Me.Label33)
        Me.Panel19.Controls.Add(Me.btnClearICD)
        Me.Panel19.Controls.Add(Me.btnClearCPT)
        Me.Panel19.Controls.Add(Me.lstVw_Orders)
        Me.Panel19.Controls.Add(Me.lstVw_Drugs)
        Me.Panel19.Controls.Add(Me.btnClearHistory)
        Me.Panel19.Controls.Add(Me.lstVw_Lab)
        Me.Panel19.Controls.Add(Me.lstVw_ICD9)
        Me.Panel19.Controls.Add(Me.btnClearOrders)
        Me.Panel19.Controls.Add(Me.lstVw_CPT)
        Me.Panel19.Controls.Add(Me.btnClearDrugs)
        Me.Panel19.Controls.Add(Me.lstVw_History)
        Me.Panel19.Controls.Add(Me.btnClearLab)
        Me.Panel19.Controls.Add(Me.btnHistory)
        Me.Panel19.Controls.Add(Me.btnRadiology)
        Me.Panel19.Controls.Add(Me.btnDrugs)
        Me.Panel19.Controls.Add(Me.btnLabs)
        Me.Panel19.Controls.Add(Me.btnICD9)
        Me.Panel19.Controls.Add(Me.btnCPT)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel19.Location = New System.Drawing.Point(0, 297)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(1077, 396)
        Me.Panel19.TabIndex = 90
        '
        'btnRemoveSelectedInsurance
        '
        Me.btnRemoveSelectedInsurance.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedInsurance.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedInsurance.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedInsurance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedInsurance.Image = CType(resources.GetObject("btnRemoveSelectedInsurance.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedInsurance.Location = New System.Drawing.Point(999, 286)
        Me.btnRemoveSelectedInsurance.Name = "btnRemoveSelectedInsurance"
        Me.btnRemoveSelectedInsurance.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedInsurance.TabIndex = 105
        Me.btnRemoveSelectedInsurance.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedInsurance, "Remove selected Insurance Plan ")
        Me.btnRemoveSelectedInsurance.UseVisualStyleBackColor = true
        '
        'lstVw_Insurance
        '
        Me.lstVw_Insurance.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader17})
        Me.lstVw_Insurance.Location = New System.Drawing.Point(714, 259)
        Me.lstVw_Insurance.MultiSelect = false
        Me.lstVw_Insurance.Name = "lstVw_Insurance"
        Me.lstVw_Insurance.Size = New System.Drawing.Size(275, 119)
        Me.lstVw_Insurance.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstVw_Insurance.TabIndex = 104
        Me.lstVw_Insurance.UseCompatibleStateImageBehavior = false
        Me.lstVw_Insurance.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "Insurance Plan"
        Me.ColumnHeader17.Width = 277
        '
        'btnClearInsurance
        '
        Me.btnClearInsurance.BackgroundImage = CType(resources.GetObject("btnClearInsurance.BackgroundImage"),System.Drawing.Image)
        Me.btnClearInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearInsurance.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearInsurance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearInsurance.Image = CType(resources.GetObject("btnClearInsurance.Image"),System.Drawing.Image)
        Me.btnClearInsurance.Location = New System.Drawing.Point(999, 313)
        Me.btnClearInsurance.Name = "btnClearInsurance"
        Me.btnClearInsurance.Size = New System.Drawing.Size(24, 24)
        Me.btnClearInsurance.TabIndex = 103
        Me.ToolTip1.SetToolTip(Me.btnClearInsurance, "Clear Insurance Plans")
        Me.btnClearInsurance.UseVisualStyleBackColor = true
        '
        'btnInsurance
        '
        Me.btnInsurance.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnInsurance.BackgroundImage = CType(resources.GetObject("btnInsurance.BackgroundImage"),System.Drawing.Image)
        Me.btnInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInsurance.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnInsurance.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnInsurance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInsurance.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnInsurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnInsurance.Image = CType(resources.GetObject("btnInsurance.Image"),System.Drawing.Image)
        Me.btnInsurance.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnInsurance.Location = New System.Drawing.Point(999, 259)
        Me.btnInsurance.Name = "btnInsurance"
        Me.btnInsurance.Size = New System.Drawing.Size(24, 24)
        Me.btnInsurance.TabIndex = 102
        Me.btnInsurance.Tag = "UnSelected"
        Me.btnInsurance.Text = "      Drugs"
        Me.ToolTip1.SetToolTip(Me.btnInsurance, "Select Insurance Plans")
        Me.btnInsurance.UseVisualStyleBackColor = false
        '
        'btnRemoveSelectedICD10
        '
        Me.btnRemoveSelectedICD10.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedICD10.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedICD10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedICD10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedICD10.Image = CType(resources.GetObject("btnRemoveSelectedICD10.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedICD10.Location = New System.Drawing.Point(660, 162)
        Me.btnRemoveSelectedICD10.Name = "btnRemoveSelectedICD10"
        Me.btnRemoveSelectedICD10.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedICD10.TabIndex = 101
        Me.btnRemoveSelectedICD10.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedICD10, "Remove selected ICD10")
        Me.btnRemoveSelectedICD10.UseVisualStyleBackColor = true
        '
        'btnClearICD10
        '
        Me.btnClearICD10.BackgroundImage = CType(resources.GetObject("btnClearICD10.BackgroundImage"),System.Drawing.Image)
        Me.btnClearICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearICD10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearICD10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearICD10.Image = CType(resources.GetObject("btnClearICD10.Image"),System.Drawing.Image)
        Me.btnClearICD10.Location = New System.Drawing.Point(660, 189)
        Me.btnClearICD10.Name = "btnClearICD10"
        Me.btnClearICD10.Size = New System.Drawing.Size(24, 24)
        Me.btnClearICD10.TabIndex = 99
        Me.ToolTip1.SetToolTip(Me.btnClearICD10, "Clear ICD10")
        Me.btnClearICD10.UseVisualStyleBackColor = true
        '
        'lstVw_ICD10
        '
        Me.lstVw_ICD10.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader15})
        Me.lstVw_ICD10.Location = New System.Drawing.Point(381, 134)
        Me.lstVw_ICD10.MultiSelect = false
        Me.lstVw_ICD10.Name = "lstVw_ICD10"
        Me.lstVw_ICD10.Size = New System.Drawing.Size(275, 119)
        Me.lstVw_ICD10.TabIndex = 100
        Me.lstVw_ICD10.UseCompatibleStateImageBehavior = false
        Me.lstVw_ICD10.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "ICD10"
        Me.ColumnHeader15.Width = 277
        '
        'btnICD10
        '
        Me.btnICD10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnICD10.BackgroundImage = CType(resources.GetObject("btnICD10.BackgroundImage"),System.Drawing.Image)
        Me.btnICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnICD10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD10.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnICD10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnICD10.Image = CType(resources.GetObject("btnICD10.Image"),System.Drawing.Image)
        Me.btnICD10.Location = New System.Drawing.Point(660, 135)
        Me.btnICD10.Name = "btnICD10"
        Me.btnICD10.Size = New System.Drawing.Size(24, 24)
        Me.btnICD10.TabIndex = 98
        Me.btnICD10.Tag = "UnSelected"
        Me.btnICD10.Text = "      ICD9"
        Me.ToolTip1.SetToolTip(Me.btnICD10, "Select ICD10")
        Me.btnICD10.UseVisualStyleBackColor = false
        '
        'btnRemoveSelectedDrug
        '
        Me.btnRemoveSelectedDrug.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedDrug.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedDrug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedDrug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedDrug.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedDrug.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedDrug.Image = CType(resources.GetObject("btnRemoveSelectedDrug.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedDrug.Location = New System.Drawing.Point(330, 286)
        Me.btnRemoveSelectedDrug.Name = "btnRemoveSelectedDrug"
        Me.btnRemoveSelectedDrug.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedDrug.TabIndex = 97
        Me.btnRemoveSelectedDrug.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedDrug, "Remove selected drugs ")
        Me.btnRemoveSelectedDrug.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedOrders
        '
        Me.btnRemoveSelectedOrders.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedOrders.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedOrders.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedOrders.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedOrders.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedOrders.Image = CType(resources.GetObject("btnRemoveSelectedOrders.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedOrders.Location = New System.Drawing.Point(995, 37)
        Me.btnRemoveSelectedOrders.Name = "btnRemoveSelectedOrders"
        Me.btnRemoveSelectedOrders.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedOrders.TabIndex = 97
        Me.btnRemoveSelectedOrders.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedOrders, "Remove selected orders")
        Me.btnRemoveSelectedOrders.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedICD9
        '
        Me.btnRemoveSelectedICD9.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedICD9.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedICD9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedICD9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedICD9.Image = CType(resources.GetObject("btnRemoveSelectedICD9.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedICD9.Location = New System.Drawing.Point(660, 37)
        Me.btnRemoveSelectedICD9.Name = "btnRemoveSelectedICD9"
        Me.btnRemoveSelectedICD9.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedICD9.TabIndex = 97
        Me.btnRemoveSelectedICD9.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedICD9, "Remove selected ICD9")
        Me.btnRemoveSelectedICD9.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedLab
        '
        Me.btnRemoveSelectedLab.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedLab.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedLab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedLab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedLab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedLab.Image = CType(resources.GetObject("btnRemoveSelectedLab.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedLab.Location = New System.Drawing.Point(662, 286)
        Me.btnRemoveSelectedLab.Name = "btnRemoveSelectedLab"
        Me.btnRemoveSelectedLab.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedLab.TabIndex = 97
        Me.btnRemoveSelectedLab.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedLab, "Remove selected Labs")
        Me.btnRemoveSelectedLab.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedCPT
        '
        Me.btnRemoveSelectedCPT.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedCPT.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedCPT.Image = CType(resources.GetObject("btnRemoveSelectedCPT.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedCPT.Location = New System.Drawing.Point(328, 161)
        Me.btnRemoveSelectedCPT.Name = "btnRemoveSelectedCPT"
        Me.btnRemoveSelectedCPT.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedCPT.TabIndex = 97
        Me.btnRemoveSelectedCPT.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedCPT, "Remove selected CPT")
        Me.btnRemoveSelectedCPT.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedHistory
        '
        Me.btnRemoveSelectedHistory.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedHistory.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedHistory.Image = CType(resources.GetObject("btnRemoveSelectedHistory.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedHistory.Location = New System.Drawing.Point(328, 36)
        Me.btnRemoveSelectedHistory.Name = "btnRemoveSelectedHistory"
        Me.btnRemoveSelectedHistory.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedHistory.TabIndex = 97
        Me.btnRemoveSelectedHistory.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedHistory, "Remove selected history")
        Me.btnRemoveSelectedHistory.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedSnomedCode
        '
        Me.btnRemoveSelectedSnomedCode.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedSnomedCode.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedSnomedCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedSnomedCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedSnomedCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedSnomedCode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedSnomedCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedSnomedCode.Image = CType(resources.GetObject("btnRemoveSelectedSnomedCode.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedSnomedCode.Location = New System.Drawing.Point(995, 161)
        Me.btnRemoveSelectedSnomedCode.Name = "btnRemoveSelectedSnomedCode"
        Me.btnRemoveSelectedSnomedCode.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedSnomedCode.TabIndex = 96
        Me.btnRemoveSelectedSnomedCode.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedSnomedCode, "Remove selected snomed codes")
        Me.btnRemoveSelectedSnomedCode.UseVisualStyleBackColor = true
        '
        'btnClearSnomed
        '
        Me.btnClearSnomed.BackgroundImage = CType(resources.GetObject("btnClearSnomed.BackgroundImage"),System.Drawing.Image)
        Me.btnClearSnomed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSnomed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearSnomed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSnomed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearSnomed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSnomed.Image = CType(resources.GetObject("btnClearSnomed.Image"),System.Drawing.Image)
        Me.btnClearSnomed.Location = New System.Drawing.Point(995, 188)
        Me.btnClearSnomed.Name = "btnClearSnomed"
        Me.btnClearSnomed.Size = New System.Drawing.Size(24, 24)
        Me.btnClearSnomed.TabIndex = 96
        Me.ToolTip1.SetToolTip(Me.btnClearSnomed, "Clear snomed codes")
        Me.btnClearSnomed.UseVisualStyleBackColor = true
        '
        'btnSnomed
        '
        Me.btnSnomed.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnSnomed.BackgroundImage = CType(resources.GetObject("btnSnomed.BackgroundImage"),System.Drawing.Image)
        Me.btnSnomed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSnomed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSnomed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSnomed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSnomed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSnomed.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnSnomed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSnomed.Image = CType(resources.GetObject("btnSnomed.Image"),System.Drawing.Image)
        Me.btnSnomed.Location = New System.Drawing.Point(995, 134)
        Me.btnSnomed.Name = "btnSnomed"
        Me.btnSnomed.Size = New System.Drawing.Size(24, 24)
        Me.btnSnomed.TabIndex = 95
        Me.btnSnomed.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnSnomed, "Add Snomed Code")
        Me.btnSnomed.UseVisualStyleBackColor = false
        '
        'lstVw_SnoMed
        '
        Me.lstVw_SnoMed.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader13})
        Me.lstVw_SnoMed.Location = New System.Drawing.Point(714, 134)
        Me.lstVw_SnoMed.MultiSelect = false
        Me.lstVw_SnoMed.Name = "lstVw_SnoMed"
        Me.lstVw_SnoMed.Size = New System.Drawing.Size(275, 119)
        Me.lstVw_SnoMed.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstVw_SnoMed.TabIndex = 94
        Me.lstVw_SnoMed.UseCompatibleStateImageBehavior = false
        Me.lstVw_SnoMed.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Snomed"
        Me.ColumnHeader13.Width = 272
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label36.Location = New System.Drawing.Point(1, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1075, 1)
        Me.Label36.TabIndex = 93
        Me.Label36.Text = "label2"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label35.Location = New System.Drawing.Point(1, 395)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1075, 1)
        Me.Label35.TabIndex = 92
        Me.Label35.Text = "label2"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label34.Location = New System.Drawing.Point(1076, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 396)
        Me.Label34.TabIndex = 91
        Me.Label34.Text = "label4"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label33.Location = New System.Drawing.Point(0, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 396)
        Me.Label33.TabIndex = 90
        Me.Label33.Text = "label4"
        '
        'btnClearICD
        '
        Me.btnClearICD.BackgroundImage = CType(resources.GetObject("btnClearICD.BackgroundImage"),System.Drawing.Image)
        Me.btnClearICD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearICD.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearICD.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearICD.Image = CType(resources.GetObject("btnClearICD.Image"),System.Drawing.Image)
        Me.btnClearICD.Location = New System.Drawing.Point(660, 64)
        Me.btnClearICD.Name = "btnClearICD"
        Me.btnClearICD.Size = New System.Drawing.Size(24, 24)
        Me.btnClearICD.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.btnClearICD, "Clear ICD9")
        Me.btnClearICD.UseVisualStyleBackColor = true
        '
        'btnClearCPT
        '
        Me.btnClearCPT.BackgroundImage = CType(resources.GetObject("btnClearCPT.BackgroundImage"),System.Drawing.Image)
        Me.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCPT.Image = CType(resources.GetObject("btnClearCPT.Image"),System.Drawing.Image)
        Me.btnClearCPT.Location = New System.Drawing.Point(328, 188)
        Me.btnClearCPT.Name = "btnClearCPT"
        Me.btnClearCPT.Size = New System.Drawing.Size(24, 24)
        Me.btnClearCPT.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.btnClearCPT, "Clear CPT")
        Me.btnClearCPT.UseVisualStyleBackColor = true
        '
        'lstVw_Orders
        '
        Me.lstVw_Orders.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4})
        Me.lstVw_Orders.Location = New System.Drawing.Point(714, 9)
        Me.lstVw_Orders.MultiSelect = false
        Me.lstVw_Orders.Name = "lstVw_Orders"
        Me.lstVw_Orders.Size = New System.Drawing.Size(275, 119)
        Me.lstVw_Orders.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstVw_Orders.TabIndex = 83
        Me.lstVw_Orders.UseCompatibleStateImageBehavior = false
        Me.lstVw_Orders.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Order Templates"
        Me.ColumnHeader4.Width = 272
        '
        'lstVw_Drugs
        '
        Me.lstVw_Drugs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5})
        Me.lstVw_Drugs.Location = New System.Drawing.Point(48, 259)
        Me.lstVw_Drugs.MultiSelect = false
        Me.lstVw_Drugs.Name = "lstVw_Drugs"
        Me.lstVw_Drugs.Size = New System.Drawing.Size(275, 119)
        Me.lstVw_Drugs.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstVw_Drugs.TabIndex = 82
        Me.lstVw_Drugs.UseCompatibleStateImageBehavior = false
        Me.lstVw_Drugs.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Drugs"
        Me.ColumnHeader5.Width = 277
        '
        'btnClearHistory
        '
        Me.btnClearHistory.BackgroundImage = CType(resources.GetObject("btnClearHistory.BackgroundImage"),System.Drawing.Image)
        Me.btnClearHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearHistory.Image = CType(resources.GetObject("btnClearHistory.Image"),System.Drawing.Image)
        Me.btnClearHistory.Location = New System.Drawing.Point(328, 63)
        Me.btnClearHistory.Name = "btnClearHistory"
        Me.btnClearHistory.Size = New System.Drawing.Size(24, 24)
        Me.btnClearHistory.TabIndex = 72
        Me.ToolTip1.SetToolTip(Me.btnClearHistory, "Clear History")
        Me.btnClearHistory.UseVisualStyleBackColor = true
        '
        'lstVw_Lab
        '
        Me.lstVw_Lab.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7})
        Me.lstVw_Lab.Location = New System.Drawing.Point(381, 259)
        Me.lstVw_Lab.MultiSelect = false
        Me.lstVw_Lab.Name = "lstVw_Lab"
        Me.lstVw_Lab.Size = New System.Drawing.Size(275, 119)
        Me.lstVw_Lab.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstVw_Lab.TabIndex = 81
        Me.lstVw_Lab.UseCompatibleStateImageBehavior = false
        Me.lstVw_Lab.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Orders"
        Me.ColumnHeader7.Width = 277
        '
        'lstVw_ICD9
        '
        Me.lstVw_ICD9.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lstVw_COL_ICDCode})
        Me.lstVw_ICD9.Location = New System.Drawing.Point(381, 9)
        Me.lstVw_ICD9.MultiSelect = false
        Me.lstVw_ICD9.Name = "lstVw_ICD9"
        Me.lstVw_ICD9.Size = New System.Drawing.Size(275, 119)
        Me.lstVw_ICD9.TabIndex = 75
        Me.lstVw_ICD9.UseCompatibleStateImageBehavior = false
        Me.lstVw_ICD9.View = System.Windows.Forms.View.Details
        '
        'lstVw_COL_ICDCode
        '
        Me.lstVw_COL_ICDCode.Text = "ICD9"
        Me.lstVw_COL_ICDCode.Width = 277
        '
        'btnClearOrders
        '
        Me.btnClearOrders.BackgroundImage = CType(resources.GetObject("btnClearOrders.BackgroundImage"),System.Drawing.Image)
        Me.btnClearOrders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearOrders.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearOrders.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearOrders.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearOrders.Image = CType(resources.GetObject("btnClearOrders.Image"),System.Drawing.Image)
        Me.btnClearOrders.Location = New System.Drawing.Point(995, 64)
        Me.btnClearOrders.Name = "btnClearOrders"
        Me.btnClearOrders.Size = New System.Drawing.Size(24, 24)
        Me.btnClearOrders.TabIndex = 80
        Me.ToolTip1.SetToolTip(Me.btnClearOrders, "Clear Orders")
        Me.btnClearOrders.UseVisualStyleBackColor = true
        '
        'lstVw_CPT
        '
        Me.lstVw_CPT.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lstVw_CPT.Location = New System.Drawing.Point(48, 134)
        Me.lstVw_CPT.MultiSelect = false
        Me.lstVw_CPT.Name = "lstVw_CPT"
        Me.lstVw_CPT.Size = New System.Drawing.Size(275, 119)
        Me.lstVw_CPT.TabIndex = 76
        Me.lstVw_CPT.UseCompatibleStateImageBehavior = false
        Me.lstVw_CPT.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "CPT"
        Me.ColumnHeader1.Width = 277
        '
        'btnClearDrugs
        '
        Me.btnClearDrugs.BackgroundImage = CType(resources.GetObject("btnClearDrugs.BackgroundImage"),System.Drawing.Image)
        Me.btnClearDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearDrugs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearDrugs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearDrugs.Image = CType(resources.GetObject("btnClearDrugs.Image"),System.Drawing.Image)
        Me.btnClearDrugs.Location = New System.Drawing.Point(330, 313)
        Me.btnClearDrugs.Name = "btnClearDrugs"
        Me.btnClearDrugs.Size = New System.Drawing.Size(24, 24)
        Me.btnClearDrugs.TabIndex = 78
        Me.ToolTip1.SetToolTip(Me.btnClearDrugs, "Clear Drugs")
        Me.btnClearDrugs.UseVisualStyleBackColor = true
        '
        'lstVw_History
        '
        Me.lstVw_History.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3})
        Me.lstVw_History.Location = New System.Drawing.Point(48, 9)
        Me.lstVw_History.MultiSelect = false
        Me.lstVw_History.Name = "lstVw_History"
        Me.lstVw_History.Size = New System.Drawing.Size(275, 119)
        Me.lstVw_History.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstVw_History.TabIndex = 77
        Me.lstVw_History.UseCompatibleStateImageBehavior = false
        Me.lstVw_History.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "History"
        Me.ColumnHeader3.Width = 272
        '
        'btnClearLab
        '
        Me.btnClearLab.BackgroundImage = CType(resources.GetObject("btnClearLab.BackgroundImage"),System.Drawing.Image)
        Me.btnClearLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearLab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearLab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearLab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearLab.Image = CType(resources.GetObject("btnClearLab.Image"),System.Drawing.Image)
        Me.btnClearLab.Location = New System.Drawing.Point(662, 313)
        Me.btnClearLab.Name = "btnClearLab"
        Me.btnClearLab.Size = New System.Drawing.Size(24, 24)
        Me.btnClearLab.TabIndex = 79
        Me.ToolTip1.SetToolTip(Me.btnClearLab, "Clear Lab")
        Me.btnClearLab.UseVisualStyleBackColor = true
        '
        'btnHistory
        '
        Me.btnHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnHistory.BackgroundImage = CType(resources.GetObject("btnHistory.BackgroundImage"),System.Drawing.Image)
        Me.btnHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistory.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnHistory.Image = CType(resources.GetObject("btnHistory.Image"),System.Drawing.Image)
        Me.btnHistory.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHistory.Location = New System.Drawing.Point(328, 9)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(24, 24)
        Me.btnHistory.TabIndex = 0
        Me.btnHistory.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnHistory, "Select History")
        Me.btnHistory.UseVisualStyleBackColor = false
        '
        'btnRadiology
        '
        Me.btnRadiology.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnRadiology.BackgroundImage = CType(resources.GetObject("btnRadiology.BackgroundImage"),System.Drawing.Image)
        Me.btnRadiology.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiology.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRadiology.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRadiology.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRadiology.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiology.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnRadiology.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRadiology.Image = CType(resources.GetObject("btnRadiology.Image"),System.Drawing.Image)
        Me.btnRadiology.Location = New System.Drawing.Point(995, 10)
        Me.btnRadiology.Name = "btnRadiology"
        Me.btnRadiology.Size = New System.Drawing.Size(24, 24)
        Me.btnRadiology.TabIndex = 0
        Me.btnRadiology.Tag = "UnSelected"
        Me.btnRadiology.Text = "      Orders"
        Me.btnRadiology.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.btnRadiology, "Select Orders")
        Me.btnRadiology.UseVisualStyleBackColor = false
        '
        'btnDrugs
        '
        Me.btnDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnDrugs.BackgroundImage = CType(resources.GetObject("btnDrugs.BackgroundImage"),System.Drawing.Image)
        Me.btnDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnDrugs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnDrugs.Image = CType(resources.GetObject("btnDrugs.Image"),System.Drawing.Image)
        Me.btnDrugs.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDrugs.Location = New System.Drawing.Point(330, 259)
        Me.btnDrugs.Name = "btnDrugs"
        Me.btnDrugs.Size = New System.Drawing.Size(24, 24)
        Me.btnDrugs.TabIndex = 0
        Me.btnDrugs.Tag = "UnSelected"
        Me.btnDrugs.Text = "      Drugs"
        Me.ToolTip1.SetToolTip(Me.btnDrugs, "Select Drugs")
        Me.btnDrugs.UseVisualStyleBackColor = false
        '
        'btnLabs
        '
        Me.btnLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnLabs.BackgroundImage = CType(resources.GetObject("btnLabs.BackgroundImage"),System.Drawing.Image)
        Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnLabs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnLabs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnLabs.Image = CType(resources.GetObject("btnLabs.Image"),System.Drawing.Image)
        Me.btnLabs.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLabs.Location = New System.Drawing.Point(662, 259)
        Me.btnLabs.Name = "btnLabs"
        Me.btnLabs.Size = New System.Drawing.Size(24, 24)
        Me.btnLabs.TabIndex = 0
        Me.btnLabs.Tag = "UnSelected"
        Me.btnLabs.Text = "      Lab"
        Me.ToolTip1.SetToolTip(Me.btnLabs, "Select Lab")
        Me.btnLabs.UseVisualStyleBackColor = false
        '
        'btnICD9
        '
        Me.btnICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnICD9.BackgroundImage = CType(resources.GetObject("btnICD9.BackgroundImage"),System.Drawing.Image)
        Me.btnICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnICD9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD9.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnICD9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnICD9.Image = CType(resources.GetObject("btnICD9.Image"),System.Drawing.Image)
        Me.btnICD9.Location = New System.Drawing.Point(660, 10)
        Me.btnICD9.Name = "btnICD9"
        Me.btnICD9.Size = New System.Drawing.Size(24, 24)
        Me.btnICD9.TabIndex = 0
        Me.btnICD9.Tag = "UnSelected"
        Me.btnICD9.Text = "      ICD9"
        Me.ToolTip1.SetToolTip(Me.btnICD9, "Select ICD9")
        Me.btnICD9.UseVisualStyleBackColor = false
        '
        'btnCPT
        '
        Me.btnCPT.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnCPT.BackgroundImage = CType(resources.GetObject("btnCPT.BackgroundImage"),System.Drawing.Image)
        Me.btnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPT.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnCPT.Image = CType(resources.GetObject("btnCPT.Image"),System.Drawing.Image)
        Me.btnCPT.Location = New System.Drawing.Point(328, 134)
        Me.btnCPT.Name = "btnCPT"
        Me.btnCPT.Size = New System.Drawing.Size(24, 24)
        Me.btnCPT.TabIndex = 0
        Me.btnCPT.Tag = "UnSelected"
        Me.btnCPT.Text = "      CPT"
        Me.ToolTip1.SetToolTip(Me.btnCPT, "Select CPT")
        Me.btnCPT.UseVisualStyleBackColor = false
        '
        'btnproblemlist
        '
        Me.btnproblemlist.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnproblemlist.BackgroundImage = CType(resources.GetObject("btnproblemlist.BackgroundImage"),System.Drawing.Image)
        Me.btnproblemlist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnproblemlist.FlatAppearance.BorderSize = 0
        Me.btnproblemlist.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnproblemlist.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnproblemlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnproblemlist.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnproblemlist.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnproblemlist.Image = CType(resources.GetObject("btnproblemlist.Image"),System.Drawing.Image)
        Me.btnproblemlist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnproblemlist.Location = New System.Drawing.Point(1028, 366)
        Me.btnproblemlist.Name = "btnproblemlist"
        Me.btnproblemlist.Size = New System.Drawing.Size(45, 25)
        Me.btnproblemlist.TabIndex = 0
        Me.btnproblemlist.Tag = "UnSelected"
        Me.btnproblemlist.Text = "      Problem List"
        Me.btnproblemlist.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnproblemlist.UseVisualStyleBackColor = false
        '
        'btnDemographics
        '
        Me.btnDemographics.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnDemographics.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnDemographics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDemographics.FlatAppearance.BorderSize = 0
        Me.btnDemographics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnDemographics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnDemographics.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDemographics.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnDemographics.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnDemographics.Image = CType(resources.GetObject("btnDemographics.Image"),System.Drawing.Image)
        Me.btnDemographics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDemographics.Location = New System.Drawing.Point(1028, 331)
        Me.btnDemographics.Name = "btnDemographics"
        Me.btnDemographics.Size = New System.Drawing.Size(45, 25)
        Me.btnDemographics.TabIndex = 0
        Me.btnDemographics.Tag = "UnSelected"
        Me.btnDemographics.Text = "      Demographics and Vitals"
        Me.btnDemographics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDemographics.UseVisualStyleBackColor = false
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Panel25)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(0, 269)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel14.Size = New System.Drawing.Size(1077, 28)
        Me.Panel14.TabIndex = 47
        '
        'Panel25
        '
        Me.Panel25.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel25.Controls.Add(Me.Label202)
        Me.Panel25.Controls.Add(Me.Label204)
        Me.Panel25.Controls.Add(Me.Label214)
        Me.Panel25.Controls.Add(Me.Label236)
        Me.Panel25.Controls.Add(Me.Label241)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel25.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel25.Location = New System.Drawing.Point(0, 0)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(1077, 25)
        Me.Panel25.TabIndex = 44
        '
        'Label202
        '
        Me.Label202.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label202.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label202.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label202.Location = New System.Drawing.Point(1, 24)
        Me.Label202.Name = "Label202"
        Me.Label202.Size = New System.Drawing.Size(1075, 1)
        Me.Label202.TabIndex = 13
        Me.Label202.Text = "label2"
        '
        'Label204
        '
        Me.Label204.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label204.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label204.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label204.Location = New System.Drawing.Point(0, 1)
        Me.Label204.Name = "Label204"
        Me.Label204.Size = New System.Drawing.Size(1, 24)
        Me.Label204.TabIndex = 12
        Me.Label204.Text = "label4"
        '
        'Label214
        '
        Me.Label214.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label214.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label214.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label214.Location = New System.Drawing.Point(1076, 1)
        Me.Label214.Name = "Label214"
        Me.Label214.Size = New System.Drawing.Size(1, 24)
        Me.Label214.TabIndex = 11
        Me.Label214.Text = "label3"
        '
        'Label236
        '
        Me.Label236.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label236.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label236.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label236.Location = New System.Drawing.Point(0, 0)
        Me.Label236.Name = "Label236"
        Me.Label236.Size = New System.Drawing.Size(1077, 1)
        Me.Label236.TabIndex = 10
        Me.Label236.Text = "label1"
        '
        'Label241
        '
        Me.Label241.BackColor = System.Drawing.Color.Transparent
        Me.Label241.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label241.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label241.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label241.ForeColor = System.Drawing.Color.White
        Me.Label241.Image = CType(resources.GetObject("Label241.Image"),System.Drawing.Image)
        Me.Label241.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label241.Location = New System.Drawing.Point(0, 0)
        Me.Label241.Name = "Label241"
        Me.Label241.Size = New System.Drawing.Size(1077, 25)
        Me.Label241.TabIndex = 9
        Me.Label241.Text = "      Other Triggers"
        Me.Label241.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlVitals
        '
        Me.pnlVitals.BackColor = System.Drawing.Color.Transparent
        Me.pnlVitals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlVitals.Controls.Add(Me.Label47)
        Me.pnlVitals.Controls.Add(Me.Label40)
        Me.pnlVitals.Controls.Add(Me.Label46)
        Me.pnlVitals.Controls.Add(Me.label52)
        Me.pnlVitals.Controls.Add(Me.Label45)
        Me.pnlVitals.Controls.Add(Me.Label44)
        Me.pnlVitals.Controls.Add(Me.Label38)
        Me.pnlVitals.Controls.Add(Me.label51)
        Me.pnlVitals.Controls.Add(Me.Label30)
        Me.pnlVitals.Controls.Add(Me.Label254)
        Me.pnlVitals.Controls.Add(Me.Label260)
        Me.pnlVitals.Controls.Add(Me.Label258)
        Me.pnlVitals.Controls.Add(Me.Label256)
        Me.pnlVitals.Controls.Add(Me.Label253)
        Me.pnlVitals.Controls.Add(Me.Label252)
        Me.pnlVitals.Controls.Add(Me.Label251)
        Me.pnlVitals.Controls.Add(Me.Label250)
        Me.pnlVitals.Controls.Add(Me.Label249)
        Me.pnlVitals.Controls.Add(Me.Label259)
        Me.pnlVitals.Controls.Add(Me.Label257)
        Me.pnlVitals.Controls.Add(Me.Label255)
        Me.pnlVitals.Controls.Add(Me.Label248)
        Me.pnlVitals.Controls.Add(Me.Label111)
        Me.pnlVitals.Controls.Add(Me.Label112)
        Me.pnlVitals.Controls.Add(Me.Label113)
        Me.pnlVitals.Controls.Add(Me.Label114)
        Me.pnlVitals.Controls.Add(Me.txtHeightMin)
        Me.pnlVitals.Controls.Add(Me.Label11)
        Me.pnlVitals.Controls.Add(Me.txtHeightMaxInch)
        Me.pnlVitals.Controls.Add(Me.Label12)
        Me.pnlVitals.Controls.Add(Me.Label10)
        Me.pnlVitals.Controls.Add(Me.txtHeightMinInch)
        Me.pnlVitals.Controls.Add(Me.Label9)
        Me.pnlVitals.Controls.Add(Me.txtPulseOXmax)
        Me.pnlVitals.Controls.Add(Me.lblPulseOXMax)
        Me.pnlVitals.Controls.Add(Me.txtPulseOXmin)
        Me.pnlVitals.Controls.Add(Me.lblPulseOXMin)
        Me.pnlVitals.Controls.Add(Me.txtPulseMax)
        Me.pnlVitals.Controls.Add(Me.lblPulseMax)
        Me.pnlVitals.Controls.Add(Me.txtPulseMin)
        Me.pnlVitals.Controls.Add(Me.lblPulseMin)
        Me.pnlVitals.Controls.Add(Me.txtTemperatureMax)
        Me.pnlVitals.Controls.Add(Me.lblTempratureMax)
        Me.pnlVitals.Controls.Add(Me.txtBMImax)
        Me.pnlVitals.Controls.Add(Me.lblBMImax)
        Me.pnlVitals.Controls.Add(Me.txtBMImin)
        Me.pnlVitals.Controls.Add(Me.lblBMImin)
        Me.pnlVitals.Controls.Add(Me.txtWeightMax)
        Me.pnlVitals.Controls.Add(Me.Label28)
        Me.pnlVitals.Controls.Add(Me.lblWeightMax)
        Me.pnlVitals.Controls.Add(Me.txtHeightMax)
        Me.pnlVitals.Controls.Add(Me.txtBPstandingMaxTo)
        Me.pnlVitals.Controls.Add(Me.txtBPstandingMinTo)
        Me.pnlVitals.Controls.Add(Me.txtBPstandingMax)
        Me.pnlVitals.Controls.Add(Me.txtBPstandingMin)
        Me.pnlVitals.Controls.Add(Me.txtBPsettingMaxTo)
        Me.pnlVitals.Controls.Add(Me.txtBPsettingMinTo)
        Me.pnlVitals.Controls.Add(Me.txtBPsettingMax)
        Me.pnlVitals.Controls.Add(Me.txtBPsettingMin)
        Me.pnlVitals.Controls.Add(Me.lblBPStandingMax)
        Me.pnlVitals.Controls.Add(Me.lblBPStandingMin)
        Me.pnlVitals.Controls.Add(Me.Label26)
        Me.pnlVitals.Controls.Add(Me.Label29)
        Me.pnlVitals.Controls.Add(Me.lblBPSittingMax)
        Me.pnlVitals.Controls.Add(Me.lblBPSittingMin)
        Me.pnlVitals.Controls.Add(Me.txtTemperatureMin)
        Me.pnlVitals.Controls.Add(Me.txtWeightMin)
        Me.pnlVitals.Controls.Add(Me.lblWeightMin)
        Me.pnlVitals.Controls.Add(Me.lblTempratureMin)
        Me.pnlVitals.Controls.Add(Me.lblHeightMin)
        Me.pnlVitals.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlVitals.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlVitals.Location = New System.Drawing.Point(0, 144)
        Me.pnlVitals.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlVitals.Name = "pnlVitals"
        Me.pnlVitals.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlVitals.Size = New System.Drawing.Size(1077, 125)
        Me.pnlVitals.TabIndex = 1
        '
        'Label47
        '
        Me.Label47.AutoSize = true
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label47.Location = New System.Drawing.Point(626, 106)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(43, 11)
        Me.Label47.TabIndex = 53
        Me.Label47.Text = "(Systolic)"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.AutoSize = true
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label40.Location = New System.Drawing.Point(234, 106)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(43, 11)
        Me.Label40.TabIndex = 53
        Me.Label40.Text = "(Systolic)"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label46
        '
        Me.Label46.AutoSize = true
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label46.Location = New System.Drawing.Point(523, 106)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(43, 11)
        Me.Label46.TabIndex = 53
        Me.Label46.Text = "(Systolic)"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label52
        '
        Me.label52.AutoSize = true
        Me.label52.BackColor = System.Drawing.Color.Transparent
        Me.label52.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.label52.Location = New System.Drawing.Point(131, 106)
        Me.label52.Name = "label52"
        Me.label52.Size = New System.Drawing.Size(43, 11)
        Me.label52.TabIndex = 53
        Me.label52.Text = "(Systolic)"
        Me.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label45
        '
        Me.Label45.AutoSize = true
        Me.Label45.BackColor = System.Drawing.Color.Transparent
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label45.Location = New System.Drawing.Point(675, 106)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(45, 11)
        Me.Label45.TabIndex = 54
        Me.Label45.Text = "(Diastolic)"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label44
        '
        Me.Label44.AutoSize = true
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label44.Location = New System.Drawing.Point(572, 106)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(45, 11)
        Me.Label44.TabIndex = 54
        Me.Label44.Text = "(Diastolic)"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.AutoSize = true
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label38.Location = New System.Drawing.Point(282, 106)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(45, 11)
        Me.Label38.TabIndex = 54
        Me.Label38.Text = "(Diastolic)"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label51
        '
        Me.label51.AutoSize = true
        Me.label51.BackColor = System.Drawing.Color.Transparent
        Me.label51.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.label51.Location = New System.Drawing.Point(180, 106)
        Me.label51.Name = "label51"
        Me.label51.Size = New System.Drawing.Size(45, 11)
        Me.label51.TabIndex = 54
        Me.label51.Text = "(Diastolic)"
        Me.label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.AutoSize = true
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label30.Location = New System.Drawing.Point(171, 85)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(12, 14)
        Me.Label30.TabIndex = 52
        Me.Label30.Text = "/"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label254
        '
        Me.Label254.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label254.AutoSize = true
        Me.Label254.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label254.ForeColor = System.Drawing.Color.Red
        Me.Label254.Location = New System.Drawing.Point(627, 5)
        Me.Label254.Name = "Label254"
        Me.Label254.Size = New System.Drawing.Size(20, 13)
        Me.Label254.TabIndex = 51
        Me.Label254.Text = "lbs"
        '
        'Label260
        '
        Me.Label260.AutoSize = true
        Me.Label260.BackColor = System.Drawing.Color.Transparent
        Me.Label260.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label260.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label260.Location = New System.Drawing.Point(828, 23)
        Me.Label260.Name = "Label260"
        Me.Label260.Size = New System.Drawing.Size(57, 14)
        Me.Label260.TabIndex = 50
        Me.Label260.Text = "between"
        Me.Label260.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label258
        '
        Me.Label258.AutoSize = true
        Me.Label258.BackColor = System.Drawing.Color.Transparent
        Me.Label258.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label258.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label258.Location = New System.Drawing.Point(467, 85)
        Me.Label258.Name = "Label258"
        Me.Label258.Size = New System.Drawing.Size(57, 14)
        Me.Label258.TabIndex = 50
        Me.Label258.Text = "between"
        Me.Label258.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label256
        '
        Me.Label256.AutoSize = true
        Me.Label256.BackColor = System.Drawing.Color.Transparent
        Me.Label256.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label256.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label256.Location = New System.Drawing.Point(466, 54)
        Me.Label256.Name = "Label256"
        Me.Label256.Size = New System.Drawing.Size(57, 14)
        Me.Label256.TabIndex = 50
        Me.Label256.Text = "between"
        Me.Label256.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label253
        '
        Me.Label253.AutoSize = true
        Me.Label253.BackColor = System.Drawing.Color.Transparent
        Me.Label253.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label253.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label253.Location = New System.Drawing.Point(469, 23)
        Me.Label253.Name = "Label253"
        Me.Label253.Size = New System.Drawing.Size(57, 14)
        Me.Label253.TabIndex = 50
        Me.Label253.Text = "between"
        Me.Label253.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label252
        '
        Me.Label252.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label252.AutoSize = true
        Me.Label252.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label252.ForeColor = System.Drawing.Color.Red
        Me.Label252.Location = New System.Drawing.Point(540, 5)
        Me.Label252.Name = "Label252"
        Me.Label252.Size = New System.Drawing.Size(20, 13)
        Me.Label252.TabIndex = 49
        Me.Label252.Text = "lbs"
        '
        'Label251
        '
        Me.Label251.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label251.AutoSize = true
        Me.Label251.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label251.ForeColor = System.Drawing.Color.Red
        Me.Label251.Location = New System.Drawing.Point(223, 5)
        Me.Label251.Name = "Label251"
        Me.Label251.Size = New System.Drawing.Size(61, 13)
        Me.Label251.TabIndex = 48
        Me.Label251.Text = " ft'      inch''"
        '
        'Label250
        '
        Me.Label250.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label250.AutoSize = true
        Me.Label250.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label250.ForeColor = System.Drawing.Color.Red
        Me.Label250.Location = New System.Drawing.Point(135, 5)
        Me.Label250.Name = "Label250"
        Me.Label250.Size = New System.Drawing.Size(61, 13)
        Me.Label250.TabIndex = 32
        Me.Label250.Text = " ft'      inch''"
        '
        'Label249
        '
        Me.Label249.AutoSize = true
        Me.Label249.BackColor = System.Drawing.Color.Transparent
        Me.Label249.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label249.Location = New System.Drawing.Point(203, 23)
        Me.Label249.Name = "Label249"
        Me.Label249.Size = New System.Drawing.Size(19, 14)
        Me.Label249.TabIndex = 32
        Me.Label249.Text = "to"
        Me.Label249.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label259
        '
        Me.Label259.AutoSize = true
        Me.Label259.BackColor = System.Drawing.Color.Transparent
        Me.Label259.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label259.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label259.Location = New System.Drawing.Point(828, 54)
        Me.Label259.Name = "Label259"
        Me.Label259.Size = New System.Drawing.Size(57, 14)
        Me.Label259.TabIndex = 32
        Me.Label259.Text = "between"
        Me.Label259.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label257
        '
        Me.Label257.AutoSize = true
        Me.Label257.BackColor = System.Drawing.Color.Transparent
        Me.Label257.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label257.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label257.Location = New System.Drawing.Point(79, 85)
        Me.Label257.Name = "Label257"
        Me.Label257.Size = New System.Drawing.Size(57, 14)
        Me.Label257.TabIndex = 32
        Me.Label257.Text = "between"
        Me.Label257.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label255
        '
        Me.Label255.AutoSize = true
        Me.Label255.BackColor = System.Drawing.Color.Transparent
        Me.Label255.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label255.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label255.Location = New System.Drawing.Point(79, 54)
        Me.Label255.Name = "Label255"
        Me.Label255.Size = New System.Drawing.Size(57, 14)
        Me.Label255.TabIndex = 32
        Me.Label255.Text = "between"
        Me.Label255.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label248
        '
        Me.Label248.AutoSize = true
        Me.Label248.BackColor = System.Drawing.Color.Transparent
        Me.Label248.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label248.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label248.Location = New System.Drawing.Point(79, 23)
        Me.Label248.Name = "Label248"
        Me.Label248.Size = New System.Drawing.Size(57, 14)
        Me.Label248.TabIndex = 32
        Me.Label248.Text = "between"
        Me.Label248.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label111
        '
        Me.Label111.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label111.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label111.Location = New System.Drawing.Point(1, 121)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(1075, 1)
        Me.Label111.TabIndex = 47
        Me.Label111.Text = "label2"
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label112.Location = New System.Drawing.Point(0, 1)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(1, 121)
        Me.Label112.TabIndex = 46
        Me.Label112.Text = "label4"
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label113.Location = New System.Drawing.Point(1076, 1)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(1, 121)
        Me.Label113.TabIndex = 45
        Me.Label113.Text = "label3"
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label114.Location = New System.Drawing.Point(0, 0)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(1077, 1)
        Me.Label114.TabIndex = 44
        Me.Label114.Text = "label1"
        '
        'txtHeightMin
        '
        Me.txtHeightMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtHeightMin.Location = New System.Drawing.Point(136, 19)
        Me.txtHeightMin.MaxLength = 1
        Me.txtHeightMin.Name = "txtHeightMin"
        Me.txtHeightMin.Size = New System.Drawing.Size(16, 22)
        Me.txtHeightMin.TabIndex = 0
        Me.txtHeightMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label11.Location = New System.Drawing.Point(283, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(12, 14)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = """"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMaxInch
        '
        Me.txtHeightMaxInch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtHeightMaxInch.Location = New System.Drawing.Point(251, 19)
        Me.txtHeightMaxInch.MaxLength = 2
        Me.txtHeightMaxInch.Name = "txtHeightMaxInch"
        Me.txtHeightMaxInch.Size = New System.Drawing.Size(32, 22)
        Me.txtHeightMaxInch.TabIndex = 3
        Me.txtHeightMaxInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = true
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label12.Location = New System.Drawing.Point(241, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(10, 14)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "'"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(192, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(12, 14)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = """"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMinInch
        '
        Me.txtHeightMinInch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtHeightMinInch.Location = New System.Drawing.Point(162, 19)
        Me.txtHeightMinInch.MaxLength = 2
        Me.txtHeightMinInch.Name = "txtHeightMinInch"
        Me.txtHeightMinInch.Size = New System.Drawing.Size(32, 22)
        Me.txtHeightMinInch.TabIndex = 1
        Me.txtHeightMinInch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label9.Location = New System.Drawing.Point(152, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 14)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "'"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseOXmax
        '
        Me.txtPulseOXmax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPulseOXmax.Location = New System.Drawing.Point(610, 50)
        Me.txtPulseOXmax.MaxLength = 3
        Me.txtPulseOXmax.Name = "txtPulseOXmax"
        Me.txtPulseOXmax.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseOXmax.TabIndex = 9
        Me.txtPulseOXmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseOXMax
        '
        Me.lblPulseOXMax.AutoSize = true
        Me.lblPulseOXMax.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseOXMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPulseOXMax.Location = New System.Drawing.Point(589, 54)
        Me.lblPulseOXMax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPulseOXMax.Name = "lblPulseOXMax"
        Me.lblPulseOXMax.Size = New System.Drawing.Size(19, 14)
        Me.lblPulseOXMax.TabIndex = 37
        Me.lblPulseOXMax.Text = "to"
        Me.lblPulseOXMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseOXmin
        '
        Me.txtPulseOXmin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPulseOXmin.Location = New System.Drawing.Point(528, 50)
        Me.txtPulseOXmin.MaxLength = 3
        Me.txtPulseOXmin.Name = "txtPulseOXmin"
        Me.txtPulseOXmin.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseOXmin.TabIndex = 8
        Me.txtPulseOXmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseOXMin
        '
        Me.lblPulseOXMin.AutoSize = true
        Me.lblPulseOXMin.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseOXMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPulseOXMin.Location = New System.Drawing.Point(403, 54)
        Me.lblPulseOXMin.Name = "lblPulseOXMin"
        Me.lblPulseOXMin.Size = New System.Drawing.Size(63, 14)
        Me.lblPulseOXMin.TabIndex = 35
        Me.lblPulseOXMin.Text = "Pulse OX :"
        Me.lblPulseOXMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseMax
        '
        Me.txtPulseMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPulseMax.Location = New System.Drawing.Point(225, 50)
        Me.txtPulseMax.MaxLength = 3
        Me.txtPulseMax.Name = "txtPulseMax"
        Me.txtPulseMax.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseMax.TabIndex = 7
        Me.txtPulseMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseMax
        '
        Me.lblPulseMax.AutoSize = true
        Me.lblPulseMax.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPulseMax.Location = New System.Drawing.Point(201, 54)
        Me.lblPulseMax.Name = "lblPulseMax"
        Me.lblPulseMax.Size = New System.Drawing.Size(19, 14)
        Me.lblPulseMax.TabIndex = 33
        Me.lblPulseMax.Text = "to"
        Me.lblPulseMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseMin
        '
        Me.txtPulseMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPulseMin.Location = New System.Drawing.Point(136, 50)
        Me.txtPulseMin.MaxLength = 3
        Me.txtPulseMin.Name = "txtPulseMin"
        Me.txtPulseMin.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseMin.TabIndex = 6
        Me.txtPulseMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPulseMin
        '
        Me.lblPulseMin.AutoSize = true
        Me.lblPulseMin.BackColor = System.Drawing.Color.Transparent
        Me.lblPulseMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblPulseMin.Location = New System.Drawing.Point(36, 54)
        Me.lblPulseMin.Name = "lblPulseMin"
        Me.lblPulseMin.Size = New System.Drawing.Size(43, 14)
        Me.lblPulseMin.TabIndex = 31
        Me.lblPulseMin.Text = "Pulse :"
        Me.lblPulseMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTemperatureMax
        '
        Me.txtTemperatureMax.AcceptsTab = true
        Me.txtTemperatureMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTemperatureMax.Location = New System.Drawing.Point(969, 19)
        Me.txtTemperatureMax.Name = "txtTemperatureMax"
        Me.txtTemperatureMax.Size = New System.Drawing.Size(59, 22)
        Me.txtTemperatureMax.TabIndex = 17
        Me.txtTemperatureMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTempratureMax
        '
        Me.lblTempratureMax.AutoSize = true
        Me.lblTempratureMax.BackColor = System.Drawing.Color.Transparent
        Me.lblTempratureMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTempratureMax.Location = New System.Drawing.Point(948, 23)
        Me.lblTempratureMax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTempratureMax.Name = "lblTempratureMax"
        Me.lblTempratureMax.Size = New System.Drawing.Size(19, 14)
        Me.lblTempratureMax.TabIndex = 29
        Me.lblTempratureMax.Text = "to"
        Me.lblTempratureMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMImax
        '
        Me.txtBMImax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBMImax.Location = New System.Drawing.Point(969, 50)
        Me.txtBMImax.MaxLength = 5
        Me.txtBMImax.Name = "txtBMImax"
        Me.txtBMImax.Size = New System.Drawing.Size(59, 22)
        Me.txtBMImax.TabIndex = 15
        Me.txtBMImax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBMImax
        '
        Me.lblBMImax.AutoSize = true
        Me.lblBMImax.BackColor = System.Drawing.Color.Transparent
        Me.lblBMImax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblBMImax.Location = New System.Drawing.Point(948, 54)
        Me.lblBMImax.Name = "lblBMImax"
        Me.lblBMImax.Size = New System.Drawing.Size(19, 14)
        Me.lblBMImax.TabIndex = 27
        Me.lblBMImax.Text = "to"
        Me.lblBMImax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMImin
        '
        Me.txtBMImin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBMImin.Location = New System.Drawing.Point(887, 50)
        Me.txtBMImin.MaxLength = 5
        Me.txtBMImin.Name = "txtBMImin"
        Me.txtBMImin.Size = New System.Drawing.Size(59, 22)
        Me.txtBMImin.TabIndex = 14
        Me.txtBMImin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBMImin
        '
        Me.lblBMImin.AutoSize = true
        Me.lblBMImin.BackColor = System.Drawing.Color.Transparent
        Me.lblBMImin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblBMImin.Location = New System.Drawing.Point(791, 54)
        Me.lblBMImin.Name = "lblBMImin"
        Me.lblBMImin.Size = New System.Drawing.Size(35, 14)
        Me.lblBMImin.TabIndex = 25
        Me.lblBMImin.Text = "BMI :"
        Me.lblBMImin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtWeightMax
        '
        Me.txtWeightMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtWeightMax.Location = New System.Drawing.Point(610, 19)
        Me.txtWeightMax.MaxLength = 6
        Me.txtWeightMax.Name = "txtWeightMax"
        Me.txtWeightMax.Size = New System.Drawing.Size(59, 22)
        Me.txtWeightMax.TabIndex = 5
        Me.txtWeightMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = true
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label28.Location = New System.Drawing.Point(219, 85)
        Me.Label28.Margin = New System.Windows.Forms.Padding(0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(19, 14)
        Me.Label28.TabIndex = 23
        Me.Label28.Text = "to"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblWeightMax
        '
        Me.lblWeightMax.AutoSize = true
        Me.lblWeightMax.BackColor = System.Drawing.Color.Transparent
        Me.lblWeightMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblWeightMax.Location = New System.Drawing.Point(589, 23)
        Me.lblWeightMax.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWeightMax.Name = "lblWeightMax"
        Me.lblWeightMax.Size = New System.Drawing.Size(19, 14)
        Me.lblWeightMax.TabIndex = 23
        Me.lblWeightMax.Text = "to"
        Me.lblWeightMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMax
        '
        Me.txtHeightMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtHeightMax.Location = New System.Drawing.Point(225, 19)
        Me.txtHeightMax.MaxLength = 1
        Me.txtHeightMax.Name = "txtHeightMax"
        Me.txtHeightMax.Size = New System.Drawing.Size(16, 22)
        Me.txtHeightMax.TabIndex = 2
        Me.txtHeightMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMaxTo
        '
        Me.txtBPstandingMaxTo.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPstandingMaxTo.Location = New System.Drawing.Point(631, 81)
        Me.txtBPstandingMaxTo.MaxLength = 3
        Me.txtBPstandingMaxTo.Name = "txtBPstandingMaxTo"
        Me.txtBPstandingMaxTo.Size = New System.Drawing.Size(34, 22)
        Me.txtBPstandingMaxTo.TabIndex = 12
        Me.txtBPstandingMaxTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMinTo
        '
        Me.txtBPstandingMinTo.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPstandingMinTo.Location = New System.Drawing.Point(679, 81)
        Me.txtBPstandingMinTo.MaxLength = 3
        Me.txtBPstandingMinTo.Name = "txtBPstandingMinTo"
        Me.txtBPstandingMinTo.Size = New System.Drawing.Size(34, 22)
        Me.txtBPstandingMinTo.TabIndex = 13
        Me.txtBPstandingMinTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMax
        '
        Me.txtBPstandingMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPstandingMax.Location = New System.Drawing.Point(528, 81)
        Me.txtBPstandingMax.MaxLength = 3
        Me.txtBPstandingMax.Name = "txtBPstandingMax"
        Me.txtBPstandingMax.Size = New System.Drawing.Size(34, 22)
        Me.txtBPstandingMax.TabIndex = 12
        Me.txtBPstandingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMin
        '
        Me.txtBPstandingMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPstandingMin.Location = New System.Drawing.Point(576, 81)
        Me.txtBPstandingMin.MaxLength = 3
        Me.txtBPstandingMin.Name = "txtBPstandingMin"
        Me.txtBPstandingMin.Size = New System.Drawing.Size(34, 22)
        Me.txtBPstandingMin.TabIndex = 13
        Me.txtBPstandingMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMaxTo
        '
        Me.txtBPsettingMaxTo.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPsettingMaxTo.Location = New System.Drawing.Point(239, 81)
        Me.txtBPsettingMaxTo.MaxLength = 3
        Me.txtBPsettingMaxTo.Name = "txtBPsettingMaxTo"
        Me.txtBPsettingMaxTo.Size = New System.Drawing.Size(34, 22)
        Me.txtBPsettingMaxTo.TabIndex = 10
        Me.txtBPsettingMaxTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMinTo
        '
        Me.txtBPsettingMinTo.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPsettingMinTo.Location = New System.Drawing.Point(287, 81)
        Me.txtBPsettingMinTo.MaxLength = 3
        Me.txtBPsettingMinTo.Name = "txtBPsettingMinTo"
        Me.txtBPsettingMinTo.Size = New System.Drawing.Size(34, 22)
        Me.txtBPsettingMinTo.TabIndex = 11
        Me.txtBPsettingMinTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMax
        '
        Me.txtBPsettingMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPsettingMax.Location = New System.Drawing.Point(136, 81)
        Me.txtBPsettingMax.MaxLength = 3
        Me.txtBPsettingMax.Name = "txtBPsettingMax"
        Me.txtBPsettingMax.Size = New System.Drawing.Size(34, 22)
        Me.txtBPsettingMax.TabIndex = 10
        Me.txtBPsettingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMin
        '
        Me.txtBPsettingMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPsettingMin.Location = New System.Drawing.Point(184, 81)
        Me.txtBPsettingMin.MaxLength = 3
        Me.txtBPsettingMin.Name = "txtBPsettingMin"
        Me.txtBPsettingMin.Size = New System.Drawing.Size(34, 22)
        Me.txtBPsettingMin.TabIndex = 11
        Me.txtBPsettingMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBPStandingMax
        '
        Me.lblBPStandingMax.AutoSize = true
        Me.lblBPStandingMax.BackColor = System.Drawing.Color.Transparent
        Me.lblBPStandingMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblBPStandingMax.Location = New System.Drawing.Point(386, 85)
        Me.lblBPStandingMax.Name = "lblBPStandingMax"
        Me.lblBPStandingMax.Size = New System.Drawing.Size(81, 14)
        Me.lblBPStandingMax.TabIndex = 16
        Me.lblBPStandingMax.Text = "BP-Standing :"
        Me.lblBPStandingMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPStandingMin
        '
        Me.lblBPStandingMin.AutoSize = true
        Me.lblBPStandingMin.BackColor = System.Drawing.Color.Transparent
        Me.lblBPStandingMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblBPStandingMin.Location = New System.Drawing.Point(611, 85)
        Me.lblBPStandingMin.Margin = New System.Windows.Forms.Padding(0)
        Me.lblBPStandingMin.Name = "lblBPStandingMin"
        Me.lblBPStandingMin.Size = New System.Drawing.Size(19, 14)
        Me.lblBPStandingMin.TabIndex = 15
        Me.lblBPStandingMin.Text = "to"
        Me.lblBPStandingMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = true
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label26.Location = New System.Drawing.Point(274, 85)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(12, 14)
        Me.Label26.TabIndex = 13
        Me.Label26.Text = "/"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label29
        '
        Me.Label29.AutoSize = true
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label29.Location = New System.Drawing.Point(666, 85)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(12, 14)
        Me.Label29.TabIndex = 13
        Me.Label29.Text = "/"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPSittingMax
        '
        Me.lblBPSittingMax.AutoSize = true
        Me.lblBPSittingMax.BackColor = System.Drawing.Color.Transparent
        Me.lblBPSittingMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblBPSittingMax.Location = New System.Drawing.Point(11, 85)
        Me.lblBPSittingMax.Name = "lblBPSittingMax"
        Me.lblBPSittingMax.Size = New System.Drawing.Size(68, 14)
        Me.lblBPSittingMax.TabIndex = 14
        Me.lblBPSittingMax.Text = "BP-Sitting :"
        Me.lblBPSittingMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPSittingMin
        '
        Me.lblBPSittingMin.AutoSize = true
        Me.lblBPSittingMin.BackColor = System.Drawing.Color.Transparent
        Me.lblBPSittingMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblBPSittingMin.Location = New System.Drawing.Point(563, 85)
        Me.lblBPSittingMin.Name = "lblBPSittingMin"
        Me.lblBPSittingMin.Size = New System.Drawing.Size(12, 14)
        Me.lblBPSittingMin.TabIndex = 13
        Me.lblBPSittingMin.Text = "/"
        Me.lblBPSittingMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTemperatureMin
        '
        Me.txtTemperatureMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTemperatureMin.Location = New System.Drawing.Point(887, 19)
        Me.txtTemperatureMin.Name = "txtTemperatureMin"
        Me.txtTemperatureMin.Size = New System.Drawing.Size(59, 22)
        Me.txtTemperatureMin.TabIndex = 16
        Me.txtTemperatureMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWeightMin
        '
        Me.txtWeightMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtWeightMin.Location = New System.Drawing.Point(528, 19)
        Me.txtWeightMin.MaxLength = 6
        Me.txtWeightMin.Name = "txtWeightMin"
        Me.txtWeightMin.Size = New System.Drawing.Size(59, 22)
        Me.txtWeightMin.TabIndex = 4
        Me.txtWeightMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblWeightMin
        '
        Me.lblWeightMin.AutoSize = true
        Me.lblWeightMin.BackColor = System.Drawing.Color.Transparent
        Me.lblWeightMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblWeightMin.Location = New System.Drawing.Point(412, 23)
        Me.lblWeightMin.Name = "lblWeightMin"
        Me.lblWeightMin.Size = New System.Drawing.Size(55, 14)
        Me.lblWeightMin.TabIndex = 3
        Me.lblWeightMin.Text = "Weight :"
        Me.lblWeightMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTempratureMin
        '
        Me.lblTempratureMin.AutoSize = true
        Me.lblTempratureMin.BackColor = System.Drawing.Color.Transparent
        Me.lblTempratureMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblTempratureMin.Location = New System.Drawing.Point(723, 23)
        Me.lblTempratureMin.Name = "lblTempratureMin"
        Me.lblTempratureMin.Size = New System.Drawing.Size(103, 14)
        Me.lblTempratureMin.TabIndex = 4
        Me.lblTempratureMin.Text = "Temperature(F) :"
        Me.lblTempratureMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHeightMin
        '
        Me.lblHeightMin.AutoSize = true
        Me.lblHeightMin.BackColor = System.Drawing.Color.Transparent
        Me.lblHeightMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblHeightMin.Location = New System.Drawing.Point(28, 23)
        Me.lblHeightMin.Name = "lblHeightMin"
        Me.lblHeightMin.Size = New System.Drawing.Size(51, 14)
        Me.lblHeightMin.TabIndex = 1
        Me.lblHeightMin.Text = "Height :"
        Me.lblHeightMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 116)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(1077, 28)
        Me.Panel3.TabIndex = 46
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label107)
        Me.Panel7.Controls.Add(Me.Label108)
        Me.Panel7.Controls.Add(Me.Label109)
        Me.Panel7.Controls.Add(Me.Label110)
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1077, 25)
        Me.Panel7.TabIndex = 44
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label107.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label107.Location = New System.Drawing.Point(1, 24)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(1075, 1)
        Me.Label107.TabIndex = 13
        Me.Label107.Text = "label2"
        '
        'Label108
        '
        Me.Label108.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label108.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label108.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label108.Location = New System.Drawing.Point(0, 1)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(1, 24)
        Me.Label108.TabIndex = 12
        Me.Label108.Text = "label4"
        '
        'Label109
        '
        Me.Label109.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label109.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label109.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label109.Location = New System.Drawing.Point(1076, 1)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(1, 24)
        Me.Label109.TabIndex = 11
        Me.Label109.Text = "label3"
        '
        'Label110
        '
        Me.Label110.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label110.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label110.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label110.Location = New System.Drawing.Point(0, 0)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(1077, 1)
        Me.Label110.TabIndex = 10
        Me.Label110.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Image = CType(resources.GetObject("Label7.Image"),System.Drawing.Image)
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1077, 25)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "      Vitals Triggers"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDemographics
        '
        Me.pnlDemographics.BackColor = System.Drawing.Color.Transparent
        Me.pnlDemographics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDemographics.Controls.Add(Me.cmbChkBoxMaritalSt)
        Me.pnlDemographics.Controls.Add(Me.cmbChkBoxGender)
        Me.pnlDemographics.Controls.Add(Me.cmbChkBoxRace)
        Me.pnlDemographics.Controls.Add(Me.txtCity)
        Me.pnlDemographics.Controls.Add(Me.lblCity)
        Me.pnlDemographics.Controls.Add(Me.Label158)
        Me.pnlDemographics.Controls.Add(Me.Label125)
        Me.pnlDemographics.Controls.Add(Me.cmbState)
        Me.pnlDemographics.Controls.Add(Me.Label155)
        Me.pnlDemographics.Controls.Add(Me.lblState)
        Me.pnlDemographics.Controls.Add(Me.txtZip)
        Me.pnlDemographics.Controls.Add(Me.Label124)
        Me.pnlDemographics.Controls.Add(Me.lblEmpStatus)
        Me.pnlDemographics.Controls.Add(Me.Label119)
        Me.pnlDemographics.Controls.Add(Me.cmbEmpStatus)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMaxMnth)
        Me.pnlDemographics.Controls.Add(Me.lblZip)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMinMnth)
        Me.pnlDemographics.Controls.Add(Me.Label103)
        Me.pnlDemographics.Controls.Add(Me.Label104)
        Me.pnlDemographics.Controls.Add(Me.Label105)
        Me.pnlDemographics.Controls.Add(Me.Label106)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMax)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMin)
        Me.pnlDemographics.Controls.Add(Me.Label200)
        Me.pnlDemographics.Controls.Add(Me.Label157)
        Me.pnlDemographics.Controls.Add(Me.lblAgeMax)
        Me.pnlDemographics.Controls.Add(Me.lblMaritalStatus)
        Me.pnlDemographics.Controls.Add(Me.lblGender)
        Me.pnlDemographics.Controls.Add(Me.lblRace)
        Me.pnlDemographics.Controls.Add(Me.Label156)
        Me.pnlDemographics.Controls.Add(Me.lblAgeMin)
        Me.pnlDemographics.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDemographics.ForeColor = System.Drawing.Color.Black
        Me.pnlDemographics.Location = New System.Drawing.Point(0, 28)
        Me.pnlDemographics.Name = "pnlDemographics"
        Me.pnlDemographics.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlDemographics.Size = New System.Drawing.Size(1077, 88)
        Me.pnlDemographics.TabIndex = 0
        '
        'cmbChkBoxMaritalSt
        '
        CheckBoxProperties7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbChkBoxMaritalSt.CheckBoxProperties = CheckBoxProperties7
        Me.cmbChkBoxMaritalSt.DisplayMemberSingleItem = ""
        Me.cmbChkBoxMaritalSt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChkBoxMaritalSt.FormattingEnabled = true
        Me.cmbChkBoxMaritalSt.Location = New System.Drawing.Point(478, 52)
        Me.cmbChkBoxMaritalSt.Name = "cmbChkBoxMaritalSt"
        Me.cmbChkBoxMaritalSt.Size = New System.Drawing.Size(138, 22)
        Me.cmbChkBoxMaritalSt.Sorted = true
        Me.cmbChkBoxMaritalSt.TabIndex = 77
        Me.cmbChkBoxMaritalSt.Tag = "MaritalStatus"
        '
        'cmbChkBoxGender
        '
        CheckBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbChkBoxGender.CheckBoxProperties = CheckBoxProperties1
        Me.cmbChkBoxGender.DisplayMemberSingleItem = ""
        Me.cmbChkBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChkBoxGender.FormattingEnabled = true
        Me.cmbChkBoxGender.Location = New System.Drawing.Point(481, 25)
        Me.cmbChkBoxGender.Name = "cmbChkBoxGender"
        Me.cmbChkBoxGender.Size = New System.Drawing.Size(133, 22)
        Me.cmbChkBoxGender.Sorted = true
        Me.cmbChkBoxGender.TabIndex = 76
        Me.cmbChkBoxGender.Tag = "Gender"
        '
        'cmbChkBoxRace
        '
        CheckBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbChkBoxRace.CheckBoxProperties = CheckBoxProperties2
        Me.cmbChkBoxRace.DisplayMemberSingleItem = ""
        Me.cmbChkBoxRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChkBoxRace.FormattingEnabled = true
        Me.cmbChkBoxRace.Location = New System.Drawing.Point(70, 52)
        Me.cmbChkBoxRace.Name = "cmbChkBoxRace"
        Me.cmbChkBoxRace.Size = New System.Drawing.Size(278, 22)
        Me.cmbChkBoxRace.Sorted = true
        Me.cmbChkBoxRace.TabIndex = 75
        Me.cmbChkBoxRace.Tag = "Race"
        '
        'txtCity
        '
        Me.txtCity.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCity.ForeColor = System.Drawing.Color.Black
        Me.txtCity.Location = New System.Drawing.Point(696, 25)
        Me.txtCity.MaxLength = 50
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(94, 22)
        Me.txtCity.TabIndex = 2
        '
        'lblCity
        '
        Me.lblCity.AutoSize = true
        Me.lblCity.BackColor = System.Drawing.Color.Transparent
        Me.lblCity.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblCity.Location = New System.Drawing.Point(660, 29)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(35, 14)
        Me.lblCity.TabIndex = 10
        Me.lblCity.Text = "City :"
        Me.lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label158
        '
        Me.Label158.AutoSize = true
        Me.Label158.BackColor = System.Drawing.Color.Transparent
        Me.Label158.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label158.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label158.Location = New System.Drawing.Point(52, 56)
        Me.Label158.Name = "Label158"
        Me.Label158.Size = New System.Drawing.Size(16, 14)
        Me.Label158.TabIndex = 30
        Me.Label158.Text = "in"
        Me.Label158.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label125
        '
        Me.Label125.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label125.AutoSize = true
        Me.Label125.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label125.ForeColor = System.Drawing.Color.Red
        Me.Label125.Location = New System.Drawing.Point(309, 9)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(21, 13)
        Me.Label125.TabIndex = 28
        Me.Label125.Text = "mn"
        '
        'cmbState
        '
        Me.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbState.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbState.ForeColor = System.Drawing.Color.Black
        Me.cmbState.Location = New System.Drawing.Point(696, 52)
        Me.cmbState.Name = "cmbState"
        Me.cmbState.Size = New System.Drawing.Size(94, 22)
        Me.cmbState.TabIndex = 4
        '
        'Label155
        '
        Me.Label155.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label155.AutoSize = true
        Me.Label155.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label155.ForeColor = System.Drawing.Color.Red
        Me.Label155.Location = New System.Drawing.Point(255, 9)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(22, 13)
        Me.Label155.TabIndex = 29
        Me.Label155.Text = "yrs"
        '
        'lblState
        '
        Me.lblState.AutoSize = true
        Me.lblState.BackColor = System.Drawing.Color.Transparent
        Me.lblState.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblState.Location = New System.Drawing.Point(650, 56)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(45, 14)
        Me.lblState.TabIndex = 11
        Me.lblState.Text = "State :"
        Me.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtZip
        '
        Me.txtZip.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtZip.ForeColor = System.Drawing.Color.Black
        Me.txtZip.Location = New System.Drawing.Point(948, 25)
        Me.txtZip.MaxLength = 50
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(94, 22)
        Me.txtZip.TabIndex = 6
        '
        'Label124
        '
        Me.Label124.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label124.AutoSize = true
        Me.Label124.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label124.ForeColor = System.Drawing.Color.Red
        Me.Label124.Location = New System.Drawing.Point(177, 9)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(21, 13)
        Me.Label124.TabIndex = 25
        Me.Label124.Text = "mn"
        '
        'lblEmpStatus
        '
        Me.lblEmpStatus.AutoSize = true
        Me.lblEmpStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblEmpStatus.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblEmpStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblEmpStatus.Location = New System.Drawing.Point(823, 56)
        Me.lblEmpStatus.Name = "lblEmpStatus"
        Me.lblEmpStatus.Size = New System.Drawing.Size(122, 14)
        Me.lblEmpStatus.TabIndex = 13
        Me.lblEmpStatus.Text = "Employment Status :"
        Me.lblEmpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label119
        '
        Me.Label119.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label119.AutoSize = true
        Me.Label119.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label119.ForeColor = System.Drawing.Color.Red
        Me.Label119.Location = New System.Drawing.Point(122, 9)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(22, 13)
        Me.Label119.TabIndex = 25
        Me.Label119.Text = "yrs"
        '
        'cmbEmpStatus
        '
        Me.cmbEmpStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEmpStatus.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbEmpStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbEmpStatus.Location = New System.Drawing.Point(948, 52)
        Me.cmbEmpStatus.Name = "cmbEmpStatus"
        Me.cmbEmpStatus.Size = New System.Drawing.Size(94, 22)
        Me.cmbEmpStatus.TabIndex = 8
        '
        'cmbAgeMaxMnth
        '
        Me.cmbAgeMaxMnth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMaxMnth.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAgeMaxMnth.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMaxMnth.Location = New System.Drawing.Point(299, 25)
        Me.cmbAgeMaxMnth.Name = "cmbAgeMaxMnth"
        Me.cmbAgeMaxMnth.Size = New System.Drawing.Size(49, 22)
        Me.cmbAgeMaxMnth.TabIndex = 27
        '
        'lblZip
        '
        Me.lblZip.AutoSize = true
        Me.lblZip.BackColor = System.Drawing.Color.Transparent
        Me.lblZip.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblZip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblZip.Location = New System.Drawing.Point(882, 29)
        Me.lblZip.Name = "lblZip"
        Me.lblZip.Size = New System.Drawing.Size(63, 14)
        Me.lblZip.TabIndex = 12
        Me.lblZip.Text = "Zip Code :"
        Me.lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAgeMinMnth
        '
        Me.cmbAgeMinMnth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMinMnth.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAgeMinMnth.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMinMnth.Location = New System.Drawing.Point(167, 25)
        Me.cmbAgeMinMnth.Name = "cmbAgeMinMnth"
        Me.cmbAgeMinMnth.Size = New System.Drawing.Size(49, 22)
        Me.cmbAgeMinMnth.TabIndex = 26
        '
        'Label103
        '
        Me.Label103.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label103.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label103.Location = New System.Drawing.Point(1, 84)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(1075, 1)
        Me.Label103.TabIndex = 22
        Me.Label103.Text = "label2"
        '
        'Label104
        '
        Me.Label104.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label104.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label104.Location = New System.Drawing.Point(0, 1)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(1, 84)
        Me.Label104.TabIndex = 21
        Me.Label104.Text = "label4"
        '
        'Label105
        '
        Me.Label105.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label105.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label105.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label105.Location = New System.Drawing.Point(1076, 1)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(1, 84)
        Me.Label105.TabIndex = 20
        Me.Label105.Text = "label3"
        '
        'Label106
        '
        Me.Label106.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label106.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label106.Location = New System.Drawing.Point(0, 0)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(1077, 1)
        Me.Label106.TabIndex = 19
        Me.Label106.Text = "label1"
        '
        'cmbAgeMax
        '
        Me.cmbAgeMax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAgeMax.Location = New System.Drawing.Point(244, 25)
        Me.cmbAgeMax.Name = "cmbAgeMax"
        Me.cmbAgeMax.Size = New System.Drawing.Size(53, 22)
        Me.cmbAgeMax.TabIndex = 1
        '
        'cmbAgeMin
        '
        Me.cmbAgeMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAgeMin.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMin.Location = New System.Drawing.Point(110, 25)
        Me.cmbAgeMin.Name = "cmbAgeMin"
        Me.cmbAgeMin.Size = New System.Drawing.Size(55, 22)
        Me.cmbAgeMin.TabIndex = 0
        '
        'Label200
        '
        Me.Label200.AutoSize = true
        Me.Label200.BackColor = System.Drawing.Color.Transparent
        Me.Label200.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label200.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label200.Location = New System.Drawing.Point(462, 56)
        Me.Label200.Name = "Label200"
        Me.Label200.Size = New System.Drawing.Size(16, 14)
        Me.Label200.TabIndex = 18
        Me.Label200.Text = "in"
        Me.Label200.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label157
        '
        Me.Label157.AutoSize = true
        Me.Label157.BackColor = System.Drawing.Color.Transparent
        Me.Label157.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label157.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label157.Location = New System.Drawing.Point(463, 29)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(16, 14)
        Me.Label157.TabIndex = 18
        Me.Label157.Text = "in"
        Me.Label157.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAgeMax
        '
        Me.lblAgeMax.AutoSize = true
        Me.lblAgeMax.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeMax.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblAgeMax.Location = New System.Drawing.Point(221, 29)
        Me.lblAgeMax.Name = "lblAgeMax"
        Me.lblAgeMax.Size = New System.Drawing.Size(19, 14)
        Me.lblAgeMax.TabIndex = 18
        Me.lblAgeMax.Text = "to"
        Me.lblAgeMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMaritalStatus
        '
        Me.lblMaritalStatus.AutoSize = true
        Me.lblMaritalStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblMaritalStatus.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblMaritalStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblMaritalStatus.Location = New System.Drawing.Point(377, 56)
        Me.lblMaritalStatus.Name = "lblMaritalStatus"
        Me.lblMaritalStatus.Size = New System.Drawing.Size(88, 14)
        Me.lblMaritalStatus.TabIndex = 5
        Me.lblMaritalStatus.Text = "Marital Status :"
        Me.lblMaritalStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblGender
        '
        Me.lblGender.AutoSize = true
        Me.lblGender.BackColor = System.Drawing.Color.Transparent
        Me.lblGender.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblGender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblGender.Location = New System.Drawing.Point(410, 29)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(55, 14)
        Me.lblGender.TabIndex = 3
        Me.lblGender.Text = "Gender :"
        Me.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblRace
        '
        Me.lblRace.AutoSize = true
        Me.lblRace.BackColor = System.Drawing.Color.Transparent
        Me.lblRace.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblRace.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblRace.Location = New System.Drawing.Point(12, 56)
        Me.lblRace.Name = "lblRace"
        Me.lblRace.Size = New System.Drawing.Size(41, 14)
        Me.lblRace.TabIndex = 4
        Me.lblRace.Text = "Race :"
        Me.lblRace.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label156
        '
        Me.Label156.AutoSize = true
        Me.Label156.BackColor = System.Drawing.Color.Transparent
        Me.Label156.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label156.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label156.Location = New System.Drawing.Point(52, 29)
        Me.Label156.Name = "Label156"
        Me.Label156.Size = New System.Drawing.Size(57, 14)
        Me.Label156.TabIndex = 1
        Me.Label156.Text = "between"
        Me.Label156.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAgeMin
        '
        Me.lblAgeMin.AutoSize = true
        Me.lblAgeMin.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeMin.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblAgeMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblAgeMin.Location = New System.Drawing.Point(16, 29)
        Me.lblAgeMin.Name = "lblAgeMin"
        Me.lblAgeMin.Size = New System.Drawing.Size(37, 14)
        Me.lblAgeMin.TabIndex = 1
        Me.lblAgeMin.Text = "Age :"
        Me.lblAgeMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(1077, 28)
        Me.Panel2.TabIndex = 45
        '
        'Panel6
        '
        Me.Panel6.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Label65)
        Me.Panel6.Controls.Add(Me.Label85)
        Me.Panel6.Controls.Add(Me.Label89)
        Me.Panel6.Controls.Add(Me.Label90)
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1077, 25)
        Me.Panel6.TabIndex = 19
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label65.Location = New System.Drawing.Point(1, 24)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1075, 1)
        Me.Label65.TabIndex = 13
        Me.Label65.Text = "label2"
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label85.Location = New System.Drawing.Point(0, 1)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1, 24)
        Me.Label85.TabIndex = 12
        Me.Label85.Text = "label4"
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label89.Location = New System.Drawing.Point(1076, 1)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1, 24)
        Me.Label89.TabIndex = 11
        Me.Label89.Text = "label3"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label90.Location = New System.Drawing.Point(0, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(1077, 1)
        Me.Label90.TabIndex = 10
        Me.Label90.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Image = CType(resources.GetObject("Label2.Image"),System.Drawing.Image)
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1077, 25)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "      Demographics Triggers"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PnlProblemList
        '
        Me.PnlProblemList.Controls.Add(Me.PnlProblemMiddle)
        Me.PnlProblemList.Controls.Add(Me.Panel35)
        Me.PnlProblemList.Controls.Add(Me.Panel37)
        Me.PnlProblemList.Controls.Add(Me.Panel38)
        Me.PnlProblemList.Controls.Add(Me.Button2)
        Me.PnlProblemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlProblemList.Location = New System.Drawing.Point(0, 0)
        Me.PnlProblemList.Name = "PnlProblemList"
        Me.PnlProblemList.Size = New System.Drawing.Size(1077, 693)
        Me.PnlProblemList.TabIndex = 9
        Me.PnlProblemList.Visible = false
        '
        'PnlProblemMiddle
        '
        Me.PnlProblemMiddle.Controls.Add(Me.Pnlsnomedprb)
        Me.PnlProblemMiddle.Controls.Add(Me.PnlProblemSearch)
        Me.PnlProblemMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlProblemMiddle.Location = New System.Drawing.Point(0, 27)
        Me.PnlProblemMiddle.Name = "PnlProblemMiddle"
        Me.PnlProblemMiddle.Size = New System.Drawing.Size(1077, 476)
        Me.PnlProblemMiddle.TabIndex = 1
        '
        'Pnlsnomedprb
        '
        Me.Pnlsnomedprb.Controls.Add(Me.pnltrvSnowmedOff)
        Me.Pnlsnomedprb.Controls.Add(Me.pnltrvfinprob)
        Me.Pnlsnomedprb.Controls.Add(Me.PnlSrchProb)
        Me.Pnlsnomedprb.Controls.Add(Me.Splitter6)
        Me.Pnlsnomedprb.Controls.Add(Me.pnltrvsubprb)
        Me.Pnlsnomedprb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pnlsnomedprb.Location = New System.Drawing.Point(0, 0)
        Me.Pnlsnomedprb.Name = "Pnlsnomedprb"
        Me.Pnlsnomedprb.Size = New System.Drawing.Size(1077, 476)
        Me.Pnlsnomedprb.TabIndex = 1
        '
        'pnltrvSnowmedOff
        '
        Me.pnltrvSnowmedOff.Controls.Add(Me.Label142)
        Me.pnltrvSnowmedOff.Controls.Add(Me.trvSnowmedOff)
        Me.pnltrvSnowmedOff.Controls.Add(Me.Panel24)
        Me.pnltrvSnowmedOff.Controls.Add(Me.Label151)
        Me.pnltrvSnowmedOff.Controls.Add(Me.Label152)
        Me.pnltrvSnowmedOff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSnowmedOff.Location = New System.Drawing.Point(0, 26)
        Me.pnltrvSnowmedOff.Name = "pnltrvSnowmedOff"
        Me.pnltrvSnowmedOff.Size = New System.Drawing.Size(1077, 285)
        Me.pnltrvSnowmedOff.TabIndex = 20
        '
        'Label142
        '
        Me.Label142.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label142.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label142.Location = New System.Drawing.Point(1, 284)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(1075, 1)
        Me.Label142.TabIndex = 37
        Me.Label142.Text = "label1"
        '
        'trvSnowmedOff
        '
        Me.trvSnowmedOff.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSnowmedOff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSnowmedOff.Location = New System.Drawing.Point(1, 22)
        Me.trvSnowmedOff.Name = "trvSnowmedOff"
        Me.trvSnowmedOff.Size = New System.Drawing.Size(1075, 263)
        Me.trvSnowmedOff.TabIndex = 0
        '
        'Panel24
        '
        Me.Panel24.BackgroundImage = CType(resources.GetObject("Panel24.BackgroundImage"),System.Drawing.Image)
        Me.Panel24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel24.Controls.Add(Me.Label143)
        Me.Panel24.Controls.Add(Me.Label144)
        Me.Panel24.Controls.Add(Me.Label150)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel24.Location = New System.Drawing.Point(1, 0)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(1075, 22)
        Me.Panel24.TabIndex = 18
        '
        'Label143
        '
        Me.Label143.BackColor = System.Drawing.Color.Transparent
        Me.Label143.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label143.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label143.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label143.Location = New System.Drawing.Point(0, 1)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(1075, 20)
        Me.Label143.TabIndex = 39
        Me.Label143.Text = "  Problem List"
        Me.Label143.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label144.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label144.Location = New System.Drawing.Point(0, 21)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(1075, 1)
        Me.Label144.TabIndex = 38
        Me.Label144.Text = "label1"
        '
        'Label150
        '
        Me.Label150.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label150.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label150.Location = New System.Drawing.Point(0, 0)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(1075, 1)
        Me.Label150.TabIndex = 37
        Me.Label150.Text = "label1"
        '
        'Label151
        '
        Me.Label151.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label151.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label151.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label151.Location = New System.Drawing.Point(0, 0)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(1, 285)
        Me.Label151.TabIndex = 19
        Me.Label151.Text = "label4"
        '
        'Label152
        '
        Me.Label152.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label152.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label152.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label152.Location = New System.Drawing.Point(1076, 0)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(1, 285)
        Me.Label152.TabIndex = 20
        Me.Label152.Text = "label4"
        '
        'pnltrvfinprob
        '
        Me.pnltrvfinprob.Controls.Add(Me.trvfinprob)
        Me.pnltrvfinprob.Controls.Add(Me.Panel31)
        Me.pnltrvfinprob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvfinprob.Location = New System.Drawing.Point(0, 26)
        Me.pnltrvfinprob.Name = "pnltrvfinprob"
        Me.pnltrvfinprob.Size = New System.Drawing.Size(1077, 285)
        Me.pnltrvfinprob.TabIndex = 1
        '
        'trvfinprob
        '
        Me.trvfinprob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.trvfinprob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvfinprob.Location = New System.Drawing.Point(0, 22)
        Me.trvfinprob.Name = "trvfinprob"
        Me.trvfinprob.Size = New System.Drawing.Size(1077, 263)
        Me.trvfinprob.TabIndex = 0
        '
        'Panel31
        '
        Me.Panel31.BackgroundImage = CType(resources.GetObject("Panel31.BackgroundImage"),System.Drawing.Image)
        Me.Panel31.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel31.Controls.Add(Me.Label154)
        Me.Panel31.Controls.Add(Me.Label153)
        Me.Panel31.Controls.Add(Me.Label201)
        Me.Panel31.Controls.Add(Me.Label203)
        Me.Panel31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel31.Location = New System.Drawing.Point(0, 0)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Size = New System.Drawing.Size(1077, 22)
        Me.Panel31.TabIndex = 18
        '
        'Label154
        '
        Me.Label154.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label154.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label154.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label154.Location = New System.Drawing.Point(1076, 1)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(1, 21)
        Me.Label154.TabIndex = 41
        Me.Label154.Text = "label4"
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label153.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label153.Location = New System.Drawing.Point(0, 1)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(1, 21)
        Me.Label153.TabIndex = 40
        Me.Label153.Text = "label4"
        '
        'Label201
        '
        Me.Label201.BackColor = System.Drawing.Color.Transparent
        Me.Label201.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label201.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label201.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label201.Location = New System.Drawing.Point(0, 1)
        Me.Label201.Name = "Label201"
        Me.Label201.Size = New System.Drawing.Size(1077, 21)
        Me.Label201.TabIndex = 39
        Me.Label201.Text = "  Finding"
        Me.Label201.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label203
        '
        Me.Label203.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label203.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label203.Location = New System.Drawing.Point(0, 0)
        Me.Label203.Name = "Label203"
        Me.Label203.Size = New System.Drawing.Size(1077, 1)
        Me.Label203.TabIndex = 37
        Me.Label203.Text = "label1"
        '
        'PnlSrchProb
        '
        Me.PnlSrchProb.BackColor = System.Drawing.Color.Transparent
        Me.PnlSrchProb.Controls.Add(Me.txtsrchprb)
        Me.PnlSrchProb.Controls.Add(Me.Label215)
        Me.PnlSrchProb.Controls.Add(Me.Label216)
        Me.PnlSrchProb.Controls.Add(Me.btnclrprb)
        Me.PnlSrchProb.Controls.Add(Me.PictureBox3)
        Me.PnlSrchProb.Controls.Add(Me.Label217)
        Me.PnlSrchProb.Controls.Add(Me.Label218)
        Me.PnlSrchProb.Controls.Add(Me.Label219)
        Me.PnlSrchProb.Controls.Add(Me.Label220)
        Me.PnlSrchProb.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlSrchProb.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.PnlSrchProb.ForeColor = System.Drawing.Color.Black
        Me.PnlSrchProb.Location = New System.Drawing.Point(0, 0)
        Me.PnlSrchProb.Name = "PnlSrchProb"
        Me.PnlSrchProb.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.PnlSrchProb.Size = New System.Drawing.Size(1077, 26)
        Me.PnlSrchProb.TabIndex = 0
        '
        'txtsrchprb
        '
        Me.txtsrchprb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsrchprb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsrchprb.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtsrchprb.ForeColor = System.Drawing.Color.Black
        Me.txtsrchprb.Location = New System.Drawing.Point(29, 4)
        Me.txtsrchprb.MaxLength = 50
        Me.txtsrchprb.Name = "txtsrchprb"
        Me.txtsrchprb.Size = New System.Drawing.Size(1026, 15)
        Me.txtsrchprb.TabIndex = 0
        '
        'Label215
        '
        Me.Label215.BackColor = System.Drawing.Color.White
        Me.Label215.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label215.Location = New System.Drawing.Point(29, 1)
        Me.Label215.Name = "Label215"
        Me.Label215.Size = New System.Drawing.Size(1026, 3)
        Me.Label215.TabIndex = 37
        '
        'Label216
        '
        Me.Label216.BackColor = System.Drawing.Color.White
        Me.Label216.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label216.Location = New System.Drawing.Point(29, 17)
        Me.Label216.Name = "Label216"
        Me.Label216.Size = New System.Drawing.Size(1026, 5)
        Me.Label216.TabIndex = 38
        '
        'btnclrprb
        '
        Me.btnclrprb.BackgroundImage = CType(resources.GetObject("btnclrprb.BackgroundImage"),System.Drawing.Image)
        Me.btnclrprb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclrprb.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnclrprb.FlatAppearance.BorderSize = 0
        Me.btnclrprb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnclrprb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnclrprb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclrprb.Image = CType(resources.GetObject("btnclrprb.Image"),System.Drawing.Image)
        Me.btnclrprb.Location = New System.Drawing.Point(1055, 1)
        Me.btnclrprb.Name = "btnclrprb"
        Me.btnclrprb.Size = New System.Drawing.Size(21, 21)
        Me.btnclrprb.TabIndex = 41
        Me.ToolTip1.SetToolTip(Me.btnclrprb, "Clear search")
        Me.btnclrprb.UseVisualStyleBackColor = true
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.White
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"),System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = false
        '
        'Label217
        '
        Me.Label217.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label217.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label217.Location = New System.Drawing.Point(1, 22)
        Me.Label217.Name = "Label217"
        Me.Label217.Size = New System.Drawing.Size(1075, 1)
        Me.Label217.TabIndex = 35
        Me.Label217.Text = "label1"
        '
        'Label218
        '
        Me.Label218.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label218.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label218.Location = New System.Drawing.Point(1, 0)
        Me.Label218.Name = "Label218"
        Me.Label218.Size = New System.Drawing.Size(1075, 1)
        Me.Label218.TabIndex = 36
        Me.Label218.Text = "label1"
        '
        'Label219
        '
        Me.Label219.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label219.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label219.Location = New System.Drawing.Point(0, 0)
        Me.Label219.Name = "Label219"
        Me.Label219.Size = New System.Drawing.Size(1, 23)
        Me.Label219.TabIndex = 39
        Me.Label219.Text = "label4"
        '
        'Label220
        '
        Me.Label220.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label220.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label220.Location = New System.Drawing.Point(1076, 0)
        Me.Label220.Name = "Label220"
        Me.Label220.Size = New System.Drawing.Size(1, 23)
        Me.Label220.TabIndex = 40
        Me.Label220.Text = "label4"
        '
        'Splitter6
        '
        Me.Splitter6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter6.Location = New System.Drawing.Point(0, 311)
        Me.Splitter6.Name = "Splitter6"
        Me.Splitter6.Size = New System.Drawing.Size(1077, 3)
        Me.Splitter6.TabIndex = 19
        Me.Splitter6.TabStop = false
        Me.Splitter6.Visible = false
        '
        'pnltrvsubprb
        '
        Me.pnltrvsubprb.Controls.Add(Me.Label221)
        Me.pnltrvsubprb.Controls.Add(Me.trvsubprb)
        Me.pnltrvsubprb.Controls.Add(Me.Panel34)
        Me.pnltrvsubprb.Controls.Add(Me.Label225)
        Me.pnltrvsubprb.Controls.Add(Me.Label226)
        Me.pnltrvsubprb.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnltrvsubprb.Location = New System.Drawing.Point(0, 314)
        Me.pnltrvsubprb.Name = "pnltrvsubprb"
        Me.pnltrvsubprb.Size = New System.Drawing.Size(1077, 162)
        Me.pnltrvsubprb.TabIndex = 2
        Me.pnltrvsubprb.Visible = false
        '
        'Label221
        '
        Me.Label221.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label221.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label221.Location = New System.Drawing.Point(1, 161)
        Me.Label221.Name = "Label221"
        Me.Label221.Size = New System.Drawing.Size(1075, 1)
        Me.Label221.TabIndex = 37
        Me.Label221.Text = "label1"
        '
        'trvsubprb
        '
        Me.trvsubprb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvsubprb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvsubprb.Location = New System.Drawing.Point(1, 22)
        Me.trvsubprb.Name = "trvsubprb"
        Me.trvsubprb.Size = New System.Drawing.Size(1075, 140)
        Me.trvsubprb.TabIndex = 0
        '
        'Panel34
        '
        Me.Panel34.BackgroundImage = CType(resources.GetObject("Panel34.BackgroundImage"),System.Drawing.Image)
        Me.Panel34.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel34.Controls.Add(Me.Label222)
        Me.Panel34.Controls.Add(Me.Label223)
        Me.Panel34.Controls.Add(Me.Label224)
        Me.Panel34.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel34.Location = New System.Drawing.Point(1, 0)
        Me.Panel34.Name = "Panel34"
        Me.Panel34.Size = New System.Drawing.Size(1075, 22)
        Me.Panel34.TabIndex = 18
        '
        'Label222
        '
        Me.Label222.BackColor = System.Drawing.Color.Transparent
        Me.Label222.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label222.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label222.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label222.Location = New System.Drawing.Point(0, 1)
        Me.Label222.Name = "Label222"
        Me.Label222.Size = New System.Drawing.Size(1075, 20)
        Me.Label222.TabIndex = 40
        Me.Label222.Text = "  Sub Type"
        Me.Label222.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label223
        '
        Me.Label223.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label223.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label223.Location = New System.Drawing.Point(0, 0)
        Me.Label223.Name = "Label223"
        Me.Label223.Size = New System.Drawing.Size(1075, 1)
        Me.Label223.TabIndex = 38
        Me.Label223.Text = "label1"
        '
        'Label224
        '
        Me.Label224.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label224.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label224.Location = New System.Drawing.Point(0, 21)
        Me.Label224.Name = "Label224"
        Me.Label224.Size = New System.Drawing.Size(1075, 1)
        Me.Label224.TabIndex = 37
        Me.Label224.Text = "label1"
        '
        'Label225
        '
        Me.Label225.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label225.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label225.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label225.Location = New System.Drawing.Point(0, 0)
        Me.Label225.Name = "Label225"
        Me.Label225.Size = New System.Drawing.Size(1, 162)
        Me.Label225.TabIndex = 19
        Me.Label225.Text = "label4"
        '
        'Label226
        '
        Me.Label226.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label226.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label226.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label226.Location = New System.Drawing.Point(1076, 0)
        Me.Label226.Name = "Label226"
        Me.Label226.Size = New System.Drawing.Size(1, 162)
        Me.Label226.TabIndex = 20
        Me.Label226.Text = "label4"
        '
        'PnlProblemSearch
        '
        Me.PnlProblemSearch.Controls.Add(Me.Panel26)
        Me.PnlProblemSearch.Controls.Add(Me.PnlProbLeft)
        Me.PnlProblemSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlProblemSearch.Location = New System.Drawing.Point(0, 0)
        Me.PnlProblemSearch.Name = "PnlProblemSearch"
        Me.PnlProblemSearch.Size = New System.Drawing.Size(1077, 476)
        Me.PnlProblemSearch.TabIndex = 2
        '
        'Panel26
        '
        Me.Panel26.Controls.Add(Me.trvprobright)
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel26.Location = New System.Drawing.Point(287, 0)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(790, 476)
        Me.Panel26.TabIndex = 0
        '
        'trvprobright
        '
        Me.trvprobright.BackColor = System.Drawing.Color.White
        Me.trvprobright.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvprobright.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvprobright.ForeColor = System.Drawing.Color.Black
        Me.trvprobright.HideSelection = false
        Me.trvprobright.ImageIndex = 0
        Me.trvprobright.ImageList = Me.ImageList1
        Me.trvprobright.ItemHeight = 18
        Me.trvprobright.Location = New System.Drawing.Point(0, 1)
        Me.trvprobright.Name = "trvprobright"
        Me.trvprobright.SelectedImageIndex = 0
        Me.trvprobright.ShowLines = false
        Me.trvprobright.Size = New System.Drawing.Size(326, 274)
        Me.trvprobright.TabIndex = 2
        Me.trvprobright.Visible = false
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(1, "Pat Demographic.ico")
        Me.ImageList1.Images.SetKeyName(2, "History.ico")
        Me.ImageList1.Images.SetKeyName(3, "ICD 09.ico")
        Me.ImageList1.Images.SetKeyName(4, "Drugs.ico")
        Me.ImageList1.Images.SetKeyName(5, "CPT.ico")
        Me.ImageList1.Images.SetKeyName(6, "Lab.ico")
        Me.ImageList1.Images.SetKeyName(7, "Radiology.ico")
        Me.ImageList1.Images.SetKeyName(8, "Guideline Template.ico")
        Me.ImageList1.Images.SetKeyName(9, "RX.ico")
        Me.ImageList1.Images.SetKeyName(10, "PatientDetails.ico")
        Me.ImageList1.Images.SetKeyName(11, "Small Arrow.ico")
        Me.ImageList1.Images.SetKeyName(12, "Immunization_Old.ico")
        '
        'PnlProbLeft
        '
        Me.PnlProbLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlProbLeft.Controls.Add(Me.Label179)
        Me.PnlProbLeft.Controls.Add(Me.Label197)
        Me.PnlProbLeft.Controls.Add(Me.Label198)
        Me.PnlProbLeft.Controls.Add(Me.Label199)
        Me.PnlProbLeft.Controls.Add(Me.trvproblem)
        Me.PnlProbLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.PnlProbLeft.Enabled = false
        Me.PnlProbLeft.Location = New System.Drawing.Point(0, 0)
        Me.PnlProbLeft.Name = "PnlProbLeft"
        Me.PnlProbLeft.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.PnlProbLeft.Size = New System.Drawing.Size(287, 476)
        Me.PnlProbLeft.TabIndex = 2
        Me.PnlProbLeft.Visible = false
        '
        'Label179
        '
        Me.Label179.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label179.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label179.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label179.Location = New System.Drawing.Point(1, 475)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(282, 1)
        Me.Label179.TabIndex = 12
        Me.Label179.Text = "label2"
        '
        'Label197
        '
        Me.Label197.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label197.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label197.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label197.Location = New System.Drawing.Point(0, 1)
        Me.Label197.Name = "Label197"
        Me.Label197.Size = New System.Drawing.Size(1, 475)
        Me.Label197.TabIndex = 11
        Me.Label197.Text = "label4"
        '
        'Label198
        '
        Me.Label198.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label198.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label198.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label198.Location = New System.Drawing.Point(283, 1)
        Me.Label198.Name = "Label198"
        Me.Label198.Size = New System.Drawing.Size(1, 475)
        Me.Label198.TabIndex = 10
        Me.Label198.Text = "label3"
        '
        'Label199
        '
        Me.Label199.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label199.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label199.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label199.Location = New System.Drawing.Point(0, 0)
        Me.Label199.Name = "Label199"
        Me.Label199.Size = New System.Drawing.Size(284, 1)
        Me.Label199.TabIndex = 9
        Me.Label199.Text = "label1"
        '
        'trvproblem
        '
        Me.trvproblem.BackColor = System.Drawing.Color.White
        Me.trvproblem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvproblem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvproblem.ForeColor = System.Drawing.Color.Black
        Me.trvproblem.HideSelection = false
        Me.trvproblem.ItemHeight = 18
        Me.trvproblem.Location = New System.Drawing.Point(0, 0)
        Me.trvproblem.Name = "trvproblem"
        Me.trvproblem.ShowLines = false
        Me.trvproblem.Size = New System.Drawing.Size(284, 476)
        Me.trvproblem.TabIndex = 0
        Me.trvproblem.Visible = false
        '
        'Panel35
        '
        Me.Panel35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel35.Controls.Add(Me.Panel36)
        Me.Panel35.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel35.Location = New System.Drawing.Point(0, 503)
        Me.Panel35.Name = "Panel35"
        Me.Panel35.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel35.Size = New System.Drawing.Size(1077, 27)
        Me.Panel35.TabIndex = 22
        '
        'Panel36
        '
        Me.Panel36.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel36.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel36.Controls.Add(Me.Label227)
        Me.Panel36.Controls.Add(Me.Label228)
        Me.Panel36.Controls.Add(Me.Label229)
        Me.Panel36.Controls.Add(Me.Label230)
        Me.Panel36.Controls.Add(Me.Label231)
        Me.Panel36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel36.Location = New System.Drawing.Point(0, 3)
        Me.Panel36.Name = "Panel36"
        Me.Panel36.Size = New System.Drawing.Size(1077, 21)
        Me.Panel36.TabIndex = 14
        '
        'Label227
        '
        Me.Label227.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label227.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label227.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label227.Location = New System.Drawing.Point(1076, 1)
        Me.Label227.Name = "Label227"
        Me.Label227.Size = New System.Drawing.Size(1, 19)
        Me.Label227.TabIndex = 11
        Me.Label227.Text = "label3"
        '
        'Label228
        '
        Me.Label228.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label228.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label228.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label228.Location = New System.Drawing.Point(0, 1)
        Me.Label228.Name = "Label228"
        Me.Label228.Size = New System.Drawing.Size(1, 19)
        Me.Label228.TabIndex = 12
        Me.Label228.Text = "label4"
        '
        'Label229
        '
        Me.Label229.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label229.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label229.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label229.Location = New System.Drawing.Point(0, 0)
        Me.Label229.Name = "Label229"
        Me.Label229.Size = New System.Drawing.Size(1077, 1)
        Me.Label229.TabIndex = 10
        Me.Label229.Text = "label1"
        '
        'Label230
        '
        Me.Label230.BackColor = System.Drawing.Color.Transparent
        Me.Label230.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label230.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label230.ForeColor = System.Drawing.Color.White
        Me.Label230.Image = CType(resources.GetObject("Label230.Image"),System.Drawing.Image)
        Me.Label230.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label230.Location = New System.Drawing.Point(0, 0)
        Me.Label230.Name = "Label230"
        Me.Label230.Size = New System.Drawing.Size(1077, 20)
        Me.Label230.TabIndex = 9
        Me.Label230.Text = "      Selected Problem"
        Me.Label230.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label231
        '
        Me.Label231.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label231.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label231.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label231.Location = New System.Drawing.Point(0, 20)
        Me.Label231.Name = "Label231"
        Me.Label231.Size = New System.Drawing.Size(1077, 1)
        Me.Label231.TabIndex = 13
        Me.Label231.Text = "label2"
        '
        'Panel37
        '
        Me.Panel37.BackColor = System.Drawing.Color.Transparent
        Me.Panel37.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel37.Controls.Add(Me.trvselectedhist)
        Me.Panel37.Controls.Add(Me.Label232)
        Me.Panel37.Controls.Add(Me.Label233)
        Me.Panel37.Controls.Add(Me.trvselectedprob)
        Me.Panel37.Controls.Add(Me.Label234)
        Me.Panel37.Controls.Add(Me.Label235)
        Me.Panel37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel37.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel37.Location = New System.Drawing.Point(0, 530)
        Me.Panel37.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel37.Name = "Panel37"
        Me.Panel37.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel37.Size = New System.Drawing.Size(1077, 163)
        Me.Panel37.TabIndex = 3
        '
        'trvselectedhist
        '
        Me.trvselectedhist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedhist.ImageIndex = 0
        Me.trvselectedhist.ImageList = Me.ImageList1
        Me.trvselectedhist.Location = New System.Drawing.Point(1, 1)
        Me.trvselectedhist.Name = "trvselectedhist"
        Me.trvselectedhist.SelectedImageIndex = 0
        Me.trvselectedhist.Size = New System.Drawing.Size(1075, 158)
        Me.trvselectedhist.TabIndex = 9
        '
        'Label232
        '
        Me.Label232.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label232.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label232.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label232.Location = New System.Drawing.Point(1, 159)
        Me.Label232.Name = "Label232"
        Me.Label232.Size = New System.Drawing.Size(1075, 1)
        Me.Label232.TabIndex = 8
        Me.Label232.Text = "label2"
        '
        'Label233
        '
        Me.Label233.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label233.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label233.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label233.Location = New System.Drawing.Point(0, 1)
        Me.Label233.Name = "Label233"
        Me.Label233.Size = New System.Drawing.Size(1, 159)
        Me.Label233.TabIndex = 7
        Me.Label233.Text = "label4"
        '
        'trvselectedprob
        '
        Me.trvselectedprob.BackColor = System.Drawing.Color.White
        Me.trvselectedprob.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselectedprob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedprob.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvselectedprob.ForeColor = System.Drawing.Color.Black
        Me.trvselectedprob.HideSelection = false
        Me.trvselectedprob.ImageIndex = 11
        Me.trvselectedprob.ImageList = Me.ImageList1
        Me.trvselectedprob.ItemHeight = 18
        Me.trvselectedprob.Location = New System.Drawing.Point(0, 1)
        Me.trvselectedprob.Name = "trvselectedprob"
        Me.trvselectedprob.SelectedImageIndex = 11
        Me.trvselectedprob.ShowLines = false
        Me.trvselectedprob.Size = New System.Drawing.Size(1076, 159)
        Me.trvselectedprob.TabIndex = 0
        '
        'Label234
        '
        Me.Label234.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label234.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label234.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label234.Location = New System.Drawing.Point(1076, 1)
        Me.Label234.Name = "Label234"
        Me.Label234.Size = New System.Drawing.Size(1, 159)
        Me.Label234.TabIndex = 6
        Me.Label234.Text = "label3"
        '
        'Label235
        '
        Me.Label235.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label235.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label235.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label235.Location = New System.Drawing.Point(0, 0)
        Me.Label235.Name = "Label235"
        Me.Label235.Size = New System.Drawing.Size(1077, 1)
        Me.Label235.TabIndex = 5
        Me.Label235.Text = "label1"
        '
        'Panel38
        '
        Me.Panel38.Controls.Add(Me.Panel39)
        Me.Panel38.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel38.Location = New System.Drawing.Point(0, 0)
        Me.Panel38.Name = "Panel38"
        Me.Panel38.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel38.Size = New System.Drawing.Size(1077, 27)
        Me.Panel38.TabIndex = 0
        '
        'Panel39
        '
        Me.Panel39.BackColor = System.Drawing.Color.Transparent
        Me.Panel39.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel39.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel39.Controls.Add(Me.cmbhistsnomed)
        Me.Panel39.Controls.Add(Me.lblsnohistcat)
        Me.Panel39.Controls.Add(Me.TextBox2)
        Me.Panel39.Controls.Add(Me.Label237)
        Me.Panel39.Controls.Add(Me.Label238)
        Me.Panel39.Controls.Add(Me.Label239)
        Me.Panel39.Controls.Add(Me.Label240)
        Me.Panel39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel39.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel39.ForeColor = System.Drawing.Color.Black
        Me.Panel39.Location = New System.Drawing.Point(0, 0)
        Me.Panel39.Name = "Panel39"
        Me.Panel39.Size = New System.Drawing.Size(1077, 24)
        Me.Panel39.TabIndex = 19
        '
        'cmbhistsnomed
        '
        Me.cmbhistsnomed.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbhistsnomed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbhistsnomed.FormattingEnabled = true
        Me.cmbhistsnomed.Location = New System.Drawing.Point(131, 1)
        Me.cmbhistsnomed.Name = "cmbhistsnomed"
        Me.cmbhistsnomed.Size = New System.Drawing.Size(284, 22)
        Me.cmbhistsnomed.TabIndex = 13
        '
        'lblsnohistcat
        '
        Me.lblsnohistcat.BackColor = System.Drawing.Color.Transparent
        Me.lblsnohistcat.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblsnohistcat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblsnohistcat.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblsnohistcat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.lblsnohistcat.Location = New System.Drawing.Point(1, 1)
        Me.lblsnohistcat.Name = "lblsnohistcat"
        Me.lblsnohistcat.Size = New System.Drawing.Size(130, 22)
        Me.lblsnohistcat.TabIndex = 14
        Me.lblsnohistcat.Text = "History Category :"
        Me.lblsnohistcat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBox2.ForeColor = System.Drawing.Color.Black
        Me.TextBox2.Location = New System.Drawing.Point(507, 5)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(87, 15)
        Me.TextBox2.TabIndex = 1
        Me.TextBox2.Visible = false
        '
        'Label237
        '
        Me.Label237.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label237.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label237.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label237.Location = New System.Drawing.Point(1, 23)
        Me.Label237.Name = "Label237"
        Me.Label237.Size = New System.Drawing.Size(1075, 1)
        Me.Label237.TabIndex = 12
        Me.Label237.Text = "label2"
        '
        'Label238
        '
        Me.Label238.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label238.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label238.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label238.Location = New System.Drawing.Point(0, 1)
        Me.Label238.Name = "Label238"
        Me.Label238.Size = New System.Drawing.Size(1, 23)
        Me.Label238.TabIndex = 11
        Me.Label238.Text = "label4"
        '
        'Label239
        '
        Me.Label239.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label239.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label239.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label239.Location = New System.Drawing.Point(1076, 1)
        Me.Label239.Name = "Label239"
        Me.Label239.Size = New System.Drawing.Size(1, 23)
        Me.Label239.TabIndex = 10
        Me.Label239.Text = "label3"
        '
        'Label240
        '
        Me.Label240.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label240.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label240.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label240.Location = New System.Drawing.Point(0, 0)
        Me.Label240.Name = "Label240"
        Me.Label240.Size = New System.Drawing.Size(1077, 1)
        Me.Label240.TabIndex = 9
        Me.Label240.Text = "label1"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button2.Location = New System.Drawing.Point(254, 134)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(63, 22)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Search"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = false
        Me.Button2.Visible = false
        '
        'pnlRadiology
        '
        Me.pnlRadiology.Controls.Add(Me.Panel18)
        Me.pnlRadiology.Controls.Add(Me.Panel17)
        Me.pnlRadiology.Controls.Add(Me.pnlInternalToolStripRadiology)
        Me.pnlRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRadiology.Location = New System.Drawing.Point(0, 0)
        Me.pnlRadiology.Name = "pnlRadiology"
        Me.pnlRadiology.Size = New System.Drawing.Size(1077, 693)
        Me.pnlRadiology.TabIndex = 1
        Me.pnlRadiology.Visible = false
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Label170)
        Me.Panel18.Controls.Add(Me.Label171)
        Me.Panel18.Controls.Add(Me.Label172)
        Me.Panel18.Controls.Add(Me.Label173)
        Me.Panel18.Controls.Add(Me.c1Labs)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel18.Location = New System.Drawing.Point(0, 80)
        Me.Panel18.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel18.Size = New System.Drawing.Size(1077, 613)
        Me.Panel18.TabIndex = 20
        '
        'Label170
        '
        Me.Label170.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label170.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label170.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label170.Location = New System.Drawing.Point(1, 609)
        Me.Label170.Name = "Label170"
        Me.Label170.Size = New System.Drawing.Size(1075, 1)
        Me.Label170.TabIndex = 8
        Me.Label170.Text = "label2"
        '
        'Label171
        '
        Me.Label171.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label171.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label171.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label171.Location = New System.Drawing.Point(0, 1)
        Me.Label171.Name = "Label171"
        Me.Label171.Size = New System.Drawing.Size(1, 609)
        Me.Label171.TabIndex = 7
        Me.Label171.Text = "label4"
        '
        'Label172
        '
        Me.Label172.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label172.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label172.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label172.Location = New System.Drawing.Point(1076, 1)
        Me.Label172.Name = "Label172"
        Me.Label172.Size = New System.Drawing.Size(1, 609)
        Me.Label172.TabIndex = 6
        Me.Label172.Text = "label3"
        '
        'Label173
        '
        Me.Label173.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label173.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label173.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label173.Location = New System.Drawing.Point(0, 0)
        Me.Label173.Name = "Label173"
        Me.Label173.Size = New System.Drawing.Size(1077, 1)
        Me.Label173.TabIndex = 5
        Me.Label173.Text = "label1"
        '
        'c1Labs
        '
        Me.c1Labs.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Labs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.c1Labs.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Labs.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.c1Labs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Labs.ExtendLastCol = true
        Me.c1Labs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.c1Labs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.c1Labs.Location = New System.Drawing.Point(0, 0)
        Me.c1Labs.Name = "c1Labs"
        Me.c1Labs.Rows.DefaultSize = 19
        Me.c1Labs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Labs.ShowCellLabels = true
        Me.c1Labs.Size = New System.Drawing.Size(1077, 610)
        Me.c1Labs.StyleInfo = resources.GetString("c1Labs.StyleInfo")
        Me.c1Labs.TabIndex = 0
        Me.c1Labs.Tree.NodeImageCollapsed = CType(resources.GetObject("c1Labs.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.c1Labs.Tree.NodeImageExpanded = CType(resources.GetObject("c1Labs.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.Transparent
        Me.Panel17.Controls.Add(Me.txtLabsSearch)
        Me.Panel17.Controls.Add(Me.btnLabClear)
        Me.Panel17.Controls.Add(Me.Label1)
        Me.Panel17.Controls.Add(Me.Label165)
        Me.Panel17.Controls.Add(Me.PictureBox6)
        Me.Panel17.Controls.Add(Me.Label166)
        Me.Panel17.Controls.Add(Me.Label167)
        Me.Panel17.Controls.Add(Me.Label168)
        Me.Panel17.Controls.Add(Me.Label169)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel17.ForeColor = System.Drawing.Color.Black
        Me.Panel17.Location = New System.Drawing.Point(0, 54)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel17.Size = New System.Drawing.Size(1077, 26)
        Me.Panel17.TabIndex = 19
        '
        'txtLabsSearch
        '
        Me.txtLabsSearch.BackColor = System.Drawing.Color.White
        Me.txtLabsSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabsSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLabsSearch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtLabsSearch.ForeColor = System.Drawing.Color.Black
        Me.txtLabsSearch.Location = New System.Drawing.Point(29, 5)
        Me.txtLabsSearch.Name = "txtLabsSearch"
        Me.txtLabsSearch.Size = New System.Drawing.Size(1026, 15)
        Me.txtLabsSearch.TabIndex = 0
        '
        'btnLabClear
        '
        Me.btnLabClear.BackColor = System.Drawing.Color.Transparent
        Me.btnLabClear.BackgroundImage = CType(resources.GetObject("btnLabClear.BackgroundImage"),System.Drawing.Image)
        Me.btnLabClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLabClear.FlatAppearance.BorderSize = 0
        Me.btnLabClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabClear.Image = CType(resources.GetObject("btnLabClear.Image"),System.Drawing.Image)
        Me.btnLabClear.Location = New System.Drawing.Point(1055, 5)
        Me.btnLabClear.Name = "btnLabClear"
        Me.btnLabClear.Size = New System.Drawing.Size(21, 15)
        Me.btnLabClear.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.btnLabClear, "Clear Search")
        Me.btnLabClear.UseVisualStyleBackColor = false
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(29, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1047, 4)
        Me.Label1.TabIndex = 37
        '
        'Label165
        '
        Me.Label165.BackColor = System.Drawing.Color.White
        Me.Label165.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label165.Location = New System.Drawing.Point(29, 20)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(1047, 2)
        Me.Label165.TabIndex = 38
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.White
        Me.PictureBox6.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"),System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 9
        Me.PictureBox6.TabStop = false
        '
        'Label166
        '
        Me.Label166.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label166.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label166.Location = New System.Drawing.Point(1, 22)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(1075, 1)
        Me.Label166.TabIndex = 35
        Me.Label166.Text = "label1"
        '
        'Label167
        '
        Me.Label167.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label167.Location = New System.Drawing.Point(1, 0)
        Me.Label167.Name = "Label167"
        Me.Label167.Size = New System.Drawing.Size(1075, 1)
        Me.Label167.TabIndex = 36
        Me.Label167.Text = "label1"
        '
        'Label168
        '
        Me.Label168.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label168.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label168.Location = New System.Drawing.Point(0, 0)
        Me.Label168.Name = "Label168"
        Me.Label168.Size = New System.Drawing.Size(1, 23)
        Me.Label168.TabIndex = 39
        Me.Label168.Text = "label4"
        '
        'Label169
        '
        Me.Label169.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label169.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label169.Location = New System.Drawing.Point(1076, 0)
        Me.Label169.Name = "Label169"
        Me.Label169.Size = New System.Drawing.Size(1, 23)
        Me.Label169.TabIndex = 40
        Me.Label169.Text = "label4"
        '
        'pnlInternalToolStripRadiology
        '
        Me.pnlInternalToolStripRadiology.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripRadiology.Controls.Add(Me.Panel41)
        Me.pnlInternalToolStripRadiology.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripRadiology.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripRadiology.Name = "pnlInternalToolStripRadiology"
        Me.pnlInternalToolStripRadiology.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripRadiology.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripRadiology.TabIndex = 56
        '
        'Panel41
        '
        Me.Panel41.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel41.Controls.Add(Me.ToolStrip6)
        Me.Panel41.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel41.Location = New System.Drawing.Point(0, 0)
        Me.Panel41.Name = "Panel41"
        Me.Panel41.Size = New System.Drawing.Size(1077, 54)
        Me.Panel41.TabIndex = 4
        '
        'ToolStrip6
        '
        Me.ToolStrip6.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip6.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip6.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip6.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveRadiology, Me.tsBtn_CancelRadiology})
        Me.ToolStrip6.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip6.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip6.Name = "ToolStrip6"
        Me.ToolStrip6.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip6.TabIndex = 0
        Me.ToolStrip6.Text = "ToolStrip6"
        '
        'tsBtn_SaveRadiology
        '
        Me.tsBtn_SaveRadiology.Image = CType(resources.GetObject("tsBtn_SaveRadiology.Image"),System.Drawing.Image)
        Me.tsBtn_SaveRadiology.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveRadiology.Name = "tsBtn_SaveRadiology"
        Me.tsBtn_SaveRadiology.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveRadiology.Tag = "Save"
        Me.tsBtn_SaveRadiology.Text = "&Done"
        Me.tsBtn_SaveRadiology.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveRadiology.ToolTipText = "Done"
        '
        'tsBtn_CancelRadiology
        '
        Me.tsBtn_CancelRadiology.Image = CType(resources.GetObject("tsBtn_CancelRadiology.Image"),System.Drawing.Image)
        Me.tsBtn_CancelRadiology.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelRadiology.Name = "tsBtn_CancelRadiology"
        Me.tsBtn_CancelRadiology.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelRadiology.Tag = "Cancel"
        Me.tsBtn_CancelRadiology.Text = "&Cancel"
        Me.tsBtn_CancelRadiology.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelRadiology.Visible = false
        '
        'pnlHistory
        '
        Me.pnlHistory.Controls.Add(Me.Panel22)
        Me.pnlHistory.Controls.Add(Me.Panel20)
        Me.pnlHistory.Controls.Add(Me.Panel16)
        Me.pnlHistory.Controls.Add(Me.Panel11)
        Me.pnlHistory.Controls.Add(Me.btnHistorySearch)
        Me.pnlHistory.Controls.Add(Me.pnlInternalToolStripHistory)
        Me.pnlHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlHistory.Location = New System.Drawing.Point(0, 0)
        Me.pnlHistory.Name = "pnlHistory"
        Me.pnlHistory.Size = New System.Drawing.Size(1077, 693)
        Me.pnlHistory.TabIndex = 0
        Me.pnlHistory.Visible = false
        '
        'Panel22
        '
        Me.Panel22.BackColor = System.Drawing.Color.Transparent
        Me.Panel22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel22.Controls.Add(Me.Label193)
        Me.Panel22.Controls.Add(Me.Label194)
        Me.Panel22.Controls.Add(Me.trvSelectedHistory)
        Me.Panel22.Controls.Add(Me.Label195)
        Me.Panel22.Controls.Add(Me.Label196)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel22.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel22.Location = New System.Drawing.Point(0, 465)
        Me.Panel22.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel22.Size = New System.Drawing.Size(1077, 228)
        Me.Panel22.TabIndex = 23
        '
        'Label193
        '
        Me.Label193.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label193.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label193.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label193.Location = New System.Drawing.Point(1, 224)
        Me.Label193.Name = "Label193"
        Me.Label193.Size = New System.Drawing.Size(1075, 1)
        Me.Label193.TabIndex = 8
        Me.Label193.Text = "label2"
        '
        'Label194
        '
        Me.Label194.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label194.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label194.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label194.Location = New System.Drawing.Point(0, 1)
        Me.Label194.Name = "Label194"
        Me.Label194.Size = New System.Drawing.Size(1, 224)
        Me.Label194.TabIndex = 7
        Me.Label194.Text = "label4"
        '
        'trvSelectedHistory
        '
        Me.trvSelectedHistory.BackColor = System.Drawing.Color.White
        Me.trvSelectedHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedHistory.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvSelectedHistory.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedHistory.HideSelection = false
        Me.trvSelectedHistory.ImageIndex = 11
        Me.trvSelectedHistory.ImageList = Me.ImageList1
        Me.trvSelectedHistory.ItemHeight = 18
        Me.trvSelectedHistory.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedHistory.Name = "trvSelectedHistory"
        Me.trvSelectedHistory.SelectedImageIndex = 11
        Me.trvSelectedHistory.ShowLines = false
        Me.trvSelectedHistory.Size = New System.Drawing.Size(1076, 224)
        Me.trvSelectedHistory.TabIndex = 0
        '
        'Label195
        '
        Me.Label195.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label195.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label195.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label195.Location = New System.Drawing.Point(1076, 1)
        Me.Label195.Name = "Label195"
        Me.Label195.Size = New System.Drawing.Size(1, 224)
        Me.Label195.TabIndex = 6
        Me.Label195.Text = "label3"
        '
        'Label196
        '
        Me.Label196.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label196.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label196.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label196.Location = New System.Drawing.Point(0, 0)
        Me.Label196.Name = "Label196"
        Me.Label196.Size = New System.Drawing.Size(1077, 1)
        Me.Label196.TabIndex = 5
        Me.Label196.Text = "label1"
        '
        'Panel20
        '
        Me.Panel20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel20.Controls.Add(Me.Panel21)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel20.Location = New System.Drawing.Point(0, 438)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel20.Size = New System.Drawing.Size(1077, 27)
        Me.Panel20.TabIndex = 22
        '
        'Panel21
        '
        Me.Panel21.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel21.Controls.Add(Me.Label188)
        Me.Panel21.Controls.Add(Me.Label189)
        Me.Panel21.Controls.Add(Me.Label190)
        Me.Panel21.Controls.Add(Me.Label191)
        Me.Panel21.Controls.Add(Me.Label192)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel21.Location = New System.Drawing.Point(0, 3)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(1077, 21)
        Me.Panel21.TabIndex = 14
        '
        'Label188
        '
        Me.Label188.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label188.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label188.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label188.Location = New System.Drawing.Point(1076, 1)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(1, 19)
        Me.Label188.TabIndex = 11
        Me.Label188.Text = "label3"
        '
        'Label189
        '
        Me.Label189.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label189.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label189.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label189.Location = New System.Drawing.Point(0, 1)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(1, 19)
        Me.Label189.TabIndex = 12
        Me.Label189.Text = "label4"
        '
        'Label190
        '
        Me.Label190.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label190.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label190.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label190.Location = New System.Drawing.Point(0, 0)
        Me.Label190.Name = "Label190"
        Me.Label190.Size = New System.Drawing.Size(1077, 1)
        Me.Label190.TabIndex = 10
        Me.Label190.Text = "label1"
        '
        'Label191
        '
        Me.Label191.BackColor = System.Drawing.Color.Transparent
        Me.Label191.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label191.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label191.ForeColor = System.Drawing.Color.White
        Me.Label191.Image = CType(resources.GetObject("Label191.Image"),System.Drawing.Image)
        Me.Label191.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label191.Location = New System.Drawing.Point(0, 0)
        Me.Label191.Name = "Label191"
        Me.Label191.Size = New System.Drawing.Size(1077, 20)
        Me.Label191.TabIndex = 9
        Me.Label191.Text = "      Selected History"
        Me.Label191.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label192
        '
        Me.Label192.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label192.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label192.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label192.Location = New System.Drawing.Point(0, 20)
        Me.Label192.Name = "Label192"
        Me.Label192.Size = New System.Drawing.Size(1077, 1)
        Me.Label192.TabIndex = 13
        Me.Label192.Text = "label2"
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.Panel9)
        Me.Panel16.Controls.Add(Me.pnlHistoryLeft)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(0, 81)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(1077, 357)
        Me.Panel16.TabIndex = 23
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.GloUC_trvHistory)
        Me.Panel9.Controls.Add(Me.trvHistoryRight)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(287, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(790, 357)
        Me.Panel9.TabIndex = 20
        '
        'trvHistoryRight
        '
        Me.trvHistoryRight.BackColor = System.Drawing.Color.White
        Me.trvHistoryRight.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHistoryRight.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvHistoryRight.ForeColor = System.Drawing.Color.Black
        Me.trvHistoryRight.HideSelection = false
        Me.trvHistoryRight.ImageIndex = 0
        Me.trvHistoryRight.ImageList = Me.ImageList1
        Me.trvHistoryRight.ItemHeight = 18
        Me.trvHistoryRight.Location = New System.Drawing.Point(0, 1)
        Me.trvHistoryRight.Name = "trvHistoryRight"
        Me.trvHistoryRight.SelectedImageIndex = 0
        Me.trvHistoryRight.ShowLines = false
        Me.trvHistoryRight.Size = New System.Drawing.Size(326, 274)
        Me.trvHistoryRight.TabIndex = 2
        Me.trvHistoryRight.Visible = false
        '
        'pnlHistoryLeft
        '
        Me.pnlHistoryLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlHistoryLeft.Controls.Add(Me.Label120)
        Me.pnlHistoryLeft.Controls.Add(Me.Label121)
        Me.pnlHistoryLeft.Controls.Add(Me.Label122)
        Me.pnlHistoryLeft.Controls.Add(Me.Label123)
        Me.pnlHistoryLeft.Controls.Add(Me.trvHistory)
        Me.pnlHistoryLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlHistoryLeft.Enabled = false
        Me.pnlHistoryLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlHistoryLeft.Name = "pnlHistoryLeft"
        Me.pnlHistoryLeft.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlHistoryLeft.Size = New System.Drawing.Size(287, 357)
        Me.pnlHistoryLeft.TabIndex = 1
        Me.pnlHistoryLeft.Visible = false
        '
        'Label120
        '
        Me.Label120.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label120.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label120.Location = New System.Drawing.Point(1, 356)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(282, 1)
        Me.Label120.TabIndex = 12
        Me.Label120.Text = "label2"
        '
        'Label121
        '
        Me.Label121.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label121.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label121.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label121.Location = New System.Drawing.Point(0, 1)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(1, 356)
        Me.Label121.TabIndex = 11
        Me.Label121.Text = "label4"
        '
        'Label122
        '
        Me.Label122.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label122.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label122.Location = New System.Drawing.Point(283, 1)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(1, 356)
        Me.Label122.TabIndex = 10
        Me.Label122.Text = "label3"
        '
        'Label123
        '
        Me.Label123.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label123.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label123.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label123.Location = New System.Drawing.Point(0, 0)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(284, 1)
        Me.Label123.TabIndex = 9
        Me.Label123.Text = "label1"
        '
        'trvHistory
        '
        Me.trvHistory.BackColor = System.Drawing.Color.White
        Me.trvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvHistory.ForeColor = System.Drawing.Color.Black
        Me.trvHistory.HideSelection = false
        Me.trvHistory.ItemHeight = 18
        Me.trvHistory.Location = New System.Drawing.Point(0, 0)
        Me.trvHistory.Name = "trvHistory"
        Me.trvHistory.ShowLines = false
        Me.trvHistory.Size = New System.Drawing.Size(284, 357)
        Me.trvHistory.TabIndex = 0
        Me.trvHistory.Visible = false
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Panel8)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 54)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel11.Size = New System.Drawing.Size(1077, 27)
        Me.Panel11.TabIndex = 21
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.cmbHistoryCategory)
        Me.Panel8.Controls.Add(Me.Label140)
        Me.Panel8.Controls.Add(Me.txtSearch)
        Me.Panel8.Controls.Add(Me.Label8)
        Me.Panel8.Controls.Add(Me.Label14)
        Me.Panel8.Controls.Add(Me.Label115)
        Me.Panel8.Controls.Add(Me.Label116)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel8.ForeColor = System.Drawing.Color.Black
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1077, 24)
        Me.Panel8.TabIndex = 19
        '
        'cmbHistoryCategory
        '
        Me.cmbHistoryCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbHistoryCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHistoryCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbHistoryCategory.FormattingEnabled = true
        Me.cmbHistoryCategory.Location = New System.Drawing.Point(131, 1)
        Me.cmbHistoryCategory.Name = "cmbHistoryCategory"
        Me.cmbHistoryCategory.Size = New System.Drawing.Size(284, 22)
        Me.cmbHistoryCategory.TabIndex = 0
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.Transparent
        Me.Label140.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label140.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label140.Location = New System.Drawing.Point(1, 1)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(130, 22)
        Me.Label140.TabIndex = 13
        Me.Label140.Text = "History Category :"
        Me.Label140.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(507, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(87, 15)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Visible = false
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label8.Location = New System.Drawing.Point(1, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1075, 1)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 23)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "label4"
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label115.Location = New System.Drawing.Point(1076, 1)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(1, 23)
        Me.Label115.TabIndex = 10
        Me.Label115.Text = "label3"
        '
        'Label116
        '
        Me.Label116.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label116.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label116.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label116.Location = New System.Drawing.Point(0, 0)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(1077, 1)
        Me.Label116.TabIndex = 9
        Me.Label116.Text = "label1"
        '
        'btnHistorySearch
        '
        Me.btnHistorySearch.BackColor = System.Drawing.Color.Transparent
        Me.btnHistorySearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHistorySearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnHistorySearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnHistorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistorySearch.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnHistorySearch.Location = New System.Drawing.Point(254, 134)
        Me.btnHistorySearch.Name = "btnHistorySearch"
        Me.btnHistorySearch.Size = New System.Drawing.Size(63, 22)
        Me.btnHistorySearch.TabIndex = 2
        Me.btnHistorySearch.Text = "Search"
        Me.btnHistorySearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHistorySearch.UseVisualStyleBackColor = false
        Me.btnHistorySearch.Visible = false
        '
        'pnlInternalToolStripHistory
        '
        Me.pnlInternalToolStripHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripHistory.Controls.Add(Me.Panel33)
        Me.pnlInternalToolStripHistory.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripHistory.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripHistory.Name = "pnlInternalToolStripHistory"
        Me.pnlInternalToolStripHistory.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripHistory.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripHistory.TabIndex = 55
        '
        'Panel33
        '
        Me.Panel33.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel33.Controls.Add(Me.ToolStrip4)
        Me.Panel33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel33.Location = New System.Drawing.Point(0, 0)
        Me.Panel33.Name = "Panel33"
        Me.Panel33.Size = New System.Drawing.Size(1077, 54)
        Me.Panel33.TabIndex = 4
        '
        'ToolStrip4
        '
        Me.ToolStrip4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip4.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip4.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveHistory, Me.tsBtn_CancelHistory})
        Me.ToolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip4.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip4.Name = "ToolStrip4"
        Me.ToolStrip4.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip4.TabIndex = 0
        Me.ToolStrip4.Text = "ToolStrip4"
        '
        'tsBtn_SaveHistory
        '
        Me.tsBtn_SaveHistory.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveHistory.Name = "tsBtn_SaveHistory"
        Me.tsBtn_SaveHistory.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveHistory.Tag = "Save"
        Me.tsBtn_SaveHistory.Text = "&Done"
        Me.tsBtn_SaveHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveHistory.ToolTipText = "Done"
        '
        'tsBtn_CancelHistory
        '
        Me.tsBtn_CancelHistory.Image = CType(resources.GetObject("tsBtn_CancelHistory.Image"),System.Drawing.Image)
        Me.tsBtn_CancelHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelHistory.Name = "tsBtn_CancelHistory"
        Me.tsBtn_CancelHistory.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelHistory.Tag = "Cancel"
        Me.tsBtn_CancelHistory.Text = "&Cancel"
        Me.tsBtn_CancelHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelHistory.Visible = false
        '
        'pnlInsurance
        '
        Me.pnlInsurance.Controls.Add(Me.pnltrvSelectedInsurance)
        Me.pnlInsurance.Controls.Add(Me.pnlSelectedInsuranceLabel)
        Me.pnlInsurance.Controls.Add(Me.Splitter2)
        Me.pnlInsurance.Controls.Add(Me.GloUC_trvInsurance)
        Me.pnlInsurance.Controls.Add(Me.pnlInternalToolStripInsurance)
        Me.pnlInsurance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInsurance.Location = New System.Drawing.Point(0, 0)
        Me.pnlInsurance.Name = "pnlInsurance"
        Me.pnlInsurance.Size = New System.Drawing.Size(1077, 693)
        Me.pnlInsurance.TabIndex = 10
        Me.pnlInsurance.Visible = false
        '
        'pnltrvSelectedInsurance
        '
        Me.pnltrvSelectedInsurance.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvSelectedInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvSelectedInsurance.Controls.Add(Me.Label329)
        Me.pnltrvSelectedInsurance.Controls.Add(Me.Label330)
        Me.pnltrvSelectedInsurance.Controls.Add(Me.trvSelectedInsurance)
        Me.pnltrvSelectedInsurance.Controls.Add(Me.Label331)
        Me.pnltrvSelectedInsurance.Controls.Add(Me.Label332)
        Me.pnltrvSelectedInsurance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSelectedInsurance.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnltrvSelectedInsurance.Location = New System.Drawing.Point(0, 476)
        Me.pnltrvSelectedInsurance.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedInsurance.Name = "pnltrvSelectedInsurance"
        Me.pnltrvSelectedInsurance.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedInsurance.Size = New System.Drawing.Size(1077, 217)
        Me.pnltrvSelectedInsurance.TabIndex = 21
        '
        'Label329
        '
        Me.Label329.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label329.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label329.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label329.Location = New System.Drawing.Point(1, 213)
        Me.Label329.Name = "Label329"
        Me.Label329.Size = New System.Drawing.Size(1075, 1)
        Me.Label329.TabIndex = 8
        Me.Label329.Text = "label2"
        '
        'Label330
        '
        Me.Label330.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label330.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label330.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label330.Location = New System.Drawing.Point(0, 1)
        Me.Label330.Name = "Label330"
        Me.Label330.Size = New System.Drawing.Size(1, 213)
        Me.Label330.TabIndex = 7
        Me.Label330.Text = "label4"
        '
        'trvSelectedInsurance
        '
        Me.trvSelectedInsurance.BackColor = System.Drawing.Color.White
        Me.trvSelectedInsurance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedInsurance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedInsurance.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvSelectedInsurance.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedInsurance.HideSelection = false
        Me.trvSelectedInsurance.ImageIndex = 0
        Me.trvSelectedInsurance.ImageList = Me.ImageList1
        Me.trvSelectedInsurance.ItemHeight = 18
        Me.trvSelectedInsurance.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedInsurance.Name = "trvSelectedInsurance"
        Me.trvSelectedInsurance.SelectedImageIndex = 0
        Me.trvSelectedInsurance.ShowLines = false
        Me.trvSelectedInsurance.Size = New System.Drawing.Size(1076, 213)
        Me.trvSelectedInsurance.TabIndex = 0
        '
        'Label331
        '
        Me.Label331.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label331.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label331.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label331.Location = New System.Drawing.Point(1076, 1)
        Me.Label331.Name = "Label331"
        Me.Label331.Size = New System.Drawing.Size(1, 213)
        Me.Label331.TabIndex = 6
        Me.Label331.Text = "label3"
        '
        'Label332
        '
        Me.Label332.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label332.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label332.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label332.Location = New System.Drawing.Point(0, 0)
        Me.Label332.Name = "Label332"
        Me.Label332.Size = New System.Drawing.Size(1077, 1)
        Me.Label332.TabIndex = 5
        Me.Label332.Text = "label1"
        '
        'pnlSelectedInsuranceLabel
        '
        Me.pnlSelectedInsuranceLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedInsuranceLabel.Controls.Add(Me.Panel56)
        Me.pnlSelectedInsuranceLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectedInsuranceLabel.Location = New System.Drawing.Point(0, 449)
        Me.pnlSelectedInsuranceLabel.Name = "pnlSelectedInsuranceLabel"
        Me.pnlSelectedInsuranceLabel.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedInsuranceLabel.Size = New System.Drawing.Size(1077, 27)
        Me.pnlSelectedInsuranceLabel.TabIndex = 20
        '
        'Panel56
        '
        Me.Panel56.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel56.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel56.Controls.Add(Me.Label333)
        Me.Panel56.Controls.Add(Me.Label334)
        Me.Panel56.Controls.Add(Me.Label335)
        Me.Panel56.Controls.Add(Me.Label336)
        Me.Panel56.Controls.Add(Me.Label337)
        Me.Panel56.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel56.Location = New System.Drawing.Point(0, 0)
        Me.Panel56.Name = "Panel56"
        Me.Panel56.Size = New System.Drawing.Size(1077, 24)
        Me.Panel56.TabIndex = 14
        '
        'Label333
        '
        Me.Label333.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label333.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label333.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label333.Location = New System.Drawing.Point(1076, 1)
        Me.Label333.Name = "Label333"
        Me.Label333.Size = New System.Drawing.Size(1, 22)
        Me.Label333.TabIndex = 11
        Me.Label333.Text = "label3"
        '
        'Label334
        '
        Me.Label334.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label334.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label334.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label334.Location = New System.Drawing.Point(0, 1)
        Me.Label334.Name = "Label334"
        Me.Label334.Size = New System.Drawing.Size(1, 22)
        Me.Label334.TabIndex = 12
        Me.Label334.Text = "label4"
        '
        'Label335
        '
        Me.Label335.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label335.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label335.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label335.Location = New System.Drawing.Point(0, 0)
        Me.Label335.Name = "Label335"
        Me.Label335.Size = New System.Drawing.Size(1077, 1)
        Me.Label335.TabIndex = 10
        Me.Label335.Text = "label1"
        '
        'Label336
        '
        Me.Label336.BackColor = System.Drawing.Color.Transparent
        Me.Label336.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label336.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label336.ForeColor = System.Drawing.Color.White
        Me.Label336.Image = CType(resources.GetObject("Label336.Image"),System.Drawing.Image)
        Me.Label336.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label336.Location = New System.Drawing.Point(0, 0)
        Me.Label336.Name = "Label336"
        Me.Label336.Size = New System.Drawing.Size(1077, 23)
        Me.Label336.TabIndex = 9
        Me.Label336.Text = "      Selected Insurance Plan"
        Me.Label336.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label337
        '
        Me.Label337.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label337.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label337.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label337.Location = New System.Drawing.Point(0, 23)
        Me.Label337.Name = "Label337"
        Me.Label337.Size = New System.Drawing.Size(1077, 1)
        Me.Label337.TabIndex = 13
        Me.Label337.Text = "label2"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 446)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(1077, 3)
        Me.Splitter2.TabIndex = 22
        Me.Splitter2.TabStop = false
        '
        'pnlInternalToolStripInsurance
        '
        Me.pnlInternalToolStripInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripInsurance.Controls.Add(Me.Panel58)
        Me.pnlInternalToolStripInsurance.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripInsurance.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripInsurance.Name = "pnlInternalToolStripInsurance"
        Me.pnlInternalToolStripInsurance.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripInsurance.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripInsurance.TabIndex = 54
        '
        'Panel58
        '
        Me.Panel58.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel58.Controls.Add(Me.ToolStrip13)
        Me.Panel58.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel58.Location = New System.Drawing.Point(0, 0)
        Me.Panel58.Name = "Panel58"
        Me.Panel58.Size = New System.Drawing.Size(1077, 54)
        Me.Panel58.TabIndex = 4
        '
        'ToolStrip13
        '
        Me.ToolStrip13.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip13.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip13.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip13.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveInsurance, Me.tsBtn_CancelInsurance})
        Me.ToolStrip13.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip13.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip13.Name = "ToolStrip13"
        Me.ToolStrip13.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip13.TabIndex = 0
        Me.ToolStrip13.Text = "ToolStrip13"
        '
        'tsBtn_SaveInsurance
        '
        Me.tsBtn_SaveInsurance.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveInsurance.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveInsurance.Name = "tsBtn_SaveInsurance"
        Me.tsBtn_SaveInsurance.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveInsurance.Tag = "Save"
        Me.tsBtn_SaveInsurance.Text = "&Done"
        Me.tsBtn_SaveInsurance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveInsurance.ToolTipText = "Done"
        '
        'tsBtn_CancelInsurance
        '
        Me.tsBtn_CancelInsurance.Image = CType(resources.GetObject("tsBtn_CancelInsurance.Image"),System.Drawing.Image)
        Me.tsBtn_CancelInsurance.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelInsurance.Name = "tsBtn_CancelInsurance"
        Me.tsBtn_CancelInsurance.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelInsurance.Tag = "Cancel"
        Me.tsBtn_CancelInsurance.Text = "&Cancel"
        Me.tsBtn_CancelInsurance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelInsurance.Visible = false
        '
        'pnlICD9
        '
        Me.pnlICD9.Controls.Add(Me.Panel15)
        Me.pnlICD9.Controls.Add(Me.Panel5)
        Me.pnlICD9.Controls.Add(Me.Splitter4)
        Me.pnlICD9.Controls.Add(Me.GloUC_trvICD10)
        Me.pnlICD9.Controls.Add(Me.GloUC_trvICD9)
        Me.pnlICD9.Controls.Add(Me.pnlInternalToolStripICD)
        Me.pnlICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlICD9.Location = New System.Drawing.Point(0, 0)
        Me.pnlICD9.Name = "pnlICD9"
        Me.pnlICD9.Size = New System.Drawing.Size(1077, 693)
        Me.pnlICD9.TabIndex = 1
        Me.pnlICD9.Visible = false
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.Transparent
        Me.Panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel15.Controls.Add(Me.Label132)
        Me.Panel15.Controls.Add(Me.Label133)
        Me.Panel15.Controls.Add(Me.trvselecteICD10s)
        Me.Panel15.Controls.Add(Me.trvselecteICDs)
        Me.Panel15.Controls.Add(Me.Label134)
        Me.Panel15.Controls.Add(Me.Label135)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel15.Location = New System.Drawing.Point(0, 864)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel15.Size = New System.Drawing.Size(1077, 0)
        Me.Panel15.TabIndex = 50
        '
        'Label132
        '
        Me.Label132.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label132.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label132.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label132.Location = New System.Drawing.Point(1, -4)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(1075, 1)
        Me.Label132.TabIndex = 8
        Me.Label132.Text = "label2"
        '
        'Label133
        '
        Me.Label133.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label133.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label133.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label133.Location = New System.Drawing.Point(0, 1)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(1, 0)
        Me.Label133.TabIndex = 7
        Me.Label133.Text = "label4"
        '
        'trvselecteICD10s
        '
        Me.trvselecteICD10s.BackColor = System.Drawing.Color.White
        Me.trvselecteICD10s.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselecteICD10s.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselecteICD10s.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvselecteICD10s.ForeColor = System.Drawing.Color.Black
        Me.trvselecteICD10s.HideSelection = false
        Me.trvselecteICD10s.ImageIndex = 0
        Me.trvselecteICD10s.ImageList = Me.ImageList1
        Me.trvselecteICD10s.ItemHeight = 18
        Me.trvselecteICD10s.Location = New System.Drawing.Point(0, 1)
        Me.trvselecteICD10s.Name = "trvselecteICD10s"
        Me.trvselecteICD10s.SelectedImageIndex = 0
        Me.trvselecteICD10s.ShowLines = false
        Me.trvselecteICD10s.Size = New System.Drawing.Size(1076, 0)
        Me.trvselecteICD10s.TabIndex = 9
        '
        'trvselecteICDs
        '
        Me.trvselecteICDs.BackColor = System.Drawing.Color.White
        Me.trvselecteICDs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselecteICDs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselecteICDs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvselecteICDs.ForeColor = System.Drawing.Color.Black
        Me.trvselecteICDs.HideSelection = false
        Me.trvselecteICDs.ImageIndex = 0
        Me.trvselecteICDs.ImageList = Me.ImageList1
        Me.trvselecteICDs.ItemHeight = 18
        Me.trvselecteICDs.Location = New System.Drawing.Point(0, 1)
        Me.trvselecteICDs.Name = "trvselecteICDs"
        Me.trvselecteICDs.SelectedImageIndex = 0
        Me.trvselecteICDs.ShowLines = false
        Me.trvselecteICDs.Size = New System.Drawing.Size(1076, 0)
        Me.trvselecteICDs.TabIndex = 0
        '
        'Label134
        '
        Me.Label134.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label134.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label134.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label134.Location = New System.Drawing.Point(1076, 1)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(1, 0)
        Me.Label134.TabIndex = 6
        Me.Label134.Text = "label3"
        '
        'Label135
        '
        Me.Label135.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label135.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label135.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label135.Location = New System.Drawing.Point(0, 0)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(1077, 1)
        Me.Label135.TabIndex = 5
        Me.Label135.Text = "label1"
        '
        'Panel5
        '
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Panel10)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 837)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(1077, 27)
        Me.Panel5.TabIndex = 49
        '
        'Panel10
        '
        Me.Panel10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label13)
        Me.Panel10.Controls.Add(Me.Label128)
        Me.Panel10.Controls.Add(Me.Label129)
        Me.Panel10.Controls.Add(Me.Label130)
        Me.Panel10.Controls.Add(Me.Label131)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1077, 24)
        Me.Panel10.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label13.Location = New System.Drawing.Point(1076, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 22)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "label3"
        '
        'Label128
        '
        Me.Label128.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label128.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label128.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label128.Location = New System.Drawing.Point(0, 1)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(1, 22)
        Me.Label128.TabIndex = 12
        Me.Label128.Text = "label4"
        '
        'Label129
        '
        Me.Label129.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label129.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label129.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label129.Location = New System.Drawing.Point(0, 0)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(1077, 1)
        Me.Label129.TabIndex = 10
        Me.Label129.Text = "label1"
        '
        'Label130
        '
        Me.Label130.BackColor = System.Drawing.Color.Transparent
        Me.Label130.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label130.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label130.ForeColor = System.Drawing.Color.White
        Me.Label130.Image = CType(resources.GetObject("Label130.Image"),System.Drawing.Image)
        Me.Label130.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label130.Location = New System.Drawing.Point(0, 0)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(1077, 23)
        Me.Label130.TabIndex = 9
        Me.Label130.Text = "      Selected ICD9"
        Me.Label130.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label131
        '
        Me.Label131.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label131.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label131.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label131.Location = New System.Drawing.Point(0, 23)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(1077, 1)
        Me.Label131.TabIndex = 13
        Me.Label131.Text = "label2"
        '
        'Splitter4
        '
        Me.Splitter4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter4.Location = New System.Drawing.Point(0, 834)
        Me.Splitter4.Name = "Splitter4"
        Me.Splitter4.Size = New System.Drawing.Size(1077, 3)
        Me.Splitter4.TabIndex = 51
        Me.Splitter4.TabStop = false
        '
        'pnlInternalToolStripICD
        '
        Me.pnlInternalToolStripICD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripICD.Controls.Add(Me.Panel29)
        Me.pnlInternalToolStripICD.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripICD.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripICD.Name = "pnlInternalToolStripICD"
        Me.pnlInternalToolStripICD.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripICD.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripICD.TabIndex = 52
        '
        'Panel29
        '
        Me.Panel29.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel29.Controls.Add(Me.ToolStrip1)
        Me.Panel29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel29.Location = New System.Drawing.Point(0, 0)
        Me.Panel29.Name = "Panel29"
        Me.Panel29.Size = New System.Drawing.Size(1077, 54)
        Me.Panel29.TabIndex = 4
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveICD, Me.tsBtn_CancelICD, Me.tsBtn_SaveICD10})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsBtn_SaveICD
        '
        Me.tsBtn_SaveICD.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveICD.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveICD.Name = "tsBtn_SaveICD"
        Me.tsBtn_SaveICD.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveICD.Tag = "Save"
        Me.tsBtn_SaveICD.Text = "&Done"
        Me.tsBtn_SaveICD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveICD.ToolTipText = "Done"
        '
        'tsBtn_CancelICD
        '
        Me.tsBtn_CancelICD.Image = CType(resources.GetObject("tsBtn_CancelICD.Image"),System.Drawing.Image)
        Me.tsBtn_CancelICD.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelICD.Name = "tsBtn_CancelICD"
        Me.tsBtn_CancelICD.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelICD.Tag = "Cancel"
        Me.tsBtn_CancelICD.Text = "&Cancel"
        Me.tsBtn_CancelICD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelICD.Visible = false
        '
        'tsBtn_SaveICD10
        '
        Me.tsBtn_SaveICD10.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveICD10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveICD10.Name = "tsBtn_SaveICD10"
        Me.tsBtn_SaveICD10.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveICD10.Text = "Done"
        Me.tsBtn_SaveICD10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlCPT
        '
        Me.pnlCPT.Controls.Add(Me.pnlSelectedCPTs)
        Me.pnlCPT.Controls.Add(Me.pnlSelecteCPTsLabels)
        Me.pnlCPT.Controls.Add(Me.Splitter1)
        Me.pnlCPT.Controls.Add(Me.GloUC_trvCPT)
        Me.pnlCPT.Controls.Add(Me.pnlInternalToolStripCPT)
        Me.pnlCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCPT.Location = New System.Drawing.Point(0, 0)
        Me.pnlCPT.Name = "pnlCPT"
        Me.pnlCPT.Size = New System.Drawing.Size(1077, 693)
        Me.pnlCPT.TabIndex = 1
        Me.pnlCPT.Visible = false
        '
        'pnlSelectedCPTs
        '
        Me.pnlSelectedCPTs.BackColor = System.Drawing.Color.Transparent
        Me.pnlSelectedCPTs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedCPTs.Controls.Add(Me.Label205)
        Me.pnlSelectedCPTs.Controls.Add(Me.Label206)
        Me.pnlSelectedCPTs.Controls.Add(Me.trvselectedCPT)
        Me.pnlSelectedCPTs.Controls.Add(Me.Label207)
        Me.pnlSelectedCPTs.Controls.Add(Me.Label208)
        Me.pnlSelectedCPTs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSelectedCPTs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlSelectedCPTs.Location = New System.Drawing.Point(0, 476)
        Me.pnlSelectedCPTs.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedCPTs.Name = "pnlSelectedCPTs"
        Me.pnlSelectedCPTs.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedCPTs.Size = New System.Drawing.Size(1077, 217)
        Me.pnlSelectedCPTs.TabIndex = 47
        '
        'Label205
        '
        Me.Label205.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label205.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label205.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label205.Location = New System.Drawing.Point(1, 213)
        Me.Label205.Name = "Label205"
        Me.Label205.Size = New System.Drawing.Size(1075, 1)
        Me.Label205.TabIndex = 8
        Me.Label205.Text = "label2"
        '
        'Label206
        '
        Me.Label206.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label206.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label206.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label206.Location = New System.Drawing.Point(0, 1)
        Me.Label206.Name = "Label206"
        Me.Label206.Size = New System.Drawing.Size(1, 213)
        Me.Label206.TabIndex = 7
        Me.Label206.Text = "label4"
        '
        'trvselectedCPT
        '
        Me.trvselectedCPT.BackColor = System.Drawing.Color.White
        Me.trvselectedCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselectedCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedCPT.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvselectedCPT.ForeColor = System.Drawing.Color.Black
        Me.trvselectedCPT.HideSelection = false
        Me.trvselectedCPT.ImageIndex = 0
        Me.trvselectedCPT.ImageList = Me.ImageList1
        Me.trvselectedCPT.ItemHeight = 18
        Me.trvselectedCPT.Location = New System.Drawing.Point(0, 1)
        Me.trvselectedCPT.Name = "trvselectedCPT"
        Me.trvselectedCPT.SelectedImageIndex = 0
        Me.trvselectedCPT.ShowLines = false
        Me.trvselectedCPT.Size = New System.Drawing.Size(1076, 213)
        Me.trvselectedCPT.TabIndex = 0
        '
        'Label207
        '
        Me.Label207.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label207.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label207.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label207.Location = New System.Drawing.Point(1076, 1)
        Me.Label207.Name = "Label207"
        Me.Label207.Size = New System.Drawing.Size(1, 213)
        Me.Label207.TabIndex = 6
        Me.Label207.Text = "label3"
        '
        'Label208
        '
        Me.Label208.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label208.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label208.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label208.Location = New System.Drawing.Point(0, 0)
        Me.Label208.Name = "Label208"
        Me.Label208.Size = New System.Drawing.Size(1077, 1)
        Me.Label208.TabIndex = 5
        Me.Label208.Text = "label1"
        '
        'pnlSelecteCPTsLabels
        '
        Me.pnlSelecteCPTsLabels.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelecteCPTsLabels.Controls.Add(Me.Panel28)
        Me.pnlSelecteCPTsLabels.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelecteCPTsLabels.Location = New System.Drawing.Point(0, 449)
        Me.pnlSelecteCPTsLabels.Name = "pnlSelecteCPTsLabels"
        Me.pnlSelecteCPTsLabels.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelecteCPTsLabels.Size = New System.Drawing.Size(1077, 27)
        Me.pnlSelecteCPTsLabels.TabIndex = 48
        '
        'Panel28
        '
        Me.Panel28.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel28.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel28.Controls.Add(Me.Label209)
        Me.Panel28.Controls.Add(Me.Label210)
        Me.Panel28.Controls.Add(Me.Label211)
        Me.Panel28.Controls.Add(Me.Label212)
        Me.Panel28.Controls.Add(Me.Label213)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel28.Location = New System.Drawing.Point(0, 0)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Size = New System.Drawing.Size(1077, 24)
        Me.Panel28.TabIndex = 14
        '
        'Label209
        '
        Me.Label209.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label209.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label209.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label209.Location = New System.Drawing.Point(1076, 1)
        Me.Label209.Name = "Label209"
        Me.Label209.Size = New System.Drawing.Size(1, 22)
        Me.Label209.TabIndex = 11
        Me.Label209.Text = "label3"
        '
        'Label210
        '
        Me.Label210.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label210.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label210.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label210.Location = New System.Drawing.Point(0, 1)
        Me.Label210.Name = "Label210"
        Me.Label210.Size = New System.Drawing.Size(1, 22)
        Me.Label210.TabIndex = 12
        Me.Label210.Text = "label4"
        '
        'Label211
        '
        Me.Label211.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label211.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label211.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label211.Location = New System.Drawing.Point(0, 0)
        Me.Label211.Name = "Label211"
        Me.Label211.Size = New System.Drawing.Size(1077, 1)
        Me.Label211.TabIndex = 10
        Me.Label211.Text = "label1"
        '
        'Label212
        '
        Me.Label212.BackColor = System.Drawing.Color.Transparent
        Me.Label212.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label212.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label212.ForeColor = System.Drawing.Color.White
        Me.Label212.Image = CType(resources.GetObject("Label212.Image"),System.Drawing.Image)
        Me.Label212.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label212.Location = New System.Drawing.Point(0, 0)
        Me.Label212.Name = "Label212"
        Me.Label212.Size = New System.Drawing.Size(1077, 23)
        Me.Label212.TabIndex = 9
        Me.Label212.Text = "      Selected CPT"
        Me.Label212.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label213
        '
        Me.Label213.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label213.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label213.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label213.Location = New System.Drawing.Point(0, 23)
        Me.Label213.Name = "Label213"
        Me.Label213.Size = New System.Drawing.Size(1077, 1)
        Me.Label213.TabIndex = 13
        Me.Label213.Text = "label2"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 446)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1077, 3)
        Me.Splitter1.TabIndex = 49
        Me.Splitter1.TabStop = false
        '
        'pnlInternalToolStripCPT
        '
        Me.pnlInternalToolStripCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripCPT.Controls.Add(Me.Panel30)
        Me.pnlInternalToolStripCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripCPT.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripCPT.Name = "pnlInternalToolStripCPT"
        Me.pnlInternalToolStripCPT.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripCPT.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripCPT.TabIndex = 53
        '
        'Panel30
        '
        Me.Panel30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel30.Controls.Add(Me.ToolStrip2)
        Me.Panel30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel30.Location = New System.Drawing.Point(0, 0)
        Me.Panel30.Name = "Panel30"
        Me.Panel30.Size = New System.Drawing.Size(1077, 54)
        Me.Panel30.TabIndex = 4
        '
        'ToolStrip2
        '
        Me.ToolStrip2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveCPT, Me.tsBtn_CancelCPT})
        Me.ToolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'tsBtn_SaveCPT
        '
        Me.tsBtn_SaveCPT.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveCPT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveCPT.Name = "tsBtn_SaveCPT"
        Me.tsBtn_SaveCPT.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveCPT.Tag = "Save"
        Me.tsBtn_SaveCPT.Text = "&Done"
        Me.tsBtn_SaveCPT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveCPT.ToolTipText = "Done"
        '
        'tsBtn_CancelCPT
        '
        Me.tsBtn_CancelCPT.Image = CType(resources.GetObject("tsBtn_CancelCPT.Image"),System.Drawing.Image)
        Me.tsBtn_CancelCPT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelCPT.Name = "tsBtn_CancelCPT"
        Me.tsBtn_CancelCPT.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelCPT.Tag = "Cancel"
        Me.tsBtn_CancelCPT.Text = "&Cancel"
        Me.tsBtn_CancelCPT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelCPT.Visible = false
        '
        'pnlDrugs
        '
        Me.pnlDrugs.Controls.Add(Me.pnltrvSelectedDrugs)
        Me.pnlDrugs.Controls.Add(Me.pnlSelectedDrugLabel)
        Me.pnlDrugs.Controls.Add(Me.Splitter3)
        Me.pnlDrugs.Controls.Add(Me.GloUC_trvDrugs)
        Me.pnlDrugs.Controls.Add(Me.pnlInternalToolStripDrugs)
        Me.pnlDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDrugs.Location = New System.Drawing.Point(0, 0)
        Me.pnlDrugs.Name = "pnlDrugs"
        Me.pnlDrugs.Size = New System.Drawing.Size(1077, 693)
        Me.pnlDrugs.TabIndex = 1
        Me.pnlDrugs.Visible = false
        '
        'pnltrvSelectedDrugs
        '
        Me.pnltrvSelectedDrugs.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvSelectedDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvSelectedDrugs.Controls.Add(Me.Label86)
        Me.pnltrvSelectedDrugs.Controls.Add(Me.Label180)
        Me.pnltrvSelectedDrugs.Controls.Add(Me.trvSelectedDrugs)
        Me.pnltrvSelectedDrugs.Controls.Add(Me.Label181)
        Me.pnltrvSelectedDrugs.Controls.Add(Me.Label182)
        Me.pnltrvSelectedDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSelectedDrugs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnltrvSelectedDrugs.Location = New System.Drawing.Point(0, 476)
        Me.pnltrvSelectedDrugs.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedDrugs.Name = "pnltrvSelectedDrugs"
        Me.pnltrvSelectedDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedDrugs.Size = New System.Drawing.Size(1077, 217)
        Me.pnltrvSelectedDrugs.TabIndex = 21
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label86.Location = New System.Drawing.Point(1, 213)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(1075, 1)
        Me.Label86.TabIndex = 8
        Me.Label86.Text = "label2"
        '
        'Label180
        '
        Me.Label180.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label180.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label180.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label180.Location = New System.Drawing.Point(0, 1)
        Me.Label180.Name = "Label180"
        Me.Label180.Size = New System.Drawing.Size(1, 213)
        Me.Label180.TabIndex = 7
        Me.Label180.Text = "label4"
        '
        'trvSelectedDrugs
        '
        Me.trvSelectedDrugs.BackColor = System.Drawing.Color.White
        Me.trvSelectedDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedDrugs.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvSelectedDrugs.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedDrugs.HideSelection = false
        Me.trvSelectedDrugs.ImageIndex = 0
        Me.trvSelectedDrugs.ImageList = Me.ImageList1
        Me.trvSelectedDrugs.ItemHeight = 18
        Me.trvSelectedDrugs.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedDrugs.Name = "trvSelectedDrugs"
        Me.trvSelectedDrugs.SelectedImageIndex = 0
        Me.trvSelectedDrugs.ShowLines = false
        Me.trvSelectedDrugs.Size = New System.Drawing.Size(1076, 213)
        Me.trvSelectedDrugs.TabIndex = 0
        '
        'Label181
        '
        Me.Label181.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label181.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label181.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label181.Location = New System.Drawing.Point(1076, 1)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(1, 213)
        Me.Label181.TabIndex = 6
        Me.Label181.Text = "label3"
        '
        'Label182
        '
        Me.Label182.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label182.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label182.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label182.Location = New System.Drawing.Point(0, 0)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(1077, 1)
        Me.Label182.TabIndex = 5
        Me.Label182.Text = "label1"
        '
        'pnlSelectedDrugLabel
        '
        Me.pnlSelectedDrugLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedDrugLabel.Controls.Add(Me.Panel4)
        Me.pnlSelectedDrugLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectedDrugLabel.Location = New System.Drawing.Point(0, 449)
        Me.pnlSelectedDrugLabel.Name = "pnlSelectedDrugLabel"
        Me.pnlSelectedDrugLabel.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedDrugLabel.Size = New System.Drawing.Size(1077, 27)
        Me.pnlSelectedDrugLabel.TabIndex = 20
        '
        'Panel4
        '
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label185)
        Me.Panel4.Controls.Add(Me.Label184)
        Me.Panel4.Controls.Add(Me.Label186)
        Me.Panel4.Controls.Add(Me.Label187)
        Me.Panel4.Controls.Add(Me.Label183)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1077, 24)
        Me.Panel4.TabIndex = 14
        '
        'Label185
        '
        Me.Label185.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label185.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label185.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label185.Location = New System.Drawing.Point(1076, 1)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(1, 22)
        Me.Label185.TabIndex = 11
        Me.Label185.Text = "label3"
        '
        'Label184
        '
        Me.Label184.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label184.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label184.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label184.Location = New System.Drawing.Point(0, 1)
        Me.Label184.Name = "Label184"
        Me.Label184.Size = New System.Drawing.Size(1, 22)
        Me.Label184.TabIndex = 12
        Me.Label184.Text = "label4"
        '
        'Label186
        '
        Me.Label186.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label186.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label186.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label186.Location = New System.Drawing.Point(0, 0)
        Me.Label186.Name = "Label186"
        Me.Label186.Size = New System.Drawing.Size(1077, 1)
        Me.Label186.TabIndex = 10
        Me.Label186.Text = "label1"
        '
        'Label187
        '
        Me.Label187.BackColor = System.Drawing.Color.Transparent
        Me.Label187.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label187.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label187.ForeColor = System.Drawing.Color.White
        Me.Label187.Image = CType(resources.GetObject("Label187.Image"),System.Drawing.Image)
        Me.Label187.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label187.Location = New System.Drawing.Point(0, 0)
        Me.Label187.Name = "Label187"
        Me.Label187.Size = New System.Drawing.Size(1077, 23)
        Me.Label187.TabIndex = 9
        Me.Label187.Text = "      Selected Drugs"
        Me.Label187.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label183
        '
        Me.Label183.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label183.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label183.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label183.Location = New System.Drawing.Point(0, 23)
        Me.Label183.Name = "Label183"
        Me.Label183.Size = New System.Drawing.Size(1077, 1)
        Me.Label183.TabIndex = 13
        Me.Label183.Text = "label2"
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Location = New System.Drawing.Point(0, 446)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(1077, 3)
        Me.Splitter3.TabIndex = 22
        Me.Splitter3.TabStop = false
        '
        'pnlInternalToolStripDrugs
        '
        Me.pnlInternalToolStripDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripDrugs.Controls.Add(Me.Panel32)
        Me.pnlInternalToolStripDrugs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripDrugs.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripDrugs.Name = "pnlInternalToolStripDrugs"
        Me.pnlInternalToolStripDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripDrugs.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripDrugs.TabIndex = 54
        '
        'Panel32
        '
        Me.Panel32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel32.Controls.Add(Me.ToolStrip3)
        Me.Panel32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel32.Location = New System.Drawing.Point(0, 0)
        Me.Panel32.Name = "Panel32"
        Me.Panel32.Size = New System.Drawing.Size(1077, 54)
        Me.Panel32.TabIndex = 4
        '
        'ToolStrip3
        '
        Me.ToolStrip3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip3.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveDrugs, Me.tsBtn_CancelDrugs})
        Me.ToolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'tsBtn_SaveDrugs
        '
        Me.tsBtn_SaveDrugs.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveDrugs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveDrugs.Name = "tsBtn_SaveDrugs"
        Me.tsBtn_SaveDrugs.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveDrugs.Tag = "Save"
        Me.tsBtn_SaveDrugs.Text = "&Done"
        Me.tsBtn_SaveDrugs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveDrugs.ToolTipText = "Done"
        '
        'tsBtn_CancelDrugs
        '
        Me.tsBtn_CancelDrugs.Image = CType(resources.GetObject("tsBtn_CancelDrugs.Image"),System.Drawing.Image)
        Me.tsBtn_CancelDrugs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelDrugs.Name = "tsBtn_CancelDrugs"
        Me.tsBtn_CancelDrugs.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelDrugs.Tag = "Cancel"
        Me.tsBtn_CancelDrugs.Text = "&Cancel"
        Me.tsBtn_CancelDrugs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelDrugs.Visible = false
        '
        'pnlLab
        '
        Me.pnlLab.Controls.Add(Me.Panel12)
        Me.pnlLab.Controls.Add(Me.Panel13)
        Me.pnlLab.Controls.Add(Me.pnlInternalToolStripLab)
        Me.pnlLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLab.Location = New System.Drawing.Point(0, 0)
        Me.pnlLab.Name = "pnlLab"
        Me.pnlLab.Size = New System.Drawing.Size(1077, 693)
        Me.pnlLab.TabIndex = 2
        Me.pnlLab.Visible = false
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.Transparent
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel12.Controls.Add(Me.Label146)
        Me.Panel12.Controls.Add(Me.C1LabResult)
        Me.Panel12.Controls.Add(Me.Label147)
        Me.Panel12.Controls.Add(Me.Label148)
        Me.Panel12.Controls.Add(Me.Label149)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel12.Location = New System.Drawing.Point(0, 80)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel12.Size = New System.Drawing.Size(1077, 613)
        Me.Panel12.TabIndex = 20
        '
        'Label146
        '
        Me.Label146.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label146.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label146.Location = New System.Drawing.Point(1, 609)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(1075, 1)
        Me.Label146.TabIndex = 8
        Me.Label146.Text = "label2"
        '
        'C1LabResult
        '
        Me.C1LabResult.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1LabResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1LabResult.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1LabResult.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1LabResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1LabResult.ExtendLastCol = true
        Me.C1LabResult.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1LabResult.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1LabResult.Location = New System.Drawing.Point(1, 1)
        Me.C1LabResult.Name = "C1LabResult"
        Me.C1LabResult.Rows.DefaultSize = 19
        Me.C1LabResult.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1LabResult.ShowCellLabels = true
        Me.C1LabResult.Size = New System.Drawing.Size(1075, 609)
        Me.C1LabResult.StyleInfo = resources.GetString("C1LabResult.StyleInfo")
        Me.C1LabResult.TabIndex = 1
        Me.C1LabResult.Tree.NodeImageCollapsed = CType(resources.GetObject("C1LabResult.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.C1LabResult.Tree.NodeImageExpanded = CType(resources.GetObject("C1LabResult.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'Label147
        '
        Me.Label147.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label147.Location = New System.Drawing.Point(0, 1)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(1, 609)
        Me.Label147.TabIndex = 7
        Me.Label147.Text = "label4"
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label148.Location = New System.Drawing.Point(1076, 1)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(1, 609)
        Me.Label148.TabIndex = 6
        Me.Label148.Text = "label3"
        '
        'Label149
        '
        Me.Label149.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label149.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label149.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label149.Location = New System.Drawing.Point(0, 0)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(1077, 1)
        Me.Label149.TabIndex = 5
        Me.Label149.Text = "label1"
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.Panel23)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 54)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel13.Size = New System.Drawing.Size(1077, 26)
        Me.Panel13.TabIndex = 21
        '
        'Panel23
        '
        Me.Panel23.BackColor = System.Drawing.Color.Transparent
        Me.Panel23.Controls.Add(Me.txtLabResultSearch)
        Me.Panel23.Controls.Add(Me.btnLabResultClear)
        Me.Panel23.Controls.Add(Me.Label126)
        Me.Panel23.Controls.Add(Me.Label127)
        Me.Panel23.Controls.Add(Me.PictureBox2)
        Me.Panel23.Controls.Add(Me.Label136)
        Me.Panel23.Controls.Add(Me.Label137)
        Me.Panel23.Controls.Add(Me.Label138)
        Me.Panel23.Controls.Add(Me.Label139)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel23.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel23.ForeColor = System.Drawing.Color.Black
        Me.Panel23.Location = New System.Drawing.Point(0, 0)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(1077, 23)
        Me.Panel23.TabIndex = 21
        '
        'txtLabResultSearch
        '
        Me.txtLabResultSearch.BackColor = System.Drawing.Color.White
        Me.txtLabResultSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabResultSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLabResultSearch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtLabResultSearch.ForeColor = System.Drawing.Color.Black
        Me.txtLabResultSearch.Location = New System.Drawing.Point(29, 5)
        Me.txtLabResultSearch.Name = "txtLabResultSearch"
        Me.txtLabResultSearch.Size = New System.Drawing.Size(1026, 15)
        Me.txtLabResultSearch.TabIndex = 0
        '
        'btnLabResultClear
        '
        Me.btnLabResultClear.BackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear.BackgroundImage = CType(resources.GetObject("btnLabResultClear.BackgroundImage"),System.Drawing.Image)
        Me.btnLabResultClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabResultClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLabResultClear.FlatAppearance.BorderSize = 0
        Me.btnLabResultClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabResultClear.Image = CType(resources.GetObject("btnLabResultClear.Image"),System.Drawing.Image)
        Me.btnLabResultClear.Location = New System.Drawing.Point(1055, 5)
        Me.btnLabResultClear.Name = "btnLabResultClear"
        Me.btnLabResultClear.Size = New System.Drawing.Size(21, 15)
        Me.btnLabResultClear.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.btnLabResultClear, "Clear Search")
        Me.btnLabResultClear.UseVisualStyleBackColor = false
        '
        'Label126
        '
        Me.Label126.BackColor = System.Drawing.Color.White
        Me.Label126.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label126.Location = New System.Drawing.Point(29, 1)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(1047, 4)
        Me.Label126.TabIndex = 37
        '
        'Label127
        '
        Me.Label127.BackColor = System.Drawing.Color.White
        Me.Label127.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label127.Location = New System.Drawing.Point(29, 20)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(1047, 2)
        Me.Label127.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"),System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = false
        '
        'Label136
        '
        Me.Label136.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label136.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label136.Location = New System.Drawing.Point(1, 22)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(1075, 1)
        Me.Label136.TabIndex = 35
        Me.Label136.Text = "label1"
        '
        'Label137
        '
        Me.Label137.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label137.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label137.Location = New System.Drawing.Point(1, 0)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(1075, 1)
        Me.Label137.TabIndex = 36
        Me.Label137.Text = "label1"
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label138.Location = New System.Drawing.Point(0, 0)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(1, 23)
        Me.Label138.TabIndex = 39
        Me.Label138.Text = "label4"
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label139.Location = New System.Drawing.Point(1076, 0)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(1, 23)
        Me.Label139.TabIndex = 40
        Me.Label139.Text = "label4"
        '
        'pnlInternalToolStripLab
        '
        Me.pnlInternalToolStripLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripLab.Controls.Add(Me.Panel40)
        Me.pnlInternalToolStripLab.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripLab.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripLab.Name = "pnlInternalToolStripLab"
        Me.pnlInternalToolStripLab.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripLab.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripLab.TabIndex = 55
        '
        'Panel40
        '
        Me.Panel40.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel40.Controls.Add(Me.ToolStrip5)
        Me.Panel40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel40.Location = New System.Drawing.Point(0, 0)
        Me.Panel40.Name = "Panel40"
        Me.Panel40.Size = New System.Drawing.Size(1077, 54)
        Me.Panel40.TabIndex = 4
        '
        'ToolStrip5
        '
        Me.ToolStrip5.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip5.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip5.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip5.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveLab, Me.tsBtn_CancelLab})
        Me.ToolStrip5.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip5.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip5.Name = "ToolStrip5"
        Me.ToolStrip5.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip5.TabIndex = 0
        Me.ToolStrip5.Text = "ToolStrip5"
        '
        'tsBtn_SaveLab
        '
        Me.tsBtn_SaveLab.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveLab.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveLab.Name = "tsBtn_SaveLab"
        Me.tsBtn_SaveLab.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveLab.Tag = "Save"
        Me.tsBtn_SaveLab.Text = "&Done"
        Me.tsBtn_SaveLab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveLab.ToolTipText = "Done"
        '
        'tsBtn_CancelLab
        '
        Me.tsBtn_CancelLab.Image = CType(resources.GetObject("tsBtn_CancelLab.Image"),System.Drawing.Image)
        Me.tsBtn_CancelLab.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelLab.Name = "tsBtn_CancelLab"
        Me.tsBtn_CancelLab.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelLab.Tag = "Cancel"
        Me.tsBtn_CancelLab.Text = "&Cancel"
        Me.tsBtn_CancelLab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelLab.Visible = false
        '
        'tbPg_Exceptions
        '
        Me.tbPg_Exceptions.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tbPg_Exceptions.Controls.Add(Me.pnlExceptionsMiddle)
        Me.tbPg_Exceptions.Location = New System.Drawing.Point(4, 22)
        Me.tbPg_Exceptions.Name = "tbPg_Exceptions"
        Me.tbPg_Exceptions.Padding = New System.Windows.Forms.Padding(3)
        Me.tbPg_Exceptions.Size = New System.Drawing.Size(1083, 700)
        Me.tbPg_Exceptions.TabIndex = 1
        Me.tbPg_Exceptions.Text = "Exceptions  "
        '
        'pnlExceptionsMiddle
        '
        Me.pnlExceptionsMiddle.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnlExceptionsMiddle.Controls.Add(Me.pnlExceptionsDemoVitals)
        Me.pnlExceptionsMiddle.Controls.Add(Me.pnlExceptionsRadiology)
        Me.pnlExceptionsMiddle.Controls.Add(Me.pnlExceptionsHistory)
        Me.pnlExceptionsMiddle.Controls.Add(Me.pnlExceptionsInsurance)
        Me.pnlExceptionsMiddle.Controls.Add(Me.pnlExceptionsCPT)
        Me.pnlExceptionsMiddle.Controls.Add(Me.pnlExceptionsDrugs)
        Me.pnlExceptionsMiddle.Controls.Add(Me.pnlExceptionsLab)
        Me.pnlExceptionsMiddle.Controls.Add(Me.pnlExceptionsICD9)
        Me.pnlExceptionsMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExceptionsMiddle.Location = New System.Drawing.Point(3, 3)
        Me.pnlExceptionsMiddle.Name = "pnlExceptionsMiddle"
        Me.pnlExceptionsMiddle.Size = New System.Drawing.Size(1077, 694)
        Me.pnlExceptionsMiddle.TabIndex = 2
        '
        'pnlExceptionsDemoVitals
        '
        Me.pnlExceptionsDemoVitals.Controls.Add(Me.Panel45)
        Me.pnlExceptionsDemoVitals.Controls.Add(Me.btnproblemlist_Ex)
        Me.pnlExceptionsDemoVitals.Controls.Add(Me.btnDemographics_Ex)
        Me.pnlExceptionsDemoVitals.Controls.Add(Me.Panel46)
        Me.pnlExceptionsDemoVitals.Controls.Add(Me.pnlVitals_Ex)
        Me.pnlExceptionsDemoVitals.Controls.Add(Me.Panel49)
        Me.pnlExceptionsDemoVitals.Controls.Add(Me.pnlDemographics_Ex)
        Me.pnlExceptionsDemoVitals.Controls.Add(Me.Panel52)
        Me.pnlExceptionsDemoVitals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExceptionsDemoVitals.Location = New System.Drawing.Point(0, 0)
        Me.pnlExceptionsDemoVitals.Name = "pnlExceptionsDemoVitals"
        Me.pnlExceptionsDemoVitals.Size = New System.Drawing.Size(1077, 694)
        Me.pnlExceptionsDemoVitals.TabIndex = 0
        '
        'Panel45
        '
        Me.Panel45.AutoScroll = true
        Me.Panel45.Controls.Add(Me.btnRemoveSelectedInsurance_Ex)
        Me.Panel45.Controls.Add(Me.lstExVw_Insurance)
        Me.Panel45.Controls.Add(Me.btnClearInsuranceEx)
        Me.Panel45.Controls.Add(Me.btnInsuranceEx)
        Me.Panel45.Controls.Add(Me.btnRemoveSelectedICD10_Ex)
        Me.Panel45.Controls.Add(Me.btnClearICD10Ex)
        Me.Panel45.Controls.Add(Me.lstExVw_ICD10)
        Me.Panel45.Controls.Add(Me.btnICD10Ex)
        Me.Panel45.Controls.Add(Me.btnRemoveSelectedDrug_Ex)
        Me.Panel45.Controls.Add(Me.btnRemoveSelectedOrders_Ex)
        Me.Panel45.Controls.Add(Me.btnRemoveSelectedICD9_Ex)
        Me.Panel45.Controls.Add(Me.btnRemoveSelectedLab_Ex)
        Me.Panel45.Controls.Add(Me.btnRemoveSelectedCPT_Ex)
        Me.Panel45.Controls.Add(Me.btnRemoveSelectedHistory_Ex)
        Me.Panel45.Controls.Add(Me.btnRemoveSelectedSnomedCodeEx)
        Me.Panel45.Controls.Add(Me.btnClearSnomedEx)
        Me.Panel45.Controls.Add(Me.btnSnomedEx)
        Me.Panel45.Controls.Add(Me.lstExVw_SnoMed)
        Me.Panel45.Controls.Add(Me.Label39)
        Me.Panel45.Controls.Add(Me.Label41)
        Me.Panel45.Controls.Add(Me.Label42)
        Me.Panel45.Controls.Add(Me.Label43)
        Me.Panel45.Controls.Add(Me.btnClearICD9Ex)
        Me.Panel45.Controls.Add(Me.btnClearCPTEx)
        Me.Panel45.Controls.Add(Me.lstExVw_Orders)
        Me.Panel45.Controls.Add(Me.lstExVw_Drugs)
        Me.Panel45.Controls.Add(Me.btnClearHistoryEx)
        Me.Panel45.Controls.Add(Me.lstExVw_Lab)
        Me.Panel45.Controls.Add(Me.lstExVw_ICD)
        Me.Panel45.Controls.Add(Me.btnClearOrdersEx)
        Me.Panel45.Controls.Add(Me.lstExVw_CPT)
        Me.Panel45.Controls.Add(Me.btnClearDrugsEx)
        Me.Panel45.Controls.Add(Me.lstExVw_History)
        Me.Panel45.Controls.Add(Me.btnClearLabEx)
        Me.Panel45.Controls.Add(Me.btnHistoryEx)
        Me.Panel45.Controls.Add(Me.btnRadiologyEx)
        Me.Panel45.Controls.Add(Me.btnDrugsEx)
        Me.Panel45.Controls.Add(Me.btnLabEx)
        Me.Panel45.Controls.Add(Me.btnICD9Ex)
        Me.Panel45.Controls.Add(Me.btnCPTEx)
        Me.Panel45.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel45.Location = New System.Drawing.Point(0, 297)
        Me.Panel45.Name = "Panel45"
        Me.Panel45.Size = New System.Drawing.Size(1077, 397)
        Me.Panel45.TabIndex = 90
        '
        'btnRemoveSelectedInsurance_Ex
        '
        Me.btnRemoveSelectedInsurance_Ex.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedInsurance_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedInsurance_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedInsurance_Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedInsurance_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedInsurance_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedInsurance_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedInsurance_Ex.Image = CType(resources.GetObject("btnRemoveSelectedInsurance_Ex.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedInsurance_Ex.Location = New System.Drawing.Point(995, 286)
        Me.btnRemoveSelectedInsurance_Ex.Name = "btnRemoveSelectedInsurance_Ex"
        Me.btnRemoveSelectedInsurance_Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedInsurance_Ex.TabIndex = 109
        Me.btnRemoveSelectedInsurance_Ex.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedInsurance_Ex, "Remove selected Insurance Plan")
        Me.btnRemoveSelectedInsurance_Ex.UseVisualStyleBackColor = true
        '
        'lstExVw_Insurance
        '
        Me.lstExVw_Insurance.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader18})
        Me.lstExVw_Insurance.Location = New System.Drawing.Point(714, 259)
        Me.lstExVw_Insurance.MultiSelect = false
        Me.lstExVw_Insurance.Name = "lstExVw_Insurance"
        Me.lstExVw_Insurance.Size = New System.Drawing.Size(275, 119)
        Me.lstExVw_Insurance.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstExVw_Insurance.TabIndex = 108
        Me.lstExVw_Insurance.UseCompatibleStateImageBehavior = false
        Me.lstExVw_Insurance.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "Insurance Plan"
        Me.ColumnHeader18.Width = 277
        '
        'btnClearInsuranceEx
        '
        Me.btnClearInsuranceEx.BackgroundImage = CType(resources.GetObject("btnClearInsuranceEx.BackgroundImage"),System.Drawing.Image)
        Me.btnClearInsuranceEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearInsuranceEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearInsuranceEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearInsuranceEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearInsuranceEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearInsuranceEx.Image = CType(resources.GetObject("btnClearInsuranceEx.Image"),System.Drawing.Image)
        Me.btnClearInsuranceEx.Location = New System.Drawing.Point(995, 313)
        Me.btnClearInsuranceEx.Name = "btnClearInsuranceEx"
        Me.btnClearInsuranceEx.Size = New System.Drawing.Size(24, 24)
        Me.btnClearInsuranceEx.TabIndex = 107
        Me.ToolTip1.SetToolTip(Me.btnClearInsuranceEx, "Clear Insurance Plans")
        Me.btnClearInsuranceEx.UseVisualStyleBackColor = true
        '
        'btnInsuranceEx
        '
        Me.btnInsuranceEx.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnInsuranceEx.BackgroundImage = CType(resources.GetObject("btnInsuranceEx.BackgroundImage"),System.Drawing.Image)
        Me.btnInsuranceEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInsuranceEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnInsuranceEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnInsuranceEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnInsuranceEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInsuranceEx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnInsuranceEx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnInsuranceEx.Image = CType(resources.GetObject("btnInsuranceEx.Image"),System.Drawing.Image)
        Me.btnInsuranceEx.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnInsuranceEx.Location = New System.Drawing.Point(995, 259)
        Me.btnInsuranceEx.Name = "btnInsuranceEx"
        Me.btnInsuranceEx.Size = New System.Drawing.Size(24, 24)
        Me.btnInsuranceEx.TabIndex = 106
        Me.btnInsuranceEx.Tag = "UnSelected"
        Me.btnInsuranceEx.Text = "      Drugs"
        Me.ToolTip1.SetToolTip(Me.btnInsuranceEx, "Select Insurance Plans")
        Me.btnInsuranceEx.UseVisualStyleBackColor = false
        '
        'btnRemoveSelectedICD10_Ex
        '
        Me.btnRemoveSelectedICD10_Ex.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedICD10_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedICD10_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedICD10_Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedICD10_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD10_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD10_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedICD10_Ex.Image = CType(resources.GetObject("btnRemoveSelectedICD10_Ex.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedICD10_Ex.Location = New System.Drawing.Point(660, 162)
        Me.btnRemoveSelectedICD10_Ex.Name = "btnRemoveSelectedICD10_Ex"
        Me.btnRemoveSelectedICD10_Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedICD10_Ex.TabIndex = 105
        Me.btnRemoveSelectedICD10_Ex.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedICD10_Ex, "Remove selected ICD10")
        Me.btnRemoveSelectedICD10_Ex.UseVisualStyleBackColor = true
        '
        'btnClearICD10Ex
        '
        Me.btnClearICD10Ex.BackgroundImage = CType(resources.GetObject("btnClearICD10Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnClearICD10Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearICD10Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearICD10Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD10Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD10Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearICD10Ex.Image = CType(resources.GetObject("btnClearICD10Ex.Image"),System.Drawing.Image)
        Me.btnClearICD10Ex.Location = New System.Drawing.Point(660, 189)
        Me.btnClearICD10Ex.Name = "btnClearICD10Ex"
        Me.btnClearICD10Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnClearICD10Ex.TabIndex = 103
        Me.ToolTip1.SetToolTip(Me.btnClearICD10Ex, "Clear ICD10")
        Me.btnClearICD10Ex.UseVisualStyleBackColor = true
        '
        'lstExVw_ICD10
        '
        Me.lstExVw_ICD10.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader16})
        Me.lstExVw_ICD10.Location = New System.Drawing.Point(381, 134)
        Me.lstExVw_ICD10.MultiSelect = false
        Me.lstExVw_ICD10.Name = "lstExVw_ICD10"
        Me.lstExVw_ICD10.Size = New System.Drawing.Size(275, 119)
        Me.lstExVw_ICD10.TabIndex = 104
        Me.lstExVw_ICD10.UseCompatibleStateImageBehavior = false
        Me.lstExVw_ICD10.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "ICD10"
        Me.ColumnHeader16.Width = 277
        '
        'btnICD10Ex
        '
        Me.btnICD10Ex.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnICD10Ex.BackgroundImage = CType(resources.GetObject("btnICD10Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnICD10Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD10Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnICD10Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD10Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD10Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD10Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnICD10Ex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnICD10Ex.Image = CType(resources.GetObject("btnICD10Ex.Image"),System.Drawing.Image)
        Me.btnICD10Ex.Location = New System.Drawing.Point(660, 135)
        Me.btnICD10Ex.Name = "btnICD10Ex"
        Me.btnICD10Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnICD10Ex.TabIndex = 102
        Me.btnICD10Ex.Tag = "UnSelected"
        Me.btnICD10Ex.Text = "      ICD9"
        Me.ToolTip1.SetToolTip(Me.btnICD10Ex, "Select ICD10")
        Me.btnICD10Ex.UseVisualStyleBackColor = false
        '
        'btnRemoveSelectedDrug_Ex
        '
        Me.btnRemoveSelectedDrug_Ex.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedDrug_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedDrug_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedDrug_Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedDrug_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedDrug_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedDrug_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedDrug_Ex.Image = CType(resources.GetObject("btnRemoveSelectedDrug_Ex.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedDrug_Ex.Location = New System.Drawing.Point(330, 286)
        Me.btnRemoveSelectedDrug_Ex.Name = "btnRemoveSelectedDrug_Ex"
        Me.btnRemoveSelectedDrug_Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedDrug_Ex.TabIndex = 101
        Me.btnRemoveSelectedDrug_Ex.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedDrug_Ex, "Remove selected drugs")
        Me.btnRemoveSelectedDrug_Ex.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedOrders_Ex
        '
        Me.btnRemoveSelectedOrders_Ex.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedOrders_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedOrders_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedOrders_Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedOrders_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedOrders_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedOrders_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedOrders_Ex.Image = CType(resources.GetObject("btnRemoveSelectedOrders_Ex.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedOrders_Ex.Location = New System.Drawing.Point(995, 37)
        Me.btnRemoveSelectedOrders_Ex.Name = "btnRemoveSelectedOrders_Ex"
        Me.btnRemoveSelectedOrders_Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedOrders_Ex.TabIndex = 101
        Me.btnRemoveSelectedOrders_Ex.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedOrders_Ex, "Remove selected orders")
        Me.btnRemoveSelectedOrders_Ex.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedICD9_Ex
        '
        Me.btnRemoveSelectedICD9_Ex.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedICD9_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedICD9_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedICD9_Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedICD9_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD9_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD9_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedICD9_Ex.Image = CType(resources.GetObject("btnRemoveSelectedICD9_Ex.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedICD9_Ex.Location = New System.Drawing.Point(660, 37)
        Me.btnRemoveSelectedICD9_Ex.Name = "btnRemoveSelectedICD9_Ex"
        Me.btnRemoveSelectedICD9_Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedICD9_Ex.TabIndex = 101
        Me.btnRemoveSelectedICD9_Ex.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedICD9_Ex, "Remove selected ICD9")
        Me.btnRemoveSelectedICD9_Ex.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedLab_Ex
        '
        Me.btnRemoveSelectedLab_Ex.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedLab_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedLab_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedLab_Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedLab_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedLab_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedLab_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedLab_Ex.Image = CType(resources.GetObject("btnRemoveSelectedLab_Ex.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedLab_Ex.Location = New System.Drawing.Point(662, 286)
        Me.btnRemoveSelectedLab_Ex.Name = "btnRemoveSelectedLab_Ex"
        Me.btnRemoveSelectedLab_Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedLab_Ex.TabIndex = 101
        Me.btnRemoveSelectedLab_Ex.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedLab_Ex, "Remove selected Labs")
        Me.btnRemoveSelectedLab_Ex.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedCPT_Ex
        '
        Me.btnRemoveSelectedCPT_Ex.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedCPT_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedCPT_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedCPT_Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedCPT_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedCPT_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedCPT_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedCPT_Ex.Image = CType(resources.GetObject("btnRemoveSelectedCPT_Ex.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedCPT_Ex.Location = New System.Drawing.Point(328, 161)
        Me.btnRemoveSelectedCPT_Ex.Name = "btnRemoveSelectedCPT_Ex"
        Me.btnRemoveSelectedCPT_Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedCPT_Ex.TabIndex = 101
        Me.btnRemoveSelectedCPT_Ex.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedCPT_Ex, "Remove selected CPT")
        Me.btnRemoveSelectedCPT_Ex.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedHistory_Ex
        '
        Me.btnRemoveSelectedHistory_Ex.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedHistory_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedHistory_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedHistory_Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedHistory_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedHistory_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedHistory_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedHistory_Ex.Image = CType(resources.GetObject("btnRemoveSelectedHistory_Ex.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedHistory_Ex.Location = New System.Drawing.Point(328, 36)
        Me.btnRemoveSelectedHistory_Ex.Name = "btnRemoveSelectedHistory_Ex"
        Me.btnRemoveSelectedHistory_Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedHistory_Ex.TabIndex = 101
        Me.btnRemoveSelectedHistory_Ex.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedHistory_Ex, "Remove selected history")
        Me.btnRemoveSelectedHistory_Ex.UseVisualStyleBackColor = true
        '
        'btnRemoveSelectedSnomedCodeEx
        '
        Me.btnRemoveSelectedSnomedCodeEx.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedSnomedCodeEx.BackgroundImage"),System.Drawing.Image)
        Me.btnRemoveSelectedSnomedCodeEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedSnomedCodeEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRemoveSelectedSnomedCodeEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedSnomedCodeEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedSnomedCodeEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedSnomedCodeEx.Image = CType(resources.GetObject("btnRemoveSelectedSnomedCodeEx.Image"),System.Drawing.Image)
        Me.btnRemoveSelectedSnomedCodeEx.Location = New System.Drawing.Point(995, 161)
        Me.btnRemoveSelectedSnomedCodeEx.Name = "btnRemoveSelectedSnomedCodeEx"
        Me.btnRemoveSelectedSnomedCodeEx.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedSnomedCodeEx.TabIndex = 99
        Me.btnRemoveSelectedSnomedCodeEx.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedSnomedCodeEx, "Remove selected snomed codes")
        Me.btnRemoveSelectedSnomedCodeEx.UseVisualStyleBackColor = true
        '
        'btnClearSnomedEx
        '
        Me.btnClearSnomedEx.BackgroundImage = CType(resources.GetObject("btnClearSnomedEx.BackgroundImage"),System.Drawing.Image)
        Me.btnClearSnomedEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSnomedEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearSnomedEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSnomedEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearSnomedEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSnomedEx.Image = CType(resources.GetObject("btnClearSnomedEx.Image"),System.Drawing.Image)
        Me.btnClearSnomedEx.Location = New System.Drawing.Point(995, 188)
        Me.btnClearSnomedEx.Name = "btnClearSnomedEx"
        Me.btnClearSnomedEx.Size = New System.Drawing.Size(24, 24)
        Me.btnClearSnomedEx.TabIndex = 100
        Me.ToolTip1.SetToolTip(Me.btnClearSnomedEx, "Clear snomed codes")
        Me.btnClearSnomedEx.UseVisualStyleBackColor = true
        '
        'btnSnomedEx
        '
        Me.btnSnomedEx.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnSnomedEx.BackgroundImage = CType(resources.GetObject("btnSnomedEx.BackgroundImage"),System.Drawing.Image)
        Me.btnSnomedEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSnomedEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSnomedEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSnomedEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSnomedEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSnomedEx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnSnomedEx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnSnomedEx.Image = CType(resources.GetObject("btnSnomedEx.Image"),System.Drawing.Image)
        Me.btnSnomedEx.Location = New System.Drawing.Point(995, 134)
        Me.btnSnomedEx.Name = "btnSnomedEx"
        Me.btnSnomedEx.Size = New System.Drawing.Size(24, 24)
        Me.btnSnomedEx.TabIndex = 98
        Me.btnSnomedEx.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnSnomedEx, "Add Snomed Code")
        Me.btnSnomedEx.UseVisualStyleBackColor = false
        '
        'lstExVw_SnoMed
        '
        Me.lstExVw_SnoMed.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader14})
        Me.lstExVw_SnoMed.Location = New System.Drawing.Point(714, 134)
        Me.lstExVw_SnoMed.MultiSelect = false
        Me.lstExVw_SnoMed.Name = "lstExVw_SnoMed"
        Me.lstExVw_SnoMed.Size = New System.Drawing.Size(275, 119)
        Me.lstExVw_SnoMed.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstExVw_SnoMed.TabIndex = 97
        Me.lstExVw_SnoMed.UseCompatibleStateImageBehavior = false
        Me.lstExVw_SnoMed.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Snomed"
        Me.ColumnHeader14.Width = 272
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label39.Location = New System.Drawing.Point(1, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1075, 1)
        Me.Label39.TabIndex = 93
        Me.Label39.Text = "label2"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label41.Location = New System.Drawing.Point(1, 396)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1075, 1)
        Me.Label41.TabIndex = 92
        Me.Label41.Text = "label2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label42.Location = New System.Drawing.Point(1076, 0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 397)
        Me.Label42.TabIndex = 91
        Me.Label42.Text = "label4"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label43.Location = New System.Drawing.Point(0, 0)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 397)
        Me.Label43.TabIndex = 90
        Me.Label43.Text = "label4"
        '
        'btnClearICD9Ex
        '
        Me.btnClearICD9Ex.BackgroundImage = CType(resources.GetObject("btnClearICD9Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnClearICD9Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearICD9Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearICD9Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD9Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD9Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearICD9Ex.Image = CType(resources.GetObject("btnClearICD9Ex.Image"),System.Drawing.Image)
        Me.btnClearICD9Ex.Location = New System.Drawing.Point(660, 64)
        Me.btnClearICD9Ex.Name = "btnClearICD9Ex"
        Me.btnClearICD9Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnClearICD9Ex.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.btnClearICD9Ex, "Clear ICD9")
        Me.btnClearICD9Ex.UseVisualStyleBackColor = true
        '
        'btnClearCPTEx
        '
        Me.btnClearCPTEx.BackgroundImage = CType(resources.GetObject("btnClearCPTEx.BackgroundImage"),System.Drawing.Image)
        Me.btnClearCPTEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCPTEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearCPTEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearCPTEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearCPTEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCPTEx.Image = CType(resources.GetObject("btnClearCPTEx.Image"),System.Drawing.Image)
        Me.btnClearCPTEx.Location = New System.Drawing.Point(328, 188)
        Me.btnClearCPTEx.Name = "btnClearCPTEx"
        Me.btnClearCPTEx.Size = New System.Drawing.Size(24, 24)
        Me.btnClearCPTEx.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.btnClearCPTEx, "Clear CPT")
        Me.btnClearCPTEx.UseVisualStyleBackColor = true
        '
        'lstExVw_Orders
        '
        Me.lstExVw_Orders.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.lstExVw_Orders.Location = New System.Drawing.Point(714, 9)
        Me.lstExVw_Orders.MultiSelect = false
        Me.lstExVw_Orders.Name = "lstExVw_Orders"
        Me.lstExVw_Orders.Size = New System.Drawing.Size(275, 119)
        Me.lstExVw_Orders.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstExVw_Orders.TabIndex = 83
        Me.lstExVw_Orders.UseCompatibleStateImageBehavior = false
        Me.lstExVw_Orders.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Order Templates"
        Me.ColumnHeader2.Width = 272
        '
        'lstExVw_Drugs
        '
        Me.lstExVw_Drugs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6})
        Me.lstExVw_Drugs.Location = New System.Drawing.Point(48, 259)
        Me.lstExVw_Drugs.MultiSelect = false
        Me.lstExVw_Drugs.Name = "lstExVw_Drugs"
        Me.lstExVw_Drugs.Size = New System.Drawing.Size(275, 119)
        Me.lstExVw_Drugs.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstExVw_Drugs.TabIndex = 82
        Me.lstExVw_Drugs.UseCompatibleStateImageBehavior = false
        Me.lstExVw_Drugs.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Drugs"
        Me.ColumnHeader6.Width = 277
        '
        'btnClearHistoryEx
        '
        Me.btnClearHistoryEx.BackgroundImage = CType(resources.GetObject("btnClearHistoryEx.BackgroundImage"),System.Drawing.Image)
        Me.btnClearHistoryEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearHistoryEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearHistoryEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearHistoryEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearHistoryEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearHistoryEx.Image = CType(resources.GetObject("btnClearHistoryEx.Image"),System.Drawing.Image)
        Me.btnClearHistoryEx.Location = New System.Drawing.Point(328, 63)
        Me.btnClearHistoryEx.Name = "btnClearHistoryEx"
        Me.btnClearHistoryEx.Size = New System.Drawing.Size(24, 24)
        Me.btnClearHistoryEx.TabIndex = 72
        Me.ToolTip1.SetToolTip(Me.btnClearHistoryEx, "Clear History")
        Me.btnClearHistoryEx.UseVisualStyleBackColor = true
        '
        'lstExVw_Lab
        '
        Me.lstExVw_Lab.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader9})
        Me.lstExVw_Lab.Location = New System.Drawing.Point(381, 259)
        Me.lstExVw_Lab.MultiSelect = false
        Me.lstExVw_Lab.Name = "lstExVw_Lab"
        Me.lstExVw_Lab.Size = New System.Drawing.Size(275, 119)
        Me.lstExVw_Lab.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstExVw_Lab.TabIndex = 81
        Me.lstExVw_Lab.UseCompatibleStateImageBehavior = false
        Me.lstExVw_Lab.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Orders"
        Me.ColumnHeader9.Width = 277
        '
        'lstExVw_ICD
        '
        Me.lstExVw_ICD.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader10})
        Me.lstExVw_ICD.Location = New System.Drawing.Point(381, 9)
        Me.lstExVw_ICD.MultiSelect = false
        Me.lstExVw_ICD.Name = "lstExVw_ICD"
        Me.lstExVw_ICD.Size = New System.Drawing.Size(275, 119)
        Me.lstExVw_ICD.TabIndex = 75
        Me.lstExVw_ICD.UseCompatibleStateImageBehavior = false
        Me.lstExVw_ICD.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "ICD9"
        Me.ColumnHeader10.Width = 277
        '
        'btnClearOrdersEx
        '
        Me.btnClearOrdersEx.BackgroundImage = CType(resources.GetObject("btnClearOrdersEx.BackgroundImage"),System.Drawing.Image)
        Me.btnClearOrdersEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearOrdersEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearOrdersEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearOrdersEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearOrdersEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearOrdersEx.Image = CType(resources.GetObject("btnClearOrdersEx.Image"),System.Drawing.Image)
        Me.btnClearOrdersEx.Location = New System.Drawing.Point(995, 64)
        Me.btnClearOrdersEx.Name = "btnClearOrdersEx"
        Me.btnClearOrdersEx.Size = New System.Drawing.Size(24, 24)
        Me.btnClearOrdersEx.TabIndex = 80
        Me.ToolTip1.SetToolTip(Me.btnClearOrdersEx, "Clear Orders")
        Me.btnClearOrdersEx.UseVisualStyleBackColor = true
        '
        'lstExVw_CPT
        '
        Me.lstExVw_CPT.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader11})
        Me.lstExVw_CPT.Location = New System.Drawing.Point(48, 134)
        Me.lstExVw_CPT.MultiSelect = false
        Me.lstExVw_CPT.Name = "lstExVw_CPT"
        Me.lstExVw_CPT.Size = New System.Drawing.Size(275, 119)
        Me.lstExVw_CPT.TabIndex = 76
        Me.lstExVw_CPT.UseCompatibleStateImageBehavior = false
        Me.lstExVw_CPT.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "CPT"
        Me.ColumnHeader11.Width = 277
        '
        'btnClearDrugsEx
        '
        Me.btnClearDrugsEx.BackgroundImage = CType(resources.GetObject("btnClearDrugsEx.BackgroundImage"),System.Drawing.Image)
        Me.btnClearDrugsEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearDrugsEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearDrugsEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearDrugsEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearDrugsEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearDrugsEx.Image = CType(resources.GetObject("btnClearDrugsEx.Image"),System.Drawing.Image)
        Me.btnClearDrugsEx.Location = New System.Drawing.Point(330, 313)
        Me.btnClearDrugsEx.Name = "btnClearDrugsEx"
        Me.btnClearDrugsEx.Size = New System.Drawing.Size(24, 24)
        Me.btnClearDrugsEx.TabIndex = 78
        Me.ToolTip1.SetToolTip(Me.btnClearDrugsEx, "Clear Drugs")
        Me.btnClearDrugsEx.UseVisualStyleBackColor = true
        '
        'lstExVw_History
        '
        Me.lstExVw_History.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12})
        Me.lstExVw_History.Location = New System.Drawing.Point(48, 9)
        Me.lstExVw_History.MultiSelect = false
        Me.lstExVw_History.Name = "lstExVw_History"
        Me.lstExVw_History.Size = New System.Drawing.Size(275, 119)
        Me.lstExVw_History.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstExVw_History.TabIndex = 77
        Me.lstExVw_History.UseCompatibleStateImageBehavior = false
        Me.lstExVw_History.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "History"
        Me.ColumnHeader12.Width = 272
        '
        'btnClearLabEx
        '
        Me.btnClearLabEx.BackgroundImage = CType(resources.GetObject("btnClearLabEx.BackgroundImage"),System.Drawing.Image)
        Me.btnClearLabEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearLabEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnClearLabEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearLabEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearLabEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearLabEx.Image = CType(resources.GetObject("btnClearLabEx.Image"),System.Drawing.Image)
        Me.btnClearLabEx.Location = New System.Drawing.Point(662, 313)
        Me.btnClearLabEx.Name = "btnClearLabEx"
        Me.btnClearLabEx.Size = New System.Drawing.Size(24, 24)
        Me.btnClearLabEx.TabIndex = 79
        Me.ToolTip1.SetToolTip(Me.btnClearLabEx, "Clear Lab")
        Me.btnClearLabEx.UseVisualStyleBackColor = true
        '
        'btnHistoryEx
        '
        Me.btnHistoryEx.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnHistoryEx.BackgroundImage = CType(resources.GetObject("btnHistoryEx.BackgroundImage"),System.Drawing.Image)
        Me.btnHistoryEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHistoryEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnHistoryEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnHistoryEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnHistoryEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistoryEx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnHistoryEx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnHistoryEx.Image = CType(resources.GetObject("btnHistoryEx.Image"),System.Drawing.Image)
        Me.btnHistoryEx.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHistoryEx.Location = New System.Drawing.Point(328, 9)
        Me.btnHistoryEx.Name = "btnHistoryEx"
        Me.btnHistoryEx.Size = New System.Drawing.Size(24, 24)
        Me.btnHistoryEx.TabIndex = 0
        Me.btnHistoryEx.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnHistoryEx, "Select History")
        Me.btnHistoryEx.UseVisualStyleBackColor = false
        '
        'btnRadiologyEx
        '
        Me.btnRadiologyEx.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnRadiologyEx.BackgroundImage = CType(resources.GetObject("btnRadiologyEx.BackgroundImage"),System.Drawing.Image)
        Me.btnRadiologyEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiologyEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRadiologyEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRadiologyEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRadiologyEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiologyEx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnRadiologyEx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnRadiologyEx.Image = CType(resources.GetObject("btnRadiologyEx.Image"),System.Drawing.Image)
        Me.btnRadiologyEx.Location = New System.Drawing.Point(995, 10)
        Me.btnRadiologyEx.Name = "btnRadiologyEx"
        Me.btnRadiologyEx.Size = New System.Drawing.Size(24, 24)
        Me.btnRadiologyEx.TabIndex = 0
        Me.btnRadiologyEx.Tag = "UnSelected"
        Me.btnRadiologyEx.Text = "      Orders"
        Me.btnRadiologyEx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.btnRadiologyEx, "Select Orders")
        Me.btnRadiologyEx.UseVisualStyleBackColor = false
        '
        'btnDrugsEx
        '
        Me.btnDrugsEx.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnDrugsEx.BackgroundImage = CType(resources.GetObject("btnDrugsEx.BackgroundImage"),System.Drawing.Image)
        Me.btnDrugsEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugsEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnDrugsEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDrugsEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDrugsEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugsEx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnDrugsEx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnDrugsEx.Image = CType(resources.GetObject("btnDrugsEx.Image"),System.Drawing.Image)
        Me.btnDrugsEx.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDrugsEx.Location = New System.Drawing.Point(330, 259)
        Me.btnDrugsEx.Name = "btnDrugsEx"
        Me.btnDrugsEx.Size = New System.Drawing.Size(24, 24)
        Me.btnDrugsEx.TabIndex = 0
        Me.btnDrugsEx.Tag = "UnSelected"
        Me.btnDrugsEx.Text = "      Drugs"
        Me.ToolTip1.SetToolTip(Me.btnDrugsEx, "Select Drugs")
        Me.btnDrugsEx.UseVisualStyleBackColor = false
        '
        'btnLabEx
        '
        Me.btnLabEx.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnLabEx.BackgroundImage = CType(resources.GetObject("btnLabEx.BackgroundImage"),System.Drawing.Image)
        Me.btnLabEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnLabEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabEx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnLabEx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnLabEx.Image = CType(resources.GetObject("btnLabEx.Image"),System.Drawing.Image)
        Me.btnLabEx.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLabEx.Location = New System.Drawing.Point(662, 259)
        Me.btnLabEx.Name = "btnLabEx"
        Me.btnLabEx.Size = New System.Drawing.Size(24, 24)
        Me.btnLabEx.TabIndex = 0
        Me.btnLabEx.Tag = "UnSelected"
        Me.btnLabEx.Text = "      Lab"
        Me.ToolTip1.SetToolTip(Me.btnLabEx, "Select Lab")
        Me.btnLabEx.UseVisualStyleBackColor = false
        '
        'btnICD9Ex
        '
        Me.btnICD9Ex.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnICD9Ex.BackgroundImage = CType(resources.GetObject("btnICD9Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnICD9Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD9Ex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnICD9Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD9Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD9Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD9Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnICD9Ex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnICD9Ex.Image = CType(resources.GetObject("btnICD9Ex.Image"),System.Drawing.Image)
        Me.btnICD9Ex.Location = New System.Drawing.Point(660, 10)
        Me.btnICD9Ex.Name = "btnICD9Ex"
        Me.btnICD9Ex.Size = New System.Drawing.Size(24, 24)
        Me.btnICD9Ex.TabIndex = 0
        Me.btnICD9Ex.Tag = "UnSelected"
        Me.btnICD9Ex.Text = "      ICD9"
        Me.ToolTip1.SetToolTip(Me.btnICD9Ex, "Select ICD9")
        Me.btnICD9Ex.UseVisualStyleBackColor = false
        '
        'btnCPTEx
        '
        Me.btnCPTEx.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnCPTEx.BackgroundImage = CType(resources.GetObject("btnCPTEx.BackgroundImage"),System.Drawing.Image)
        Me.btnCPTEx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCPTEx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnCPTEx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCPTEx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCPTEx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPTEx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnCPTEx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnCPTEx.Image = CType(resources.GetObject("btnCPTEx.Image"),System.Drawing.Image)
        Me.btnCPTEx.Location = New System.Drawing.Point(328, 134)
        Me.btnCPTEx.Name = "btnCPTEx"
        Me.btnCPTEx.Size = New System.Drawing.Size(24, 24)
        Me.btnCPTEx.TabIndex = 0
        Me.btnCPTEx.Tag = "UnSelected"
        Me.btnCPTEx.Text = "      CPT"
        Me.ToolTip1.SetToolTip(Me.btnCPTEx, "Select CPT")
        Me.btnCPTEx.UseVisualStyleBackColor = false
        '
        'btnproblemlist_Ex
        '
        Me.btnproblemlist_Ex.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnproblemlist_Ex.BackgroundImage = CType(resources.GetObject("btnproblemlist_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnproblemlist_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnproblemlist_Ex.FlatAppearance.BorderSize = 0
        Me.btnproblemlist_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnproblemlist_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnproblemlist_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnproblemlist_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnproblemlist_Ex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnproblemlist_Ex.Image = CType(resources.GetObject("btnproblemlist_Ex.Image"),System.Drawing.Image)
        Me.btnproblemlist_Ex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnproblemlist_Ex.Location = New System.Drawing.Point(1028, 366)
        Me.btnproblemlist_Ex.Name = "btnproblemlist_Ex"
        Me.btnproblemlist_Ex.Size = New System.Drawing.Size(45, 25)
        Me.btnproblemlist_Ex.TabIndex = 0
        Me.btnproblemlist_Ex.Tag = "UnSelected"
        Me.btnproblemlist_Ex.Text = "      Problem List"
        Me.btnproblemlist_Ex.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnproblemlist_Ex.UseVisualStyleBackColor = false
        '
        'btnDemographics_Ex
        '
        Me.btnDemographics_Ex.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.btnDemographics_Ex.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnDemographics_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDemographics_Ex.FlatAppearance.BorderSize = 0
        Me.btnDemographics_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnDemographics_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnDemographics_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDemographics_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnDemographics_Ex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.btnDemographics_Ex.Image = CType(resources.GetObject("btnDemographics_Ex.Image"),System.Drawing.Image)
        Me.btnDemographics_Ex.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDemographics_Ex.Location = New System.Drawing.Point(1028, 331)
        Me.btnDemographics_Ex.Name = "btnDemographics_Ex"
        Me.btnDemographics_Ex.Size = New System.Drawing.Size(45, 25)
        Me.btnDemographics_Ex.TabIndex = 0
        Me.btnDemographics_Ex.Tag = "UnSelected"
        Me.btnDemographics_Ex.Text = "      Demographics and Vitals"
        Me.btnDemographics_Ex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDemographics_Ex.UseVisualStyleBackColor = false
        '
        'Panel46
        '
        Me.Panel46.Controls.Add(Me.Panel47)
        Me.Panel46.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel46.Location = New System.Drawing.Point(0, 269)
        Me.Panel46.Name = "Panel46"
        Me.Panel46.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel46.Size = New System.Drawing.Size(1077, 28)
        Me.Panel46.TabIndex = 47
        '
        'Panel47
        '
        Me.Panel47.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel47.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel47.Controls.Add(Me.Label59)
        Me.Panel47.Controls.Add(Me.Label60)
        Me.Panel47.Controls.Add(Me.Label61)
        Me.Panel47.Controls.Add(Me.Label162)
        Me.Panel47.Controls.Add(Me.Label163)
        Me.Panel47.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel47.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel47.Location = New System.Drawing.Point(0, 0)
        Me.Panel47.Name = "Panel47"
        Me.Panel47.Size = New System.Drawing.Size(1077, 25)
        Me.Panel47.TabIndex = 44
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label59.Location = New System.Drawing.Point(1, 24)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(1075, 1)
        Me.Label59.TabIndex = 13
        Me.Label59.Text = "label2"
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label60.Location = New System.Drawing.Point(0, 1)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(1, 24)
        Me.Label60.TabIndex = 12
        Me.Label60.Text = "label4"
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label61.Location = New System.Drawing.Point(1076, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(1, 24)
        Me.Label61.TabIndex = 11
        Me.Label61.Text = "label3"
        '
        'Label162
        '
        Me.Label162.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label162.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label162.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label162.Location = New System.Drawing.Point(0, 0)
        Me.Label162.Name = "Label162"
        Me.Label162.Size = New System.Drawing.Size(1077, 1)
        Me.Label162.TabIndex = 10
        Me.Label162.Text = "label1"
        '
        'Label163
        '
        Me.Label163.BackColor = System.Drawing.Color.Transparent
        Me.Label163.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label163.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label163.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label163.ForeColor = System.Drawing.Color.White
        Me.Label163.Image = CType(resources.GetObject("Label163.Image"),System.Drawing.Image)
        Me.Label163.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label163.Location = New System.Drawing.Point(0, 0)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(1077, 25)
        Me.Label163.TabIndex = 9
        Me.Label163.Text = "      Other Exceptions"
        Me.Label163.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlVitals_Ex
        '
        Me.pnlVitals_Ex.BackColor = System.Drawing.Color.Transparent
        Me.pnlVitals_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlVitals_Ex.Controls.Add(Me.Label93)
        Me.pnlVitals_Ex.Controls.Add(Me.Label54)
        Me.pnlVitals_Ex.Controls.Add(Me.Label92)
        Me.pnlVitals_Ex.Controls.Add(Me.Label58)
        Me.pnlVitals_Ex.Controls.Add(Me.Label91)
        Me.pnlVitals_Ex.Controls.Add(Me.Label88)
        Me.pnlVitals_Ex.Controls.Add(Me.Label64)
        Me.pnlVitals_Ex.Controls.Add(Me.Label87)
        Me.pnlVitals_Ex.Controls.Add(Me.Label53)
        Me.pnlVitals_Ex.Controls.Add(Me.Label49)
        Me.pnlVitals_Ex.Controls.Add(Me.Label50)
        Me.pnlVitals_Ex.Controls.Add(Me.Label48)
        Me.pnlVitals_Ex.Controls.Add(Me.Label164)
        Me.pnlVitals_Ex.Controls.Add(Me.Label174)
        Me.pnlVitals_Ex.Controls.Add(Me.Label175)
        Me.pnlVitals_Ex.Controls.Add(Me.Label176)
        Me.pnlVitals_Ex.Controls.Add(Me.Label177)
        Me.pnlVitals_Ex.Controls.Add(Me.Label178)
        Me.pnlVitals_Ex.Controls.Add(Me.Label244)
        Me.pnlVitals_Ex.Controls.Add(Me.Label245)
        Me.pnlVitals_Ex.Controls.Add(Me.Label263)
        Me.pnlVitals_Ex.Controls.Add(Me.Label264)
        Me.pnlVitals_Ex.Controls.Add(Me.Label265)
        Me.pnlVitals_Ex.Controls.Add(Me.Label266)
        Me.pnlVitals_Ex.Controls.Add(Me.Label267)
        Me.pnlVitals_Ex.Controls.Add(Me.Label268)
        Me.pnlVitals_Ex.Controls.Add(Me.Label269)
        Me.pnlVitals_Ex.Controls.Add(Me.Label270)
        Me.pnlVitals_Ex.Controls.Add(Me.Label271)
        Me.pnlVitals_Ex.Controls.Add(Me.txtHeightMin_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label272)
        Me.pnlVitals_Ex.Controls.Add(Me.txtHeightMaxInch_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label273)
        Me.pnlVitals_Ex.Controls.Add(Me.Label274)
        Me.pnlVitals_Ex.Controls.Add(Me.txtHeightMinInch_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label275)
        Me.pnlVitals_Ex.Controls.Add(Me.txtPulseOXmax_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label276)
        Me.pnlVitals_Ex.Controls.Add(Me.txtPulseOXmin_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label277)
        Me.pnlVitals_Ex.Controls.Add(Me.txtPulseMax_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label278)
        Me.pnlVitals_Ex.Controls.Add(Me.txtPulseMin_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label279)
        Me.pnlVitals_Ex.Controls.Add(Me.txtTemperatureMax_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label280)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBMImax_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label281)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBMImin_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label282)
        Me.pnlVitals_Ex.Controls.Add(Me.txtWeightMax_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label283)
        Me.pnlVitals_Ex.Controls.Add(Me.txtHeightMax_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBPstandingMax_Ex_To)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBPstandingMin_Ex_To)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBPstandingMax_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBPstandingMin_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBPsettingMax_Ex_To)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBPsettingMin_Ex_To)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBPsettingMax_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.txtBPsettingMin_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label284)
        Me.pnlVitals_Ex.Controls.Add(Me.Label285)
        Me.pnlVitals_Ex.Controls.Add(Me.Label286)
        Me.pnlVitals_Ex.Controls.Add(Me.Label287)
        Me.pnlVitals_Ex.Controls.Add(Me.txtTemperatureMin_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.txtWeightMin_Ex)
        Me.pnlVitals_Ex.Controls.Add(Me.Label288)
        Me.pnlVitals_Ex.Controls.Add(Me.Label289)
        Me.pnlVitals_Ex.Controls.Add(Me.Label290)
        Me.pnlVitals_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlVitals_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlVitals_Ex.Location = New System.Drawing.Point(0, 144)
        Me.pnlVitals_Ex.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlVitals_Ex.Name = "pnlVitals_Ex"
        Me.pnlVitals_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlVitals_Ex.Size = New System.Drawing.Size(1077, 125)
        Me.pnlVitals_Ex.TabIndex = 1
        '
        'Label93
        '
        Me.Label93.AutoSize = true
        Me.Label93.BackColor = System.Drawing.Color.Transparent
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label93.Location = New System.Drawing.Point(626, 106)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(43, 11)
        Me.Label93.TabIndex = 55
        Me.Label93.Text = "(Systolic)"
        Me.Label93.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label54
        '
        Me.Label54.AutoSize = true
        Me.Label54.BackColor = System.Drawing.Color.Transparent
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label54.Location = New System.Drawing.Point(234, 106)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(43, 11)
        Me.Label54.TabIndex = 55
        Me.Label54.Text = "(Systolic)"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label92
        '
        Me.Label92.AutoSize = true
        Me.Label92.BackColor = System.Drawing.Color.Transparent
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label92.Location = New System.Drawing.Point(523, 106)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(43, 11)
        Me.Label92.TabIndex = 56
        Me.Label92.Text = "(Systolic)"
        Me.Label92.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label58
        '
        Me.Label58.AutoSize = true
        Me.Label58.BackColor = System.Drawing.Color.Transparent
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label58.Location = New System.Drawing.Point(131, 106)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(43, 11)
        Me.Label58.TabIndex = 56
        Me.Label58.Text = "(Systolic)"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label91
        '
        Me.Label91.AutoSize = true
        Me.Label91.BackColor = System.Drawing.Color.Transparent
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label91.Location = New System.Drawing.Point(675, 106)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(45, 11)
        Me.Label91.TabIndex = 57
        Me.Label91.Text = "(Diastolic)"
        Me.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label88
        '
        Me.Label88.AutoSize = true
        Me.Label88.BackColor = System.Drawing.Color.Transparent
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label88.Location = New System.Drawing.Point(572, 106)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(45, 11)
        Me.Label88.TabIndex = 58
        Me.Label88.Text = "(Diastolic)"
        Me.Label88.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label64
        '
        Me.Label64.AutoSize = true
        Me.Label64.BackColor = System.Drawing.Color.Transparent
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label64.Location = New System.Drawing.Point(282, 106)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(45, 11)
        Me.Label64.TabIndex = 57
        Me.Label64.Text = "(Diastolic)"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label87
        '
        Me.Label87.AutoSize = true
        Me.Label87.BackColor = System.Drawing.Color.Transparent
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label87.Location = New System.Drawing.Point(180, 106)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(45, 11)
        Me.Label87.TabIndex = 58
        Me.Label87.Text = "(Diastolic)"
        Me.Label87.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label53
        '
        Me.Label53.AutoSize = true
        Me.Label53.BackColor = System.Drawing.Color.Transparent
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label53.Location = New System.Drawing.Point(666, 85)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(12, 14)
        Me.Label53.TabIndex = 54
        Me.Label53.Text = "/"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label49
        '
        Me.Label49.AutoSize = true
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label49.Location = New System.Drawing.Point(563, 85)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(12, 14)
        Me.Label49.TabIndex = 54
        Me.Label49.Text = "/"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label50
        '
        Me.Label50.AutoSize = true
        Me.Label50.BackColor = System.Drawing.Color.Transparent
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label50.Location = New System.Drawing.Point(274, 85)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(12, 14)
        Me.Label50.TabIndex = 53
        Me.Label50.Text = "/"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label48
        '
        Me.Label48.AutoSize = true
        Me.Label48.BackColor = System.Drawing.Color.Transparent
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label48.Location = New System.Drawing.Point(171, 85)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(12, 14)
        Me.Label48.TabIndex = 53
        Me.Label48.Text = "/"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label164
        '
        Me.Label164.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label164.AutoSize = true
        Me.Label164.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label164.ForeColor = System.Drawing.Color.Red
        Me.Label164.Location = New System.Drawing.Point(627, 5)
        Me.Label164.Name = "Label164"
        Me.Label164.Size = New System.Drawing.Size(20, 13)
        Me.Label164.TabIndex = 51
        Me.Label164.Text = "lbs"
        '
        'Label174
        '
        Me.Label174.AutoSize = true
        Me.Label174.BackColor = System.Drawing.Color.Transparent
        Me.Label174.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label174.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label174.Location = New System.Drawing.Point(828, 23)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(57, 14)
        Me.Label174.TabIndex = 50
        Me.Label174.Text = "between"
        Me.Label174.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label175
        '
        Me.Label175.AutoSize = true
        Me.Label175.BackColor = System.Drawing.Color.Transparent
        Me.Label175.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label175.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label175.Location = New System.Drawing.Point(467, 85)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(57, 14)
        Me.Label175.TabIndex = 50
        Me.Label175.Text = "between"
        Me.Label175.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label176
        '
        Me.Label176.AutoSize = true
        Me.Label176.BackColor = System.Drawing.Color.Transparent
        Me.Label176.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label176.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label176.Location = New System.Drawing.Point(466, 54)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(57, 14)
        Me.Label176.TabIndex = 50
        Me.Label176.Text = "between"
        Me.Label176.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label177
        '
        Me.Label177.AutoSize = true
        Me.Label177.BackColor = System.Drawing.Color.Transparent
        Me.Label177.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label177.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label177.Location = New System.Drawing.Point(469, 23)
        Me.Label177.Name = "Label177"
        Me.Label177.Size = New System.Drawing.Size(57, 14)
        Me.Label177.TabIndex = 50
        Me.Label177.Text = "between"
        Me.Label177.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label178
        '
        Me.Label178.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label178.AutoSize = true
        Me.Label178.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label178.ForeColor = System.Drawing.Color.Red
        Me.Label178.Location = New System.Drawing.Point(540, 5)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(20, 13)
        Me.Label178.TabIndex = 49
        Me.Label178.Text = "lbs"
        '
        'Label244
        '
        Me.Label244.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label244.AutoSize = true
        Me.Label244.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label244.ForeColor = System.Drawing.Color.Red
        Me.Label244.Location = New System.Drawing.Point(223, 5)
        Me.Label244.Name = "Label244"
        Me.Label244.Size = New System.Drawing.Size(61, 13)
        Me.Label244.TabIndex = 48
        Me.Label244.Text = " ft'      inch''"
        '
        'Label245
        '
        Me.Label245.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label245.AutoSize = true
        Me.Label245.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label245.ForeColor = System.Drawing.Color.Red
        Me.Label245.Location = New System.Drawing.Point(135, 5)
        Me.Label245.Name = "Label245"
        Me.Label245.Size = New System.Drawing.Size(61, 13)
        Me.Label245.TabIndex = 32
        Me.Label245.Text = " ft'      inch''"
        '
        'Label263
        '
        Me.Label263.AutoSize = true
        Me.Label263.BackColor = System.Drawing.Color.Transparent
        Me.Label263.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label263.Location = New System.Drawing.Point(203, 23)
        Me.Label263.Name = "Label263"
        Me.Label263.Size = New System.Drawing.Size(19, 14)
        Me.Label263.TabIndex = 32
        Me.Label263.Text = "to"
        Me.Label263.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label264
        '
        Me.Label264.AutoSize = true
        Me.Label264.BackColor = System.Drawing.Color.Transparent
        Me.Label264.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label264.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label264.Location = New System.Drawing.Point(828, 54)
        Me.Label264.Name = "Label264"
        Me.Label264.Size = New System.Drawing.Size(57, 14)
        Me.Label264.TabIndex = 32
        Me.Label264.Text = "between"
        Me.Label264.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label265
        '
        Me.Label265.AutoSize = true
        Me.Label265.BackColor = System.Drawing.Color.Transparent
        Me.Label265.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label265.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label265.Location = New System.Drawing.Point(79, 85)
        Me.Label265.Name = "Label265"
        Me.Label265.Size = New System.Drawing.Size(57, 14)
        Me.Label265.TabIndex = 32
        Me.Label265.Text = "between"
        Me.Label265.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label266
        '
        Me.Label266.AutoSize = true
        Me.Label266.BackColor = System.Drawing.Color.Transparent
        Me.Label266.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label266.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label266.Location = New System.Drawing.Point(79, 54)
        Me.Label266.Name = "Label266"
        Me.Label266.Size = New System.Drawing.Size(57, 14)
        Me.Label266.TabIndex = 32
        Me.Label266.Text = "between"
        Me.Label266.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label267
        '
        Me.Label267.AutoSize = true
        Me.Label267.BackColor = System.Drawing.Color.Transparent
        Me.Label267.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label267.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label267.Location = New System.Drawing.Point(79, 23)
        Me.Label267.Name = "Label267"
        Me.Label267.Size = New System.Drawing.Size(57, 14)
        Me.Label267.TabIndex = 32
        Me.Label267.Text = "between"
        Me.Label267.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label268
        '
        Me.Label268.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label268.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label268.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label268.Location = New System.Drawing.Point(1, 121)
        Me.Label268.Name = "Label268"
        Me.Label268.Size = New System.Drawing.Size(1075, 1)
        Me.Label268.TabIndex = 47
        Me.Label268.Text = "label2"
        '
        'Label269
        '
        Me.Label269.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label269.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label269.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label269.Location = New System.Drawing.Point(0, 1)
        Me.Label269.Name = "Label269"
        Me.Label269.Size = New System.Drawing.Size(1, 121)
        Me.Label269.TabIndex = 46
        Me.Label269.Text = "label4"
        '
        'Label270
        '
        Me.Label270.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label270.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label270.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label270.Location = New System.Drawing.Point(1076, 1)
        Me.Label270.Name = "Label270"
        Me.Label270.Size = New System.Drawing.Size(1, 121)
        Me.Label270.TabIndex = 45
        Me.Label270.Text = "label3"
        '
        'Label271
        '
        Me.Label271.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label271.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label271.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label271.Location = New System.Drawing.Point(0, 0)
        Me.Label271.Name = "Label271"
        Me.Label271.Size = New System.Drawing.Size(1077, 1)
        Me.Label271.TabIndex = 44
        Me.Label271.Text = "label1"
        '
        'txtHeightMin_Ex
        '
        Me.txtHeightMin_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtHeightMin_Ex.Location = New System.Drawing.Point(136, 19)
        Me.txtHeightMin_Ex.MaxLength = 1
        Me.txtHeightMin_Ex.Name = "txtHeightMin_Ex"
        Me.txtHeightMin_Ex.Size = New System.Drawing.Size(16, 22)
        Me.txtHeightMin_Ex.TabIndex = 0
        Me.txtHeightMin_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label272
        '
        Me.Label272.AutoSize = true
        Me.Label272.BackColor = System.Drawing.Color.Transparent
        Me.Label272.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label272.Location = New System.Drawing.Point(283, 23)
        Me.Label272.Name = "Label272"
        Me.Label272.Size = New System.Drawing.Size(12, 14)
        Me.Label272.TabIndex = 43
        Me.Label272.Text = """"
        Me.Label272.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMaxInch_Ex
        '
        Me.txtHeightMaxInch_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtHeightMaxInch_Ex.Location = New System.Drawing.Point(251, 19)
        Me.txtHeightMaxInch_Ex.MaxLength = 2
        Me.txtHeightMaxInch_Ex.Name = "txtHeightMaxInch_Ex"
        Me.txtHeightMaxInch_Ex.Size = New System.Drawing.Size(32, 22)
        Me.txtHeightMaxInch_Ex.TabIndex = 3
        Me.txtHeightMaxInch_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label273
        '
        Me.Label273.AutoSize = true
        Me.Label273.BackColor = System.Drawing.Color.Transparent
        Me.Label273.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label273.Location = New System.Drawing.Point(241, 23)
        Me.Label273.Name = "Label273"
        Me.Label273.Size = New System.Drawing.Size(10, 14)
        Me.Label273.TabIndex = 41
        Me.Label273.Text = "'"
        Me.Label273.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label274
        '
        Me.Label274.AutoSize = true
        Me.Label274.BackColor = System.Drawing.Color.Transparent
        Me.Label274.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label274.Location = New System.Drawing.Point(192, 23)
        Me.Label274.Name = "Label274"
        Me.Label274.Size = New System.Drawing.Size(12, 14)
        Me.Label274.TabIndex = 2
        Me.Label274.Text = """"
        Me.Label274.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMinInch_Ex
        '
        Me.txtHeightMinInch_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtHeightMinInch_Ex.Location = New System.Drawing.Point(162, 19)
        Me.txtHeightMinInch_Ex.MaxLength = 2
        Me.txtHeightMinInch_Ex.Name = "txtHeightMinInch_Ex"
        Me.txtHeightMinInch_Ex.Size = New System.Drawing.Size(32, 22)
        Me.txtHeightMinInch_Ex.TabIndex = 1
        Me.txtHeightMinInch_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label275
        '
        Me.Label275.AutoSize = true
        Me.Label275.BackColor = System.Drawing.Color.Transparent
        Me.Label275.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label275.Location = New System.Drawing.Point(152, 22)
        Me.Label275.Name = "Label275"
        Me.Label275.Size = New System.Drawing.Size(10, 14)
        Me.Label275.TabIndex = 38
        Me.Label275.Text = "'"
        Me.Label275.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseOXmax_Ex
        '
        Me.txtPulseOXmax_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPulseOXmax_Ex.Location = New System.Drawing.Point(610, 50)
        Me.txtPulseOXmax_Ex.MaxLength = 3
        Me.txtPulseOXmax_Ex.Name = "txtPulseOXmax_Ex"
        Me.txtPulseOXmax_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseOXmax_Ex.TabIndex = 9
        Me.txtPulseOXmax_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label276
        '
        Me.Label276.AutoSize = true
        Me.Label276.BackColor = System.Drawing.Color.Transparent
        Me.Label276.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label276.Location = New System.Drawing.Point(589, 54)
        Me.Label276.Margin = New System.Windows.Forms.Padding(0)
        Me.Label276.Name = "Label276"
        Me.Label276.Size = New System.Drawing.Size(19, 14)
        Me.Label276.TabIndex = 37
        Me.Label276.Text = "to"
        Me.Label276.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseOXmin_Ex
        '
        Me.txtPulseOXmin_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPulseOXmin_Ex.Location = New System.Drawing.Point(528, 50)
        Me.txtPulseOXmin_Ex.MaxLength = 3
        Me.txtPulseOXmin_Ex.Name = "txtPulseOXmin_Ex"
        Me.txtPulseOXmin_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseOXmin_Ex.TabIndex = 8
        Me.txtPulseOXmin_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label277
        '
        Me.Label277.AutoSize = true
        Me.Label277.BackColor = System.Drawing.Color.Transparent
        Me.Label277.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label277.Location = New System.Drawing.Point(403, 54)
        Me.Label277.Name = "Label277"
        Me.Label277.Size = New System.Drawing.Size(63, 14)
        Me.Label277.TabIndex = 35
        Me.Label277.Text = "Pulse OX :"
        Me.Label277.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseMax_Ex
        '
        Me.txtPulseMax_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPulseMax_Ex.Location = New System.Drawing.Point(225, 50)
        Me.txtPulseMax_Ex.MaxLength = 3
        Me.txtPulseMax_Ex.Name = "txtPulseMax_Ex"
        Me.txtPulseMax_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseMax_Ex.TabIndex = 7
        Me.txtPulseMax_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label278
        '
        Me.Label278.AutoSize = true
        Me.Label278.BackColor = System.Drawing.Color.Transparent
        Me.Label278.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label278.Location = New System.Drawing.Point(201, 54)
        Me.Label278.Name = "Label278"
        Me.Label278.Size = New System.Drawing.Size(19, 14)
        Me.Label278.TabIndex = 33
        Me.Label278.Text = "to"
        Me.Label278.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPulseMin_Ex
        '
        Me.txtPulseMin_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtPulseMin_Ex.Location = New System.Drawing.Point(136, 50)
        Me.txtPulseMin_Ex.MaxLength = 3
        Me.txtPulseMin_Ex.Name = "txtPulseMin_Ex"
        Me.txtPulseMin_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtPulseMin_Ex.TabIndex = 6
        Me.txtPulseMin_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label279
        '
        Me.Label279.AutoSize = true
        Me.Label279.BackColor = System.Drawing.Color.Transparent
        Me.Label279.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label279.Location = New System.Drawing.Point(36, 54)
        Me.Label279.Name = "Label279"
        Me.Label279.Size = New System.Drawing.Size(43, 14)
        Me.Label279.TabIndex = 31
        Me.Label279.Text = "Pulse :"
        Me.Label279.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTemperatureMax_Ex
        '
        Me.txtTemperatureMax_Ex.AcceptsTab = true
        Me.txtTemperatureMax_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTemperatureMax_Ex.Location = New System.Drawing.Point(969, 19)
        Me.txtTemperatureMax_Ex.Name = "txtTemperatureMax_Ex"
        Me.txtTemperatureMax_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtTemperatureMax_Ex.TabIndex = 17
        Me.txtTemperatureMax_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label280
        '
        Me.Label280.AutoSize = true
        Me.Label280.BackColor = System.Drawing.Color.Transparent
        Me.Label280.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label280.Location = New System.Drawing.Point(948, 23)
        Me.Label280.Margin = New System.Windows.Forms.Padding(0)
        Me.Label280.Name = "Label280"
        Me.Label280.Size = New System.Drawing.Size(19, 14)
        Me.Label280.TabIndex = 29
        Me.Label280.Text = "to"
        Me.Label280.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMImax_Ex
        '
        Me.txtBMImax_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBMImax_Ex.Location = New System.Drawing.Point(969, 50)
        Me.txtBMImax_Ex.MaxLength = 5
        Me.txtBMImax_Ex.Name = "txtBMImax_Ex"
        Me.txtBMImax_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtBMImax_Ex.TabIndex = 15
        Me.txtBMImax_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label281
        '
        Me.Label281.AutoSize = true
        Me.Label281.BackColor = System.Drawing.Color.Transparent
        Me.Label281.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label281.Location = New System.Drawing.Point(948, 54)
        Me.Label281.Name = "Label281"
        Me.Label281.Size = New System.Drawing.Size(19, 14)
        Me.Label281.TabIndex = 27
        Me.Label281.Text = "to"
        Me.Label281.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMImin_Ex
        '
        Me.txtBMImin_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBMImin_Ex.Location = New System.Drawing.Point(887, 50)
        Me.txtBMImin_Ex.MaxLength = 5
        Me.txtBMImin_Ex.Name = "txtBMImin_Ex"
        Me.txtBMImin_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtBMImin_Ex.TabIndex = 14
        Me.txtBMImin_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label282
        '
        Me.Label282.AutoSize = true
        Me.Label282.BackColor = System.Drawing.Color.Transparent
        Me.Label282.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label282.Location = New System.Drawing.Point(791, 54)
        Me.Label282.Name = "Label282"
        Me.Label282.Size = New System.Drawing.Size(35, 14)
        Me.Label282.TabIndex = 25
        Me.Label282.Text = "BMI :"
        Me.Label282.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtWeightMax_Ex
        '
        Me.txtWeightMax_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtWeightMax_Ex.Location = New System.Drawing.Point(610, 19)
        Me.txtWeightMax_Ex.MaxLength = 6
        Me.txtWeightMax_Ex.Name = "txtWeightMax_Ex"
        Me.txtWeightMax_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtWeightMax_Ex.TabIndex = 5
        Me.txtWeightMax_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label283
        '
        Me.Label283.AutoSize = true
        Me.Label283.BackColor = System.Drawing.Color.Transparent
        Me.Label283.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label283.Location = New System.Drawing.Point(589, 23)
        Me.Label283.Margin = New System.Windows.Forms.Padding(0)
        Me.Label283.Name = "Label283"
        Me.Label283.Size = New System.Drawing.Size(19, 14)
        Me.Label283.TabIndex = 23
        Me.Label283.Text = "to"
        Me.Label283.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHeightMax_Ex
        '
        Me.txtHeightMax_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtHeightMax_Ex.Location = New System.Drawing.Point(225, 19)
        Me.txtHeightMax_Ex.MaxLength = 1
        Me.txtHeightMax_Ex.Name = "txtHeightMax_Ex"
        Me.txtHeightMax_Ex.Size = New System.Drawing.Size(16, 22)
        Me.txtHeightMax_Ex.TabIndex = 2
        Me.txtHeightMax_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMax_Ex_To
        '
        Me.txtBPstandingMax_Ex_To.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPstandingMax_Ex_To.Location = New System.Drawing.Point(631, 81)
        Me.txtBPstandingMax_Ex_To.MaxLength = 3
        Me.txtBPstandingMax_Ex_To.Name = "txtBPstandingMax_Ex_To"
        Me.txtBPstandingMax_Ex_To.Size = New System.Drawing.Size(34, 22)
        Me.txtBPstandingMax_Ex_To.TabIndex = 12
        Me.txtBPstandingMax_Ex_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMin_Ex_To
        '
        Me.txtBPstandingMin_Ex_To.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPstandingMin_Ex_To.Location = New System.Drawing.Point(679, 81)
        Me.txtBPstandingMin_Ex_To.MaxLength = 3
        Me.txtBPstandingMin_Ex_To.Name = "txtBPstandingMin_Ex_To"
        Me.txtBPstandingMin_Ex_To.Size = New System.Drawing.Size(34, 22)
        Me.txtBPstandingMin_Ex_To.TabIndex = 13
        Me.txtBPstandingMin_Ex_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMax_Ex
        '
        Me.txtBPstandingMax_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPstandingMax_Ex.Location = New System.Drawing.Point(528, 81)
        Me.txtBPstandingMax_Ex.MaxLength = 3
        Me.txtBPstandingMax_Ex.Name = "txtBPstandingMax_Ex"
        Me.txtBPstandingMax_Ex.Size = New System.Drawing.Size(34, 22)
        Me.txtBPstandingMax_Ex.TabIndex = 12
        Me.txtBPstandingMax_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMin_Ex
        '
        Me.txtBPstandingMin_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPstandingMin_Ex.Location = New System.Drawing.Point(576, 81)
        Me.txtBPstandingMin_Ex.MaxLength = 3
        Me.txtBPstandingMin_Ex.Name = "txtBPstandingMin_Ex"
        Me.txtBPstandingMin_Ex.Size = New System.Drawing.Size(34, 22)
        Me.txtBPstandingMin_Ex.TabIndex = 13
        Me.txtBPstandingMin_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMax_Ex_To
        '
        Me.txtBPsettingMax_Ex_To.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPsettingMax_Ex_To.Location = New System.Drawing.Point(239, 81)
        Me.txtBPsettingMax_Ex_To.MaxLength = 3
        Me.txtBPsettingMax_Ex_To.Name = "txtBPsettingMax_Ex_To"
        Me.txtBPsettingMax_Ex_To.Size = New System.Drawing.Size(34, 22)
        Me.txtBPsettingMax_Ex_To.TabIndex = 10
        Me.txtBPsettingMax_Ex_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMin_Ex_To
        '
        Me.txtBPsettingMin_Ex_To.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPsettingMin_Ex_To.Location = New System.Drawing.Point(287, 81)
        Me.txtBPsettingMin_Ex_To.MaxLength = 3
        Me.txtBPsettingMin_Ex_To.Name = "txtBPsettingMin_Ex_To"
        Me.txtBPsettingMin_Ex_To.Size = New System.Drawing.Size(34, 22)
        Me.txtBPsettingMin_Ex_To.TabIndex = 11
        Me.txtBPsettingMin_Ex_To.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMax_Ex
        '
        Me.txtBPsettingMax_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPsettingMax_Ex.Location = New System.Drawing.Point(136, 81)
        Me.txtBPsettingMax_Ex.MaxLength = 3
        Me.txtBPsettingMax_Ex.Name = "txtBPsettingMax_Ex"
        Me.txtBPsettingMax_Ex.Size = New System.Drawing.Size(34, 22)
        Me.txtBPsettingMax_Ex.TabIndex = 10
        Me.txtBPsettingMax_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMin_Ex
        '
        Me.txtBPsettingMin_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBPsettingMin_Ex.Location = New System.Drawing.Point(184, 81)
        Me.txtBPsettingMin_Ex.MaxLength = 3
        Me.txtBPsettingMin_Ex.Name = "txtBPsettingMin_Ex"
        Me.txtBPsettingMin_Ex.Size = New System.Drawing.Size(34, 22)
        Me.txtBPsettingMin_Ex.TabIndex = 11
        Me.txtBPsettingMin_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label284
        '
        Me.Label284.AutoSize = true
        Me.Label284.BackColor = System.Drawing.Color.Transparent
        Me.Label284.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label284.Location = New System.Drawing.Point(386, 85)
        Me.Label284.Name = "Label284"
        Me.Label284.Size = New System.Drawing.Size(81, 14)
        Me.Label284.TabIndex = 16
        Me.Label284.Text = "BP-Standing :"
        Me.Label284.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label285
        '
        Me.Label285.AutoSize = true
        Me.Label285.BackColor = System.Drawing.Color.Transparent
        Me.Label285.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label285.Location = New System.Drawing.Point(611, 85)
        Me.Label285.Margin = New System.Windows.Forms.Padding(0)
        Me.Label285.Name = "Label285"
        Me.Label285.Size = New System.Drawing.Size(19, 14)
        Me.Label285.TabIndex = 15
        Me.Label285.Text = "to"
        Me.Label285.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label286
        '
        Me.Label286.AutoSize = true
        Me.Label286.BackColor = System.Drawing.Color.Transparent
        Me.Label286.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label286.Location = New System.Drawing.Point(11, 85)
        Me.Label286.Name = "Label286"
        Me.Label286.Size = New System.Drawing.Size(68, 14)
        Me.Label286.TabIndex = 14
        Me.Label286.Text = "BP-Sitting :"
        Me.Label286.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label287
        '
        Me.Label287.AutoSize = true
        Me.Label287.BackColor = System.Drawing.Color.Transparent
        Me.Label287.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label287.Location = New System.Drawing.Point(219, 85)
        Me.Label287.Name = "Label287"
        Me.Label287.Size = New System.Drawing.Size(19, 14)
        Me.Label287.TabIndex = 13
        Me.Label287.Text = "to"
        Me.Label287.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTemperatureMin_Ex
        '
        Me.txtTemperatureMin_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtTemperatureMin_Ex.Location = New System.Drawing.Point(887, 19)
        Me.txtTemperatureMin_Ex.Name = "txtTemperatureMin_Ex"
        Me.txtTemperatureMin_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtTemperatureMin_Ex.TabIndex = 16
        Me.txtTemperatureMin_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtWeightMin_Ex
        '
        Me.txtWeightMin_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtWeightMin_Ex.Location = New System.Drawing.Point(528, 19)
        Me.txtWeightMin_Ex.MaxLength = 6
        Me.txtWeightMin_Ex.Name = "txtWeightMin_Ex"
        Me.txtWeightMin_Ex.Size = New System.Drawing.Size(59, 22)
        Me.txtWeightMin_Ex.TabIndex = 4
        Me.txtWeightMin_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label288
        '
        Me.Label288.AutoSize = true
        Me.Label288.BackColor = System.Drawing.Color.Transparent
        Me.Label288.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label288.Location = New System.Drawing.Point(412, 23)
        Me.Label288.Name = "Label288"
        Me.Label288.Size = New System.Drawing.Size(55, 14)
        Me.Label288.TabIndex = 3
        Me.Label288.Text = "Weight :"
        Me.Label288.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label289
        '
        Me.Label289.AutoSize = true
        Me.Label289.BackColor = System.Drawing.Color.Transparent
        Me.Label289.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label289.Location = New System.Drawing.Point(723, 23)
        Me.Label289.Name = "Label289"
        Me.Label289.Size = New System.Drawing.Size(103, 14)
        Me.Label289.TabIndex = 4
        Me.Label289.Text = "Temperature(F) :"
        Me.Label289.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label290
        '
        Me.Label290.AutoSize = true
        Me.Label290.BackColor = System.Drawing.Color.Transparent
        Me.Label290.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label290.Location = New System.Drawing.Point(28, 23)
        Me.Label290.Name = "Label290"
        Me.Label290.Size = New System.Drawing.Size(51, 14)
        Me.Label290.TabIndex = 1
        Me.Label290.Text = "Height :"
        Me.Label290.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel49
        '
        Me.Panel49.Controls.Add(Me.Panel50)
        Me.Panel49.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel49.Location = New System.Drawing.Point(0, 116)
        Me.Panel49.Name = "Panel49"
        Me.Panel49.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel49.Size = New System.Drawing.Size(1077, 28)
        Me.Panel49.TabIndex = 46
        '
        'Panel50
        '
        Me.Panel50.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel50.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel50.Controls.Add(Me.Label291)
        Me.Panel50.Controls.Add(Me.Label292)
        Me.Panel50.Controls.Add(Me.Label293)
        Me.Panel50.Controls.Add(Me.Label294)
        Me.Panel50.Controls.Add(Me.Label295)
        Me.Panel50.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel50.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel50.Location = New System.Drawing.Point(0, 0)
        Me.Panel50.Name = "Panel50"
        Me.Panel50.Size = New System.Drawing.Size(1077, 25)
        Me.Panel50.TabIndex = 44
        '
        'Label291
        '
        Me.Label291.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label291.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label291.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label291.Location = New System.Drawing.Point(1, 24)
        Me.Label291.Name = "Label291"
        Me.Label291.Size = New System.Drawing.Size(1075, 1)
        Me.Label291.TabIndex = 13
        Me.Label291.Text = "label2"
        '
        'Label292
        '
        Me.Label292.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label292.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label292.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label292.Location = New System.Drawing.Point(0, 1)
        Me.Label292.Name = "Label292"
        Me.Label292.Size = New System.Drawing.Size(1, 24)
        Me.Label292.TabIndex = 12
        Me.Label292.Text = "label4"
        '
        'Label293
        '
        Me.Label293.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label293.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label293.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label293.Location = New System.Drawing.Point(1076, 1)
        Me.Label293.Name = "Label293"
        Me.Label293.Size = New System.Drawing.Size(1, 24)
        Me.Label293.TabIndex = 11
        Me.Label293.Text = "label3"
        '
        'Label294
        '
        Me.Label294.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label294.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label294.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label294.Location = New System.Drawing.Point(0, 0)
        Me.Label294.Name = "Label294"
        Me.Label294.Size = New System.Drawing.Size(1077, 1)
        Me.Label294.TabIndex = 10
        Me.Label294.Text = "label1"
        '
        'Label295
        '
        Me.Label295.BackColor = System.Drawing.Color.Transparent
        Me.Label295.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label295.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label295.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label295.ForeColor = System.Drawing.Color.White
        Me.Label295.Image = CType(resources.GetObject("Label295.Image"),System.Drawing.Image)
        Me.Label295.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label295.Location = New System.Drawing.Point(0, 0)
        Me.Label295.Name = "Label295"
        Me.Label295.Size = New System.Drawing.Size(1077, 25)
        Me.Label295.TabIndex = 9
        Me.Label295.Text = "      Vitals Exceptions"
        Me.Label295.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDemographics_Ex
        '
        Me.pnlDemographics_Ex.BackColor = System.Drawing.Color.Transparent
        Me.pnlDemographics_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDemographics_Ex.Controls.Add(Me.cmbChkBoxMaritalSt_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.cmbGender_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.cmbChkBoxRace_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.txtCity_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label296)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label297)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label298)
        Me.pnlDemographics_Ex.Controls.Add(Me.cmbState_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label299)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label300)
        Me.pnlDemographics_Ex.Controls.Add(Me.txtZip_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label301)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label302)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label303)
        Me.pnlDemographics_Ex.Controls.Add(Me.cmbEmpStatus_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.cmbAgeMaxMnth_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label304)
        Me.pnlDemographics_Ex.Controls.Add(Me.cmbAgeMinMnth_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label305)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label306)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label307)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label308)
        Me.pnlDemographics_Ex.Controls.Add(Me.cmbAgeMax_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.cmbAgeMin_Ex)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label309)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label310)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label311)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label312)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label313)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label314)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label315)
        Me.pnlDemographics_Ex.Controls.Add(Me.Label316)
        Me.pnlDemographics_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDemographics_Ex.ForeColor = System.Drawing.Color.Black
        Me.pnlDemographics_Ex.Location = New System.Drawing.Point(0, 28)
        Me.pnlDemographics_Ex.Name = "pnlDemographics_Ex"
        Me.pnlDemographics_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlDemographics_Ex.Size = New System.Drawing.Size(1077, 88)
        Me.pnlDemographics_Ex.TabIndex = 0
        '
        'cmbChkBoxMaritalSt_Ex
        '
        CheckBoxProperties3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbChkBoxMaritalSt_Ex.CheckBoxProperties = CheckBoxProperties3
        Me.cmbChkBoxMaritalSt_Ex.DisplayMemberSingleItem = ""
        Me.cmbChkBoxMaritalSt_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChkBoxMaritalSt_Ex.FormattingEnabled = true
        Me.cmbChkBoxMaritalSt_Ex.Location = New System.Drawing.Point(478, 52)
        Me.cmbChkBoxMaritalSt_Ex.Name = "cmbChkBoxMaritalSt_Ex"
        Me.cmbChkBoxMaritalSt_Ex.Size = New System.Drawing.Size(138, 22)
        Me.cmbChkBoxMaritalSt_Ex.Sorted = true
        Me.cmbChkBoxMaritalSt_Ex.TabIndex = 77
        Me.cmbChkBoxMaritalSt_Ex.Tag = "MaritialStatus"
        '
        'cmbGender_Ex
        '
        CheckBoxProperties4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbGender_Ex.CheckBoxProperties = CheckBoxProperties4
        Me.cmbGender_Ex.DisplayMemberSingleItem = ""
        Me.cmbGender_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender_Ex.FormattingEnabled = true
        Me.cmbGender_Ex.Location = New System.Drawing.Point(481, 25)
        Me.cmbGender_Ex.Name = "cmbGender_Ex"
        Me.cmbGender_Ex.Size = New System.Drawing.Size(133, 22)
        Me.cmbGender_Ex.Sorted = true
        Me.cmbGender_Ex.TabIndex = 76
        Me.cmbGender_Ex.Tag = "Gender"
        '
        'cmbChkBoxRace_Ex
        '
        CheckBoxProperties5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbChkBoxRace_Ex.CheckBoxProperties = CheckBoxProperties5
        Me.cmbChkBoxRace_Ex.DisplayMemberSingleItem = ""
        Me.cmbChkBoxRace_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChkBoxRace_Ex.FormattingEnabled = true
        Me.cmbChkBoxRace_Ex.Location = New System.Drawing.Point(70, 52)
        Me.cmbChkBoxRace_Ex.Name = "cmbChkBoxRace_Ex"
        Me.cmbChkBoxRace_Ex.Size = New System.Drawing.Size(278, 22)
        Me.cmbChkBoxRace_Ex.Sorted = true
        Me.cmbChkBoxRace_Ex.TabIndex = 75
        Me.cmbChkBoxRace_Ex.Tag = "Race"
        '
        'txtCity_Ex
        '
        Me.txtCity_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCity_Ex.ForeColor = System.Drawing.Color.Black
        Me.txtCity_Ex.Location = New System.Drawing.Point(696, 25)
        Me.txtCity_Ex.MaxLength = 50
        Me.txtCity_Ex.Name = "txtCity_Ex"
        Me.txtCity_Ex.Size = New System.Drawing.Size(94, 22)
        Me.txtCity_Ex.TabIndex = 2
        '
        'Label296
        '
        Me.Label296.AutoSize = true
        Me.Label296.BackColor = System.Drawing.Color.Transparent
        Me.Label296.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label296.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label296.Location = New System.Drawing.Point(660, 29)
        Me.Label296.Name = "Label296"
        Me.Label296.Size = New System.Drawing.Size(35, 14)
        Me.Label296.TabIndex = 10
        Me.Label296.Text = "City :"
        Me.Label296.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label297
        '
        Me.Label297.AutoSize = true
        Me.Label297.BackColor = System.Drawing.Color.Transparent
        Me.Label297.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label297.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label297.Location = New System.Drawing.Point(52, 56)
        Me.Label297.Name = "Label297"
        Me.Label297.Size = New System.Drawing.Size(16, 14)
        Me.Label297.TabIndex = 30
        Me.Label297.Text = "in"
        Me.Label297.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label298
        '
        Me.Label298.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label298.AutoSize = true
        Me.Label298.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label298.ForeColor = System.Drawing.Color.Red
        Me.Label298.Location = New System.Drawing.Point(309, 9)
        Me.Label298.Name = "Label298"
        Me.Label298.Size = New System.Drawing.Size(21, 13)
        Me.Label298.TabIndex = 28
        Me.Label298.Text = "mn"
        '
        'cmbState_Ex
        '
        Me.cmbState_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbState_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbState_Ex.ForeColor = System.Drawing.Color.Black
        Me.cmbState_Ex.Location = New System.Drawing.Point(696, 52)
        Me.cmbState_Ex.Name = "cmbState_Ex"
        Me.cmbState_Ex.Size = New System.Drawing.Size(94, 22)
        Me.cmbState_Ex.TabIndex = 4
        '
        'Label299
        '
        Me.Label299.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label299.AutoSize = true
        Me.Label299.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label299.ForeColor = System.Drawing.Color.Red
        Me.Label299.Location = New System.Drawing.Point(255, 9)
        Me.Label299.Name = "Label299"
        Me.Label299.Size = New System.Drawing.Size(22, 13)
        Me.Label299.TabIndex = 29
        Me.Label299.Text = "yrs"
        '
        'Label300
        '
        Me.Label300.AutoSize = true
        Me.Label300.BackColor = System.Drawing.Color.Transparent
        Me.Label300.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label300.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label300.Location = New System.Drawing.Point(650, 56)
        Me.Label300.Name = "Label300"
        Me.Label300.Size = New System.Drawing.Size(45, 14)
        Me.Label300.TabIndex = 11
        Me.Label300.Text = "State :"
        Me.Label300.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtZip_Ex
        '
        Me.txtZip_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtZip_Ex.ForeColor = System.Drawing.Color.Black
        Me.txtZip_Ex.Location = New System.Drawing.Point(948, 25)
        Me.txtZip_Ex.MaxLength = 50
        Me.txtZip_Ex.Name = "txtZip_Ex"
        Me.txtZip_Ex.Size = New System.Drawing.Size(94, 22)
        Me.txtZip_Ex.TabIndex = 6
        '
        'Label301
        '
        Me.Label301.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label301.AutoSize = true
        Me.Label301.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label301.ForeColor = System.Drawing.Color.Red
        Me.Label301.Location = New System.Drawing.Point(177, 9)
        Me.Label301.Name = "Label301"
        Me.Label301.Size = New System.Drawing.Size(21, 13)
        Me.Label301.TabIndex = 25
        Me.Label301.Text = "mn"
        '
        'Label302
        '
        Me.Label302.AutoSize = true
        Me.Label302.BackColor = System.Drawing.Color.Transparent
        Me.Label302.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label302.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label302.Location = New System.Drawing.Point(823, 56)
        Me.Label302.Name = "Label302"
        Me.Label302.Size = New System.Drawing.Size(122, 14)
        Me.Label302.TabIndex = 13
        Me.Label302.Text = "Employment Status :"
        Me.Label302.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label303
        '
        Me.Label303.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label303.AutoSize = true
        Me.Label303.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label303.ForeColor = System.Drawing.Color.Red
        Me.Label303.Location = New System.Drawing.Point(122, 9)
        Me.Label303.Name = "Label303"
        Me.Label303.Size = New System.Drawing.Size(22, 13)
        Me.Label303.TabIndex = 25
        Me.Label303.Text = "yrs"
        '
        'cmbEmpStatus_Ex
        '
        Me.cmbEmpStatus_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEmpStatus_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbEmpStatus_Ex.ForeColor = System.Drawing.Color.Black
        Me.cmbEmpStatus_Ex.Location = New System.Drawing.Point(948, 52)
        Me.cmbEmpStatus_Ex.Name = "cmbEmpStatus_Ex"
        Me.cmbEmpStatus_Ex.Size = New System.Drawing.Size(94, 22)
        Me.cmbEmpStatus_Ex.TabIndex = 8
        '
        'cmbAgeMaxMnth_Ex
        '
        Me.cmbAgeMaxMnth_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMaxMnth_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAgeMaxMnth_Ex.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMaxMnth_Ex.Location = New System.Drawing.Point(299, 25)
        Me.cmbAgeMaxMnth_Ex.Name = "cmbAgeMaxMnth_Ex"
        Me.cmbAgeMaxMnth_Ex.Size = New System.Drawing.Size(49, 22)
        Me.cmbAgeMaxMnth_Ex.TabIndex = 27
        '
        'Label304
        '
        Me.Label304.AutoSize = true
        Me.Label304.BackColor = System.Drawing.Color.Transparent
        Me.Label304.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label304.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label304.Location = New System.Drawing.Point(882, 29)
        Me.Label304.Name = "Label304"
        Me.Label304.Size = New System.Drawing.Size(63, 14)
        Me.Label304.TabIndex = 12
        Me.Label304.Text = "Zip Code :"
        Me.Label304.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAgeMinMnth_Ex
        '
        Me.cmbAgeMinMnth_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMinMnth_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAgeMinMnth_Ex.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMinMnth_Ex.Location = New System.Drawing.Point(167, 25)
        Me.cmbAgeMinMnth_Ex.Name = "cmbAgeMinMnth_Ex"
        Me.cmbAgeMinMnth_Ex.Size = New System.Drawing.Size(49, 22)
        Me.cmbAgeMinMnth_Ex.TabIndex = 26
        '
        'Label305
        '
        Me.Label305.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label305.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label305.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label305.Location = New System.Drawing.Point(1, 84)
        Me.Label305.Name = "Label305"
        Me.Label305.Size = New System.Drawing.Size(1075, 1)
        Me.Label305.TabIndex = 22
        Me.Label305.Text = "label2"
        '
        'Label306
        '
        Me.Label306.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label306.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label306.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label306.Location = New System.Drawing.Point(0, 1)
        Me.Label306.Name = "Label306"
        Me.Label306.Size = New System.Drawing.Size(1, 84)
        Me.Label306.TabIndex = 21
        Me.Label306.Text = "label4"
        '
        'Label307
        '
        Me.Label307.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label307.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label307.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label307.Location = New System.Drawing.Point(1076, 1)
        Me.Label307.Name = "Label307"
        Me.Label307.Size = New System.Drawing.Size(1, 84)
        Me.Label307.TabIndex = 20
        Me.Label307.Text = "label3"
        '
        'Label308
        '
        Me.Label308.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label308.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label308.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label308.Location = New System.Drawing.Point(0, 0)
        Me.Label308.Name = "Label308"
        Me.Label308.Size = New System.Drawing.Size(1077, 1)
        Me.Label308.TabIndex = 19
        Me.Label308.Text = "label1"
        '
        'cmbAgeMax_Ex
        '
        Me.cmbAgeMax_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMax_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAgeMax_Ex.Location = New System.Drawing.Point(244, 25)
        Me.cmbAgeMax_Ex.Name = "cmbAgeMax_Ex"
        Me.cmbAgeMax_Ex.Size = New System.Drawing.Size(53, 22)
        Me.cmbAgeMax_Ex.TabIndex = 1
        '
        'cmbAgeMin_Ex
        '
        Me.cmbAgeMin_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMin_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbAgeMin_Ex.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMin_Ex.Location = New System.Drawing.Point(110, 25)
        Me.cmbAgeMin_Ex.Name = "cmbAgeMin_Ex"
        Me.cmbAgeMin_Ex.Size = New System.Drawing.Size(55, 22)
        Me.cmbAgeMin_Ex.TabIndex = 0
        '
        'Label309
        '
        Me.Label309.AutoSize = true
        Me.Label309.BackColor = System.Drawing.Color.Transparent
        Me.Label309.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label309.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label309.Location = New System.Drawing.Point(462, 56)
        Me.Label309.Name = "Label309"
        Me.Label309.Size = New System.Drawing.Size(16, 14)
        Me.Label309.TabIndex = 18
        Me.Label309.Text = "in"
        Me.Label309.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label310
        '
        Me.Label310.AutoSize = true
        Me.Label310.BackColor = System.Drawing.Color.Transparent
        Me.Label310.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label310.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label310.Location = New System.Drawing.Point(463, 29)
        Me.Label310.Name = "Label310"
        Me.Label310.Size = New System.Drawing.Size(16, 14)
        Me.Label310.TabIndex = 18
        Me.Label310.Text = "in"
        Me.Label310.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label311
        '
        Me.Label311.AutoSize = true
        Me.Label311.BackColor = System.Drawing.Color.Transparent
        Me.Label311.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label311.Location = New System.Drawing.Point(221, 29)
        Me.Label311.Name = "Label311"
        Me.Label311.Size = New System.Drawing.Size(19, 14)
        Me.Label311.TabIndex = 18
        Me.Label311.Text = "to"
        Me.Label311.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label312
        '
        Me.Label312.AutoSize = true
        Me.Label312.BackColor = System.Drawing.Color.Transparent
        Me.Label312.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label312.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label312.Location = New System.Drawing.Point(377, 56)
        Me.Label312.Name = "Label312"
        Me.Label312.Size = New System.Drawing.Size(88, 14)
        Me.Label312.TabIndex = 5
        Me.Label312.Text = "Marital Status :"
        Me.Label312.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label313
        '
        Me.Label313.AutoSize = true
        Me.Label313.BackColor = System.Drawing.Color.Transparent
        Me.Label313.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label313.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label313.Location = New System.Drawing.Point(410, 29)
        Me.Label313.Name = "Label313"
        Me.Label313.Size = New System.Drawing.Size(55, 14)
        Me.Label313.TabIndex = 3
        Me.Label313.Text = "Gender :"
        Me.Label313.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label314
        '
        Me.Label314.AutoSize = true
        Me.Label314.BackColor = System.Drawing.Color.Transparent
        Me.Label314.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label314.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label314.Location = New System.Drawing.Point(12, 56)
        Me.Label314.Name = "Label314"
        Me.Label314.Size = New System.Drawing.Size(41, 14)
        Me.Label314.TabIndex = 4
        Me.Label314.Text = "Race :"
        Me.Label314.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label315
        '
        Me.Label315.AutoSize = true
        Me.Label315.BackColor = System.Drawing.Color.Transparent
        Me.Label315.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label315.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label315.Location = New System.Drawing.Point(52, 29)
        Me.Label315.Name = "Label315"
        Me.Label315.Size = New System.Drawing.Size(57, 14)
        Me.Label315.TabIndex = 1
        Me.Label315.Text = "between"
        Me.Label315.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label316
        '
        Me.Label316.AutoSize = true
        Me.Label316.BackColor = System.Drawing.Color.Transparent
        Me.Label316.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label316.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label316.Location = New System.Drawing.Point(16, 29)
        Me.Label316.Name = "Label316"
        Me.Label316.Size = New System.Drawing.Size(37, 14)
        Me.Label316.TabIndex = 1
        Me.Label316.Text = "Age :"
        Me.Label316.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel52
        '
        Me.Panel52.Controls.Add(Me.Panel53)
        Me.Panel52.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel52.Location = New System.Drawing.Point(0, 0)
        Me.Panel52.Name = "Panel52"
        Me.Panel52.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel52.Size = New System.Drawing.Size(1077, 28)
        Me.Panel52.TabIndex = 45
        '
        'Panel53
        '
        Me.Panel53.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel53.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel53.Controls.Add(Me.Label317)
        Me.Panel53.Controls.Add(Me.Label318)
        Me.Panel53.Controls.Add(Me.Label319)
        Me.Panel53.Controls.Add(Me.Label320)
        Me.Panel53.Controls.Add(Me.Label321)
        Me.Panel53.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel53.Location = New System.Drawing.Point(0, 0)
        Me.Panel53.Name = "Panel53"
        Me.Panel53.Size = New System.Drawing.Size(1077, 25)
        Me.Panel53.TabIndex = 19
        '
        'Label317
        '
        Me.Label317.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label317.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label317.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label317.Location = New System.Drawing.Point(1, 24)
        Me.Label317.Name = "Label317"
        Me.Label317.Size = New System.Drawing.Size(1075, 1)
        Me.Label317.TabIndex = 13
        Me.Label317.Text = "label2"
        '
        'Label318
        '
        Me.Label318.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label318.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label318.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label318.Location = New System.Drawing.Point(0, 1)
        Me.Label318.Name = "Label318"
        Me.Label318.Size = New System.Drawing.Size(1, 24)
        Me.Label318.TabIndex = 12
        Me.Label318.Text = "label4"
        '
        'Label319
        '
        Me.Label319.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label319.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label319.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label319.Location = New System.Drawing.Point(1076, 1)
        Me.Label319.Name = "Label319"
        Me.Label319.Size = New System.Drawing.Size(1, 24)
        Me.Label319.TabIndex = 11
        Me.Label319.Text = "label3"
        '
        'Label320
        '
        Me.Label320.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label320.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label320.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label320.Location = New System.Drawing.Point(0, 0)
        Me.Label320.Name = "Label320"
        Me.Label320.Size = New System.Drawing.Size(1077, 1)
        Me.Label320.TabIndex = 10
        Me.Label320.Text = "label1"
        '
        'Label321
        '
        Me.Label321.BackColor = System.Drawing.Color.Transparent
        Me.Label321.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label321.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label321.ForeColor = System.Drawing.Color.White
        Me.Label321.Image = CType(resources.GetObject("Label321.Image"),System.Drawing.Image)
        Me.Label321.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label321.Location = New System.Drawing.Point(0, 0)
        Me.Label321.Name = "Label321"
        Me.Label321.Size = New System.Drawing.Size(1077, 25)
        Me.Label321.TabIndex = 9
        Me.Label321.Text = "      Demographics Exceptions"
        Me.Label321.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlExceptionsRadiology
        '
        Me.pnlExceptionsRadiology.Controls.Add(Me.Panel18_Ex)
        Me.pnlExceptionsRadiology.Controls.Add(Me.Panel17_Ex)
        Me.pnlExceptionsRadiology.Controls.Add(Me.pnlInternalToolStripRadiology_Ex)
        Me.pnlExceptionsRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExceptionsRadiology.Location = New System.Drawing.Point(0, 0)
        Me.pnlExceptionsRadiology.Name = "pnlExceptionsRadiology"
        Me.pnlExceptionsRadiology.Size = New System.Drawing.Size(1077, 694)
        Me.pnlExceptionsRadiology.TabIndex = 1
        Me.pnlExceptionsRadiology.Visible = false
        '
        'Panel18_Ex
        '
        Me.Panel18_Ex.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.Panel18_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18_Ex.Controls.Add(Me.Label362)
        Me.Panel18_Ex.Controls.Add(Me.Label363)
        Me.Panel18_Ex.Controls.Add(Me.Label364)
        Me.Panel18_Ex.Controls.Add(Me.Label365)
        Me.Panel18_Ex.Controls.Add(Me.c1Labs_Ex)
        Me.Panel18_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel18_Ex.Location = New System.Drawing.Point(0, 80)
        Me.Panel18_Ex.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel18_Ex.Name = "Panel18_Ex"
        Me.Panel18_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel18_Ex.Size = New System.Drawing.Size(1077, 614)
        Me.Panel18_Ex.TabIndex = 20
        '
        'Label362
        '
        Me.Label362.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label362.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label362.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label362.Location = New System.Drawing.Point(1, 610)
        Me.Label362.Name = "Label362"
        Me.Label362.Size = New System.Drawing.Size(1075, 1)
        Me.Label362.TabIndex = 8
        Me.Label362.Text = "label2"
        '
        'Label363
        '
        Me.Label363.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label363.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label363.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label363.Location = New System.Drawing.Point(0, 1)
        Me.Label363.Name = "Label363"
        Me.Label363.Size = New System.Drawing.Size(1, 610)
        Me.Label363.TabIndex = 7
        Me.Label363.Text = "label4"
        '
        'Label364
        '
        Me.Label364.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label364.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label364.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label364.Location = New System.Drawing.Point(1076, 1)
        Me.Label364.Name = "Label364"
        Me.Label364.Size = New System.Drawing.Size(1, 610)
        Me.Label364.TabIndex = 6
        Me.Label364.Text = "label3"
        '
        'Label365
        '
        Me.Label365.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label365.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label365.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label365.Location = New System.Drawing.Point(0, 0)
        Me.Label365.Name = "Label365"
        Me.Label365.Size = New System.Drawing.Size(1077, 1)
        Me.Label365.TabIndex = 5
        Me.Label365.Text = "label1"
        '
        'c1Labs_Ex
        '
        Me.c1Labs_Ex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Labs_Ex.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.c1Labs_Ex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Labs_Ex.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.c1Labs_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Labs_Ex.ExtendLastCol = true
        Me.c1Labs_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.c1Labs_Ex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.c1Labs_Ex.Location = New System.Drawing.Point(0, 0)
        Me.c1Labs_Ex.Name = "c1Labs_Ex"
        Me.c1Labs_Ex.Rows.DefaultSize = 19
        Me.c1Labs_Ex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Labs_Ex.ShowCellLabels = true
        Me.c1Labs_Ex.Size = New System.Drawing.Size(1077, 611)
        Me.c1Labs_Ex.StyleInfo = resources.GetString("c1Labs_Ex.StyleInfo")
        Me.c1Labs_Ex.TabIndex = 0
        Me.c1Labs_Ex.Tree.NodeImageCollapsed = CType(resources.GetObject("c1Labs_Ex.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.c1Labs_Ex.Tree.NodeImageExpanded = CType(resources.GetObject("c1Labs_Ex.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'Panel17_Ex
        '
        Me.Panel17_Ex.BackColor = System.Drawing.Color.Transparent
        Me.Panel17_Ex.Controls.Add(Me.txtLabsSearch_Ex)
        Me.Panel17_Ex.Controls.Add(Me.bntLabClear_Ex)
        Me.Panel17_Ex.Controls.Add(Me.Label366)
        Me.Panel17_Ex.Controls.Add(Me.Label367)
        Me.Panel17_Ex.Controls.Add(Me.PictureBox5)
        Me.Panel17_Ex.Controls.Add(Me.Label368)
        Me.Panel17_Ex.Controls.Add(Me.Label369)
        Me.Panel17_Ex.Controls.Add(Me.Label370)
        Me.Panel17_Ex.Controls.Add(Me.Label371)
        Me.Panel17_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel17_Ex.ForeColor = System.Drawing.Color.Black
        Me.Panel17_Ex.Location = New System.Drawing.Point(0, 54)
        Me.Panel17_Ex.Name = "Panel17_Ex"
        Me.Panel17_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel17_Ex.Size = New System.Drawing.Size(1077, 26)
        Me.Panel17_Ex.TabIndex = 19
        '
        'txtLabsSearch_Ex
        '
        Me.txtLabsSearch_Ex.BackColor = System.Drawing.Color.White
        Me.txtLabsSearch_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabsSearch_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLabsSearch_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtLabsSearch_Ex.ForeColor = System.Drawing.Color.Black
        Me.txtLabsSearch_Ex.Location = New System.Drawing.Point(29, 5)
        Me.txtLabsSearch_Ex.Name = "txtLabsSearch_Ex"
        Me.txtLabsSearch_Ex.Size = New System.Drawing.Size(1026, 15)
        Me.txtLabsSearch_Ex.TabIndex = 0
        '
        'bntLabClear_Ex
        '
        Me.bntLabClear_Ex.BackColor = System.Drawing.Color.Transparent
        Me.bntLabClear_Ex.BackgroundImage = CType(resources.GetObject("bntLabClear_Ex.BackgroundImage"),System.Drawing.Image)
        Me.bntLabClear_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.bntLabClear_Ex.Dock = System.Windows.Forms.DockStyle.Right
        Me.bntLabClear_Ex.FlatAppearance.BorderSize = 0
        Me.bntLabClear_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.bntLabClear_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.bntLabClear_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bntLabClear_Ex.Image = CType(resources.GetObject("bntLabClear_Ex.Image"),System.Drawing.Image)
        Me.bntLabClear_Ex.Location = New System.Drawing.Point(1055, 5)
        Me.bntLabClear_Ex.Name = "bntLabClear_Ex"
        Me.bntLabClear_Ex.Size = New System.Drawing.Size(21, 15)
        Me.bntLabClear_Ex.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.bntLabClear_Ex, "Clear Search")
        Me.bntLabClear_Ex.UseVisualStyleBackColor = false
        '
        'Label366
        '
        Me.Label366.BackColor = System.Drawing.Color.White
        Me.Label366.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label366.Location = New System.Drawing.Point(29, 1)
        Me.Label366.Name = "Label366"
        Me.Label366.Size = New System.Drawing.Size(1047, 4)
        Me.Label366.TabIndex = 37
        '
        'Label367
        '
        Me.Label367.BackColor = System.Drawing.Color.White
        Me.Label367.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label367.Location = New System.Drawing.Point(29, 20)
        Me.Label367.Name = "Label367"
        Me.Label367.Size = New System.Drawing.Size(1047, 2)
        Me.Label367.TabIndex = 38
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.White
        Me.PictureBox5.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox5.Image = CType(resources.GetObject("PictureBox5.Image"),System.Drawing.Image)
        Me.PictureBox5.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox5.TabIndex = 9
        Me.PictureBox5.TabStop = false
        '
        'Label368
        '
        Me.Label368.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label368.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label368.Location = New System.Drawing.Point(1, 22)
        Me.Label368.Name = "Label368"
        Me.Label368.Size = New System.Drawing.Size(1075, 1)
        Me.Label368.TabIndex = 35
        Me.Label368.Text = "label1"
        '
        'Label369
        '
        Me.Label369.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label369.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label369.Location = New System.Drawing.Point(1, 0)
        Me.Label369.Name = "Label369"
        Me.Label369.Size = New System.Drawing.Size(1075, 1)
        Me.Label369.TabIndex = 36
        Me.Label369.Text = "label1"
        '
        'Label370
        '
        Me.Label370.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label370.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label370.Location = New System.Drawing.Point(0, 0)
        Me.Label370.Name = "Label370"
        Me.Label370.Size = New System.Drawing.Size(1, 23)
        Me.Label370.TabIndex = 39
        Me.Label370.Text = "label4"
        '
        'Label371
        '
        Me.Label371.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label371.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label371.Location = New System.Drawing.Point(1076, 0)
        Me.Label371.Name = "Label371"
        Me.Label371.Size = New System.Drawing.Size(1, 23)
        Me.Label371.TabIndex = 40
        Me.Label371.Text = "label4"
        '
        'pnlInternalToolStripRadiology_Ex
        '
        Me.pnlInternalToolStripRadiology_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripRadiology_Ex.Controls.Add(Me.Panel76)
        Me.pnlInternalToolStripRadiology_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripRadiology_Ex.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripRadiology_Ex.Name = "pnlInternalToolStripRadiology_Ex"
        Me.pnlInternalToolStripRadiology_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripRadiology_Ex.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripRadiology_Ex.TabIndex = 56
        '
        'Panel76
        '
        Me.Panel76.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel76.Controls.Add(Me.ToolStrip7)
        Me.Panel76.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel76.Location = New System.Drawing.Point(0, 0)
        Me.Panel76.Name = "Panel76"
        Me.Panel76.Size = New System.Drawing.Size(1077, 54)
        Me.Panel76.TabIndex = 4
        '
        'ToolStrip7
        '
        Me.ToolStrip7.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip7.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip7.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip7.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Btn_SaveRadiology_Ex, Me.ToolStripButton2})
        Me.ToolStrip7.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip7.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip7.Name = "ToolStrip7"
        Me.ToolStrip7.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip7.TabIndex = 0
        Me.ToolStrip7.Text = "ToolStrip7"
        '
        'ts_Btn_SaveRadiology_Ex
        '
        Me.ts_Btn_SaveRadiology_Ex.Image = CType(resources.GetObject("ts_Btn_SaveRadiology_Ex.Image"),System.Drawing.Image)
        Me.ts_Btn_SaveRadiology_Ex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_Btn_SaveRadiology_Ex.Name = "ts_Btn_SaveRadiology_Ex"
        Me.ts_Btn_SaveRadiology_Ex.Size = New System.Drawing.Size(43, 50)
        Me.ts_Btn_SaveRadiology_Ex.Tag = "Save"
        Me.ts_Btn_SaveRadiology_Ex.Text = "&Done"
        Me.ts_Btn_SaveRadiology_Ex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_Btn_SaveRadiology_Ex.ToolTipText = "Done"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"),System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton2.Tag = "Cancel"
        Me.ToolStripButton2.Text = "&Cancel"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton2.Visible = false
        '
        'pnlExceptionsHistory
        '
        Me.pnlExceptionsHistory.Controls.Add(Me.Panel22_Ex)
        Me.pnlExceptionsHistory.Controls.Add(Me.Panel20_Ex)
        Me.pnlExceptionsHistory.Controls.Add(Me.Panel16_Ex)
        Me.pnlExceptionsHistory.Controls.Add(Me.Panel11_Ex)
        Me.pnlExceptionsHistory.Controls.Add(Me.btnHistorySearch_Ex)
        Me.pnlExceptionsHistory.Controls.Add(Me.pnlInternalToolStripHistory_Ex)
        Me.pnlExceptionsHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExceptionsHistory.Location = New System.Drawing.Point(0, 0)
        Me.pnlExceptionsHistory.Name = "pnlExceptionsHistory"
        Me.pnlExceptionsHistory.Size = New System.Drawing.Size(1077, 694)
        Me.pnlExceptionsHistory.TabIndex = 0
        Me.pnlExceptionsHistory.Visible = false
        '
        'Panel22_Ex
        '
        Me.Panel22_Ex.BackColor = System.Drawing.Color.Transparent
        Me.Panel22_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel22_Ex.Controls.Add(Me.Label372)
        Me.Panel22_Ex.Controls.Add(Me.Label373)
        Me.Panel22_Ex.Controls.Add(Me.trvSelectedHistory_Ex)
        Me.Panel22_Ex.Controls.Add(Me.Label374)
        Me.Panel22_Ex.Controls.Add(Me.Label375)
        Me.Panel22_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel22_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel22_Ex.Location = New System.Drawing.Point(0, 465)
        Me.Panel22_Ex.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel22_Ex.Name = "Panel22_Ex"
        Me.Panel22_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel22_Ex.Size = New System.Drawing.Size(1077, 229)
        Me.Panel22_Ex.TabIndex = 23
        '
        'Label372
        '
        Me.Label372.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label372.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label372.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label372.Location = New System.Drawing.Point(1, 225)
        Me.Label372.Name = "Label372"
        Me.Label372.Size = New System.Drawing.Size(1075, 1)
        Me.Label372.TabIndex = 8
        Me.Label372.Text = "label2"
        '
        'Label373
        '
        Me.Label373.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label373.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label373.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label373.Location = New System.Drawing.Point(0, 1)
        Me.Label373.Name = "Label373"
        Me.Label373.Size = New System.Drawing.Size(1, 225)
        Me.Label373.TabIndex = 7
        Me.Label373.Text = "label4"
        '
        'trvSelectedHistory_Ex
        '
        Me.trvSelectedHistory_Ex.BackColor = System.Drawing.Color.White
        Me.trvSelectedHistory_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedHistory_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedHistory_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvSelectedHistory_Ex.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedHistory_Ex.HideSelection = false
        Me.trvSelectedHistory_Ex.ImageIndex = 11
        Me.trvSelectedHistory_Ex.ImageList = Me.ImageList1
        Me.trvSelectedHistory_Ex.ItemHeight = 18
        Me.trvSelectedHistory_Ex.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedHistory_Ex.Name = "trvSelectedHistory_Ex"
        Me.trvSelectedHistory_Ex.SelectedImageIndex = 11
        Me.trvSelectedHistory_Ex.ShowLines = false
        Me.trvSelectedHistory_Ex.Size = New System.Drawing.Size(1076, 225)
        Me.trvSelectedHistory_Ex.TabIndex = 0
        '
        'Label374
        '
        Me.Label374.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label374.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label374.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label374.Location = New System.Drawing.Point(1076, 1)
        Me.Label374.Name = "Label374"
        Me.Label374.Size = New System.Drawing.Size(1, 225)
        Me.Label374.TabIndex = 6
        Me.Label374.Text = "label3"
        '
        'Label375
        '
        Me.Label375.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label375.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label375.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label375.Location = New System.Drawing.Point(0, 0)
        Me.Label375.Name = "Label375"
        Me.Label375.Size = New System.Drawing.Size(1077, 1)
        Me.Label375.TabIndex = 5
        Me.Label375.Text = "label1"
        '
        'Panel20_Ex
        '
        Me.Panel20_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel20_Ex.Controls.Add(Me.Panel80)
        Me.Panel20_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel20_Ex.Location = New System.Drawing.Point(0, 438)
        Me.Panel20_Ex.Name = "Panel20_Ex"
        Me.Panel20_Ex.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel20_Ex.Size = New System.Drawing.Size(1077, 27)
        Me.Panel20_Ex.TabIndex = 22
        '
        'Panel80
        '
        Me.Panel80.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel80.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel80.Controls.Add(Me.Label376)
        Me.Panel80.Controls.Add(Me.Label377)
        Me.Panel80.Controls.Add(Me.Label378)
        Me.Panel80.Controls.Add(Me.Label379)
        Me.Panel80.Controls.Add(Me.Label380)
        Me.Panel80.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel80.Location = New System.Drawing.Point(0, 3)
        Me.Panel80.Name = "Panel80"
        Me.Panel80.Size = New System.Drawing.Size(1077, 21)
        Me.Panel80.TabIndex = 14
        '
        'Label376
        '
        Me.Label376.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label376.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label376.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label376.Location = New System.Drawing.Point(1076, 1)
        Me.Label376.Name = "Label376"
        Me.Label376.Size = New System.Drawing.Size(1, 19)
        Me.Label376.TabIndex = 11
        Me.Label376.Text = "label3"
        '
        'Label377
        '
        Me.Label377.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label377.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label377.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label377.Location = New System.Drawing.Point(0, 1)
        Me.Label377.Name = "Label377"
        Me.Label377.Size = New System.Drawing.Size(1, 19)
        Me.Label377.TabIndex = 12
        Me.Label377.Text = "label4"
        '
        'Label378
        '
        Me.Label378.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label378.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label378.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label378.Location = New System.Drawing.Point(0, 0)
        Me.Label378.Name = "Label378"
        Me.Label378.Size = New System.Drawing.Size(1077, 1)
        Me.Label378.TabIndex = 10
        Me.Label378.Text = "label1"
        '
        'Label379
        '
        Me.Label379.BackColor = System.Drawing.Color.Transparent
        Me.Label379.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label379.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label379.ForeColor = System.Drawing.Color.White
        Me.Label379.Image = CType(resources.GetObject("Label379.Image"),System.Drawing.Image)
        Me.Label379.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label379.Location = New System.Drawing.Point(0, 0)
        Me.Label379.Name = "Label379"
        Me.Label379.Size = New System.Drawing.Size(1077, 20)
        Me.Label379.TabIndex = 9
        Me.Label379.Text = "      Selected History"
        Me.Label379.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label380
        '
        Me.Label380.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label380.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label380.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label380.Location = New System.Drawing.Point(0, 20)
        Me.Label380.Name = "Label380"
        Me.Label380.Size = New System.Drawing.Size(1077, 1)
        Me.Label380.TabIndex = 13
        Me.Label380.Text = "label2"
        '
        'Panel16_Ex
        '
        Me.Panel16_Ex.Controls.Add(Me.Panel9_Ex)
        Me.Panel16_Ex.Controls.Add(Me.Panel83)
        Me.Panel16_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16_Ex.Location = New System.Drawing.Point(0, 81)
        Me.Panel16_Ex.Name = "Panel16_Ex"
        Me.Panel16_Ex.Size = New System.Drawing.Size(1077, 357)
        Me.Panel16_Ex.TabIndex = 23
        '
        'Panel9_Ex
        '
        Me.Panel9_Ex.Controls.Add(Me.GloUC_TrvHistoryEx)
        Me.Panel9_Ex.Controls.Add(Me.trvHistoryRight_Ex)
        Me.Panel9_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9_Ex.Location = New System.Drawing.Point(287, 0)
        Me.Panel9_Ex.Name = "Panel9_Ex"
        Me.Panel9_Ex.Size = New System.Drawing.Size(790, 357)
        Me.Panel9_Ex.TabIndex = 20
        '
        'trvHistoryRight_Ex
        '
        Me.trvHistoryRight_Ex.BackColor = System.Drawing.Color.White
        Me.trvHistoryRight_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHistoryRight_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvHistoryRight_Ex.ForeColor = System.Drawing.Color.Black
        Me.trvHistoryRight_Ex.HideSelection = false
        Me.trvHistoryRight_Ex.ImageIndex = 0
        Me.trvHistoryRight_Ex.ImageList = Me.ImageList1
        Me.trvHistoryRight_Ex.ItemHeight = 18
        Me.trvHistoryRight_Ex.Location = New System.Drawing.Point(0, 1)
        Me.trvHistoryRight_Ex.Name = "trvHistoryRight_Ex"
        Me.trvHistoryRight_Ex.SelectedImageIndex = 0
        Me.trvHistoryRight_Ex.ShowLines = false
        Me.trvHistoryRight_Ex.Size = New System.Drawing.Size(326, 274)
        Me.trvHistoryRight_Ex.TabIndex = 2
        Me.trvHistoryRight_Ex.Visible = false
        '
        'Panel83
        '
        Me.Panel83.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel83.Controls.Add(Me.Label381)
        Me.Panel83.Controls.Add(Me.Label382)
        Me.Panel83.Controls.Add(Me.Label383)
        Me.Panel83.Controls.Add(Me.Label384)
        Me.Panel83.Controls.Add(Me.TreeView10)
        Me.Panel83.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel83.Enabled = false
        Me.Panel83.Location = New System.Drawing.Point(0, 0)
        Me.Panel83.Name = "Panel83"
        Me.Panel83.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Panel83.Size = New System.Drawing.Size(287, 357)
        Me.Panel83.TabIndex = 1
        Me.Panel83.Visible = false
        '
        'Label381
        '
        Me.Label381.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label381.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label381.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label381.Location = New System.Drawing.Point(1, 356)
        Me.Label381.Name = "Label381"
        Me.Label381.Size = New System.Drawing.Size(282, 1)
        Me.Label381.TabIndex = 12
        Me.Label381.Text = "label2"
        '
        'Label382
        '
        Me.Label382.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label382.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label382.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label382.Location = New System.Drawing.Point(0, 1)
        Me.Label382.Name = "Label382"
        Me.Label382.Size = New System.Drawing.Size(1, 356)
        Me.Label382.TabIndex = 11
        Me.Label382.Text = "label4"
        '
        'Label383
        '
        Me.Label383.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label383.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label383.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label383.Location = New System.Drawing.Point(283, 1)
        Me.Label383.Name = "Label383"
        Me.Label383.Size = New System.Drawing.Size(1, 356)
        Me.Label383.TabIndex = 10
        Me.Label383.Text = "label3"
        '
        'Label384
        '
        Me.Label384.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label384.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label384.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label384.Location = New System.Drawing.Point(0, 0)
        Me.Label384.Name = "Label384"
        Me.Label384.Size = New System.Drawing.Size(284, 1)
        Me.Label384.TabIndex = 9
        Me.Label384.Text = "label1"
        '
        'TreeView10
        '
        Me.TreeView10.BackColor = System.Drawing.Color.White
        Me.TreeView10.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView10.ForeColor = System.Drawing.Color.Black
        Me.TreeView10.HideSelection = false
        Me.TreeView10.ItemHeight = 18
        Me.TreeView10.Location = New System.Drawing.Point(0, 0)
        Me.TreeView10.Name = "TreeView10"
        Me.TreeView10.ShowLines = false
        Me.TreeView10.Size = New System.Drawing.Size(284, 357)
        Me.TreeView10.TabIndex = 0
        Me.TreeView10.Visible = false
        '
        'Panel11_Ex
        '
        Me.Panel11_Ex.Controls.Add(Me.Panel8_Ex)
        Me.Panel11_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11_Ex.Location = New System.Drawing.Point(0, 54)
        Me.Panel11_Ex.Name = "Panel11_Ex"
        Me.Panel11_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel11_Ex.Size = New System.Drawing.Size(1077, 27)
        Me.Panel11_Ex.TabIndex = 21
        '
        'Panel8_Ex
        '
        Me.Panel8_Ex.BackColor = System.Drawing.Color.Transparent
        Me.Panel8_Ex.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel8_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8_Ex.Controls.Add(Me.cmbHistoryCategory_Ex)
        Me.Panel8_Ex.Controls.Add(Me.Label385)
        Me.Panel8_Ex.Controls.Add(Me.txtSearch_Ex)
        Me.Panel8_Ex.Controls.Add(Me.Label386)
        Me.Panel8_Ex.Controls.Add(Me.Label387)
        Me.Panel8_Ex.Controls.Add(Me.Label388)
        Me.Panel8_Ex.Controls.Add(Me.Label389)
        Me.Panel8_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel8_Ex.ForeColor = System.Drawing.Color.Black
        Me.Panel8_Ex.Location = New System.Drawing.Point(0, 0)
        Me.Panel8_Ex.Name = "Panel8_Ex"
        Me.Panel8_Ex.Size = New System.Drawing.Size(1077, 24)
        Me.Panel8_Ex.TabIndex = 19
        '
        'cmbHistoryCategory_Ex
        '
        Me.cmbHistoryCategory_Ex.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbHistoryCategory_Ex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHistoryCategory_Ex.ForeColor = System.Drawing.Color.Black
        Me.cmbHistoryCategory_Ex.FormattingEnabled = true
        Me.cmbHistoryCategory_Ex.Location = New System.Drawing.Point(131, 1)
        Me.cmbHistoryCategory_Ex.Name = "cmbHistoryCategory_Ex"
        Me.cmbHistoryCategory_Ex.Size = New System.Drawing.Size(284, 22)
        Me.cmbHistoryCategory_Ex.TabIndex = 0
        '
        'Label385
        '
        Me.Label385.BackColor = System.Drawing.Color.Transparent
        Me.Label385.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label385.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label385.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label385.Location = New System.Drawing.Point(1, 1)
        Me.Label385.Name = "Label385"
        Me.Label385.Size = New System.Drawing.Size(130, 22)
        Me.Label385.TabIndex = 13
        Me.Label385.Text = "History Category :"
        Me.Label385.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearch_Ex
        '
        Me.txtSearch_Ex.BackColor = System.Drawing.Color.White
        Me.txtSearch_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtSearch_Ex.ForeColor = System.Drawing.Color.Black
        Me.txtSearch_Ex.Location = New System.Drawing.Point(507, 5)
        Me.txtSearch_Ex.Name = "txtSearch_Ex"
        Me.txtSearch_Ex.Size = New System.Drawing.Size(87, 15)
        Me.txtSearch_Ex.TabIndex = 1
        Me.txtSearch_Ex.Visible = false
        '
        'Label386
        '
        Me.Label386.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label386.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label386.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label386.Location = New System.Drawing.Point(1, 23)
        Me.Label386.Name = "Label386"
        Me.Label386.Size = New System.Drawing.Size(1075, 1)
        Me.Label386.TabIndex = 12
        Me.Label386.Text = "label2"
        '
        'Label387
        '
        Me.Label387.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label387.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label387.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label387.Location = New System.Drawing.Point(0, 1)
        Me.Label387.Name = "Label387"
        Me.Label387.Size = New System.Drawing.Size(1, 23)
        Me.Label387.TabIndex = 11
        Me.Label387.Text = "label4"
        '
        'Label388
        '
        Me.Label388.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label388.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label388.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label388.Location = New System.Drawing.Point(1076, 1)
        Me.Label388.Name = "Label388"
        Me.Label388.Size = New System.Drawing.Size(1, 23)
        Me.Label388.TabIndex = 10
        Me.Label388.Text = "label3"
        '
        'Label389
        '
        Me.Label389.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label389.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label389.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label389.Location = New System.Drawing.Point(0, 0)
        Me.Label389.Name = "Label389"
        Me.Label389.Size = New System.Drawing.Size(1077, 1)
        Me.Label389.TabIndex = 9
        Me.Label389.Text = "label1"
        '
        'btnHistorySearch_Ex
        '
        Me.btnHistorySearch_Ex.BackColor = System.Drawing.Color.Transparent
        Me.btnHistorySearch_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHistorySearch_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(197,Byte),Integer), CType(CType(108,Byte),Integer))
        Me.btnHistorySearch_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(160,Byte),Integer))
        Me.btnHistorySearch_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistorySearch_Ex.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnHistorySearch_Ex.Location = New System.Drawing.Point(254, 134)
        Me.btnHistorySearch_Ex.Name = "btnHistorySearch_Ex"
        Me.btnHistorySearch_Ex.Size = New System.Drawing.Size(63, 22)
        Me.btnHistorySearch_Ex.TabIndex = 2
        Me.btnHistorySearch_Ex.Text = "Search"
        Me.btnHistorySearch_Ex.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHistorySearch_Ex.UseVisualStyleBackColor = false
        Me.btnHistorySearch_Ex.Visible = false
        '
        'pnlInternalToolStripHistory_Ex
        '
        Me.pnlInternalToolStripHistory_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripHistory_Ex.Controls.Add(Me.Panel87)
        Me.pnlInternalToolStripHistory_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripHistory_Ex.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripHistory_Ex.Name = "pnlInternalToolStripHistory_Ex"
        Me.pnlInternalToolStripHistory_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripHistory_Ex.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripHistory_Ex.TabIndex = 55
        '
        'Panel87
        '
        Me.Panel87.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel87.Controls.Add(Me.ToolStrip8)
        Me.Panel87.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel87.Location = New System.Drawing.Point(0, 0)
        Me.Panel87.Name = "Panel87"
        Me.Panel87.Size = New System.Drawing.Size(1077, 54)
        Me.Panel87.TabIndex = 4
        '
        'ToolStrip8
        '
        Me.ToolStrip8.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip8.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip8.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip8.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveHistory_Ex, Me.ToolStripButton4})
        Me.ToolStrip8.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip8.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip8.Name = "ToolStrip8"
        Me.ToolStrip8.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip8.TabIndex = 0
        Me.ToolStrip8.Text = "ToolStrip8"
        '
        'tsBtn_SaveHistory_Ex
        '
        Me.tsBtn_SaveHistory_Ex.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveHistory_Ex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveHistory_Ex.Name = "tsBtn_SaveHistory_Ex"
        Me.tsBtn_SaveHistory_Ex.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveHistory_Ex.Tag = "Save"
        Me.tsBtn_SaveHistory_Ex.Text = "&Done"
        Me.tsBtn_SaveHistory_Ex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveHistory_Ex.ToolTipText = "Done"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"),System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton4.Tag = "Cancel"
        Me.ToolStripButton4.Text = "&Cancel"
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton4.Visible = false
        '
        'pnlExceptionsInsurance
        '
        Me.pnlExceptionsInsurance.Controls.Add(Me.pnltrvSelectedInsurance_Ex)
        Me.pnlExceptionsInsurance.Controls.Add(Me.pnlSelectedInsuranceLabel_Ex)
        Me.pnlExceptionsInsurance.Controls.Add(Me.Splitter5)
        Me.pnlExceptionsInsurance.Controls.Add(Me.GloUC_trvInsurance_Ex)
        Me.pnlExceptionsInsurance.Controls.Add(Me.pnlInternalToolStripInsurance_Ex)
        Me.pnlExceptionsInsurance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExceptionsInsurance.Location = New System.Drawing.Point(0, 0)
        Me.pnlExceptionsInsurance.Name = "pnlExceptionsInsurance"
        Me.pnlExceptionsInsurance.Size = New System.Drawing.Size(1077, 694)
        Me.pnlExceptionsInsurance.TabIndex = 3
        Me.pnlExceptionsInsurance.Visible = false
        '
        'pnltrvSelectedInsurance_Ex
        '
        Me.pnltrvSelectedInsurance_Ex.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvSelectedInsurance_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvSelectedInsurance_Ex.Controls.Add(Me.Label338)
        Me.pnltrvSelectedInsurance_Ex.Controls.Add(Me.Label339)
        Me.pnltrvSelectedInsurance_Ex.Controls.Add(Me.trvSelectedInsurance_Ex)
        Me.pnltrvSelectedInsurance_Ex.Controls.Add(Me.Label340)
        Me.pnltrvSelectedInsurance_Ex.Controls.Add(Me.Label341)
        Me.pnltrvSelectedInsurance_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSelectedInsurance_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnltrvSelectedInsurance_Ex.Location = New System.Drawing.Point(0, 476)
        Me.pnltrvSelectedInsurance_Ex.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedInsurance_Ex.Name = "pnltrvSelectedInsurance_Ex"
        Me.pnltrvSelectedInsurance_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedInsurance_Ex.Size = New System.Drawing.Size(1077, 218)
        Me.pnltrvSelectedInsurance_Ex.TabIndex = 21
        '
        'Label338
        '
        Me.Label338.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label338.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label338.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label338.Location = New System.Drawing.Point(1, 214)
        Me.Label338.Name = "Label338"
        Me.Label338.Size = New System.Drawing.Size(1075, 1)
        Me.Label338.TabIndex = 8
        Me.Label338.Text = "label2"
        '
        'Label339
        '
        Me.Label339.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label339.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label339.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label339.Location = New System.Drawing.Point(0, 1)
        Me.Label339.Name = "Label339"
        Me.Label339.Size = New System.Drawing.Size(1, 214)
        Me.Label339.TabIndex = 7
        Me.Label339.Text = "label4"
        '
        'trvSelectedInsurance_Ex
        '
        Me.trvSelectedInsurance_Ex.BackColor = System.Drawing.Color.White
        Me.trvSelectedInsurance_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedInsurance_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedInsurance_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvSelectedInsurance_Ex.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedInsurance_Ex.HideSelection = false
        Me.trvSelectedInsurance_Ex.ImageIndex = 0
        Me.trvSelectedInsurance_Ex.ImageList = Me.ImageList1
        Me.trvSelectedInsurance_Ex.ItemHeight = 18
        Me.trvSelectedInsurance_Ex.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedInsurance_Ex.Name = "trvSelectedInsurance_Ex"
        Me.trvSelectedInsurance_Ex.SelectedImageIndex = 0
        Me.trvSelectedInsurance_Ex.ShowLines = false
        Me.trvSelectedInsurance_Ex.Size = New System.Drawing.Size(1076, 214)
        Me.trvSelectedInsurance_Ex.TabIndex = 0
        '
        'Label340
        '
        Me.Label340.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label340.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label340.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label340.Location = New System.Drawing.Point(1076, 1)
        Me.Label340.Name = "Label340"
        Me.Label340.Size = New System.Drawing.Size(1, 214)
        Me.Label340.TabIndex = 6
        Me.Label340.Text = "label3"
        '
        'Label341
        '
        Me.Label341.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label341.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label341.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label341.Location = New System.Drawing.Point(0, 0)
        Me.Label341.Name = "Label341"
        Me.Label341.Size = New System.Drawing.Size(1077, 1)
        Me.Label341.TabIndex = 5
        Me.Label341.Text = "label1"
        '
        'pnlSelectedInsuranceLabel_Ex
        '
        Me.pnlSelectedInsuranceLabel_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedInsuranceLabel_Ex.Controls.Add(Me.Panel57)
        Me.pnlSelectedInsuranceLabel_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectedInsuranceLabel_Ex.Location = New System.Drawing.Point(0, 449)
        Me.pnlSelectedInsuranceLabel_Ex.Name = "pnlSelectedInsuranceLabel_Ex"
        Me.pnlSelectedInsuranceLabel_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedInsuranceLabel_Ex.Size = New System.Drawing.Size(1077, 27)
        Me.pnlSelectedInsuranceLabel_Ex.TabIndex = 20
        '
        'Panel57
        '
        Me.Panel57.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel57.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel57.Controls.Add(Me.Label342)
        Me.Panel57.Controls.Add(Me.Label343)
        Me.Panel57.Controls.Add(Me.Label344)
        Me.Panel57.Controls.Add(Me.Label345)
        Me.Panel57.Controls.Add(Me.Label346)
        Me.Panel57.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel57.Location = New System.Drawing.Point(0, 0)
        Me.Panel57.Name = "Panel57"
        Me.Panel57.Size = New System.Drawing.Size(1077, 24)
        Me.Panel57.TabIndex = 14
        '
        'Label342
        '
        Me.Label342.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label342.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label342.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label342.Location = New System.Drawing.Point(1076, 1)
        Me.Label342.Name = "Label342"
        Me.Label342.Size = New System.Drawing.Size(1, 22)
        Me.Label342.TabIndex = 11
        Me.Label342.Text = "label3"
        '
        'Label343
        '
        Me.Label343.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label343.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label343.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label343.Location = New System.Drawing.Point(0, 1)
        Me.Label343.Name = "Label343"
        Me.Label343.Size = New System.Drawing.Size(1, 22)
        Me.Label343.TabIndex = 12
        Me.Label343.Text = "label4"
        '
        'Label344
        '
        Me.Label344.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label344.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label344.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label344.Location = New System.Drawing.Point(0, 0)
        Me.Label344.Name = "Label344"
        Me.Label344.Size = New System.Drawing.Size(1077, 1)
        Me.Label344.TabIndex = 10
        Me.Label344.Text = "label1"
        '
        'Label345
        '
        Me.Label345.BackColor = System.Drawing.Color.Transparent
        Me.Label345.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label345.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label345.ForeColor = System.Drawing.Color.White
        Me.Label345.Image = CType(resources.GetObject("Label345.Image"),System.Drawing.Image)
        Me.Label345.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label345.Location = New System.Drawing.Point(0, 0)
        Me.Label345.Name = "Label345"
        Me.Label345.Size = New System.Drawing.Size(1077, 23)
        Me.Label345.TabIndex = 9
        Me.Label345.Text = "      Selected Insurance Plan"
        Me.Label345.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label346
        '
        Me.Label346.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label346.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label346.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label346.Location = New System.Drawing.Point(0, 23)
        Me.Label346.Name = "Label346"
        Me.Label346.Size = New System.Drawing.Size(1077, 1)
        Me.Label346.TabIndex = 13
        Me.Label346.Text = "label2"
        '
        'Splitter5
        '
        Me.Splitter5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter5.Location = New System.Drawing.Point(0, 446)
        Me.Splitter5.Name = "Splitter5"
        Me.Splitter5.Size = New System.Drawing.Size(1077, 3)
        Me.Splitter5.TabIndex = 22
        Me.Splitter5.TabStop = false
        '
        'pnlInternalToolStripInsurance_Ex
        '
        Me.pnlInternalToolStripInsurance_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripInsurance_Ex.Controls.Add(Me.Panel60)
        Me.pnlInternalToolStripInsurance_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripInsurance_Ex.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripInsurance_Ex.Name = "pnlInternalToolStripInsurance_Ex"
        Me.pnlInternalToolStripInsurance_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripInsurance_Ex.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripInsurance_Ex.TabIndex = 54
        '
        'Panel60
        '
        Me.Panel60.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel60.Controls.Add(Me.ToolStrip14)
        Me.Panel60.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel60.Location = New System.Drawing.Point(0, 0)
        Me.Panel60.Name = "Panel60"
        Me.Panel60.Size = New System.Drawing.Size(1077, 54)
        Me.Panel60.TabIndex = 4
        '
        'ToolStrip14
        '
        Me.ToolStrip14.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip14.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip14.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip14.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveInsurance_Ex, Me.tsBtn_CancelInsurance_Ex})
        Me.ToolStrip14.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip14.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip14.Name = "ToolStrip14"
        Me.ToolStrip14.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip14.TabIndex = 0
        Me.ToolStrip14.Text = "ToolStrip14"
        '
        'tsBtn_SaveInsurance_Ex
        '
        Me.tsBtn_SaveInsurance_Ex.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveInsurance_Ex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveInsurance_Ex.Name = "tsBtn_SaveInsurance_Ex"
        Me.tsBtn_SaveInsurance_Ex.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveInsurance_Ex.Tag = "Save"
        Me.tsBtn_SaveInsurance_Ex.Text = "&Done"
        Me.tsBtn_SaveInsurance_Ex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveInsurance_Ex.ToolTipText = "Done"
        '
        'tsBtn_CancelInsurance_Ex
        '
        Me.tsBtn_CancelInsurance_Ex.Image = CType(resources.GetObject("tsBtn_CancelInsurance_Ex.Image"),System.Drawing.Image)
        Me.tsBtn_CancelInsurance_Ex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelInsurance_Ex.Name = "tsBtn_CancelInsurance_Ex"
        Me.tsBtn_CancelInsurance_Ex.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelInsurance_Ex.Tag = "Cancel"
        Me.tsBtn_CancelInsurance_Ex.Text = "&Cancel"
        Me.tsBtn_CancelInsurance_Ex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelInsurance_Ex.Visible = false
        '
        'pnlExceptionsCPT
        '
        Me.pnlExceptionsCPT.Controls.Add(Me.pnlSelectedCPTs_Ex)
        Me.pnlExceptionsCPT.Controls.Add(Me.pnlSelectedCPTsLabels_Ex)
        Me.pnlExceptionsCPT.Controls.Add(Me.Splitter8)
        Me.pnlExceptionsCPT.Controls.Add(Me.GloUC_trvCPT_Ex)
        Me.pnlExceptionsCPT.Controls.Add(Me.pnlInternalToolStripCPT_Ex)
        Me.pnlExceptionsCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExceptionsCPT.Location = New System.Drawing.Point(0, 0)
        Me.pnlExceptionsCPT.Name = "pnlExceptionsCPT"
        Me.pnlExceptionsCPT.Size = New System.Drawing.Size(1077, 694)
        Me.pnlExceptionsCPT.TabIndex = 1
        Me.pnlExceptionsCPT.Visible = false
        '
        'pnlSelectedCPTs_Ex
        '
        Me.pnlSelectedCPTs_Ex.BackColor = System.Drawing.Color.Transparent
        Me.pnlSelectedCPTs_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedCPTs_Ex.Controls.Add(Me.Label399)
        Me.pnlSelectedCPTs_Ex.Controls.Add(Me.Label400)
        Me.pnlSelectedCPTs_Ex.Controls.Add(Me.trvselectedCPT_Ex)
        Me.pnlSelectedCPTs_Ex.Controls.Add(Me.Label401)
        Me.pnlSelectedCPTs_Ex.Controls.Add(Me.Label402)
        Me.pnlSelectedCPTs_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSelectedCPTs_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlSelectedCPTs_Ex.Location = New System.Drawing.Point(0, 476)
        Me.pnlSelectedCPTs_Ex.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedCPTs_Ex.Name = "pnlSelectedCPTs_Ex"
        Me.pnlSelectedCPTs_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedCPTs_Ex.Size = New System.Drawing.Size(1077, 218)
        Me.pnlSelectedCPTs_Ex.TabIndex = 47
        '
        'Label399
        '
        Me.Label399.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label399.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label399.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label399.Location = New System.Drawing.Point(1, 214)
        Me.Label399.Name = "Label399"
        Me.Label399.Size = New System.Drawing.Size(1075, 1)
        Me.Label399.TabIndex = 8
        Me.Label399.Text = "label2"
        '
        'Label400
        '
        Me.Label400.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label400.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label400.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label400.Location = New System.Drawing.Point(0, 1)
        Me.Label400.Name = "Label400"
        Me.Label400.Size = New System.Drawing.Size(1, 214)
        Me.Label400.TabIndex = 7
        Me.Label400.Text = "label4"
        '
        'trvselectedCPT_Ex
        '
        Me.trvselectedCPT_Ex.BackColor = System.Drawing.Color.White
        Me.trvselectedCPT_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselectedCPT_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedCPT_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvselectedCPT_Ex.ForeColor = System.Drawing.Color.Black
        Me.trvselectedCPT_Ex.HideSelection = false
        Me.trvselectedCPT_Ex.ImageIndex = 0
        Me.trvselectedCPT_Ex.ImageList = Me.ImageList1
        Me.trvselectedCPT_Ex.ItemHeight = 18
        Me.trvselectedCPT_Ex.Location = New System.Drawing.Point(0, 1)
        Me.trvselectedCPT_Ex.Name = "trvselectedCPT_Ex"
        Me.trvselectedCPT_Ex.SelectedImageIndex = 0
        Me.trvselectedCPT_Ex.ShowLines = false
        Me.trvselectedCPT_Ex.Size = New System.Drawing.Size(1076, 214)
        Me.trvselectedCPT_Ex.TabIndex = 0
        '
        'Label401
        '
        Me.Label401.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label401.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label401.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label401.Location = New System.Drawing.Point(1076, 1)
        Me.Label401.Name = "Label401"
        Me.Label401.Size = New System.Drawing.Size(1, 214)
        Me.Label401.TabIndex = 6
        Me.Label401.Text = "label3"
        '
        'Label402
        '
        Me.Label402.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label402.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label402.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label402.Location = New System.Drawing.Point(0, 0)
        Me.Label402.Name = "Label402"
        Me.Label402.Size = New System.Drawing.Size(1077, 1)
        Me.Label402.TabIndex = 5
        Me.Label402.Text = "label1"
        '
        'pnlSelectedCPTsLabels_Ex
        '
        Me.pnlSelectedCPTsLabels_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedCPTsLabels_Ex.Controls.Add(Me.Panel97)
        Me.pnlSelectedCPTsLabels_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectedCPTsLabels_Ex.Location = New System.Drawing.Point(0, 449)
        Me.pnlSelectedCPTsLabels_Ex.Name = "pnlSelectedCPTsLabels_Ex"
        Me.pnlSelectedCPTsLabels_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedCPTsLabels_Ex.Size = New System.Drawing.Size(1077, 27)
        Me.pnlSelectedCPTsLabels_Ex.TabIndex = 48
        '
        'Panel97
        '
        Me.Panel97.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel97.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel97.Controls.Add(Me.Label403)
        Me.Panel97.Controls.Add(Me.Label404)
        Me.Panel97.Controls.Add(Me.Label405)
        Me.Panel97.Controls.Add(Me.Label406)
        Me.Panel97.Controls.Add(Me.Label407)
        Me.Panel97.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel97.Location = New System.Drawing.Point(0, 0)
        Me.Panel97.Name = "Panel97"
        Me.Panel97.Size = New System.Drawing.Size(1077, 24)
        Me.Panel97.TabIndex = 14
        '
        'Label403
        '
        Me.Label403.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label403.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label403.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label403.Location = New System.Drawing.Point(1076, 1)
        Me.Label403.Name = "Label403"
        Me.Label403.Size = New System.Drawing.Size(1, 22)
        Me.Label403.TabIndex = 11
        Me.Label403.Text = "label3"
        '
        'Label404
        '
        Me.Label404.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label404.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label404.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label404.Location = New System.Drawing.Point(0, 1)
        Me.Label404.Name = "Label404"
        Me.Label404.Size = New System.Drawing.Size(1, 22)
        Me.Label404.TabIndex = 12
        Me.Label404.Text = "label4"
        '
        'Label405
        '
        Me.Label405.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label405.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label405.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label405.Location = New System.Drawing.Point(0, 0)
        Me.Label405.Name = "Label405"
        Me.Label405.Size = New System.Drawing.Size(1077, 1)
        Me.Label405.TabIndex = 10
        Me.Label405.Text = "label1"
        '
        'Label406
        '
        Me.Label406.BackColor = System.Drawing.Color.Transparent
        Me.Label406.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label406.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label406.ForeColor = System.Drawing.Color.White
        Me.Label406.Image = CType(resources.GetObject("Label406.Image"),System.Drawing.Image)
        Me.Label406.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label406.Location = New System.Drawing.Point(0, 0)
        Me.Label406.Name = "Label406"
        Me.Label406.Size = New System.Drawing.Size(1077, 23)
        Me.Label406.TabIndex = 9
        Me.Label406.Text = "      Selected CPT"
        Me.Label406.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label407
        '
        Me.Label407.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label407.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label407.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label407.Location = New System.Drawing.Point(0, 23)
        Me.Label407.Name = "Label407"
        Me.Label407.Size = New System.Drawing.Size(1077, 1)
        Me.Label407.TabIndex = 13
        Me.Label407.Text = "label2"
        '
        'Splitter8
        '
        Me.Splitter8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter8.Location = New System.Drawing.Point(0, 446)
        Me.Splitter8.Name = "Splitter8"
        Me.Splitter8.Size = New System.Drawing.Size(1077, 3)
        Me.Splitter8.TabIndex = 49
        Me.Splitter8.TabStop = false
        '
        'pnlInternalToolStripCPT_Ex
        '
        Me.pnlInternalToolStripCPT_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripCPT_Ex.Controls.Add(Me.Panel99)
        Me.pnlInternalToolStripCPT_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripCPT_Ex.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripCPT_Ex.Name = "pnlInternalToolStripCPT_Ex"
        Me.pnlInternalToolStripCPT_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripCPT_Ex.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripCPT_Ex.TabIndex = 53
        '
        'Panel99
        '
        Me.Panel99.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel99.Controls.Add(Me.ToolStrip10)
        Me.Panel99.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel99.Location = New System.Drawing.Point(0, 0)
        Me.Panel99.Name = "Panel99"
        Me.Panel99.Size = New System.Drawing.Size(1077, 54)
        Me.Panel99.TabIndex = 4
        '
        'ToolStrip10
        '
        Me.ToolStrip10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip10.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip10.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip10.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveCPT_Ex, Me.ToolStripButton8})
        Me.ToolStrip10.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip10.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip10.Name = "ToolStrip10"
        Me.ToolStrip10.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip10.TabIndex = 0
        Me.ToolStrip10.Text = "ToolStrip10"
        '
        'tsBtn_SaveCPT_Ex
        '
        Me.tsBtn_SaveCPT_Ex.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveCPT_Ex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveCPT_Ex.Name = "tsBtn_SaveCPT_Ex"
        Me.tsBtn_SaveCPT_Ex.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveCPT_Ex.Tag = "Save"
        Me.tsBtn_SaveCPT_Ex.Text = "&Done"
        Me.tsBtn_SaveCPT_Ex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveCPT_Ex.ToolTipText = "Done"
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"),System.Drawing.Image)
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton8.Tag = "Cancel"
        Me.ToolStripButton8.Text = "&Cancel"
        Me.ToolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton8.Visible = false
        '
        'pnlExceptionsDrugs
        '
        Me.pnlExceptionsDrugs.Controls.Add(Me.pnltrvSelectedDrugs_Ex)
        Me.pnlExceptionsDrugs.Controls.Add(Me.pnlSelectedDrugLabel_Ex)
        Me.pnlExceptionsDrugs.Controls.Add(Me.Splitter9)
        Me.pnlExceptionsDrugs.Controls.Add(Me.GloUC_trvDrugs_Ex)
        Me.pnlExceptionsDrugs.Controls.Add(Me.pnlInternalToolStripDrugs_Ex)
        Me.pnlExceptionsDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExceptionsDrugs.Location = New System.Drawing.Point(0, 0)
        Me.pnlExceptionsDrugs.Name = "pnlExceptionsDrugs"
        Me.pnlExceptionsDrugs.Size = New System.Drawing.Size(1077, 694)
        Me.pnlExceptionsDrugs.TabIndex = 1
        Me.pnlExceptionsDrugs.Visible = false
        '
        'pnltrvSelectedDrugs_Ex
        '
        Me.pnltrvSelectedDrugs_Ex.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvSelectedDrugs_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvSelectedDrugs_Ex.Controls.Add(Me.Label408)
        Me.pnltrvSelectedDrugs_Ex.Controls.Add(Me.Label409)
        Me.pnltrvSelectedDrugs_Ex.Controls.Add(Me.trvSelectedDrugs_Ex)
        Me.pnltrvSelectedDrugs_Ex.Controls.Add(Me.Label410)
        Me.pnltrvSelectedDrugs_Ex.Controls.Add(Me.Label411)
        Me.pnltrvSelectedDrugs_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvSelectedDrugs_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnltrvSelectedDrugs_Ex.Location = New System.Drawing.Point(0, 476)
        Me.pnltrvSelectedDrugs_Ex.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedDrugs_Ex.Name = "pnltrvSelectedDrugs_Ex"
        Me.pnltrvSelectedDrugs_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedDrugs_Ex.Size = New System.Drawing.Size(1077, 218)
        Me.pnltrvSelectedDrugs_Ex.TabIndex = 21
        '
        'Label408
        '
        Me.Label408.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label408.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label408.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label408.Location = New System.Drawing.Point(1, 214)
        Me.Label408.Name = "Label408"
        Me.Label408.Size = New System.Drawing.Size(1075, 1)
        Me.Label408.TabIndex = 8
        Me.Label408.Text = "label2"
        '
        'Label409
        '
        Me.Label409.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label409.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label409.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label409.Location = New System.Drawing.Point(0, 1)
        Me.Label409.Name = "Label409"
        Me.Label409.Size = New System.Drawing.Size(1, 214)
        Me.Label409.TabIndex = 7
        Me.Label409.Text = "label4"
        '
        'trvSelectedDrugs_Ex
        '
        Me.trvSelectedDrugs_Ex.BackColor = System.Drawing.Color.White
        Me.trvSelectedDrugs_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedDrugs_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedDrugs_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvSelectedDrugs_Ex.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedDrugs_Ex.HideSelection = false
        Me.trvSelectedDrugs_Ex.ImageIndex = 0
        Me.trvSelectedDrugs_Ex.ImageList = Me.ImageList1
        Me.trvSelectedDrugs_Ex.ItemHeight = 18
        Me.trvSelectedDrugs_Ex.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedDrugs_Ex.Name = "trvSelectedDrugs_Ex"
        Me.trvSelectedDrugs_Ex.SelectedImageIndex = 0
        Me.trvSelectedDrugs_Ex.ShowLines = false
        Me.trvSelectedDrugs_Ex.Size = New System.Drawing.Size(1076, 214)
        Me.trvSelectedDrugs_Ex.TabIndex = 0
        '
        'Label410
        '
        Me.Label410.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label410.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label410.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label410.Location = New System.Drawing.Point(1076, 1)
        Me.Label410.Name = "Label410"
        Me.Label410.Size = New System.Drawing.Size(1, 214)
        Me.Label410.TabIndex = 6
        Me.Label410.Text = "label3"
        '
        'Label411
        '
        Me.Label411.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label411.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label411.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label411.Location = New System.Drawing.Point(0, 0)
        Me.Label411.Name = "Label411"
        Me.Label411.Size = New System.Drawing.Size(1077, 1)
        Me.Label411.TabIndex = 5
        Me.Label411.Text = "label1"
        '
        'pnlSelectedDrugLabel_Ex
        '
        Me.pnlSelectedDrugLabel_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSelectedDrugLabel_Ex.Controls.Add(Me.Panel103)
        Me.pnlSelectedDrugLabel_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectedDrugLabel_Ex.Location = New System.Drawing.Point(0, 449)
        Me.pnlSelectedDrugLabel_Ex.Name = "pnlSelectedDrugLabel_Ex"
        Me.pnlSelectedDrugLabel_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedDrugLabel_Ex.Size = New System.Drawing.Size(1077, 27)
        Me.pnlSelectedDrugLabel_Ex.TabIndex = 20
        '
        'Panel103
        '
        Me.Panel103.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel103.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel103.Controls.Add(Me.Label412)
        Me.Panel103.Controls.Add(Me.Label413)
        Me.Panel103.Controls.Add(Me.Label414)
        Me.Panel103.Controls.Add(Me.Label415)
        Me.Panel103.Controls.Add(Me.Label416)
        Me.Panel103.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel103.Location = New System.Drawing.Point(0, 0)
        Me.Panel103.Name = "Panel103"
        Me.Panel103.Size = New System.Drawing.Size(1077, 24)
        Me.Panel103.TabIndex = 14
        '
        'Label412
        '
        Me.Label412.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label412.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label412.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label412.Location = New System.Drawing.Point(1076, 1)
        Me.Label412.Name = "Label412"
        Me.Label412.Size = New System.Drawing.Size(1, 22)
        Me.Label412.TabIndex = 11
        Me.Label412.Text = "label3"
        '
        'Label413
        '
        Me.Label413.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label413.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label413.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label413.Location = New System.Drawing.Point(0, 1)
        Me.Label413.Name = "Label413"
        Me.Label413.Size = New System.Drawing.Size(1, 22)
        Me.Label413.TabIndex = 12
        Me.Label413.Text = "label4"
        '
        'Label414
        '
        Me.Label414.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label414.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label414.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label414.Location = New System.Drawing.Point(0, 0)
        Me.Label414.Name = "Label414"
        Me.Label414.Size = New System.Drawing.Size(1077, 1)
        Me.Label414.TabIndex = 10
        Me.Label414.Text = "label1"
        '
        'Label415
        '
        Me.Label415.BackColor = System.Drawing.Color.Transparent
        Me.Label415.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label415.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label415.ForeColor = System.Drawing.Color.White
        Me.Label415.Image = CType(resources.GetObject("Label415.Image"),System.Drawing.Image)
        Me.Label415.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label415.Location = New System.Drawing.Point(0, 0)
        Me.Label415.Name = "Label415"
        Me.Label415.Size = New System.Drawing.Size(1077, 23)
        Me.Label415.TabIndex = 9
        Me.Label415.Text = "      Selected Drugs"
        Me.Label415.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label416
        '
        Me.Label416.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label416.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label416.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label416.Location = New System.Drawing.Point(0, 23)
        Me.Label416.Name = "Label416"
        Me.Label416.Size = New System.Drawing.Size(1077, 1)
        Me.Label416.TabIndex = 13
        Me.Label416.Text = "label2"
        '
        'Splitter9
        '
        Me.Splitter9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter9.Location = New System.Drawing.Point(0, 446)
        Me.Splitter9.Name = "Splitter9"
        Me.Splitter9.Size = New System.Drawing.Size(1077, 3)
        Me.Splitter9.TabIndex = 22
        Me.Splitter9.TabStop = false
        '
        'pnlInternalToolStripDrugs_Ex
        '
        Me.pnlInternalToolStripDrugs_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripDrugs_Ex.Controls.Add(Me.Panel105)
        Me.pnlInternalToolStripDrugs_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripDrugs_Ex.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripDrugs_Ex.Name = "pnlInternalToolStripDrugs_Ex"
        Me.pnlInternalToolStripDrugs_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripDrugs_Ex.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripDrugs_Ex.TabIndex = 54
        '
        'Panel105
        '
        Me.Panel105.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel105.Controls.Add(Me.ToolStrip11)
        Me.Panel105.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel105.Location = New System.Drawing.Point(0, 0)
        Me.Panel105.Name = "Panel105"
        Me.Panel105.Size = New System.Drawing.Size(1077, 54)
        Me.Panel105.TabIndex = 4
        '
        'ToolStrip11
        '
        Me.ToolStrip11.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip11.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip11.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip11.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveDrugs_Ex, Me.ToolStripButton10})
        Me.ToolStrip11.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip11.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip11.Name = "ToolStrip11"
        Me.ToolStrip11.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip11.TabIndex = 0
        Me.ToolStrip11.Text = "ToolStrip11"
        '
        'tsBtn_SaveDrugs_Ex
        '
        Me.tsBtn_SaveDrugs_Ex.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveDrugs_Ex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveDrugs_Ex.Name = "tsBtn_SaveDrugs_Ex"
        Me.tsBtn_SaveDrugs_Ex.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveDrugs_Ex.Tag = "Save"
        Me.tsBtn_SaveDrugs_Ex.Text = "&Done"
        Me.tsBtn_SaveDrugs_Ex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveDrugs_Ex.ToolTipText = "Done"
        '
        'ToolStripButton10
        '
        Me.ToolStripButton10.Image = CType(resources.GetObject("ToolStripButton10.Image"),System.Drawing.Image)
        Me.ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton10.Name = "ToolStripButton10"
        Me.ToolStripButton10.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton10.Tag = "Cancel"
        Me.ToolStripButton10.Text = "&Cancel"
        Me.ToolStripButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton10.Visible = false
        '
        'pnlExceptionsLab
        '
        Me.pnlExceptionsLab.Controls.Add(Me.Panel112_Ex)
        Me.pnlExceptionsLab.Controls.Add(Me.Panel13_Ex)
        Me.pnlExceptionsLab.Controls.Add(Me.pnlInternalToolStripLab_Ex)
        Me.pnlExceptionsLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExceptionsLab.Location = New System.Drawing.Point(0, 0)
        Me.pnlExceptionsLab.Name = "pnlExceptionsLab"
        Me.pnlExceptionsLab.Size = New System.Drawing.Size(1077, 694)
        Me.pnlExceptionsLab.TabIndex = 2
        Me.pnlExceptionsLab.Visible = false
        '
        'Panel112_Ex
        '
        Me.Panel112_Ex.BackColor = System.Drawing.Color.Transparent
        Me.Panel112_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel112_Ex.Controls.Add(Me.Label417)
        Me.Panel112_Ex.Controls.Add(Me.C1LabResult_Ex)
        Me.Panel112_Ex.Controls.Add(Me.Label418)
        Me.Panel112_Ex.Controls.Add(Me.Label419)
        Me.Panel112_Ex.Controls.Add(Me.Label420)
        Me.Panel112_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel112_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel112_Ex.Location = New System.Drawing.Point(0, 80)
        Me.Panel112_Ex.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel112_Ex.Name = "Panel112_Ex"
        Me.Panel112_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel112_Ex.Size = New System.Drawing.Size(1077, 614)
        Me.Panel112_Ex.TabIndex = 20
        '
        'Label417
        '
        Me.Label417.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label417.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label417.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label417.Location = New System.Drawing.Point(1, 610)
        Me.Label417.Name = "Label417"
        Me.Label417.Size = New System.Drawing.Size(1075, 1)
        Me.Label417.TabIndex = 8
        Me.Label417.Text = "label2"
        '
        'C1LabResult_Ex
        '
        Me.C1LabResult_Ex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1LabResult_Ex.BackColor = System.Drawing.Color.FromArgb(CType(CType(240,Byte),Integer), CType(CType(247,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.C1LabResult_Ex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1LabResult_Ex.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1LabResult_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1LabResult_Ex.ExtendLastCol = true
        Me.C1LabResult_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.C1LabResult_Ex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.C1LabResult_Ex.Location = New System.Drawing.Point(1, 1)
        Me.C1LabResult_Ex.Name = "C1LabResult_Ex"
        Me.C1LabResult_Ex.Rows.DefaultSize = 19
        Me.C1LabResult_Ex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1LabResult_Ex.ShowCellLabels = true
        Me.C1LabResult_Ex.Size = New System.Drawing.Size(1075, 610)
        Me.C1LabResult_Ex.StyleInfo = resources.GetString("C1LabResult_Ex.StyleInfo")
        Me.C1LabResult_Ex.TabIndex = 1
        Me.C1LabResult_Ex.Tree.NodeImageCollapsed = CType(resources.GetObject("C1LabResult_Ex.Tree.NodeImageCollapsed"),System.Drawing.Image)
        Me.C1LabResult_Ex.Tree.NodeImageExpanded = CType(resources.GetObject("C1LabResult_Ex.Tree.NodeImageExpanded"),System.Drawing.Image)
        '
        'Label418
        '
        Me.Label418.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label418.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label418.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label418.Location = New System.Drawing.Point(0, 1)
        Me.Label418.Name = "Label418"
        Me.Label418.Size = New System.Drawing.Size(1, 610)
        Me.Label418.TabIndex = 7
        Me.Label418.Text = "label4"
        '
        'Label419
        '
        Me.Label419.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label419.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label419.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label419.Location = New System.Drawing.Point(1076, 1)
        Me.Label419.Name = "Label419"
        Me.Label419.Size = New System.Drawing.Size(1, 610)
        Me.Label419.TabIndex = 6
        Me.Label419.Text = "label3"
        '
        'Label420
        '
        Me.Label420.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label420.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label420.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label420.Location = New System.Drawing.Point(0, 0)
        Me.Label420.Name = "Label420"
        Me.Label420.Size = New System.Drawing.Size(1077, 1)
        Me.Label420.TabIndex = 5
        Me.Label420.Text = "label1"
        '
        'Panel13_Ex
        '
        Me.Panel13_Ex.Controls.Add(Me.Panel23_Ex)
        Me.Panel13_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13_Ex.Location = New System.Drawing.Point(0, 54)
        Me.Panel13_Ex.Name = "Panel13_Ex"
        Me.Panel13_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel13_Ex.Size = New System.Drawing.Size(1077, 26)
        Me.Panel13_Ex.TabIndex = 21
        '
        'Panel23_Ex
        '
        Me.Panel23_Ex.BackColor = System.Drawing.Color.Transparent
        Me.Panel23_Ex.Controls.Add(Me.txtLabResultSearch_Ex)
        Me.Panel23_Ex.Controls.Add(Me.btnLabResultClear_Ex)
        Me.Panel23_Ex.Controls.Add(Me.Label421)
        Me.Panel23_Ex.Controls.Add(Me.Label422)
        Me.Panel23_Ex.Controls.Add(Me.PictureBox7)
        Me.Panel23_Ex.Controls.Add(Me.Label423)
        Me.Panel23_Ex.Controls.Add(Me.Label424)
        Me.Panel23_Ex.Controls.Add(Me.Label425)
        Me.Panel23_Ex.Controls.Add(Me.Label426)
        Me.Panel23_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel23_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel23_Ex.ForeColor = System.Drawing.Color.Black
        Me.Panel23_Ex.Location = New System.Drawing.Point(0, 0)
        Me.Panel23_Ex.Name = "Panel23_Ex"
        Me.Panel23_Ex.Size = New System.Drawing.Size(1077, 23)
        Me.Panel23_Ex.TabIndex = 21
        '
        'txtLabResultSearch_Ex
        '
        Me.txtLabResultSearch_Ex.BackColor = System.Drawing.Color.White
        Me.txtLabResultSearch_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabResultSearch_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLabResultSearch_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtLabResultSearch_Ex.ForeColor = System.Drawing.Color.Black
        Me.txtLabResultSearch_Ex.Location = New System.Drawing.Point(29, 5)
        Me.txtLabResultSearch_Ex.Name = "txtLabResultSearch_Ex"
        Me.txtLabResultSearch_Ex.Size = New System.Drawing.Size(1026, 15)
        Me.txtLabResultSearch_Ex.TabIndex = 0
        '
        'btnLabResultClear_Ex
        '
        Me.btnLabResultClear_Ex.BackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear_Ex.BackgroundImage = CType(resources.GetObject("btnLabResultClear_Ex.BackgroundImage"),System.Drawing.Image)
        Me.btnLabResultClear_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabResultClear_Ex.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLabResultClear_Ex.FlatAppearance.BorderSize = 0
        Me.btnLabResultClear_Ex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear_Ex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear_Ex.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabResultClear_Ex.Image = CType(resources.GetObject("btnLabResultClear_Ex.Image"),System.Drawing.Image)
        Me.btnLabResultClear_Ex.Location = New System.Drawing.Point(1055, 5)
        Me.btnLabResultClear_Ex.Name = "btnLabResultClear_Ex"
        Me.btnLabResultClear_Ex.Size = New System.Drawing.Size(21, 15)
        Me.btnLabResultClear_Ex.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.btnLabResultClear_Ex, "Clear Search")
        Me.btnLabResultClear_Ex.UseVisualStyleBackColor = false
        '
        'Label421
        '
        Me.Label421.BackColor = System.Drawing.Color.White
        Me.Label421.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label421.Location = New System.Drawing.Point(29, 1)
        Me.Label421.Name = "Label421"
        Me.Label421.Size = New System.Drawing.Size(1047, 4)
        Me.Label421.TabIndex = 37
        '
        'Label422
        '
        Me.Label422.BackColor = System.Drawing.Color.White
        Me.Label422.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label422.Location = New System.Drawing.Point(29, 20)
        Me.Label422.Name = "Label422"
        Me.Label422.Size = New System.Drawing.Size(1047, 2)
        Me.Label422.TabIndex = 38
        '
        'PictureBox7
        '
        Me.PictureBox7.BackColor = System.Drawing.Color.White
        Me.PictureBox7.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox7.Image = CType(resources.GetObject("PictureBox7.Image"),System.Drawing.Image)
        Me.PictureBox7.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox7.TabIndex = 9
        Me.PictureBox7.TabStop = false
        '
        'Label423
        '
        Me.Label423.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label423.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label423.Location = New System.Drawing.Point(1, 22)
        Me.Label423.Name = "Label423"
        Me.Label423.Size = New System.Drawing.Size(1075, 1)
        Me.Label423.TabIndex = 35
        Me.Label423.Text = "label1"
        '
        'Label424
        '
        Me.Label424.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label424.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label424.Location = New System.Drawing.Point(1, 0)
        Me.Label424.Name = "Label424"
        Me.Label424.Size = New System.Drawing.Size(1075, 1)
        Me.Label424.TabIndex = 36
        Me.Label424.Text = "label1"
        '
        'Label425
        '
        Me.Label425.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label425.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label425.Location = New System.Drawing.Point(0, 0)
        Me.Label425.Name = "Label425"
        Me.Label425.Size = New System.Drawing.Size(1, 23)
        Me.Label425.TabIndex = 39
        Me.Label425.Text = "label4"
        '
        'Label426
        '
        Me.Label426.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label426.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label426.Location = New System.Drawing.Point(1076, 0)
        Me.Label426.Name = "Label426"
        Me.Label426.Size = New System.Drawing.Size(1, 23)
        Me.Label426.TabIndex = 40
        Me.Label426.Text = "label4"
        '
        'pnlInternalToolStripLab_Ex
        '
        Me.pnlInternalToolStripLab_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripLab_Ex.Controls.Add(Me.Panel111)
        Me.pnlInternalToolStripLab_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripLab_Ex.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripLab_Ex.Name = "pnlInternalToolStripLab_Ex"
        Me.pnlInternalToolStripLab_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripLab_Ex.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripLab_Ex.TabIndex = 55
        '
        'Panel111
        '
        Me.Panel111.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel111.Controls.Add(Me.ToolStrip12)
        Me.Panel111.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel111.Location = New System.Drawing.Point(0, 0)
        Me.Panel111.Name = "Panel111"
        Me.Panel111.Size = New System.Drawing.Size(1077, 54)
        Me.Panel111.TabIndex = 4
        '
        'ToolStrip12
        '
        Me.ToolStrip12.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip12.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip12.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip12.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveLab_Ex, Me.ToolStripButton12})
        Me.ToolStrip12.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip12.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip12.Name = "ToolStrip12"
        Me.ToolStrip12.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip12.TabIndex = 0
        Me.ToolStrip12.Text = "ToolStrip12"
        '
        'tsBtn_SaveLab_Ex
        '
        Me.tsBtn_SaveLab_Ex.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveLab_Ex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveLab_Ex.Name = "tsBtn_SaveLab_Ex"
        Me.tsBtn_SaveLab_Ex.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveLab_Ex.Tag = "Save"
        Me.tsBtn_SaveLab_Ex.Text = "&Done"
        Me.tsBtn_SaveLab_Ex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveLab_Ex.ToolTipText = "Done"
        '
        'ToolStripButton12
        '
        Me.ToolStripButton12.Image = CType(resources.GetObject("ToolStripButton12.Image"),System.Drawing.Image)
        Me.ToolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton12.Name = "ToolStripButton12"
        Me.ToolStripButton12.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton12.Tag = "Cancel"
        Me.ToolStripButton12.Text = "&Cancel"
        Me.ToolStripButton12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton12.Visible = false
        '
        'pnlExceptionsICD9
        '
        Me.pnlExceptionsICD9.Controls.Add(Me.Panel15_Ex)
        Me.pnlExceptionsICD9.Controls.Add(Me.Panel5_Ex)
        Me.pnlExceptionsICD9.Controls.Add(Me.Splitter7)
        Me.pnlExceptionsICD9.Controls.Add(Me.GloUC_trvICD10_Ex)
        Me.pnlExceptionsICD9.Controls.Add(Me.GloUC_trvICD9_Ex)
        Me.pnlExceptionsICD9.Controls.Add(Me.pnlInternalToolStripICD_Ex)
        Me.pnlExceptionsICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlExceptionsICD9.Location = New System.Drawing.Point(0, 0)
        Me.pnlExceptionsICD9.Name = "pnlExceptionsICD9"
        Me.pnlExceptionsICD9.Size = New System.Drawing.Size(1077, 694)
        Me.pnlExceptionsICD9.TabIndex = 1
        Me.pnlExceptionsICD9.Visible = false
        '
        'Panel15_Ex
        '
        Me.Panel15_Ex.BackColor = System.Drawing.Color.Transparent
        Me.Panel15_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel15_Ex.Controls.Add(Me.Label390)
        Me.Panel15_Ex.Controls.Add(Me.Label391)
        Me.Panel15_Ex.Controls.Add(Me.trvselectedICD10s_Ex)
        Me.Panel15_Ex.Controls.Add(Me.trvselectedICDs_Ex)
        Me.Panel15_Ex.Controls.Add(Me.Label392)
        Me.Panel15_Ex.Controls.Add(Me.Label393)
        Me.Panel15_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Panel15_Ex.Location = New System.Drawing.Point(0, 864)
        Me.Panel15_Ex.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel15_Ex.Name = "Panel15_Ex"
        Me.Panel15_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel15_Ex.Size = New System.Drawing.Size(1077, 0)
        Me.Panel15_Ex.TabIndex = 50
        '
        'Label390
        '
        Me.Label390.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label390.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label390.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label390.Location = New System.Drawing.Point(1, -4)
        Me.Label390.Name = "Label390"
        Me.Label390.Size = New System.Drawing.Size(1075, 1)
        Me.Label390.TabIndex = 8
        Me.Label390.Text = "label2"
        '
        'Label391
        '
        Me.Label391.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label391.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label391.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label391.Location = New System.Drawing.Point(0, 1)
        Me.Label391.Name = "Label391"
        Me.Label391.Size = New System.Drawing.Size(1, 0)
        Me.Label391.TabIndex = 7
        Me.Label391.Text = "label4"
        '
        'trvselectedICD10s_Ex
        '
        Me.trvselectedICD10s_Ex.BackColor = System.Drawing.Color.White
        Me.trvselectedICD10s_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselectedICD10s_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedICD10s_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvselectedICD10s_Ex.ForeColor = System.Drawing.Color.Black
        Me.trvselectedICD10s_Ex.HideSelection = false
        Me.trvselectedICD10s_Ex.ImageIndex = 0
        Me.trvselectedICD10s_Ex.ImageList = Me.ImageList1
        Me.trvselectedICD10s_Ex.ItemHeight = 18
        Me.trvselectedICD10s_Ex.Location = New System.Drawing.Point(0, 1)
        Me.trvselectedICD10s_Ex.Name = "trvselectedICD10s_Ex"
        Me.trvselectedICD10s_Ex.SelectedImageIndex = 0
        Me.trvselectedICD10s_Ex.ShowLines = false
        Me.trvselectedICD10s_Ex.Size = New System.Drawing.Size(1076, 0)
        Me.trvselectedICD10s_Ex.TabIndex = 9
        '
        'trvselectedICDs_Ex
        '
        Me.trvselectedICDs_Ex.BackColor = System.Drawing.Color.White
        Me.trvselectedICDs_Ex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselectedICDs_Ex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedICDs_Ex.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trvselectedICDs_Ex.ForeColor = System.Drawing.Color.Black
        Me.trvselectedICDs_Ex.HideSelection = false
        Me.trvselectedICDs_Ex.ImageIndex = 0
        Me.trvselectedICDs_Ex.ImageList = Me.ImageList1
        Me.trvselectedICDs_Ex.ItemHeight = 18
        Me.trvselectedICDs_Ex.Location = New System.Drawing.Point(0, 1)
        Me.trvselectedICDs_Ex.Name = "trvselectedICDs_Ex"
        Me.trvselectedICDs_Ex.SelectedImageIndex = 0
        Me.trvselectedICDs_Ex.ShowLines = false
        Me.trvselectedICDs_Ex.Size = New System.Drawing.Size(1076, 0)
        Me.trvselectedICDs_Ex.TabIndex = 0
        '
        'Label392
        '
        Me.Label392.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label392.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label392.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label392.Location = New System.Drawing.Point(1076, 1)
        Me.Label392.Name = "Label392"
        Me.Label392.Size = New System.Drawing.Size(1, 0)
        Me.Label392.TabIndex = 6
        Me.Label392.Text = "label3"
        '
        'Label393
        '
        Me.Label393.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label393.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label393.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label393.Location = New System.Drawing.Point(0, 0)
        Me.Label393.Name = "Label393"
        Me.Label393.Size = New System.Drawing.Size(1077, 1)
        Me.Label393.TabIndex = 5
        Me.Label393.Text = "label1"
        '
        'Panel5_Ex
        '
        Me.Panel5_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5_Ex.Controls.Add(Me.Panel91)
        Me.Panel5_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5_Ex.Location = New System.Drawing.Point(0, 837)
        Me.Panel5_Ex.Name = "Panel5_Ex"
        Me.Panel5_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel5_Ex.Size = New System.Drawing.Size(1077, 27)
        Me.Panel5_Ex.TabIndex = 49
        '
        'Panel91
        '
        Me.Panel91.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel91.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel91.Controls.Add(Me.Label394)
        Me.Panel91.Controls.Add(Me.Label395)
        Me.Panel91.Controls.Add(Me.Label396)
        Me.Panel91.Controls.Add(Me.Label397)
        Me.Panel91.Controls.Add(Me.Label398)
        Me.Panel91.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel91.Location = New System.Drawing.Point(0, 0)
        Me.Panel91.Name = "Panel91"
        Me.Panel91.Size = New System.Drawing.Size(1077, 24)
        Me.Panel91.TabIndex = 14
        '
        'Label394
        '
        Me.Label394.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label394.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label394.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label394.Location = New System.Drawing.Point(1076, 1)
        Me.Label394.Name = "Label394"
        Me.Label394.Size = New System.Drawing.Size(1, 22)
        Me.Label394.TabIndex = 11
        Me.Label394.Text = "label3"
        '
        'Label395
        '
        Me.Label395.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label395.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label395.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label395.Location = New System.Drawing.Point(0, 1)
        Me.Label395.Name = "Label395"
        Me.Label395.Size = New System.Drawing.Size(1, 22)
        Me.Label395.TabIndex = 12
        Me.Label395.Text = "label4"
        '
        'Label396
        '
        Me.Label396.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label396.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label396.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label396.Location = New System.Drawing.Point(0, 0)
        Me.Label396.Name = "Label396"
        Me.Label396.Size = New System.Drawing.Size(1077, 1)
        Me.Label396.TabIndex = 10
        Me.Label396.Text = "label1"
        '
        'Label397
        '
        Me.Label397.BackColor = System.Drawing.Color.Transparent
        Me.Label397.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label397.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label397.ForeColor = System.Drawing.Color.White
        Me.Label397.Image = CType(resources.GetObject("Label397.Image"),System.Drawing.Image)
        Me.Label397.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label397.Location = New System.Drawing.Point(0, 0)
        Me.Label397.Name = "Label397"
        Me.Label397.Size = New System.Drawing.Size(1077, 23)
        Me.Label397.TabIndex = 9
        Me.Label397.Text = "      Selected ICD9"
        Me.Label397.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label398
        '
        Me.Label398.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label398.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label398.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label398.Location = New System.Drawing.Point(0, 23)
        Me.Label398.Name = "Label398"
        Me.Label398.Size = New System.Drawing.Size(1077, 1)
        Me.Label398.TabIndex = 13
        Me.Label398.Text = "label2"
        '
        'Splitter7
        '
        Me.Splitter7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter7.Location = New System.Drawing.Point(0, 834)
        Me.Splitter7.Name = "Splitter7"
        Me.Splitter7.Size = New System.Drawing.Size(1077, 3)
        Me.Splitter7.TabIndex = 51
        Me.Splitter7.TabStop = false
        '
        'pnlInternalToolStripICD_Ex
        '
        Me.pnlInternalToolStripICD_Ex.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripICD_Ex.Controls.Add(Me.Panel93)
        Me.pnlInternalToolStripICD_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripICD_Ex.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripICD_Ex.Name = "pnlInternalToolStripICD_Ex"
        Me.pnlInternalToolStripICD_Ex.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripICD_Ex.Size = New System.Drawing.Size(1077, 54)
        Me.pnlInternalToolStripICD_Ex.TabIndex = 52
        '
        'Panel93
        '
        Me.Panel93.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel93.Controls.Add(Me.ToolStrip9)
        Me.Panel93.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel93.Location = New System.Drawing.Point(0, 0)
        Me.Panel93.Name = "Panel93"
        Me.Panel93.Size = New System.Drawing.Size(1077, 54)
        Me.Panel93.TabIndex = 4
        '
        'ToolStrip9
        '
        Me.ToolStrip9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip9.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ToolStrip9.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip9.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveICD_Ex, Me.ToolStripButton6, Me.tsBtn_SaveICD10_Ex})
        Me.ToolStrip9.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip9.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip9.Name = "ToolStrip9"
        Me.ToolStrip9.Size = New System.Drawing.Size(1077, 53)
        Me.ToolStrip9.TabIndex = 0
        Me.ToolStrip9.Text = "ToolStrip9"
        '
        'tsBtn_SaveICD_Ex
        '
        Me.tsBtn_SaveICD_Ex.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveICD_Ex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveICD_Ex.Name = "tsBtn_SaveICD_Ex"
        Me.tsBtn_SaveICD_Ex.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveICD_Ex.Tag = "Save"
        Me.tsBtn_SaveICD_Ex.Text = "&Done"
        Me.tsBtn_SaveICD_Ex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveICD_Ex.ToolTipText = "Done"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"),System.Drawing.Image)
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton6.Tag = "Cancel"
        Me.ToolStripButton6.Text = "&Cancel"
        Me.ToolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton6.Visible = false
        '
        'tsBtn_SaveICD10_Ex
        '
        Me.tsBtn_SaveICD10_Ex.Image = Global.gloEMR.My.Resources.Resources.OK
        Me.tsBtn_SaveICD10_Ex.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveICD10_Ex.Name = "tsBtn_SaveICD10_Ex"
        Me.tsBtn_SaveICD10_Ex.Size = New System.Drawing.Size(43, 50)
        Me.tsBtn_SaveICD10_Ex.Text = "Done"
        Me.tsBtn_SaveICD10_Ex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tbPg_QuickOrders
        '
        Me.tbPg_QuickOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tbPg_QuickOrders.Controls.Add(Me.pnlSummaryOthers)
        Me.tbPg_QuickOrders.Controls.Add(Me.sptRight)
        Me.tbPg_QuickOrders.Controls.Add(Me.pnlRight)
        Me.tbPg_QuickOrders.Location = New System.Drawing.Point(4, 22)
        Me.tbPg_QuickOrders.Name = "tbPg_QuickOrders"
        Me.tbPg_QuickOrders.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.tbPg_QuickOrders.Size = New System.Drawing.Size(1083, 700)
        Me.tbPg_QuickOrders.TabIndex = 2
        Me.tbPg_QuickOrders.Text = "Quick Orders"
        '
        'pnlSummaryOthers
        '
        Me.pnlSummaryOthers.Controls.Add(Me.pnlGuideline)
        Me.pnlSummaryOthers.Controls.Add(Me.pnlGuidelineHeader)
        Me.pnlSummaryOthers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSummaryOthers.Location = New System.Drawing.Point(0, 3)
        Me.pnlSummaryOthers.Name = "pnlSummaryOthers"
        Me.pnlSummaryOthers.Size = New System.Drawing.Size(852, 697)
        Me.pnlSummaryOthers.TabIndex = 2
        '
        'pnlGuideline
        '
        Me.pnlGuideline.Controls.Add(Me.Label99)
        Me.pnlGuideline.Controls.Add(Me.Label100)
        Me.pnlGuideline.Controls.Add(Me.Label101)
        Me.pnlGuideline.Controls.Add(Me.Label102)
        Me.pnlGuideline.Controls.Add(Me.trOrderInfo)
        Me.pnlGuideline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGuideline.Location = New System.Drawing.Point(0, 27)
        Me.pnlGuideline.Name = "pnlGuideline"
        Me.pnlGuideline.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGuideline.Size = New System.Drawing.Size(852, 670)
        Me.pnlGuideline.TabIndex = 2
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label99.Location = New System.Drawing.Point(1, 666)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(850, 1)
        Me.Label99.TabIndex = 12
        Me.Label99.Text = "label2"
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label100.Location = New System.Drawing.Point(0, 1)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(1, 666)
        Me.Label100.TabIndex = 11
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label101.Location = New System.Drawing.Point(851, 1)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(1, 666)
        Me.Label101.TabIndex = 10
        Me.Label101.Text = "label3"
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label102.Location = New System.Drawing.Point(0, 0)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(852, 1)
        Me.Label102.TabIndex = 9
        Me.Label102.Text = "label1"
        '
        'trOrderInfo
        '
        Me.trOrderInfo.BackColor = System.Drawing.Color.White
        Me.trOrderInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trOrderInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trOrderInfo.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.trOrderInfo.ForeColor = System.Drawing.Color.Black
        Me.trOrderInfo.HideSelection = false
        Me.trOrderInfo.ImageIndex = 0
        Me.trOrderInfo.ImageList = Me.ImageList1
        Me.trOrderInfo.ItemHeight = 21
        Me.trOrderInfo.Location = New System.Drawing.Point(0, 0)
        Me.trOrderInfo.Name = "trOrderInfo"
        Me.trOrderInfo.SelectedImageIndex = 0
        Me.trOrderInfo.ShowLines = false
        Me.trOrderInfo.Size = New System.Drawing.Size(852, 667)
        Me.trOrderInfo.TabIndex = 5
        '
        'pnlGuidelineHeader
        '
        Me.pnlGuidelineHeader.Controls.Add(Me.pnl3)
        Me.pnlGuidelineHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGuidelineHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlGuidelineHeader.Name = "pnlGuidelineHeader"
        Me.pnlGuidelineHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlGuidelineHeader.Size = New System.Drawing.Size(852, 27)
        Me.pnlGuidelineHeader.TabIndex = 5
        '
        'pnl3
        '
        Me.pnl3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.pnl3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl3.Controls.Add(Me.Label95)
        Me.pnl3.Controls.Add(Me.Label96)
        Me.pnl3.Controls.Add(Me.Label97)
        Me.pnl3.Controls.Add(Me.Label98)
        Me.pnl3.Controls.Add(Me.Label6)
        Me.pnl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl3.Location = New System.Drawing.Point(0, 0)
        Me.pnl3.Name = "pnl3"
        Me.pnl3.Size = New System.Drawing.Size(852, 24)
        Me.pnl3.TabIndex = 1
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label95.Location = New System.Drawing.Point(1, 23)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(850, 1)
        Me.Label95.TabIndex = 12
        Me.Label95.Text = "label2"
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label96.Location = New System.Drawing.Point(0, 1)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 23)
        Me.Label96.TabIndex = 11
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label97.Location = New System.Drawing.Point(851, 1)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(1, 23)
        Me.Label97.TabIndex = 10
        Me.Label97.Text = "label3"
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label98.Location = New System.Drawing.Point(0, 0)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(852, 1)
        Me.Label98.TabIndex = 9
        Me.Label98.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Image = CType(resources.GetObject("Label6.Image"),System.Drawing.Image)
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(852, 24)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "       Quick Orders"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'sptRight
        '
        Me.sptRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.sptRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.sptRight.Location = New System.Drawing.Point(852, 3)
        Me.sptRight.Name = "sptRight"
        Me.sptRight.Size = New System.Drawing.Size(3, 697)
        Me.sptRight.TabIndex = 335
        Me.sptRight.TabStop = false
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.GloUC_trvAssociates)
        Me.pnlRight.Controls.Add(Me.pnltrvTriggers)
        Me.pnlRight.Controls.Add(Me.pnltxtSearchOrder)
        Me.pnlRight.Controls.Add(Me.pnlbtnLab)
        Me.pnlRight.Controls.Add(Me.pnlbtnReferrals)
        Me.pnlRight.Controls.Add(Me.pnlbtnRx)
        Me.pnlRight.Controls.Add(Me.pnlbtnRadiologyTest)
        Me.pnlRight.Controls.Add(Me.pnlbtnGuideline)
        Me.pnlRight.Controls.Add(Me.pnlIM)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(855, 3)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(228, 697)
        Me.pnlRight.TabIndex = 334
        '
        'pnltrvTriggers
        '
        Me.pnltrvTriggers.BackColor = System.Drawing.Color.Transparent
        Me.pnltrvTriggers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrvTriggers.Controls.Add(Me.Label16)
        Me.pnltrvTriggers.Controls.Add(Me.Label74)
        Me.pnltrvTriggers.Controls.Add(Me.Label75)
        Me.pnltrvTriggers.Controls.Add(Me.Label76)
        Me.pnltrvTriggers.Controls.Add(Me.Label77)
        Me.pnltrvTriggers.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnltrvTriggers.Location = New System.Drawing.Point(0, 56)
        Me.pnltrvTriggers.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrvTriggers.Name = "pnltrvTriggers"
        Me.pnltrvTriggers.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltrvTriggers.Size = New System.Drawing.Size(206, 400)
        Me.pnltrvTriggers.TabIndex = 23
        Me.pnltrvTriggers.Visible = false
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.White
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Location = New System.Drawing.Point(1, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(201, 4)
        Me.Label16.TabIndex = 38
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label74.Location = New System.Drawing.Point(1, 396)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(201, 1)
        Me.Label74.TabIndex = 8
        Me.Label74.Text = "label2"
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label75.Location = New System.Drawing.Point(0, 1)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(1, 396)
        Me.Label75.TabIndex = 7
        Me.Label75.Text = "label4"
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label76.Location = New System.Drawing.Point(202, 1)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1, 396)
        Me.Label76.TabIndex = 6
        Me.Label76.Text = "label3"
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label77.Location = New System.Drawing.Point(0, 0)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(203, 1)
        Me.Label77.TabIndex = 5
        Me.Label77.Text = "label1"
        '
        'pnltxtSearchOrder
        '
        Me.pnltxtSearchOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.pnltxtSearchOrder.Controls.Add(Me.txtSearchOrder)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label20)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label21)
        Me.pnltxtSearchOrder.Controls.Add(Me.PictureBox1)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label15)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label82)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label83)
        Me.pnltxtSearchOrder.Controls.Add(Me.Label84)
        Me.pnltxtSearchOrder.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnltxtSearchOrder.ForeColor = System.Drawing.Color.Black
        Me.pnltxtSearchOrder.Location = New System.Drawing.Point(0, 30)
        Me.pnltxtSearchOrder.Name = "pnltxtSearchOrder"
        Me.pnltxtSearchOrder.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnltxtSearchOrder.Size = New System.Drawing.Size(206, 26)
        Me.pnltxtSearchOrder.TabIndex = 16
        Me.pnltxtSearchOrder.Visible = false
        '
        'txtSearchOrder
        '
        Me.txtSearchOrder.BackColor = System.Drawing.Color.White
        Me.txtSearchOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchOrder.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtSearchOrder.ForeColor = System.Drawing.Color.Black
        Me.txtSearchOrder.Location = New System.Drawing.Point(29, 5)
        Me.txtSearchOrder.Name = "txtSearchOrder"
        Me.txtSearchOrder.Size = New System.Drawing.Size(173, 15)
        Me.txtSearchOrder.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(29, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(173, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(29, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(173, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"),System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = false
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label15.Location = New System.Drawing.Point(1, 22)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(201, 1)
        Me.Label15.TabIndex = 42
        Me.Label15.Text = "label2"
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label82.Location = New System.Drawing.Point(0, 1)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(1, 22)
        Me.Label82.TabIndex = 41
        Me.Label82.Text = "label4"
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label83.Location = New System.Drawing.Point(202, 1)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(1, 22)
        Me.Label83.TabIndex = 40
        Me.Label83.Text = "label3"
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label84.Location = New System.Drawing.Point(0, 0)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(203, 1)
        Me.Label84.TabIndex = 39
        Me.Label84.Text = "label1"
        '
        'pnlbtnLab
        '
        Me.pnlbtnLab.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnLab.Controls.Add(Me.btnLab)
        Me.pnlbtnLab.Controls.Add(Me.Label78)
        Me.pnlbtnLab.Controls.Add(Me.Label79)
        Me.pnlbtnLab.Controls.Add(Me.Label80)
        Me.pnlbtnLab.Controls.Add(Me.Label81)
        Me.pnlbtnLab.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnLab.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlbtnLab.Location = New System.Drawing.Point(0, 0)
        Me.pnlbtnLab.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnLab.Name = "pnlbtnLab"
        Me.pnlbtnLab.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnLab.Size = New System.Drawing.Size(228, 30)
        Me.pnlbtnLab.TabIndex = 24
        '
        'btnLab
        '
        Me.btnLab.BackColor = System.Drawing.Color.Transparent
        Me.btnLab.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
        Me.btnLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLab.FlatAppearance.BorderSize = 0
        Me.btnLab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLab.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnLab.Location = New System.Drawing.Point(1, 1)
        Me.btnLab.Name = "btnLab"
        Me.btnLab.Size = New System.Drawing.Size(223, 25)
        Me.btnLab.TabIndex = 0
        Me.btnLab.Tag = "Selected"
        Me.btnLab.Text = "Orders"
        Me.btnLab.UseVisualStyleBackColor = false
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label78.Location = New System.Drawing.Point(1, 26)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(223, 1)
        Me.Label78.TabIndex = 8
        Me.Label78.Text = "label2"
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label79.Location = New System.Drawing.Point(0, 1)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(1, 26)
        Me.Label79.TabIndex = 7
        Me.Label79.Text = "label4"
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label80.Location = New System.Drawing.Point(224, 1)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(1, 26)
        Me.Label80.TabIndex = 6
        Me.Label80.Text = "label3"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label81.Location = New System.Drawing.Point(0, 0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(225, 1)
        Me.Label81.TabIndex = 5
        Me.Label81.Text = "label1"
        '
        'pnlbtnReferrals
        '
        Me.pnlbtnReferrals.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnReferrals.Controls.Add(Me.btnReferrals)
        Me.pnlbtnReferrals.Controls.Add(Me.Label70)
        Me.pnlbtnReferrals.Controls.Add(Me.Label71)
        Me.pnlbtnReferrals.Controls.Add(Me.Label72)
        Me.pnlbtnReferrals.Controls.Add(Me.Label73)
        Me.pnlbtnReferrals.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnReferrals.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlbtnReferrals.Location = New System.Drawing.Point(0, 547)
        Me.pnlbtnReferrals.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnReferrals.Name = "pnlbtnReferrals"
        Me.pnlbtnReferrals.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnReferrals.Size = New System.Drawing.Size(228, 30)
        Me.pnlbtnReferrals.TabIndex = 22
        '
        'btnReferrals
        '
        Me.btnReferrals.BackgroundImage = CType(resources.GetObject("btnReferrals.BackgroundImage"),System.Drawing.Image)
        Me.btnReferrals.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReferrals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReferrals.FlatAppearance.BorderSize = 0
        Me.btnReferrals.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnReferrals.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnReferrals.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReferrals.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnReferrals.Location = New System.Drawing.Point(1, 1)
        Me.btnReferrals.Name = "btnReferrals"
        Me.btnReferrals.Size = New System.Drawing.Size(223, 25)
        Me.btnReferrals.TabIndex = 5
        Me.btnReferrals.Tag = "UnSelected"
        Me.btnReferrals.Text = "Referrals"
        Me.btnReferrals.UseVisualStyleBackColor = true
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label70.Location = New System.Drawing.Point(1, 26)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(223, 1)
        Me.Label70.TabIndex = 8
        Me.Label70.Text = "label2"
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label71.Location = New System.Drawing.Point(0, 1)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(1, 26)
        Me.Label71.TabIndex = 7
        Me.Label71.Text = "label4"
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label72.Location = New System.Drawing.Point(224, 1)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(1, 26)
        Me.Label72.TabIndex = 6
        Me.Label72.Text = "label3"
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label73.Location = New System.Drawing.Point(0, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(225, 1)
        Me.Label73.TabIndex = 5
        Me.Label73.Text = "label1"
        '
        'pnlbtnRx
        '
        Me.pnlbtnRx.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnRx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnRx.Controls.Add(Me.btnRx)
        Me.pnlbtnRx.Controls.Add(Me.Label66)
        Me.pnlbtnRx.Controls.Add(Me.Label67)
        Me.pnlbtnRx.Controls.Add(Me.Label68)
        Me.pnlbtnRx.Controls.Add(Me.Label69)
        Me.pnlbtnRx.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnRx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlbtnRx.Location = New System.Drawing.Point(0, 577)
        Me.pnlbtnRx.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnRx.Name = "pnlbtnRx"
        Me.pnlbtnRx.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnRx.Size = New System.Drawing.Size(228, 30)
        Me.pnlbtnRx.TabIndex = 21
        '
        'btnRx
        '
        Me.btnRx.BackgroundImage = CType(resources.GetObject("btnRx.BackgroundImage"),System.Drawing.Image)
        Me.btnRx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRx.FlatAppearance.BorderSize = 0
        Me.btnRx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRx.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnRx.Location = New System.Drawing.Point(1, 1)
        Me.btnRx.Name = "btnRx"
        Me.btnRx.Size = New System.Drawing.Size(223, 25)
        Me.btnRx.TabIndex = 4
        Me.btnRx.Tag = "UnSelected"
        Me.btnRx.Text = "Rx"
        Me.btnRx.UseVisualStyleBackColor = true
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label66.Location = New System.Drawing.Point(1, 26)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(223, 1)
        Me.Label66.TabIndex = 8
        Me.Label66.Text = "label2"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label67.Location = New System.Drawing.Point(0, 1)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(1, 26)
        Me.Label67.TabIndex = 7
        Me.Label67.Text = "label4"
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label68.Location = New System.Drawing.Point(224, 1)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(1, 26)
        Me.Label68.TabIndex = 6
        Me.Label68.Text = "label3"
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label69.Location = New System.Drawing.Point(0, 0)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(225, 1)
        Me.Label69.TabIndex = 5
        Me.Label69.Text = "label1"
        '
        'pnlbtnRadiologyTest
        '
        Me.pnlbtnRadiologyTest.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnRadiologyTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnRadiologyTest.Controls.Add(Me.btnRadiologyTest)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label55)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label56)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label57)
        Me.pnlbtnRadiologyTest.Controls.Add(Me.Label62)
        Me.pnlbtnRadiologyTest.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnRadiologyTest.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlbtnRadiologyTest.Location = New System.Drawing.Point(0, 607)
        Me.pnlbtnRadiologyTest.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnRadiologyTest.Name = "pnlbtnRadiologyTest"
        Me.pnlbtnRadiologyTest.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnRadiologyTest.Size = New System.Drawing.Size(228, 30)
        Me.pnlbtnRadiologyTest.TabIndex = 20
        '
        'btnRadiologyTest
        '
        Me.btnRadiologyTest.BackgroundImage = CType(resources.GetObject("btnRadiologyTest.BackgroundImage"),System.Drawing.Image)
        Me.btnRadiologyTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiologyTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRadiologyTest.FlatAppearance.BorderSize = 0
        Me.btnRadiologyTest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRadiologyTest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRadiologyTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiologyTest.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnRadiologyTest.Location = New System.Drawing.Point(1, 1)
        Me.btnRadiologyTest.Name = "btnRadiologyTest"
        Me.btnRadiologyTest.Size = New System.Drawing.Size(223, 25)
        Me.btnRadiologyTest.TabIndex = 2
        Me.btnRadiologyTest.Tag = "UnSelected"
        Me.btnRadiologyTest.Text = "Order Templates"
        Me.btnRadiologyTest.UseVisualStyleBackColor = true
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label55.Location = New System.Drawing.Point(1, 26)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(223, 1)
        Me.Label55.TabIndex = 8
        Me.Label55.Text = "label2"
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label56.Location = New System.Drawing.Point(0, 1)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1, 26)
        Me.Label56.TabIndex = 7
        Me.Label56.Text = "label4"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label57.Location = New System.Drawing.Point(224, 1)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 26)
        Me.Label57.TabIndex = 6
        Me.Label57.Text = "label3"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label62.Location = New System.Drawing.Point(0, 0)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(225, 1)
        Me.Label62.TabIndex = 5
        Me.Label62.Text = "label1"
        '
        'pnlbtnGuideline
        '
        Me.pnlbtnGuideline.BackColor = System.Drawing.Color.Transparent
        Me.pnlbtnGuideline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnGuideline.Controls.Add(Me.btnGuideline)
        Me.pnlbtnGuideline.Controls.Add(Me.Label17)
        Me.pnlbtnGuideline.Controls.Add(Me.Label18)
        Me.pnlbtnGuideline.Controls.Add(Me.Label19)
        Me.pnlbtnGuideline.Controls.Add(Me.Label22)
        Me.pnlbtnGuideline.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnGuideline.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlbtnGuideline.Location = New System.Drawing.Point(0, 637)
        Me.pnlbtnGuideline.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnGuideline.Name = "pnlbtnGuideline"
        Me.pnlbtnGuideline.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnGuideline.Size = New System.Drawing.Size(228, 30)
        Me.pnlbtnGuideline.TabIndex = 20
        '
        'btnGuideline
        '
        Me.btnGuideline.BackgroundImage = CType(resources.GetObject("btnGuideline.BackgroundImage"),System.Drawing.Image)
        Me.btnGuideline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGuideline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnGuideline.FlatAppearance.BorderSize = 0
        Me.btnGuideline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnGuideline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnGuideline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuideline.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnGuideline.Location = New System.Drawing.Point(1, 1)
        Me.btnGuideline.Name = "btnGuideline"
        Me.btnGuideline.Size = New System.Drawing.Size(223, 25)
        Me.btnGuideline.TabIndex = 1
        Me.btnGuideline.Tag = "UnSelected"
        Me.btnGuideline.Text = "Guidelines"
        Me.btnGuideline.UseVisualStyleBackColor = true
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label17.Location = New System.Drawing.Point(1, 26)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(223, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 26)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label19.Location = New System.Drawing.Point(224, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 26)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(225, 1)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "label1"
        '
        'pnlIM
        '
        Me.pnlIM.BackColor = System.Drawing.Color.Transparent
        Me.pnlIM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlIM.Controls.Add(Me.btnIM)
        Me.pnlIM.Controls.Add(Me.Label145)
        Me.pnlIM.Controls.Add(Me.Label159)
        Me.pnlIM.Controls.Add(Me.Label160)
        Me.pnlIM.Controls.Add(Me.Label161)
        Me.pnlIM.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlIM.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.pnlIM.Location = New System.Drawing.Point(0, 667)
        Me.pnlIM.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlIM.Name = "pnlIM"
        Me.pnlIM.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlIM.Size = New System.Drawing.Size(228, 30)
        Me.pnlIM.TabIndex = 27
        '
        'btnIM
        '
        Me.btnIM.BackgroundImage = CType(resources.GetObject("btnIM.BackgroundImage"),System.Drawing.Image)
        Me.btnIM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnIM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnIM.FlatAppearance.BorderSize = 0
        Me.btnIM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnIM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnIM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIM.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnIM.Location = New System.Drawing.Point(1, 1)
        Me.btnIM.Name = "btnIM"
        Me.btnIM.Size = New System.Drawing.Size(223, 25)
        Me.btnIM.TabIndex = 1
        Me.btnIM.Tag = "UnSelected"
        Me.btnIM.Text = "IM"
        Me.btnIM.UseVisualStyleBackColor = true
        '
        'Label145
        '
        Me.Label145.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label145.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label145.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label145.Location = New System.Drawing.Point(1, 26)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(223, 1)
        Me.Label145.TabIndex = 8
        Me.Label145.Text = "label2"
        '
        'Label159
        '
        Me.Label159.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label159.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label159.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label159.Location = New System.Drawing.Point(0, 1)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(1, 26)
        Me.Label159.TabIndex = 7
        Me.Label159.Text = "label4"
        '
        'Label160
        '
        Me.Label160.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label160.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label160.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label160.Location = New System.Drawing.Point(224, 1)
        Me.Label160.Name = "Label160"
        Me.Label160.Size = New System.Drawing.Size(1, 26)
        Me.Label160.TabIndex = 6
        Me.Label160.Text = "label3"
        '
        'Label161
        '
        Me.Label161.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label161.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label161.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label161.Location = New System.Drawing.Point(0, 0)
        Me.Label161.Name = "Label161"
        Me.Label161.Size = New System.Drawing.Size(225, 1)
        Me.Label161.TabIndex = 5
        Me.Label161.Text = "label1"
        '
        'tbPg_RefInfo
        '
        Me.tbPg_RefInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.tbPg_RefInfo.Controls.Add(Me.Panel44)
        Me.tbPg_RefInfo.Controls.Add(Me.Panel1)
        Me.tbPg_RefInfo.Location = New System.Drawing.Point(4, 22)
        Me.tbPg_RefInfo.Name = "tbPg_RefInfo"
        Me.tbPg_RefInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.tbPg_RefInfo.Size = New System.Drawing.Size(1083, 700)
        Me.tbPg_RefInfo.TabIndex = 3
        Me.tbPg_RefInfo.Text = "Reference Info"
        '
        'Panel44
        '
        Me.Panel44.Controls.Add(Me.Label242)
        Me.Panel44.Controls.Add(Me.Label243)
        Me.Panel44.Controls.Add(Me.Label328)
        Me.Panel44.Controls.Add(Me.Label327)
        Me.Panel44.Controls.Add(Me.Label326)
        Me.Panel44.Controls.Add(Me.Label325)
        Me.Panel44.Controls.Add(Me.Label247)
        Me.Panel44.Controls.Add(Me.Label246)
        Me.Panel44.Controls.Add(Me.txtRevisionDates)
        Me.Panel44.Controls.Add(Me.Label94)
        Me.Panel44.Controls.Add(Me.txtRelease)
        Me.Panel44.Controls.Add(Me.txtFundingSource)
        Me.Panel44.Controls.Add(Me.txtBibliographicCitation)
        Me.Panel44.Controls.Add(Me.txtInterventionDeveloper)
        Me.Panel44.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel44.Location = New System.Drawing.Point(3, 31)
        Me.Panel44.Name = "Panel44"
        Me.Panel44.Size = New System.Drawing.Size(1077, 666)
        Me.Panel44.TabIndex = 18
        '
        'Label242
        '
        Me.Label242.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label242.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label242.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label242.Location = New System.Drawing.Point(1076, 1)
        Me.Label242.Name = "Label242"
        Me.Label242.Size = New System.Drawing.Size(1, 664)
        Me.Label242.TabIndex = 18
        '
        'Label243
        '
        Me.Label243.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label243.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label243.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label243.Location = New System.Drawing.Point(1, 665)
        Me.Label243.Name = "Label243"
        Me.Label243.Size = New System.Drawing.Size(1076, 1)
        Me.Label243.TabIndex = 17
        Me.Label243.Text = "label2"
        '
        'Label328
        '
        Me.Label328.AutoSize = true
        Me.Label328.Location = New System.Drawing.Point(123, 336)
        Me.Label328.Name = "Label328"
        Me.Label328.Size = New System.Drawing.Size(93, 14)
        Me.Label328.TabIndex = 16
        Me.Label328.Text = "Revision Dates :"
        '
        'Label327
        '
        Me.Label327.AutoSize = true
        Me.Label327.Location = New System.Drawing.Point(160, 266)
        Me.Label327.Name = "Label327"
        Me.Label327.Size = New System.Drawing.Size(56, 14)
        Me.Label327.TabIndex = 16
        Me.Label327.Text = "Release :"
        '
        'Label326
        '
        Me.Label326.AutoSize = true
        Me.Label326.Location = New System.Drawing.Point(116, 194)
        Me.Label326.Name = "Label326"
        Me.Label326.Size = New System.Drawing.Size(100, 14)
        Me.Label326.TabIndex = 16
        Me.Label326.Text = "Funding Source :"
        '
        'Label325
        '
        Me.Label325.AutoSize = true
        Me.Label325.Location = New System.Drawing.Point(74, 124)
        Me.Label325.Name = "Label325"
        Me.Label325.Size = New System.Drawing.Size(142, 14)
        Me.Label325.TabIndex = 16
        Me.Label325.Text = "Intervention Developer :"
        '
        'Label247
        '
        Me.Label247.AutoSize = true
        Me.Label247.Location = New System.Drawing.Point(90, 36)
        Me.Label247.Name = "Label247"
        Me.Label247.Size = New System.Drawing.Size(126, 14)
        Me.Label247.TabIndex = 16
        Me.Label247.Text = "Bibliographic Citation :"
        '
        'Label246
        '
        Me.Label246.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label246.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label246.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label246.Location = New System.Drawing.Point(1, 0)
        Me.Label246.Name = "Label246"
        Me.Label246.Size = New System.Drawing.Size(1076, 1)
        Me.Label246.TabIndex = 16
        Me.Label246.Text = "label2"
        '
        'txtRevisionDates
        '
        Me.txtRevisionDates.Location = New System.Drawing.Point(219, 332)
        Me.txtRevisionDates.MaxLength = 2500
        Me.txtRevisionDates.Multiline = true
        Me.txtRevisionDates.Name = "txtRevisionDates"
        Me.txtRevisionDates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRevisionDates.Size = New System.Drawing.Size(732, 63)
        Me.txtRevisionDates.TabIndex = 4
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label94.Location = New System.Drawing.Point(0, 0)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(1, 666)
        Me.Label94.TabIndex = 13
        '
        'txtRelease
        '
        Me.txtRelease.Location = New System.Drawing.Point(219, 261)
        Me.txtRelease.MaxLength = 2500
        Me.txtRelease.Multiline = true
        Me.txtRelease.Name = "txtRelease"
        Me.txtRelease.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRelease.Size = New System.Drawing.Size(732, 63)
        Me.txtRelease.TabIndex = 3
        '
        'txtFundingSource
        '
        Me.txtFundingSource.Location = New System.Drawing.Point(219, 190)
        Me.txtFundingSource.MaxLength = 2500
        Me.txtFundingSource.Multiline = true
        Me.txtFundingSource.Name = "txtFundingSource"
        Me.txtFundingSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFundingSource.Size = New System.Drawing.Size(732, 63)
        Me.txtFundingSource.TabIndex = 2
        '
        'txtBibliographicCitation
        '
        Me.txtBibliographicCitation.Location = New System.Drawing.Point(219, 32)
        Me.txtBibliographicCitation.MaxLength = 2500
        Me.txtBibliographicCitation.Multiline = true
        Me.txtBibliographicCitation.Name = "txtBibliographicCitation"
        Me.txtBibliographicCitation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBibliographicCitation.Size = New System.Drawing.Size(732, 79)
        Me.txtBibliographicCitation.TabIndex = 0
        '
        'txtInterventionDeveloper
        '
        Me.txtInterventionDeveloper.Location = New System.Drawing.Point(219, 119)
        Me.txtInterventionDeveloper.MaxLength = 2500
        Me.txtInterventionDeveloper.Multiline = true
        Me.txtInterventionDeveloper.Name = "txtInterventionDeveloper"
        Me.txtInterventionDeveloper.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInterventionDeveloper.Size = New System.Drawing.Size(732, 63)
        Me.txtInterventionDeveloper.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel43)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(1077, 28)
        Me.Panel1.TabIndex = 46
        '
        'Panel43
        '
        Me.Panel43.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel43.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel43.Controls.Add(Me.Label261)
        Me.Panel43.Controls.Add(Me.Label262)
        Me.Panel43.Controls.Add(Me.Label322)
        Me.Panel43.Controls.Add(Me.Label323)
        Me.Panel43.Controls.Add(Me.Label324)
        Me.Panel43.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel43.Location = New System.Drawing.Point(0, 0)
        Me.Panel43.Name = "Panel43"
        Me.Panel43.Size = New System.Drawing.Size(1077, 25)
        Me.Panel43.TabIndex = 19
        '
        'Label261
        '
        Me.Label261.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label261.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label261.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label261.Location = New System.Drawing.Point(1, 24)
        Me.Label261.Name = "Label261"
        Me.Label261.Size = New System.Drawing.Size(1075, 1)
        Me.Label261.TabIndex = 13
        Me.Label261.Text = "label2"
        '
        'Label262
        '
        Me.Label262.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label262.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label262.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label262.Location = New System.Drawing.Point(0, 1)
        Me.Label262.Name = "Label262"
        Me.Label262.Size = New System.Drawing.Size(1, 24)
        Me.Label262.TabIndex = 12
        Me.Label262.Text = "label4"
        '
        'Label322
        '
        Me.Label322.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label322.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label322.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label322.Location = New System.Drawing.Point(1076, 1)
        Me.Label322.Name = "Label322"
        Me.Label322.Size = New System.Drawing.Size(1, 24)
        Me.Label322.TabIndex = 11
        Me.Label322.Text = "label3"
        '
        'Label323
        '
        Me.Label323.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label323.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label323.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label323.Location = New System.Drawing.Point(0, 0)
        Me.Label323.Name = "Label323"
        Me.Label323.Size = New System.Drawing.Size(1077, 1)
        Me.Label323.TabIndex = 10
        Me.Label323.Text = "label1"
        '
        'Label324
        '
        Me.Label324.BackColor = System.Drawing.Color.Transparent
        Me.Label324.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label324.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label324.ForeColor = System.Drawing.Color.White
        Me.Label324.Image = CType(resources.GetObject("Label324.Image"),System.Drawing.Image)
        Me.Label324.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label324.Location = New System.Drawing.Point(0, 0)
        Me.Label324.Name = "Label324"
        Me.Label324.Size = New System.Drawing.Size(1077, 25)
        Me.Label324.TabIndex = 9
        Me.Label324.Text = "      Referance Info"
        Me.Label324.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'sptLeft
        '
        Me.sptLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.sptLeft.Location = New System.Drawing.Point(0, 0)
        Me.sptLeft.Name = "sptLeft"
        Me.sptLeft.Size = New System.Drawing.Size(3, 726)
        Me.sptLeft.TabIndex = 4
        Me.sptLeft.TabStop = false
        '
        'Panel84
        '
        Me.Panel84.Location = New System.Drawing.Point(0, 0)
        Me.Panel84.Name = "Panel84"
        Me.Panel84.Size = New System.Drawing.Size(200, 100)
        Me.Panel84.TabIndex = 0
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Description"
        Me.ColumnHeader8.Width = 215
        '
        'cmbMaritalSt
        '
        Me.cmbMaritalSt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMaritalSt.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbMaritalSt.ForeColor = System.Drawing.Color.Black
        Me.cmbMaritalSt.Location = New System.Drawing.Point(665, 12)
        Me.cmbMaritalSt.Name = "cmbMaritalSt"
        Me.cmbMaritalSt.Size = New System.Drawing.Size(42, 22)
        Me.cmbMaritalSt.TabIndex = 7
        Me.cmbMaritalSt.Visible = false
        '
        'cmbRace
        '
        Me.cmbRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRace.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbRace.ForeColor = System.Drawing.Color.Black
        Me.cmbRace.Location = New System.Drawing.Point(761, 12)
        Me.cmbRace.Name = "cmbRace"
        Me.cmbRace.Size = New System.Drawing.Size(42, 22)
        Me.cmbRace.TabIndex = 5
        Me.cmbRace.Visible = false
        '
        'cmbGender
        '
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmbGender.ForeColor = System.Drawing.Color.Black
        Me.cmbGender.Location = New System.Drawing.Point(713, 12)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(42, 22)
        Me.cmbGender.TabIndex = 3
        Me.cmbGender.Visible = false
        '
        'pnlMsgTOP
        '
        Me.pnlMsgTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsgTOP.Controls.Add(Me.pnlMsg)
        Me.pnlMsgTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMsgTOP.Location = New System.Drawing.Point(0, 54)
        Me.pnlMsgTOP.Name = "pnlMsgTOP"
        Me.pnlMsgTOP.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMsgTOP.Size = New System.Drawing.Size(1094, 92)
        Me.pnlMsgTOP.TabIndex = 0
        '
        'pnlMsg
        '
        Me.pnlMsg.BackColor = System.Drawing.Color.Transparent
        Me.pnlMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsg.Controls.Add(Me.Label24)
        Me.pnlMsg.Controls.Add(Me.Panel27)
        Me.pnlMsg.Controls.Add(Me.Label141)
        Me.pnlMsg.Controls.Add(Me.Label4)
        Me.pnlMsg.Controls.Add(Me.Label118)
        Me.pnlMsg.Controls.Add(Me.txtName)
        Me.pnlMsg.Controls.Add(Me.txtMessage)
        Me.pnlMsg.Controls.Add(Me.Label3)
        Me.pnlMsg.Controls.Add(Me.Label23)
        Me.pnlMsg.Controls.Add(Me.Label63)
        Me.pnlMsg.Controls.Add(Me.Label25)
        Me.pnlMsg.Controls.Add(Me.Label117)
        Me.pnlMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMsg.Location = New System.Drawing.Point(3, 3)
        Me.pnlMsg.Name = "pnlMsg"
        Me.pnlMsg.Size = New System.Drawing.Size(1088, 86)
        Me.pnlMsg.TabIndex = 0
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 84)
        Me.Label24.TabIndex = 115
        Me.Label24.Text = "label3"
        '
        'Panel27
        '
        Me.Panel27.Controls.Add(Me.pnlRecurring)
        Me.Panel27.Controls.Add(Me.Panel42)
        Me.Panel27.Controls.Add(Me.Label5)
        Me.Panel27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel27.Location = New System.Drawing.Point(583, 1)
        Me.Panel27.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel27.Name = "Panel27"
        Me.Panel27.Size = New System.Drawing.Size(504, 84)
        Me.Panel27.TabIndex = 114
        '
        'pnlRecurring
        '
        Me.pnlRecurring.Controls.Add(Me.pnlRecurrenceControls)
        Me.pnlRecurring.Controls.Add(Me.Label27)
        Me.pnlRecurring.Controls.Add(Me.chckRecurring)
        Me.pnlRecurring.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRecurring.Location = New System.Drawing.Point(1, 23)
        Me.pnlRecurring.Name = "pnlRecurring"
        Me.pnlRecurring.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlRecurring.Size = New System.Drawing.Size(503, 61)
        Me.pnlRecurring.TabIndex = 88
        '
        'pnlRecurrenceControls
        '
        Me.pnlRecurrenceControls.Controls.Add(Me.cmbPeriod)
        Me.pnlRecurrenceControls.Controls.Add(Me.cmbDurationType)
        Me.pnlRecurrenceControls.Controls.Add(Me.Label37)
        Me.pnlRecurrenceControls.Controls.Add(Me.Label32)
        Me.pnlRecurrenceControls.Controls.Add(Me.dtStartDate)
        Me.pnlRecurrenceControls.Controls.Add(Me.dtEndDate)
        Me.pnlRecurrenceControls.Controls.Add(Me.Label31)
        Me.pnlRecurrenceControls.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRecurrenceControls.Enabled = false
        Me.pnlRecurrenceControls.Location = New System.Drawing.Point(102, 1)
        Me.pnlRecurrenceControls.Name = "pnlRecurrenceControls"
        Me.pnlRecurrenceControls.Size = New System.Drawing.Size(398, 60)
        Me.pnlRecurrenceControls.TabIndex = 127
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = true
        Me.cmbPeriod.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.cmbPeriod.Location = New System.Drawing.Point(88, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(58, 22)
        Me.cmbPeriod.TabIndex = 10
        '
        'cmbDurationType
        '
        Me.cmbDurationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDurationType.FormattingEnabled = true
        Me.cmbDurationType.Items.AddRange(New Object() {"Day(s)", "Week(s)", "Month(s)", "Year(s)"})
        Me.cmbDurationType.Location = New System.Drawing.Point(154, 5)
        Me.cmbDurationType.Name = "cmbDurationType"
        Me.cmbDurationType.Size = New System.Drawing.Size(85, 22)
        Me.cmbDurationType.TabIndex = 11
        '
        'Label37
        '
        Me.Label37.AutoSize = true
        Me.Label37.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label37.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label37.Location = New System.Drawing.Point(5, 9)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(80, 14)
        Me.Label37.TabIndex = 0
        Me.Label37.Text = "Recur every :"
        '
        'Label32
        '
        Me.Label32.AutoSize = true
        Me.Label32.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label32.Location = New System.Drawing.Point(13, 38)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(72, 14)
        Me.Label32.TabIndex = 5
        Me.Label32.Text = "Start Date :"
        Me.Label32.Visible = false
        '
        'dtStartDate
        '
        Me.dtStartDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtStartDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStartDate.Location = New System.Drawing.Point(88, 33)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(104, 22)
        Me.dtStartDate.TabIndex = 8
        Me.dtStartDate.Visible = false
        '
        'dtEndDate
        '
        Me.dtEndDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtEndDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEndDate.Location = New System.Drawing.Point(267, 33)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(104, 22)
        Me.dtEndDate.TabIndex = 9
        Me.dtEndDate.Visible = false
        '
        'Label31
        '
        Me.Label31.AutoSize = true
        Me.Label31.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label31.Location = New System.Drawing.Point(198, 37)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(66, 14)
        Me.Label31.TabIndex = 12
        Me.Label31.Text = "End Date :"
        Me.Label31.Visible = false
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label27.Location = New System.Drawing.Point(0, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(500, 1)
        Me.Label27.TabIndex = 123
        Me.Label27.Text = "label1"
        '
        'chckRecurring
        '
        Me.chckRecurring.AutoSize = true
        Me.chckRecurring.Location = New System.Drawing.Point(9, 8)
        Me.chckRecurring.Name = "chckRecurring"
        Me.chckRecurring.Size = New System.Drawing.Size(90, 18)
        Me.chckRecurring.TabIndex = 122
        Me.chckRecurring.Text = "Is Recurring"
        Me.chckRecurring.UseVisualStyleBackColor = true
        '
        'Panel42
        '
        Me.Panel42.Controls.Add(Me.lblHealthPlanAlert)
        Me.Panel42.Controls.Add(Me.chkSpecialAlert)
        Me.Panel42.Controls.Add(Me.chkIsActiveRule)
        Me.Panel42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel42.Location = New System.Drawing.Point(1, 0)
        Me.Panel42.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel42.Name = "Panel42"
        Me.Panel42.Size = New System.Drawing.Size(503, 23)
        Me.Panel42.TabIndex = 5
        '
        'lblHealthPlanAlert
        '
        Me.lblHealthPlanAlert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHealthPlanAlert.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblHealthPlanAlert.ForeColor = System.Drawing.Color.Red
        Me.lblHealthPlanAlert.Location = New System.Drawing.Point(209, 0)
        Me.lblHealthPlanAlert.Name = "lblHealthPlanAlert"
        Me.lblHealthPlanAlert.Size = New System.Drawing.Size(294, 23)
        Me.lblHealthPlanAlert.TabIndex = 78
        Me.lblHealthPlanAlert.Text = " This is a health plan orders to be given rule"
        Me.lblHealthPlanAlert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblHealthPlanAlert.Visible = false
        '
        'chkSpecialAlert
        '
        Me.chkSpecialAlert.AutoSize = true
        Me.chkSpecialAlert.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkSpecialAlert.Location = New System.Drawing.Point(108, 0)
        Me.chkSpecialAlert.Name = "chkSpecialAlert"
        Me.chkSpecialAlert.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.chkSpecialAlert.Size = New System.Drawing.Size(101, 23)
        Me.chkSpecialAlert.TabIndex = 124
        Me.chkSpecialAlert.Text = "Special Alert"
        Me.ToolTip1.SetToolTip(Me.chkSpecialAlert, resources.GetString("chkSpecialAlert.ToolTip"))
        Me.chkSpecialAlert.UseVisualStyleBackColor = true
        '
        'chkIsActiveRule
        '
        Me.chkIsActiveRule.AutoSize = true
        Me.chkIsActiveRule.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkIsActiveRule.Enabled = false
        Me.chkIsActiveRule.Location = New System.Drawing.Point(0, 0)
        Me.chkIsActiveRule.Name = "chkIsActiveRule"
        Me.chkIsActiveRule.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.chkIsActiveRule.Size = New System.Drawing.Size(108, 23)
        Me.chkIsActiveRule.TabIndex = 123
        Me.chkIsActiveRule.Text = "Is Active Rule"
        Me.chkIsActiveRule.UseVisualStyleBackColor = true
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 84)
        Me.Label5.TabIndex = 89
        Me.Label5.Text = "label4"
        '
        'Label141
        '
        Me.Label141.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Label141.AutoSize = true
        Me.Label141.ForeColor = System.Drawing.Color.Red
        Me.Label141.Location = New System.Drawing.Point(-121, 41)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(14, 14)
        Me.Label141.TabIndex = 24
        Me.Label141.Text = "*"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(18, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 14)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Message :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label118
        '
        Me.Label118.AutoSize = true
        Me.Label118.ForeColor = System.Drawing.Color.Red
        Me.Label118.Location = New System.Drawing.Point(26, 10)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(14, 14)
        Me.Label118.TabIndex = 22
        Me.Label118.Text = "*"
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(86, 6)
        Me.txtName.MaxLength = 255
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(480, 22)
        Me.txtName.TabIndex = 0
        '
        'txtMessage
        '
        Me.txtMessage.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtMessage.ForeColor = System.Drawing.Color.Black
        Me.txtMessage.Location = New System.Drawing.Point(86, 32)
        Me.txtMessage.MaxLength = 1500
        Me.txtMessage.Multiline = true
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMessage.Size = New System.Drawing.Size(480, 48)
        Me.txtMessage.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(29, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 14)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "  Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label23.Location = New System.Drawing.Point(0, 85)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1087, 1)
        Me.Label23.TabIndex = 17
        Me.Label23.Text = "label2"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.Label63.Location = New System.Drawing.Point(1087, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 85)
        Me.Label63.TabIndex = 15
        Me.Label63.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1088, 1)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = "label1"
        '
        'Label117
        '
        Me.Label117.AutoSize = true
        Me.Label117.ForeColor = System.Drawing.Color.Red
        Me.Label117.Location = New System.Drawing.Point(6, 35)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(14, 14)
        Me.Label117.TabIndex = 1
        Me.Label117.Text = "*"
        '
        'pnl_tlstrip
        '
        Me.pnl_tlstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlstrip.Controls.Add(Me.pnlgloStreamLogo)
        Me.pnl_tlstrip.Controls.Add(Me.tlsDM)
        Me.pnl_tlstrip.Controls.Add(Me.cmbMaritalSt)
        Me.pnl_tlstrip.Controls.Add(Me.cmbRace)
        Me.pnl_tlstrip.Controls.Add(Me.cmbGender)
        Me.pnl_tlstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlstrip.Name = "pnl_tlstrip"
        Me.pnl_tlstrip.Size = New System.Drawing.Size(1094, 54)
        Me.pnl_tlstrip.TabIndex = 3
        '
        'pnlgloStreamLogo
        '
        Me.pnlgloStreamLogo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pnlgloStreamLogo.BackColor = System.Drawing.Color.Transparent
        Me.pnlgloStreamLogo.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.pnlgloStreamLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlgloStreamLogo.Controls.Add(Me.PictureBox4)
        Me.pnlgloStreamLogo.Location = New System.Drawing.Point(865, 0)
        Me.pnlgloStreamLogo.Name = "pnlgloStreamLogo"
        Me.pnlgloStreamLogo.Size = New System.Drawing.Size(216, 52)
        Me.pnlgloStreamLogo.TabIndex = 9
        '
        'PictureBox4
        '
        Me.PictureBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"),System.Drawing.Image)
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox4.Location = New System.Drawing.Point(12, 3)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(192, 48)
        Me.PictureBox4.TabIndex = 8
        Me.PictureBox4.TabStop = false
        '
        'tlsDM
        '
        Me.tlsDM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDM.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.tlsDM.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsDM_Save, Me.tlsDM_Close})
        Me.tlsDM.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDM.Location = New System.Drawing.Point(0, 0)
        Me.tlsDM.Name = "tlsDM"
        Me.tlsDM.Size = New System.Drawing.Size(1094, 53)
        Me.tlsDM.TabIndex = 0
        Me.tlsDM.Text = "ToolStrip1"
        '
        'tlsDM_Save
        '
        Me.tlsDM_Save.Image = CType(resources.GetObject("tlsDM_Save.Image"),System.Drawing.Image)
        Me.tlsDM_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsDM_Save.Name = "tlsDM_Save"
        Me.tlsDM_Save.Size = New System.Drawing.Size(66, 50)
        Me.tlsDM_Save.Tag = "Save"
        Me.tlsDM_Save.Text = "&Save&&Cls"
        Me.tlsDM_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsDM_Save.ToolTipText = "Save and Close"
        '
        'tlsDM_Close
        '
        Me.tlsDM_Close.Image = CType(resources.GetObject("tlsDM_Close.Image"),System.Drawing.Image)
        Me.tlsDM_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsDM_Close.Name = "tlsDM_Close"
        Me.tlsDM_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlsDM_Close.Tag = "Close"
        Me.tlsDM_Close.Text = "&Close"
        Me.tlsDM_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CntConditions
        '
        Me.CntConditions.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDelete, Me.mnuReferral, Me.EditReferral, Me.mnuEditTemplate})
        '
        'mnuDelete
        '
        Me.mnuDelete.Index = 0
        Me.mnuDelete.Text = "Delete "
        '
        'mnuReferral
        '
        Me.mnuReferral.Index = 1
        Me.mnuReferral.Text = "Add Referral"
        '
        'EditReferral
        '
        Me.EditReferral.Index = 2
        Me.EditReferral.Text = "Edit Referrals"
        '
        'mnuEditTemplate
        '
        Me.mnuEditTemplate.Index = 3
        Me.mnuEditTemplate.Text = "Edit Template"
        '
        'cntFindingsprb
        '
        Me.cntFindingsprb.Name = "cntFindingsprb"
        Me.cntFindingsprb.Size = New System.Drawing.Size(61, 4)
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteInsurance})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(188, 26)
        '
        'mnuDeleteInsurance
        '
        Me.mnuDeleteInsurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuDeleteInsurance.Image = CType(resources.GetObject("mnuDeleteInsurance.Image"),System.Drawing.Image)
        Me.mnuDeleteInsurance.Name = "mnuDeleteInsurance"
        Me.mnuDeleteInsurance.Size = New System.Drawing.Size(187, 22)
        Me.mnuDeleteInsurance.Text = "Delete Insurance plan"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteDrugs})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(142, 26)
        '
        'mnuDeleteDrugs
        '
        Me.mnuDeleteDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuDeleteDrugs.Image = CType(resources.GetObject("mnuDeleteDrugs.Image"),System.Drawing.Image)
        Me.mnuDeleteDrugs.Name = "mnuDeleteDrugs"
        Me.mnuDeleteDrugs.Size = New System.Drawing.Size(141, 22)
        Me.mnuDeleteDrugs.Text = "Delete Drugs"
        '
        'ContextMenuHistory
        '
        Me.ContextMenuHistory.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteHistory})
        Me.ContextMenuHistory.Name = "ContextMenuStrip1"
        Me.ContextMenuHistory.Size = New System.Drawing.Size(149, 26)
        '
        'mnuDeleteHistory
        '
        Me.mnuDeleteHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuDeleteHistory.Image = CType(resources.GetObject("mnuDeleteHistory.Image"),System.Drawing.Image)
        Me.mnuDeleteHistory.Name = "mnuDeleteHistory"
        Me.mnuDeleteHistory.Size = New System.Drawing.Size(148, 22)
        Me.mnuDeleteHistory.Text = "Delete History"
        '
        'CmnuStripCPT
        '
        Me.CmnuStripCPT.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItem_DeleteCPT})
        Me.CmnuStripCPT.Name = "ContextMenuStrip1"
        Me.CmnuStripCPT.Size = New System.Drawing.Size(133, 26)
        '
        'mnuItem_DeleteCPT
        '
        Me.mnuItem_DeleteCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuItem_DeleteCPT.Image = CType(resources.GetObject("mnuItem_DeleteCPT.Image"),System.Drawing.Image)
        Me.mnuItem_DeleteCPT.Name = "mnuItem_DeleteCPT"
        Me.mnuItem_DeleteCPT.Size = New System.Drawing.Size(132, 22)
        Me.mnuItem_DeleteCPT.Text = "Delete CPT"
        '
        'CmnustripICD
        '
        Me.CmnustripICD.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItem_DeleteICD})
        Me.CmnustripICD.Name = "ContextMenuStrip1"
        Me.CmnustripICD.Size = New System.Drawing.Size(130, 26)
        '
        'mnuItem_DeleteICD
        '
        Me.mnuItem_DeleteICD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.mnuItem_DeleteICD.Image = CType(resources.GetObject("mnuItem_DeleteICD.Image"),System.Drawing.Image)
        Me.mnuItem_DeleteICD.Name = "mnuItem_DeleteICD"
        Me.mnuItem_DeleteICD.Size = New System.Drawing.Size(129, 22)
        Me.mnuItem_DeleteICD.Text = "Delete ICD"
        '
        'cntFindings
        '
        Me.cntFindings.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnu_AddFindings})
        Me.cntFindings.Name = "cntFindings"
        Me.cntFindings.Size = New System.Drawing.Size(145, 26)
        '
        'mnu_AddFindings
        '
        Me.mnu_AddFindings.Name = "mnu_AddFindings"
        Me.mnu_AddFindings.Size = New System.Drawing.Size(144, 22)
        Me.mnu_AddFindings.Text = "Add Findings"
        '
        'ContextMenuProblem
        '
        Me.ContextMenuProblem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteProblem})
        Me.ContextMenuProblem.Name = "ContextMenuStrip1"
        Me.ContextMenuProblem.Size = New System.Drawing.Size(156, 26)
        '
        'mnuDeleteProblem
        '
        Me.mnuDeleteProblem.Name = "mnuDeleteProblem"
        Me.mnuDeleteProblem.Size = New System.Drawing.Size(155, 22)
        Me.mnuDeleteProblem.Text = "Delete Problem"
        Me.mnuDeleteProblem.ToolTipText = "Delete Problem"
        '
        'GloUC_trvHistory
        '
        Me.GloUC_trvHistory.AllergyClassID = Nothing
        Me.GloUC_trvHistory.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvHistory.CheckBoxes = false
        Me.GloUC_trvHistory.CodeMember = Nothing
        Me.GloUC_trvHistory.ColonAsSeparator = false
        Me.GloUC_trvHistory.Comment = Nothing
        Me.GloUC_trvHistory.ConceptID = Nothing
        Me.GloUC_trvHistory.CPT = Nothing
        Me.GloUC_trvHistory.DDIDMember = Nothing
        Me.GloUC_trvHistory.DescriptionMember = Nothing
        Me.GloUC_trvHistory.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvHistory.DrugFlag = CType(16,Short)
        Me.GloUC_trvHistory.DrugFormMember = Nothing
        Me.GloUC_trvHistory.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvHistory.DurationMember = Nothing
        Me.GloUC_trvHistory.EducationMappingSearchType = 1
        Me.GloUC_trvHistory.FrequencyMember = Nothing
        Me.GloUC_trvHistory.HistoryType = Nothing
        Me.GloUC_trvHistory.ICD9 = Nothing
        Me.GloUC_trvHistory.ICDRevision = Nothing
        Me.GloUC_trvHistory.ImageIndex = 0
        Me.GloUC_trvHistory.ImageList = Me.ImageList1
        Me.GloUC_trvHistory.ImageObject = Nothing
        Me.GloUC_trvHistory.Indicator = Nothing
        Me.GloUC_trvHistory.IsCPTSearch = false
        Me.GloUC_trvHistory.IsDiagnosisSearch = false
        Me.GloUC_trvHistory.IsDrug = false
        Me.GloUC_trvHistory.IsNarcoticsMember = Nothing
        Me.GloUC_trvHistory.IsSearchForEducationMapping = false
        Me.GloUC_trvHistory.IsSystemCategory = Nothing
        Me.GloUC_trvHistory.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_trvHistory.MaximumNodes = 1000
        Me.GloUC_trvHistory.mpidmember = Nothing
        Me.GloUC_trvHistory.Name = "GloUC_trvHistory"
        Me.GloUC_trvHistory.NDCCodeMember = Nothing
        Me.GloUC_trvHistory.ParentImageIndex = 0
        Me.GloUC_trvHistory.ParentMember = Nothing
        Me.GloUC_trvHistory.RouteMember = Nothing
        Me.GloUC_trvHistory.RowOrderMember = Nothing
        Me.GloUC_trvHistory.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvHistory.SearchBox = true
        Me.GloUC_trvHistory.SearchText = Nothing
        Me.GloUC_trvHistory.SelectedImageIndex = 0
        Me.GloUC_trvHistory.SelectedNode = Nothing
        Me.GloUC_trvHistory.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvHistory.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvHistory.SelectedParentImageIndex = 0
        Me.GloUC_trvHistory.Size = New System.Drawing.Size(790, 357)
        Me.GloUC_trvHistory.SmartTreatmentId = Nothing
        Me.GloUC_trvHistory.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvHistory.TabIndex = 48
        Me.GloUC_trvHistory.Tag = Nothing
        Me.GloUC_trvHistory.UnitMember = Nothing
        Me.GloUC_trvHistory.ValueMember = Nothing
        '
        'GloUC_trvInsurance
        '
        Me.GloUC_trvInsurance.AllergyClassID = Nothing
        Me.GloUC_trvInsurance.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvInsurance.CheckBoxes = false
        Me.GloUC_trvInsurance.CodeMember = Nothing
        Me.GloUC_trvInsurance.ColonAsSeparator = false
        Me.GloUC_trvInsurance.Comment = Nothing
        Me.GloUC_trvInsurance.ConceptID = Nothing
        Me.GloUC_trvInsurance.CPT = Nothing
        Me.GloUC_trvInsurance.DDIDMember = Nothing
        Me.GloUC_trvInsurance.DescriptionMember = Nothing
        Me.GloUC_trvInsurance.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvInsurance.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvInsurance.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvInsurance.DrugFlag = CType(16,Short)
        Me.GloUC_trvInsurance.DrugFormMember = Nothing
        Me.GloUC_trvInsurance.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvInsurance.DurationMember = Nothing
        Me.GloUC_trvInsurance.EducationMappingSearchType = 1
        Me.GloUC_trvInsurance.FrequencyMember = Nothing
        Me.GloUC_trvInsurance.HistoryType = Nothing
        Me.GloUC_trvInsurance.ICD9 = Nothing
        Me.GloUC_trvInsurance.ICDRevision = Nothing
        Me.GloUC_trvInsurance.ImageIndex = 0
        Me.GloUC_trvInsurance.ImageList = Me.ImageList1
        Me.GloUC_trvInsurance.ImageObject = Nothing
        Me.GloUC_trvInsurance.Indicator = Nothing
        Me.GloUC_trvInsurance.IsCPTSearch = false
        Me.GloUC_trvInsurance.IsDiagnosisSearch = false
        Me.GloUC_trvInsurance.IsDrug = false
        Me.GloUC_trvInsurance.IsNarcoticsMember = Nothing
        Me.GloUC_trvInsurance.IsSearchForEducationMapping = false
        Me.GloUC_trvInsurance.IsSystemCategory = Nothing
        Me.GloUC_trvInsurance.Location = New System.Drawing.Point(0, 54)
        Me.GloUC_trvInsurance.MaximumNodes = 1000
        Me.GloUC_trvInsurance.mpidmember = Nothing
        Me.GloUC_trvInsurance.Name = "GloUC_trvInsurance"
        Me.GloUC_trvInsurance.NDCCodeMember = Nothing
        Me.GloUC_trvInsurance.ParentImageIndex = 0
        Me.GloUC_trvInsurance.ParentMember = Nothing
        Me.GloUC_trvInsurance.RouteMember = Nothing
        Me.GloUC_trvInsurance.RowOrderMember = Nothing
        Me.GloUC_trvInsurance.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvInsurance.SearchBox = true
        Me.GloUC_trvInsurance.SearchText = Nothing
        Me.GloUC_trvInsurance.SelectedImageIndex = 0
        Me.GloUC_trvInsurance.SelectedNode = Nothing
        Me.GloUC_trvInsurance.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvInsurance.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvInsurance.SelectedParentImageIndex = 0
        Me.GloUC_trvInsurance.Size = New System.Drawing.Size(1077, 392)
        Me.GloUC_trvInsurance.SmartTreatmentId = Nothing
        Me.GloUC_trvInsurance.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvInsurance.TabIndex = 9
        Me.GloUC_trvInsurance.Tag = Nothing
        Me.GloUC_trvInsurance.UnitMember = Nothing
        Me.GloUC_trvInsurance.ValueMember = Nothing
        '
        'GloUC_trvICD10
        '
        Me.GloUC_trvICD10.AllergyClassID = Nothing
        Me.GloUC_trvICD10.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvICD10.CheckBoxes = false
        Me.GloUC_trvICD10.CodeMember = Nothing
        Me.GloUC_trvICD10.ColonAsSeparator = false
        Me.GloUC_trvICD10.Comment = Nothing
        Me.GloUC_trvICD10.ConceptID = Nothing
        Me.GloUC_trvICD10.CPT = Nothing
        Me.GloUC_trvICD10.DDIDMember = Nothing
        Me.GloUC_trvICD10.DescriptionMember = Nothing
        Me.GloUC_trvICD10.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvICD10.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvICD10.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvICD10.DrugFlag = CType(16,Short)
        Me.GloUC_trvICD10.DrugFormMember = Nothing
        Me.GloUC_trvICD10.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvICD10.DurationMember = Nothing
        Me.GloUC_trvICD10.EducationMappingSearchType = 1
        Me.GloUC_trvICD10.FrequencyMember = Nothing
        Me.GloUC_trvICD10.HistoryType = Nothing
        Me.GloUC_trvICD10.ICD9 = Nothing
        Me.GloUC_trvICD10.ICDRevision = Nothing
        Me.GloUC_trvICD10.ImageIndex = 0
        Me.GloUC_trvICD10.ImageList = Me.ImageList1
        Me.GloUC_trvICD10.ImageObject = Nothing
        Me.GloUC_trvICD10.Indicator = Nothing
        Me.GloUC_trvICD10.IsCPTSearch = false
        Me.GloUC_trvICD10.IsDiagnosisSearch = false
        Me.GloUC_trvICD10.IsDrug = false
        Me.GloUC_trvICD10.IsNarcoticsMember = Nothing
        Me.GloUC_trvICD10.IsSearchForEducationMapping = false
        Me.GloUC_trvICD10.IsSystemCategory = Nothing
        Me.GloUC_trvICD10.Location = New System.Drawing.Point(0, 444)
        Me.GloUC_trvICD10.MaximumNodes = 1000
        Me.GloUC_trvICD10.mpidmember = Nothing
        Me.GloUC_trvICD10.Name = "GloUC_trvICD10"
        Me.GloUC_trvICD10.NDCCodeMember = Nothing
        Me.GloUC_trvICD10.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.GloUC_trvICD10.ParentImageIndex = 0
        Me.GloUC_trvICD10.ParentMember = Nothing
        Me.GloUC_trvICD10.RouteMember = Nothing
        Me.GloUC_trvICD10.RowOrderMember = Nothing
        Me.GloUC_trvICD10.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvICD10.SearchBox = true
        Me.GloUC_trvICD10.SearchText = Nothing
        Me.GloUC_trvICD10.SelectedImageIndex = 0
        Me.GloUC_trvICD10.SelectedNode = Nothing
        Me.GloUC_trvICD10.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvICD10.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvICD10.SelectedParentImageIndex = 0
        Me.GloUC_trvICD10.Size = New System.Drawing.Size(1077, 390)
        Me.GloUC_trvICD10.SmartTreatmentId = Nothing
        Me.GloUC_trvICD10.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvICD10.TabIndex = 53
        Me.GloUC_trvICD10.Tag = Nothing
        Me.GloUC_trvICD10.UnitMember = Nothing
        Me.GloUC_trvICD10.ValueMember = Nothing
        '
        'GloUC_trvICD9
        '
        Me.GloUC_trvICD9.AllergyClassID = Nothing
        Me.GloUC_trvICD9.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvICD9.CheckBoxes = false
        Me.GloUC_trvICD9.CodeMember = Nothing
        Me.GloUC_trvICD9.ColonAsSeparator = false
        Me.GloUC_trvICD9.Comment = Nothing
        Me.GloUC_trvICD9.ConceptID = Nothing
        Me.GloUC_trvICD9.CPT = Nothing
        Me.GloUC_trvICD9.DDIDMember = Nothing
        Me.GloUC_trvICD9.DescriptionMember = Nothing
        Me.GloUC_trvICD9.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvICD9.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvICD9.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvICD9.DrugFlag = CType(16,Short)
        Me.GloUC_trvICD9.DrugFormMember = Nothing
        Me.GloUC_trvICD9.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvICD9.DurationMember = Nothing
        Me.GloUC_trvICD9.EducationMappingSearchType = 1
        Me.GloUC_trvICD9.FrequencyMember = Nothing
        Me.GloUC_trvICD9.HistoryType = Nothing
        Me.GloUC_trvICD9.ICD9 = Nothing
        Me.GloUC_trvICD9.ICDRevision = Nothing
        Me.GloUC_trvICD9.ImageIndex = 0
        Me.GloUC_trvICD9.ImageList = Me.ImageList1
        Me.GloUC_trvICD9.ImageObject = Nothing
        Me.GloUC_trvICD9.Indicator = Nothing
        Me.GloUC_trvICD9.IsCPTSearch = false
        Me.GloUC_trvICD9.IsDiagnosisSearch = false
        Me.GloUC_trvICD9.IsDrug = false
        Me.GloUC_trvICD9.IsNarcoticsMember = Nothing
        Me.GloUC_trvICD9.IsSearchForEducationMapping = false
        Me.GloUC_trvICD9.IsSystemCategory = Nothing
        Me.GloUC_trvICD9.Location = New System.Drawing.Point(0, 54)
        Me.GloUC_trvICD9.MaximumNodes = 1000
        Me.GloUC_trvICD9.mpidmember = Nothing
        Me.GloUC_trvICD9.Name = "GloUC_trvICD9"
        Me.GloUC_trvICD9.NDCCodeMember = Nothing
        Me.GloUC_trvICD9.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.GloUC_trvICD9.ParentImageIndex = 0
        Me.GloUC_trvICD9.ParentMember = Nothing
        Me.GloUC_trvICD9.RouteMember = Nothing
        Me.GloUC_trvICD9.RowOrderMember = Nothing
        Me.GloUC_trvICD9.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvICD9.SearchBox = true
        Me.GloUC_trvICD9.SearchText = Nothing
        Me.GloUC_trvICD9.SelectedImageIndex = 0
        Me.GloUC_trvICD9.SelectedNode = Nothing
        Me.GloUC_trvICD9.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvICD9.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvICD9.SelectedParentImageIndex = 0
        Me.GloUC_trvICD9.Size = New System.Drawing.Size(1077, 390)
        Me.GloUC_trvICD9.SmartTreatmentId = Nothing
        Me.GloUC_trvICD9.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvICD9.TabIndex = 47
        Me.GloUC_trvICD9.Tag = Nothing
        Me.GloUC_trvICD9.UnitMember = Nothing
        Me.GloUC_trvICD9.ValueMember = Nothing
        '
        'GloUC_trvCPT
        '
        Me.GloUC_trvCPT.AllergyClassID = Nothing
        Me.GloUC_trvCPT.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvCPT.CheckBoxes = false
        Me.GloUC_trvCPT.CodeMember = Nothing
        Me.GloUC_trvCPT.ColonAsSeparator = false
        Me.GloUC_trvCPT.Comment = Nothing
        Me.GloUC_trvCPT.ConceptID = Nothing
        Me.GloUC_trvCPT.CPT = Nothing
        Me.GloUC_trvCPT.DDIDMember = Nothing
        Me.GloUC_trvCPT.DescriptionMember = Nothing
        Me.GloUC_trvCPT.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvCPT.DrugFlag = CType(16,Short)
        Me.GloUC_trvCPT.DrugFormMember = Nothing
        Me.GloUC_trvCPT.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvCPT.DurationMember = Nothing
        Me.GloUC_trvCPT.EducationMappingSearchType = 1
        Me.GloUC_trvCPT.FrequencyMember = Nothing
        Me.GloUC_trvCPT.HistoryType = Nothing
        Me.GloUC_trvCPT.ICD9 = Nothing
        Me.GloUC_trvCPT.ICDRevision = Nothing
        Me.GloUC_trvCPT.ImageIndex = 0
        Me.GloUC_trvCPT.ImageList = Me.ImageList1
        Me.GloUC_trvCPT.ImageObject = Nothing
        Me.GloUC_trvCPT.Indicator = Nothing
        Me.GloUC_trvCPT.IsCPTSearch = false
        Me.GloUC_trvCPT.IsDiagnosisSearch = false
        Me.GloUC_trvCPT.IsDrug = false
        Me.GloUC_trvCPT.IsNarcoticsMember = Nothing
        Me.GloUC_trvCPT.IsSearchForEducationMapping = false
        Me.GloUC_trvCPT.IsSystemCategory = Nothing
        Me.GloUC_trvCPT.Location = New System.Drawing.Point(0, 54)
        Me.GloUC_trvCPT.MaximumNodes = 1000
        Me.GloUC_trvCPT.mpidmember = Nothing
        Me.GloUC_trvCPT.Name = "GloUC_trvCPT"
        Me.GloUC_trvCPT.NDCCodeMember = Nothing
        Me.GloUC_trvCPT.ParentImageIndex = 0
        Me.GloUC_trvCPT.ParentMember = Nothing
        Me.GloUC_trvCPT.RouteMember = Nothing
        Me.GloUC_trvCPT.RowOrderMember = Nothing
        Me.GloUC_trvCPT.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvCPT.SearchBox = true
        Me.GloUC_trvCPT.SearchText = Nothing
        Me.GloUC_trvCPT.SelectedImageIndex = 0
        Me.GloUC_trvCPT.SelectedNode = Nothing
        Me.GloUC_trvCPT.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvCPT.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvCPT.SelectedParentImageIndex = 0
        Me.GloUC_trvCPT.Size = New System.Drawing.Size(1077, 392)
        Me.GloUC_trvCPT.SmartTreatmentId = Nothing
        Me.GloUC_trvCPT.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvCPT.TabIndex = 46
        Me.GloUC_trvCPT.Tag = Nothing
        Me.GloUC_trvCPT.UnitMember = Nothing
        Me.GloUC_trvCPT.ValueMember = Nothing
        '
        'GloUC_trvDrugs
        '
        Me.GloUC_trvDrugs.AllergyClassID = Nothing
        Me.GloUC_trvDrugs.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvDrugs.CheckBoxes = false
        Me.GloUC_trvDrugs.CodeMember = Nothing
        Me.GloUC_trvDrugs.ColonAsSeparator = false
        Me.GloUC_trvDrugs.Comment = Nothing
        Me.GloUC_trvDrugs.ConceptID = Nothing
        Me.GloUC_trvDrugs.CPT = Nothing
        Me.GloUC_trvDrugs.DDIDMember = Nothing
        Me.GloUC_trvDrugs.DescriptionMember = Nothing
        Me.GloUC_trvDrugs.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvDrugs.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvDrugs.DrugFlag = CType(16,Short)
        Me.GloUC_trvDrugs.DrugFormMember = Nothing
        Me.GloUC_trvDrugs.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvDrugs.DurationMember = Nothing
        Me.GloUC_trvDrugs.EducationMappingSearchType = 1
        Me.GloUC_trvDrugs.FrequencyMember = Nothing
        Me.GloUC_trvDrugs.HistoryType = Nothing
        Me.GloUC_trvDrugs.ICD9 = Nothing
        Me.GloUC_trvDrugs.ICDRevision = Nothing
        Me.GloUC_trvDrugs.ImageIndex = 0
        Me.GloUC_trvDrugs.ImageList = Me.ImageList1
        Me.GloUC_trvDrugs.ImageObject = Nothing
        Me.GloUC_trvDrugs.Indicator = Nothing
        Me.GloUC_trvDrugs.IsCPTSearch = false
        Me.GloUC_trvDrugs.IsDiagnosisSearch = false
        Me.GloUC_trvDrugs.IsDrug = false
        Me.GloUC_trvDrugs.IsNarcoticsMember = Nothing
        Me.GloUC_trvDrugs.IsSearchForEducationMapping = false
        Me.GloUC_trvDrugs.IsSystemCategory = Nothing
        Me.GloUC_trvDrugs.Location = New System.Drawing.Point(0, 54)
        Me.GloUC_trvDrugs.MaximumNodes = 1000
        Me.GloUC_trvDrugs.mpidmember = Nothing
        Me.GloUC_trvDrugs.Name = "GloUC_trvDrugs"
        Me.GloUC_trvDrugs.NDCCodeMember = Nothing
        Me.GloUC_trvDrugs.ParentImageIndex = 0
        Me.GloUC_trvDrugs.ParentMember = Nothing
        Me.GloUC_trvDrugs.RouteMember = Nothing
        Me.GloUC_trvDrugs.RowOrderMember = Nothing
        Me.GloUC_trvDrugs.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvDrugs.SearchBox = true
        Me.GloUC_trvDrugs.SearchText = Nothing
        Me.GloUC_trvDrugs.SelectedImageIndex = 0
        Me.GloUC_trvDrugs.SelectedNode = Nothing
        Me.GloUC_trvDrugs.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvDrugs.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvDrugs.SelectedParentImageIndex = 0
        Me.GloUC_trvDrugs.Size = New System.Drawing.Size(1077, 392)
        Me.GloUC_trvDrugs.SmartTreatmentId = Nothing
        Me.GloUC_trvDrugs.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvDrugs.TabIndex = 9
        Me.GloUC_trvDrugs.Tag = Nothing
        Me.GloUC_trvDrugs.UnitMember = Nothing
        Me.GloUC_trvDrugs.ValueMember = Nothing
        '
        'GloUC_TrvHistoryEx
        '
        Me.GloUC_TrvHistoryEx.AllergyClassID = Nothing
        Me.GloUC_TrvHistoryEx.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_TrvHistoryEx.CheckBoxes = false
        Me.GloUC_TrvHistoryEx.CodeMember = Nothing
        Me.GloUC_TrvHistoryEx.ColonAsSeparator = false
        Me.GloUC_TrvHistoryEx.Comment = Nothing
        Me.GloUC_TrvHistoryEx.ConceptID = Nothing
        Me.GloUC_TrvHistoryEx.CPT = Nothing
        Me.GloUC_TrvHistoryEx.DDIDMember = Nothing
        Me.GloUC_TrvHistoryEx.DescriptionMember = Nothing
        Me.GloUC_TrvHistoryEx.DisplayContextMenuStrip = Nothing
        Me.GloUC_TrvHistoryEx.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_TrvHistoryEx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_TrvHistoryEx.DrugFlag = CType(16,Short)
        Me.GloUC_TrvHistoryEx.DrugFormMember = Nothing
        Me.GloUC_TrvHistoryEx.DrugQtyQualifierMember = Nothing
        Me.GloUC_TrvHistoryEx.DurationMember = Nothing
        Me.GloUC_TrvHistoryEx.EducationMappingSearchType = 1
        Me.GloUC_TrvHistoryEx.FrequencyMember = Nothing
        Me.GloUC_TrvHistoryEx.HistoryType = Nothing
        Me.GloUC_TrvHistoryEx.ICD9 = Nothing
        Me.GloUC_TrvHistoryEx.ICDRevision = Nothing
        Me.GloUC_TrvHistoryEx.ImageIndex = 0
        Me.GloUC_TrvHistoryEx.ImageList = Me.ImageList1
        Me.GloUC_TrvHistoryEx.ImageObject = Nothing
        Me.GloUC_TrvHistoryEx.Indicator = Nothing
        Me.GloUC_TrvHistoryEx.IsCPTSearch = false
        Me.GloUC_TrvHistoryEx.IsDiagnosisSearch = false
        Me.GloUC_TrvHistoryEx.IsDrug = false
        Me.GloUC_TrvHistoryEx.IsNarcoticsMember = Nothing
        Me.GloUC_TrvHistoryEx.IsSearchForEducationMapping = false
        Me.GloUC_TrvHistoryEx.IsSystemCategory = Nothing
        Me.GloUC_TrvHistoryEx.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_TrvHistoryEx.MaximumNodes = 1000
        Me.GloUC_TrvHistoryEx.mpidmember = Nothing
        Me.GloUC_TrvHistoryEx.Name = "GloUC_TrvHistoryEx"
        Me.GloUC_TrvHistoryEx.NDCCodeMember = Nothing
        Me.GloUC_TrvHistoryEx.ParentImageIndex = 0
        Me.GloUC_TrvHistoryEx.ParentMember = Nothing
        Me.GloUC_TrvHistoryEx.RouteMember = Nothing
        Me.GloUC_TrvHistoryEx.RowOrderMember = Nothing
        Me.GloUC_TrvHistoryEx.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_TrvHistoryEx.SearchBox = true
        Me.GloUC_TrvHistoryEx.SearchText = Nothing
        Me.GloUC_TrvHistoryEx.SelectedImageIndex = 0
        Me.GloUC_TrvHistoryEx.SelectedNode = Nothing
        Me.GloUC_TrvHistoryEx.SelectedNodeIDs = CType(resources.GetObject("GloUC_TrvHistoryEx.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_TrvHistoryEx.SelectedParentImageIndex = 0
        Me.GloUC_TrvHistoryEx.Size = New System.Drawing.Size(790, 357)
        Me.GloUC_TrvHistoryEx.SmartTreatmentId = Nothing
        Me.GloUC_TrvHistoryEx.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_TrvHistoryEx.TabIndex = 48
        Me.GloUC_TrvHistoryEx.Tag = Nothing
        Me.GloUC_TrvHistoryEx.UnitMember = Nothing
        Me.GloUC_TrvHistoryEx.ValueMember = Nothing
        '
        'GloUC_trvInsurance_Ex
        '
        Me.GloUC_trvInsurance_Ex.AllergyClassID = Nothing
        Me.GloUC_trvInsurance_Ex.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvInsurance_Ex.CheckBoxes = false
        Me.GloUC_trvInsurance_Ex.CodeMember = Nothing
        Me.GloUC_trvInsurance_Ex.ColonAsSeparator = false
        Me.GloUC_trvInsurance_Ex.Comment = Nothing
        Me.GloUC_trvInsurance_Ex.ConceptID = Nothing
        Me.GloUC_trvInsurance_Ex.CPT = Nothing
        Me.GloUC_trvInsurance_Ex.DDIDMember = Nothing
        Me.GloUC_trvInsurance_Ex.DescriptionMember = Nothing
        Me.GloUC_trvInsurance_Ex.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvInsurance_Ex.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvInsurance_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvInsurance_Ex.DrugFlag = CType(16,Short)
        Me.GloUC_trvInsurance_Ex.DrugFormMember = Nothing
        Me.GloUC_trvInsurance_Ex.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvInsurance_Ex.DurationMember = Nothing
        Me.GloUC_trvInsurance_Ex.EducationMappingSearchType = 1
        Me.GloUC_trvInsurance_Ex.FrequencyMember = Nothing
        Me.GloUC_trvInsurance_Ex.HistoryType = Nothing
        Me.GloUC_trvInsurance_Ex.ICD9 = Nothing
        Me.GloUC_trvInsurance_Ex.ICDRevision = Nothing
        Me.GloUC_trvInsurance_Ex.ImageIndex = 0
        Me.GloUC_trvInsurance_Ex.ImageList = Me.ImageList1
        Me.GloUC_trvInsurance_Ex.ImageObject = Nothing
        Me.GloUC_trvInsurance_Ex.Indicator = Nothing
        Me.GloUC_trvInsurance_Ex.IsCPTSearch = false
        Me.GloUC_trvInsurance_Ex.IsDiagnosisSearch = false
        Me.GloUC_trvInsurance_Ex.IsDrug = false
        Me.GloUC_trvInsurance_Ex.IsNarcoticsMember = Nothing
        Me.GloUC_trvInsurance_Ex.IsSearchForEducationMapping = false
        Me.GloUC_trvInsurance_Ex.IsSystemCategory = Nothing
        Me.GloUC_trvInsurance_Ex.Location = New System.Drawing.Point(0, 54)
        Me.GloUC_trvInsurance_Ex.MaximumNodes = 1000
        Me.GloUC_trvInsurance_Ex.mpidmember = Nothing
        Me.GloUC_trvInsurance_Ex.Name = "GloUC_trvInsurance_Ex"
        Me.GloUC_trvInsurance_Ex.NDCCodeMember = Nothing
        Me.GloUC_trvInsurance_Ex.ParentImageIndex = 0
        Me.GloUC_trvInsurance_Ex.ParentMember = Nothing
        Me.GloUC_trvInsurance_Ex.RouteMember = Nothing
        Me.GloUC_trvInsurance_Ex.RowOrderMember = Nothing
        Me.GloUC_trvInsurance_Ex.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvInsurance_Ex.SearchBox = true
        Me.GloUC_trvInsurance_Ex.SearchText = Nothing
        Me.GloUC_trvInsurance_Ex.SelectedImageIndex = 0
        Me.GloUC_trvInsurance_Ex.SelectedNode = Nothing
        Me.GloUC_trvInsurance_Ex.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvInsurance_Ex.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvInsurance_Ex.SelectedParentImageIndex = 0
        Me.GloUC_trvInsurance_Ex.Size = New System.Drawing.Size(1077, 392)
        Me.GloUC_trvInsurance_Ex.SmartTreatmentId = Nothing
        Me.GloUC_trvInsurance_Ex.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvInsurance_Ex.TabIndex = 9
        Me.GloUC_trvInsurance_Ex.Tag = Nothing
        Me.GloUC_trvInsurance_Ex.UnitMember = Nothing
        Me.GloUC_trvInsurance_Ex.ValueMember = Nothing
        '
        'GloUC_trvCPT_Ex
        '
        Me.GloUC_trvCPT_Ex.AllergyClassID = Nothing
        Me.GloUC_trvCPT_Ex.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvCPT_Ex.CheckBoxes = false
        Me.GloUC_trvCPT_Ex.CodeMember = Nothing
        Me.GloUC_trvCPT_Ex.ColonAsSeparator = false
        Me.GloUC_trvCPT_Ex.Comment = Nothing
        Me.GloUC_trvCPT_Ex.ConceptID = Nothing
        Me.GloUC_trvCPT_Ex.CPT = Nothing
        Me.GloUC_trvCPT_Ex.DDIDMember = Nothing
        Me.GloUC_trvCPT_Ex.DescriptionMember = Nothing
        Me.GloUC_trvCPT_Ex.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvCPT_Ex.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvCPT_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvCPT_Ex.DrugFlag = CType(16,Short)
        Me.GloUC_trvCPT_Ex.DrugFormMember = Nothing
        Me.GloUC_trvCPT_Ex.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvCPT_Ex.DurationMember = Nothing
        Me.GloUC_trvCPT_Ex.EducationMappingSearchType = 1
        Me.GloUC_trvCPT_Ex.FrequencyMember = Nothing
        Me.GloUC_trvCPT_Ex.HistoryType = Nothing
        Me.GloUC_trvCPT_Ex.ICD9 = Nothing
        Me.GloUC_trvCPT_Ex.ICDRevision = Nothing
        Me.GloUC_trvCPT_Ex.ImageIndex = 0
        Me.GloUC_trvCPT_Ex.ImageList = Me.ImageList1
        Me.GloUC_trvCPT_Ex.ImageObject = Nothing
        Me.GloUC_trvCPT_Ex.Indicator = Nothing
        Me.GloUC_trvCPT_Ex.IsCPTSearch = false
        Me.GloUC_trvCPT_Ex.IsDiagnosisSearch = false
        Me.GloUC_trvCPT_Ex.IsDrug = false
        Me.GloUC_trvCPT_Ex.IsNarcoticsMember = Nothing
        Me.GloUC_trvCPT_Ex.IsSearchForEducationMapping = false
        Me.GloUC_trvCPT_Ex.IsSystemCategory = Nothing
        Me.GloUC_trvCPT_Ex.Location = New System.Drawing.Point(0, 54)
        Me.GloUC_trvCPT_Ex.MaximumNodes = 1000
        Me.GloUC_trvCPT_Ex.mpidmember = Nothing
        Me.GloUC_trvCPT_Ex.Name = "GloUC_trvCPT_Ex"
        Me.GloUC_trvCPT_Ex.NDCCodeMember = Nothing
        Me.GloUC_trvCPT_Ex.ParentImageIndex = 0
        Me.GloUC_trvCPT_Ex.ParentMember = Nothing
        Me.GloUC_trvCPT_Ex.RouteMember = Nothing
        Me.GloUC_trvCPT_Ex.RowOrderMember = Nothing
        Me.GloUC_trvCPT_Ex.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvCPT_Ex.SearchBox = true
        Me.GloUC_trvCPT_Ex.SearchText = Nothing
        Me.GloUC_trvCPT_Ex.SelectedImageIndex = 0
        Me.GloUC_trvCPT_Ex.SelectedNode = Nothing
        Me.GloUC_trvCPT_Ex.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvCPT_Ex.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvCPT_Ex.SelectedParentImageIndex = 0
        Me.GloUC_trvCPT_Ex.Size = New System.Drawing.Size(1077, 392)
        Me.GloUC_trvCPT_Ex.SmartTreatmentId = Nothing
        Me.GloUC_trvCPT_Ex.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvCPT_Ex.TabIndex = 46
        Me.GloUC_trvCPT_Ex.Tag = Nothing
        Me.GloUC_trvCPT_Ex.UnitMember = Nothing
        Me.GloUC_trvCPT_Ex.ValueMember = Nothing
        '
        'GloUC_trvDrugs_Ex
        '
        Me.GloUC_trvDrugs_Ex.AllergyClassID = Nothing
        Me.GloUC_trvDrugs_Ex.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvDrugs_Ex.CheckBoxes = false
        Me.GloUC_trvDrugs_Ex.CodeMember = Nothing
        Me.GloUC_trvDrugs_Ex.ColonAsSeparator = false
        Me.GloUC_trvDrugs_Ex.Comment = Nothing
        Me.GloUC_trvDrugs_Ex.ConceptID = Nothing
        Me.GloUC_trvDrugs_Ex.CPT = Nothing
        Me.GloUC_trvDrugs_Ex.DDIDMember = Nothing
        Me.GloUC_trvDrugs_Ex.DescriptionMember = Nothing
        Me.GloUC_trvDrugs_Ex.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvDrugs_Ex.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvDrugs_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvDrugs_Ex.DrugFlag = CType(16,Short)
        Me.GloUC_trvDrugs_Ex.DrugFormMember = Nothing
        Me.GloUC_trvDrugs_Ex.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvDrugs_Ex.DurationMember = Nothing
        Me.GloUC_trvDrugs_Ex.EducationMappingSearchType = 1
        Me.GloUC_trvDrugs_Ex.FrequencyMember = Nothing
        Me.GloUC_trvDrugs_Ex.HistoryType = Nothing
        Me.GloUC_trvDrugs_Ex.ICD9 = Nothing
        Me.GloUC_trvDrugs_Ex.ICDRevision = Nothing
        Me.GloUC_trvDrugs_Ex.ImageIndex = 0
        Me.GloUC_trvDrugs_Ex.ImageList = Me.ImageList1
        Me.GloUC_trvDrugs_Ex.ImageObject = Nothing
        Me.GloUC_trvDrugs_Ex.Indicator = Nothing
        Me.GloUC_trvDrugs_Ex.IsCPTSearch = false
        Me.GloUC_trvDrugs_Ex.IsDiagnosisSearch = false
        Me.GloUC_trvDrugs_Ex.IsDrug = false
        Me.GloUC_trvDrugs_Ex.IsNarcoticsMember = Nothing
        Me.GloUC_trvDrugs_Ex.IsSearchForEducationMapping = false
        Me.GloUC_trvDrugs_Ex.IsSystemCategory = Nothing
        Me.GloUC_trvDrugs_Ex.Location = New System.Drawing.Point(0, 54)
        Me.GloUC_trvDrugs_Ex.MaximumNodes = 1000
        Me.GloUC_trvDrugs_Ex.mpidmember = Nothing
        Me.GloUC_trvDrugs_Ex.Name = "GloUC_trvDrugs_Ex"
        Me.GloUC_trvDrugs_Ex.NDCCodeMember = Nothing
        Me.GloUC_trvDrugs_Ex.ParentImageIndex = 0
        Me.GloUC_trvDrugs_Ex.ParentMember = Nothing
        Me.GloUC_trvDrugs_Ex.RouteMember = Nothing
        Me.GloUC_trvDrugs_Ex.RowOrderMember = Nothing
        Me.GloUC_trvDrugs_Ex.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvDrugs_Ex.SearchBox = true
        Me.GloUC_trvDrugs_Ex.SearchText = Nothing
        Me.GloUC_trvDrugs_Ex.SelectedImageIndex = 0
        Me.GloUC_trvDrugs_Ex.SelectedNode = Nothing
        Me.GloUC_trvDrugs_Ex.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvDrugs_Ex.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvDrugs_Ex.SelectedParentImageIndex = 0
        Me.GloUC_trvDrugs_Ex.Size = New System.Drawing.Size(1077, 392)
        Me.GloUC_trvDrugs_Ex.SmartTreatmentId = Nothing
        Me.GloUC_trvDrugs_Ex.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvDrugs_Ex.TabIndex = 9
        Me.GloUC_trvDrugs_Ex.Tag = Nothing
        Me.GloUC_trvDrugs_Ex.UnitMember = Nothing
        Me.GloUC_trvDrugs_Ex.ValueMember = Nothing
        '
        'GloUC_trvICD10_Ex
        '
        Me.GloUC_trvICD10_Ex.AllergyClassID = Nothing
        Me.GloUC_trvICD10_Ex.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvICD10_Ex.CheckBoxes = false
        Me.GloUC_trvICD10_Ex.CodeMember = Nothing
        Me.GloUC_trvICD10_Ex.ColonAsSeparator = false
        Me.GloUC_trvICD10_Ex.Comment = Nothing
        Me.GloUC_trvICD10_Ex.ConceptID = Nothing
        Me.GloUC_trvICD10_Ex.CPT = Nothing
        Me.GloUC_trvICD10_Ex.DDIDMember = Nothing
        Me.GloUC_trvICD10_Ex.DescriptionMember = Nothing
        Me.GloUC_trvICD10_Ex.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvICD10_Ex.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvICD10_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvICD10_Ex.DrugFlag = CType(16,Short)
        Me.GloUC_trvICD10_Ex.DrugFormMember = Nothing
        Me.GloUC_trvICD10_Ex.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvICD10_Ex.DurationMember = Nothing
        Me.GloUC_trvICD10_Ex.EducationMappingSearchType = 1
        Me.GloUC_trvICD10_Ex.FrequencyMember = Nothing
        Me.GloUC_trvICD10_Ex.HistoryType = Nothing
        Me.GloUC_trvICD10_Ex.ICD9 = Nothing
        Me.GloUC_trvICD10_Ex.ICDRevision = Nothing
        Me.GloUC_trvICD10_Ex.ImageIndex = 0
        Me.GloUC_trvICD10_Ex.ImageList = Me.ImageList1
        Me.GloUC_trvICD10_Ex.ImageObject = Nothing
        Me.GloUC_trvICD10_Ex.Indicator = Nothing
        Me.GloUC_trvICD10_Ex.IsCPTSearch = false
        Me.GloUC_trvICD10_Ex.IsDiagnosisSearch = false
        Me.GloUC_trvICD10_Ex.IsDrug = false
        Me.GloUC_trvICD10_Ex.IsNarcoticsMember = Nothing
        Me.GloUC_trvICD10_Ex.IsSearchForEducationMapping = false
        Me.GloUC_trvICD10_Ex.IsSystemCategory = Nothing
        Me.GloUC_trvICD10_Ex.Location = New System.Drawing.Point(0, 444)
        Me.GloUC_trvICD10_Ex.MaximumNodes = 1000
        Me.GloUC_trvICD10_Ex.mpidmember = Nothing
        Me.GloUC_trvICD10_Ex.Name = "GloUC_trvICD10_Ex"
        Me.GloUC_trvICD10_Ex.NDCCodeMember = Nothing
        Me.GloUC_trvICD10_Ex.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.GloUC_trvICD10_Ex.ParentImageIndex = 0
        Me.GloUC_trvICD10_Ex.ParentMember = Nothing
        Me.GloUC_trvICD10_Ex.RouteMember = Nothing
        Me.GloUC_trvICD10_Ex.RowOrderMember = Nothing
        Me.GloUC_trvICD10_Ex.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvICD10_Ex.SearchBox = true
        Me.GloUC_trvICD10_Ex.SearchText = Nothing
        Me.GloUC_trvICD10_Ex.SelectedImageIndex = 0
        Me.GloUC_trvICD10_Ex.SelectedNode = Nothing
        Me.GloUC_trvICD10_Ex.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvICD10_Ex.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvICD10_Ex.SelectedParentImageIndex = 0
        Me.GloUC_trvICD10_Ex.Size = New System.Drawing.Size(1077, 390)
        Me.GloUC_trvICD10_Ex.SmartTreatmentId = Nothing
        Me.GloUC_trvICD10_Ex.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvICD10_Ex.TabIndex = 53
        Me.GloUC_trvICD10_Ex.Tag = Nothing
        Me.GloUC_trvICD10_Ex.UnitMember = Nothing
        Me.GloUC_trvICD10_Ex.ValueMember = Nothing
        '
        'GloUC_trvICD9_Ex
        '
        Me.GloUC_trvICD9_Ex.AllergyClassID = Nothing
        Me.GloUC_trvICD9_Ex.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvICD9_Ex.CheckBoxes = false
        Me.GloUC_trvICD9_Ex.CodeMember = Nothing
        Me.GloUC_trvICD9_Ex.ColonAsSeparator = false
        Me.GloUC_trvICD9_Ex.Comment = Nothing
        Me.GloUC_trvICD9_Ex.ConceptID = Nothing
        Me.GloUC_trvICD9_Ex.CPT = Nothing
        Me.GloUC_trvICD9_Ex.DDIDMember = Nothing
        Me.GloUC_trvICD9_Ex.DescriptionMember = Nothing
        Me.GloUC_trvICD9_Ex.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvICD9_Ex.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvICD9_Ex.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvICD9_Ex.DrugFlag = CType(16,Short)
        Me.GloUC_trvICD9_Ex.DrugFormMember = Nothing
        Me.GloUC_trvICD9_Ex.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvICD9_Ex.DurationMember = Nothing
        Me.GloUC_trvICD9_Ex.EducationMappingSearchType = 1
        Me.GloUC_trvICD9_Ex.FrequencyMember = Nothing
        Me.GloUC_trvICD9_Ex.HistoryType = Nothing
        Me.GloUC_trvICD9_Ex.ICD9 = Nothing
        Me.GloUC_trvICD9_Ex.ICDRevision = Nothing
        Me.GloUC_trvICD9_Ex.ImageIndex = 0
        Me.GloUC_trvICD9_Ex.ImageList = Me.ImageList1
        Me.GloUC_trvICD9_Ex.ImageObject = Nothing
        Me.GloUC_trvICD9_Ex.Indicator = Nothing
        Me.GloUC_trvICD9_Ex.IsCPTSearch = false
        Me.GloUC_trvICD9_Ex.IsDiagnosisSearch = false
        Me.GloUC_trvICD9_Ex.IsDrug = false
        Me.GloUC_trvICD9_Ex.IsNarcoticsMember = Nothing
        Me.GloUC_trvICD9_Ex.IsSearchForEducationMapping = false
        Me.GloUC_trvICD9_Ex.IsSystemCategory = Nothing
        Me.GloUC_trvICD9_Ex.Location = New System.Drawing.Point(0, 54)
        Me.GloUC_trvICD9_Ex.MaximumNodes = 1000
        Me.GloUC_trvICD9_Ex.mpidmember = Nothing
        Me.GloUC_trvICD9_Ex.Name = "GloUC_trvICD9_Ex"
        Me.GloUC_trvICD9_Ex.NDCCodeMember = Nothing
        Me.GloUC_trvICD9_Ex.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.GloUC_trvICD9_Ex.ParentImageIndex = 0
        Me.GloUC_trvICD9_Ex.ParentMember = Nothing
        Me.GloUC_trvICD9_Ex.RouteMember = Nothing
        Me.GloUC_trvICD9_Ex.RowOrderMember = Nothing
        Me.GloUC_trvICD9_Ex.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvICD9_Ex.SearchBox = true
        Me.GloUC_trvICD9_Ex.SearchText = Nothing
        Me.GloUC_trvICD9_Ex.SelectedImageIndex = 0
        Me.GloUC_trvICD9_Ex.SelectedNode = Nothing
        Me.GloUC_trvICD9_Ex.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvICD9_Ex.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvICD9_Ex.SelectedParentImageIndex = 0
        Me.GloUC_trvICD9_Ex.Size = New System.Drawing.Size(1077, 390)
        Me.GloUC_trvICD9_Ex.SmartTreatmentId = Nothing
        Me.GloUC_trvICD9_Ex.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvICD9_Ex.TabIndex = 47
        Me.GloUC_trvICD9_Ex.Tag = Nothing
        Me.GloUC_trvICD9_Ex.UnitMember = Nothing
        Me.GloUC_trvICD9_Ex.ValueMember = Nothing
        '
        'GloUC_trvAssociates
        '
        Me.GloUC_trvAssociates.AllergyClassID = Nothing
        Me.GloUC_trvAssociates.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvAssociates.CheckBoxes = false
        Me.GloUC_trvAssociates.CodeMember = Nothing
        Me.GloUC_trvAssociates.ColonAsSeparator = false
        Me.GloUC_trvAssociates.Comment = Nothing
        Me.GloUC_trvAssociates.ConceptID = Nothing
        Me.GloUC_trvAssociates.CPT = Nothing
        Me.GloUC_trvAssociates.DDIDMember = Nothing
        Me.GloUC_trvAssociates.DescriptionMember = Nothing
        Me.GloUC_trvAssociates.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
        Me.GloUC_trvAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvAssociates.DrugFlag = CType(16,Short)
        Me.GloUC_trvAssociates.DrugFormMember = Nothing
        Me.GloUC_trvAssociates.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvAssociates.DurationMember = Nothing
        Me.GloUC_trvAssociates.EducationMappingSearchType = 1
        Me.GloUC_trvAssociates.FrequencyMember = Nothing
        Me.GloUC_trvAssociates.HistoryType = Nothing
        Me.GloUC_trvAssociates.ICD9 = Nothing
        Me.GloUC_trvAssociates.ICDRevision = Nothing
        Me.GloUC_trvAssociates.ImageIndex = 0
        Me.GloUC_trvAssociates.ImageList = Me.ImageList1
        Me.GloUC_trvAssociates.ImageObject = Nothing
        Me.GloUC_trvAssociates.Indicator = Nothing
        Me.GloUC_trvAssociates.IsCPTSearch = false
        Me.GloUC_trvAssociates.IsDiagnosisSearch = false
        Me.GloUC_trvAssociates.IsDrug = false
        Me.GloUC_trvAssociates.IsNarcoticsMember = Nothing
        Me.GloUC_trvAssociates.IsSearchForEducationMapping = false
        Me.GloUC_trvAssociates.IsSystemCategory = Nothing
        Me.GloUC_trvAssociates.Location = New System.Drawing.Point(0, 30)
        Me.GloUC_trvAssociates.MaximumNodes = 1000
        Me.GloUC_trvAssociates.mpidmember = Nothing
        Me.GloUC_trvAssociates.Name = "GloUC_trvAssociates"
        Me.GloUC_trvAssociates.NDCCodeMember = Nothing
        Me.GloUC_trvAssociates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.GloUC_trvAssociates.ParentImageIndex = 0
        Me.GloUC_trvAssociates.ParentMember = Nothing
        Me.GloUC_trvAssociates.RouteMember = Nothing
        Me.GloUC_trvAssociates.RowOrderMember = Nothing
        Me.GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvAssociates.SearchBox = true
        Me.GloUC_trvAssociates.SearchText = Nothing
        Me.GloUC_trvAssociates.SelectedImageIndex = 0
        Me.GloUC_trvAssociates.SelectedNode = Nothing
        Me.GloUC_trvAssociates.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvAssociates.SelectedNodeIDs"),System.Collections.ArrayList)
        Me.GloUC_trvAssociates.SelectedParentImageIndex = 0
        Me.GloUC_trvAssociates.Size = New System.Drawing.Size(228, 517)
        Me.GloUC_trvAssociates.SmartTreatmentId = Nothing
        Me.GloUC_trvAssociates.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvAssociates.TabIndex = 25
        Me.GloUC_trvAssociates.Tag = Nothing
        Me.GloUC_trvAssociates.UnitMember = Nothing
        Me.GloUC_trvAssociates.ValueMember = Nothing
        '
        'frmDM_RulesSetup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1094, 872)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlMsgTOP)
        Me.Controls.Add(Me.pnl_tlstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmDM_RulesSetup"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clinical Recommendation Rule Setup"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(false)
        Me.tbCntrl_RuleSetup.ResumeLayout(false)
        Me.tbPg_Triggers.ResumeLayout(false)
        Me.pnlMiddle.ResumeLayout(false)
        Me.pnlDemoVitals.ResumeLayout(false)
        Me.Panel19.ResumeLayout(false)
        Me.Panel14.ResumeLayout(false)
        Me.Panel25.ResumeLayout(false)
        Me.pnlVitals.ResumeLayout(false)
        Me.pnlVitals.PerformLayout
        Me.Panel3.ResumeLayout(false)
        Me.Panel7.ResumeLayout(false)
        Me.pnlDemographics.ResumeLayout(false)
        Me.pnlDemographics.PerformLayout
        Me.Panel2.ResumeLayout(false)
        Me.Panel6.ResumeLayout(false)
        Me.PnlProblemList.ResumeLayout(false)
        Me.PnlProblemMiddle.ResumeLayout(false)
        Me.Pnlsnomedprb.ResumeLayout(false)
        Me.pnltrvSnowmedOff.ResumeLayout(false)
        Me.Panel24.ResumeLayout(false)
        Me.pnltrvfinprob.ResumeLayout(false)
        Me.Panel31.ResumeLayout(false)
        Me.PnlSrchProb.ResumeLayout(false)
        Me.PnlSrchProb.PerformLayout
        CType(Me.PictureBox3,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnltrvsubprb.ResumeLayout(false)
        Me.Panel34.ResumeLayout(false)
        Me.PnlProblemSearch.ResumeLayout(false)
        Me.Panel26.ResumeLayout(false)
        Me.PnlProbLeft.ResumeLayout(false)
        Me.Panel35.ResumeLayout(false)
        Me.Panel36.ResumeLayout(false)
        Me.Panel37.ResumeLayout(false)
        Me.Panel38.ResumeLayout(false)
        Me.Panel39.ResumeLayout(false)
        Me.Panel39.PerformLayout
        Me.pnlRadiology.ResumeLayout(false)
        Me.Panel18.ResumeLayout(false)
        CType(Me.c1Labs,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel17.ResumeLayout(false)
        Me.Panel17.PerformLayout
        CType(Me.PictureBox6,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlInternalToolStripRadiology.ResumeLayout(false)
        Me.Panel41.ResumeLayout(false)
        Me.Panel41.PerformLayout
        Me.ToolStrip6.ResumeLayout(false)
        Me.ToolStrip6.PerformLayout
        Me.pnlHistory.ResumeLayout(false)
        Me.Panel22.ResumeLayout(false)
        Me.Panel20.ResumeLayout(false)
        Me.Panel21.ResumeLayout(false)
        Me.Panel16.ResumeLayout(false)
        Me.Panel9.ResumeLayout(false)
        Me.pnlHistoryLeft.ResumeLayout(false)
        Me.Panel11.ResumeLayout(false)
        Me.Panel8.ResumeLayout(false)
        Me.Panel8.PerformLayout
        Me.pnlInternalToolStripHistory.ResumeLayout(false)
        Me.Panel33.ResumeLayout(false)
        Me.Panel33.PerformLayout
        Me.ToolStrip4.ResumeLayout(false)
        Me.ToolStrip4.PerformLayout
        Me.pnlInsurance.ResumeLayout(false)
        Me.pnltrvSelectedInsurance.ResumeLayout(false)
        Me.pnlSelectedInsuranceLabel.ResumeLayout(false)
        Me.Panel56.ResumeLayout(false)
        Me.pnlInternalToolStripInsurance.ResumeLayout(false)
        Me.Panel58.ResumeLayout(false)
        Me.Panel58.PerformLayout
        Me.ToolStrip13.ResumeLayout(false)
        Me.ToolStrip13.PerformLayout
        Me.pnlICD9.ResumeLayout(false)
        Me.Panel15.ResumeLayout(false)
        Me.Panel5.ResumeLayout(false)
        Me.Panel10.ResumeLayout(false)
        Me.pnlInternalToolStripICD.ResumeLayout(false)
        Me.Panel29.ResumeLayout(false)
        Me.Panel29.PerformLayout
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        Me.pnlCPT.ResumeLayout(false)
        Me.pnlSelectedCPTs.ResumeLayout(false)
        Me.pnlSelecteCPTsLabels.ResumeLayout(false)
        Me.Panel28.ResumeLayout(false)
        Me.pnlInternalToolStripCPT.ResumeLayout(false)
        Me.Panel30.ResumeLayout(false)
        Me.Panel30.PerformLayout
        Me.ToolStrip2.ResumeLayout(false)
        Me.ToolStrip2.PerformLayout
        Me.pnlDrugs.ResumeLayout(false)
        Me.pnltrvSelectedDrugs.ResumeLayout(false)
        Me.pnlSelectedDrugLabel.ResumeLayout(false)
        Me.Panel4.ResumeLayout(false)
        Me.pnlInternalToolStripDrugs.ResumeLayout(false)
        Me.Panel32.ResumeLayout(false)
        Me.Panel32.PerformLayout
        Me.ToolStrip3.ResumeLayout(false)
        Me.ToolStrip3.PerformLayout
        Me.pnlLab.ResumeLayout(false)
        Me.Panel12.ResumeLayout(false)
        CType(Me.C1LabResult,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel13.ResumeLayout(false)
        Me.Panel23.ResumeLayout(false)
        Me.Panel23.PerformLayout
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlInternalToolStripLab.ResumeLayout(false)
        Me.Panel40.ResumeLayout(false)
        Me.Panel40.PerformLayout
        Me.ToolStrip5.ResumeLayout(false)
        Me.ToolStrip5.PerformLayout
        Me.tbPg_Exceptions.ResumeLayout(false)
        Me.pnlExceptionsMiddle.ResumeLayout(false)
        Me.pnlExceptionsDemoVitals.ResumeLayout(false)
        Me.Panel45.ResumeLayout(false)
        Me.Panel46.ResumeLayout(false)
        Me.Panel47.ResumeLayout(false)
        Me.pnlVitals_Ex.ResumeLayout(false)
        Me.pnlVitals_Ex.PerformLayout
        Me.Panel49.ResumeLayout(false)
        Me.Panel50.ResumeLayout(false)
        Me.pnlDemographics_Ex.ResumeLayout(false)
        Me.pnlDemographics_Ex.PerformLayout
        Me.Panel52.ResumeLayout(false)
        Me.Panel53.ResumeLayout(false)
        Me.pnlExceptionsRadiology.ResumeLayout(false)
        Me.Panel18_Ex.ResumeLayout(false)
        CType(Me.c1Labs_Ex,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel17_Ex.ResumeLayout(false)
        Me.Panel17_Ex.PerformLayout
        CType(Me.PictureBox5,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlInternalToolStripRadiology_Ex.ResumeLayout(false)
        Me.Panel76.ResumeLayout(false)
        Me.Panel76.PerformLayout
        Me.ToolStrip7.ResumeLayout(false)
        Me.ToolStrip7.PerformLayout
        Me.pnlExceptionsHistory.ResumeLayout(false)
        Me.Panel22_Ex.ResumeLayout(false)
        Me.Panel20_Ex.ResumeLayout(false)
        Me.Panel80.ResumeLayout(false)
        Me.Panel16_Ex.ResumeLayout(false)
        Me.Panel9_Ex.ResumeLayout(false)
        Me.Panel83.ResumeLayout(false)
        Me.Panel11_Ex.ResumeLayout(false)
        Me.Panel8_Ex.ResumeLayout(false)
        Me.Panel8_Ex.PerformLayout
        Me.pnlInternalToolStripHistory_Ex.ResumeLayout(false)
        Me.Panel87.ResumeLayout(false)
        Me.Panel87.PerformLayout
        Me.ToolStrip8.ResumeLayout(false)
        Me.ToolStrip8.PerformLayout
        Me.pnlExceptionsInsurance.ResumeLayout(false)
        Me.pnltrvSelectedInsurance_Ex.ResumeLayout(false)
        Me.pnlSelectedInsuranceLabel_Ex.ResumeLayout(false)
        Me.Panel57.ResumeLayout(false)
        Me.pnlInternalToolStripInsurance_Ex.ResumeLayout(false)
        Me.Panel60.ResumeLayout(false)
        Me.Panel60.PerformLayout
        Me.ToolStrip14.ResumeLayout(false)
        Me.ToolStrip14.PerformLayout
        Me.pnlExceptionsCPT.ResumeLayout(false)
        Me.pnlSelectedCPTs_Ex.ResumeLayout(false)
        Me.pnlSelectedCPTsLabels_Ex.ResumeLayout(false)
        Me.Panel97.ResumeLayout(false)
        Me.pnlInternalToolStripCPT_Ex.ResumeLayout(false)
        Me.Panel99.ResumeLayout(false)
        Me.Panel99.PerformLayout
        Me.ToolStrip10.ResumeLayout(false)
        Me.ToolStrip10.PerformLayout
        Me.pnlExceptionsDrugs.ResumeLayout(false)
        Me.pnltrvSelectedDrugs_Ex.ResumeLayout(false)
        Me.pnlSelectedDrugLabel_Ex.ResumeLayout(false)
        Me.Panel103.ResumeLayout(false)
        Me.pnlInternalToolStripDrugs_Ex.ResumeLayout(false)
        Me.Panel105.ResumeLayout(false)
        Me.Panel105.PerformLayout
        Me.ToolStrip11.ResumeLayout(false)
        Me.ToolStrip11.PerformLayout
        Me.pnlExceptionsLab.ResumeLayout(false)
        Me.Panel112_Ex.ResumeLayout(false)
        CType(Me.C1LabResult_Ex,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel13_Ex.ResumeLayout(false)
        Me.Panel23_Ex.ResumeLayout(false)
        Me.Panel23_Ex.PerformLayout
        CType(Me.PictureBox7,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlInternalToolStripLab_Ex.ResumeLayout(false)
        Me.Panel111.ResumeLayout(false)
        Me.Panel111.PerformLayout
        Me.ToolStrip12.ResumeLayout(false)
        Me.ToolStrip12.PerformLayout
        Me.pnlExceptionsICD9.ResumeLayout(false)
        Me.Panel15_Ex.ResumeLayout(false)
        Me.Panel5_Ex.ResumeLayout(false)
        Me.Panel91.ResumeLayout(false)
        Me.pnlInternalToolStripICD_Ex.ResumeLayout(false)
        Me.Panel93.ResumeLayout(false)
        Me.Panel93.PerformLayout
        Me.ToolStrip9.ResumeLayout(false)
        Me.ToolStrip9.PerformLayout
        Me.tbPg_QuickOrders.ResumeLayout(false)
        Me.pnlSummaryOthers.ResumeLayout(false)
        Me.pnlGuideline.ResumeLayout(false)
        Me.pnlGuidelineHeader.ResumeLayout(false)
        Me.pnl3.ResumeLayout(false)
        Me.pnlRight.ResumeLayout(false)
        Me.pnltrvTriggers.ResumeLayout(false)
        Me.pnltxtSearchOrder.ResumeLayout(false)
        Me.pnltxtSearchOrder.PerformLayout
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.pnlbtnLab.ResumeLayout(false)
        Me.pnlbtnReferrals.ResumeLayout(false)
        Me.pnlbtnRx.ResumeLayout(false)
        Me.pnlbtnRadiologyTest.ResumeLayout(false)
        Me.pnlbtnGuideline.ResumeLayout(false)
        Me.pnlIM.ResumeLayout(false)
        Me.tbPg_RefInfo.ResumeLayout(false)
        Me.Panel44.ResumeLayout(false)
        Me.Panel44.PerformLayout
        Me.Panel1.ResumeLayout(false)
        Me.Panel43.ResumeLayout(false)
        Me.pnlMsgTOP.ResumeLayout(false)
        Me.pnlMsg.ResumeLayout(false)
        Me.pnlMsg.PerformLayout
        Me.Panel27.ResumeLayout(false)
        Me.pnlRecurring.ResumeLayout(false)
        Me.pnlRecurring.PerformLayout
        Me.pnlRecurrenceControls.ResumeLayout(false)
        Me.pnlRecurrenceControls.PerformLayout
        Me.Panel42.ResumeLayout(false)
        Me.Panel42.PerformLayout
        Me.pnl_tlstrip.ResumeLayout(false)
        Me.pnl_tlstrip.PerformLayout
        Me.pnlgloStreamLogo.ResumeLayout(false)
        CType(Me.PictureBox4,System.ComponentModel.ISupportInitialize).EndInit
        Me.tlsDM.ResumeLayout(false)
        Me.tlsDM.PerformLayout
        Me.ContextMenuStrip2.ResumeLayout(false)
        Me.ContextMenuStrip1.ResumeLayout(false)
        Me.ContextMenuHistory.ResumeLayout(false)
        Me.CmnuStripCPT.ResumeLayout(false)
        Me.CmnustripICD.ResumeLayout(false)
        Me.cntFindings.ResumeLayout(false)
        Me.ContextMenuProblem.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

#Region " Windows Controls "
    Friend WithEvents cntFindingsprb As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnLabs As System.Windows.Forms.Button
    Friend WithEvents C1LabResult As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnl_tlstrip As System.Windows.Forms.Panel
    Friend WithEvents pnlGuideline As System.Windows.Forms.Panel
    Friend WithEvents pnl3 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tlsDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsDM_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsDM_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMsg As System.Windows.Forms.Panel
    Friend WithEvents pnlDemoVitals As System.Windows.Forms.Panel
    Friend WithEvents pnlLab As System.Windows.Forms.Panel
    Friend WithEvents trOrderInfo As System.Windows.Forms.TreeView
    Friend WithEvents CntConditions As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReferral As System.Windows.Forms.MenuItem
    Friend WithEvents EditReferral As System.Windows.Forms.MenuItem
    Friend WithEvents btnHistorySearch As System.Windows.Forms.Button
    Friend WithEvents sptLeft As System.Windows.Forms.Splitter
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents pnlMsgTOP As System.Windows.Forms.Panel
    Friend WithEvents pnlGuidelineHeader As System.Windows.Forms.Panel
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label97 As System.Windows.Forms.Label
    Private WithEvents Label98 As System.Windows.Forms.Label
    Private WithEvents Label99 As System.Windows.Forms.Label
    Private WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents Label101 As System.Windows.Forms.Label
    Private WithEvents Label102 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label111 As System.Windows.Forms.Label
    Private WithEvents Label112 As System.Windows.Forms.Label
    Private WithEvents Label113 As System.Windows.Forms.Label
    Private WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label107 As System.Windows.Forms.Label
    Private WithEvents Label108 As System.Windows.Forms.Label
    Private WithEvents Label109 As System.Windows.Forms.Label
    Private WithEvents Label110 As System.Windows.Forms.Label
    Private WithEvents Label103 As System.Windows.Forms.Label
    Private WithEvents Label104 As System.Windows.Forms.Label
    Private WithEvents Label105 As System.Windows.Forms.Label
    Private WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label89 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label120 As System.Windows.Forms.Label
    Private WithEvents Label121 As System.Windows.Forms.Label
    Private WithEvents Label122 As System.Windows.Forms.Label
    Private WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Private WithEvents Label146 As System.Windows.Forms.Label
    Private WithEvents Label147 As System.Windows.Forms.Label
    Private WithEvents Label148 As System.Windows.Forms.Label
    Private WithEvents Label149 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Private WithEvents Label170 As System.Windows.Forms.Label
    Private WithEvents Label171 As System.Windows.Forms.Label
    Private WithEvents Label172 As System.Windows.Forms.Label
    Private WithEvents Label173 As System.Windows.Forms.Label
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label165 As System.Windows.Forms.Label
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Private WithEvents Label166 As System.Windows.Forms.Label
    Private WithEvents Label167 As System.Windows.Forms.Label
    Private WithEvents Label168 As System.Windows.Forms.Label
    Private WithEvents Label169 As System.Windows.Forms.Label
    Friend WithEvents mnuEditTemplate As System.Windows.Forms.MenuItem
    Friend WithEvents pnltrvSelectedDrugs As System.Windows.Forms.Panel
    Private WithEvents Label86 As System.Windows.Forms.Label
    Private WithEvents Label180 As System.Windows.Forms.Label
    Friend WithEvents trvSelectedDrugs As System.Windows.Forms.TreeView
    Private WithEvents Label181 As System.Windows.Forms.Label
    Private WithEvents Label182 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectedDrugLabel As System.Windows.Forms.Panel
    Private WithEvents Label183 As System.Windows.Forms.Label
    Private WithEvents Label184 As System.Windows.Forms.Label
    Private WithEvents Label185 As System.Windows.Forms.Label
    Private WithEvents Label186 As System.Windows.Forms.Label
    Friend WithEvents Label187 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteDrugs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteInsurance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents cmbHistoryCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Private WithEvents Label188 As System.Windows.Forms.Label
    Private WithEvents Label189 As System.Windows.Forms.Label
    Private WithEvents Label190 As System.Windows.Forms.Label
    Friend WithEvents Label191 As System.Windows.Forms.Label
    Private WithEvents Label192 As System.Windows.Forms.Label
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Private WithEvents Label193 As System.Windows.Forms.Label
    Private WithEvents Label194 As System.Windows.Forms.Label
    Friend WithEvents trvSelectedHistory As System.Windows.Forms.TreeView
    Private WithEvents Label195 As System.Windows.Forms.Label
    Private WithEvents Label196 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuHistory As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents GloUC_trvCPT As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents GloUC_trvICD9 As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlSelecteCPTsLabels As System.Windows.Forms.Panel
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Private WithEvents Label209 As System.Windows.Forms.Label
    Private WithEvents Label210 As System.Windows.Forms.Label
    Private WithEvents Label211 As System.Windows.Forms.Label
    Friend WithEvents Label212 As System.Windows.Forms.Label
    Private WithEvents Label213 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectedCPTs As System.Windows.Forms.Panel
    Private WithEvents Label205 As System.Windows.Forms.Label
    Private WithEvents Label206 As System.Windows.Forms.Label
    Friend WithEvents trvselectedCPT As System.Windows.Forms.TreeView
    Private WithEvents Label207 As System.Windows.Forms.Label
    Private WithEvents Label208 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Private WithEvents Label132 As System.Windows.Forms.Label
    Private WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents trvselecteICDs As System.Windows.Forms.TreeView
    Private WithEvents Label134 As System.Windows.Forms.Label
    Private WithEvents Label135 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label128 As System.Windows.Forms.Label
    Private WithEvents Label129 As System.Windows.Forms.Label
    Friend WithEvents Label130 As System.Windows.Forms.Label
    Private WithEvents Label131 As System.Windows.Forms.Label
    Friend WithEvents GloUC_trvDrugs As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents CmnuStripCPT As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuItem_DeleteCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CmnustripICD As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuItem_DeleteICD As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GloUC_trvHistory As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter4 As System.Windows.Forms.Splitter
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label115 As System.Windows.Forms.Label
    Private WithEvents Label116 As System.Windows.Forms.Label
    Friend WithEvents Label118 As System.Windows.Forms.Label
    Friend WithEvents Label117 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents txtLabResultSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents Label127 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label136 As System.Windows.Forms.Label
    Private WithEvents Label137 As System.Windows.Forms.Label
    Private WithEvents Label138 As System.Windows.Forms.Label
    Private WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents Label140 As System.Windows.Forms.Label
    Friend WithEvents Label141 As System.Windows.Forms.Label
    Friend WithEvents btnLabResultClear As System.Windows.Forms.Button
    Friend WithEvents btnLabClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnproblemlist As System.Windows.Forms.Button
    Friend WithEvents PnlProblemList As System.Windows.Forms.Panel
    Friend WithEvents PnlProblemMiddle As System.Windows.Forms.Panel
    Friend WithEvents PnlProblemSearch As System.Windows.Forms.Panel
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Friend WithEvents trvprobright As System.Windows.Forms.TreeView
    Friend WithEvents PnlProbLeft As System.Windows.Forms.Panel
    Private WithEvents Label179 As System.Windows.Forms.Label
    Private WithEvents Label197 As System.Windows.Forms.Label
    Private WithEvents Label198 As System.Windows.Forms.Label
    Private WithEvents Label199 As System.Windows.Forms.Label
    Friend WithEvents trvproblem As System.Windows.Forms.TreeView
    Friend WithEvents Pnlsnomedprb As System.Windows.Forms.Panel
    Friend WithEvents pnltrvfinprob As System.Windows.Forms.Panel
    Friend WithEvents trvfinprob As System.Windows.Forms.TreeView
    Friend WithEvents Panel31 As System.Windows.Forms.Panel
    Friend WithEvents Label201 As System.Windows.Forms.Label
    Private WithEvents Label203 As System.Windows.Forms.Label
    Friend WithEvents PnlSrchProb As System.Windows.Forms.Panel
    Friend WithEvents txtsrchprb As System.Windows.Forms.TextBox
    'Friend WithEvents ContextMenuProblem As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Label215 As System.Windows.Forms.Label
    Friend WithEvents Label216 As System.Windows.Forms.Label
    Friend WithEvents btnclrprb As System.Windows.Forms.Button
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents Label217 As System.Windows.Forms.Label
    Private WithEvents Label218 As System.Windows.Forms.Label
    Private WithEvents Label219 As System.Windows.Forms.Label
    Private WithEvents Label220 As System.Windows.Forms.Label
    Friend WithEvents Splitter6 As System.Windows.Forms.Splitter
    Friend WithEvents pnltrvsubprb As System.Windows.Forms.Panel
    Private WithEvents Label221 As System.Windows.Forms.Label
    Friend WithEvents trvsubprb As System.Windows.Forms.TreeView
    Friend WithEvents Panel34 As System.Windows.Forms.Panel
    Friend WithEvents Label222 As System.Windows.Forms.Label
    Private WithEvents Label223 As System.Windows.Forms.Label
    Private WithEvents Label224 As System.Windows.Forms.Label
    Private WithEvents Label225 As System.Windows.Forms.Label
    Private WithEvents Label226 As System.Windows.Forms.Label
    Friend WithEvents Panel35 As System.Windows.Forms.Panel
    Friend WithEvents Panel36 As System.Windows.Forms.Panel
    Private WithEvents Label227 As System.Windows.Forms.Label
    Private WithEvents Label228 As System.Windows.Forms.Label
    Private WithEvents Label229 As System.Windows.Forms.Label
    Friend WithEvents Label230 As System.Windows.Forms.Label
    Private WithEvents Label231 As System.Windows.Forms.Label
    Friend WithEvents Panel37 As System.Windows.Forms.Panel
    Private WithEvents Label232 As System.Windows.Forms.Label
    Private WithEvents Label233 As System.Windows.Forms.Label
    Friend WithEvents trvselectedprob As System.Windows.Forms.TreeView
    Private WithEvents Label234 As System.Windows.Forms.Label
    Private WithEvents Label235 As System.Windows.Forms.Label
    Friend WithEvents Panel38 As System.Windows.Forms.Panel
    Friend WithEvents Panel39 As System.Windows.Forms.Panel
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Private WithEvents Label237 As System.Windows.Forms.Label
    Private WithEvents Label238 As System.Windows.Forms.Label
    Private WithEvents Label239 As System.Windows.Forms.Label
    Private WithEvents Label240 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents mnuDeleteHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cntFindings As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnu_AddFindings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDeleteProblem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnltrvSnowmedOff As System.Windows.Forms.Panel
    Private WithEvents Label142 As System.Windows.Forms.Label
    Friend WithEvents trvSnowmedOff As System.Windows.Forms.TreeView
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents Label143 As System.Windows.Forms.Label
    Private WithEvents Label144 As System.Windows.Forms.Label
    Private WithEvents Label150 As System.Windows.Forms.Label
    Private WithEvents Label151 As System.Windows.Forms.Label
    Private WithEvents Label152 As System.Windows.Forms.Label
    Private WithEvents Label154 As System.Windows.Forms.Label
    Private WithEvents Label153 As System.Windows.Forms.Label
    Friend WithEvents trvselectedhist As System.Windows.Forms.TreeView
    Friend WithEvents cmbhistsnomed As System.Windows.Forms.ComboBox
    Friend WithEvents lblsnohistcat As System.Windows.Forms.Label
    Friend WithEvents cmbAgeMaxMnth As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAgeMinMnth As System.Windows.Forms.ComboBox
    Friend WithEvents Label158 As System.Windows.Forms.Label
    Friend WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents cmbRace As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGender As System.Windows.Forms.ComboBox
    Friend WithEvents Label157 As System.Windows.Forms.Label
    Friend WithEvents lblAgeMax As System.Windows.Forms.Label
    Friend WithEvents Label156 As System.Windows.Forms.Label
    Friend WithEvents Label200 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Private WithEvents Label202 As System.Windows.Forms.Label
    Private WithEvents Label204 As System.Windows.Forms.Label
    Private WithEvents Label214 As System.Windows.Forms.Label
    Private WithEvents Label236 As System.Windows.Forms.Label
    Friend WithEvents Label241 As System.Windows.Forms.Label
    Friend WithEvents Label251 As System.Windows.Forms.Label
    Friend WithEvents Label250 As System.Windows.Forms.Label
    Friend WithEvents Label249 As System.Windows.Forms.Label
    Friend WithEvents Label248 As System.Windows.Forms.Label
    Friend WithEvents Label254 As System.Windows.Forms.Label
    Friend WithEvents Label253 As System.Windows.Forms.Label
    Friend WithEvents Label252 As System.Windows.Forms.Label
    Friend WithEvents Label260 As System.Windows.Forms.Label
    Friend WithEvents Label258 As System.Windows.Forms.Label
    Friend WithEvents Label256 As System.Windows.Forms.Label
    Friend WithEvents Label259 As System.Windows.Forms.Label
    Friend WithEvents Label257 As System.Windows.Forms.Label
    Friend WithEvents Label255 As System.Windows.Forms.Label
    Friend WithEvents pnlInternalToolStripICD As System.Windows.Forms.Panel
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveICD As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_CancelICD As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClearHistory As System.Windows.Forms.Button
    Friend WithEvents btnClearCPT As System.Windows.Forms.Button
    Friend WithEvents btnClearICD As System.Windows.Forms.Button
    Friend WithEvents pnlInternalToolStripHistory As System.Windows.Forms.Panel
    Friend WithEvents Panel33 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip4 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_CancelHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlInternalToolStripCPT As System.Windows.Forms.Panel
    Friend WithEvents Panel30 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip2 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveCPT As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_CancelCPT As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlInternalToolStripDrugs As System.Windows.Forms.Panel
    Friend WithEvents Panel32 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip3 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveDrugs As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_CancelDrugs As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlInternalToolStripRadiology As System.Windows.Forms.Panel
    Friend WithEvents Panel41 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip6 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveRadiology As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_CancelRadiology As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlInternalToolStripLab As System.Windows.Forms.Panel
    Friend WithEvents Panel40 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip5 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveLab As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_CancelLab As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbChkBoxRace As PresentationControls.CheckBoxComboBox
    Friend WithEvents cmbChkBoxMaritalSt As PresentationControls.CheckBoxComboBox
    Friend WithEvents cmbChkBoxGender As PresentationControls.CheckBoxComboBox
    Friend WithEvents lstVw_ICD9 As System.Windows.Forms.ListView
    Friend WithEvents lstVw_COL_ICDCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstVw_CPT As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstVw_History As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbCntrl_RuleSetup As System.Windows.Forms.TabControl
    Friend WithEvents tbPg_Triggers As System.Windows.Forms.TabPage
    Friend WithEvents tbPg_Exceptions As System.Windows.Forms.TabPage
    Friend WithEvents tbPg_QuickOrders As System.Windows.Forms.TabPage
    Friend WithEvents lstVw_Orders As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstVw_Drugs As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstVw_Lab As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClearOrders As System.Windows.Forms.Button
    Friend WithEvents btnClearDrugs As System.Windows.Forms.Button
    Friend WithEvents btnClearLab As System.Windows.Forms.Button
    Friend WithEvents btnHistory As System.Windows.Forms.Button
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents GloUC_trvAssociates As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnltrvTriggers As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Private WithEvents Label76 As System.Windows.Forms.Label
    Private WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents pnltxtSearchOrder As System.Windows.Forms.Panel
    Friend WithEvents txtSearchOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Private WithEvents Label83 As System.Windows.Forms.Label
    Private WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnLab As System.Windows.Forms.Panel
    Friend WithEvents btnLab As System.Windows.Forms.Button
    Private WithEvents Label78 As System.Windows.Forms.Label
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnReferrals As System.Windows.Forms.Panel
    Friend WithEvents btnReferrals As System.Windows.Forms.Button
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnRx As System.Windows.Forms.Panel
    Friend WithEvents btnRx As System.Windows.Forms.Button
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnRadiologyTest As System.Windows.Forms.Panel
    Friend WithEvents btnRadiologyTest As System.Windows.Forms.Button
    Private WithEvents Label55 As System.Windows.Forms.Label
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnGuideline As System.Windows.Forms.Panel
    Friend WithEvents btnGuideline As System.Windows.Forms.Button
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnlIM As System.Windows.Forms.Panel
    Friend WithEvents btnIM As System.Windows.Forms.Button
    Private WithEvents Label145 As System.Windows.Forms.Label
    Private WithEvents Label159 As System.Windows.Forms.Label
    Private WithEvents Label160 As System.Windows.Forms.Label
    Private WithEvents Label161 As System.Windows.Forms.Label
    Private WithEvents Panel27 As System.Windows.Forms.Panel
    Private WithEvents Panel42 As System.Windows.Forms.Panel
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents chkIsActiveRule As System.Windows.Forms.CheckBox
    Friend WithEvents pnlExceptionsMiddle As System.Windows.Forms.Panel
    Friend WithEvents pnlExceptionsDemoVitals As System.Windows.Forms.Panel
    Friend WithEvents Panel45 As System.Windows.Forms.Panel
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents btnClearICD9Ex As System.Windows.Forms.Button
    Friend WithEvents btnClearCPTEx As System.Windows.Forms.Button
    Friend WithEvents lstExVw_Orders As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstExVw_Drugs As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClearHistoryEx As System.Windows.Forms.Button
    Friend WithEvents lstExVw_Lab As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstExVw_ICD As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClearOrdersEx As System.Windows.Forms.Button
    Friend WithEvents lstExVw_CPT As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClearDrugsEx As System.Windows.Forms.Button
    Friend WithEvents lstExVw_History As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClearLabEx As System.Windows.Forms.Button
    Friend WithEvents btnHistoryEx As System.Windows.Forms.Button
    Friend WithEvents btnRadiologyEx As System.Windows.Forms.Button
    Friend WithEvents btnDrugsEx As System.Windows.Forms.Button
    Friend WithEvents btnLabEx As System.Windows.Forms.Button
    Friend WithEvents btnICD9Ex As System.Windows.Forms.Button
    Friend WithEvents btnCPTEx As System.Windows.Forms.Button
    Friend WithEvents btnproblemlist_Ex As System.Windows.Forms.Button
    Friend WithEvents btnDemographics_Ex As System.Windows.Forms.Button
    Friend WithEvents Panel46 As System.Windows.Forms.Panel
    Friend WithEvents Panel47 As System.Windows.Forms.Panel
    Private WithEvents Label59 As System.Windows.Forms.Label
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label61 As System.Windows.Forms.Label
    Private WithEvents Label162 As System.Windows.Forms.Label
    Friend WithEvents Label163 As System.Windows.Forms.Label
    Friend WithEvents pnlVitals_Ex As System.Windows.Forms.Panel
    Friend WithEvents Label164 As System.Windows.Forms.Label
    Friend WithEvents Label174 As System.Windows.Forms.Label
    Friend WithEvents Label175 As System.Windows.Forms.Label
    Friend WithEvents Label176 As System.Windows.Forms.Label
    Friend WithEvents Label177 As System.Windows.Forms.Label
    Friend WithEvents Label178 As System.Windows.Forms.Label
    Friend WithEvents Label244 As System.Windows.Forms.Label
    Friend WithEvents Label245 As System.Windows.Forms.Label
    Friend WithEvents Label263 As System.Windows.Forms.Label
    Friend WithEvents Label264 As System.Windows.Forms.Label
    Friend WithEvents Label265 As System.Windows.Forms.Label
    Friend WithEvents Label266 As System.Windows.Forms.Label
    Friend WithEvents Label267 As System.Windows.Forms.Label
    Private WithEvents Label268 As System.Windows.Forms.Label
    Private WithEvents Label269 As System.Windows.Forms.Label
    Private WithEvents Label270 As System.Windows.Forms.Label
    Private WithEvents Label271 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMin_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label272 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMaxInch_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label273 As System.Windows.Forms.Label
    Friend WithEvents Label274 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMinInch_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label275 As System.Windows.Forms.Label
    Friend WithEvents txtPulseOXmax_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label276 As System.Windows.Forms.Label
    Friend WithEvents txtPulseOXmin_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label277 As System.Windows.Forms.Label
    Friend WithEvents txtPulseMax_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label278 As System.Windows.Forms.Label
    Friend WithEvents txtPulseMin_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label279 As System.Windows.Forms.Label
    Friend WithEvents txtTemperatureMax_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label280 As System.Windows.Forms.Label
    Friend WithEvents txtBMImax_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label281 As System.Windows.Forms.Label
    Friend WithEvents txtBMImin_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label282 As System.Windows.Forms.Label
    Friend WithEvents txtWeightMax_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label283 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMax_Ex As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMax_Ex As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMin_Ex As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMax_Ex As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMin_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label284 As System.Windows.Forms.Label
    Friend WithEvents Label285 As System.Windows.Forms.Label
    Friend WithEvents Label286 As System.Windows.Forms.Label
    Friend WithEvents Label287 As System.Windows.Forms.Label
    Friend WithEvents txtTemperatureMin_Ex As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightMin_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label288 As System.Windows.Forms.Label
    Friend WithEvents Label289 As System.Windows.Forms.Label
    Friend WithEvents Label290 As System.Windows.Forms.Label
    Friend WithEvents Panel49 As System.Windows.Forms.Panel
    Friend WithEvents Panel50 As System.Windows.Forms.Panel
    Private WithEvents Label291 As System.Windows.Forms.Label
    Private WithEvents Label292 As System.Windows.Forms.Label
    Private WithEvents Label293 As System.Windows.Forms.Label
    Private WithEvents Label294 As System.Windows.Forms.Label
    Friend WithEvents Label295 As System.Windows.Forms.Label
    Friend WithEvents pnlDemographics_Ex As System.Windows.Forms.Panel
    Friend WithEvents cmbChkBoxMaritalSt_Ex As PresentationControls.CheckBoxComboBox
    Friend WithEvents cmbGender_Ex As PresentationControls.CheckBoxComboBox
    Friend WithEvents cmbChkBoxRace_Ex As PresentationControls.CheckBoxComboBox
    Friend WithEvents txtCity_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label296 As System.Windows.Forms.Label
    Friend WithEvents Label297 As System.Windows.Forms.Label
    Friend WithEvents Label298 As System.Windows.Forms.Label
    Friend WithEvents cmbState_Ex As System.Windows.Forms.ComboBox
    Friend WithEvents Label299 As System.Windows.Forms.Label
    Friend WithEvents Label300 As System.Windows.Forms.Label
    Friend WithEvents txtZip_Ex As System.Windows.Forms.TextBox
    Friend WithEvents Label301 As System.Windows.Forms.Label
    Friend WithEvents Label302 As System.Windows.Forms.Label
    Friend WithEvents Label303 As System.Windows.Forms.Label
    Friend WithEvents cmbEmpStatus_Ex As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAgeMaxMnth_Ex As System.Windows.Forms.ComboBox
    Friend WithEvents Label304 As System.Windows.Forms.Label
    Friend WithEvents cmbAgeMinMnth_Ex As System.Windows.Forms.ComboBox
    Private WithEvents Label305 As System.Windows.Forms.Label
    Private WithEvents Label306 As System.Windows.Forms.Label
    Private WithEvents Label307 As System.Windows.Forms.Label
    Private WithEvents Label308 As System.Windows.Forms.Label
    Friend WithEvents cmbAgeMax_Ex As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAgeMin_Ex As System.Windows.Forms.ComboBox
    Friend WithEvents Label309 As System.Windows.Forms.Label
    Friend WithEvents Label310 As System.Windows.Forms.Label
    Friend WithEvents Label311 As System.Windows.Forms.Label
    Friend WithEvents Label312 As System.Windows.Forms.Label
    Friend WithEvents Label313 As System.Windows.Forms.Label
    Friend WithEvents Label314 As System.Windows.Forms.Label
    Friend WithEvents Label315 As System.Windows.Forms.Label
    Friend WithEvents Label316 As System.Windows.Forms.Label
    Friend WithEvents Panel52 As System.Windows.Forms.Panel
    Friend WithEvents Panel53 As System.Windows.Forms.Panel
    Private WithEvents Label317 As System.Windows.Forms.Label
    Private WithEvents Label318 As System.Windows.Forms.Label
    Private WithEvents Label319 As System.Windows.Forms.Label
    Private WithEvents Label320 As System.Windows.Forms.Label
    Friend WithEvents Label321 As System.Windows.Forms.Label
    Friend WithEvents pnlExceptionsRadiology As System.Windows.Forms.Panel
    Friend WithEvents Panel18_Ex As System.Windows.Forms.Panel
    Private WithEvents Label362 As System.Windows.Forms.Label
    Private WithEvents Label363 As System.Windows.Forms.Label
    Private WithEvents Label364 As System.Windows.Forms.Label
    Private WithEvents Label365 As System.Windows.Forms.Label
    Friend WithEvents c1Labs_Ex As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel17_Ex As System.Windows.Forms.Panel
    Friend WithEvents txtLabsSearch_Ex As System.Windows.Forms.TextBox
    Friend WithEvents bntLabClear_Ex As System.Windows.Forms.Button
    Friend WithEvents Label366 As System.Windows.Forms.Label
    Friend WithEvents Label367 As System.Windows.Forms.Label
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Private WithEvents Label368 As System.Windows.Forms.Label
    Private WithEvents Label369 As System.Windows.Forms.Label
    Private WithEvents Label370 As System.Windows.Forms.Label
    Private WithEvents Label371 As System.Windows.Forms.Label
    Friend WithEvents pnlInternalToolStripRadiology_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel76 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip7 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_Btn_SaveRadiology_Ex As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlExceptionsHistory As System.Windows.Forms.Panel
    Friend WithEvents Panel22_Ex As System.Windows.Forms.Panel
    Private WithEvents Label372 As System.Windows.Forms.Label
    Private WithEvents Label373 As System.Windows.Forms.Label
    Friend WithEvents trvSelectedHistory_Ex As System.Windows.Forms.TreeView
    Private WithEvents Label374 As System.Windows.Forms.Label
    Private WithEvents Label375 As System.Windows.Forms.Label
    Friend WithEvents Panel20_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel80 As System.Windows.Forms.Panel
    Private WithEvents Label376 As System.Windows.Forms.Label
    Private WithEvents Label377 As System.Windows.Forms.Label
    Private WithEvents Label378 As System.Windows.Forms.Label
    Friend WithEvents Label379 As System.Windows.Forms.Label
    Private WithEvents Label380 As System.Windows.Forms.Label
    Friend WithEvents Panel16_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel9_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel11_Ex As System.Windows.Forms.Panel
    Friend WithEvents GloUC_TrvHistoryEx As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents trvHistoryRight_Ex As System.Windows.Forms.TreeView
    Friend WithEvents Panel83 As System.Windows.Forms.Panel
    Private WithEvents Label381 As System.Windows.Forms.Label
    Private WithEvents Label382 As System.Windows.Forms.Label
    Private WithEvents Label383 As System.Windows.Forms.Label
    Private WithEvents Label384 As System.Windows.Forms.Label
    Friend WithEvents TreeView10 As System.Windows.Forms.TreeView
    Friend WithEvents Panel84 As System.Windows.Forms.Panel
    Friend WithEvents Panel8_Ex As System.Windows.Forms.Panel
    Friend WithEvents cmbHistoryCategory_Ex As System.Windows.Forms.ComboBox
    Friend WithEvents Label385 As System.Windows.Forms.Label
    Friend WithEvents txtSearch_Ex As System.Windows.Forms.TextBox
    Private WithEvents Label386 As System.Windows.Forms.Label
    Private WithEvents Label387 As System.Windows.Forms.Label
    Private WithEvents Label388 As System.Windows.Forms.Label
    Private WithEvents Label389 As System.Windows.Forms.Label
    Friend WithEvents btnHistorySearch_Ex As System.Windows.Forms.Button
    Friend WithEvents pnlInternalToolStripHistory_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel87 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip8 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveHistory_Ex As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlExceptionsICD9 As System.Windows.Forms.Panel
    Friend WithEvents Panel15_Ex As System.Windows.Forms.Panel
    Private WithEvents Label390 As System.Windows.Forms.Label
    Private WithEvents Label391 As System.Windows.Forms.Label
    Friend WithEvents trvselectedICDs_Ex As System.Windows.Forms.TreeView
    Private WithEvents Label392 As System.Windows.Forms.Label
    Private WithEvents Label393 As System.Windows.Forms.Label
    Friend WithEvents Panel5_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel91 As System.Windows.Forms.Panel
    Private WithEvents Label394 As System.Windows.Forms.Label
    Private WithEvents Label395 As System.Windows.Forms.Label
    Private WithEvents Label396 As System.Windows.Forms.Label
    Friend WithEvents Label397 As System.Windows.Forms.Label
    Private WithEvents Label398 As System.Windows.Forms.Label
    Friend WithEvents Splitter7 As System.Windows.Forms.Splitter
    Friend WithEvents GloUC_trvICD9_Ex As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlInternalToolStripICD_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel93 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip9 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveICD_Ex As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlExceptionsCPT As System.Windows.Forms.Panel
    Friend WithEvents pnlSelectedCPTs_Ex As System.Windows.Forms.Panel
    Private WithEvents Label399 As System.Windows.Forms.Label
    Private WithEvents Label400 As System.Windows.Forms.Label
    Friend WithEvents trvselectedCPT_Ex As System.Windows.Forms.TreeView
    Private WithEvents Label401 As System.Windows.Forms.Label
    Private WithEvents Label402 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectedCPTsLabels_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel97 As System.Windows.Forms.Panel
    Private WithEvents Label403 As System.Windows.Forms.Label
    Private WithEvents Label404 As System.Windows.Forms.Label
    Private WithEvents Label405 As System.Windows.Forms.Label
    Friend WithEvents Label406 As System.Windows.Forms.Label
    Private WithEvents Label407 As System.Windows.Forms.Label
    Friend WithEvents Splitter8 As System.Windows.Forms.Splitter
    Friend WithEvents GloUC_trvCPT_Ex As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlInternalToolStripCPT_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel99 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip10 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveCPT_Ex As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlExceptionsDrugs As System.Windows.Forms.Panel
    Friend WithEvents pnltrvSelectedDrugs_Ex As System.Windows.Forms.Panel
    Private WithEvents Label408 As System.Windows.Forms.Label
    Private WithEvents Label409 As System.Windows.Forms.Label
    Friend WithEvents trvSelectedDrugs_Ex As System.Windows.Forms.TreeView
    Private WithEvents Label410 As System.Windows.Forms.Label
    Private WithEvents Label411 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectedDrugLabel_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel103 As System.Windows.Forms.Panel
    Private WithEvents Label412 As System.Windows.Forms.Label
    Private WithEvents Label413 As System.Windows.Forms.Label
    Private WithEvents Label414 As System.Windows.Forms.Label
    Friend WithEvents Label415 As System.Windows.Forms.Label
    Private WithEvents Label416 As System.Windows.Forms.Label
    Friend WithEvents Splitter9 As System.Windows.Forms.Splitter
    Friend WithEvents GloUC_trvDrugs_Ex As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlInternalToolStripDrugs_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel105 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip11 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveDrugs_Ex As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton10 As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlExceptionsLab As System.Windows.Forms.Panel
    Friend WithEvents Panel112_Ex As System.Windows.Forms.Panel
    Private WithEvents Label417 As System.Windows.Forms.Label
    Friend WithEvents C1LabResult_Ex As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label418 As System.Windows.Forms.Label
    Private WithEvents Label419 As System.Windows.Forms.Label
    Private WithEvents Label420 As System.Windows.Forms.Label
    Friend WithEvents Panel13_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel23_Ex As System.Windows.Forms.Panel
    Friend WithEvents txtLabResultSearch_Ex As System.Windows.Forms.TextBox
    Friend WithEvents btnLabResultClear_Ex As System.Windows.Forms.Button
    Friend WithEvents Label421 As System.Windows.Forms.Label
    Friend WithEvents Label422 As System.Windows.Forms.Label
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Private WithEvents Label423 As System.Windows.Forms.Label
    Private WithEvents Label424 As System.Windows.Forms.Label
    Private WithEvents Label425 As System.Windows.Forms.Label
    Private WithEvents Label426 As System.Windows.Forms.Label
    Friend WithEvents pnlInternalToolStripLab_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel111 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip12 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveLab_Ex As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton12 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContextMenuProblem As System.Windows.Forms.ContextMenuStrip
    '' chetan integrated for snomed problem list
    ' Friend WithEvents mnuDeleteProblem As System.Windows.Forms.ToolStripMenuItem
#End Region

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents btnDemographics As System.Windows.Forms.Button
    Friend WithEvents cmbMaritalSt As System.Windows.Forms.ComboBox
    Friend WithEvents pnlDemographics As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlVitals As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents cmbState As System.Windows.Forms.ComboBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents cmbEmpStatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtBPsettingMin As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMax As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMin As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMax As System.Windows.Forms.TextBox
    Friend WithEvents pnlHistory As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlHistoryLeft As System.Windows.Forms.Panel
    Friend WithEvents trvHistory As System.Windows.Forms.TreeView
    Friend WithEvents trvHistoryRight As System.Windows.Forms.TreeView
    Friend WithEvents txtLabsSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlDrugs As System.Windows.Forms.Panel
    Friend WithEvents pnlICD9 As System.Windows.Forms.Panel
    Friend WithEvents pnlCPT As System.Windows.Forms.Panel
    Friend WithEvents pnlRadiology As System.Windows.Forms.Panel
    Friend WithEvents pnlSummaryOthers As System.Windows.Forms.Panel
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents btnRadiology As System.Windows.Forms.Button
    Friend WithEvents btnCPT As System.Windows.Forms.Button
    Friend WithEvents btnICD9 As System.Windows.Forms.Button
    Friend WithEvents btnDrugs As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents cmbAgeMin As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAgeMax As System.Windows.Forms.ComboBox
    Friend WithEvents txtTemperatureMax As System.Windows.Forms.TextBox
    Friend WithEvents txtBMImax As System.Windows.Forms.TextBox
    Friend WithEvents txtBMImin As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightMax As System.Windows.Forms.TextBox
    Friend WithEvents txtHeightMax As System.Windows.Forms.TextBox
    Friend WithEvents txtTemperatureMin As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightMin As System.Windows.Forms.TextBox
    Friend WithEvents txtHeightMin As System.Windows.Forms.TextBox
    Friend WithEvents txtPulseOXmax As System.Windows.Forms.TextBox
    Friend WithEvents txtPulseOXmin As System.Windows.Forms.TextBox
    Friend WithEvents txtPulseMax As System.Windows.Forms.TextBox
    Friend WithEvents txtPulseMin As System.Windows.Forms.TextBox
    Friend WithEvents lblPulseOXMax As System.Windows.Forms.Label
    Friend WithEvents lblPulseOXMin As System.Windows.Forms.Label
    Friend WithEvents lblPulseMax As System.Windows.Forms.Label
    Friend WithEvents lblPulseMin As System.Windows.Forms.Label
    Friend WithEvents lblTempratureMax As System.Windows.Forms.Label
    Friend WithEvents lblBMImax As System.Windows.Forms.Label
    Friend WithEvents lblBMImin As System.Windows.Forms.Label
    Friend WithEvents lblWeightMax As System.Windows.Forms.Label
    Friend WithEvents lblBPStandingMax As System.Windows.Forms.Label
    Friend WithEvents lblBPStandingMin As System.Windows.Forms.Label
    Friend WithEvents lblBPSittingMax As System.Windows.Forms.Label
    Friend WithEvents lblBPSittingMin As System.Windows.Forms.Label
    Friend WithEvents lblWeightMin As System.Windows.Forms.Label
    Friend WithEvents lblTempratureMin As System.Windows.Forms.Label
    Friend WithEvents lblHeightMin As System.Windows.Forms.Label
    Friend WithEvents lblEmpStatus As System.Windows.Forms.Label
    Friend WithEvents lblZip As System.Windows.Forms.Label
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents lblMaritalStatus As System.Windows.Forms.Label
    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents lblRace As System.Windows.Forms.Label
    Friend WithEvents lblAgeMin As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents c1Labs As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMinInch As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtHeightMaxInch As System.Windows.Forms.TextBox
    Friend WithEvents btnClearSnomed As System.Windows.Forms.Button
    Friend WithEvents btnSnomed As System.Windows.Forms.Button
    Friend WithEvents lstVw_SnoMed As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnRemoveSelectedSnomedCode As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedSnomedCodeEx As System.Windows.Forms.Button
    Friend WithEvents btnClearSnomedEx As System.Windows.Forms.Button
    Friend WithEvents btnSnomedEx As System.Windows.Forms.Button
    Friend WithEvents lstExVw_SnoMed As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlRecurring As System.Windows.Forms.Panel
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents cmbDurationType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents chckRecurring As System.Windows.Forms.CheckBox
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents pnlRecurrenceControls As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtBPstandingMaxTo As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMinTo As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMaxTo As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMinTo As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents label52 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents label51 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label87 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents txtBPstandingMax_Ex_To As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMin_Ex_To As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMax_Ex_To As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMin_Ex_To As System.Windows.Forms.TextBox
    Friend WithEvents lblHealthPlanAlert As System.Windows.Forms.Label
    Friend WithEvents tbPg_RefInfo As System.Windows.Forms.TabPage
    Friend WithEvents Panel44 As System.Windows.Forms.Panel
    Private WithEvents Label242 As System.Windows.Forms.Label
    Private WithEvents Label243 As System.Windows.Forms.Label
    Friend WithEvents Label328 As System.Windows.Forms.Label
    Friend WithEvents Label327 As System.Windows.Forms.Label
    Friend WithEvents Label326 As System.Windows.Forms.Label
    Friend WithEvents Label325 As System.Windows.Forms.Label
    Friend WithEvents Label247 As System.Windows.Forms.Label
    Private WithEvents Label246 As System.Windows.Forms.Label
    Friend WithEvents txtRevisionDates As System.Windows.Forms.TextBox
    Private WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents txtRelease As System.Windows.Forms.TextBox
    Friend WithEvents txtFundingSource As System.Windows.Forms.TextBox
    Friend WithEvents txtBibliographicCitation As System.Windows.Forms.TextBox
    Friend WithEvents txtInterventionDeveloper As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel43 As System.Windows.Forms.Panel
    Private WithEvents Label261 As System.Windows.Forms.Label
    Private WithEvents Label262 As System.Windows.Forms.Label
    Private WithEvents Label322 As System.Windows.Forms.Label
    Private WithEvents Label323 As System.Windows.Forms.Label
    Friend WithEvents Label324 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveSelectedDrug As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedOrders As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedICD9 As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedLab As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedCPT As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedHistory As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedHistory_Ex As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedDrug_Ex As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedOrders_Ex As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedICD9_Ex As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedLab_Ex As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedCPT_Ex As System.Windows.Forms.Button
    Friend WithEvents chkSpecialAlert As System.Windows.Forms.CheckBox
    Friend WithEvents btnRemoveSelectedICD10 As System.Windows.Forms.Button
    Friend WithEvents btnClearICD10 As System.Windows.Forms.Button
    Friend WithEvents lstVw_ICD10 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnICD10 As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedICD10_Ex As System.Windows.Forms.Button
    Friend WithEvents btnClearICD10Ex As System.Windows.Forms.Button
    Friend WithEvents lstExVw_ICD10 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnICD10Ex As System.Windows.Forms.Button
    Friend WithEvents tsBtn_SaveICD10 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_SaveICD10_Ex As System.Windows.Forms.ToolStripButton
    Friend WithEvents GloUC_trvICD10 As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents GloUC_trvICD10_Ex As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents trvselecteICD10s As System.Windows.Forms.TreeView
    Friend WithEvents trvselectedICD10s_Ex As System.Windows.Forms.TreeView
    Friend WithEvents pnlgloStreamLogo As System.Windows.Forms.Panel
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel48 As System.Windows.Forms.Panel
    Friend WithEvents sptRight As System.Windows.Forms.Splitter
    Friend WithEvents btnRemoveSelectedInsurance As System.Windows.Forms.Button
    Friend WithEvents lstVw_Insurance As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClearInsurance As System.Windows.Forms.Button
    Friend WithEvents btnInsurance As System.Windows.Forms.Button
    Friend WithEvents pnlInsurance As System.Windows.Forms.Panel
    Friend WithEvents pnltrvSelectedInsurance As System.Windows.Forms.Panel
    Private WithEvents Label329 As System.Windows.Forms.Label
    Private WithEvents Label330 As System.Windows.Forms.Label
    Friend WithEvents trvSelectedInsurance As System.Windows.Forms.TreeView
    Private WithEvents Label331 As System.Windows.Forms.Label
    Private WithEvents Label332 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectedInsuranceLabel As System.Windows.Forms.Panel
    Friend WithEvents Panel56 As System.Windows.Forms.Panel
    Private WithEvents Label333 As System.Windows.Forms.Label
    Private WithEvents Label334 As System.Windows.Forms.Label
    Private WithEvents Label335 As System.Windows.Forms.Label
    Friend WithEvents Label336 As System.Windows.Forms.Label
    Private WithEvents Label337 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents GloUC_trvInsurance As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlInternalToolStripInsurance As System.Windows.Forms.Panel
    Friend WithEvents Panel58 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip13 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveInsurance As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_CancelInsurance As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRemoveSelectedInsurance_Ex As System.Windows.Forms.Button
    Friend WithEvents lstExVw_Insurance As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClearInsuranceEx As System.Windows.Forms.Button
    Friend WithEvents btnInsuranceEx As System.Windows.Forms.Button
    Friend WithEvents pnlExceptionsInsurance As System.Windows.Forms.Panel
    Friend WithEvents pnltrvSelectedInsurance_Ex As System.Windows.Forms.Panel
    Private WithEvents Label338 As System.Windows.Forms.Label
    Private WithEvents Label339 As System.Windows.Forms.Label
    Friend WithEvents trvSelectedInsurance_Ex As System.Windows.Forms.TreeView
    Private WithEvents Label340 As System.Windows.Forms.Label
    Private WithEvents Label341 As System.Windows.Forms.Label
    Friend WithEvents pnlSelectedInsuranceLabel_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel57 As System.Windows.Forms.Panel
    Private WithEvents Label342 As System.Windows.Forms.Label
    Private WithEvents Label343 As System.Windows.Forms.Label
    Private WithEvents Label344 As System.Windows.Forms.Label
    Friend WithEvents Label345 As System.Windows.Forms.Label
    Private WithEvents Label346 As System.Windows.Forms.Label
    Friend WithEvents Splitter5 As System.Windows.Forms.Splitter
    Friend WithEvents GloUC_trvInsurance_Ex As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlInternalToolStripInsurance_Ex As System.Windows.Forms.Panel
    Friend WithEvents Panel60 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip14 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveInsurance_Ex As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_CancelInsurance_Ex As System.Windows.Forms.ToolStripButton
    Private WithEvents Label24 As System.Windows.Forms.Label

End Class