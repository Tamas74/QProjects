<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCQM_RulesSetup
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

                Try
                    'If (IsNothing(dtEndDate) = False) Then
                    '    Try
                    '        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtEndDate)
                    '    Catch ex As Exception

                    '    End Try


                    '    dtEndDate.Dispose()
                    '    dtEndDate = Nothing
                    'End If
                Catch
                End Try

                'Try
                '    If (IsNothing(dtStartDate) = False) Then
                '        Try
                '            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtStartDate)
                '        Catch ex As Exception

                '        End Try


                '        dtStartDate.Dispose()
                '        dtStartDate = Nothing
                '    End If
                'Catch
                ' End Try

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
        Dim colICD9 As System.Windows.Forms.ColumnHeader
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCQM_RulesSetup))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tbCntrl_RuleSetup = New System.Windows.Forms.TabControl()
        Me.tbPg_Triggers = New System.Windows.Forms.TabPage()
        Me.pnlMiddle = New System.Windows.Forms.Panel()
        Me.pnlDemoVitals = New System.Windows.Forms.Panel()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.btnRemoveSelectedCVX = New System.Windows.Forms.Button()
        Me.btnClearCVX = New System.Windows.Forms.Button()
        Me.lstVw_CVX = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btncvx = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedICD10 = New System.Windows.Forms.Button()
        Me.btnClearICD10 = New System.Windows.Forms.Button()
        Me.lstVw_ICD10 = New System.Windows.Forms.ListView()
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnICD10 = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedDrug = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedICD9 = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedLab = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedCPT = New System.Windows.Forms.Button()
        Me.btnRemoveSelectedSnomedCode = New System.Windows.Forms.Button()
        Me.btnClearSnomed = New System.Windows.Forms.Button()
        Me.btnSnomed = New System.Windows.Forms.Button()
        Me.lstVw_SnoMed = New System.Windows.Forms.ListView()
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnClearICD = New System.Windows.Forms.Button()
        Me.btnClearCPT = New System.Windows.Forms.Button()
        Me.lstVw_Drugs = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstVw_Lab = New System.Windows.Forms.ListView()
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstVw_ICD9 = New System.Windows.Forms.ListView()
        Me.lstVw_CPT = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnClearDrugs = New System.Windows.Forms.Button()
        Me.btnClearLab = New System.Windows.Forms.Button()
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
        Me.Label258 = New System.Windows.Forms.Label()
        Me.Label259 = New System.Windows.Forms.Label()
        Me.Label257 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.txtBMImax = New System.Windows.Forms.TextBox()
        Me.lblBMImax = New System.Windows.Forms.Label()
        Me.txtBMImin = New System.Windows.Forms.TextBox()
        Me.lblBMImin = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlDemographics = New System.Windows.Forms.Panel()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.cmbAgeMaxMnth = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMinMnth = New System.Windows.Forms.ComboBox()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.cmbAgeMax = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMin = New System.Windows.Forms.ComboBox()
        Me.lblAgeMax = New System.Windows.Forms.Label()
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
        Me.GloUC_trvHistory = New gloUserControlLibrary.gloUC_TreeView()
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
        Me.GloUC_trvInsurance = New gloUserControlLibrary.gloUC_TreeView()
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
        Me.GloUC_trvICD10 = New gloUserControlLibrary.gloUC_TreeView()
        Me.GloUC_trvICD9 = New gloUserControlLibrary.gloUC_TreeView()
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
        Me.GloUC_trvCPT = New gloUserControlLibrary.gloUC_TreeView()
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
        Me.GloUC_trvDrugs = New gloUserControlLibrary.gloUC_TreeView()
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
        Me.Panel84 = New System.Windows.Forms.Panel()
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmbMaritalSt = New System.Windows.Forms.ComboBox()
        Me.cmbRace = New System.Windows.Forms.ComboBox()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.pnlMsgTOP = New System.Windows.Forms.Panel()
        Me.pnlMsg = New System.Windows.Forms.Panel()
        Me.lnkhelp = New System.Windows.Forms.LinkLabel()
        Me.cmb_measure = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.pnl_tlstrip = New System.Windows.Forms.Panel()
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
        Me.lstVw_COL_ICDCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        colICD9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlMain.SuspendLayout()
        Me.tbCntrl_RuleSetup.SuspendLayout()
        Me.tbPg_Triggers.SuspendLayout()
        Me.pnlMiddle.SuspendLayout()
        Me.pnlDemoVitals.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel25.SuspendLayout()
        Me.pnlVitals.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlDemographics.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.PnlProblemList.SuspendLayout()
        Me.PnlProblemMiddle.SuspendLayout()
        Me.Pnlsnomedprb.SuspendLayout()
        Me.pnltrvSnowmedOff.SuspendLayout()
        Me.Panel24.SuspendLayout()
        Me.pnltrvfinprob.SuspendLayout()
        Me.Panel31.SuspendLayout()
        Me.PnlSrchProb.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnltrvsubprb.SuspendLayout()
        Me.Panel34.SuspendLayout()
        Me.PnlProblemSearch.SuspendLayout()
        Me.Panel26.SuspendLayout()
        Me.PnlProbLeft.SuspendLayout()
        Me.Panel35.SuspendLayout()
        Me.Panel36.SuspendLayout()
        Me.Panel37.SuspendLayout()
        Me.Panel38.SuspendLayout()
        Me.Panel39.SuspendLayout()
        Me.pnlRadiology.SuspendLayout()
        Me.Panel18.SuspendLayout()
        CType(Me.c1Labs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel17.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInternalToolStripRadiology.SuspendLayout()
        Me.Panel41.SuspendLayout()
        Me.ToolStrip6.SuspendLayout()
        Me.pnlHistory.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlHistoryLeft.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnlInternalToolStripHistory.SuspendLayout()
        Me.Panel33.SuspendLayout()
        Me.ToolStrip4.SuspendLayout()
        Me.pnlInsurance.SuspendLayout()
        Me.pnltrvSelectedInsurance.SuspendLayout()
        Me.pnlSelectedInsuranceLabel.SuspendLayout()
        Me.Panel56.SuspendLayout()
        Me.pnlInternalToolStripInsurance.SuspendLayout()
        Me.Panel58.SuspendLayout()
        Me.ToolStrip13.SuspendLayout()
        Me.pnlICD9.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.pnlInternalToolStripICD.SuspendLayout()
        Me.Panel29.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlCPT.SuspendLayout()
        Me.pnlSelectedCPTs.SuspendLayout()
        Me.pnlSelecteCPTsLabels.SuspendLayout()
        Me.Panel28.SuspendLayout()
        Me.pnlInternalToolStripCPT.SuspendLayout()
        Me.Panel30.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.pnlDrugs.SuspendLayout()
        Me.pnltrvSelectedDrugs.SuspendLayout()
        Me.pnlSelectedDrugLabel.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlInternalToolStripDrugs.SuspendLayout()
        Me.Panel32.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        Me.pnlLab.SuspendLayout()
        Me.Panel12.SuspendLayout()
        CType(Me.C1LabResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel13.SuspendLayout()
        Me.Panel23.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInternalToolStripLab.SuspendLayout()
        Me.Panel40.SuspendLayout()
        Me.ToolStrip5.SuspendLayout()
        Me.pnlMsgTOP.SuspendLayout()
        Me.pnlMsg.SuspendLayout()
        Me.pnl_tlstrip.SuspendLayout()
        Me.tlsDM.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuHistory.SuspendLayout()
        Me.CmnuStripCPT.SuspendLayout()
        Me.CmnustripICD.SuspendLayout()
        Me.cntFindings.SuspendLayout()
        Me.ContextMenuProblem.SuspendLayout()
        Me.SuspendLayout()
        '
        'colICD9
        '
        colICD9.Text = "ICD9"
        colICD9.Width = 410
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.tbCntrl_RuleSetup)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 94)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1184, 868)
        Me.pnlMain.TabIndex = 0
        '
        'tbCntrl_RuleSetup
        '
        Me.tbCntrl_RuleSetup.Controls.Add(Me.tbPg_Triggers)
        Me.tbCntrl_RuleSetup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbCntrl_RuleSetup.Location = New System.Drawing.Point(0, 0)
        Me.tbCntrl_RuleSetup.Name = "tbCntrl_RuleSetup"
        Me.tbCntrl_RuleSetup.SelectedIndex = 0
        Me.tbCntrl_RuleSetup.Size = New System.Drawing.Size(1184, 868)
        Me.tbCntrl_RuleSetup.TabIndex = 334
        '
        'tbPg_Triggers
        '
        Me.tbPg_Triggers.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbPg_Triggers.Controls.Add(Me.pnlMiddle)
        Me.tbPg_Triggers.Location = New System.Drawing.Point(4, 23)
        Me.tbPg_Triggers.Name = "tbPg_Triggers"
        Me.tbPg_Triggers.Padding = New System.Windows.Forms.Padding(3)
        Me.tbPg_Triggers.Size = New System.Drawing.Size(1176, 841)
        Me.tbPg_Triggers.TabIndex = 0
        Me.tbPg_Triggers.Text = "Triggers  "
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
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
        Me.pnlMiddle.Size = New System.Drawing.Size(1170, 835)
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
        Me.pnlDemoVitals.Size = New System.Drawing.Size(1170, 835)
        Me.pnlDemoVitals.TabIndex = 0
        '
        'Panel19
        '
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedCVX)
        Me.Panel19.Controls.Add(Me.btnClearCVX)
        Me.Panel19.Controls.Add(Me.lstVw_CVX)
        Me.Panel19.Controls.Add(Me.btncvx)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedICD10)
        Me.Panel19.Controls.Add(Me.btnClearICD10)
        Me.Panel19.Controls.Add(Me.lstVw_ICD10)
        Me.Panel19.Controls.Add(Me.btnICD10)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedDrug)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedICD9)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedLab)
        Me.Panel19.Controls.Add(Me.btnRemoveSelectedCPT)
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
        Me.Panel19.Controls.Add(Me.lstVw_Drugs)
        Me.Panel19.Controls.Add(Me.lstVw_Lab)
        Me.Panel19.Controls.Add(Me.lstVw_ICD9)
        Me.Panel19.Controls.Add(Me.lstVw_CPT)
        Me.Panel19.Controls.Add(Me.btnClearDrugs)
        Me.Panel19.Controls.Add(Me.btnClearLab)
        Me.Panel19.Controls.Add(Me.btnDrugs)
        Me.Panel19.Controls.Add(Me.btnLabs)
        Me.Panel19.Controls.Add(Me.btnICD9)
        Me.Panel19.Controls.Add(Me.btnCPT)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel19.Location = New System.Drawing.Point(0, 196)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(1170, 639)
        Me.Panel19.TabIndex = 90
        '
        'btnRemoveSelectedCVX
        '
        Me.btnRemoveSelectedCVX.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedCVX.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveSelectedCVX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedCVX.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRemoveSelectedCVX.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedCVX.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedCVX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedCVX.Image = CType(resources.GetObject("btnRemoveSelectedCVX.Image"), System.Drawing.Image)
        Me.btnRemoveSelectedCVX.Location = New System.Drawing.Point(548, 504)
        Me.btnRemoveSelectedCVX.Name = "btnRemoveSelectedCVX"
        Me.btnRemoveSelectedCVX.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedCVX.TabIndex = 105
        Me.btnRemoveSelectedCVX.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedCVX, "Remove selected ICD10")
        Me.btnRemoveSelectedCVX.UseVisualStyleBackColor = True
        '
        'btnClearCVX
        '
        Me.btnClearCVX.BackgroundImage = CType(resources.GetObject("btnClearCVX.BackgroundImage"), System.Drawing.Image)
        Me.btnClearCVX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCVX.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearCVX.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearCVX.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearCVX.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCVX.Image = CType(resources.GetObject("btnClearCVX.Image"), System.Drawing.Image)
        Me.btnClearCVX.Location = New System.Drawing.Point(548, 531)
        Me.btnClearCVX.Name = "btnClearCVX"
        Me.btnClearCVX.Size = New System.Drawing.Size(24, 24)
        Me.btnClearCVX.TabIndex = 103
        Me.ToolTip1.SetToolTip(Me.btnClearCVX, "Clear ICD10")
        Me.btnClearCVX.UseVisualStyleBackColor = True
        '
        'lstVw_CVX
        '
        Me.lstVw_CVX.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.lstVw_CVX.Location = New System.Drawing.Point(15, 478)
        Me.lstVw_CVX.MultiSelect = False
        Me.lstVw_CVX.Name = "lstVw_CVX"
        Me.lstVw_CVX.ShowItemToolTips = True
        Me.lstVw_CVX.Size = New System.Drawing.Size(525, 137)
        Me.lstVw_CVX.TabIndex = 104
        Me.lstVw_CVX.UseCompatibleStateImageBehavior = False
        Me.lstVw_CVX.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "CVX"
        Me.ColumnHeader2.Width = 410
        '
        'btncvx
        '
        Me.btncvx.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btncvx.BackgroundImage = CType(resources.GetObject("btncvx.BackgroundImage"), System.Drawing.Image)
        Me.btncvx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncvx.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btncvx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btncvx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btncvx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncvx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncvx.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btncvx.Image = CType(resources.GetObject("btncvx.Image"), System.Drawing.Image)
        Me.btncvx.Location = New System.Drawing.Point(548, 477)
        Me.btncvx.Name = "btncvx"
        Me.btncvx.Size = New System.Drawing.Size(24, 24)
        Me.btncvx.TabIndex = 102
        Me.btncvx.Tag = "UnSelected"
        Me.btncvx.Text = "      ICD9"
        Me.ToolTip1.SetToolTip(Me.btncvx, "Select ICD10")
        Me.btncvx.UseVisualStyleBackColor = False
        '
        'btnRemoveSelectedICD10
        '
        Me.btnRemoveSelectedICD10.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedICD10.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveSelectedICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedICD10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRemoveSelectedICD10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedICD10.Image = CType(resources.GetObject("btnRemoveSelectedICD10.Image"), System.Drawing.Image)
        Me.btnRemoveSelectedICD10.Location = New System.Drawing.Point(548, 349)
        Me.btnRemoveSelectedICD10.Name = "btnRemoveSelectedICD10"
        Me.btnRemoveSelectedICD10.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedICD10.TabIndex = 101
        Me.btnRemoveSelectedICD10.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedICD10, "Remove selected ICD10")
        Me.btnRemoveSelectedICD10.UseVisualStyleBackColor = True
        '
        'btnClearICD10
        '
        Me.btnClearICD10.BackgroundImage = CType(resources.GetObject("btnClearICD10.BackgroundImage"), System.Drawing.Image)
        Me.btnClearICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearICD10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearICD10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearICD10.Image = CType(resources.GetObject("btnClearICD10.Image"), System.Drawing.Image)
        Me.btnClearICD10.Location = New System.Drawing.Point(548, 376)
        Me.btnClearICD10.Name = "btnClearICD10"
        Me.btnClearICD10.Size = New System.Drawing.Size(24, 24)
        Me.btnClearICD10.TabIndex = 99
        Me.ToolTip1.SetToolTip(Me.btnClearICD10, "Clear ICD10")
        Me.btnClearICD10.UseVisualStyleBackColor = True
        '
        'lstVw_ICD10
        '
        Me.lstVw_ICD10.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader15})
        Me.lstVw_ICD10.Location = New System.Drawing.Point(15, 322)
        Me.lstVw_ICD10.MultiSelect = False
        Me.lstVw_ICD10.Name = "lstVw_ICD10"
        Me.lstVw_ICD10.ShowItemToolTips = True
        Me.lstVw_ICD10.Size = New System.Drawing.Size(525, 137)
        Me.lstVw_ICD10.TabIndex = 100
        Me.lstVw_ICD10.UseCompatibleStateImageBehavior = False
        Me.lstVw_ICD10.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "ICD10"
        Me.ColumnHeader15.Width = 410
        '
        'btnICD10
        '
        Me.btnICD10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnICD10.BackgroundImage = CType(resources.GetObject("btnICD10.BackgroundImage"), System.Drawing.Image)
        Me.btnICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICD10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD10.Image = CType(resources.GetObject("btnICD10.Image"), System.Drawing.Image)
        Me.btnICD10.Location = New System.Drawing.Point(548, 322)
        Me.btnICD10.Name = "btnICD10"
        Me.btnICD10.Size = New System.Drawing.Size(24, 24)
        Me.btnICD10.TabIndex = 98
        Me.btnICD10.Tag = "UnSelected"
        Me.btnICD10.Text = "      ICD9"
        Me.ToolTip1.SetToolTip(Me.btnICD10, "Select ICD10")
        Me.btnICD10.UseVisualStyleBackColor = False
        '
        'btnRemoveSelectedDrug
        '
        Me.btnRemoveSelectedDrug.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedDrug.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveSelectedDrug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedDrug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRemoveSelectedDrug.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedDrug.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedDrug.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedDrug.Image = CType(resources.GetObject("btnRemoveSelectedDrug.Image"), System.Drawing.Image)
        Me.btnRemoveSelectedDrug.Location = New System.Drawing.Point(1154, 37)
        Me.btnRemoveSelectedDrug.Name = "btnRemoveSelectedDrug"
        Me.btnRemoveSelectedDrug.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedDrug.TabIndex = 97
        Me.btnRemoveSelectedDrug.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedDrug, "Remove selected drugs ")
        Me.btnRemoveSelectedDrug.UseVisualStyleBackColor = True
        '
        'btnRemoveSelectedICD9
        '
        Me.btnRemoveSelectedICD9.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedICD9.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveSelectedICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedICD9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRemoveSelectedICD9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedICD9.Image = CType(resources.GetObject("btnRemoveSelectedICD9.Image"), System.Drawing.Image)
        Me.btnRemoveSelectedICD9.Location = New System.Drawing.Point(548, 37)
        Me.btnRemoveSelectedICD9.Name = "btnRemoveSelectedICD9"
        Me.btnRemoveSelectedICD9.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedICD9.TabIndex = 97
        Me.btnRemoveSelectedICD9.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedICD9, "Remove selected ICD9")
        Me.btnRemoveSelectedICD9.UseVisualStyleBackColor = True
        '
        'btnRemoveSelectedLab
        '
        Me.btnRemoveSelectedLab.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedLab.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveSelectedLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedLab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRemoveSelectedLab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedLab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedLab.Image = CType(resources.GetObject("btnRemoveSelectedLab.Image"), System.Drawing.Image)
        Me.btnRemoveSelectedLab.Location = New System.Drawing.Point(1154, 193)
        Me.btnRemoveSelectedLab.Name = "btnRemoveSelectedLab"
        Me.btnRemoveSelectedLab.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedLab.TabIndex = 97
        Me.btnRemoveSelectedLab.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedLab, "Remove selected Labs")
        Me.btnRemoveSelectedLab.UseVisualStyleBackColor = True
        '
        'btnRemoveSelectedCPT
        '
        Me.btnRemoveSelectedCPT.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveSelectedCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRemoveSelectedCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedCPT.Image = CType(resources.GetObject("btnRemoveSelectedCPT.Image"), System.Drawing.Image)
        Me.btnRemoveSelectedCPT.Location = New System.Drawing.Point(548, 193)
        Me.btnRemoveSelectedCPT.Name = "btnRemoveSelectedCPT"
        Me.btnRemoveSelectedCPT.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedCPT.TabIndex = 97
        Me.btnRemoveSelectedCPT.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedCPT, "Remove selected CPT")
        Me.btnRemoveSelectedCPT.UseVisualStyleBackColor = True
        '
        'btnRemoveSelectedSnomedCode
        '
        Me.btnRemoveSelectedSnomedCode.BackgroundImage = CType(resources.GetObject("btnRemoveSelectedSnomedCode.BackgroundImage"), System.Drawing.Image)
        Me.btnRemoveSelectedSnomedCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemoveSelectedSnomedCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRemoveSelectedSnomedCode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedSnomedCode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveSelectedSnomedCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveSelectedSnomedCode.Image = CType(resources.GetObject("btnRemoveSelectedSnomedCode.Image"), System.Drawing.Image)
        Me.btnRemoveSelectedSnomedCode.Location = New System.Drawing.Point(1154, 349)
        Me.btnRemoveSelectedSnomedCode.Name = "btnRemoveSelectedSnomedCode"
        Me.btnRemoveSelectedSnomedCode.Size = New System.Drawing.Size(24, 24)
        Me.btnRemoveSelectedSnomedCode.TabIndex = 96
        Me.btnRemoveSelectedSnomedCode.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnRemoveSelectedSnomedCode, "Remove selected snomed codes")
        Me.btnRemoveSelectedSnomedCode.UseVisualStyleBackColor = True
        '
        'btnClearSnomed
        '
        Me.btnClearSnomed.BackgroundImage = CType(resources.GetObject("btnClearSnomed.BackgroundImage"), System.Drawing.Image)
        Me.btnClearSnomed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSnomed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearSnomed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSnomed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearSnomed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSnomed.Image = CType(resources.GetObject("btnClearSnomed.Image"), System.Drawing.Image)
        Me.btnClearSnomed.Location = New System.Drawing.Point(1154, 376)
        Me.btnClearSnomed.Name = "btnClearSnomed"
        Me.btnClearSnomed.Size = New System.Drawing.Size(24, 24)
        Me.btnClearSnomed.TabIndex = 96
        Me.ToolTip1.SetToolTip(Me.btnClearSnomed, "Clear snomed codes")
        Me.btnClearSnomed.UseVisualStyleBackColor = True
        '
        'btnSnomed
        '
        Me.btnSnomed.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnSnomed.BackgroundImage = CType(resources.GetObject("btnSnomed.BackgroundImage"), System.Drawing.Image)
        Me.btnSnomed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSnomed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSnomed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSnomed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSnomed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSnomed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSnomed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSnomed.Image = CType(resources.GetObject("btnSnomed.Image"), System.Drawing.Image)
        Me.btnSnomed.Location = New System.Drawing.Point(1154, 322)
        Me.btnSnomed.Name = "btnSnomed"
        Me.btnSnomed.Size = New System.Drawing.Size(24, 24)
        Me.btnSnomed.TabIndex = 95
        Me.btnSnomed.Tag = "UnSelected"
        Me.ToolTip1.SetToolTip(Me.btnSnomed, "Add Snomed Code")
        Me.btnSnomed.UseVisualStyleBackColor = False
        '
        'lstVw_SnoMed
        '
        Me.lstVw_SnoMed.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader13})
        Me.lstVw_SnoMed.Location = New System.Drawing.Point(620, 322)
        Me.lstVw_SnoMed.MultiSelect = False
        Me.lstVw_SnoMed.Name = "lstVw_SnoMed"
        Me.lstVw_SnoMed.ShowItemToolTips = True
        Me.lstVw_SnoMed.Size = New System.Drawing.Size(525, 137)
        Me.lstVw_SnoMed.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstVw_SnoMed.TabIndex = 94
        Me.lstVw_SnoMed.UseCompatibleStateImageBehavior = False
        Me.lstVw_SnoMed.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Snomed"
        Me.ColumnHeader13.Width = 410
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label36.Location = New System.Drawing.Point(1, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1168, 1)
        Me.Label36.TabIndex = 93
        Me.Label36.Text = "label2"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label35.Location = New System.Drawing.Point(1, 638)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1168, 1)
        Me.Label35.TabIndex = 92
        Me.Label35.Text = "label2"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(1169, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 639)
        Me.Label34.TabIndex = 91
        Me.Label34.Text = "label4"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(0, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 639)
        Me.Label33.TabIndex = 90
        Me.Label33.Text = "label4"
        '
        'btnClearICD
        '
        Me.btnClearICD.BackgroundImage = CType(resources.GetObject("btnClearICD.BackgroundImage"), System.Drawing.Image)
        Me.btnClearICD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearICD.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearICD.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearICD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearICD.Image = CType(resources.GetObject("btnClearICD.Image"), System.Drawing.Image)
        Me.btnClearICD.Location = New System.Drawing.Point(548, 64)
        Me.btnClearICD.Name = "btnClearICD"
        Me.btnClearICD.Size = New System.Drawing.Size(24, 24)
        Me.btnClearICD.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.btnClearICD, "Clear ICD9")
        Me.btnClearICD.UseVisualStyleBackColor = True
        '
        'btnClearCPT
        '
        Me.btnClearCPT.BackgroundImage = CType(resources.GetObject("btnClearCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnClearCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearCPT.Image = CType(resources.GetObject("btnClearCPT.Image"), System.Drawing.Image)
        Me.btnClearCPT.Location = New System.Drawing.Point(548, 220)
        Me.btnClearCPT.Name = "btnClearCPT"
        Me.btnClearCPT.Size = New System.Drawing.Size(24, 24)
        Me.btnClearCPT.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.btnClearCPT, "Clear CPT")
        Me.btnClearCPT.UseVisualStyleBackColor = True
        '
        'lstVw_Drugs
        '
        Me.lstVw_Drugs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5})
        Me.lstVw_Drugs.Location = New System.Drawing.Point(620, 10)
        Me.lstVw_Drugs.MultiSelect = False
        Me.lstVw_Drugs.Name = "lstVw_Drugs"
        Me.lstVw_Drugs.ShowItemToolTips = True
        Me.lstVw_Drugs.Size = New System.Drawing.Size(525, 137)
        Me.lstVw_Drugs.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstVw_Drugs.TabIndex = 82
        Me.lstVw_Drugs.UseCompatibleStateImageBehavior = False
        Me.lstVw_Drugs.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Drugs"
        Me.ColumnHeader5.Width = 410
        '
        'lstVw_Lab
        '
        Me.lstVw_Lab.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7})
        Me.lstVw_Lab.Location = New System.Drawing.Point(620, 166)
        Me.lstVw_Lab.MultiSelect = False
        Me.lstVw_Lab.Name = "lstVw_Lab"
        Me.lstVw_Lab.ShowItemToolTips = True
        Me.lstVw_Lab.Size = New System.Drawing.Size(525, 137)
        Me.lstVw_Lab.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstVw_Lab.TabIndex = 81
        Me.lstVw_Lab.UseCompatibleStateImageBehavior = False
        Me.lstVw_Lab.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Orders"
        Me.ColumnHeader7.Width = 410
        '
        'lstVw_ICD9
        '
        Me.lstVw_ICD9.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {colICD9})
        Me.lstVw_ICD9.Location = New System.Drawing.Point(15, 10)
        Me.lstVw_ICD9.MultiSelect = False
        Me.lstVw_ICD9.Name = "lstVw_ICD9"
        Me.lstVw_ICD9.ShowItemToolTips = True
        Me.lstVw_ICD9.Size = New System.Drawing.Size(525, 137)
        Me.lstVw_ICD9.TabIndex = 75
        Me.lstVw_ICD9.UseCompatibleStateImageBehavior = False
        Me.lstVw_ICD9.View = System.Windows.Forms.View.Details
        '
        'lstVw_CPT
        '
        Me.lstVw_CPT.BackColor = System.Drawing.Color.White
        Me.lstVw_CPT.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lstVw_CPT.Location = New System.Drawing.Point(15, 166)
        Me.lstVw_CPT.MultiSelect = False
        Me.lstVw_CPT.Name = "lstVw_CPT"
        Me.lstVw_CPT.ShowItemToolTips = True
        Me.lstVw_CPT.Size = New System.Drawing.Size(525, 137)
        Me.lstVw_CPT.TabIndex = 76
        Me.lstVw_CPT.UseCompatibleStateImageBehavior = False
        Me.lstVw_CPT.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "CPT"
        Me.ColumnHeader1.Width = 410
        '
        'btnClearDrugs
        '
        Me.btnClearDrugs.BackgroundImage = CType(resources.GetObject("btnClearDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnClearDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearDrugs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearDrugs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearDrugs.Image = CType(resources.GetObject("btnClearDrugs.Image"), System.Drawing.Image)
        Me.btnClearDrugs.Location = New System.Drawing.Point(1154, 64)
        Me.btnClearDrugs.Name = "btnClearDrugs"
        Me.btnClearDrugs.Size = New System.Drawing.Size(24, 24)
        Me.btnClearDrugs.TabIndex = 78
        Me.ToolTip1.SetToolTip(Me.btnClearDrugs, "Clear Drugs")
        Me.btnClearDrugs.UseVisualStyleBackColor = True
        '
        'btnClearLab
        '
        Me.btnClearLab.BackgroundImage = CType(resources.GetObject("btnClearLab.BackgroundImage"), System.Drawing.Image)
        Me.btnClearLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearLab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearLab.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearLab.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearLab.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearLab.Image = CType(resources.GetObject("btnClearLab.Image"), System.Drawing.Image)
        Me.btnClearLab.Location = New System.Drawing.Point(1154, 220)
        Me.btnClearLab.Name = "btnClearLab"
        Me.btnClearLab.Size = New System.Drawing.Size(24, 24)
        Me.btnClearLab.TabIndex = 79
        Me.ToolTip1.SetToolTip(Me.btnClearLab, "Clear Lab")
        Me.btnClearLab.UseVisualStyleBackColor = True
        '
        'btnDrugs
        '
        Me.btnDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDrugs.BackgroundImage = CType(resources.GetObject("btnDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDrugs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDrugs.Image = CType(resources.GetObject("btnDrugs.Image"), System.Drawing.Image)
        Me.btnDrugs.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDrugs.Location = New System.Drawing.Point(1154, 10)
        Me.btnDrugs.Name = "btnDrugs"
        Me.btnDrugs.Size = New System.Drawing.Size(24, 24)
        Me.btnDrugs.TabIndex = 0
        Me.btnDrugs.Tag = "UnSelected"
        Me.btnDrugs.Text = "      Drugs"
        Me.ToolTip1.SetToolTip(Me.btnDrugs, "Select Drugs")
        Me.btnDrugs.UseVisualStyleBackColor = False
        '
        'btnLabs
        '
        Me.btnLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnLabs.BackgroundImage = CType(resources.GetObject("btnLabs.BackgroundImage"), System.Drawing.Image)
        Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLabs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLabs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLabs.Image = CType(resources.GetObject("btnLabs.Image"), System.Drawing.Image)
        Me.btnLabs.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLabs.Location = New System.Drawing.Point(1154, 166)
        Me.btnLabs.Name = "btnLabs"
        Me.btnLabs.Size = New System.Drawing.Size(24, 24)
        Me.btnLabs.TabIndex = 0
        Me.btnLabs.Tag = "UnSelected"
        Me.btnLabs.Text = "      Lab"
        Me.ToolTip1.SetToolTip(Me.btnLabs, "Select Lab")
        Me.btnLabs.UseVisualStyleBackColor = False
        '
        'btnICD9
        '
        Me.btnICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnICD9.BackgroundImage = CType(resources.GetObject("btnICD9.BackgroundImage"), System.Drawing.Image)
        Me.btnICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICD9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD9.Image = CType(resources.GetObject("btnICD9.Image"), System.Drawing.Image)
        Me.btnICD9.Location = New System.Drawing.Point(548, 10)
        Me.btnICD9.Name = "btnICD9"
        Me.btnICD9.Size = New System.Drawing.Size(24, 24)
        Me.btnICD9.TabIndex = 0
        Me.btnICD9.Tag = "UnSelected"
        Me.btnICD9.Text = "      ICD9"
        Me.ToolTip1.SetToolTip(Me.btnICD9, "Select ICD9")
        Me.btnICD9.UseVisualStyleBackColor = False
        '
        'btnCPT
        '
        Me.btnCPT.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnCPT.BackgroundImage = CType(resources.GetObject("btnCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCPT.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCPT.Image = CType(resources.GetObject("btnCPT.Image"), System.Drawing.Image)
        Me.btnCPT.Location = New System.Drawing.Point(548, 166)
        Me.btnCPT.Name = "btnCPT"
        Me.btnCPT.Size = New System.Drawing.Size(24, 24)
        Me.btnCPT.TabIndex = 0
        Me.btnCPT.Tag = "UnSelected"
        Me.btnCPT.Text = "      CPT"
        Me.ToolTip1.SetToolTip(Me.btnCPT, "Select CPT")
        Me.btnCPT.UseVisualStyleBackColor = False
        '
        'btnproblemlist
        '
        Me.btnproblemlist.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnproblemlist.BackgroundImage = CType(resources.GetObject("btnproblemlist.BackgroundImage"), System.Drawing.Image)
        Me.btnproblemlist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnproblemlist.FlatAppearance.BorderSize = 0
        Me.btnproblemlist.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnproblemlist.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnproblemlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnproblemlist.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnproblemlist.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnproblemlist.Image = CType(resources.GetObject("btnproblemlist.Image"), System.Drawing.Image)
        Me.btnproblemlist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnproblemlist.Location = New System.Drawing.Point(1028, 366)
        Me.btnproblemlist.Name = "btnproblemlist"
        Me.btnproblemlist.Size = New System.Drawing.Size(45, 25)
        Me.btnproblemlist.TabIndex = 0
        Me.btnproblemlist.Tag = "UnSelected"
        Me.btnproblemlist.Text = "      Problem List"
        Me.btnproblemlist.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnproblemlist.UseVisualStyleBackColor = False
        '
        'btnDemographics
        '
        Me.btnDemographics.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDemographics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDemographics.FlatAppearance.BorderSize = 0
        Me.btnDemographics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDemographics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDemographics.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDemographics.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDemographics.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDemographics.Image = CType(resources.GetObject("btnDemographics.Image"), System.Drawing.Image)
        Me.btnDemographics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDemographics.Location = New System.Drawing.Point(1028, 331)
        Me.btnDemographics.Name = "btnDemographics"
        Me.btnDemographics.Size = New System.Drawing.Size(45, 25)
        Me.btnDemographics.TabIndex = 0
        Me.btnDemographics.Tag = "UnSelected"
        Me.btnDemographics.Text = "      Demographics and Vitals"
        Me.btnDemographics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDemographics.UseVisualStyleBackColor = False
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.Panel25)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(0, 168)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel14.Size = New System.Drawing.Size(1170, 28)
        Me.Panel14.TabIndex = 47
        '
        'Panel25
        '
        Me.Panel25.BackgroundImage = CType(resources.GetObject("Panel25.BackgroundImage"), System.Drawing.Image)
        Me.Panel25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel25.Controls.Add(Me.Label202)
        Me.Panel25.Controls.Add(Me.Label204)
        Me.Panel25.Controls.Add(Me.Label214)
        Me.Panel25.Controls.Add(Me.Label236)
        Me.Panel25.Controls.Add(Me.Label241)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel25.Location = New System.Drawing.Point(0, 0)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(1170, 25)
        Me.Panel25.TabIndex = 44
        '
        'Label202
        '
        Me.Label202.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label202.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label202.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label202.Location = New System.Drawing.Point(1, 24)
        Me.Label202.Name = "Label202"
        Me.Label202.Size = New System.Drawing.Size(1168, 1)
        Me.Label202.TabIndex = 13
        Me.Label202.Text = "label2"
        '
        'Label204
        '
        Me.Label204.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label204.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label204.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label204.Location = New System.Drawing.Point(0, 1)
        Me.Label204.Name = "Label204"
        Me.Label204.Size = New System.Drawing.Size(1, 24)
        Me.Label204.TabIndex = 12
        Me.Label204.Text = "label4"
        '
        'Label214
        '
        Me.Label214.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label214.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label214.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label214.Location = New System.Drawing.Point(1169, 1)
        Me.Label214.Name = "Label214"
        Me.Label214.Size = New System.Drawing.Size(1, 24)
        Me.Label214.TabIndex = 11
        Me.Label214.Text = "label3"
        '
        'Label236
        '
        Me.Label236.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label236.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label236.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label236.Location = New System.Drawing.Point(0, 0)
        Me.Label236.Name = "Label236"
        Me.Label236.Size = New System.Drawing.Size(1170, 1)
        Me.Label236.TabIndex = 10
        Me.Label236.Text = "label1"
        '
        'Label241
        '
        Me.Label241.BackColor = System.Drawing.Color.Transparent
        Me.Label241.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label241.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label241.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label241.ForeColor = System.Drawing.Color.White
        Me.Label241.Image = CType(resources.GetObject("Label241.Image"), System.Drawing.Image)
        Me.Label241.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label241.Location = New System.Drawing.Point(0, 0)
        Me.Label241.Name = "Label241"
        Me.Label241.Size = New System.Drawing.Size(1170, 25)
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
        Me.pnlVitals.Controls.Add(Me.Label258)
        Me.pnlVitals.Controls.Add(Me.Label259)
        Me.pnlVitals.Controls.Add(Me.Label257)
        Me.pnlVitals.Controls.Add(Me.Label111)
        Me.pnlVitals.Controls.Add(Me.Label112)
        Me.pnlVitals.Controls.Add(Me.Label113)
        Me.pnlVitals.Controls.Add(Me.Label114)
        Me.pnlVitals.Controls.Add(Me.txtBMImax)
        Me.pnlVitals.Controls.Add(Me.lblBMImax)
        Me.pnlVitals.Controls.Add(Me.txtBMImin)
        Me.pnlVitals.Controls.Add(Me.lblBMImin)
        Me.pnlVitals.Controls.Add(Me.Label28)
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
        Me.pnlVitals.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlVitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlVitals.Location = New System.Drawing.Point(0, 113)
        Me.pnlVitals.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlVitals.Name = "pnlVitals"
        Me.pnlVitals.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlVitals.Size = New System.Drawing.Size(1170, 55)
        Me.pnlVitals.TabIndex = 1
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.Color.Transparent
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(766, 33)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(43, 11)
        Me.Label47.TabIndex = 53
        Me.Label47.Text = "(Systolic)"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(291, 33)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(43, 11)
        Me.Label40.TabIndex = 53
        Me.Label40.Text = "(Systolic)"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(613, 33)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(43, 11)
        Me.Label46.TabIndex = 53
        Me.Label46.Text = "(Systolic)"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label52
        '
        Me.label52.AutoSize = True
        Me.label52.BackColor = System.Drawing.Color.Transparent
        Me.label52.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label52.Location = New System.Drawing.Point(146, 33)
        Me.label52.Name = "label52"
        Me.label52.Size = New System.Drawing.Size(43, 11)
        Me.label52.TabIndex = 53
        Me.label52.Text = "(Systolic)"
        Me.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.Color.Transparent
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(838, 33)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(45, 11)
        Me.Label45.TabIndex = 54
        Me.Label45.Text = "(Diastolic)"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(685, 33)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(45, 11)
        Me.Label44.TabIndex = 54
        Me.Label44.Text = "(Diastolic)"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(359, 33)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(45, 11)
        Me.Label38.TabIndex = 54
        Me.Label38.Text = "(Diastolic)"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label51
        '
        Me.label51.AutoSize = True
        Me.label51.BackColor = System.Drawing.Color.Transparent
        Me.label51.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label51.Location = New System.Drawing.Point(214, 33)
        Me.label51.Name = "label51"
        Me.label51.Size = New System.Drawing.Size(45, 11)
        Me.label51.TabIndex = 54
        Me.label51.Text = "(Diastolic)"
        Me.label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(196, 11)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(12, 14)
        Me.Label30.TabIndex = 52
        Me.Label30.Text = "/"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label258
        '
        Me.Label258.AutoSize = True
        Me.Label258.BackColor = System.Drawing.Color.Transparent
        Me.Label258.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label258.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label258.Location = New System.Drawing.Point(548, 11)
        Me.Label258.Name = "Label258"
        Me.Label258.Size = New System.Drawing.Size(57, 14)
        Me.Label258.TabIndex = 50
        Me.Label258.Text = "between"
        Me.Label258.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label259
        '
        Me.Label259.AutoSize = True
        Me.Label259.BackColor = System.Drawing.Color.Transparent
        Me.Label259.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label259.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label259.Location = New System.Drawing.Point(962, 11)
        Me.Label259.Name = "Label259"
        Me.Label259.Size = New System.Drawing.Size(57, 14)
        Me.Label259.TabIndex = 32
        Me.Label259.Text = "between"
        Me.Label259.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label257
        '
        Me.Label257.AutoSize = True
        Me.Label257.BackColor = System.Drawing.Color.Transparent
        Me.Label257.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label257.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label257.Location = New System.Drawing.Point(79, 11)
        Me.Label257.Name = "Label257"
        Me.Label257.Size = New System.Drawing.Size(57, 14)
        Me.Label257.TabIndex = 32
        Me.Label257.Text = "between"
        Me.Label257.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label111
        '
        Me.Label111.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label111.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label111.Location = New System.Drawing.Point(1, 51)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(1168, 1)
        Me.Label111.TabIndex = 47
        Me.Label111.Text = "label2"
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.Location = New System.Drawing.Point(0, 1)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(1, 51)
        Me.Label112.TabIndex = 46
        Me.Label112.Text = "label4"
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label113.Location = New System.Drawing.Point(1169, 1)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(1, 51)
        Me.Label113.TabIndex = 45
        Me.Label113.Text = "label3"
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.Location = New System.Drawing.Point(0, 0)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(1170, 1)
        Me.Label114.TabIndex = 44
        Me.Label114.Text = "label1"
        '
        'txtBMImax
        '
        Me.txtBMImax.Enabled = False
        Me.txtBMImax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMImax.Location = New System.Drawing.Point(1103, 7)
        Me.txtBMImax.MaxLength = 5
        Me.txtBMImax.Name = "txtBMImax"
        Me.txtBMImax.Size = New System.Drawing.Size(60, 22)
        Me.txtBMImax.TabIndex = 15
        Me.txtBMImax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBMImax
        '
        Me.lblBMImax.AutoSize = True
        Me.lblBMImax.BackColor = System.Drawing.Color.Transparent
        Me.lblBMImax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBMImax.Location = New System.Drawing.Point(1082, 11)
        Me.lblBMImax.Name = "lblBMImax"
        Me.lblBMImax.Size = New System.Drawing.Size(19, 14)
        Me.lblBMImax.TabIndex = 27
        Me.lblBMImax.Text = "to"
        Me.lblBMImax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBMImin
        '
        Me.txtBMImin.Enabled = False
        Me.txtBMImin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBMImin.Location = New System.Drawing.Point(1021, 7)
        Me.txtBMImin.MaxLength = 5
        Me.txtBMImin.Name = "txtBMImin"
        Me.txtBMImin.Size = New System.Drawing.Size(60, 22)
        Me.txtBMImin.TabIndex = 14
        Me.txtBMImin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBMImin
        '
        Me.lblBMImin.AutoSize = True
        Me.lblBMImin.BackColor = System.Drawing.Color.Transparent
        Me.lblBMImin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBMImin.Location = New System.Drawing.Point(925, 11)
        Me.lblBMImin.Name = "lblBMImin"
        Me.lblBMImin.Size = New System.Drawing.Size(35, 14)
        Me.lblBMImin.TabIndex = 25
        Me.lblBMImin.Text = "BMI :"
        Me.lblBMImin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(265, 11)
        Me.Label28.Margin = New System.Windows.Forms.Padding(0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(19, 14)
        Me.Label28.TabIndex = 23
        Me.Label28.Text = "to"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtBPstandingMaxTo
        '
        Me.txtBPstandingMaxTo.Enabled = False
        Me.txtBPstandingMaxTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPstandingMaxTo.Location = New System.Drawing.Point(760, 7)
        Me.txtBPstandingMaxTo.MaxLength = 3
        Me.txtBPstandingMaxTo.Name = "txtBPstandingMaxTo"
        Me.txtBPstandingMaxTo.Size = New System.Drawing.Size(55, 22)
        Me.txtBPstandingMaxTo.TabIndex = 12
        Me.txtBPstandingMaxTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMinTo
        '
        Me.txtBPstandingMinTo.Enabled = False
        Me.txtBPstandingMinTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPstandingMinTo.Location = New System.Drawing.Point(833, 7)
        Me.txtBPstandingMinTo.MaxLength = 3
        Me.txtBPstandingMinTo.Name = "txtBPstandingMinTo"
        Me.txtBPstandingMinTo.Size = New System.Drawing.Size(55, 22)
        Me.txtBPstandingMinTo.TabIndex = 13
        Me.txtBPstandingMinTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMax
        '
        Me.txtBPstandingMax.Enabled = False
        Me.txtBPstandingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPstandingMax.Location = New System.Drawing.Point(607, 7)
        Me.txtBPstandingMax.MaxLength = 3
        Me.txtBPstandingMax.Name = "txtBPstandingMax"
        Me.txtBPstandingMax.Size = New System.Drawing.Size(55, 22)
        Me.txtBPstandingMax.TabIndex = 12
        Me.txtBPstandingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPstandingMin
        '
        Me.txtBPstandingMin.Enabled = False
        Me.txtBPstandingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPstandingMin.Location = New System.Drawing.Point(680, 7)
        Me.txtBPstandingMin.MaxLength = 3
        Me.txtBPstandingMin.Name = "txtBPstandingMin"
        Me.txtBPstandingMin.Size = New System.Drawing.Size(55, 22)
        Me.txtBPstandingMin.TabIndex = 13
        Me.txtBPstandingMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMaxTo
        '
        Me.txtBPsettingMaxTo.Enabled = False
        Me.txtBPsettingMaxTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPsettingMaxTo.Location = New System.Drawing.Point(285, 7)
        Me.txtBPsettingMaxTo.MaxLength = 3
        Me.txtBPsettingMaxTo.Name = "txtBPsettingMaxTo"
        Me.txtBPsettingMaxTo.Size = New System.Drawing.Size(55, 22)
        Me.txtBPsettingMaxTo.TabIndex = 10
        Me.txtBPsettingMaxTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMinTo
        '
        Me.txtBPsettingMinTo.Enabled = False
        Me.txtBPsettingMinTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPsettingMinTo.Location = New System.Drawing.Point(354, 7)
        Me.txtBPsettingMinTo.MaxLength = 3
        Me.txtBPsettingMinTo.Name = "txtBPsettingMinTo"
        Me.txtBPsettingMinTo.Size = New System.Drawing.Size(55, 22)
        Me.txtBPsettingMinTo.TabIndex = 11
        Me.txtBPsettingMinTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMax
        '
        Me.txtBPsettingMax.Enabled = False
        Me.txtBPsettingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPsettingMax.Location = New System.Drawing.Point(140, 7)
        Me.txtBPsettingMax.MaxLength = 3
        Me.txtBPsettingMax.Name = "txtBPsettingMax"
        Me.txtBPsettingMax.Size = New System.Drawing.Size(55, 22)
        Me.txtBPsettingMax.TabIndex = 10
        Me.txtBPsettingMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBPsettingMin
        '
        Me.txtBPsettingMin.Enabled = False
        Me.txtBPsettingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBPsettingMin.Location = New System.Drawing.Point(209, 7)
        Me.txtBPsettingMin.MaxLength = 3
        Me.txtBPsettingMin.Name = "txtBPsettingMin"
        Me.txtBPsettingMin.Size = New System.Drawing.Size(55, 22)
        Me.txtBPsettingMin.TabIndex = 11
        Me.txtBPsettingMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBPStandingMax
        '
        Me.lblBPStandingMax.AutoSize = True
        Me.lblBPStandingMax.BackColor = System.Drawing.Color.Transparent
        Me.lblBPStandingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPStandingMax.Location = New System.Drawing.Point(467, 11)
        Me.lblBPStandingMax.Name = "lblBPStandingMax"
        Me.lblBPStandingMax.Size = New System.Drawing.Size(81, 14)
        Me.lblBPStandingMax.TabIndex = 16
        Me.lblBPStandingMax.Text = "BP-Standing :"
        Me.lblBPStandingMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPStandingMin
        '
        Me.lblBPStandingMin.AutoSize = True
        Me.lblBPStandingMin.BackColor = System.Drawing.Color.Transparent
        Me.lblBPStandingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPStandingMin.Location = New System.Drawing.Point(738, 11)
        Me.lblBPStandingMin.Margin = New System.Windows.Forms.Padding(0)
        Me.lblBPStandingMin.Name = "lblBPStandingMin"
        Me.lblBPStandingMin.Size = New System.Drawing.Size(19, 14)
        Me.lblBPStandingMin.TabIndex = 15
        Me.lblBPStandingMin.Text = "to"
        Me.lblBPStandingMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(341, 11)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(12, 14)
        Me.Label26.TabIndex = 13
        Me.Label26.Text = "/"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(818, 11)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(12, 14)
        Me.Label29.TabIndex = 13
        Me.Label29.Text = "/"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPSittingMax
        '
        Me.lblBPSittingMax.AutoSize = True
        Me.lblBPSittingMax.BackColor = System.Drawing.Color.Transparent
        Me.lblBPSittingMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPSittingMax.Location = New System.Drawing.Point(12, 11)
        Me.lblBPSittingMax.Name = "lblBPSittingMax"
        Me.lblBPSittingMax.Size = New System.Drawing.Size(68, 14)
        Me.lblBPSittingMax.TabIndex = 14
        Me.lblBPSittingMax.Text = "BP-Sitting :"
        Me.lblBPSittingMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBPSittingMin
        '
        Me.lblBPSittingMin.AutoSize = True
        Me.lblBPSittingMin.BackColor = System.Drawing.Color.Transparent
        Me.lblBPSittingMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBPSittingMin.Location = New System.Drawing.Point(665, 11)
        Me.lblBPSittingMin.Name = "lblBPSittingMin"
        Me.lblBPSittingMin.Size = New System.Drawing.Size(12, 14)
        Me.lblBPSittingMin.TabIndex = 13
        Me.lblBPSittingMin.Text = "/"
        Me.lblBPSittingMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 85)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(1170, 28)
        Me.Panel3.TabIndex = 46
        '
        'Panel7
        '
        Me.Panel7.BackgroundImage = CType(resources.GetObject("Panel7.BackgroundImage"), System.Drawing.Image)
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label107)
        Me.Panel7.Controls.Add(Me.Label108)
        Me.Panel7.Controls.Add(Me.Label109)
        Me.Panel7.Controls.Add(Me.Label110)
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1170, 25)
        Me.Panel7.TabIndex = 44
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label107.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label107.Location = New System.Drawing.Point(1, 24)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(1168, 1)
        Me.Label107.TabIndex = 13
        Me.Label107.Text = "label2"
        '
        'Label108
        '
        Me.Label108.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label108.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label108.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label108.Location = New System.Drawing.Point(0, 1)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(1, 24)
        Me.Label108.TabIndex = 12
        Me.Label108.Text = "label4"
        '
        'Label109
        '
        Me.Label109.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label109.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label109.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label109.Location = New System.Drawing.Point(1169, 1)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(1, 24)
        Me.Label109.TabIndex = 11
        Me.Label109.Text = "label3"
        '
        'Label110
        '
        Me.Label110.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label110.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label110.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label110.Location = New System.Drawing.Point(0, 0)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(1170, 1)
        Me.Label110.TabIndex = 10
        Me.Label110.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Image = CType(resources.GetObject("Label7.Image"), System.Drawing.Image)
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1170, 25)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "      Vitals Triggers"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDemographics
        '
        Me.pnlDemographics.BackColor = System.Drawing.Color.Transparent
        Me.pnlDemographics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDemographics.Controls.Add(Me.Label125)
        Me.pnlDemographics.Controls.Add(Me.Label155)
        Me.pnlDemographics.Controls.Add(Me.Label124)
        Me.pnlDemographics.Controls.Add(Me.Label119)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMaxMnth)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMinMnth)
        Me.pnlDemographics.Controls.Add(Me.Label103)
        Me.pnlDemographics.Controls.Add(Me.Label104)
        Me.pnlDemographics.Controls.Add(Me.Label105)
        Me.pnlDemographics.Controls.Add(Me.Label106)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMax)
        Me.pnlDemographics.Controls.Add(Me.cmbAgeMin)
        Me.pnlDemographics.Controls.Add(Me.lblAgeMax)
        Me.pnlDemographics.Controls.Add(Me.Label156)
        Me.pnlDemographics.Controls.Add(Me.lblAgeMin)
        Me.pnlDemographics.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDemographics.ForeColor = System.Drawing.Color.Black
        Me.pnlDemographics.Location = New System.Drawing.Point(0, 28)
        Me.pnlDemographics.Name = "pnlDemographics"
        Me.pnlDemographics.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlDemographics.Size = New System.Drawing.Size(1170, 57)
        Me.pnlDemographics.TabIndex = 0
        '
        'Label125
        '
        Me.Label125.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label125.AutoSize = True
        Me.Label125.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label125.ForeColor = System.Drawing.Color.Red
        Me.Label125.Location = New System.Drawing.Point(339, 5)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(21, 13)
        Me.Label125.TabIndex = 28
        Me.Label125.Text = "mn"
        '
        'Label155
        '
        Me.Label155.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label155.AutoSize = True
        Me.Label155.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label155.ForeColor = System.Drawing.Color.Red
        Me.Label155.Location = New System.Drawing.Point(278, 5)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(22, 13)
        Me.Label155.TabIndex = 29
        Me.Label155.Text = "yrs"
        '
        'Label124
        '
        Me.Label124.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label124.AutoSize = True
        Me.Label124.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label124.ForeColor = System.Drawing.Color.Red
        Me.Label124.Location = New System.Drawing.Point(188, 5)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(21, 13)
        Me.Label124.TabIndex = 25
        Me.Label124.Text = "mn"
        '
        'Label119
        '
        Me.Label119.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label119.AutoSize = True
        Me.Label119.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label119.ForeColor = System.Drawing.Color.Red
        Me.Label119.Location = New System.Drawing.Point(127, 5)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(22, 13)
        Me.Label119.TabIndex = 25
        Me.Label119.Text = "yrs"
        '
        'cmbAgeMaxMnth
        '
        Me.cmbAgeMaxMnth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMaxMnth.Enabled = False
        Me.cmbAgeMaxMnth.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMaxMnth.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMaxMnth.Location = New System.Drawing.Point(321, 21)
        Me.cmbAgeMaxMnth.Name = "cmbAgeMaxMnth"
        Me.cmbAgeMaxMnth.Size = New System.Drawing.Size(56, 22)
        Me.cmbAgeMaxMnth.TabIndex = 27
        '
        'cmbAgeMinMnth
        '
        Me.cmbAgeMinMnth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMinMnth.Enabled = False
        Me.cmbAgeMinMnth.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMinMnth.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMinMnth.Location = New System.Drawing.Point(170, 21)
        Me.cmbAgeMinMnth.Name = "cmbAgeMinMnth"
        Me.cmbAgeMinMnth.Size = New System.Drawing.Size(56, 22)
        Me.cmbAgeMinMnth.TabIndex = 26
        '
        'Label103
        '
        Me.Label103.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label103.Location = New System.Drawing.Point(1, 53)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(1168, 1)
        Me.Label103.TabIndex = 22
        Me.Label103.Text = "label2"
        '
        'Label104
        '
        Me.Label104.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label104.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label104.Location = New System.Drawing.Point(0, 1)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(1, 53)
        Me.Label104.TabIndex = 21
        Me.Label104.Text = "label4"
        '
        'Label105
        '
        Me.Label105.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label105.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label105.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label105.Location = New System.Drawing.Point(1169, 1)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(1, 53)
        Me.Label105.TabIndex = 20
        Me.Label105.Text = "label3"
        '
        'Label106
        '
        Me.Label106.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label106.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.Location = New System.Drawing.Point(0, 0)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(1170, 1)
        Me.Label106.TabIndex = 19
        Me.Label106.Text = "label1"
        '
        'cmbAgeMax
        '
        Me.cmbAgeMax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMax.Enabled = False
        Me.cmbAgeMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMax.Location = New System.Drawing.Point(261, 21)
        Me.cmbAgeMax.Name = "cmbAgeMax"
        Me.cmbAgeMax.Size = New System.Drawing.Size(56, 22)
        Me.cmbAgeMax.TabIndex = 1
        '
        'cmbAgeMin
        '
        Me.cmbAgeMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMin.Enabled = False
        Me.cmbAgeMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMin.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMin.Location = New System.Drawing.Point(110, 21)
        Me.cmbAgeMin.Name = "cmbAgeMin"
        Me.cmbAgeMin.Size = New System.Drawing.Size(56, 22)
        Me.cmbAgeMin.TabIndex = 0
        '
        'lblAgeMax
        '
        Me.lblAgeMax.AutoSize = True
        Me.lblAgeMax.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgeMax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAgeMax.Location = New System.Drawing.Point(230, 25)
        Me.lblAgeMax.Name = "lblAgeMax"
        Me.lblAgeMax.Size = New System.Drawing.Size(27, 14)
        Me.lblAgeMax.TabIndex = 18
        Me.lblAgeMax.Text = "and"
        Me.lblAgeMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label156
        '
        Me.Label156.AutoSize = True
        Me.Label156.BackColor = System.Drawing.Color.Transparent
        Me.Label156.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label156.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label156.Location = New System.Drawing.Point(52, 25)
        Me.Label156.Name = "Label156"
        Me.Label156.Size = New System.Drawing.Size(57, 14)
        Me.Label156.TabIndex = 1
        Me.Label156.Text = "between"
        Me.Label156.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAgeMin
        '
        Me.lblAgeMin.AutoSize = True
        Me.lblAgeMin.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgeMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAgeMin.Location = New System.Drawing.Point(16, 25)
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
        Me.Panel2.Size = New System.Drawing.Size(1170, 28)
        Me.Panel2.TabIndex = 45
        '
        'Panel6
        '
        Me.Panel6.BackgroundImage = CType(resources.GetObject("Panel6.BackgroundImage"), System.Drawing.Image)
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Label65)
        Me.Panel6.Controls.Add(Me.Label85)
        Me.Panel6.Controls.Add(Me.Label89)
        Me.Panel6.Controls.Add(Me.Label90)
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1170, 25)
        Me.Panel6.TabIndex = 19
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label65.Location = New System.Drawing.Point(1, 24)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1168, 1)
        Me.Label65.TabIndex = 13
        Me.Label65.Text = "label2"
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.Location = New System.Drawing.Point(0, 1)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1, 24)
        Me.Label85.TabIndex = 12
        Me.Label85.Text = "label4"
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label89.Location = New System.Drawing.Point(1169, 1)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1, 24)
        Me.Label89.TabIndex = 11
        Me.Label89.Text = "label3"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.Location = New System.Drawing.Point(0, 0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(1170, 1)
        Me.Label90.TabIndex = 10
        Me.Label90.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Image = CType(resources.GetObject("Label2.Image"), System.Drawing.Image)
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1170, 25)
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
        Me.PnlProblemList.Size = New System.Drawing.Size(1170, 835)
        Me.PnlProblemList.TabIndex = 9
        Me.PnlProblemList.Visible = False
        '
        'PnlProblemMiddle
        '
        Me.PnlProblemMiddle.Controls.Add(Me.Pnlsnomedprb)
        Me.PnlProblemMiddle.Controls.Add(Me.PnlProblemSearch)
        Me.PnlProblemMiddle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlProblemMiddle.Location = New System.Drawing.Point(0, 27)
        Me.PnlProblemMiddle.Name = "PnlProblemMiddle"
        Me.PnlProblemMiddle.Size = New System.Drawing.Size(1170, 618)
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
        Me.Pnlsnomedprb.Size = New System.Drawing.Size(1170, 618)
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
        Me.pnltrvSnowmedOff.Size = New System.Drawing.Size(1170, 427)
        Me.pnltrvSnowmedOff.TabIndex = 20
        '
        'Label142
        '
        Me.Label142.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label142.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label142.Location = New System.Drawing.Point(1, 426)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(1168, 1)
        Me.Label142.TabIndex = 37
        Me.Label142.Text = "label1"
        '
        'trvSnowmedOff
        '
        Me.trvSnowmedOff.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSnowmedOff.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSnowmedOff.Location = New System.Drawing.Point(1, 22)
        Me.trvSnowmedOff.Name = "trvSnowmedOff"
        Me.trvSnowmedOff.Size = New System.Drawing.Size(1168, 405)
        Me.trvSnowmedOff.TabIndex = 0
        '
        'Panel24
        '
        Me.Panel24.BackgroundImage = CType(resources.GetObject("Panel24.BackgroundImage"), System.Drawing.Image)
        Me.Panel24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel24.Controls.Add(Me.Label143)
        Me.Panel24.Controls.Add(Me.Label144)
        Me.Panel24.Controls.Add(Me.Label150)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel24.Location = New System.Drawing.Point(1, 0)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(1168, 22)
        Me.Panel24.TabIndex = 18
        '
        'Label143
        '
        Me.Label143.BackColor = System.Drawing.Color.Transparent
        Me.Label143.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label143.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label143.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label143.Location = New System.Drawing.Point(0, 1)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(1168, 20)
        Me.Label143.TabIndex = 39
        Me.Label143.Text = "  Problem List"
        Me.Label143.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label144.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label144.Location = New System.Drawing.Point(0, 21)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(1168, 1)
        Me.Label144.TabIndex = 38
        Me.Label144.Text = "label1"
        '
        'Label150
        '
        Me.Label150.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label150.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label150.Location = New System.Drawing.Point(0, 0)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(1168, 1)
        Me.Label150.TabIndex = 37
        Me.Label150.Text = "label1"
        '
        'Label151
        '
        Me.Label151.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label151.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label151.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label151.Location = New System.Drawing.Point(0, 0)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(1, 427)
        Me.Label151.TabIndex = 19
        Me.Label151.Text = "label4"
        '
        'Label152
        '
        Me.Label152.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label152.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label152.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label152.Location = New System.Drawing.Point(1169, 0)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(1, 427)
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
        Me.pnltrvfinprob.Size = New System.Drawing.Size(1170, 427)
        Me.pnltrvfinprob.TabIndex = 1
        '
        'trvfinprob
        '
        Me.trvfinprob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.trvfinprob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvfinprob.Location = New System.Drawing.Point(0, 22)
        Me.trvfinprob.Name = "trvfinprob"
        Me.trvfinprob.Size = New System.Drawing.Size(1170, 405)
        Me.trvfinprob.TabIndex = 0
        '
        'Panel31
        '
        Me.Panel31.BackgroundImage = CType(resources.GetObject("Panel31.BackgroundImage"), System.Drawing.Image)
        Me.Panel31.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel31.Controls.Add(Me.Label154)
        Me.Panel31.Controls.Add(Me.Label153)
        Me.Panel31.Controls.Add(Me.Label201)
        Me.Panel31.Controls.Add(Me.Label203)
        Me.Panel31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel31.Location = New System.Drawing.Point(0, 0)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Size = New System.Drawing.Size(1170, 22)
        Me.Panel31.TabIndex = 18
        '
        'Label154
        '
        Me.Label154.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label154.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label154.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label154.Location = New System.Drawing.Point(1169, 1)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(1, 21)
        Me.Label154.TabIndex = 41
        Me.Label154.Text = "label4"
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label153.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.Label201.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label201.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label201.Location = New System.Drawing.Point(0, 1)
        Me.Label201.Name = "Label201"
        Me.Label201.Size = New System.Drawing.Size(1170, 21)
        Me.Label201.TabIndex = 39
        Me.Label201.Text = "  Finding"
        Me.Label201.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label203
        '
        Me.Label203.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label203.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label203.Location = New System.Drawing.Point(0, 0)
        Me.Label203.Name = "Label203"
        Me.Label203.Size = New System.Drawing.Size(1170, 1)
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
        Me.PnlSrchProb.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlSrchProb.ForeColor = System.Drawing.Color.Black
        Me.PnlSrchProb.Location = New System.Drawing.Point(0, 0)
        Me.PnlSrchProb.Name = "PnlSrchProb"
        Me.PnlSrchProb.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.PnlSrchProb.Size = New System.Drawing.Size(1170, 26)
        Me.PnlSrchProb.TabIndex = 0
        '
        'txtsrchprb
        '
        Me.txtsrchprb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsrchprb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsrchprb.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsrchprb.ForeColor = System.Drawing.Color.Black
        Me.txtsrchprb.Location = New System.Drawing.Point(29, 4)
        Me.txtsrchprb.MaxLength = 50
        Me.txtsrchprb.Name = "txtsrchprb"
        Me.txtsrchprb.Size = New System.Drawing.Size(1119, 15)
        Me.txtsrchprb.TabIndex = 0
        '
        'Label215
        '
        Me.Label215.BackColor = System.Drawing.Color.White
        Me.Label215.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label215.Location = New System.Drawing.Point(29, 1)
        Me.Label215.Name = "Label215"
        Me.Label215.Size = New System.Drawing.Size(1119, 3)
        Me.Label215.TabIndex = 37
        '
        'Label216
        '
        Me.Label216.BackColor = System.Drawing.Color.White
        Me.Label216.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label216.Location = New System.Drawing.Point(29, 17)
        Me.Label216.Name = "Label216"
        Me.Label216.Size = New System.Drawing.Size(1119, 5)
        Me.Label216.TabIndex = 38
        '
        'btnclrprb
        '
        Me.btnclrprb.BackgroundImage = CType(resources.GetObject("btnclrprb.BackgroundImage"), System.Drawing.Image)
        Me.btnclrprb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclrprb.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnclrprb.FlatAppearance.BorderSize = 0
        Me.btnclrprb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnclrprb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnclrprb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclrprb.Image = CType(resources.GetObject("btnclrprb.Image"), System.Drawing.Image)
        Me.btnclrprb.Location = New System.Drawing.Point(1148, 1)
        Me.btnclrprb.Name = "btnclrprb"
        Me.btnclrprb.Size = New System.Drawing.Size(21, 21)
        Me.btnclrprb.TabIndex = 41
        Me.ToolTip1.SetToolTip(Me.btnclrprb, "Clear search")
        Me.btnclrprb.UseVisualStyleBackColor = True
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.White
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox3.TabIndex = 9
        Me.PictureBox3.TabStop = False
        '
        'Label217
        '
        Me.Label217.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label217.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label217.Location = New System.Drawing.Point(1, 22)
        Me.Label217.Name = "Label217"
        Me.Label217.Size = New System.Drawing.Size(1168, 1)
        Me.Label217.TabIndex = 35
        Me.Label217.Text = "label1"
        '
        'Label218
        '
        Me.Label218.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label218.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label218.Location = New System.Drawing.Point(1, 0)
        Me.Label218.Name = "Label218"
        Me.Label218.Size = New System.Drawing.Size(1168, 1)
        Me.Label218.TabIndex = 36
        Me.Label218.Text = "label1"
        '
        'Label219
        '
        Me.Label219.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label219.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label219.Location = New System.Drawing.Point(0, 0)
        Me.Label219.Name = "Label219"
        Me.Label219.Size = New System.Drawing.Size(1, 23)
        Me.Label219.TabIndex = 39
        Me.Label219.Text = "label4"
        '
        'Label220
        '
        Me.Label220.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label220.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label220.Location = New System.Drawing.Point(1169, 0)
        Me.Label220.Name = "Label220"
        Me.Label220.Size = New System.Drawing.Size(1, 23)
        Me.Label220.TabIndex = 40
        Me.Label220.Text = "label4"
        '
        'Splitter6
        '
        Me.Splitter6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter6.Location = New System.Drawing.Point(0, 453)
        Me.Splitter6.Name = "Splitter6"
        Me.Splitter6.Size = New System.Drawing.Size(1170, 3)
        Me.Splitter6.TabIndex = 19
        Me.Splitter6.TabStop = False
        Me.Splitter6.Visible = False
        '
        'pnltrvsubprb
        '
        Me.pnltrvsubprb.Controls.Add(Me.Label221)
        Me.pnltrvsubprb.Controls.Add(Me.trvsubprb)
        Me.pnltrvsubprb.Controls.Add(Me.Panel34)
        Me.pnltrvsubprb.Controls.Add(Me.Label225)
        Me.pnltrvsubprb.Controls.Add(Me.Label226)
        Me.pnltrvsubprb.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnltrvsubprb.Location = New System.Drawing.Point(0, 456)
        Me.pnltrvsubprb.Name = "pnltrvsubprb"
        Me.pnltrvsubprb.Size = New System.Drawing.Size(1170, 162)
        Me.pnltrvsubprb.TabIndex = 2
        Me.pnltrvsubprb.Visible = False
        '
        'Label221
        '
        Me.Label221.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label221.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label221.Location = New System.Drawing.Point(1, 161)
        Me.Label221.Name = "Label221"
        Me.Label221.Size = New System.Drawing.Size(1168, 1)
        Me.Label221.TabIndex = 37
        Me.Label221.Text = "label1"
        '
        'trvsubprb
        '
        Me.trvsubprb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvsubprb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvsubprb.Location = New System.Drawing.Point(1, 22)
        Me.trvsubprb.Name = "trvsubprb"
        Me.trvsubprb.Size = New System.Drawing.Size(1168, 140)
        Me.trvsubprb.TabIndex = 0
        '
        'Panel34
        '
        Me.Panel34.BackgroundImage = CType(resources.GetObject("Panel34.BackgroundImage"), System.Drawing.Image)
        Me.Panel34.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel34.Controls.Add(Me.Label222)
        Me.Panel34.Controls.Add(Me.Label223)
        Me.Panel34.Controls.Add(Me.Label224)
        Me.Panel34.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel34.Location = New System.Drawing.Point(1, 0)
        Me.Panel34.Name = "Panel34"
        Me.Panel34.Size = New System.Drawing.Size(1168, 22)
        Me.Panel34.TabIndex = 18
        '
        'Label222
        '
        Me.Label222.BackColor = System.Drawing.Color.Transparent
        Me.Label222.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label222.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label222.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label222.Location = New System.Drawing.Point(0, 1)
        Me.Label222.Name = "Label222"
        Me.Label222.Size = New System.Drawing.Size(1168, 20)
        Me.Label222.TabIndex = 40
        Me.Label222.Text = "  Sub Type"
        Me.Label222.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label223
        '
        Me.Label223.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label223.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label223.Location = New System.Drawing.Point(0, 0)
        Me.Label223.Name = "Label223"
        Me.Label223.Size = New System.Drawing.Size(1168, 1)
        Me.Label223.TabIndex = 38
        Me.Label223.Text = "label1"
        '
        'Label224
        '
        Me.Label224.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label224.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label224.Location = New System.Drawing.Point(0, 21)
        Me.Label224.Name = "Label224"
        Me.Label224.Size = New System.Drawing.Size(1168, 1)
        Me.Label224.TabIndex = 37
        Me.Label224.Text = "label1"
        '
        'Label225
        '
        Me.Label225.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label225.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label225.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label225.Location = New System.Drawing.Point(0, 0)
        Me.Label225.Name = "Label225"
        Me.Label225.Size = New System.Drawing.Size(1, 162)
        Me.Label225.TabIndex = 19
        Me.Label225.Text = "label4"
        '
        'Label226
        '
        Me.Label226.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label226.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label226.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label226.Location = New System.Drawing.Point(1169, 0)
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
        Me.PnlProblemSearch.Size = New System.Drawing.Size(1170, 618)
        Me.PnlProblemSearch.TabIndex = 2
        '
        'Panel26
        '
        Me.Panel26.Controls.Add(Me.trvprobright)
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel26.Location = New System.Drawing.Point(287, 0)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(883, 618)
        Me.Panel26.TabIndex = 0
        '
        'trvprobright
        '
        Me.trvprobright.BackColor = System.Drawing.Color.White
        Me.trvprobright.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvprobright.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvprobright.ForeColor = System.Drawing.Color.Black
        Me.trvprobright.HideSelection = False
        Me.trvprobright.ImageIndex = 0
        Me.trvprobright.ImageList = Me.ImageList1
        Me.trvprobright.ItemHeight = 18
        Me.trvprobright.Location = New System.Drawing.Point(0, 1)
        Me.trvprobright.Name = "trvprobright"
        Me.trvprobright.SelectedImageIndex = 0
        Me.trvprobright.ShowLines = False
        Me.trvprobright.Size = New System.Drawing.Size(326, 274)
        Me.trvprobright.TabIndex = 2
        Me.trvprobright.Visible = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
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
        Me.PnlProbLeft.Enabled = False
        Me.PnlProbLeft.Location = New System.Drawing.Point(0, 0)
        Me.PnlProbLeft.Name = "PnlProbLeft"
        Me.PnlProbLeft.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.PnlProbLeft.Size = New System.Drawing.Size(287, 618)
        Me.PnlProbLeft.TabIndex = 2
        Me.PnlProbLeft.Visible = False
        '
        'Label179
        '
        Me.Label179.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label179.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label179.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label179.Location = New System.Drawing.Point(1, 617)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(282, 1)
        Me.Label179.TabIndex = 12
        Me.Label179.Text = "label2"
        '
        'Label197
        '
        Me.Label197.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label197.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label197.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label197.Location = New System.Drawing.Point(0, 1)
        Me.Label197.Name = "Label197"
        Me.Label197.Size = New System.Drawing.Size(1, 617)
        Me.Label197.TabIndex = 11
        Me.Label197.Text = "label4"
        '
        'Label198
        '
        Me.Label198.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label198.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label198.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label198.Location = New System.Drawing.Point(283, 1)
        Me.Label198.Name = "Label198"
        Me.Label198.Size = New System.Drawing.Size(1, 617)
        Me.Label198.TabIndex = 10
        Me.Label198.Text = "label3"
        '
        'Label199
        '
        Me.Label199.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label199.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label199.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.trvproblem.HideSelection = False
        Me.trvproblem.ItemHeight = 18
        Me.trvproblem.Location = New System.Drawing.Point(0, 0)
        Me.trvproblem.Name = "trvproblem"
        Me.trvproblem.ShowLines = False
        Me.trvproblem.Size = New System.Drawing.Size(284, 618)
        Me.trvproblem.TabIndex = 0
        Me.trvproblem.Visible = False
        '
        'Panel35
        '
        Me.Panel35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel35.Controls.Add(Me.Panel36)
        Me.Panel35.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel35.Location = New System.Drawing.Point(0, 645)
        Me.Panel35.Name = "Panel35"
        Me.Panel35.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel35.Size = New System.Drawing.Size(1170, 27)
        Me.Panel35.TabIndex = 22
        '
        'Panel36
        '
        Me.Panel36.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel36.Controls.Add(Me.Label227)
        Me.Panel36.Controls.Add(Me.Label228)
        Me.Panel36.Controls.Add(Me.Label229)
        Me.Panel36.Controls.Add(Me.Label230)
        Me.Panel36.Controls.Add(Me.Label231)
        Me.Panel36.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel36.Location = New System.Drawing.Point(0, 3)
        Me.Panel36.Name = "Panel36"
        Me.Panel36.Size = New System.Drawing.Size(1170, 21)
        Me.Panel36.TabIndex = 14
        '
        'Label227
        '
        Me.Label227.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label227.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label227.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label227.Location = New System.Drawing.Point(1169, 1)
        Me.Label227.Name = "Label227"
        Me.Label227.Size = New System.Drawing.Size(1, 19)
        Me.Label227.TabIndex = 11
        Me.Label227.Text = "label3"
        '
        'Label228
        '
        Me.Label228.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label228.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label228.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label228.Location = New System.Drawing.Point(0, 1)
        Me.Label228.Name = "Label228"
        Me.Label228.Size = New System.Drawing.Size(1, 19)
        Me.Label228.TabIndex = 12
        Me.Label228.Text = "label4"
        '
        'Label229
        '
        Me.Label229.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label229.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label229.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label229.Location = New System.Drawing.Point(0, 0)
        Me.Label229.Name = "Label229"
        Me.Label229.Size = New System.Drawing.Size(1170, 1)
        Me.Label229.TabIndex = 10
        Me.Label229.Text = "label1"
        '
        'Label230
        '
        Me.Label230.BackColor = System.Drawing.Color.Transparent
        Me.Label230.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label230.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label230.ForeColor = System.Drawing.Color.White
        Me.Label230.Image = CType(resources.GetObject("Label230.Image"), System.Drawing.Image)
        Me.Label230.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label230.Location = New System.Drawing.Point(0, 0)
        Me.Label230.Name = "Label230"
        Me.Label230.Size = New System.Drawing.Size(1170, 20)
        Me.Label230.TabIndex = 9
        Me.Label230.Text = "      Selected Problem"
        Me.Label230.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label231
        '
        Me.Label231.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label231.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label231.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label231.Location = New System.Drawing.Point(0, 20)
        Me.Label231.Name = "Label231"
        Me.Label231.Size = New System.Drawing.Size(1170, 1)
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
        Me.Panel37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel37.Location = New System.Drawing.Point(0, 672)
        Me.Panel37.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel37.Name = "Panel37"
        Me.Panel37.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel37.Size = New System.Drawing.Size(1170, 163)
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
        Me.trvselectedhist.Size = New System.Drawing.Size(1168, 158)
        Me.trvselectedhist.TabIndex = 9
        '
        'Label232
        '
        Me.Label232.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label232.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label232.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label232.Location = New System.Drawing.Point(1, 159)
        Me.Label232.Name = "Label232"
        Me.Label232.Size = New System.Drawing.Size(1168, 1)
        Me.Label232.TabIndex = 8
        Me.Label232.Text = "label2"
        '
        'Label233
        '
        Me.Label233.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label233.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label233.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.trvselectedprob.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvselectedprob.ForeColor = System.Drawing.Color.Black
        Me.trvselectedprob.HideSelection = False
        Me.trvselectedprob.ImageIndex = 11
        Me.trvselectedprob.ImageList = Me.ImageList1
        Me.trvselectedprob.ItemHeight = 18
        Me.trvselectedprob.Location = New System.Drawing.Point(0, 1)
        Me.trvselectedprob.Name = "trvselectedprob"
        Me.trvselectedprob.SelectedImageIndex = 11
        Me.trvselectedprob.ShowLines = False
        Me.trvselectedprob.Size = New System.Drawing.Size(1169, 159)
        Me.trvselectedprob.TabIndex = 0
        '
        'Label234
        '
        Me.Label234.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label234.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label234.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label234.Location = New System.Drawing.Point(1169, 1)
        Me.Label234.Name = "Label234"
        Me.Label234.Size = New System.Drawing.Size(1, 159)
        Me.Label234.TabIndex = 6
        Me.Label234.Text = "label3"
        '
        'Label235
        '
        Me.Label235.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label235.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label235.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label235.Location = New System.Drawing.Point(0, 0)
        Me.Label235.Name = "Label235"
        Me.Label235.Size = New System.Drawing.Size(1170, 1)
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
        Me.Panel38.Size = New System.Drawing.Size(1170, 27)
        Me.Panel38.TabIndex = 0
        '
        'Panel39
        '
        Me.Panel39.BackColor = System.Drawing.Color.Transparent
        Me.Panel39.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel39.Controls.Add(Me.cmbhistsnomed)
        Me.Panel39.Controls.Add(Me.lblsnohistcat)
        Me.Panel39.Controls.Add(Me.TextBox2)
        Me.Panel39.Controls.Add(Me.Label237)
        Me.Panel39.Controls.Add(Me.Label238)
        Me.Panel39.Controls.Add(Me.Label239)
        Me.Panel39.Controls.Add(Me.Label240)
        Me.Panel39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel39.ForeColor = System.Drawing.Color.Black
        Me.Panel39.Location = New System.Drawing.Point(0, 0)
        Me.Panel39.Name = "Panel39"
        Me.Panel39.Size = New System.Drawing.Size(1170, 24)
        Me.Panel39.TabIndex = 19
        '
        'cmbhistsnomed
        '
        Me.cmbhistsnomed.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbhistsnomed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbhistsnomed.FormattingEnabled = True
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
        Me.lblsnohistcat.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsnohistcat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.TextBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.Color.Black
        Me.TextBox2.Location = New System.Drawing.Point(507, 5)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(87, 15)
        Me.TextBox2.TabIndex = 1
        Me.TextBox2.Visible = False
        '
        'Label237
        '
        Me.Label237.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label237.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label237.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label237.Location = New System.Drawing.Point(1, 23)
        Me.Label237.Name = "Label237"
        Me.Label237.Size = New System.Drawing.Size(1168, 1)
        Me.Label237.TabIndex = 12
        Me.Label237.Text = "label2"
        '
        'Label238
        '
        Me.Label238.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label238.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label238.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label238.Location = New System.Drawing.Point(0, 1)
        Me.Label238.Name = "Label238"
        Me.Label238.Size = New System.Drawing.Size(1, 23)
        Me.Label238.TabIndex = 11
        Me.Label238.Text = "label4"
        '
        'Label239
        '
        Me.Label239.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label239.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label239.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label239.Location = New System.Drawing.Point(1169, 1)
        Me.Label239.Name = "Label239"
        Me.Label239.Size = New System.Drawing.Size(1, 23)
        Me.Label239.TabIndex = 10
        Me.Label239.Text = "label3"
        '
        'Label240
        '
        Me.Label240.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label240.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label240.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label240.Location = New System.Drawing.Point(0, 0)
        Me.Label240.Name = "Label240"
        Me.Label240.Size = New System.Drawing.Size(1170, 1)
        Me.Label240.TabIndex = 9
        Me.Label240.Text = "label1"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(254, 134)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(63, 22)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Search"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = False
        Me.Button2.Visible = False
        '
        'pnlRadiology
        '
        Me.pnlRadiology.Controls.Add(Me.Panel18)
        Me.pnlRadiology.Controls.Add(Me.Panel17)
        Me.pnlRadiology.Controls.Add(Me.pnlInternalToolStripRadiology)
        Me.pnlRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRadiology.Location = New System.Drawing.Point(0, 0)
        Me.pnlRadiology.Name = "pnlRadiology"
        Me.pnlRadiology.Size = New System.Drawing.Size(1170, 835)
        Me.pnlRadiology.TabIndex = 1
        Me.pnlRadiology.Visible = False
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Label170)
        Me.Panel18.Controls.Add(Me.Label171)
        Me.Panel18.Controls.Add(Me.Label172)
        Me.Panel18.Controls.Add(Me.Label173)
        Me.Panel18.Controls.Add(Me.c1Labs)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel18.Location = New System.Drawing.Point(0, 80)
        Me.Panel18.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel18.Size = New System.Drawing.Size(1170, 755)
        Me.Panel18.TabIndex = 20
        '
        'Label170
        '
        Me.Label170.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label170.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label170.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label170.Location = New System.Drawing.Point(1, 751)
        Me.Label170.Name = "Label170"
        Me.Label170.Size = New System.Drawing.Size(1168, 1)
        Me.Label170.TabIndex = 8
        Me.Label170.Text = "label2"
        '
        'Label171
        '
        Me.Label171.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label171.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label171.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label171.Location = New System.Drawing.Point(0, 1)
        Me.Label171.Name = "Label171"
        Me.Label171.Size = New System.Drawing.Size(1, 751)
        Me.Label171.TabIndex = 7
        Me.Label171.Text = "label4"
        '
        'Label172
        '
        Me.Label172.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label172.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label172.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label172.Location = New System.Drawing.Point(1169, 1)
        Me.Label172.Name = "Label172"
        Me.Label172.Size = New System.Drawing.Size(1, 751)
        Me.Label172.TabIndex = 6
        Me.Label172.Text = "label3"
        '
        'Label173
        '
        Me.Label173.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label173.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label173.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label173.Location = New System.Drawing.Point(0, 0)
        Me.Label173.Name = "Label173"
        Me.Label173.Size = New System.Drawing.Size(1170, 1)
        Me.Label173.TabIndex = 5
        Me.Label173.Text = "label1"
        '
        'c1Labs
        '
        Me.c1Labs.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1Labs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1Labs.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Labs.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.c1Labs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Labs.ExtendLastCol = True
        Me.c1Labs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1Labs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Labs.Location = New System.Drawing.Point(0, 0)
        Me.c1Labs.Name = "c1Labs"
        Me.c1Labs.Rows.DefaultSize = 19
        Me.c1Labs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Labs.ShowCellLabels = True
        Me.c1Labs.Size = New System.Drawing.Size(1170, 752)
        Me.c1Labs.StyleInfo = resources.GetString("c1Labs.StyleInfo")
        Me.c1Labs.TabIndex = 0
        Me.c1Labs.Tree.NodeImageCollapsed = CType(resources.GetObject("c1Labs.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.c1Labs.Tree.NodeImageExpanded = CType(resources.GetObject("c1Labs.Tree.NodeImageExpanded"), System.Drawing.Image)
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
        Me.Panel17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel17.ForeColor = System.Drawing.Color.Black
        Me.Panel17.Location = New System.Drawing.Point(0, 54)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel17.Size = New System.Drawing.Size(1170, 26)
        Me.Panel17.TabIndex = 19
        '
        'txtLabsSearch
        '
        Me.txtLabsSearch.BackColor = System.Drawing.Color.White
        Me.txtLabsSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabsSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLabsSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabsSearch.ForeColor = System.Drawing.Color.Black
        Me.txtLabsSearch.Location = New System.Drawing.Point(29, 5)
        Me.txtLabsSearch.Name = "txtLabsSearch"
        Me.txtLabsSearch.Size = New System.Drawing.Size(1119, 15)
        Me.txtLabsSearch.TabIndex = 0
        '
        'btnLabClear
        '
        Me.btnLabClear.BackColor = System.Drawing.Color.Transparent
        Me.btnLabClear.BackgroundImage = CType(resources.GetObject("btnLabClear.BackgroundImage"), System.Drawing.Image)
        Me.btnLabClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLabClear.FlatAppearance.BorderSize = 0
        Me.btnLabClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabClear.Image = CType(resources.GetObject("btnLabClear.Image"), System.Drawing.Image)
        Me.btnLabClear.Location = New System.Drawing.Point(1148, 5)
        Me.btnLabClear.Name = "btnLabClear"
        Me.btnLabClear.Size = New System.Drawing.Size(21, 15)
        Me.btnLabClear.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.btnLabClear, "Clear Search")
        Me.btnLabClear.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(29, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1140, 4)
        Me.Label1.TabIndex = 37
        '
        'Label165
        '
        Me.Label165.BackColor = System.Drawing.Color.White
        Me.Label165.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label165.Location = New System.Drawing.Point(29, 20)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(1140, 2)
        Me.Label165.TabIndex = 38
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.White
        Me.PictureBox6.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 9
        Me.PictureBox6.TabStop = False
        '
        'Label166
        '
        Me.Label166.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label166.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label166.Location = New System.Drawing.Point(1, 22)
        Me.Label166.Name = "Label166"
        Me.Label166.Size = New System.Drawing.Size(1168, 1)
        Me.Label166.TabIndex = 35
        Me.Label166.Text = "label1"
        '
        'Label167
        '
        Me.Label167.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label167.Location = New System.Drawing.Point(1, 0)
        Me.Label167.Name = "Label167"
        Me.Label167.Size = New System.Drawing.Size(1168, 1)
        Me.Label167.TabIndex = 36
        Me.Label167.Text = "label1"
        '
        'Label168
        '
        Me.Label168.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label168.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label168.Location = New System.Drawing.Point(0, 0)
        Me.Label168.Name = "Label168"
        Me.Label168.Size = New System.Drawing.Size(1, 23)
        Me.Label168.TabIndex = 39
        Me.Label168.Text = "label4"
        '
        'Label169
        '
        Me.Label169.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label169.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label169.Location = New System.Drawing.Point(1169, 0)
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
        Me.pnlInternalToolStripRadiology.Size = New System.Drawing.Size(1170, 54)
        Me.pnlInternalToolStripRadiology.TabIndex = 56
        '
        'Panel41
        '
        Me.Panel41.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel41.Controls.Add(Me.ToolStrip6)
        Me.Panel41.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel41.Location = New System.Drawing.Point(0, 0)
        Me.Panel41.Name = "Panel41"
        Me.Panel41.Size = New System.Drawing.Size(1170, 54)
        Me.Panel41.TabIndex = 4
        '
        'ToolStrip6
        '
        Me.ToolStrip6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip6.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip6.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveRadiology, Me.tsBtn_CancelRadiology})
        Me.ToolStrip6.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip6.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip6.Name = "ToolStrip6"
        Me.ToolStrip6.Size = New System.Drawing.Size(1170, 53)
        Me.ToolStrip6.TabIndex = 0
        Me.ToolStrip6.Text = "ToolStrip6"
        '
        'tsBtn_SaveRadiology
        '
        Me.tsBtn_SaveRadiology.Image = CType(resources.GetObject("tsBtn_SaveRadiology.Image"), System.Drawing.Image)
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
        Me.tsBtn_CancelRadiology.Image = CType(resources.GetObject("tsBtn_CancelRadiology.Image"), System.Drawing.Image)
        Me.tsBtn_CancelRadiology.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelRadiology.Name = "tsBtn_CancelRadiology"
        Me.tsBtn_CancelRadiology.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelRadiology.Tag = "Cancel"
        Me.tsBtn_CancelRadiology.Text = "&Cancel"
        Me.tsBtn_CancelRadiology.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelRadiology.Visible = False
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
        Me.pnlHistory.Size = New System.Drawing.Size(1170, 835)
        Me.pnlHistory.TabIndex = 0
        Me.pnlHistory.Visible = False
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
        Me.Panel22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel22.Location = New System.Drawing.Point(0, 465)
        Me.Panel22.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel22.Size = New System.Drawing.Size(1170, 370)
        Me.Panel22.TabIndex = 23
        '
        'Label193
        '
        Me.Label193.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label193.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label193.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label193.Location = New System.Drawing.Point(1, 366)
        Me.Label193.Name = "Label193"
        Me.Label193.Size = New System.Drawing.Size(1168, 1)
        Me.Label193.TabIndex = 8
        Me.Label193.Text = "label2"
        '
        'Label194
        '
        Me.Label194.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label194.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label194.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label194.Location = New System.Drawing.Point(0, 1)
        Me.Label194.Name = "Label194"
        Me.Label194.Size = New System.Drawing.Size(1, 366)
        Me.Label194.TabIndex = 7
        Me.Label194.Text = "label4"
        '
        'trvSelectedHistory
        '
        Me.trvSelectedHistory.BackColor = System.Drawing.Color.White
        Me.trvSelectedHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvSelectedHistory.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedHistory.HideSelection = False
        Me.trvSelectedHistory.ImageIndex = 11
        Me.trvSelectedHistory.ImageList = Me.ImageList1
        Me.trvSelectedHistory.ItemHeight = 18
        Me.trvSelectedHistory.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedHistory.Name = "trvSelectedHistory"
        Me.trvSelectedHistory.SelectedImageIndex = 11
        Me.trvSelectedHistory.ShowLines = False
        Me.trvSelectedHistory.Size = New System.Drawing.Size(1169, 366)
        Me.trvSelectedHistory.TabIndex = 0
        '
        'Label195
        '
        Me.Label195.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label195.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label195.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label195.Location = New System.Drawing.Point(1169, 1)
        Me.Label195.Name = "Label195"
        Me.Label195.Size = New System.Drawing.Size(1, 366)
        Me.Label195.TabIndex = 6
        Me.Label195.Text = "label3"
        '
        'Label196
        '
        Me.Label196.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label196.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label196.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label196.Location = New System.Drawing.Point(0, 0)
        Me.Label196.Name = "Label196"
        Me.Label196.Size = New System.Drawing.Size(1170, 1)
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
        Me.Panel20.Size = New System.Drawing.Size(1170, 27)
        Me.Panel20.TabIndex = 22
        '
        'Panel21
        '
        Me.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel21.Controls.Add(Me.Label188)
        Me.Panel21.Controls.Add(Me.Label189)
        Me.Panel21.Controls.Add(Me.Label190)
        Me.Panel21.Controls.Add(Me.Label191)
        Me.Panel21.Controls.Add(Me.Label192)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel21.Location = New System.Drawing.Point(0, 3)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(1170, 21)
        Me.Panel21.TabIndex = 14
        '
        'Label188
        '
        Me.Label188.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label188.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label188.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label188.Location = New System.Drawing.Point(1169, 1)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(1, 19)
        Me.Label188.TabIndex = 11
        Me.Label188.Text = "label3"
        '
        'Label189
        '
        Me.Label189.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label189.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label189.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label189.Location = New System.Drawing.Point(0, 1)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(1, 19)
        Me.Label189.TabIndex = 12
        Me.Label189.Text = "label4"
        '
        'Label190
        '
        Me.Label190.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label190.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label190.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label190.Location = New System.Drawing.Point(0, 0)
        Me.Label190.Name = "Label190"
        Me.Label190.Size = New System.Drawing.Size(1170, 1)
        Me.Label190.TabIndex = 10
        Me.Label190.Text = "label1"
        '
        'Label191
        '
        Me.Label191.BackColor = System.Drawing.Color.Transparent
        Me.Label191.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label191.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label191.ForeColor = System.Drawing.Color.White
        Me.Label191.Image = CType(resources.GetObject("Label191.Image"), System.Drawing.Image)
        Me.Label191.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label191.Location = New System.Drawing.Point(0, 0)
        Me.Label191.Name = "Label191"
        Me.Label191.Size = New System.Drawing.Size(1170, 20)
        Me.Label191.TabIndex = 9
        Me.Label191.Text = "      Selected History"
        Me.Label191.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label192
        '
        Me.Label192.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label192.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label192.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label192.Location = New System.Drawing.Point(0, 20)
        Me.Label192.Name = "Label192"
        Me.Label192.Size = New System.Drawing.Size(1170, 1)
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
        Me.Panel16.Size = New System.Drawing.Size(1170, 357)
        Me.Panel16.TabIndex = 23
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.GloUC_trvHistory)
        Me.Panel9.Controls.Add(Me.trvHistoryRight)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(287, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(883, 357)
        Me.Panel9.TabIndex = 20
        '
        'GloUC_trvHistory
        '
        Me.GloUC_trvHistory.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvHistory.CheckBoxes = False
        Me.GloUC_trvHistory.CodeMember = Nothing
        Me.GloUC_trvHistory.ColonAsSeparator = False
        Me.GloUC_trvHistory.Comment = Nothing
        Me.GloUC_trvHistory.ConceptID = Nothing
        Me.GloUC_trvHistory.CPT = Nothing
        Me.GloUC_trvHistory.DDIDMember = Nothing
        Me.GloUC_trvHistory.DescriptionMember = Nothing
        Me.GloUC_trvHistory.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvHistory.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvHistory.DrugFlag = CType(16, Short)
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
        Me.GloUC_trvHistory.IsCPTSearch = False
        Me.GloUC_trvHistory.IsDiagnosisSearch = False
        Me.GloUC_trvHistory.IsDrug = False
        Me.GloUC_trvHistory.IsNarcoticsMember = Nothing
        Me.GloUC_trvHistory.IsSearchForEducationMapping = False
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
        Me.GloUC_trvHistory.SearchBox = True
        Me.GloUC_trvHistory.SearchText = Nothing
        Me.GloUC_trvHistory.SelectedImageIndex = 0
        Me.GloUC_trvHistory.SelectedNode = Nothing
        Me.GloUC_trvHistory.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvHistory.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvHistory.SelectedParentImageIndex = 0
        Me.GloUC_trvHistory.Size = New System.Drawing.Size(883, 357)
        Me.GloUC_trvHistory.SmartTreatmentId = Nothing
        Me.GloUC_trvHistory.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvHistory.TabIndex = 48
        Me.GloUC_trvHistory.Tag = Nothing
        Me.GloUC_trvHistory.UnitMember = Nothing
        Me.GloUC_trvHistory.ValueMember = Nothing
        '
        'trvHistoryRight
        '
        Me.trvHistoryRight.BackColor = System.Drawing.Color.White
        Me.trvHistoryRight.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvHistoryRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvHistoryRight.ForeColor = System.Drawing.Color.Black
        Me.trvHistoryRight.HideSelection = False
        Me.trvHistoryRight.ImageIndex = 0
        Me.trvHistoryRight.ImageList = Me.ImageList1
        Me.trvHistoryRight.ItemHeight = 18
        Me.trvHistoryRight.Location = New System.Drawing.Point(0, 1)
        Me.trvHistoryRight.Name = "trvHistoryRight"
        Me.trvHistoryRight.SelectedImageIndex = 0
        Me.trvHistoryRight.ShowLines = False
        Me.trvHistoryRight.Size = New System.Drawing.Size(326, 274)
        Me.trvHistoryRight.TabIndex = 2
        Me.trvHistoryRight.Visible = False
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
        Me.pnlHistoryLeft.Enabled = False
        Me.pnlHistoryLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlHistoryLeft.Name = "pnlHistoryLeft"
        Me.pnlHistoryLeft.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.pnlHistoryLeft.Size = New System.Drawing.Size(287, 357)
        Me.pnlHistoryLeft.TabIndex = 1
        Me.pnlHistoryLeft.Visible = False
        '
        'Label120
        '
        Me.Label120.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label120.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label120.Location = New System.Drawing.Point(1, 356)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(282, 1)
        Me.Label120.TabIndex = 12
        Me.Label120.Text = "label2"
        '
        'Label121
        '
        Me.Label121.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label121.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label121.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label121.Location = New System.Drawing.Point(0, 1)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(1, 356)
        Me.Label121.TabIndex = 11
        Me.Label121.Text = "label4"
        '
        'Label122
        '
        Me.Label122.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label122.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label122.Location = New System.Drawing.Point(283, 1)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(1, 356)
        Me.Label122.TabIndex = 10
        Me.Label122.Text = "label3"
        '
        'Label123
        '
        Me.Label123.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label123.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label123.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.trvHistory.HideSelection = False
        Me.trvHistory.ItemHeight = 18
        Me.trvHistory.Location = New System.Drawing.Point(0, 0)
        Me.trvHistory.Name = "trvHistory"
        Me.trvHistory.ShowLines = False
        Me.trvHistory.Size = New System.Drawing.Size(284, 357)
        Me.trvHistory.TabIndex = 0
        Me.trvHistory.Visible = False
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Panel8)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 54)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel11.Size = New System.Drawing.Size(1170, 27)
        Me.Panel11.TabIndex = 21
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.cmbHistoryCategory)
        Me.Panel8.Controls.Add(Me.Label140)
        Me.Panel8.Controls.Add(Me.txtSearch)
        Me.Panel8.Controls.Add(Me.Label8)
        Me.Panel8.Controls.Add(Me.Label14)
        Me.Panel8.Controls.Add(Me.Label115)
        Me.Panel8.Controls.Add(Me.Label116)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.ForeColor = System.Drawing.Color.Black
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1170, 24)
        Me.Panel8.TabIndex = 19
        '
        'cmbHistoryCategory
        '
        Me.cmbHistoryCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbHistoryCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHistoryCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbHistoryCategory.FormattingEnabled = True
        Me.cmbHistoryCategory.Location = New System.Drawing.Point(131, 1)
        Me.cmbHistoryCategory.Name = "cmbHistoryCategory"
        Me.cmbHistoryCategory.Size = New System.Drawing.Size(284, 22)
        Me.cmbHistoryCategory.TabIndex = 0
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.Transparent
        Me.Label140.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label140.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(507, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(87, 15)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(1, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1168, 1)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 23)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "label4"
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label115.Location = New System.Drawing.Point(1169, 1)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(1, 23)
        Me.Label115.TabIndex = 10
        Me.Label115.Text = "label3"
        '
        'Label116
        '
        Me.Label116.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label116.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label116.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label116.Location = New System.Drawing.Point(0, 0)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(1170, 1)
        Me.Label116.TabIndex = 9
        Me.Label116.Text = "label1"
        '
        'btnHistorySearch
        '
        Me.btnHistorySearch.BackColor = System.Drawing.Color.Transparent
        Me.btnHistorySearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnHistorySearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnHistorySearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnHistorySearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistorySearch.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistorySearch.Location = New System.Drawing.Point(254, 134)
        Me.btnHistorySearch.Name = "btnHistorySearch"
        Me.btnHistorySearch.Size = New System.Drawing.Size(63, 22)
        Me.btnHistorySearch.TabIndex = 2
        Me.btnHistorySearch.Text = "Search"
        Me.btnHistorySearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnHistorySearch.UseVisualStyleBackColor = False
        Me.btnHistorySearch.Visible = False
        '
        'pnlInternalToolStripHistory
        '
        Me.pnlInternalToolStripHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripHistory.Controls.Add(Me.Panel33)
        Me.pnlInternalToolStripHistory.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripHistory.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripHistory.Name = "pnlInternalToolStripHistory"
        Me.pnlInternalToolStripHistory.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripHistory.Size = New System.Drawing.Size(1170, 54)
        Me.pnlInternalToolStripHistory.TabIndex = 55
        '
        'Panel33
        '
        Me.Panel33.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel33.Controls.Add(Me.ToolStrip4)
        Me.Panel33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel33.Location = New System.Drawing.Point(0, 0)
        Me.Panel33.Name = "Panel33"
        Me.Panel33.Size = New System.Drawing.Size(1170, 54)
        Me.Panel33.TabIndex = 4
        '
        'ToolStrip4
        '
        Me.ToolStrip4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip4.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveHistory, Me.tsBtn_CancelHistory})
        Me.ToolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip4.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip4.Name = "ToolStrip4"
        Me.ToolStrip4.Size = New System.Drawing.Size(1170, 21)
        Me.ToolStrip4.TabIndex = 0
        Me.ToolStrip4.Text = "ToolStrip4"
        '
        'tsBtn_SaveHistory
        '
        Me.tsBtn_SaveHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveHistory.Name = "tsBtn_SaveHistory"
        Me.tsBtn_SaveHistory.Size = New System.Drawing.Size(43, 18)
        Me.tsBtn_SaveHistory.Tag = "Save"
        Me.tsBtn_SaveHistory.Text = "&Done"
        Me.tsBtn_SaveHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveHistory.ToolTipText = "Done"
        '
        'tsBtn_CancelHistory
        '
        Me.tsBtn_CancelHistory.Image = CType(resources.GetObject("tsBtn_CancelHistory.Image"), System.Drawing.Image)
        Me.tsBtn_CancelHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelHistory.Name = "tsBtn_CancelHistory"
        Me.tsBtn_CancelHistory.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelHistory.Tag = "Cancel"
        Me.tsBtn_CancelHistory.Text = "&Cancel"
        Me.tsBtn_CancelHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelHistory.Visible = False
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
        Me.pnlInsurance.Size = New System.Drawing.Size(1170, 835)
        Me.pnlInsurance.TabIndex = 10
        Me.pnlInsurance.Visible = False
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
        Me.pnltrvSelectedInsurance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvSelectedInsurance.Location = New System.Drawing.Point(0, 476)
        Me.pnltrvSelectedInsurance.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedInsurance.Name = "pnltrvSelectedInsurance"
        Me.pnltrvSelectedInsurance.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedInsurance.Size = New System.Drawing.Size(1170, 359)
        Me.pnltrvSelectedInsurance.TabIndex = 21
        '
        'Label329
        '
        Me.Label329.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label329.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label329.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label329.Location = New System.Drawing.Point(1, 355)
        Me.Label329.Name = "Label329"
        Me.Label329.Size = New System.Drawing.Size(1168, 1)
        Me.Label329.TabIndex = 8
        Me.Label329.Text = "label2"
        '
        'Label330
        '
        Me.Label330.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label330.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label330.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label330.Location = New System.Drawing.Point(0, 1)
        Me.Label330.Name = "Label330"
        Me.Label330.Size = New System.Drawing.Size(1, 355)
        Me.Label330.TabIndex = 7
        Me.Label330.Text = "label4"
        '
        'trvSelectedInsurance
        '
        Me.trvSelectedInsurance.BackColor = System.Drawing.Color.White
        Me.trvSelectedInsurance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedInsurance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedInsurance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvSelectedInsurance.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedInsurance.HideSelection = False
        Me.trvSelectedInsurance.ImageIndex = 0
        Me.trvSelectedInsurance.ImageList = Me.ImageList1
        Me.trvSelectedInsurance.ItemHeight = 18
        Me.trvSelectedInsurance.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedInsurance.Name = "trvSelectedInsurance"
        Me.trvSelectedInsurance.SelectedImageIndex = 0
        Me.trvSelectedInsurance.ShowLines = False
        Me.trvSelectedInsurance.Size = New System.Drawing.Size(1169, 355)
        Me.trvSelectedInsurance.TabIndex = 0
        '
        'Label331
        '
        Me.Label331.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label331.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label331.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label331.Location = New System.Drawing.Point(1169, 1)
        Me.Label331.Name = "Label331"
        Me.Label331.Size = New System.Drawing.Size(1, 355)
        Me.Label331.TabIndex = 6
        Me.Label331.Text = "label3"
        '
        'Label332
        '
        Me.Label332.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label332.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label332.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label332.Location = New System.Drawing.Point(0, 0)
        Me.Label332.Name = "Label332"
        Me.Label332.Size = New System.Drawing.Size(1170, 1)
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
        Me.pnlSelectedInsuranceLabel.Size = New System.Drawing.Size(1170, 27)
        Me.pnlSelectedInsuranceLabel.TabIndex = 20
        '
        'Panel56
        '
        Me.Panel56.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel56.Controls.Add(Me.Label333)
        Me.Panel56.Controls.Add(Me.Label334)
        Me.Panel56.Controls.Add(Me.Label335)
        Me.Panel56.Controls.Add(Me.Label336)
        Me.Panel56.Controls.Add(Me.Label337)
        Me.Panel56.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel56.Location = New System.Drawing.Point(0, 0)
        Me.Panel56.Name = "Panel56"
        Me.Panel56.Size = New System.Drawing.Size(1170, 24)
        Me.Panel56.TabIndex = 14
        '
        'Label333
        '
        Me.Label333.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label333.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label333.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label333.Location = New System.Drawing.Point(1169, 1)
        Me.Label333.Name = "Label333"
        Me.Label333.Size = New System.Drawing.Size(1, 22)
        Me.Label333.TabIndex = 11
        Me.Label333.Text = "label3"
        '
        'Label334
        '
        Me.Label334.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label334.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label334.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label334.Location = New System.Drawing.Point(0, 1)
        Me.Label334.Name = "Label334"
        Me.Label334.Size = New System.Drawing.Size(1, 22)
        Me.Label334.TabIndex = 12
        Me.Label334.Text = "label4"
        '
        'Label335
        '
        Me.Label335.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label335.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label335.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label335.Location = New System.Drawing.Point(0, 0)
        Me.Label335.Name = "Label335"
        Me.Label335.Size = New System.Drawing.Size(1170, 1)
        Me.Label335.TabIndex = 10
        Me.Label335.Text = "label1"
        '
        'Label336
        '
        Me.Label336.BackColor = System.Drawing.Color.Transparent
        Me.Label336.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label336.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label336.ForeColor = System.Drawing.Color.White
        Me.Label336.Image = CType(resources.GetObject("Label336.Image"), System.Drawing.Image)
        Me.Label336.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label336.Location = New System.Drawing.Point(0, 0)
        Me.Label336.Name = "Label336"
        Me.Label336.Size = New System.Drawing.Size(1170, 23)
        Me.Label336.TabIndex = 9
        Me.Label336.Text = "      Selected Insurance Plan"
        Me.Label336.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label337
        '
        Me.Label337.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label337.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label337.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label337.Location = New System.Drawing.Point(0, 23)
        Me.Label337.Name = "Label337"
        Me.Label337.Size = New System.Drawing.Size(1170, 1)
        Me.Label337.TabIndex = 13
        Me.Label337.Text = "label2"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 446)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(1170, 3)
        Me.Splitter2.TabIndex = 22
        Me.Splitter2.TabStop = False
        '
        'GloUC_trvInsurance
        '
        Me.GloUC_trvInsurance.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvInsurance.CheckBoxes = False
        Me.GloUC_trvInsurance.CodeMember = Nothing
        Me.GloUC_trvInsurance.ColonAsSeparator = False
        Me.GloUC_trvInsurance.Comment = Nothing
        Me.GloUC_trvInsurance.ConceptID = Nothing
        Me.GloUC_trvInsurance.CPT = Nothing
        Me.GloUC_trvInsurance.DDIDMember = Nothing
        Me.GloUC_trvInsurance.DescriptionMember = Nothing
        Me.GloUC_trvInsurance.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvInsurance.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvInsurance.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvInsurance.DrugFlag = CType(16, Short)
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
        Me.GloUC_trvInsurance.IsCPTSearch = False
        Me.GloUC_trvInsurance.IsDiagnosisSearch = False
        Me.GloUC_trvInsurance.IsDrug = False
        Me.GloUC_trvInsurance.IsNarcoticsMember = Nothing
        Me.GloUC_trvInsurance.IsSearchForEducationMapping = False
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
        Me.GloUC_trvInsurance.SearchBox = True
        Me.GloUC_trvInsurance.SearchText = Nothing
        Me.GloUC_trvInsurance.SelectedImageIndex = 0
        Me.GloUC_trvInsurance.SelectedNode = Nothing
        Me.GloUC_trvInsurance.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvInsurance.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvInsurance.SelectedParentImageIndex = 0
        Me.GloUC_trvInsurance.Size = New System.Drawing.Size(1170, 392)
        Me.GloUC_trvInsurance.SmartTreatmentId = Nothing
        Me.GloUC_trvInsurance.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvInsurance.TabIndex = 9
        Me.GloUC_trvInsurance.Tag = Nothing
        Me.GloUC_trvInsurance.UnitMember = Nothing
        Me.GloUC_trvInsurance.ValueMember = Nothing
        '
        'pnlInternalToolStripInsurance
        '
        Me.pnlInternalToolStripInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripInsurance.Controls.Add(Me.Panel58)
        Me.pnlInternalToolStripInsurance.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripInsurance.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripInsurance.Name = "pnlInternalToolStripInsurance"
        Me.pnlInternalToolStripInsurance.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripInsurance.Size = New System.Drawing.Size(1170, 54)
        Me.pnlInternalToolStripInsurance.TabIndex = 54
        '
        'Panel58
        '
        Me.Panel58.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel58.Controls.Add(Me.ToolStrip13)
        Me.Panel58.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel58.Location = New System.Drawing.Point(0, 0)
        Me.Panel58.Name = "Panel58"
        Me.Panel58.Size = New System.Drawing.Size(1170, 54)
        Me.Panel58.TabIndex = 4
        '
        'ToolStrip13
        '
        Me.ToolStrip13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip13.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip13.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveInsurance, Me.tsBtn_CancelInsurance})
        Me.ToolStrip13.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip13.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip13.Name = "ToolStrip13"
        Me.ToolStrip13.Size = New System.Drawing.Size(1170, 21)
        Me.ToolStrip13.TabIndex = 0
        Me.ToolStrip13.Text = "ToolStrip13"
        '
        'tsBtn_SaveInsurance
        '
        Me.tsBtn_SaveInsurance.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveInsurance.Name = "tsBtn_SaveInsurance"
        Me.tsBtn_SaveInsurance.Size = New System.Drawing.Size(43, 18)
        Me.tsBtn_SaveInsurance.Tag = "Save"
        Me.tsBtn_SaveInsurance.Text = "&Done"
        Me.tsBtn_SaveInsurance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveInsurance.ToolTipText = "Done"
        '
        'tsBtn_CancelInsurance
        '
        Me.tsBtn_CancelInsurance.Image = CType(resources.GetObject("tsBtn_CancelInsurance.Image"), System.Drawing.Image)
        Me.tsBtn_CancelInsurance.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelInsurance.Name = "tsBtn_CancelInsurance"
        Me.tsBtn_CancelInsurance.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelInsurance.Tag = "Cancel"
        Me.tsBtn_CancelInsurance.Text = "&Cancel"
        Me.tsBtn_CancelInsurance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelInsurance.Visible = False
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
        Me.pnlICD9.Size = New System.Drawing.Size(1170, 835)
        Me.pnlICD9.TabIndex = 1
        Me.pnlICD9.Visible = False
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
        Me.Panel15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel15.Location = New System.Drawing.Point(0, 864)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel15.Size = New System.Drawing.Size(1170, 0)
        Me.Panel15.TabIndex = 50
        '
        'Label132
        '
        Me.Label132.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label132.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label132.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label132.Location = New System.Drawing.Point(1, -4)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(1168, 1)
        Me.Label132.TabIndex = 8
        Me.Label132.Text = "label2"
        '
        'Label133
        '
        Me.Label133.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label133.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label133.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.trvselecteICD10s.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvselecteICD10s.ForeColor = System.Drawing.Color.Black
        Me.trvselecteICD10s.HideSelection = False
        Me.trvselecteICD10s.ImageIndex = 0
        Me.trvselecteICD10s.ImageList = Me.ImageList1
        Me.trvselecteICD10s.ItemHeight = 18
        Me.trvselecteICD10s.Location = New System.Drawing.Point(0, 1)
        Me.trvselecteICD10s.Name = "trvselecteICD10s"
        Me.trvselecteICD10s.SelectedImageIndex = 0
        Me.trvselecteICD10s.ShowLines = False
        Me.trvselecteICD10s.Size = New System.Drawing.Size(1169, 0)
        Me.trvselecteICD10s.TabIndex = 9
        '
        'trvselecteICDs
        '
        Me.trvselecteICDs.BackColor = System.Drawing.Color.White
        Me.trvselecteICDs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselecteICDs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselecteICDs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvselecteICDs.ForeColor = System.Drawing.Color.Black
        Me.trvselecteICDs.HideSelection = False
        Me.trvselecteICDs.ImageIndex = 0
        Me.trvselecteICDs.ImageList = Me.ImageList1
        Me.trvselecteICDs.ItemHeight = 18
        Me.trvselecteICDs.Location = New System.Drawing.Point(0, 1)
        Me.trvselecteICDs.Name = "trvselecteICDs"
        Me.trvselecteICDs.SelectedImageIndex = 0
        Me.trvselecteICDs.ShowLines = False
        Me.trvselecteICDs.Size = New System.Drawing.Size(1169, 0)
        Me.trvselecteICDs.TabIndex = 0
        '
        'Label134
        '
        Me.Label134.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label134.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label134.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label134.Location = New System.Drawing.Point(1169, 1)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(1, 0)
        Me.Label134.TabIndex = 6
        Me.Label134.Text = "label3"
        '
        'Label135
        '
        Me.Label135.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label135.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label135.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label135.Location = New System.Drawing.Point(0, 0)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(1170, 1)
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
        Me.Panel5.Size = New System.Drawing.Size(1170, 27)
        Me.Panel5.TabIndex = 49
        '
        'Panel10
        '
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label13)
        Me.Panel10.Controls.Add(Me.Label128)
        Me.Panel10.Controls.Add(Me.Label129)
        Me.Panel10.Controls.Add(Me.Label130)
        Me.Panel10.Controls.Add(Me.Label131)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1170, 24)
        Me.Panel10.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1169, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 22)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "label3"
        '
        'Label128
        '
        Me.Label128.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label128.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label128.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label128.Location = New System.Drawing.Point(0, 1)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(1, 22)
        Me.Label128.TabIndex = 12
        Me.Label128.Text = "label4"
        '
        'Label129
        '
        Me.Label129.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label129.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label129.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label129.Location = New System.Drawing.Point(0, 0)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(1170, 1)
        Me.Label129.TabIndex = 10
        Me.Label129.Text = "label1"
        '
        'Label130
        '
        Me.Label130.BackColor = System.Drawing.Color.Transparent
        Me.Label130.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label130.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label130.ForeColor = System.Drawing.Color.White
        Me.Label130.Image = CType(resources.GetObject("Label130.Image"), System.Drawing.Image)
        Me.Label130.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label130.Location = New System.Drawing.Point(0, 0)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(1170, 23)
        Me.Label130.TabIndex = 9
        Me.Label130.Text = "      Selected ICD9"
        Me.Label130.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label131
        '
        Me.Label131.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label131.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label131.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label131.Location = New System.Drawing.Point(0, 23)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(1170, 1)
        Me.Label131.TabIndex = 13
        Me.Label131.Text = "label2"
        '
        'Splitter4
        '
        Me.Splitter4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter4.Location = New System.Drawing.Point(0, 834)
        Me.Splitter4.Name = "Splitter4"
        Me.Splitter4.Size = New System.Drawing.Size(1170, 3)
        Me.Splitter4.TabIndex = 51
        Me.Splitter4.TabStop = False
        '
        'GloUC_trvICD10
        '
        Me.GloUC_trvICD10.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvICD10.CheckBoxes = False
        Me.GloUC_trvICD10.CodeMember = Nothing
        Me.GloUC_trvICD10.ColonAsSeparator = False
        Me.GloUC_trvICD10.Comment = Nothing
        Me.GloUC_trvICD10.ConceptID = Nothing
        Me.GloUC_trvICD10.CPT = Nothing
        Me.GloUC_trvICD10.DDIDMember = Nothing
        Me.GloUC_trvICD10.DescriptionMember = Nothing
        Me.GloUC_trvICD10.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvICD10.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvICD10.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvICD10.DrugFlag = CType(16, Short)
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
        Me.GloUC_trvICD10.IsCPTSearch = False
        Me.GloUC_trvICD10.IsDiagnosisSearch = False
        Me.GloUC_trvICD10.IsDrug = False
        Me.GloUC_trvICD10.IsNarcoticsMember = Nothing
        Me.GloUC_trvICD10.IsSearchForEducationMapping = False
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
        Me.GloUC_trvICD10.SearchBox = True
        Me.GloUC_trvICD10.SearchText = Nothing
        Me.GloUC_trvICD10.SelectedImageIndex = 0
        Me.GloUC_trvICD10.SelectedNode = Nothing
        Me.GloUC_trvICD10.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvICD10.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvICD10.SelectedParentImageIndex = 0
        Me.GloUC_trvICD10.Size = New System.Drawing.Size(1170, 390)
        Me.GloUC_trvICD10.SmartTreatmentId = Nothing
        Me.GloUC_trvICD10.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvICD10.TabIndex = 53
        Me.GloUC_trvICD10.Tag = Nothing
        Me.GloUC_trvICD10.UnitMember = Nothing
        Me.GloUC_trvICD10.ValueMember = Nothing
        '
        'GloUC_trvICD9
        '
        Me.GloUC_trvICD9.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvICD9.CheckBoxes = False
        Me.GloUC_trvICD9.CodeMember = Nothing
        Me.GloUC_trvICD9.ColonAsSeparator = False
        Me.GloUC_trvICD9.Comment = Nothing
        Me.GloUC_trvICD9.ConceptID = Nothing
        Me.GloUC_trvICD9.CPT = Nothing
        Me.GloUC_trvICD9.DDIDMember = Nothing
        Me.GloUC_trvICD9.DescriptionMember = Nothing
        Me.GloUC_trvICD9.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvICD9.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvICD9.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvICD9.DrugFlag = CType(16, Short)
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
        Me.GloUC_trvICD9.IsCPTSearch = False
        Me.GloUC_trvICD9.IsDiagnosisSearch = False
        Me.GloUC_trvICD9.IsDrug = False
        Me.GloUC_trvICD9.IsNarcoticsMember = Nothing
        Me.GloUC_trvICD9.IsSearchForEducationMapping = False
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
        Me.GloUC_trvICD9.SearchBox = True
        Me.GloUC_trvICD9.SearchText = Nothing
        Me.GloUC_trvICD9.SelectedImageIndex = 0
        Me.GloUC_trvICD9.SelectedNode = Nothing
        Me.GloUC_trvICD9.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvICD9.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvICD9.SelectedParentImageIndex = 0
        Me.GloUC_trvICD9.Size = New System.Drawing.Size(1170, 390)
        Me.GloUC_trvICD9.SmartTreatmentId = Nothing
        Me.GloUC_trvICD9.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvICD9.TabIndex = 47
        Me.GloUC_trvICD9.Tag = Nothing
        Me.GloUC_trvICD9.UnitMember = Nothing
        Me.GloUC_trvICD9.ValueMember = Nothing
        '
        'pnlInternalToolStripICD
        '
        Me.pnlInternalToolStripICD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripICD.Controls.Add(Me.Panel29)
        Me.pnlInternalToolStripICD.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripICD.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripICD.Name = "pnlInternalToolStripICD"
        Me.pnlInternalToolStripICD.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripICD.Size = New System.Drawing.Size(1170, 54)
        Me.pnlInternalToolStripICD.TabIndex = 52
        '
        'Panel29
        '
        Me.Panel29.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel29.Controls.Add(Me.ToolStrip1)
        Me.Panel29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel29.Location = New System.Drawing.Point(0, 0)
        Me.Panel29.Name = "Panel29"
        Me.Panel29.Size = New System.Drawing.Size(1170, 54)
        Me.Panel29.TabIndex = 4
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveICD, Me.tsBtn_CancelICD, Me.tsBtn_SaveICD10})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1170, 21)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsBtn_SaveICD
        '
        Me.tsBtn_SaveICD.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveICD.Name = "tsBtn_SaveICD"
        Me.tsBtn_SaveICD.Size = New System.Drawing.Size(43, 18)
        Me.tsBtn_SaveICD.Tag = "Save"
        Me.tsBtn_SaveICD.Text = "&Done"
        Me.tsBtn_SaveICD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveICD.ToolTipText = "Done"
        '
        'tsBtn_CancelICD
        '
        Me.tsBtn_CancelICD.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelICD.Name = "tsBtn_CancelICD"
        Me.tsBtn_CancelICD.Size = New System.Drawing.Size(50, 18)
        Me.tsBtn_CancelICD.Tag = "Cancel"
        Me.tsBtn_CancelICD.Text = "&Cancel"
        Me.tsBtn_CancelICD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelICD.Visible = False
        '
        'tsBtn_SaveICD10
        '
        Me.tsBtn_SaveICD10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveICD10.Name = "tsBtn_SaveICD10"
        Me.tsBtn_SaveICD10.Size = New System.Drawing.Size(43, 18)
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
        Me.pnlCPT.Size = New System.Drawing.Size(1170, 835)
        Me.pnlCPT.TabIndex = 1
        Me.pnlCPT.Visible = False
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
        Me.pnlSelectedCPTs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSelectedCPTs.Location = New System.Drawing.Point(0, 476)
        Me.pnlSelectedCPTs.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedCPTs.Name = "pnlSelectedCPTs"
        Me.pnlSelectedCPTs.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSelectedCPTs.Size = New System.Drawing.Size(1170, 359)
        Me.pnlSelectedCPTs.TabIndex = 47
        '
        'Label205
        '
        Me.Label205.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label205.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label205.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label205.Location = New System.Drawing.Point(1, 355)
        Me.Label205.Name = "Label205"
        Me.Label205.Size = New System.Drawing.Size(1168, 1)
        Me.Label205.TabIndex = 8
        Me.Label205.Text = "label2"
        '
        'Label206
        '
        Me.Label206.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label206.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label206.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label206.Location = New System.Drawing.Point(0, 1)
        Me.Label206.Name = "Label206"
        Me.Label206.Size = New System.Drawing.Size(1, 355)
        Me.Label206.TabIndex = 7
        Me.Label206.Text = "label4"
        '
        'trvselectedCPT
        '
        Me.trvselectedCPT.BackColor = System.Drawing.Color.White
        Me.trvselectedCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvselectedCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvselectedCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvselectedCPT.ForeColor = System.Drawing.Color.Black
        Me.trvselectedCPT.HideSelection = False
        Me.trvselectedCPT.ImageIndex = 0
        Me.trvselectedCPT.ImageList = Me.ImageList1
        Me.trvselectedCPT.ItemHeight = 18
        Me.trvselectedCPT.Location = New System.Drawing.Point(0, 1)
        Me.trvselectedCPT.Name = "trvselectedCPT"
        Me.trvselectedCPT.SelectedImageIndex = 0
        Me.trvselectedCPT.ShowLines = False
        Me.trvselectedCPT.Size = New System.Drawing.Size(1169, 355)
        Me.trvselectedCPT.TabIndex = 0
        '
        'Label207
        '
        Me.Label207.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label207.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label207.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label207.Location = New System.Drawing.Point(1169, 1)
        Me.Label207.Name = "Label207"
        Me.Label207.Size = New System.Drawing.Size(1, 355)
        Me.Label207.TabIndex = 6
        Me.Label207.Text = "label3"
        '
        'Label208
        '
        Me.Label208.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label208.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label208.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label208.Location = New System.Drawing.Point(0, 0)
        Me.Label208.Name = "Label208"
        Me.Label208.Size = New System.Drawing.Size(1170, 1)
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
        Me.pnlSelecteCPTsLabels.Size = New System.Drawing.Size(1170, 27)
        Me.pnlSelecteCPTsLabels.TabIndex = 48
        '
        'Panel28
        '
        Me.Panel28.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel28.Controls.Add(Me.Label209)
        Me.Panel28.Controls.Add(Me.Label210)
        Me.Panel28.Controls.Add(Me.Label211)
        Me.Panel28.Controls.Add(Me.Label212)
        Me.Panel28.Controls.Add(Me.Label213)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel28.Location = New System.Drawing.Point(0, 0)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Size = New System.Drawing.Size(1170, 24)
        Me.Panel28.TabIndex = 14
        '
        'Label209
        '
        Me.Label209.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label209.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label209.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label209.Location = New System.Drawing.Point(1169, 1)
        Me.Label209.Name = "Label209"
        Me.Label209.Size = New System.Drawing.Size(1, 22)
        Me.Label209.TabIndex = 11
        Me.Label209.Text = "label3"
        '
        'Label210
        '
        Me.Label210.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label210.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label210.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label210.Location = New System.Drawing.Point(0, 1)
        Me.Label210.Name = "Label210"
        Me.Label210.Size = New System.Drawing.Size(1, 22)
        Me.Label210.TabIndex = 12
        Me.Label210.Text = "label4"
        '
        'Label211
        '
        Me.Label211.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label211.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label211.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label211.Location = New System.Drawing.Point(0, 0)
        Me.Label211.Name = "Label211"
        Me.Label211.Size = New System.Drawing.Size(1170, 1)
        Me.Label211.TabIndex = 10
        Me.Label211.Text = "label1"
        '
        'Label212
        '
        Me.Label212.BackColor = System.Drawing.Color.Transparent
        Me.Label212.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label212.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label212.ForeColor = System.Drawing.Color.White
        Me.Label212.Image = CType(resources.GetObject("Label212.Image"), System.Drawing.Image)
        Me.Label212.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label212.Location = New System.Drawing.Point(0, 0)
        Me.Label212.Name = "Label212"
        Me.Label212.Size = New System.Drawing.Size(1170, 23)
        Me.Label212.TabIndex = 9
        Me.Label212.Text = "      Selected CPT"
        Me.Label212.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label213
        '
        Me.Label213.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label213.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label213.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label213.Location = New System.Drawing.Point(0, 23)
        Me.Label213.Name = "Label213"
        Me.Label213.Size = New System.Drawing.Size(1170, 1)
        Me.Label213.TabIndex = 13
        Me.Label213.Text = "label2"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 446)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1170, 3)
        Me.Splitter1.TabIndex = 49
        Me.Splitter1.TabStop = False
        '
        'GloUC_trvCPT
        '
        Me.GloUC_trvCPT.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvCPT.CheckBoxes = False
        Me.GloUC_trvCPT.CodeMember = Nothing
        Me.GloUC_trvCPT.ColonAsSeparator = False
        Me.GloUC_trvCPT.Comment = Nothing
        Me.GloUC_trvCPT.ConceptID = Nothing
        Me.GloUC_trvCPT.CPT = Nothing
        Me.GloUC_trvCPT.DDIDMember = Nothing
        Me.GloUC_trvCPT.DescriptionMember = Nothing
        Me.GloUC_trvCPT.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvCPT.DrugFlag = CType(16, Short)
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
        Me.GloUC_trvCPT.IsCPTSearch = False
        Me.GloUC_trvCPT.IsDiagnosisSearch = False
        Me.GloUC_trvCPT.IsDrug = False
        Me.GloUC_trvCPT.IsNarcoticsMember = Nothing
        Me.GloUC_trvCPT.IsSearchForEducationMapping = False
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
        Me.GloUC_trvCPT.SearchBox = True
        Me.GloUC_trvCPT.SearchText = Nothing
        Me.GloUC_trvCPT.SelectedImageIndex = 0
        Me.GloUC_trvCPT.SelectedNode = Nothing
        Me.GloUC_trvCPT.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvCPT.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvCPT.SelectedParentImageIndex = 0
        Me.GloUC_trvCPT.Size = New System.Drawing.Size(1170, 392)
        Me.GloUC_trvCPT.SmartTreatmentId = Nothing
        Me.GloUC_trvCPT.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvCPT.TabIndex = 46
        Me.GloUC_trvCPT.Tag = Nothing
        Me.GloUC_trvCPT.UnitMember = Nothing
        Me.GloUC_trvCPT.ValueMember = Nothing
        '
        'pnlInternalToolStripCPT
        '
        Me.pnlInternalToolStripCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripCPT.Controls.Add(Me.Panel30)
        Me.pnlInternalToolStripCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripCPT.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripCPT.Name = "pnlInternalToolStripCPT"
        Me.pnlInternalToolStripCPT.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripCPT.Size = New System.Drawing.Size(1170, 54)
        Me.pnlInternalToolStripCPT.TabIndex = 53
        '
        'Panel30
        '
        Me.Panel30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel30.Controls.Add(Me.ToolStrip2)
        Me.Panel30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel30.Location = New System.Drawing.Point(0, 0)
        Me.Panel30.Name = "Panel30"
        Me.Panel30.Size = New System.Drawing.Size(1170, 54)
        Me.Panel30.TabIndex = 4
        '
        'ToolStrip2
        '
        Me.ToolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveCPT, Me.tsBtn_CancelCPT})
        Me.ToolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1170, 21)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'tsBtn_SaveCPT
        '
        Me.tsBtn_SaveCPT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveCPT.Name = "tsBtn_SaveCPT"
        Me.tsBtn_SaveCPT.Size = New System.Drawing.Size(43, 18)
        Me.tsBtn_SaveCPT.Tag = "Save"
        Me.tsBtn_SaveCPT.Text = "&Done"
        Me.tsBtn_SaveCPT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveCPT.ToolTipText = "Done"
        '
        'tsBtn_CancelCPT
        '
        Me.tsBtn_CancelCPT.Image = CType(resources.GetObject("tsBtn_CancelCPT.Image"), System.Drawing.Image)
        Me.tsBtn_CancelCPT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelCPT.Name = "tsBtn_CancelCPT"
        Me.tsBtn_CancelCPT.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelCPT.Tag = "Cancel"
        Me.tsBtn_CancelCPT.Text = "&Cancel"
        Me.tsBtn_CancelCPT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelCPT.Visible = False
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
        Me.pnlDrugs.Size = New System.Drawing.Size(1170, 835)
        Me.pnlDrugs.TabIndex = 1
        Me.pnlDrugs.Visible = False
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
        Me.pnltrvSelectedDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvSelectedDrugs.Location = New System.Drawing.Point(0, 476)
        Me.pnltrvSelectedDrugs.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedDrugs.Name = "pnltrvSelectedDrugs"
        Me.pnltrvSelectedDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltrvSelectedDrugs.Size = New System.Drawing.Size(1170, 359)
        Me.pnltrvSelectedDrugs.TabIndex = 21
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label86.Location = New System.Drawing.Point(1, 355)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(1168, 1)
        Me.Label86.TabIndex = 8
        Me.Label86.Text = "label2"
        '
        'Label180
        '
        Me.Label180.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label180.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label180.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label180.Location = New System.Drawing.Point(0, 1)
        Me.Label180.Name = "Label180"
        Me.Label180.Size = New System.Drawing.Size(1, 355)
        Me.Label180.TabIndex = 7
        Me.Label180.Text = "label4"
        '
        'trvSelectedDrugs
        '
        Me.trvSelectedDrugs.BackColor = System.Drawing.Color.White
        Me.trvSelectedDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvSelectedDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvSelectedDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvSelectedDrugs.ForeColor = System.Drawing.Color.Black
        Me.trvSelectedDrugs.HideSelection = False
        Me.trvSelectedDrugs.ImageIndex = 0
        Me.trvSelectedDrugs.ImageList = Me.ImageList1
        Me.trvSelectedDrugs.ItemHeight = 18
        Me.trvSelectedDrugs.Location = New System.Drawing.Point(0, 1)
        Me.trvSelectedDrugs.Name = "trvSelectedDrugs"
        Me.trvSelectedDrugs.SelectedImageIndex = 0
        Me.trvSelectedDrugs.ShowLines = False
        Me.trvSelectedDrugs.Size = New System.Drawing.Size(1169, 355)
        Me.trvSelectedDrugs.TabIndex = 0
        '
        'Label181
        '
        Me.Label181.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label181.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label181.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label181.Location = New System.Drawing.Point(1169, 1)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(1, 355)
        Me.Label181.TabIndex = 6
        Me.Label181.Text = "label3"
        '
        'Label182
        '
        Me.Label182.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label182.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label182.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label182.Location = New System.Drawing.Point(0, 0)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(1170, 1)
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
        Me.pnlSelectedDrugLabel.Size = New System.Drawing.Size(1170, 27)
        Me.pnlSelectedDrugLabel.TabIndex = 20
        '
        'Panel4
        '
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label185)
        Me.Panel4.Controls.Add(Me.Label184)
        Me.Panel4.Controls.Add(Me.Label186)
        Me.Panel4.Controls.Add(Me.Label187)
        Me.Panel4.Controls.Add(Me.Label183)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1170, 24)
        Me.Panel4.TabIndex = 14
        '
        'Label185
        '
        Me.Label185.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label185.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label185.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label185.Location = New System.Drawing.Point(1169, 1)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(1, 22)
        Me.Label185.TabIndex = 11
        Me.Label185.Text = "label3"
        '
        'Label184
        '
        Me.Label184.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label184.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label184.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label184.Location = New System.Drawing.Point(0, 1)
        Me.Label184.Name = "Label184"
        Me.Label184.Size = New System.Drawing.Size(1, 22)
        Me.Label184.TabIndex = 12
        Me.Label184.Text = "label4"
        '
        'Label186
        '
        Me.Label186.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label186.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label186.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label186.Location = New System.Drawing.Point(0, 0)
        Me.Label186.Name = "Label186"
        Me.Label186.Size = New System.Drawing.Size(1170, 1)
        Me.Label186.TabIndex = 10
        Me.Label186.Text = "label1"
        '
        'Label187
        '
        Me.Label187.BackColor = System.Drawing.Color.Transparent
        Me.Label187.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label187.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label187.ForeColor = System.Drawing.Color.White
        Me.Label187.Image = CType(resources.GetObject("Label187.Image"), System.Drawing.Image)
        Me.Label187.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label187.Location = New System.Drawing.Point(0, 0)
        Me.Label187.Name = "Label187"
        Me.Label187.Size = New System.Drawing.Size(1170, 23)
        Me.Label187.TabIndex = 9
        Me.Label187.Text = "      Selected Drugs"
        Me.Label187.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label183
        '
        Me.Label183.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label183.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label183.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label183.Location = New System.Drawing.Point(0, 23)
        Me.Label183.Name = "Label183"
        Me.Label183.Size = New System.Drawing.Size(1170, 1)
        Me.Label183.TabIndex = 13
        Me.Label183.Text = "label2"
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Location = New System.Drawing.Point(0, 446)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(1170, 3)
        Me.Splitter3.TabIndex = 22
        Me.Splitter3.TabStop = False
        '
        'GloUC_trvDrugs
        '
        Me.GloUC_trvDrugs.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvDrugs.CheckBoxes = False
        Me.GloUC_trvDrugs.CodeMember = Nothing
        Me.GloUC_trvDrugs.ColonAsSeparator = False
        Me.GloUC_trvDrugs.Comment = Nothing
        Me.GloUC_trvDrugs.ConceptID = Nothing
        Me.GloUC_trvDrugs.CPT = Nothing
        Me.GloUC_trvDrugs.DDIDMember = Nothing
        Me.GloUC_trvDrugs.DescriptionMember = Nothing
        Me.GloUC_trvDrugs.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvDrugs.Dock = System.Windows.Forms.DockStyle.Top
        Me.GloUC_trvDrugs.DrugFlag = CType(16, Short)
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
        Me.GloUC_trvDrugs.IsCPTSearch = False
        Me.GloUC_trvDrugs.IsDiagnosisSearch = False
        Me.GloUC_trvDrugs.IsDrug = False
        Me.GloUC_trvDrugs.IsNarcoticsMember = Nothing
        Me.GloUC_trvDrugs.IsSearchForEducationMapping = False
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
        Me.GloUC_trvDrugs.SearchBox = True
        Me.GloUC_trvDrugs.SearchText = Nothing
        Me.GloUC_trvDrugs.SelectedImageIndex = 0
        Me.GloUC_trvDrugs.SelectedNode = Nothing
        Me.GloUC_trvDrugs.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvDrugs.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvDrugs.SelectedParentImageIndex = 0
        Me.GloUC_trvDrugs.Size = New System.Drawing.Size(1170, 392)
        Me.GloUC_trvDrugs.SmartTreatmentId = Nothing
        Me.GloUC_trvDrugs.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvDrugs.TabIndex = 9
        Me.GloUC_trvDrugs.Tag = Nothing
        Me.GloUC_trvDrugs.UnitMember = Nothing
        Me.GloUC_trvDrugs.ValueMember = Nothing
        '
        'pnlInternalToolStripDrugs
        '
        Me.pnlInternalToolStripDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlInternalToolStripDrugs.Controls.Add(Me.Panel32)
        Me.pnlInternalToolStripDrugs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInternalToolStripDrugs.Location = New System.Drawing.Point(0, 0)
        Me.pnlInternalToolStripDrugs.Name = "pnlInternalToolStripDrugs"
        Me.pnlInternalToolStripDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlInternalToolStripDrugs.Size = New System.Drawing.Size(1170, 54)
        Me.pnlInternalToolStripDrugs.TabIndex = 54
        '
        'Panel32
        '
        Me.Panel32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel32.Controls.Add(Me.ToolStrip3)
        Me.Panel32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel32.Location = New System.Drawing.Point(0, 0)
        Me.Panel32.Name = "Panel32"
        Me.Panel32.Size = New System.Drawing.Size(1170, 54)
        Me.Panel32.TabIndex = 4
        '
        'ToolStrip3
        '
        Me.ToolStrip3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveDrugs, Me.tsBtn_CancelDrugs})
        Me.ToolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(1170, 21)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'tsBtn_SaveDrugs
        '
        Me.tsBtn_SaveDrugs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveDrugs.Name = "tsBtn_SaveDrugs"
        Me.tsBtn_SaveDrugs.Size = New System.Drawing.Size(43, 18)
        Me.tsBtn_SaveDrugs.Tag = "Save"
        Me.tsBtn_SaveDrugs.Text = "&Done"
        Me.tsBtn_SaveDrugs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveDrugs.ToolTipText = "Done"
        '
        'tsBtn_CancelDrugs
        '
        Me.tsBtn_CancelDrugs.Image = CType(resources.GetObject("tsBtn_CancelDrugs.Image"), System.Drawing.Image)
        Me.tsBtn_CancelDrugs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelDrugs.Name = "tsBtn_CancelDrugs"
        Me.tsBtn_CancelDrugs.Size = New System.Drawing.Size(50, 50)
        Me.tsBtn_CancelDrugs.Tag = "Cancel"
        Me.tsBtn_CancelDrugs.Text = "&Cancel"
        Me.tsBtn_CancelDrugs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelDrugs.Visible = False
        '
        'pnlLab
        '
        Me.pnlLab.Controls.Add(Me.Panel12)
        Me.pnlLab.Controls.Add(Me.Panel13)
        Me.pnlLab.Controls.Add(Me.pnlInternalToolStripLab)
        Me.pnlLab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLab.Location = New System.Drawing.Point(0, 0)
        Me.pnlLab.Name = "pnlLab"
        Me.pnlLab.Size = New System.Drawing.Size(1170, 835)
        Me.pnlLab.TabIndex = 2
        Me.pnlLab.Visible = False
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
        Me.Panel12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel12.Location = New System.Drawing.Point(0, 80)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel12.Size = New System.Drawing.Size(1170, 755)
        Me.Panel12.TabIndex = 20
        '
        'Label146
        '
        Me.Label146.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label146.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label146.Location = New System.Drawing.Point(1, 751)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(1168, 1)
        Me.Label146.TabIndex = 8
        Me.Label146.Text = "label2"
        '
        'C1LabResult
        '
        Me.C1LabResult.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1LabResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1LabResult.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1LabResult.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1LabResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1LabResult.ExtendLastCol = True
        Me.C1LabResult.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1LabResult.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1LabResult.Location = New System.Drawing.Point(1, 1)
        Me.C1LabResult.Name = "C1LabResult"
        Me.C1LabResult.Rows.DefaultSize = 19
        Me.C1LabResult.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1LabResult.ShowCellLabels = True
        Me.C1LabResult.Size = New System.Drawing.Size(1168, 751)
        Me.C1LabResult.StyleInfo = resources.GetString("C1LabResult.StyleInfo")
        Me.C1LabResult.TabIndex = 1
        Me.C1LabResult.Tree.NodeImageCollapsed = CType(resources.GetObject("C1LabResult.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1LabResult.Tree.NodeImageExpanded = CType(resources.GetObject("C1LabResult.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label147
        '
        Me.Label147.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label147.Location = New System.Drawing.Point(0, 1)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(1, 751)
        Me.Label147.TabIndex = 7
        Me.Label147.Text = "label4"
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label148.Location = New System.Drawing.Point(1169, 1)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(1, 751)
        Me.Label148.TabIndex = 6
        Me.Label148.Text = "label3"
        '
        'Label149
        '
        Me.Label149.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label149.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label149.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label149.Location = New System.Drawing.Point(0, 0)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(1170, 1)
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
        Me.Panel13.Size = New System.Drawing.Size(1170, 26)
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
        Me.Panel23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel23.ForeColor = System.Drawing.Color.Black
        Me.Panel23.Location = New System.Drawing.Point(0, 0)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(1170, 23)
        Me.Panel23.TabIndex = 21
        '
        'txtLabResultSearch
        '
        Me.txtLabResultSearch.BackColor = System.Drawing.Color.White
        Me.txtLabResultSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabResultSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLabResultSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabResultSearch.ForeColor = System.Drawing.Color.Black
        Me.txtLabResultSearch.Location = New System.Drawing.Point(29, 5)
        Me.txtLabResultSearch.Name = "txtLabResultSearch"
        Me.txtLabResultSearch.Size = New System.Drawing.Size(1119, 15)
        Me.txtLabResultSearch.TabIndex = 0
        '
        'btnLabResultClear
        '
        Me.btnLabResultClear.BackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear.BackgroundImage = CType(resources.GetObject("btnLabResultClear.BackgroundImage"), System.Drawing.Image)
        Me.btnLabResultClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabResultClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLabResultClear.FlatAppearance.BorderSize = 0
        Me.btnLabResultClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabResultClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabResultClear.Image = CType(resources.GetObject("btnLabResultClear.Image"), System.Drawing.Image)
        Me.btnLabResultClear.Location = New System.Drawing.Point(1148, 5)
        Me.btnLabResultClear.Name = "btnLabResultClear"
        Me.btnLabResultClear.Size = New System.Drawing.Size(21, 15)
        Me.btnLabResultClear.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.btnLabResultClear, "Clear Search")
        Me.btnLabResultClear.UseVisualStyleBackColor = False
        '
        'Label126
        '
        Me.Label126.BackColor = System.Drawing.Color.White
        Me.Label126.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label126.Location = New System.Drawing.Point(29, 1)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(1140, 4)
        Me.Label126.TabIndex = 37
        '
        'Label127
        '
        Me.Label127.BackColor = System.Drawing.Color.White
        Me.Label127.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label127.Location = New System.Drawing.Point(29, 20)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(1140, 2)
        Me.Label127.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label136
        '
        Me.Label136.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label136.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label136.Location = New System.Drawing.Point(1, 22)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(1168, 1)
        Me.Label136.TabIndex = 35
        Me.Label136.Text = "label1"
        '
        'Label137
        '
        Me.Label137.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label137.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label137.Location = New System.Drawing.Point(1, 0)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(1168, 1)
        Me.Label137.TabIndex = 36
        Me.Label137.Text = "label1"
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label138.Location = New System.Drawing.Point(0, 0)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(1, 23)
        Me.Label138.TabIndex = 39
        Me.Label138.Text = "label4"
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label139.Location = New System.Drawing.Point(1169, 0)
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
        Me.pnlInternalToolStripLab.Size = New System.Drawing.Size(1170, 54)
        Me.pnlInternalToolStripLab.TabIndex = 55
        '
        'Panel40
        '
        Me.Panel40.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel40.Controls.Add(Me.ToolStrip5)
        Me.Panel40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel40.Location = New System.Drawing.Point(0, 0)
        Me.Panel40.Name = "Panel40"
        Me.Panel40.Size = New System.Drawing.Size(1170, 54)
        Me.Panel40.TabIndex = 4
        '
        'ToolStrip5
        '
        Me.ToolStrip5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip5.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip5.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsBtn_SaveLab, Me.tsBtn_CancelLab})
        Me.ToolStrip5.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip5.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip5.Name = "ToolStrip5"
        Me.ToolStrip5.Size = New System.Drawing.Size(1170, 21)
        Me.ToolStrip5.TabIndex = 0
        Me.ToolStrip5.Text = "ToolStrip5"
        '
        'tsBtn_SaveLab
        '
        Me.tsBtn_SaveLab.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_SaveLab.Name = "tsBtn_SaveLab"
        Me.tsBtn_SaveLab.Size = New System.Drawing.Size(43, 18)
        Me.tsBtn_SaveLab.Tag = "Save"
        Me.tsBtn_SaveLab.Text = "&Done"
        Me.tsBtn_SaveLab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_SaveLab.ToolTipText = "Done"
        '
        'tsBtn_CancelLab
        '
        Me.tsBtn_CancelLab.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsBtn_CancelLab.Name = "tsBtn_CancelLab"
        Me.tsBtn_CancelLab.Size = New System.Drawing.Size(50, 18)
        Me.tsBtn_CancelLab.Tag = "Cancel"
        Me.tsBtn_CancelLab.Text = "&Cancel"
        Me.tsBtn_CancelLab.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsBtn_CancelLab.Visible = False
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
        Me.cmbMaritalSt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMaritalSt.ForeColor = System.Drawing.Color.Black
        Me.cmbMaritalSt.Location = New System.Drawing.Point(665, 12)
        Me.cmbMaritalSt.Name = "cmbMaritalSt"
        Me.cmbMaritalSt.Size = New System.Drawing.Size(42, 22)
        Me.cmbMaritalSt.TabIndex = 7
        Me.cmbMaritalSt.Visible = False
        '
        'cmbRace
        '
        Me.cmbRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRace.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRace.ForeColor = System.Drawing.Color.Black
        Me.cmbRace.Location = New System.Drawing.Point(761, 12)
        Me.cmbRace.Name = "cmbRace"
        Me.cmbRace.Size = New System.Drawing.Size(42, 22)
        Me.cmbRace.TabIndex = 5
        Me.cmbRace.Visible = False
        '
        'cmbGender
        '
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGender.ForeColor = System.Drawing.Color.Black
        Me.cmbGender.Location = New System.Drawing.Point(713, 12)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(42, 22)
        Me.cmbGender.TabIndex = 3
        Me.cmbGender.Visible = False
        '
        'pnlMsgTOP
        '
        Me.pnlMsgTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsgTOP.Controls.Add(Me.pnlMsg)
        Me.pnlMsgTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMsgTOP.Location = New System.Drawing.Point(0, 54)
        Me.pnlMsgTOP.Name = "pnlMsgTOP"
        Me.pnlMsgTOP.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMsgTOP.Size = New System.Drawing.Size(1184, 40)
        Me.pnlMsgTOP.TabIndex = 0
        '
        'pnlMsg
        '
        Me.pnlMsg.BackColor = System.Drawing.Color.Transparent
        Me.pnlMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMsg.Controls.Add(Me.lnkhelp)
        Me.pnlMsg.Controls.Add(Me.cmb_measure)
        Me.pnlMsg.Controls.Add(Me.Label24)
        Me.pnlMsg.Controls.Add(Me.Label141)
        Me.pnlMsg.Controls.Add(Me.Label4)
        Me.pnlMsg.Controls.Add(Me.Label23)
        Me.pnlMsg.Controls.Add(Me.Label63)
        Me.pnlMsg.Controls.Add(Me.Label25)
        Me.pnlMsg.Controls.Add(Me.Label117)
        Me.pnlMsg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMsg.Location = New System.Drawing.Point(3, 3)
        Me.pnlMsg.Name = "pnlMsg"
        Me.pnlMsg.Size = New System.Drawing.Size(1178, 34)
        Me.pnlMsg.TabIndex = 0
        '
        'lnkhelp
        '
        Me.lnkhelp.AutoSize = True
        Me.lnkhelp.Location = New System.Drawing.Point(659, 10)
        Me.lnkhelp.Name = "lnkhelp"
        Me.lnkhelp.Size = New System.Drawing.Size(106, 14)
        Me.lnkhelp.TabIndex = 120
        Me.lnkhelp.TabStop = True
        Me.lnkhelp.Text = "Click here for Help"
        '
        'cmb_measure
        '
        Me.cmb_measure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_measure.Enabled = False
        Me.cmb_measure.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_measure.FormattingEnabled = True
        Me.cmb_measure.Location = New System.Drawing.Point(118, 6)
        Me.cmb_measure.Name = "cmb_measure"
        Me.cmb_measure.Size = New System.Drawing.Size(482, 22)
        Me.cmb_measure.TabIndex = 117
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 32)
        Me.Label24.TabIndex = 115
        Me.Label24.Text = "label3"
        '
        'Label141
        '
        Me.Label141.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label141.AutoSize = True
        Me.Label141.ForeColor = System.Drawing.Color.Red
        Me.Label141.Location = New System.Drawing.Point(-31, 41)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(14, 14)
        Me.Label141.TabIndex = 24
        Me.Label141.Text = "*"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(18, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 14)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Select Measure :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(0, 33)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1177, 1)
        Me.Label23.TabIndex = 17
        Me.Label23.Text = "label2"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label63.Location = New System.Drawing.Point(1177, 1)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(1, 33)
        Me.Label63.TabIndex = 15
        Me.Label63.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1178, 1)
        Me.Label25.TabIndex = 21
        Me.Label25.Text = "label1"
        '
        'Label117
        '
        Me.Label117.AutoSize = True
        Me.Label117.ForeColor = System.Drawing.Color.Red
        Me.Label117.Location = New System.Drawing.Point(6, 11)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(14, 14)
        Me.Label117.TabIndex = 1
        Me.Label117.Text = "*"
        '
        'pnl_tlstrip
        '
        Me.pnl_tlstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlstrip.Controls.Add(Me.tlsDM)
        Me.pnl_tlstrip.Controls.Add(Me.cmbMaritalSt)
        Me.pnl_tlstrip.Controls.Add(Me.cmbRace)
        Me.pnl_tlstrip.Controls.Add(Me.cmbGender)
        Me.pnl_tlstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlstrip.Name = "pnl_tlstrip"
        Me.pnl_tlstrip.Size = New System.Drawing.Size(1184, 54)
        Me.pnl_tlstrip.TabIndex = 3
        '
        'tlsDM
        '
        Me.tlsDM.BackColor = System.Drawing.Color.Transparent
        Me.tlsDM.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Toolstrip
        Me.tlsDM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDM.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsDM_Save, Me.tlsDM_Close})
        Me.tlsDM.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDM.Location = New System.Drawing.Point(0, 0)
        Me.tlsDM.Name = "tlsDM"
        Me.tlsDM.Size = New System.Drawing.Size(1184, 53)
        Me.tlsDM.TabIndex = 0
        Me.tlsDM.Text = "ToolStrip1"
        '
        'tlsDM_Save
        '
        Me.tlsDM_Save.Image = CType(resources.GetObject("tlsDM_Save.Image"), System.Drawing.Image)
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
        Me.tlsDM_Close.Image = CType(resources.GetObject("tlsDM_Close.Image"), System.Drawing.Image)
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
        Me.mnuDeleteInsurance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuDeleteInsurance.Image = CType(resources.GetObject("mnuDeleteInsurance.Image"), System.Drawing.Image)
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
        Me.mnuDeleteDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuDeleteDrugs.Image = CType(resources.GetObject("mnuDeleteDrugs.Image"), System.Drawing.Image)
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
        Me.mnuDeleteHistory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuDeleteHistory.Image = CType(resources.GetObject("mnuDeleteHistory.Image"), System.Drawing.Image)
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
        Me.mnuItem_DeleteCPT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuItem_DeleteCPT.Image = CType(resources.GetObject("mnuItem_DeleteCPT.Image"), System.Drawing.Image)
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
        Me.mnuItem_DeleteICD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuItem_DeleteICD.Image = CType(resources.GetObject("mnuItem_DeleteICD.Image"), System.Drawing.Image)
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
        'lstVw_COL_ICDCode
        '
        Me.lstVw_COL_ICDCode.Text = "ICD9"
        Me.lstVw_COL_ICDCode.Width = 277
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmCQM_RulesSetup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1184, 962)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlMsgTOP)
        Me.Controls.Add(Me.pnl_tlstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCQM_RulesSetup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CQM Setup"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.tbCntrl_RuleSetup.ResumeLayout(False)
        Me.tbPg_Triggers.ResumeLayout(False)
        Me.pnlMiddle.ResumeLayout(False)
        Me.pnlDemoVitals.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel25.ResumeLayout(False)
        Me.pnlVitals.ResumeLayout(False)
        Me.pnlVitals.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnlDemographics.ResumeLayout(False)
        Me.pnlDemographics.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.PnlProblemList.ResumeLayout(False)
        Me.PnlProblemMiddle.ResumeLayout(False)
        Me.Pnlsnomedprb.ResumeLayout(False)
        Me.pnltrvSnowmedOff.ResumeLayout(False)
        Me.Panel24.ResumeLayout(False)
        Me.pnltrvfinprob.ResumeLayout(False)
        Me.Panel31.ResumeLayout(False)
        Me.PnlSrchProb.ResumeLayout(False)
        Me.PnlSrchProb.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnltrvsubprb.ResumeLayout(False)
        Me.Panel34.ResumeLayout(False)
        Me.PnlProblemSearch.ResumeLayout(False)
        Me.Panel26.ResumeLayout(False)
        Me.PnlProbLeft.ResumeLayout(False)
        Me.Panel35.ResumeLayout(False)
        Me.Panel36.ResumeLayout(False)
        Me.Panel37.ResumeLayout(False)
        Me.Panel38.ResumeLayout(False)
        Me.Panel39.ResumeLayout(False)
        Me.Panel39.PerformLayout()
        Me.pnlRadiology.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        CType(Me.c1Labs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInternalToolStripRadiology.ResumeLayout(False)
        Me.Panel41.ResumeLayout(False)
        Me.Panel41.PerformLayout()
        Me.ToolStrip6.ResumeLayout(False)
        Me.ToolStrip6.PerformLayout()
        Me.pnlHistory.ResumeLayout(False)
        Me.Panel22.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel21.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.pnlHistoryLeft.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.pnlInternalToolStripHistory.ResumeLayout(False)
        Me.Panel33.ResumeLayout(False)
        Me.Panel33.PerformLayout()
        Me.ToolStrip4.ResumeLayout(False)
        Me.ToolStrip4.PerformLayout()
        Me.pnlInsurance.ResumeLayout(False)
        Me.pnltrvSelectedInsurance.ResumeLayout(False)
        Me.pnlSelectedInsuranceLabel.ResumeLayout(False)
        Me.Panel56.ResumeLayout(False)
        Me.pnlInternalToolStripInsurance.ResumeLayout(False)
        Me.Panel58.ResumeLayout(False)
        Me.Panel58.PerformLayout()
        Me.ToolStrip13.ResumeLayout(False)
        Me.ToolStrip13.PerformLayout()
        Me.pnlICD9.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.pnlInternalToolStripICD.ResumeLayout(False)
        Me.Panel29.ResumeLayout(False)
        Me.Panel29.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlCPT.ResumeLayout(False)
        Me.pnlSelectedCPTs.ResumeLayout(False)
        Me.pnlSelecteCPTsLabels.ResumeLayout(False)
        Me.Panel28.ResumeLayout(False)
        Me.pnlInternalToolStripCPT.ResumeLayout(False)
        Me.Panel30.ResumeLayout(False)
        Me.Panel30.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.pnlDrugs.ResumeLayout(False)
        Me.pnltrvSelectedDrugs.ResumeLayout(False)
        Me.pnlSelectedDrugLabel.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlInternalToolStripDrugs.ResumeLayout(False)
        Me.Panel32.ResumeLayout(False)
        Me.Panel32.PerformLayout()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.pnlLab.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        CType(Me.C1LabResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel13.ResumeLayout(False)
        Me.Panel23.ResumeLayout(False)
        Me.Panel23.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInternalToolStripLab.ResumeLayout(False)
        Me.Panel40.ResumeLayout(False)
        Me.Panel40.PerformLayout()
        Me.ToolStrip5.ResumeLayout(False)
        Me.ToolStrip5.PerformLayout()
        Me.pnlMsgTOP.ResumeLayout(False)
        Me.pnlMsg.ResumeLayout(False)
        Me.pnlMsg.PerformLayout()
        Me.pnl_tlstrip.ResumeLayout(False)
        Me.pnl_tlstrip.PerformLayout()
        Me.tlsDM.ResumeLayout(False)
        Me.tlsDM.PerformLayout()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuHistory.ResumeLayout(False)
        Me.CmnuStripCPT.ResumeLayout(False)
        Me.CmnustripICD.ResumeLayout(False)
        Me.cntFindings.ResumeLayout(False)
        Me.ContextMenuProblem.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#Region " Windows Controls "
    Friend WithEvents cntFindingsprb As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnLabs As System.Windows.Forms.Button
    Friend WithEvents C1LabResult As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnl_tlstrip As System.Windows.Forms.Panel
    Friend WithEvents tlsDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsDM_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsDM_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMsg As System.Windows.Forms.Panel
    Friend WithEvents pnlDemoVitals As System.Windows.Forms.Panel
    Friend WithEvents pnlLab As System.Windows.Forms.Panel
    Friend WithEvents CntConditions As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReferral As System.Windows.Forms.MenuItem
    Friend WithEvents EditReferral As System.Windows.Forms.MenuItem
    Friend WithEvents btnHistorySearch As System.Windows.Forms.Button
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents pnlMsgTOP As System.Windows.Forms.Panel
    Private WithEvents Label25 As System.Windows.Forms.Label
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
    Friend WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents Label124 As System.Windows.Forms.Label
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents cmbRace As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGender As System.Windows.Forms.ComboBox
    Friend WithEvents lblAgeMax As System.Windows.Forms.Label
    Friend WithEvents Label156 As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Private WithEvents Label202 As System.Windows.Forms.Label
    Private WithEvents Label204 As System.Windows.Forms.Label
    Private WithEvents Label214 As System.Windows.Forms.Label
    Private WithEvents Label236 As System.Windows.Forms.Label
    Friend WithEvents Label241 As System.Windows.Forms.Label
    Friend WithEvents pnlInternalToolStripICD As System.Windows.Forms.Panel
    Friend WithEvents Panel29 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsBtn_SaveICD As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsBtn_CancelICD As System.Windows.Forms.ToolStripButton
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
    Friend WithEvents lstVw_ICD9 As System.Windows.Forms.ListView
    Friend WithEvents lstVw_CPT As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbCntrl_RuleSetup As System.Windows.Forms.TabControl
    Friend WithEvents tbPg_Triggers As System.Windows.Forms.TabPage
    Friend WithEvents lstVw_Drugs As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstVw_Lab As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClearDrugs As System.Windows.Forms.Button
    Friend WithEvents btnClearLab As System.Windows.Forms.Button
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Panel84 As System.Windows.Forms.Panel
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
    Friend WithEvents Label7 As System.Windows.Forms.Label
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
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents btnCPT As System.Windows.Forms.Button
    Friend WithEvents btnICD9 As System.Windows.Forms.Button
    Friend WithEvents btnDrugs As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents cmbAgeMin As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAgeMax As System.Windows.Forms.ComboBox
    Friend WithEvents lblAgeMin As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents c1Labs As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btnClearSnomed As System.Windows.Forms.Button
    Friend WithEvents btnSnomed As System.Windows.Forms.Button
    Friend WithEvents lstVw_SnoMed As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnRemoveSelectedSnomedCode As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedDrug As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedICD9 As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedLab As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedCPT As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSelectedICD10 As System.Windows.Forms.Button
    Friend WithEvents btnClearICD10 As System.Windows.Forms.Button
    Friend WithEvents lstVw_ICD10 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnICD10 As System.Windows.Forms.Button
    Friend WithEvents tsBtn_SaveICD10 As System.Windows.Forms.ToolStripButton
    Friend WithEvents GloUC_trvICD10 As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents trvselecteICD10s As System.Windows.Forms.TreeView
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
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cmb_measure As System.Windows.Forms.ComboBox
    Friend WithEvents pnlVitals As System.Windows.Forms.Panel
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents label52 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents label51 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label258 As System.Windows.Forms.Label
    Friend WithEvents Label259 As System.Windows.Forms.Label
    Friend WithEvents Label257 As System.Windows.Forms.Label
    Private WithEvents Label111 As System.Windows.Forms.Label
    Private WithEvents Label112 As System.Windows.Forms.Label
    Private WithEvents Label113 As System.Windows.Forms.Label
    Private WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents txtBMImax As System.Windows.Forms.TextBox
    Friend WithEvents lblBMImax As System.Windows.Forms.Label
    Friend WithEvents txtBMImin As System.Windows.Forms.TextBox
    Friend WithEvents lblBMImin As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtBPstandingMaxTo As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMinTo As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMax As System.Windows.Forms.TextBox
    Friend WithEvents txtBPstandingMin As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMaxTo As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMinTo As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMax As System.Windows.Forms.TextBox
    Friend WithEvents txtBPsettingMin As System.Windows.Forms.TextBox
    Friend WithEvents lblBPStandingMax As System.Windows.Forms.Label
    Friend WithEvents lblBPStandingMin As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblBPSittingMax As System.Windows.Forms.Label
    Friend WithEvents lblBPSittingMin As System.Windows.Forms.Label
    Friend WithEvents lstVw_COL_ICDCode As System.Windows.Forms.ColumnHeader
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents lnkhelp As System.Windows.Forms.LinkLabel
    Friend WithEvents btnRemoveSelectedCVX As System.Windows.Forms.Button
    Friend WithEvents btnClearCVX As System.Windows.Forms.Button
    Friend WithEvents lstVw_CVX As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btncvx As System.Windows.Forms.Button

End Class