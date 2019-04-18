namespace gloBilling
{
    partial class frmSetupEDIData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpReturnToWork, dtpLastDayWorked, dtpHospitalDischargeDate, dtpHospitalizationDate, dtpOnsetoSimilarSyptomsorillness, dtpInsuranceExpiryDate, dtpInsuranceEffectiveDate };
            System.Windows.Forms.Control[] cntControls = { dtpReturnToWork, dtpLastDayWorked, dtpHospitalDischargeDate, dtpHospitalizationDate, dtpOnsetoSimilarSyptomsorillness, dtpInsuranceExpiryDate, dtpInsuranceEffectiveDate };
 
            if (disposing && (components != null))
            {

                try
                {



                    if (cntdtControls != null)
                    {
                        if (cntdtControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                        }
                    }
                    if (cntControls != null)
                    {
                        if (cntControls.Length > 0)
                        {
                            gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                        }
                    }
                    
                    
                    
                    
                //    if (dtpReturnToWork != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpReturnToWork);

                //        }
                //        catch
                //        {
                //        }


                //        dtpReturnToWork.Dispose();
                //        dtpReturnToWork = null;
                //    }
                }
                catch
                {
                }

                //try
                //{
                //    if (dtpLastDayWorked != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpLastDayWorked);

                //        }
                //        catch
                //        {
                //        }


                //        dtpLastDayWorked.Dispose();
                //        dtpLastDayWorked = null;
                //    }
                //}
                //catch
                //{
                //}

                //try
                //{
                //    if (dtpHospitalDischargeDate != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpHospitalDischargeDate);

                //        }
                //        catch
                //        {
                //        }


                //        dtpHospitalDischargeDate.Dispose();
                //        dtpHospitalDischargeDate = null;
                //    }
                //}
                //catch
                //{
                //}

                //try
                //{
                //    if (dtpHospitalizationDate != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpHospitalizationDate);

                //        }
                //        catch
                //        {
                //        }


                //        dtpHospitalizationDate.Dispose();
                //        dtpHospitalizationDate = null;
                //    }
                //}
                //catch
                //{
                //}

                //try
                //{
                //    if (dtpOnsetoSimilarSyptomsorillness != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpOnsetoSimilarSyptomsorillness);

                //        }
                //        catch
                //        {
                //        }


                //        dtpOnsetoSimilarSyptomsorillness.Dispose();
                //        dtpOnsetoSimilarSyptomsorillness = null;
                //    }
                //}
                //catch
                //{
                //}

                //try
                //{
                //    if (dtpOnsetofCurrentSymptomsorillness != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpOnsetofCurrentSymptomsorillness);

                //        }
                //        catch
                //        {
                //        }


                //        dtpOnsetofCurrentSymptomsorillness.Dispose();
                //        dtpOnsetofCurrentSymptomsorillness = null;
                //    }
                //}
                //catch
                //{
                //}

                //try
                //{
                //    if (dtpInsuranceExpiryDate != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpInsuranceExpiryDate);

                //        }
                //        catch
                //        {
                //        }


                //        dtpInsuranceExpiryDate.Dispose();
                //        dtpInsuranceExpiryDate = null;
                //    }
                //}
                //catch
                //{
                //}

                //try
                //{
                //    if (dtpInsuranceEffectiveDate != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpInsuranceEffectiveDate);

                //        }
                //        catch
                //        {
                //        }


                //        dtpInsuranceEffectiveDate.Dispose();
                //        dtpInsuranceEffectiveDate = null;
                //    }
                //}
                //catch
                //{
                //}

                //try
                //{
                //    if (dtpSubscriberDOB != null)
                //    {
                //        try
                //        {
                //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpSubscriberDOB);

                //        }
                //        catch
                //        {
                //        }


                //        dtpSubscriberDOB.Dispose();
                //        dtpSubscriberDOB = null;
                //    }
                //}
                //catch
                //{
                //}

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupEDIData));
            this.ts_Commands = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_ShowHCFA1500 = new System.Windows.Forms.ToolStripButton();
            this.tlb_btnGenerateEDI = new System.Windows.Forms.ToolStripButton();
            this.tls_btnValidate = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsb_OK = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tb_EDIDataDetails = new System.Windows.Forms.TabControl();
            this.tbpg_EDIDetails = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbl_BottomBrd = new System.Windows.Forms.Label();
            this.lbl_LeftBrd = new System.Windows.Forms.Label();
            this.lbl_RightBrd = new System.Windows.Forms.Label();
            this.lbl_TopBrd = new System.Windows.Forms.Label();
            this.c1Transaction1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.grp_Receiver = new System.Windows.Forms.GroupBox();
            this.cmbRecIDCodeQual = new System.Windows.Forms.ComboBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.txtRecEntityTypeQualifier = new System.Windows.Forms.TextBox();
            this.label112 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.txtEntityIDQualRec = new System.Windows.Forms.TextBox();
            this.label114 = new System.Windows.Forms.Label();
            this.txtRecieverIdentificationCode = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtRecieverName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.grp_Submitter = new System.Windows.Forms.GroupBox();
            this.label110 = new System.Windows.Forms.Label();
            this.txtSubEntityTypeQualifier = new System.Windows.Forms.TextBox();
            this.lblEntityType = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.txtSubEntityIDQualifier = new System.Windows.Forms.TextBox();
            this.lblEntityIDQualifier = new System.Windows.Forms.Label();
            this.txtSubIdentificationCode = new System.Windows.Forms.TextBox();
            this.lblIdentifierCode = new System.Windows.Forms.Label();
            this.cmbSubmitterIdentifierCodeQualifier = new System.Windows.Forms.ComboBox();
            this.lblSubmitterIdCode = new System.Windows.Forms.Label();
            this.cmbContactFunctionCode = new System.Windows.Forms.ComboBox();
            this.txtSubmitterAdd2 = new System.Windows.Forms.TextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.txtSubmitterContactPersonPhone = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.txtSubmitterContactPersonPhoneExt = new System.Windows.Forms.TextBox();
            this.label104 = new System.Windows.Forms.Label();
            this.cmbClinic = new System.Windows.Forms.ComboBox();
            this.cmb_CommunicationQualifier = new System.Windows.Forms.ComboBox();
            this.lblSubmitterName = new System.Windows.Forms.Label();
            this.txtSubmitterPhone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSubmitterExt = new System.Windows.Forms.TextBox();
            this.lblEXT = new System.Windows.Forms.Label();
            this.txtSubmitterContactName = new System.Windows.Forms.TextBox();
            this.lblSubmitterContactName = new System.Windows.Forms.Label();
            this.txtSubmitterAddress = new System.Windows.Forms.TextBox();
            this.lblSubmitterAddress = new System.Windows.Forms.Label();
            this.txtSubmitterCity = new System.Windows.Forms.TextBox();
            this.lblSubmitterCity = new System.Windows.Forms.Label();
            this.txtSubmitterState = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.lblSubmitterState = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.txtSubmitterZip = new System.Windows.Forms.TextBox();
            this.lblSubmitterZIP = new System.Windows.Forms.Label();
            this.txtCommunicationNumber = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.txtSubmitterFax = new System.Windows.Forms.TextBox();
            this.lblSubmitterFAX = new System.Windows.Forms.Label();
            this.grp_BHT = new System.Windows.Forms.GroupBox();
            this.cmbBHT_TSTypeCode = new System.Windows.Forms.ComboBox();
            this.cmbBHT_TSPurposeCode = new System.Windows.Forms.ComboBox();
            this.txtBHTRefIdentification = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.txtBHT_HerarchicalStrCode = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.grp_TransactionSet = new System.Windows.Forms.GroupBox();
            this.txtTSControlNumber = new System.Windows.Forms.TextBox();
            this.lblTSControlNumber = new System.Windows.Forms.Label();
            this.txtTSIdCode = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.grp_FunctionalGroup = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtFunctionID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReceiverDept = new System.Windows.Forms.TextBox();
            this.lblSenderDept = new System.Windows.Forms.Label();
            this.lblFunctionID = new System.Windows.Forms.Label();
            this.txtSenderDept = new System.Windows.Forms.TextBox();
            this.grp_ISASegment = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSenderID = new System.Windows.Forms.TextBox();
            this.txtReferenceID = new System.Windows.Forms.TextBox();
            this.txtClaimNo = new System.Windows.Forms.TextBox();
            this.txtClaimDate = new System.Windows.Forms.TextBox();
            this.lblClaimNumber = new System.Windows.Forms.Label();
            this.lblSenderID = new System.Windows.Forms.Label();
            this.lblReferenceID = new System.Windows.Forms.Label();
            this.txtReceiverID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClaimTime = new System.Windows.Forms.TextBox();
            this.lblClaimTime = new System.Windows.Forms.Label();
            this.txtControlNo = new System.Windows.Forms.TextBox();
            this.lblControlNo = new System.Windows.Forms.Label();
            this.tbpg_BillingProvider = new System.Windows.Forms.TabPage();
            this.grp_REF = new System.Windows.Forms.GroupBox();
            this.txtREFIdentificationRefNo = new System.Windows.Forms.TextBox();
            this.label165 = new System.Windows.Forms.Label();
            this.cmbREF_ReferenceIdCode = new System.Windows.Forms.ComboBox();
            this.label166 = new System.Windows.Forms.Label();
            this.grp_PayToProvider = new System.Windows.Forms.GroupBox();
            this.txtBLIdentificationCode = new System.Windows.Forms.TextBox();
            this.label133 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.cmbBLCodeQualifier = new System.Windows.Forms.ComboBox();
            this.txtPTPIDCode = new System.Windows.Forms.TextBox();
            this.label134 = new System.Windows.Forms.Label();
            this.txtPTPUPIN = new System.Windows.Forms.TextBox();
            this.label139 = new System.Windows.Forms.Label();
            this.label126 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.cmbPTPIDCodeQualifier = new System.Windows.Forms.ComboBox();
            this.txtBLProvEntityType = new System.Windows.Forms.TextBox();
            this.label141 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.label142 = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.cmbPTPName = new System.Windows.Forms.ComboBox();
            this.txtBLProvEntitiyIDQual = new System.Windows.Forms.TextBox();
            this.label140 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.txtBLProviderUPIN = new System.Windows.Forms.TextBox();
            this.txtPTPType = new System.Windows.Forms.TextBox();
            this.cmbBillingProvider = new System.Windows.Forms.ComboBox();
            this.label136 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.txtBLProviderAddress = new System.Windows.Forms.TextBox();
            this.txtPTPIDQualifier = new System.Windows.Forms.TextBox();
            this.txtBLProviderState = new System.Windows.Forms.TextBox();
            this.label138 = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.txtPTPAddress = new System.Windows.Forms.TextBox();
            this.txtBLProviderCity = new System.Windows.Forms.TextBox();
            this.txtPTPState = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.txtBLProviderZip = new System.Windows.Forms.TextBox();
            this.txtPTPCity = new System.Windows.Forms.TextBox();
            this.label124 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.txtBLProvNPI_ID = new System.Windows.Forms.TextBox();
            this.txtPTPZip = new System.Windows.Forms.TextBox();
            this.label125 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.txtPTPNPI_ID = new System.Windows.Forms.TextBox();
            this.label129 = new System.Windows.Forms.Label();
            this.grp_BillingProvider = new System.Windows.Forms.GroupBox();
            this.grp_Address = new System.Windows.Forms.GroupBox();
            this.rdb_Company = new System.Windows.Forms.RadioButton();
            this.rdb_Practice = new System.Windows.Forms.RadioButton();
            this.rdb_Business = new System.Windows.Forms.RadioButton();
            this.tbpg_PatientDetails = new System.Windows.Forms.TabPage();
            this.pnlPatientInsurence = new System.Windows.Forms.Panel();
            this.chkUseSecondaryInsurance = new System.Windows.Forms.CheckBox();
            this.label183 = new System.Windows.Forms.Label();
            this.txtSubscriberMiddleName = new System.Windows.Forms.TextBox();
            this.label182 = new System.Windows.Forms.Label();
            this.txtSubscriberFirstName = new System.Windows.Forms.TextBox();
            this.label167 = new System.Windows.Forms.Label();
            this.dtpInsuranceExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.dtpInsuranceEffectiveDate = new System.Windows.Forms.DateTimePicker();
            this.dtpSubscriberDOB = new System.Windows.Forms.DateTimePicker();
            this.label175 = new System.Windows.Forms.Label();
            this.label174 = new System.Windows.Forms.Label();
            this.label173 = new System.Windows.Forms.Label();
            this.label172 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtPatientRelationship = new System.Windows.Forms.TextBox();
            this.chkInsurenceIsPrimary = new System.Windows.Forms.CheckBox();
            this.label93 = new System.Windows.Forms.Label();
            this.lblSubscriberName = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblSubscribePolicy = new System.Windows.Forms.Label();
            this.txtInsurenceGroup = new System.Windows.Forms.TextBox();
            this.txtInsurenceSubscriberLastName = new System.Windows.Forms.TextBox();
            this.txtInsurenceID = new System.Windows.Forms.TextBox();
            this.label97 = new System.Windows.Forms.Label();
            this.txtPayerID = new System.Windows.Forms.TextBox();
            this.txtPayerName = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label178 = new System.Windows.Forms.Label();
            this.label177 = new System.Windows.Forms.Label();
            this.label176 = new System.Windows.Forms.Label();
            this.trvPatientInsurences = new System.Windows.Forms.TreeView();
            this.label179 = new System.Windows.Forms.Label();
            this.pnlPatientInsuranceHeader = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.lblPatientInsurance = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label158 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label168 = new System.Windows.Forms.Label();
            this.label169 = new System.Windows.Forms.Label();
            this.grp_PatientDetails = new System.Windows.Forms.GroupBox();
            this.label164 = new System.Windows.Forms.Label();
            this.txtPatientGender = new System.Windows.Forms.TextBox();
            this.label109 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.txtPatientLName = new System.Windows.Forms.TextBox();
            this.txtPatientMName = new System.Windows.Forms.TextBox();
            this.txtPatientFName = new System.Windows.Forms.TextBox();
            this.label95 = new System.Windows.Forms.Label();
            this.txtPatientDOB = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.txtPatientAddress = new System.Windows.Forms.TextBox();
            this.label100 = new System.Windows.Forms.Label();
            this.txtPatientCity = new System.Windows.Forms.TextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.txtPatientState = new System.Windows.Forms.TextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.txtPatientZip = new System.Windows.Forms.TextBox();
            this.label105 = new System.Windows.Forms.Label();
            this.label170 = new System.Windows.Forms.Label();
            this.label171 = new System.Windows.Forms.Label();
            this.tbpg_TransactionDetails = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grp_Facility = new System.Windows.Forms.GroupBox();
            this.txtFacilityCode = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtFacilityName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFacilityAddress = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFacilityCity = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFacilityState = new System.Windows.Forms.TextBox();
            this.txtFacilityExt = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFacilityZip = new System.Windows.Forms.TextBox();
            this.txtFacilityPhone = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFacilityFax = new System.Windows.Forms.TextBox();
            this.grp_IllnessDetails = new System.Windows.Forms.GroupBox();
            this.dtpReturnToWork = new System.Windows.Forms.DateTimePicker();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpLastDayWorked = new System.Windows.Forms.DateTimePicker();
            this.label25 = new System.Windows.Forms.Label();
            this.dtpHospitalDischargeDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpHospitalizationDate = new System.Windows.Forms.DateTimePicker();
            this.label23 = new System.Windows.Forms.Label();
            this.dtpOnsetoSimilarSyptomsorillness = new System.Windows.Forms.DateTimePicker();
            this.label26 = new System.Windows.Forms.Label();
            this.dtpOnsetofCurrentSymptomsorillness = new System.Windows.Forms.DateTimePicker();
            this.label34 = new System.Windows.Forms.Label();
            this.grpRefferingProvider = new System.Windows.Forms.GroupBox();
            this.txtREF_RefProvIdCode = new System.Windows.Forms.TextBox();
            this.label181 = new System.Windows.Forms.Label();
            this.txtRefProvIDCode = new System.Windows.Forms.TextBox();
            this.cmbREF_RefPrReferenceCodeQualifier = new System.Windows.Forms.ComboBox();
            this.label144 = new System.Windows.Forms.Label();
            this.label180 = new System.Windows.Forms.Label();
            this.cmbRefProvCodeQualifier = new System.Windows.Forms.ComboBox();
            this.label145 = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.txtRefProvEntityType = new System.Windows.Forms.TextBox();
            this.label149 = new System.Windows.Forms.Label();
            this.label150 = new System.Windows.Forms.Label();
            this.txtRefProvEntityIDQualifier = new System.Windows.Forms.TextBox();
            this.label151 = new System.Windows.Forms.Label();
            this.txtRefProv_UPIN = new System.Windows.Forms.TextBox();
            this.cmbRefferingProvider = new System.Windows.Forms.ComboBox();
            this.label152 = new System.Windows.Forms.Label();
            this.txtRefProvAddress = new System.Windows.Forms.TextBox();
            this.txtRefProvState = new System.Windows.Forms.TextBox();
            this.label153 = new System.Windows.Forms.Label();
            this.label154 = new System.Windows.Forms.Label();
            this.txtRefProvCity = new System.Windows.Forms.TextBox();
            this.label155 = new System.Windows.Forms.Label();
            this.txtRefProvZip = new System.Windows.Forms.TextBox();
            this.label156 = new System.Windows.Forms.Label();
            this.txtRefProv_NPI_ID = new System.Windows.Forms.TextBox();
            this.label157 = new System.Windows.Forms.Label();
            this.label187 = new System.Windows.Forms.Label();
            this.label188 = new System.Windows.Forms.Label();
            this.label189 = new System.Windows.Forms.Label();
            this.label190 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.c1Transaction = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label160 = new System.Windows.Forms.Label();
            this.label161 = new System.Windows.Forms.Label();
            this.label162 = new System.Windows.Forms.Label();
            this.label163 = new System.Windows.Forms.Label();
            this.pnlTransactionDetailsHeader = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label159 = new System.Windows.Forms.Label();
            this.lblTranscationDetails = new System.Windows.Forms.Label();
            this.label184 = new System.Windows.Forms.Label();
            this.label185 = new System.Windows.Forms.Label();
            this.label186 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.label63 = new System.Windows.Forms.Label();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label65 = new System.Windows.Forms.Label();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.label69 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.textBox39 = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.textBox40 = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.label78 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textBox44 = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.textBox45 = new System.Windows.Forms.TextBox();
            this.textBox46 = new System.Windows.Forms.TextBox();
            this.label83 = new System.Windows.Forms.Label();
            this.textBox47 = new System.Windows.Forms.TextBox();
            this.label84 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.textBox48 = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label86 = new System.Windows.Forms.Label();
            this.textBox49 = new System.Windows.Forms.TextBox();
            this.textBox50 = new System.Windows.Forms.TextBox();
            this.textBox51 = new System.Windows.Forms.TextBox();
            this.textBox52 = new System.Windows.Forms.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.textBox53 = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.textBox54 = new System.Windows.Forms.TextBox();
            this.label91 = new System.Windows.Forms.Label();
            this.textBox55 = new System.Windows.Forms.TextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.ts_Commands.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tb_EDIDataDetails.SuspendLayout();
            this.tbpg_EDIDetails.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Transaction1)).BeginInit();
            this.grp_Receiver.SuspendLayout();
            this.grp_Submitter.SuspendLayout();
            this.grp_BHT.SuspendLayout();
            this.grp_TransactionSet.SuspendLayout();
            this.grp_FunctionalGroup.SuspendLayout();
            this.grp_ISASegment.SuspendLayout();
            this.tbpg_BillingProvider.SuspendLayout();
            this.grp_REF.SuspendLayout();
            this.grp_PayToProvider.SuspendLayout();
            this.grp_BillingProvider.SuspendLayout();
            this.grp_Address.SuspendLayout();
            this.tbpg_PatientDetails.SuspendLayout();
            this.pnlPatientInsurence.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlPatientInsuranceHeader.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.grp_PatientDetails.SuspendLayout();
            this.tbpg_TransactionDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grp_Facility.SuspendLayout();
            this.grp_IllnessDetails.SuspendLayout();
            this.grpRefferingProvider.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Transaction)).BeginInit();
            this.pnlTransactionDetailsHeader.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts_Commands
            // 
            this.ts_Commands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ts_Commands.BackgroundImage = global::gloBilling.Properties.Resources.Img_Toolstrip;
            this.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Commands.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Commands.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ts_Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_ShowHCFA1500,
            this.tlb_btnGenerateEDI,
            this.tls_btnValidate,
            this.ToolStripButton1,
            this.tsb_OK,
            this.tsb_Cancel});
            this.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ts_Commands.Location = new System.Drawing.Point(0, 0);
            this.ts_Commands.Name = "ts_Commands";
            this.ts_Commands.Size = new System.Drawing.Size(1178, 53);
            this.ts_Commands.TabIndex = 1;
            this.ts_Commands.Text = "ToolStrip1";
            this.ts_Commands.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_Commands_ItemClicked);
            // 
            // tsb_ShowHCFA1500
            // 
            this.tsb_ShowHCFA1500.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowHCFA1500.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowHCFA1500.Image")));
            this.tsb_ShowHCFA1500.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowHCFA1500.Name = "tsb_ShowHCFA1500";
            this.tsb_ShowHCFA1500.Size = new System.Drawing.Size(79, 50);
            this.tsb_ShowHCFA1500.Tag = "ShowHCFA1500";
            this.tsb_ShowHCFA1500.Text = "HCFA 1500";
            this.tsb_ShowHCFA1500.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tlb_btnGenerateEDI
            // 
            this.tlb_btnGenerateEDI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlb_btnGenerateEDI.Image = ((System.Drawing.Image)(resources.GetObject("tlb_btnGenerateEDI.Image")));
            this.tlb_btnGenerateEDI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlb_btnGenerateEDI.Name = "tlb_btnGenerateEDI";
            this.tlb_btnGenerateEDI.Size = new System.Drawing.Size(99, 50);
            this.tlb_btnGenerateEDI.Tag = "GenerateEDI";
            this.tlb_btnGenerateEDI.Text = "Generate EDI  ";
            this.tlb_btnGenerateEDI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tls_btnValidate
            // 
            this.tls_btnValidate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tls_btnValidate.Image = ((System.Drawing.Image)(resources.GetObject("tls_btnValidate.Image")));
            this.tls_btnValidate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tls_btnValidate.Name = "tls_btnValidate";
            this.tls_btnValidate.Size = new System.Drawing.Size(60, 50);
            this.tls_btnValidate.Tag = "Validate";
            this.tls_btnValidate.Text = "Validate";
            this.tls_btnValidate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tls_btnValidate.Click += new System.EventHandler(this.tls_btnValidate_Click);
            // 
            // ToolStripButton1
            // 
            this.ToolStripButton1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton1.Image")));
            this.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton1.Name = "ToolStripButton1";
            this.ToolStripButton1.Size = new System.Drawing.Size(40, 50);
            this.ToolStripButton1.Text = "Sa&ve";
            this.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolStripButton1.ToolTipText = "Save";
            // 
            // tsb_OK
            // 
            this.tsb_OK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_OK.Image = ((System.Drawing.Image)(resources.GetObject("tsb_OK.Image")));
            this.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_OK.Name = "tsb_OK";
            this.tsb_OK.Size = new System.Drawing.Size(66, 50);
            this.tsb_OK.Tag = "OK";
            this.tsb_OK.Text = "&Save&&Cls";
            this.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_OK.ToolTipText = "Save and Close";
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(47, 50);
            this.tsb_Cancel.Tag = "Cancel";
            this.tsb_Cancel.Text = " &Close";
            this.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Cancel.ToolTipText = "Close";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlMain.Controls.Add(this.tb_EDIDataDetails);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 53);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1178, 742);
            this.pnlMain.TabIndex = 2;
            // 
            // tb_EDIDataDetails
            // 
            this.tb_EDIDataDetails.Controls.Add(this.tbpg_EDIDetails);
            this.tb_EDIDataDetails.Controls.Add(this.tbpg_BillingProvider);
            this.tb_EDIDataDetails.Controls.Add(this.tbpg_PatientDetails);
            this.tb_EDIDataDetails.Controls.Add(this.tbpg_TransactionDetails);
            this.tb_EDIDataDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_EDIDataDetails.Location = new System.Drawing.Point(0, 0);
            this.tb_EDIDataDetails.Name = "tb_EDIDataDetails";
            this.tb_EDIDataDetails.SelectedIndex = 0;
            this.tb_EDIDataDetails.Size = new System.Drawing.Size(1178, 742);
            this.tb_EDIDataDetails.TabIndex = 0;
            // 
            // tbpg_EDIDetails
            // 
            this.tbpg_EDIDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_EDIDetails.Controls.Add(this.panel5);
            this.tbpg_EDIDetails.Controls.Add(this.grp_Receiver);
            this.tbpg_EDIDetails.Controls.Add(this.grp_Submitter);
            this.tbpg_EDIDetails.Controls.Add(this.grp_BHT);
            this.tbpg_EDIDetails.Controls.Add(this.grp_TransactionSet);
            this.tbpg_EDIDetails.Controls.Add(this.grp_FunctionalGroup);
            this.tbpg_EDIDetails.Controls.Add(this.grp_ISASegment);
            this.tbpg_EDIDetails.Location = new System.Drawing.Point(4, 23);
            this.tbpg_EDIDetails.Name = "tbpg_EDIDetails";
            this.tbpg_EDIDetails.Size = new System.Drawing.Size(1170, 715);
            this.tbpg_EDIDetails.TabIndex = 0;
            this.tbpg_EDIDetails.Tag = "Segment";
            this.tbpg_EDIDetails.Text = "Segment Details (1000A and 1000B)";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lbl_BottomBrd);
            this.panel5.Controls.Add(this.lbl_LeftBrd);
            this.panel5.Controls.Add(this.lbl_RightBrd);
            this.panel5.Controls.Add(this.lbl_TopBrd);
            this.panel5.Controls.Add(this.c1Transaction1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 637);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(1170, 78);
            this.panel5.TabIndex = 74;
            // 
            // lbl_BottomBrd
            // 
            this.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_BottomBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_BottomBrd.Location = new System.Drawing.Point(4, 74);
            this.lbl_BottomBrd.Name = "lbl_BottomBrd";
            this.lbl_BottomBrd.Size = new System.Drawing.Size(1162, 1);
            this.lbl_BottomBrd.TabIndex = 77;
            this.lbl_BottomBrd.Text = "label2";
            // 
            // lbl_LeftBrd
            // 
            this.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_LeftBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LeftBrd.Location = new System.Drawing.Point(3, 4);
            this.lbl_LeftBrd.Name = "lbl_LeftBrd";
            this.lbl_LeftBrd.Size = new System.Drawing.Size(1, 71);
            this.lbl_LeftBrd.TabIndex = 76;
            this.lbl_LeftBrd.Text = "label4";
            // 
            // lbl_RightBrd
            // 
            this.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_RightBrd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_RightBrd.Location = new System.Drawing.Point(1166, 4);
            this.lbl_RightBrd.Name = "lbl_RightBrd";
            this.lbl_RightBrd.Size = new System.Drawing.Size(1, 71);
            this.lbl_RightBrd.TabIndex = 75;
            this.lbl_RightBrd.Text = "label3";
            // 
            // lbl_TopBrd
            // 
            this.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_TopBrd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TopBrd.Location = new System.Drawing.Point(3, 3);
            this.lbl_TopBrd.Name = "lbl_TopBrd";
            this.lbl_TopBrd.Size = new System.Drawing.Size(1164, 1);
            this.lbl_TopBrd.TabIndex = 74;
            this.lbl_TopBrd.Text = "label1";
            // 
            // c1Transaction1
            // 
            this.c1Transaction1.AllowEditing = false;
            this.c1Transaction1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Transaction1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Transaction1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Transaction1.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Transaction1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Transaction1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Transaction1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Transaction1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Transaction1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Transaction1.Location = new System.Drawing.Point(3, 3);
            this.c1Transaction1.Name = "c1Transaction1";
            this.c1Transaction1.Rows.Count = 1;
            this.c1Transaction1.Rows.DefaultSize = 19;
            this.c1Transaction1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Transaction1.Size = new System.Drawing.Size(1164, 72);
            this.c1Transaction1.StyleInfo = resources.GetString("c1Transaction1.StyleInfo");
            this.c1Transaction1.TabIndex = 73;
            this.c1Transaction1.Visible = false;
            // 
            // grp_Receiver
            // 
            this.grp_Receiver.Controls.Add(this.cmbRecIDCodeQual);
            this.grp_Receiver.Controls.Add(this.label115);
            this.grp_Receiver.Controls.Add(this.label111);
            this.grp_Receiver.Controls.Add(this.txtRecEntityTypeQualifier);
            this.grp_Receiver.Controls.Add(this.label112);
            this.grp_Receiver.Controls.Add(this.label113);
            this.grp_Receiver.Controls.Add(this.txtEntityIDQualRec);
            this.grp_Receiver.Controls.Add(this.label114);
            this.grp_Receiver.Controls.Add(this.txtRecieverIdentificationCode);
            this.grp_Receiver.Controls.Add(this.label18);
            this.grp_Receiver.Controls.Add(this.txtRecieverName);
            this.grp_Receiver.Controls.Add(this.label17);
            this.grp_Receiver.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_Receiver.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Receiver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_Receiver.Location = new System.Drawing.Point(0, 532);
            this.grp_Receiver.Name = "grp_Receiver";
            this.grp_Receiver.Size = new System.Drawing.Size(1170, 102);
            this.grp_Receiver.TabIndex = 71;
            this.grp_Receiver.TabStop = false;
            this.grp_Receiver.Text = "Reciever";
            // 
            // cmbRecIDCodeQual
            // 
            this.cmbRecIDCodeQual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecIDCodeQual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRecIDCodeQual.ForeColor = System.Drawing.Color.Black;
            this.cmbRecIDCodeQual.FormattingEnabled = true;
            this.cmbRecIDCodeQual.Items.AddRange(new object[] {
            "46 - Electronic Transmitter Identification Number(ETIN)"});
            this.cmbRecIDCodeQual.Location = new System.Drawing.Point(690, 58);
            this.cmbRecIDCodeQual.Name = "cmbRecIDCodeQual";
            this.cmbRecIDCodeQual.Size = new System.Drawing.Size(165, 22);
            this.cmbRecIDCodeQual.TabIndex = 3;
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label115.Location = new System.Drawing.Point(541, 62);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(146, 14);
            this.label115.TabIndex = 57;
            this.label115.Text = "Receiver Identifier Code :";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label111.Location = new System.Drawing.Point(513, 33);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(203, 14);
            this.label111.TabIndex = 62;
            this.label111.Text = "(1 - Person , 2 - Non Person Entity)";
            // 
            // txtRecEntityTypeQualifier
            // 
            this.txtRecEntityTypeQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecEntityTypeQualifier.ForeColor = System.Drawing.Color.Black;
            this.txtRecEntityTypeQualifier.Location = new System.Drawing.Point(460, 29);
            this.txtRecEntityTypeQualifier.Name = "txtRecEntityTypeQualifier";
            this.txtRecEntityTypeQualifier.Size = new System.Drawing.Size(52, 22);
            this.txtRecEntityTypeQualifier.TabIndex = 1;
            this.txtRecEntityTypeQualifier.Tag = "";
            this.txtRecEntityTypeQualifier.Text = "2";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.Location = new System.Drawing.Point(331, 33);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(126, 14);
            this.label112.TabIndex = 61;
            this.label112.Text = "Entity Type Qualifier :";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label113.Location = new System.Drawing.Point(210, 33);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(89, 14);
            this.label113.TabIndex = 59;
            this.label113.Text = "(40 - Receiver)";
            // 
            // txtEntityIDQualRec
            // 
            this.txtEntityIDQualRec.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntityIDQualRec.ForeColor = System.Drawing.Color.Black;
            this.txtEntityIDQualRec.Location = new System.Drawing.Point(157, 29);
            this.txtEntityIDQualRec.Name = "txtEntityIDQualRec";
            this.txtEntityIDQualRec.Size = new System.Drawing.Size(52, 22);
            this.txtEntityIDQualRec.TabIndex = 0;
            this.txtEntityIDQualRec.Tag = "40";
            this.txtEntityIDQualRec.Text = "40";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label114.Location = new System.Drawing.Point(35, 33);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(110, 14);
            this.label114.TabIndex = 58;
            this.label114.Text = "Entity ID Qualifier :";
            // 
            // txtRecieverIdentificationCode
            // 
            this.txtRecieverIdentificationCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecieverIdentificationCode.ForeColor = System.Drawing.Color.Black;
            this.txtRecieverIdentificationCode.Location = new System.Drawing.Point(993, 58);
            this.txtRecieverIdentificationCode.MaxLength = 80;
            this.txtRecieverIdentificationCode.Name = "txtRecieverIdentificationCode";
            this.txtRecieverIdentificationCode.Size = new System.Drawing.Size(137, 22);
            this.txtRecieverIdentificationCode.TabIndex = 4;
            this.txtRecieverIdentificationCode.Text = "23589866";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(871, 62);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(118, 14);
            this.label18.TabIndex = 15;
            this.label18.Text = "Identification Code :";
            // 
            // txtRecieverName
            // 
            this.txtRecieverName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecieverName.ForeColor = System.Drawing.Color.Black;
            this.txtRecieverName.Location = new System.Drawing.Point(157, 58);
            this.txtRecieverName.MaxLength = 35;
            this.txtRecieverName.Name = "txtRecieverName";
            this.txtRecieverName.Size = new System.Drawing.Size(364, 22);
            this.txtRecieverName.TabIndex = 2;
            this.txtRecieverName.Text = "Gateway EDI";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(49, 62);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(96, 14);
            this.label17.TabIndex = 13;
            this.label17.Text = "Reciever Name :";
            // 
            // grp_Submitter
            // 
            this.grp_Submitter.Controls.Add(this.label110);
            this.grp_Submitter.Controls.Add(this.txtSubEntityTypeQualifier);
            this.grp_Submitter.Controls.Add(this.lblEntityType);
            this.grp_Submitter.Controls.Add(this.label107);
            this.grp_Submitter.Controls.Add(this.txtSubEntityIDQualifier);
            this.grp_Submitter.Controls.Add(this.lblEntityIDQualifier);
            this.grp_Submitter.Controls.Add(this.txtSubIdentificationCode);
            this.grp_Submitter.Controls.Add(this.lblIdentifierCode);
            this.grp_Submitter.Controls.Add(this.cmbSubmitterIdentifierCodeQualifier);
            this.grp_Submitter.Controls.Add(this.lblSubmitterIdCode);
            this.grp_Submitter.Controls.Add(this.cmbContactFunctionCode);
            this.grp_Submitter.Controls.Add(this.txtSubmitterAdd2);
            this.grp_Submitter.Controls.Add(this.label106);
            this.grp_Submitter.Controls.Add(this.txtSubmitterContactPersonPhone);
            this.grp_Submitter.Controls.Add(this.label102);
            this.grp_Submitter.Controls.Add(this.txtSubmitterContactPersonPhoneExt);
            this.grp_Submitter.Controls.Add(this.label104);
            this.grp_Submitter.Controls.Add(this.cmbClinic);
            this.grp_Submitter.Controls.Add(this.cmb_CommunicationQualifier);
            this.grp_Submitter.Controls.Add(this.lblSubmitterName);
            this.grp_Submitter.Controls.Add(this.txtSubmitterPhone);
            this.grp_Submitter.Controls.Add(this.label7);
            this.grp_Submitter.Controls.Add(this.txtSubmitterExt);
            this.grp_Submitter.Controls.Add(this.lblEXT);
            this.grp_Submitter.Controls.Add(this.txtSubmitterContactName);
            this.grp_Submitter.Controls.Add(this.lblSubmitterContactName);
            this.grp_Submitter.Controls.Add(this.txtSubmitterAddress);
            this.grp_Submitter.Controls.Add(this.lblSubmitterAddress);
            this.grp_Submitter.Controls.Add(this.txtSubmitterCity);
            this.grp_Submitter.Controls.Add(this.lblSubmitterCity);
            this.grp_Submitter.Controls.Add(this.txtSubmitterState);
            this.grp_Submitter.Controls.Add(this.label59);
            this.grp_Submitter.Controls.Add(this.lblSubmitterState);
            this.grp_Submitter.Controls.Add(this.label58);
            this.grp_Submitter.Controls.Add(this.txtSubmitterZip);
            this.grp_Submitter.Controls.Add(this.lblSubmitterZIP);
            this.grp_Submitter.Controls.Add(this.txtCommunicationNumber);
            this.grp_Submitter.Controls.Add(this.label57);
            this.grp_Submitter.Controls.Add(this.txtSubmitterFax);
            this.grp_Submitter.Controls.Add(this.lblSubmitterFAX);
            this.grp_Submitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_Submitter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Submitter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_Submitter.Location = new System.Drawing.Point(0, 283);
            this.grp_Submitter.Name = "grp_Submitter";
            this.grp_Submitter.Size = new System.Drawing.Size(1170, 249);
            this.grp_Submitter.TabIndex = 70;
            this.grp_Submitter.TabStop = false;
            this.grp_Submitter.Text = "Submitter";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label110.Location = new System.Drawing.Point(564, 23);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(203, 14);
            this.label110.TabIndex = 56;
            this.label110.Text = "(1 - Person , 2 - Non Person Entity)";
            // 
            // txtSubEntityTypeQualifier
            // 
            this.txtSubEntityTypeQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubEntityTypeQualifier.ForeColor = System.Drawing.Color.Black;
            this.txtSubEntityTypeQualifier.Location = new System.Drawing.Point(510, 18);
            this.txtSubEntityTypeQualifier.Name = "txtSubEntityTypeQualifier";
            this.txtSubEntityTypeQualifier.Size = new System.Drawing.Size(52, 22);
            this.txtSubEntityTypeQualifier.TabIndex = 1;
            this.txtSubEntityTypeQualifier.Tag = "";
            this.txtSubEntityTypeQualifier.Text = "2";
            // 
            // lblEntityType
            // 
            this.lblEntityType.AutoSize = true;
            this.lblEntityType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityType.Location = new System.Drawing.Point(381, 23);
            this.lblEntityType.Name = "lblEntityType";
            this.lblEntityType.Size = new System.Drawing.Size(126, 14);
            this.lblEntityType.TabIndex = 55;
            this.lblEntityType.Text = "Entity Type Qualifier :";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(263, 23);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(97, 14);
            this.label107.TabIndex = 53;
            this.label107.Text = "(41 - Submitter)";
            // 
            // txtSubEntityIDQualifier
            // 
            this.txtSubEntityIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubEntityIDQualifier.ForeColor = System.Drawing.Color.Black;
            this.txtSubEntityIDQualifier.Location = new System.Drawing.Point(210, 18);
            this.txtSubEntityIDQualifier.Name = "txtSubEntityIDQualifier";
            this.txtSubEntityIDQualifier.Size = new System.Drawing.Size(52, 22);
            this.txtSubEntityIDQualifier.TabIndex = 0;
            this.txtSubEntityIDQualifier.Tag = "41";
            this.txtSubEntityIDQualifier.Text = "41";
            // 
            // lblEntityIDQualifier
            // 
            this.lblEntityIDQualifier.AutoSize = true;
            this.lblEntityIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityIDQualifier.Location = new System.Drawing.Point(96, 23);
            this.lblEntityIDQualifier.Name = "lblEntityIDQualifier";
            this.lblEntityIDQualifier.Size = new System.Drawing.Size(110, 14);
            this.lblEntityIDQualifier.TabIndex = 52;
            this.lblEntityIDQualifier.Text = "Entity ID Qualifier :";
            // 
            // txtSubIdentificationCode
            // 
            this.txtSubIdentificationCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubIdentificationCode.ForeColor = System.Drawing.Color.Black;
            this.txtSubIdentificationCode.Location = new System.Drawing.Point(971, 178);
            this.txtSubIdentificationCode.Name = "txtSubIdentificationCode";
            this.txtSubIdentificationCode.Size = new System.Drawing.Size(167, 22);
            this.txtSubIdentificationCode.TabIndex = 13;
            this.txtSubIdentificationCode.Text = "23432343";
            // 
            // lblIdentifierCode
            // 
            this.lblIdentifierCode.AutoSize = true;
            this.lblIdentifierCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdentifierCode.Location = new System.Drawing.Point(843, 182);
            this.lblIdentifierCode.Name = "lblIdentifierCode";
            this.lblIdentifierCode.Size = new System.Drawing.Size(118, 14);
            this.lblIdentifierCode.TabIndex = 50;
            this.lblIdentifierCode.Text = "Identification Code :";
            // 
            // cmbSubmitterIdentifierCodeQualifier
            // 
            this.cmbSubmitterIdentifierCodeQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubmitterIdentifierCodeQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubmitterIdentifierCodeQualifier.ForeColor = System.Drawing.Color.Black;
            this.cmbSubmitterIdentifierCodeQualifier.FormattingEnabled = true;
            this.cmbSubmitterIdentifierCodeQualifier.Items.AddRange(new object[] {
            "46 - Electronic Transmitter Identification Number(ETIN)"});
            this.cmbSubmitterIdentifierCodeQualifier.Location = new System.Drawing.Point(665, 178);
            this.cmbSubmitterIdentifierCodeQualifier.Name = "cmbSubmitterIdentifierCodeQualifier";
            this.cmbSubmitterIdentifierCodeQualifier.Size = new System.Drawing.Size(167, 22);
            this.cmbSubmitterIdentifierCodeQualifier.TabIndex = 12;
            // 
            // lblSubmitterIdCode
            // 
            this.lblSubmitterIdCode.AutoSize = true;
            this.lblSubmitterIdCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmitterIdCode.Location = new System.Drawing.Point(461, 182);
            this.lblSubmitterIdCode.Name = "lblSubmitterIdCode";
            this.lblSubmitterIdCode.Size = new System.Drawing.Size(201, 14);
            this.lblSubmitterIdCode.TabIndex = 47;
            this.lblSubmitterIdCode.Text = "Submitter Identifier Code Qualifier :";
            // 
            // cmbContactFunctionCode
            // 
            this.cmbContactFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContactFunctionCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbContactFunctionCode.ForeColor = System.Drawing.Color.Black;
            this.cmbContactFunctionCode.FormattingEnabled = true;
            this.cmbContactFunctionCode.Items.AddRange(new object[] {
            "IC - Information Contact"});
            this.cmbContactFunctionCode.Location = new System.Drawing.Point(210, 178);
            this.cmbContactFunctionCode.Name = "cmbContactFunctionCode";
            this.cmbContactFunctionCode.Size = new System.Drawing.Size(217, 22);
            this.cmbContactFunctionCode.TabIndex = 11;
            // 
            // txtSubmitterAdd2
            // 
            this.txtSubmitterAdd2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterAdd2.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterAdd2.Location = new System.Drawing.Point(210, 114);
            this.txtSubmitterAdd2.Name = "txtSubmitterAdd2";
            this.txtSubmitterAdd2.Size = new System.Drawing.Size(324, 22);
            this.txtSubmitterAdd2.TabIndex = 8;
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label106.Location = new System.Drawing.Point(79, 119);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(127, 14);
            this.label106.TabIndex = 45;
            this.label106.Text = "Submitter Address 2 :";
            // 
            // txtSubmitterContactPersonPhone
            // 
            this.txtSubmitterContactPersonPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterContactPersonPhone.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterContactPersonPhone.Location = new System.Drawing.Point(665, 146);
            this.txtSubmitterContactPersonPhone.MaxLength = 80;
            this.txtSubmitterContactPersonPhone.Name = "txtSubmitterContactPersonPhone";
            this.txtSubmitterContactPersonPhone.Size = new System.Drawing.Size(167, 22);
            this.txtSubmitterContactPersonPhone.TabIndex = 15;
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.Location = new System.Drawing.Point(466, 150);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(196, 14);
            this.label102.TabIndex = 42;
            this.label102.Text = "Submitter Contact Person Phone :";
            // 
            // txtSubmitterContactPersonPhoneExt
            // 
            this.txtSubmitterContactPersonPhoneExt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterContactPersonPhoneExt.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterContactPersonPhoneExt.Location = new System.Drawing.Point(971, 146);
            this.txtSubmitterContactPersonPhoneExt.MaxLength = 4;
            this.txtSubmitterContactPersonPhoneExt.Name = "txtSubmitterContactPersonPhoneExt";
            this.txtSubmitterContactPersonPhoneExt.Size = new System.Drawing.Size(65, 22);
            this.txtSubmitterContactPersonPhoneExt.TabIndex = 41;
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.Location = new System.Drawing.Point(928, 150);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(33, 14);
            this.label104.TabIndex = 43;
            this.label104.Text = "Ext :";
            // 
            // cmbClinic
            // 
            this.cmbClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClinic.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClinic.ForeColor = System.Drawing.Color.Black;
            this.cmbClinic.FormattingEnabled = true;
            this.cmbClinic.Location = new System.Drawing.Point(210, 50);
            this.cmbClinic.Name = "cmbClinic";
            this.cmbClinic.Size = new System.Drawing.Size(324, 22);
            this.cmbClinic.TabIndex = 2;
            this.cmbClinic.SelectedIndexChanged += new System.EventHandler(this.cmbClinic_SelectedIndexChanged);
            // 
            // cmb_CommunicationQualifier
            // 
            this.cmb_CommunicationQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CommunicationQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_CommunicationQualifier.ForeColor = System.Drawing.Color.Black;
            this.cmb_CommunicationQualifier.FormattingEnabled = true;
            this.cmb_CommunicationQualifier.Items.AddRange(new object[] {
            "ED- Electronic Data Interchange number",
            "EM- Electronic Mail",
            "FX- Facimile",
            "TE- Telephone"});
            this.cmb_CommunicationQualifier.Location = new System.Drawing.Point(210, 210);
            this.cmb_CommunicationQualifier.Name = "cmb_CommunicationQualifier";
            this.cmb_CommunicationQualifier.Size = new System.Drawing.Size(217, 22);
            this.cmb_CommunicationQualifier.TabIndex = 16;
            // 
            // lblSubmitterName
            // 
            this.lblSubmitterName.AutoSize = true;
            this.lblSubmitterName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmitterName.Location = new System.Drawing.Point(71, 55);
            this.lblSubmitterName.Name = "lblSubmitterName";
            this.lblSubmitterName.Size = new System.Drawing.Size(135, 14);
            this.lblSubmitterName.TabIndex = 11;
            this.lblSubmitterName.Text = "Submitter/Clinic Name :";
            // 
            // txtSubmitterPhone
            // 
            this.txtSubmitterPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterPhone.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterPhone.Location = new System.Drawing.Point(665, 50);
            this.txtSubmitterPhone.MaxLength = 80;
            this.txtSubmitterPhone.Name = "txtSubmitterPhone";
            this.txtSubmitterPhone.Size = new System.Drawing.Size(167, 22);
            this.txtSubmitterPhone.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(554, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 14);
            this.label7.TabIndex = 34;
            this.label7.Text = "Submitter Phone :";
            // 
            // txtSubmitterExt
            // 
            this.txtSubmitterExt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterExt.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterExt.Location = new System.Drawing.Point(971, 50);
            this.txtSubmitterExt.MaxLength = 4;
            this.txtSubmitterExt.Name = "txtSubmitterExt";
            this.txtSubmitterExt.Size = new System.Drawing.Size(65, 22);
            this.txtSubmitterExt.TabIndex = 4;
            // 
            // lblEXT
            // 
            this.lblEXT.AutoSize = true;
            this.lblEXT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEXT.Location = new System.Drawing.Point(928, 54);
            this.lblEXT.Name = "lblEXT";
            this.lblEXT.Size = new System.Drawing.Size(33, 14);
            this.lblEXT.TabIndex = 36;
            this.lblEXT.Text = "Ext :";
            // 
            // txtSubmitterContactName
            // 
            this.txtSubmitterContactName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterContactName.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterContactName.Location = new System.Drawing.Point(210, 146);
            this.txtSubmitterContactName.MaxLength = 60;
            this.txtSubmitterContactName.Name = "txtSubmitterContactName";
            this.txtSubmitterContactName.Size = new System.Drawing.Size(217, 22);
            this.txtSubmitterContactName.TabIndex = 14;
            this.txtSubmitterContactName.Text = "Sarah Parker";
            // 
            // lblSubmitterContactName
            // 
            this.lblSubmitterContactName.AutoSize = true;
            this.lblSubmitterContactName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmitterContactName.Location = new System.Drawing.Point(14, 151);
            this.lblSubmitterContactName.Name = "lblSubmitterContactName";
            this.lblSubmitterContactName.Size = new System.Drawing.Size(192, 14);
            this.lblSubmitterContactName.TabIndex = 13;
            this.lblSubmitterContactName.Text = "Submitter Contact Person Name :";
            // 
            // txtSubmitterAddress
            // 
            this.txtSubmitterAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterAddress.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterAddress.Location = new System.Drawing.Point(210, 82);
            this.txtSubmitterAddress.Name = "txtSubmitterAddress";
            this.txtSubmitterAddress.Size = new System.Drawing.Size(324, 22);
            this.txtSubmitterAddress.TabIndex = 5;
            // 
            // lblSubmitterAddress
            // 
            this.lblSubmitterAddress.AutoSize = true;
            this.lblSubmitterAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmitterAddress.Location = new System.Drawing.Point(79, 87);
            this.lblSubmitterAddress.Name = "lblSubmitterAddress";
            this.lblSubmitterAddress.Size = new System.Drawing.Size(127, 14);
            this.lblSubmitterAddress.TabIndex = 24;
            this.lblSubmitterAddress.Text = "Submitter Address 1 :";
            // 
            // txtSubmitterCity
            // 
            this.txtSubmitterCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterCity.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterCity.Location = new System.Drawing.Point(665, 82);
            this.txtSubmitterCity.Name = "txtSubmitterCity";
            this.txtSubmitterCity.Size = new System.Drawing.Size(167, 22);
            this.txtSubmitterCity.TabIndex = 6;
            // 
            // lblSubmitterCity
            // 
            this.lblSubmitterCity.AutoSize = true;
            this.lblSubmitterCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmitterCity.Location = new System.Drawing.Point(569, 86);
            this.lblSubmitterCity.Name = "lblSubmitterCity";
            this.lblSubmitterCity.Size = new System.Drawing.Size(93, 14);
            this.lblSubmitterCity.TabIndex = 15;
            this.lblSubmitterCity.Text = "Submitter City :";
            // 
            // txtSubmitterState
            // 
            this.txtSubmitterState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterState.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterState.Location = new System.Drawing.Point(665, 114);
            this.txtSubmitterState.Name = "txtSubmitterState";
            this.txtSubmitterState.Size = new System.Drawing.Size(167, 22);
            this.txtSubmitterState.TabIndex = 9;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(65, 183);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(141, 14);
            this.label59.TabIndex = 18;
            this.label59.Text = "Contact Function Code :";
            // 
            // lblSubmitterState
            // 
            this.lblSubmitterState.AutoSize = true;
            this.lblSubmitterState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmitterState.Location = new System.Drawing.Point(559, 118);
            this.lblSubmitterState.Name = "lblSubmitterState";
            this.lblSubmitterState.Size = new System.Drawing.Size(103, 14);
            this.lblSubmitterState.TabIndex = 18;
            this.lblSubmitterState.Text = "Submitter State :";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(61, 215);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(145, 14);
            this.label58.TabIndex = 20;
            this.label58.Text = "Communication Qualifier :";
            // 
            // txtSubmitterZip
            // 
            this.txtSubmitterZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterZip.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterZip.Location = new System.Drawing.Point(971, 82);
            this.txtSubmitterZip.Name = "txtSubmitterZip";
            this.txtSubmitterZip.Size = new System.Drawing.Size(167, 22);
            this.txtSubmitterZip.TabIndex = 7;
            // 
            // lblSubmitterZIP
            // 
            this.lblSubmitterZIP.AutoSize = true;
            this.lblSubmitterZIP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmitterZIP.Location = new System.Drawing.Point(872, 86);
            this.lblSubmitterZIP.Name = "lblSubmitterZIP";
            this.lblSubmitterZIP.Size = new System.Drawing.Size(89, 14);
            this.lblSubmitterZIP.TabIndex = 20;
            this.lblSubmitterZIP.Text = "Submitter Zip :";
            // 
            // txtCommunicationNumber
            // 
            this.txtCommunicationNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommunicationNumber.ForeColor = System.Drawing.Color.Black;
            this.txtCommunicationNumber.Location = new System.Drawing.Point(665, 210);
            this.txtCommunicationNumber.Name = "txtCommunicationNumber";
            this.txtCommunicationNumber.Size = new System.Drawing.Size(167, 22);
            this.txtCommunicationNumber.TabIndex = 17;
            this.txtCommunicationNumber.Text = "2485654565";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(517, 214);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(145, 14);
            this.label57.TabIndex = 22;
            this.label57.Text = "Communication Number :";
            // 
            // txtSubmitterFax
            // 
            this.txtSubmitterFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubmitterFax.ForeColor = System.Drawing.Color.Black;
            this.txtSubmitterFax.Location = new System.Drawing.Point(971, 114);
            this.txtSubmitterFax.Name = "txtSubmitterFax";
            this.txtSubmitterFax.Size = new System.Drawing.Size(167, 22);
            this.txtSubmitterFax.TabIndex = 10;
            // 
            // lblSubmitterFAX
            // 
            this.lblSubmitterFAX.AutoSize = true;
            this.lblSubmitterFAX.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmitterFAX.Location = new System.Drawing.Point(867, 118);
            this.lblSubmitterFAX.Name = "lblSubmitterFAX";
            this.lblSubmitterFAX.Size = new System.Drawing.Size(94, 14);
            this.lblSubmitterFAX.TabIndex = 22;
            this.lblSubmitterFAX.Text = "Submitter FAX :";
            // 
            // grp_BHT
            // 
            this.grp_BHT.Controls.Add(this.cmbBHT_TSTypeCode);
            this.grp_BHT.Controls.Add(this.cmbBHT_TSPurposeCode);
            this.grp_BHT.Controls.Add(this.txtBHTRefIdentification);
            this.grp_BHT.Controls.Add(this.label56);
            this.grp_BHT.Controls.Add(this.label52);
            this.grp_BHT.Controls.Add(this.label55);
            this.grp_BHT.Controls.Add(this.txtBHT_HerarchicalStrCode);
            this.grp_BHT.Controls.Add(this.label54);
            this.grp_BHT.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_BHT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_BHT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_BHT.Location = new System.Drawing.Point(0, 226);
            this.grp_BHT.Name = "grp_BHT";
            this.grp_BHT.Size = new System.Drawing.Size(1170, 57);
            this.grp_BHT.TabIndex = 72;
            this.grp_BHT.TabStop = false;
            this.grp_BHT.Text = "BHT";
            // 
            // cmbBHT_TSTypeCode
            // 
            this.cmbBHT_TSTypeCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBHT_TSTypeCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBHT_TSTypeCode.ForeColor = System.Drawing.Color.Black;
            this.cmbBHT_TSTypeCode.FormattingEnabled = true;
            this.cmbBHT_TSTypeCode.Items.AddRange(new object[] {
            "CH-Chargeable",
            "RP-Reporting"});
            this.cmbBHT_TSTypeCode.Location = new System.Drawing.Point(978, 22);
            this.cmbBHT_TSTypeCode.Name = "cmbBHT_TSTypeCode";
            this.cmbBHT_TSTypeCode.Size = new System.Drawing.Size(160, 22);
            this.cmbBHT_TSTypeCode.TabIndex = 3;
            // 
            // cmbBHT_TSPurposeCode
            // 
            this.cmbBHT_TSPurposeCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBHT_TSPurposeCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBHT_TSPurposeCode.ForeColor = System.Drawing.Color.Black;
            this.cmbBHT_TSPurposeCode.FormattingEnabled = true;
            this.cmbBHT_TSPurposeCode.Items.AddRange(new object[] {
            "00-Original",
            "01-Re-issue"});
            this.cmbBHT_TSPurposeCode.Location = new System.Drawing.Point(642, 22);
            this.cmbBHT_TSPurposeCode.Name = "cmbBHT_TSPurposeCode";
            this.cmbBHT_TSPurposeCode.Size = new System.Drawing.Size(173, 22);
            this.cmbBHT_TSPurposeCode.TabIndex = 2;
            // 
            // txtBHTRefIdentification
            // 
            this.txtBHTRefIdentification.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBHTRefIdentification.ForeColor = System.Drawing.Color.Black;
            this.txtBHTRefIdentification.Location = new System.Drawing.Point(424, 23);
            this.txtBHTRefIdentification.MaxLength = 80;
            this.txtBHTRefIdentification.Name = "txtBHTRefIdentification";
            this.txtBHTRefIdentification.Size = new System.Drawing.Size(73, 22);
            this.txtBHTRefIdentification.TabIndex = 1;
            this.txtBHTRefIdentification.Text = "1234";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(833, 26);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(142, 14);
            this.label56.TabIndex = 15;
            this.label56.Text = "Transaction Type Code :";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(280, 27);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(140, 14);
            this.label52.TabIndex = 15;
            this.label52.Text = "Reference Identication :";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(528, 26);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(110, 14);
            this.label55.TabIndex = 13;
            this.label55.Text = "TS Purpose Code :";
            // 
            // txtBHT_HerarchicalStrCode
            // 
            this.txtBHT_HerarchicalStrCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBHT_HerarchicalStrCode.ForeColor = System.Drawing.Color.Black;
            this.txtBHT_HerarchicalStrCode.Location = new System.Drawing.Point(187, 23);
            this.txtBHT_HerarchicalStrCode.MaxLength = 35;
            this.txtBHT_HerarchicalStrCode.Name = "txtBHT_HerarchicalStrCode";
            this.txtBHT_HerarchicalStrCode.Size = new System.Drawing.Size(58, 22);
            this.txtBHT_HerarchicalStrCode.TabIndex = 0;
            this.txtBHT_HerarchicalStrCode.Text = "0019";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(23, 27);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(161, 14);
            this.label54.TabIndex = 13;
            this.label54.Text = "Herarchical Structure Code :";
            // 
            // grp_TransactionSet
            // 
            this.grp_TransactionSet.Controls.Add(this.txtTSControlNumber);
            this.grp_TransactionSet.Controls.Add(this.lblTSControlNumber);
            this.grp_TransactionSet.Controls.Add(this.txtTSIdCode);
            this.grp_TransactionSet.Controls.Add(this.label53);
            this.grp_TransactionSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_TransactionSet.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_TransactionSet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_TransactionSet.Location = new System.Drawing.Point(0, 175);
            this.grp_TransactionSet.Name = "grp_TransactionSet";
            this.grp_TransactionSet.Size = new System.Drawing.Size(1170, 51);
            this.grp_TransactionSet.TabIndex = 72;
            this.grp_TransactionSet.TabStop = false;
            this.grp_TransactionSet.Text = "Transaction Set";
            // 
            // txtTSControlNumber
            // 
            this.txtTSControlNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTSControlNumber.ForeColor = System.Drawing.Color.Black;
            this.txtTSControlNumber.Location = new System.Drawing.Point(546, 19);
            this.txtTSControlNumber.MaxLength = 80;
            this.txtTSControlNumber.Name = "txtTSControlNumber";
            this.txtTSControlNumber.Size = new System.Drawing.Size(233, 22);
            this.txtTSControlNumber.TabIndex = 1;
            this.txtTSControlNumber.Text = "54366";
            // 
            // lblTSControlNumber
            // 
            this.lblTSControlNumber.AutoSize = true;
            this.lblTSControlNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTSControlNumber.Location = new System.Drawing.Point(423, 23);
            this.lblTSControlNumber.Name = "lblTSControlNumber";
            this.lblTSControlNumber.Size = new System.Drawing.Size(120, 14);
            this.lblTSControlNumber.TabIndex = 15;
            this.lblTSControlNumber.Text = "TS Control Number :";
            // 
            // txtTSIdCode
            // 
            this.txtTSIdCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTSIdCode.ForeColor = System.Drawing.Color.Black;
            this.txtTSIdCode.Location = new System.Drawing.Point(162, 19);
            this.txtTSIdCode.MaxLength = 35;
            this.txtTSIdCode.Name = "txtTSIdCode";
            this.txtTSIdCode.Size = new System.Drawing.Size(233, 22);
            this.txtTSIdCode.TabIndex = 0;
            this.txtTSIdCode.Text = "837";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(22, 23);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(137, 14);
            this.label53.TabIndex = 13;
            this.label53.Text = "TS Identification Code :";
            // 
            // grp_FunctionalGroup
            // 
            this.grp_FunctionalGroup.Controls.Add(this.textBox2);
            this.grp_FunctionalGroup.Controls.Add(this.label6);
            this.grp_FunctionalGroup.Controls.Add(this.label5);
            this.grp_FunctionalGroup.Controls.Add(this.textBox1);
            this.grp_FunctionalGroup.Controls.Add(this.txtFunctionID);
            this.grp_FunctionalGroup.Controls.Add(this.label4);
            this.grp_FunctionalGroup.Controls.Add(this.txtReceiverDept);
            this.grp_FunctionalGroup.Controls.Add(this.lblSenderDept);
            this.grp_FunctionalGroup.Controls.Add(this.lblFunctionID);
            this.grp_FunctionalGroup.Controls.Add(this.txtSenderDept);
            this.grp_FunctionalGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_FunctionalGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_FunctionalGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_FunctionalGroup.Location = new System.Drawing.Point(0, 90);
            this.grp_FunctionalGroup.Name = "grp_FunctionalGroup";
            this.grp_FunctionalGroup.Size = new System.Drawing.Size(1170, 85);
            this.grp_FunctionalGroup.TabIndex = 69;
            this.grp_FunctionalGroup.TabStop = false;
            this.grp_FunctionalGroup.Text = "Functional Group";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(104, 48);
            this.textBox2.MaxLength = 4;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(139, 22);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "1300";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(59, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 14);
            this.label6.TabIndex = 64;
            this.label6.Text = "Time :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(836, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 14);
            this.label5.TabIndex = 62;
            this.label5.Text = "Date  [CC]YYMMDD :";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(963, 20);
            this.textBox1.MaxLength = 6;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(139, 22);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "20081015";
            // 
            // txtFunctionID
            // 
            this.txtFunctionID.BackColor = System.Drawing.Color.White;
            this.txtFunctionID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFunctionID.ForeColor = System.Drawing.Color.Black;
            this.txtFunctionID.Location = new System.Drawing.Point(104, 20);
            this.txtFunctionID.MaxLength = 2;
            this.txtFunctionID.Name = "txtFunctionID";
            this.txtFunctionID.ReadOnly = true;
            this.txtFunctionID.Size = new System.Drawing.Size(139, 22);
            this.txtFunctionID.TabIndex = 0;
            this.txtFunctionID.Text = "HC";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(563, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Receiver Dept :";
            // 
            // txtReceiverDept
            // 
            this.txtReceiverDept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiverDept.ForeColor = System.Drawing.Color.Black;
            this.txtReceiverDept.Location = new System.Drawing.Point(658, 20);
            this.txtReceiverDept.MaxLength = 15;
            this.txtReceiverDept.Name = "txtReceiverDept";
            this.txtReceiverDept.Size = new System.Drawing.Size(139, 22);
            this.txtReceiverDept.TabIndex = 2;
            this.txtReceiverDept.Text = "Gateway EDI";
            // 
            // lblSenderDept
            // 
            this.lblSenderDept.AutoSize = true;
            this.lblSenderDept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderDept.Location = new System.Drawing.Point(285, 24);
            this.lblSenderDept.Name = "lblSenderDept";
            this.lblSenderDept.Size = new System.Drawing.Size(85, 14);
            this.lblSenderDept.TabIndex = 5;
            this.lblSenderDept.Text = "Sender Dept :";
            // 
            // lblFunctionID
            // 
            this.lblFunctionID.AutoSize = true;
            this.lblFunctionID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFunctionID.Location = new System.Drawing.Point(23, 23);
            this.lblFunctionID.Name = "lblFunctionID";
            this.lblFunctionID.Size = new System.Drawing.Size(78, 14);
            this.lblFunctionID.TabIndex = 58;
            this.lblFunctionID.Text = "Function ID :";
            // 
            // txtSenderDept
            // 
            this.txtSenderDept.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenderDept.ForeColor = System.Drawing.Color.Black;
            this.txtSenderDept.Location = new System.Drawing.Point(374, 20);
            this.txtSenderDept.MaxLength = 15;
            this.txtSenderDept.Name = "txtSenderDept";
            this.txtSenderDept.Size = new System.Drawing.Size(139, 22);
            this.txtSenderDept.TabIndex = 1;
            this.txtSenderDept.Text = "gloClinic";
            // 
            // grp_ISASegment
            // 
            this.grp_ISASegment.Controls.Add(this.label3);
            this.grp_ISASegment.Controls.Add(this.txtSenderID);
            this.grp_ISASegment.Controls.Add(this.txtReferenceID);
            this.grp_ISASegment.Controls.Add(this.txtClaimNo);
            this.grp_ISASegment.Controls.Add(this.txtClaimDate);
            this.grp_ISASegment.Controls.Add(this.lblClaimNumber);
            this.grp_ISASegment.Controls.Add(this.lblSenderID);
            this.grp_ISASegment.Controls.Add(this.lblReferenceID);
            this.grp_ISASegment.Controls.Add(this.txtReceiverID);
            this.grp_ISASegment.Controls.Add(this.label2);
            this.grp_ISASegment.Controls.Add(this.txtClaimTime);
            this.grp_ISASegment.Controls.Add(this.lblClaimTime);
            this.grp_ISASegment.Controls.Add(this.txtControlNo);
            this.grp_ISASegment.Controls.Add(this.lblControlNo);
            this.grp_ISASegment.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_ISASegment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_ISASegment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_ISASegment.Location = new System.Drawing.Point(0, 0);
            this.grp_ISASegment.Name = "grp_ISASegment";
            this.grp_ISASegment.Size = new System.Drawing.Size(1170, 90);
            this.grp_ISASegment.TabIndex = 68;
            this.grp_ISASegment.TabStop = false;
            this.grp_ISASegment.Text = "ISA Segment";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(549, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 14);
            this.label3.TabIndex = 60;
            this.label3.Text = "Claim Date  [CC]YYMMDD :";
            // 
            // txtSenderID
            // 
            this.txtSenderID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenderID.ForeColor = System.Drawing.Color.Black;
            this.txtSenderID.Location = new System.Drawing.Point(104, 22);
            this.txtSenderID.MaxLength = 15;
            this.txtSenderID.Name = "txtSenderID";
            this.txtSenderID.Size = new System.Drawing.Size(139, 22);
            this.txtSenderID.TabIndex = 0;
            this.txtSenderID.Text = "12345";
            // 
            // txtReferenceID
            // 
            this.txtReferenceID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferenceID.ForeColor = System.Drawing.Color.Black;
            this.txtReferenceID.Location = new System.Drawing.Point(707, 54);
            this.txtReferenceID.Name = "txtReferenceID";
            this.txtReferenceID.Size = new System.Drawing.Size(139, 22);
            this.txtReferenceID.TabIndex = 6;
            this.txtReferenceID.Text = "12121";
            // 
            // txtClaimNo
            // 
            this.txtClaimNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimNo.ForeColor = System.Drawing.Color.Black;
            this.txtClaimNo.Location = new System.Drawing.Point(374, 54);
            this.txtClaimNo.Name = "txtClaimNo";
            this.txtClaimNo.Size = new System.Drawing.Size(139, 22);
            this.txtClaimNo.TabIndex = 5;
            this.txtClaimNo.Text = "1021";
            // 
            // txtClaimDate
            // 
            this.txtClaimDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimDate.ForeColor = System.Drawing.Color.Black;
            this.txtClaimDate.Location = new System.Drawing.Point(707, 22);
            this.txtClaimDate.MaxLength = 6;
            this.txtClaimDate.Name = "txtClaimDate";
            this.txtClaimDate.Size = new System.Drawing.Size(139, 22);
            this.txtClaimDate.TabIndex = 2;
            this.txtClaimDate.Text = "081015";
            // 
            // lblClaimNumber
            // 
            this.lblClaimNumber.AutoSize = true;
            this.lblClaimNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimNumber.Location = new System.Drawing.Point(281, 58);
            this.lblClaimNumber.Name = "lblClaimNumber";
            this.lblClaimNumber.Size = new System.Drawing.Size(89, 14);
            this.lblClaimNumber.TabIndex = 30;
            this.lblClaimNumber.Text = "Claim Number :";
            // 
            // lblSenderID
            // 
            this.lblSenderID.AutoSize = true;
            this.lblSenderID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenderID.Location = new System.Drawing.Point(31, 26);
            this.lblSenderID.Name = "lblSenderID";
            this.lblSenderID.Size = new System.Drawing.Size(70, 14);
            this.lblSenderID.TabIndex = 1;
            this.lblSenderID.Text = "Sender ID :";
            // 
            // lblReferenceID
            // 
            this.lblReferenceID.AutoSize = true;
            this.lblReferenceID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReferenceID.Location = new System.Drawing.Point(616, 58);
            this.lblReferenceID.Name = "lblReferenceID";
            this.lblReferenceID.Size = new System.Drawing.Size(87, 14);
            this.lblReferenceID.TabIndex = 32;
            this.lblReferenceID.Text = "Reference ID :";
            // 
            // txtReceiverID
            // 
            this.txtReceiverID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiverID.ForeColor = System.Drawing.Color.Black;
            this.txtReceiverID.Location = new System.Drawing.Point(374, 22);
            this.txtReceiverID.MaxLength = 15;
            this.txtReceiverID.Name = "txtReceiverID";
            this.txtReceiverID.Size = new System.Drawing.Size(139, 22);
            this.txtReceiverID.TabIndex = 1;
            this.txtReceiverID.Text = "V2LE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(293, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Receiver ID :";
            // 
            // txtClaimTime
            // 
            this.txtClaimTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaimTime.ForeColor = System.Drawing.Color.Black;
            this.txtClaimTime.Location = new System.Drawing.Point(963, 21);
            this.txtClaimTime.MaxLength = 4;
            this.txtClaimTime.Name = "txtClaimTime";
            this.txtClaimTime.Size = new System.Drawing.Size(139, 22);
            this.txtClaimTime.TabIndex = 3;
            this.txtClaimTime.Text = "1300";
            // 
            // lblClaimTime
            // 
            this.lblClaimTime.AutoSize = true;
            this.lblClaimTime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaimTime.Location = new System.Drawing.Point(886, 25);
            this.lblClaimTime.Name = "lblClaimTime";
            this.lblClaimTime.Size = new System.Drawing.Size(73, 14);
            this.lblClaimTime.TabIndex = 26;
            this.lblClaimTime.Text = "Claim Time :";
            // 
            // txtControlNo
            // 
            this.txtControlNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtControlNo.ForeColor = System.Drawing.Color.Black;
            this.txtControlNo.Location = new System.Drawing.Point(104, 52);
            this.txtControlNo.MaxLength = 9;
            this.txtControlNo.Name = "txtControlNo";
            this.txtControlNo.Size = new System.Drawing.Size(139, 22);
            this.txtControlNo.TabIndex = 4;
            this.txtControlNo.Text = "987654321";
            // 
            // lblControlNo
            // 
            this.lblControlNo.AutoSize = true;
            this.lblControlNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlNo.Location = new System.Drawing.Point(28, 56);
            this.lblControlNo.Name = "lblControlNo";
            this.lblControlNo.Size = new System.Drawing.Size(73, 14);
            this.lblControlNo.TabIndex = 28;
            this.lblControlNo.Text = "Control No :";
            // 
            // tbpg_BillingProvider
            // 
            this.tbpg_BillingProvider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_BillingProvider.Controls.Add(this.grp_REF);
            this.tbpg_BillingProvider.Controls.Add(this.grp_PayToProvider);
            this.tbpg_BillingProvider.Controls.Add(this.grp_BillingProvider);
            this.tbpg_BillingProvider.Location = new System.Drawing.Point(4, 23);
            this.tbpg_BillingProvider.Name = "tbpg_BillingProvider";
            this.tbpg_BillingProvider.Size = new System.Drawing.Size(1170, 715);
            this.tbpg_BillingProvider.TabIndex = 3;
            this.tbpg_BillingProvider.Text = "Billing Provider (2000 A)";
            // 
            // grp_REF
            // 
            this.grp_REF.Controls.Add(this.txtREFIdentificationRefNo);
            this.grp_REF.Controls.Add(this.label165);
            this.grp_REF.Controls.Add(this.cmbREF_ReferenceIdCode);
            this.grp_REF.Controls.Add(this.label166);
            this.grp_REF.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_REF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_REF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_REF.Location = new System.Drawing.Point(0, 510);
            this.grp_REF.Name = "grp_REF";
            this.grp_REF.Size = new System.Drawing.Size(1170, 65);
            this.grp_REF.TabIndex = 83;
            this.grp_REF.TabStop = false;
            this.grp_REF.Text = "Provider REF Section";
            // 
            // txtREFIdentificationRefNo
            // 
            this.txtREFIdentificationRefNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtREFIdentificationRefNo.ForeColor = System.Drawing.Color.Black;
            this.txtREFIdentificationRefNo.Location = new System.Drawing.Point(596, 22);
            this.txtREFIdentificationRefNo.Name = "txtREFIdentificationRefNo";
            this.txtREFIdentificationRefNo.Size = new System.Drawing.Size(151, 22);
            this.txtREFIdentificationRefNo.TabIndex = 1;
            this.txtREFIdentificationRefNo.Text = "121323322";
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label165.Location = new System.Drawing.Point(467, 26);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(118, 14);
            this.label165.TabIndex = 80;
            this.label165.Text = "Reference Number :";
            // 
            // cmbREF_ReferenceIdCode
            // 
            this.cmbREF_ReferenceIdCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbREF_ReferenceIdCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbREF_ReferenceIdCode.ForeColor = System.Drawing.Color.Black;
            this.cmbREF_ReferenceIdCode.FormattingEnabled = true;
            this.cmbREF_ReferenceIdCode.Items.AddRange(new object[] {
            "24 - Employer\'s Identification Number",
            "34 - Social Security Number",
            "XX - HCFA-NPI"});
            this.cmbREF_ReferenceIdCode.Location = new System.Drawing.Point(206, 22);
            this.cmbREF_ReferenceIdCode.Name = "cmbREF_ReferenceIdCode";
            this.cmbREF_ReferenceIdCode.Size = new System.Drawing.Size(203, 22);
            this.cmbREF_ReferenceIdCode.TabIndex = 0;
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label166.Location = new System.Drawing.Point(15, 26);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(178, 14);
            this.label166.TabIndex = 77;
            this.label166.Text = "Reference Identification Code :";
            // 
            // grp_PayToProvider
            // 
            this.grp_PayToProvider.Controls.Add(this.txtBLIdentificationCode);
            this.grp_PayToProvider.Controls.Add(this.label133);
            this.grp_PayToProvider.Controls.Add(this.label143);
            this.grp_PayToProvider.Controls.Add(this.cmbBLCodeQualifier);
            this.grp_PayToProvider.Controls.Add(this.txtPTPIDCode);
            this.grp_PayToProvider.Controls.Add(this.label134);
            this.grp_PayToProvider.Controls.Add(this.txtPTPUPIN);
            this.grp_PayToProvider.Controls.Add(this.label139);
            this.grp_PayToProvider.Controls.Add(this.label126);
            this.grp_PayToProvider.Controls.Add(this.label116);
            this.grp_PayToProvider.Controls.Add(this.cmbPTPIDCodeQualifier);
            this.grp_PayToProvider.Controls.Add(this.txtBLProvEntityType);
            this.grp_PayToProvider.Controls.Add(this.label141);
            this.grp_PayToProvider.Controls.Add(this.label117);
            this.grp_PayToProvider.Controls.Add(this.label142);
            this.grp_PayToProvider.Controls.Add(this.label130);
            this.grp_PayToProvider.Controls.Add(this.cmbPTPName);
            this.grp_PayToProvider.Controls.Add(this.txtBLProvEntitiyIDQual);
            this.grp_PayToProvider.Controls.Add(this.label140);
            this.grp_PayToProvider.Controls.Add(this.label131);
            this.grp_PayToProvider.Controls.Add(this.label135);
            this.grp_PayToProvider.Controls.Add(this.txtBLProviderUPIN);
            this.grp_PayToProvider.Controls.Add(this.txtPTPType);
            this.grp_PayToProvider.Controls.Add(this.cmbBillingProvider);
            this.grp_PayToProvider.Controls.Add(this.label136);
            this.grp_PayToProvider.Controls.Add(this.label119);
            this.grp_PayToProvider.Controls.Add(this.label137);
            this.grp_PayToProvider.Controls.Add(this.txtBLProviderAddress);
            this.grp_PayToProvider.Controls.Add(this.txtPTPIDQualifier);
            this.grp_PayToProvider.Controls.Add(this.txtBLProviderState);
            this.grp_PayToProvider.Controls.Add(this.label138);
            this.grp_PayToProvider.Controls.Add(this.label120);
            this.grp_PayToProvider.Controls.Add(this.label118);
            this.grp_PayToProvider.Controls.Add(this.label121);
            this.grp_PayToProvider.Controls.Add(this.txtPTPAddress);
            this.grp_PayToProvider.Controls.Add(this.txtBLProviderCity);
            this.grp_PayToProvider.Controls.Add(this.txtPTPState);
            this.grp_PayToProvider.Controls.Add(this.label123);
            this.grp_PayToProvider.Controls.Add(this.label122);
            this.grp_PayToProvider.Controls.Add(this.txtBLProviderZip);
            this.grp_PayToProvider.Controls.Add(this.txtPTPCity);
            this.grp_PayToProvider.Controls.Add(this.label124);
            this.grp_PayToProvider.Controls.Add(this.label127);
            this.grp_PayToProvider.Controls.Add(this.txtBLProvNPI_ID);
            this.grp_PayToProvider.Controls.Add(this.txtPTPZip);
            this.grp_PayToProvider.Controls.Add(this.label125);
            this.grp_PayToProvider.Controls.Add(this.label128);
            this.grp_PayToProvider.Controls.Add(this.txtPTPNPI_ID);
            this.grp_PayToProvider.Controls.Add(this.label129);
            this.grp_PayToProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_PayToProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_PayToProvider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_PayToProvider.Location = new System.Drawing.Point(0, 116);
            this.grp_PayToProvider.Name = "grp_PayToProvider";
            this.grp_PayToProvider.Size = new System.Drawing.Size(1170, 394);
            this.grp_PayToProvider.TabIndex = 82;
            this.grp_PayToProvider.TabStop = false;
            this.grp_PayToProvider.Text = "Payto Provider";
            this.grp_PayToProvider.Visible = false;
            // 
            // txtBLIdentificationCode
            // 
            this.txtBLIdentificationCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLIdentificationCode.ForeColor = System.Drawing.Color.Black;
            this.txtBLIdentificationCode.Location = new System.Drawing.Point(534, 363);
            this.txtBLIdentificationCode.Name = "txtBLIdentificationCode";
            this.txtBLIdentificationCode.Size = new System.Drawing.Size(151, 22);
            this.txtBLIdentificationCode.TabIndex = 10;
            this.txtBLIdentificationCode.Text = "213213212";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label133.Location = new System.Drawing.Point(412, 367);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(118, 14);
            this.label133.TabIndex = 73;
            this.label133.Text = "Identification Code :";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label143.Location = new System.Drawing.Point(468, 154);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(42, 14);
            this.label143.TabIndex = 76;
            this.label143.Text = "UPIN :";
            // 
            // cmbBLCodeQualifier
            // 
            this.cmbBLCodeQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBLCodeQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBLCodeQualifier.ForeColor = System.Drawing.Color.Black;
            this.cmbBLCodeQualifier.FormattingEnabled = true;
            this.cmbBLCodeQualifier.Items.AddRange(new object[] {
            "24 - Employer\'s Identification Number",
            "34 - Social Security Number",
            "XX - HCFA-NPI"});
            this.cmbBLCodeQualifier.Location = new System.Drawing.Point(174, 363);
            this.cmbBLCodeQualifier.Name = "cmbBLCodeQualifier";
            this.cmbBLCodeQualifier.Size = new System.Drawing.Size(203, 22);
            this.cmbBLCodeQualifier.TabIndex = 9;
            // 
            // txtPTPIDCode
            // 
            this.txtPTPIDCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTPIDCode.ForeColor = System.Drawing.Color.Black;
            this.txtPTPIDCode.Location = new System.Drawing.Point(517, 181);
            this.txtPTPIDCode.Name = "txtPTPIDCode";
            this.txtPTPIDCode.Size = new System.Drawing.Size(151, 22);
            this.txtPTPIDCode.TabIndex = 10;
            this.txtPTPIDCode.Text = "213213133";
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label134.Location = new System.Drawing.Point(27, 368);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(143, 14);
            this.label134.TabIndex = 70;
            this.label134.Text = "Identifier Code Qualifier :";
            // 
            // txtPTPUPIN
            // 
            this.txtPTPUPIN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTPUPIN.ForeColor = System.Drawing.Color.Black;
            this.txtPTPUPIN.Location = new System.Drawing.Point(517, 150);
            this.txtPTPUPIN.MaxLength = 9;
            this.txtPTPUPIN.Name = "txtPTPUPIN";
            this.txtPTPUPIN.Size = new System.Drawing.Size(151, 22);
            this.txtPTPUPIN.TabIndex = 8;
            this.txtPTPUPIN.Text = "21213212";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label139.Location = new System.Drawing.Point(488, 338);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(42, 14);
            this.label139.TabIndex = 74;
            this.label139.Text = "UPIN :";
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label126.Location = new System.Drawing.Point(392, 185);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(118, 14);
            this.label126.TabIndex = 80;
            this.label126.Text = "Identification Code :";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label116.Location = new System.Drawing.Point(593, 217);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(203, 14);
            this.label116.TabIndex = 67;
            this.label116.Text = "(1 - Person , 2 - Non Person Entity)";
            // 
            // cmbPTPIDCodeQualifier
            // 
            this.cmbPTPIDCodeQualifier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPTPIDCodeQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPTPIDCodeQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPTPIDCodeQualifier.ForeColor = System.Drawing.Color.Black;
            this.cmbPTPIDCodeQualifier.FormattingEnabled = true;
            this.cmbPTPIDCodeQualifier.Items.AddRange(new object[] {
            "24 - Employer\'s Identification Number",
            "34 - Social Security Number",
            "XX - HCFA-NPI"});
            this.cmbPTPIDCodeQualifier.Location = new System.Drawing.Point(174, 181);
            this.cmbPTPIDCodeQualifier.Name = "cmbPTPIDCodeQualifier";
            this.cmbPTPIDCodeQualifier.Size = new System.Drawing.Size(203, 23);
            this.cmbPTPIDCodeQualifier.TabIndex = 9;
            // 
            // txtBLProvEntityType
            // 
            this.txtBLProvEntityType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLProvEntityType.ForeColor = System.Drawing.Color.Black;
            this.txtBLProvEntityType.Location = new System.Drawing.Point(551, 214);
            this.txtBLProvEntityType.Name = "txtBLProvEntityType";
            this.txtBLProvEntityType.Size = new System.Drawing.Size(42, 22);
            this.txtBLProvEntityType.TabIndex = 1;
            this.txtBLProvEntityType.Tag = "";
            this.txtBLProvEntityType.Text = "1";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label141.Location = new System.Drawing.Point(27, 185);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(143, 14);
            this.label141.TabIndex = 77;
            this.label141.Text = "Identifier Code Qualifier :";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label117.Location = new System.Drawing.Point(424, 218);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(126, 14);
            this.label117.TabIndex = 66;
            this.label117.Text = "Entity Type Qualifier :";
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label142.Location = new System.Drawing.Point(337, 190);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(38, 13);
            this.label142.TabIndex = 76;
            this.label142.Text = "UPIN :";
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label130.Location = new System.Drawing.Point(227, 218);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(120, 14);
            this.label130.TabIndex = 64;
            this.label130.Text = "(85 - Billing Provider)";
            // 
            // cmbPTPName
            // 
            this.cmbPTPName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPTPName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPTPName.ForeColor = System.Drawing.Color.Black;
            this.cmbPTPName.FormattingEnabled = true;
            this.cmbPTPName.Location = new System.Drawing.Point(174, 56);
            this.cmbPTPName.Name = "cmbPTPName";
            this.cmbPTPName.Size = new System.Drawing.Size(383, 22);
            this.cmbPTPName.TabIndex = 2;
            this.cmbPTPName.SelectedIndexChanged += new System.EventHandler(this.cmbPTPName_SelectedIndexChanged);
            // 
            // txtBLProvEntitiyIDQual
            // 
            this.txtBLProvEntitiyIDQual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLProvEntitiyIDQual.ForeColor = System.Drawing.Color.Black;
            this.txtBLProvEntitiyIDQual.Location = new System.Drawing.Point(174, 214);
            this.txtBLProvEntitiyIDQual.Name = "txtBLProvEntitiyIDQual";
            this.txtBLProvEntitiyIDQual.Size = new System.Drawing.Size(52, 22);
            this.txtBLProvEntitiyIDQual.TabIndex = 0;
            this.txtBLProvEntitiyIDQual.Tag = "85";
            this.txtBLProvEntitiyIDQual.Text = "85";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label140.Location = new System.Drawing.Point(124, 60);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(46, 14);
            this.label140.TabIndex = 74;
            this.label140.Text = "Name :";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label131.Location = new System.Drawing.Point(60, 218);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(110, 14);
            this.label131.TabIndex = 63;
            this.label131.Text = "Entity ID Qualifier :";
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label135.Location = new System.Drawing.Point(594, 29);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(203, 14);
            this.label135.TabIndex = 73;
            this.label135.Text = "(1 - Person , 2 - Non Person Entity)";
            // 
            // txtBLProviderUPIN
            // 
            this.txtBLProviderUPIN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLProviderUPIN.ForeColor = System.Drawing.Color.Black;
            this.txtBLProviderUPIN.Location = new System.Drawing.Point(534, 334);
            this.txtBLProviderUPIN.MaxLength = 9;
            this.txtBLProviderUPIN.Name = "txtBLProviderUPIN";
            this.txtBLProviderUPIN.Size = new System.Drawing.Size(151, 22);
            this.txtBLProviderUPIN.TabIndex = 8;
            this.txtBLProviderUPIN.Text = "3212122";
            // 
            // txtPTPType
            // 
            this.txtPTPType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTPType.ForeColor = System.Drawing.Color.Black;
            this.txtPTPType.Location = new System.Drawing.Point(534, 25);
            this.txtPTPType.Name = "txtPTPType";
            this.txtPTPType.Size = new System.Drawing.Size(42, 22);
            this.txtPTPType.TabIndex = 1;
            this.txtPTPType.Tag = "";
            this.txtPTPType.Text = "1";
            // 
            // cmbBillingProvider
            // 
            this.cmbBillingProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBillingProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBillingProvider.ForeColor = System.Drawing.Color.Black;
            this.cmbBillingProvider.FormattingEnabled = true;
            this.cmbBillingProvider.Location = new System.Drawing.Point(174, 243);
            this.cmbBillingProvider.Name = "cmbBillingProvider";
            this.cmbBillingProvider.Size = new System.Drawing.Size(404, 22);
            this.cmbBillingProvider.TabIndex = 2;
            this.cmbBillingProvider.SelectedIndexChanged += new System.EventHandler(this.cmbBillingProvider_SelectedIndexChanged);
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label136.Location = new System.Drawing.Point(401, 29);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(126, 14);
            this.label136.TabIndex = 72;
            this.label136.Text = "Entity Type Qualifier :";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label119.Location = new System.Drawing.Point(485, 309);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(45, 14);
            this.label119.TabIndex = 60;
            this.label119.Text = "State :";
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label137.Location = new System.Drawing.Point(232, 29);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(133, 13);
            this.label137.TabIndex = 70;
            this.label137.Text = "(87 - Pay-To Provider)";
            // 
            // txtBLProviderAddress
            // 
            this.txtBLProviderAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLProviderAddress.ForeColor = System.Drawing.Color.Black;
            this.txtBLProviderAddress.Location = new System.Drawing.Point(174, 272);
            this.txtBLProviderAddress.Multiline = true;
            this.txtBLProviderAddress.Name = "txtBLProviderAddress";
            this.txtBLProviderAddress.Size = new System.Drawing.Size(512, 26);
            this.txtBLProviderAddress.TabIndex = 3;
            // 
            // txtPTPIDQualifier
            // 
            this.txtPTPIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTPIDQualifier.ForeColor = System.Drawing.Color.Black;
            this.txtPTPIDQualifier.Location = new System.Drawing.Point(174, 25);
            this.txtPTPIDQualifier.Name = "txtPTPIDQualifier";
            this.txtPTPIDQualifier.Size = new System.Drawing.Size(52, 22);
            this.txtPTPIDQualifier.TabIndex = 0;
            this.txtPTPIDQualifier.Tag = "87";
            this.txtPTPIDQualifier.Text = "87";
            // 
            // txtBLProviderState
            // 
            this.txtBLProviderState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLProviderState.ForeColor = System.Drawing.Color.Black;
            this.txtBLProviderState.Location = new System.Drawing.Point(534, 305);
            this.txtBLProviderState.MaxLength = 6;
            this.txtBLProviderState.Name = "txtBLProviderState";
            this.txtBLProviderState.Size = new System.Drawing.Size(151, 22);
            this.txtBLProviderState.TabIndex = 5;
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label138.Location = new System.Drawing.Point(60, 29);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(110, 14);
            this.label138.TabIndex = 69;
            this.label138.Text = "Entity ID Qualifier :";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label120.Location = new System.Drawing.Point(112, 278);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(58, 14);
            this.label120.TabIndex = 30;
            this.label120.Text = "Address :";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label118.Location = new System.Drawing.Point(465, 123);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(45, 14);
            this.label118.TabIndex = 60;
            this.label118.Text = "State :";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label121.Location = new System.Drawing.Point(124, 248);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(46, 14);
            this.label121.TabIndex = 1;
            this.label121.Text = "Name :";
            // 
            // txtPTPAddress
            // 
            this.txtPTPAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTPAddress.ForeColor = System.Drawing.Color.Black;
            this.txtPTPAddress.Location = new System.Drawing.Point(174, 85);
            this.txtPTPAddress.Multiline = true;
            this.txtPTPAddress.Name = "txtPTPAddress";
            this.txtPTPAddress.Size = new System.Drawing.Size(644, 26);
            this.txtPTPAddress.TabIndex = 3;
            // 
            // txtBLProviderCity
            // 
            this.txtBLProviderCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLProviderCity.ForeColor = System.Drawing.Color.Black;
            this.txtBLProviderCity.Location = new System.Drawing.Point(174, 305);
            this.txtBLProviderCity.MaxLength = 15;
            this.txtBLProviderCity.Name = "txtBLProviderCity";
            this.txtBLProviderCity.Size = new System.Drawing.Size(203, 22);
            this.txtBLProviderCity.TabIndex = 4;
            // 
            // txtPTPState
            // 
            this.txtPTPState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTPState.ForeColor = System.Drawing.Color.Black;
            this.txtPTPState.Location = new System.Drawing.Point(517, 118);
            this.txtPTPState.MaxLength = 6;
            this.txtPTPState.Name = "txtPTPState";
            this.txtPTPState.Size = new System.Drawing.Size(151, 22);
            this.txtPTPState.TabIndex = 5;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label123.Location = new System.Drawing.Point(135, 308);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(35, 14);
            this.label123.TabIndex = 3;
            this.label123.Text = "City :";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label122.Location = new System.Drawing.Point(112, 92);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(58, 14);
            this.label122.TabIndex = 30;
            this.label122.Text = "Address :";
            // 
            // txtBLProviderZip
            // 
            this.txtBLProviderZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLProviderZip.ForeColor = System.Drawing.Color.Black;
            this.txtBLProviderZip.Location = new System.Drawing.Point(730, 305);
            this.txtBLProviderZip.MaxLength = 4;
            this.txtBLProviderZip.Name = "txtBLProviderZip";
            this.txtBLProviderZip.Size = new System.Drawing.Size(104, 22);
            this.txtBLProviderZip.TabIndex = 6;
            // 
            // txtPTPCity
            // 
            this.txtPTPCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTPCity.ForeColor = System.Drawing.Color.Black;
            this.txtPTPCity.Location = new System.Drawing.Point(174, 118);
            this.txtPTPCity.MaxLength = 15;
            this.txtPTPCity.Name = "txtPTPCity";
            this.txtPTPCity.Size = new System.Drawing.Size(203, 22);
            this.txtPTPCity.TabIndex = 4;
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label124.Location = new System.Drawing.Point(696, 309);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(31, 14);
            this.label124.TabIndex = 26;
            this.label124.Text = "Zip :";
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label127.Location = new System.Drawing.Point(135, 123);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(35, 14);
            this.label127.TabIndex = 3;
            this.label127.Text = "City :";
            // 
            // txtBLProvNPI_ID
            // 
            this.txtBLProvNPI_ID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBLProvNPI_ID.ForeColor = System.Drawing.Color.Black;
            this.txtBLProvNPI_ID.Location = new System.Drawing.Point(174, 334);
            this.txtBLProvNPI_ID.MaxLength = 9;
            this.txtBLProvNPI_ID.Name = "txtBLProvNPI_ID";
            this.txtBLProvNPI_ID.Size = new System.Drawing.Size(203, 22);
            this.txtBLProvNPI_ID.TabIndex = 7;
            // 
            // txtPTPZip
            // 
            this.txtPTPZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTPZip.ForeColor = System.Drawing.Color.Black;
            this.txtPTPZip.Location = new System.Drawing.Point(713, 118);
            this.txtPTPZip.MaxLength = 4;
            this.txtPTPZip.Name = "txtPTPZip";
            this.txtPTPZip.Size = new System.Drawing.Size(104, 22);
            this.txtPTPZip.TabIndex = 6;
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label125.Location = new System.Drawing.Point(119, 338);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(51, 14);
            this.label125.TabIndex = 28;
            this.label125.Text = "NPI/ID :";
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label128.Location = new System.Drawing.Point(677, 123);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(31, 14);
            this.label128.TabIndex = 26;
            this.label128.Text = "Zip :";
            // 
            // txtPTPNPI_ID
            // 
            this.txtPTPNPI_ID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPTPNPI_ID.ForeColor = System.Drawing.Color.Black;
            this.txtPTPNPI_ID.Location = new System.Drawing.Point(174, 150);
            this.txtPTPNPI_ID.MaxLength = 9;
            this.txtPTPNPI_ID.Name = "txtPTPNPI_ID";
            this.txtPTPNPI_ID.Size = new System.Drawing.Size(203, 22);
            this.txtPTPNPI_ID.TabIndex = 7;
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label129.Location = new System.Drawing.Point(119, 154);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(51, 14);
            this.label129.TabIndex = 28;
            this.label129.Text = "NPI/ID :";
            // 
            // grp_BillingProvider
            // 
            this.grp_BillingProvider.Controls.Add(this.grp_Address);
            this.grp_BillingProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_BillingProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_BillingProvider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_BillingProvider.Location = new System.Drawing.Point(0, 0);
            this.grp_BillingProvider.Name = "grp_BillingProvider";
            this.grp_BillingProvider.Size = new System.Drawing.Size(1170, 116);
            this.grp_BillingProvider.TabIndex = 81;
            this.grp_BillingProvider.TabStop = false;
            this.grp_BillingProvider.Text = "Billing Provider";
            // 
            // grp_Address
            // 
            this.grp_Address.Controls.Add(this.rdb_Company);
            this.grp_Address.Controls.Add(this.rdb_Practice);
            this.grp_Address.Controls.Add(this.rdb_Business);
            this.grp_Address.Location = new System.Drawing.Point(53, 26);
            this.grp_Address.Name = "grp_Address";
            this.grp_Address.Size = new System.Drawing.Size(323, 60);
            this.grp_Address.TabIndex = 1;
            this.grp_Address.TabStop = false;
            this.grp_Address.Text = "Address";
            // 
            // rdb_Company
            // 
            this.rdb_Company.AutoSize = true;
            this.rdb_Company.Location = new System.Drawing.Point(211, 26);
            this.rdb_Company.Name = "rdb_Company";
            this.rdb_Company.Size = new System.Drawing.Size(82, 18);
            this.rdb_Company.TabIndex = 2;
            this.rdb_Company.TabStop = true;
            this.rdb_Company.Text = "Company";
            this.rdb_Company.UseVisualStyleBackColor = true;
            // 
            // rdb_Practice
            // 
            this.rdb_Practice.AutoSize = true;
            this.rdb_Practice.Location = new System.Drawing.Point(117, 26);
            this.rdb_Practice.Name = "rdb_Practice";
            this.rdb_Practice.Size = new System.Drawing.Size(73, 18);
            this.rdb_Practice.TabIndex = 1;
            this.rdb_Practice.TabStop = true;
            this.rdb_Practice.Text = "Practice";
            this.rdb_Practice.UseVisualStyleBackColor = true;
            // 
            // rdb_Business
            // 
            this.rdb_Business.AutoSize = true;
            this.rdb_Business.Location = new System.Drawing.Point(10, 24);
            this.rdb_Business.Name = "rdb_Business";
            this.rdb_Business.Size = new System.Drawing.Size(77, 18);
            this.rdb_Business.TabIndex = 0;
            this.rdb_Business.TabStop = true;
            this.rdb_Business.Text = "Business";
            this.rdb_Business.UseVisualStyleBackColor = true;
            // 
            // tbpg_PatientDetails
            // 
            this.tbpg_PatientDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_PatientDetails.Controls.Add(this.pnlPatientInsurence);
            this.tbpg_PatientDetails.Controls.Add(this.panel4);
            this.tbpg_PatientDetails.Controls.Add(this.pnlPatientInsuranceHeader);
            this.tbpg_PatientDetails.Controls.Add(this.panel3);
            this.tbpg_PatientDetails.Location = new System.Drawing.Point(4, 23);
            this.tbpg_PatientDetails.Name = "tbpg_PatientDetails";
            this.tbpg_PatientDetails.Size = new System.Drawing.Size(1170, 715);
            this.tbpg_PatientDetails.TabIndex = 2;
            this.tbpg_PatientDetails.Tag = "Patient";
            this.tbpg_PatientDetails.Text = "Patient (Subscriber 2000 B) Insurance 2010 BB";
            // 
            // pnlPatientInsurence
            // 
            this.pnlPatientInsurence.Controls.Add(this.chkUseSecondaryInsurance);
            this.pnlPatientInsurence.Controls.Add(this.label183);
            this.pnlPatientInsurence.Controls.Add(this.txtSubscriberMiddleName);
            this.pnlPatientInsurence.Controls.Add(this.label182);
            this.pnlPatientInsurence.Controls.Add(this.txtSubscriberFirstName);
            this.pnlPatientInsurence.Controls.Add(this.label167);
            this.pnlPatientInsurence.Controls.Add(this.dtpInsuranceExpiryDate);
            this.pnlPatientInsurence.Controls.Add(this.dtpInsuranceEffectiveDate);
            this.pnlPatientInsurence.Controls.Add(this.dtpSubscriberDOB);
            this.pnlPatientInsurence.Controls.Add(this.label175);
            this.pnlPatientInsurence.Controls.Add(this.label174);
            this.pnlPatientInsurence.Controls.Add(this.label173);
            this.pnlPatientInsurence.Controls.Add(this.label172);
            this.pnlPatientInsurence.Controls.Add(this.label20);
            this.pnlPatientInsurence.Controls.Add(this.label19);
            this.pnlPatientInsurence.Controls.Add(this.txtPatientRelationship);
            this.pnlPatientInsurence.Controls.Add(this.chkInsurenceIsPrimary);
            this.pnlPatientInsurence.Controls.Add(this.label93);
            this.pnlPatientInsurence.Controls.Add(this.lblSubscriberName);
            this.pnlPatientInsurence.Controls.Add(this.label94);
            this.pnlPatientInsurence.Controls.Add(this.lblGroup);
            this.pnlPatientInsurence.Controls.Add(this.lblSubscribePolicy);
            this.pnlPatientInsurence.Controls.Add(this.txtInsurenceGroup);
            this.pnlPatientInsurence.Controls.Add(this.txtInsurenceSubscriberLastName);
            this.pnlPatientInsurence.Controls.Add(this.txtInsurenceID);
            this.pnlPatientInsurence.Controls.Add(this.label97);
            this.pnlPatientInsurence.Controls.Add(this.txtPayerID);
            this.pnlPatientInsurence.Controls.Add(this.txtPayerName);
            this.pnlPatientInsurence.Controls.Add(this.label99);
            this.pnlPatientInsurence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientInsurence.Location = new System.Drawing.Point(252, 157);
            this.pnlPatientInsurence.Name = "pnlPatientInsurence";
            this.pnlPatientInsurence.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPatientInsurence.Size = new System.Drawing.Size(918, 558);
            this.pnlPatientInsurence.TabIndex = 75;
            // 
            // chkUseSecondaryInsurance
            // 
            this.chkUseSecondaryInsurance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUseSecondaryInsurance.AutoSize = true;
            this.chkUseSecondaryInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseSecondaryInsurance.Location = new System.Drawing.Point(455, 99);
            this.chkUseSecondaryInsurance.Name = "chkUseSecondaryInsurance";
            this.chkUseSecondaryInsurance.Size = new System.Drawing.Size(164, 18);
            this.chkUseSecondaryInsurance.TabIndex = 91;
            this.chkUseSecondaryInsurance.Text = "Use Secondary Insurance";
            this.chkUseSecondaryInsurance.UseVisualStyleBackColor = true;
            // 
            // label183
            // 
            this.label183.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label183.AutoEllipsis = true;
            this.label183.AutoSize = true;
            this.label183.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label183.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label183.Location = new System.Drawing.Point(557, 20);
            this.label183.Name = "label183";
            this.label183.Size = new System.Drawing.Size(84, 14);
            this.label183.TabIndex = 90;
            this.label183.Text = "Middle Name :";
            // 
            // txtSubscriberMiddleName
            // 
            this.txtSubscriberMiddleName.BackColor = System.Drawing.Color.White;
            this.txtSubscriberMiddleName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberMiddleName.ForeColor = System.Drawing.Color.Black;
            this.txtSubscriberMiddleName.Location = new System.Drawing.Point(647, 17);
            this.txtSubscriberMiddleName.Name = "txtSubscriberMiddleName";
            this.txtSubscriberMiddleName.Size = new System.Drawing.Size(111, 22);
            this.txtSubscriberMiddleName.TabIndex = 89;
            // 
            // label182
            // 
            this.label182.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label182.AutoEllipsis = true;
            this.label182.AutoSize = true;
            this.label182.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label182.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label182.Location = new System.Drawing.Point(325, 21);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(72, 14);
            this.label182.TabIndex = 88;
            this.label182.Text = "First Name :";
            // 
            // txtSubscriberFirstName
            // 
            this.txtSubscriberFirstName.BackColor = System.Drawing.Color.White;
            this.txtSubscriberFirstName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubscriberFirstName.ForeColor = System.Drawing.Color.Black;
            this.txtSubscriberFirstName.Location = new System.Drawing.Point(403, 17);
            this.txtSubscriberFirstName.Name = "txtSubscriberFirstName";
            this.txtSubscriberFirstName.Size = new System.Drawing.Size(136, 22);
            this.txtSubscriberFirstName.TabIndex = 87;
            // 
            // label167
            // 
            this.label167.AutoSize = true;
            this.label167.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label167.Location = new System.Drawing.Point(355, 285);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(71, 14);
            this.label167.TabIndex = 86;
            this.label167.Text = "( 18 - Self )";
            // 
            // dtpInsuranceExpiryDate
            // 
            this.dtpInsuranceExpiryDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpInsuranceExpiryDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpInsuranceExpiryDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpInsuranceExpiryDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpInsuranceExpiryDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpInsuranceExpiryDate.CustomFormat = "MM/dd/yyyy";
            this.dtpInsuranceExpiryDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInsuranceExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInsuranceExpiryDate.Location = new System.Drawing.Point(174, 241);
            this.dtpInsuranceExpiryDate.Name = "dtpInsuranceExpiryDate";
            this.dtpInsuranceExpiryDate.Size = new System.Drawing.Size(174, 21);
            this.dtpInsuranceExpiryDate.TabIndex = 5;
            // 
            // dtpInsuranceEffectiveDate
            // 
            this.dtpInsuranceEffectiveDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpInsuranceEffectiveDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpInsuranceEffectiveDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpInsuranceEffectiveDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpInsuranceEffectiveDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpInsuranceEffectiveDate.CustomFormat = "MM/dd/yyyy";
            this.dtpInsuranceEffectiveDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInsuranceEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInsuranceEffectiveDate.Location = new System.Drawing.Point(174, 200);
            this.dtpInsuranceEffectiveDate.Name = "dtpInsuranceEffectiveDate";
            this.dtpInsuranceEffectiveDate.Size = new System.Drawing.Size(174, 21);
            this.dtpInsuranceEffectiveDate.TabIndex = 4;
            // 
            // dtpSubscriberDOB
            // 
            this.dtpSubscriberDOB.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpSubscriberDOB.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpSubscriberDOB.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpSubscriberDOB.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpSubscriberDOB.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpSubscriberDOB.CustomFormat = "MM/dd/yyyy";
            this.dtpSubscriberDOB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSubscriberDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSubscriberDOB.Location = new System.Drawing.Point(174, 159);
            this.dtpSubscriberDOB.Name = "dtpSubscriberDOB";
            this.dtpSubscriberDOB.Size = new System.Drawing.Size(175, 21);
            this.dtpSubscriberDOB.TabIndex = 3;
            // 
            // label175
            // 
            this.label175.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label175.Dock = System.Windows.Forms.DockStyle.Right;
            this.label175.Location = new System.Drawing.Point(914, 1);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(1, 553);
            this.label175.TabIndex = 81;
            // 
            // label174
            // 
            this.label174.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label174.Dock = System.Windows.Forms.DockStyle.Left;
            this.label174.Location = new System.Drawing.Point(3, 1);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(1, 553);
            this.label174.TabIndex = 80;
            // 
            // label173
            // 
            this.label173.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label173.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label173.Location = new System.Drawing.Point(3, 554);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(912, 1);
            this.label173.TabIndex = 79;
            // 
            // label172
            // 
            this.label172.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label172.Dock = System.Windows.Forms.DockStyle.Top;
            this.label172.Location = new System.Drawing.Point(3, 0);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(912, 1);
            this.label172.TabIndex = 78;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(34, 244);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(134, 14);
            this.label20.TabIndex = 44;
            this.label20.Text = "Insurance Expiry Date :";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(18, 203);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(150, 14);
            this.label19.TabIndex = 42;
            this.label19.Text = "Insurance Effective Date :";
            // 
            // txtPatientRelationship
            // 
            this.txtPatientRelationship.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientRelationship.Location = new System.Drawing.Point(174, 281);
            this.txtPatientRelationship.MaxLength = 35;
            this.txtPatientRelationship.Name = "txtPatientRelationship";
            this.txtPatientRelationship.Size = new System.Drawing.Size(175, 22);
            this.txtPatientRelationship.TabIndex = 6;
            this.txtPatientRelationship.Text = "18";
            // 
            // chkInsurenceIsPrimary
            // 
            this.chkInsurenceIsPrimary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInsurenceIsPrimary.AutoSize = true;
            this.chkInsurenceIsPrimary.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInsurenceIsPrimary.Location = new System.Drawing.Point(175, 130);
            this.chkInsurenceIsPrimary.Name = "chkInsurenceIsPrimary";
            this.chkInsurenceIsPrimary.Size = new System.Drawing.Size(78, 18);
            this.chkInsurenceIsPrimary.TabIndex = 26;
            this.chkInsurenceIsPrimary.Text = "Is Primary";
            this.chkInsurenceIsPrimary.UseVisualStyleBackColor = true;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(46, 285);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(122, 14);
            this.label93.TabIndex = 38;
            this.label93.Text = "Patient Relationship :";
            // 
            // lblSubscriberName
            // 
            this.lblSubscriberName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscriberName.AutoEllipsis = true;
            this.lblSubscriberName.AutoSize = true;
            this.lblSubscriberName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubscriberName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscriberName.Location = new System.Drawing.Point(36, 21);
            this.lblSubscriberName.Name = "lblSubscriberName";
            this.lblSubscriberName.Size = new System.Drawing.Size(132, 14);
            this.lblSubscriberName.TabIndex = 18;
            this.lblSubscriberName.Text = "Subscriber Last Name :";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(69, 162);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(99, 14);
            this.label94.TabIndex = 40;
            this.label94.Text = "Subscriber DOB :";
            // 
            // lblGroup
            // 
            this.lblGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGroup.AutoEllipsis = true;
            this.lblGroup.AutoSize = true;
            this.lblGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroup.Location = new System.Drawing.Point(41, 103);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(127, 14);
            this.lblGroup.TabIndex = 24;
            this.lblGroup.Text = "Subscriber Group No :";
            // 
            // lblSubscribePolicy
            // 
            this.lblSubscribePolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubscribePolicy.AutoEllipsis = true;
            this.lblSubscribePolicy.AutoSize = true;
            this.lblSubscribePolicy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSubscribePolicy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubscribePolicy.Location = new System.Drawing.Point(23, 62);
            this.lblSubscribePolicy.Name = "lblSubscribePolicy";
            this.lblSubscribePolicy.Size = new System.Drawing.Size(145, 14);
            this.lblSubscribePolicy.TabIndex = 20;
            this.lblSubscribePolicy.Text = "Subscriber/Insurance ID :";
            // 
            // txtInsurenceGroup
            // 
            this.txtInsurenceGroup.BackColor = System.Drawing.Color.White;
            this.txtInsurenceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsurenceGroup.ForeColor = System.Drawing.Color.Black;
            this.txtInsurenceGroup.Location = new System.Drawing.Point(174, 99);
            this.txtInsurenceGroup.Name = "txtInsurenceGroup";
            this.txtInsurenceGroup.Size = new System.Drawing.Size(269, 22);
            this.txtInsurenceGroup.TabIndex = 2;
            // 
            // txtInsurenceSubscriberLastName
            // 
            this.txtInsurenceSubscriberLastName.BackColor = System.Drawing.Color.White;
            this.txtInsurenceSubscriberLastName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsurenceSubscriberLastName.ForeColor = System.Drawing.Color.Black;
            this.txtInsurenceSubscriberLastName.Location = new System.Drawing.Point(174, 17);
            this.txtInsurenceSubscriberLastName.Name = "txtInsurenceSubscriberLastName";
            this.txtInsurenceSubscriberLastName.Size = new System.Drawing.Size(141, 22);
            this.txtInsurenceSubscriberLastName.TabIndex = 0;
            // 
            // txtInsurenceID
            // 
            this.txtInsurenceID.BackColor = System.Drawing.Color.White;
            this.txtInsurenceID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsurenceID.ForeColor = System.Drawing.Color.Black;
            this.txtInsurenceID.Location = new System.Drawing.Point(174, 58);
            this.txtInsurenceID.Name = "txtInsurenceID";
            this.txtInsurenceID.Size = new System.Drawing.Size(269, 22);
            this.txtInsurenceID.TabIndex = 1;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label97.Location = new System.Drawing.Point(88, 326);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(80, 14);
            this.label97.TabIndex = 39;
            this.label97.Text = "Payer Name :";
            // 
            // txtPayerID
            // 
            this.txtPayerID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayerID.Location = new System.Drawing.Point(174, 363);
            this.txtPayerID.Name = "txtPayerID";
            this.txtPayerID.Size = new System.Drawing.Size(269, 22);
            this.txtPayerID.TabIndex = 8;
            // 
            // txtPayerName
            // 
            this.txtPayerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayerName.Location = new System.Drawing.Point(174, 322);
            this.txtPayerName.Name = "txtPayerName";
            this.txtPayerName.Size = new System.Drawing.Size(269, 22);
            this.txtPayerName.TabIndex = 7;
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(107, 367);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(61, 14);
            this.label99.TabIndex = 39;
            this.label99.Text = "Payer ID :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label178);
            this.panel4.Controls.Add(this.label177);
            this.panel4.Controls.Add(this.label176);
            this.panel4.Controls.Add(this.trvPatientInsurences);
            this.panel4.Controls.Add(this.label179);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 157);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel4.Size = new System.Drawing.Size(252, 558);
            this.panel4.TabIndex = 84;
            // 
            // label178
            // 
            this.label178.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label178.Dock = System.Windows.Forms.DockStyle.Left;
            this.label178.Location = new System.Drawing.Point(3, 1);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(1, 553);
            this.label178.TabIndex = 81;
            // 
            // label177
            // 
            this.label177.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label177.Dock = System.Windows.Forms.DockStyle.Top;
            this.label177.Location = new System.Drawing.Point(3, 0);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(245, 1);
            this.label177.TabIndex = 80;
            // 
            // label176
            // 
            this.label176.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label176.Dock = System.Windows.Forms.DockStyle.Right;
            this.label176.Location = new System.Drawing.Point(248, 0);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(1, 554);
            this.label176.TabIndex = 79;
            // 
            // trvPatientInsurences
            // 
            this.trvPatientInsurences.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvPatientInsurences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvPatientInsurences.Location = new System.Drawing.Point(3, 0);
            this.trvPatientInsurences.Name = "trvPatientInsurences";
            this.trvPatientInsurences.Size = new System.Drawing.Size(246, 554);
            this.trvPatientInsurences.TabIndex = 0;
            this.trvPatientInsurences.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvPatientInsurences_AfterSelect);
            // 
            // label179
            // 
            this.label179.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label179.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label179.Location = new System.Drawing.Point(3, 554);
            this.label179.Name = "label179";
            this.label179.Size = new System.Drawing.Size(246, 1);
            this.label179.TabIndex = 76;
            // 
            // pnlPatientInsuranceHeader
            // 
            this.pnlPatientInsuranceHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatientInsuranceHeader.Controls.Add(this.panel6);
            this.pnlPatientInsuranceHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatientInsuranceHeader.Location = new System.Drawing.Point(0, 129);
            this.pnlPatientInsuranceHeader.Name = "pnlPatientInsuranceHeader";
            this.pnlPatientInsuranceHeader.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPatientInsuranceHeader.Size = new System.Drawing.Size(1170, 28);
            this.pnlPatientInsuranceHeader.TabIndex = 85;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.lblPatientInsurance);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label132);
            this.panel6.Controls.Add(this.label158);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1164, 25);
            this.panel6.TabIndex = 0;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(1, 24);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(1162, 1);
            this.label21.TabIndex = 12;
            this.label21.Text = "label2";
            // 
            // lblPatientInsurance
            // 
            this.lblPatientInsurance.BackColor = System.Drawing.Color.Transparent;
            this.lblPatientInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatientInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientInsurance.ForeColor = System.Drawing.Color.White;
            this.lblPatientInsurance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPatientInsurance.Location = new System.Drawing.Point(1, 1);
            this.lblPatientInsurance.Name = "lblPatientInsurance";
            this.lblPatientInsurance.Size = new System.Drawing.Size(1162, 24);
            this.lblPatientInsurance.TabIndex = 1;
            this.lblPatientInsurance.Text = "  Patient Insurance";
            this.lblPatientInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Left;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 1);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(1, 24);
            this.label22.TabIndex = 11;
            this.label22.Text = "label4";
            // 
            // label132
            // 
            this.label132.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label132.Dock = System.Windows.Forms.DockStyle.Right;
            this.label132.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label132.Location = new System.Drawing.Point(1163, 1);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(1, 24);
            this.label132.TabIndex = 10;
            this.label132.Text = "label3";
            // 
            // label158
            // 
            this.label158.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label158.Dock = System.Windows.Forms.DockStyle.Top;
            this.label158.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label158.Location = new System.Drawing.Point(0, 0);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(1164, 1);
            this.label158.TabIndex = 9;
            this.label158.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label168);
            this.panel3.Controls.Add(this.label169);
            this.panel3.Controls.Add(this.grp_PatientDetails);
            this.panel3.Controls.Add(this.label170);
            this.panel3.Controls.Add(this.label171);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(1170, 129);
            this.panel3.TabIndex = 84;
            // 
            // label168
            // 
            this.label168.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label168.Dock = System.Windows.Forms.DockStyle.Right;
            this.label168.Location = new System.Drawing.Point(1166, 4);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(1, 121);
            this.label168.TabIndex = 79;
            // 
            // label169
            // 
            this.label169.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label169.Dock = System.Windows.Forms.DockStyle.Left;
            this.label169.Location = new System.Drawing.Point(3, 4);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(1, 121);
            this.label169.TabIndex = 78;
            // 
            // grp_PatientDetails
            // 
            this.grp_PatientDetails.Controls.Add(this.label164);
            this.grp_PatientDetails.Controls.Add(this.txtPatientGender);
            this.grp_PatientDetails.Controls.Add(this.label109);
            this.grp_PatientDetails.Controls.Add(this.label108);
            this.grp_PatientDetails.Controls.Add(this.txtPatientLName);
            this.grp_PatientDetails.Controls.Add(this.txtPatientMName);
            this.grp_PatientDetails.Controls.Add(this.txtPatientFName);
            this.grp_PatientDetails.Controls.Add(this.label95);
            this.grp_PatientDetails.Controls.Add(this.txtPatientDOB);
            this.grp_PatientDetails.Controls.Add(this.label96);
            this.grp_PatientDetails.Controls.Add(this.label98);
            this.grp_PatientDetails.Controls.Add(this.txtPatientAddress);
            this.grp_PatientDetails.Controls.Add(this.label100);
            this.grp_PatientDetails.Controls.Add(this.txtPatientCity);
            this.grp_PatientDetails.Controls.Add(this.label101);
            this.grp_PatientDetails.Controls.Add(this.txtPatientState);
            this.grp_PatientDetails.Controls.Add(this.label103);
            this.grp_PatientDetails.Controls.Add(this.txtPatientZip);
            this.grp_PatientDetails.Controls.Add(this.label105);
            this.grp_PatientDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_PatientDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_PatientDetails.Location = new System.Drawing.Point(10, 6);
            this.grp_PatientDetails.Name = "grp_PatientDetails";
            this.grp_PatientDetails.Size = new System.Drawing.Size(807, 114);
            this.grp_PatientDetails.TabIndex = 70;
            this.grp_PatientDetails.TabStop = false;
            this.grp_PatientDetails.Text = "Patient";
            // 
            // label164
            // 
            this.label164.AutoSize = true;
            this.label164.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label164.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label164.Location = new System.Drawing.Point(619, 83);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(55, 14);
            this.label164.TabIndex = 41;
            this.label164.Text = "Gender :";
            // 
            // txtPatientGender
            // 
            this.txtPatientGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtPatientGender.Location = new System.Drawing.Point(683, 79);
            this.txtPatientGender.MaxLength = 4;
            this.txtPatientGender.Name = "txtPatientGender";
            this.txtPatientGender.Size = new System.Drawing.Size(84, 22);
            this.txtPatientGender.TabIndex = 3;
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label109.Location = new System.Drawing.Point(485, 27);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(72, 14);
            this.label109.TabIndex = 39;
            this.label109.Text = "Last Name :";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label108.Location = new System.Drawing.Point(283, 27);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(84, 14);
            this.label108.TabIndex = 39;
            this.label108.Text = "Middle Name :";
            // 
            // txtPatientLName
            // 
            this.txtPatientLName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientLName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtPatientLName.Location = new System.Drawing.Point(562, 23);
            this.txtPatientLName.MaxLength = 4;
            this.txtPatientLName.Name = "txtPatientLName";
            this.txtPatientLName.Size = new System.Drawing.Size(167, 22);
            this.txtPatientLName.TabIndex = 2;
            // 
            // txtPatientMName
            // 
            this.txtPatientMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientMName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtPatientMName.Location = new System.Drawing.Point(373, 23);
            this.txtPatientMName.MaxLength = 4;
            this.txtPatientMName.Name = "txtPatientMName";
            this.txtPatientMName.Size = new System.Drawing.Size(103, 22);
            this.txtPatientMName.TabIndex = 1;
            // 
            // txtPatientFName
            // 
            this.txtPatientFName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientFName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtPatientFName.Location = new System.Drawing.Point(105, 23);
            this.txtPatientFName.MaxLength = 35;
            this.txtPatientFName.Name = "txtPatientFName";
            this.txtPatientFName.Size = new System.Drawing.Size(167, 22);
            this.txtPatientFName.TabIndex = 0;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label95.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label95.Location = new System.Drawing.Point(30, 27);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(72, 14);
            this.label95.TabIndex = 11;
            this.label95.Text = "First Name :";
            // 
            // txtPatientDOB
            // 
            this.txtPatientDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientDOB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtPatientDOB.Location = new System.Drawing.Point(105, 51);
            this.txtPatientDOB.MaxLength = 80;
            this.txtPatientDOB.Name = "txtPatientDOB";
            this.txtPatientDOB.Size = new System.Drawing.Size(167, 22);
            this.txtPatientDOB.TabIndex = 4;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label96.Location = new System.Drawing.Point(17, 55);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(85, 14);
            this.label96.TabIndex = 34;
            this.label96.Text = "Date of Birth :";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label98.Location = new System.Drawing.Point(407, 23);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(0, 14);
            this.label98.TabIndex = 9;
            // 
            // txtPatientAddress
            // 
            this.txtPatientAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtPatientAddress.Location = new System.Drawing.Point(373, 51);
            this.txtPatientAddress.Name = "txtPatientAddress";
            this.txtPatientAddress.Size = new System.Drawing.Size(394, 22);
            this.txtPatientAddress.TabIndex = 5;
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label100.Location = new System.Drawing.Point(309, 55);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(58, 14);
            this.label100.TabIndex = 24;
            this.label100.Text = "Address :";
            // 
            // txtPatientCity
            // 
            this.txtPatientCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientCity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtPatientCity.Location = new System.Drawing.Point(105, 79);
            this.txtPatientCity.Name = "txtPatientCity";
            this.txtPatientCity.Size = new System.Drawing.Size(117, 22);
            this.txtPatientCity.TabIndex = 6;
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label101.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label101.Location = new System.Drawing.Point(67, 83);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(35, 14);
            this.label101.TabIndex = 15;
            this.label101.Text = "City :";
            // 
            // txtPatientState
            // 
            this.txtPatientState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtPatientState.Location = new System.Drawing.Point(299, 79);
            this.txtPatientState.Name = "txtPatientState";
            this.txtPatientState.Size = new System.Drawing.Size(105, 22);
            this.txtPatientState.TabIndex = 7;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label103.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label103.Location = new System.Drawing.Point(248, 83);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(45, 14);
            this.label103.TabIndex = 18;
            this.label103.Text = "State :";
            // 
            // txtPatientZip
            // 
            this.txtPatientZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatientZip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.txtPatientZip.Location = new System.Drawing.Point(488, 79);
            this.txtPatientZip.Name = "txtPatientZip";
            this.txtPatientZip.Size = new System.Drawing.Size(80, 22);
            this.txtPatientZip.TabIndex = 8;
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label105.Location = new System.Drawing.Point(448, 83);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(35, 14);
            this.label105.TabIndex = 20;
            this.label105.Text = " Zip :";
            // 
            // label170
            // 
            this.label170.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label170.Dock = System.Windows.Forms.DockStyle.Top;
            this.label170.Location = new System.Drawing.Point(3, 3);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(1164, 1);
            this.label170.TabIndex = 77;
            // 
            // label171
            // 
            this.label171.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label171.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label171.Location = new System.Drawing.Point(3, 125);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(1164, 1);
            this.label171.TabIndex = 76;
            // 
            // tbpg_TransactionDetails
            // 
            this.tbpg_TransactionDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tbpg_TransactionDetails.Controls.Add(this.panel2);
            this.tbpg_TransactionDetails.Controls.Add(this.panel1);
            this.tbpg_TransactionDetails.Controls.Add(this.pnlTransactionDetailsHeader);
            this.tbpg_TransactionDetails.Location = new System.Drawing.Point(4, 23);
            this.tbpg_TransactionDetails.Name = "tbpg_TransactionDetails";
            this.tbpg_TransactionDetails.Size = new System.Drawing.Size(1170, 715);
            this.tbpg_TransactionDetails.TabIndex = 1;
            this.tbpg_TransactionDetails.Tag = "Transaction";
            this.tbpg_TransactionDetails.Text = "Transaction Details (2300)";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grp_Facility);
            this.panel2.Controls.Add(this.grp_IllnessDetails);
            this.panel2.Controls.Add(this.grpRefferingProvider);
            this.panel2.Controls.Add(this.label187);
            this.panel2.Controls.Add(this.label188);
            this.panel2.Controls.Add(this.label189);
            this.panel2.Controls.Add(this.label190);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 217);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.panel2.Size = new System.Drawing.Size(1170, 498);
            this.panel2.TabIndex = 83;
            // 
            // grp_Facility
            // 
            this.grp_Facility.Controls.Add(this.txtFacilityCode);
            this.grp_Facility.Controls.Add(this.label16);
            this.grp_Facility.Controls.Add(this.txtFacilityName);
            this.grp_Facility.Controls.Add(this.label15);
            this.grp_Facility.Controls.Add(this.txtFacilityAddress);
            this.grp_Facility.Controls.Add(this.label10);
            this.grp_Facility.Controls.Add(this.txtFacilityCity);
            this.grp_Facility.Controls.Add(this.label14);
            this.grp_Facility.Controls.Add(this.label8);
            this.grp_Facility.Controls.Add(this.txtFacilityState);
            this.grp_Facility.Controls.Add(this.txtFacilityExt);
            this.grp_Facility.Controls.Add(this.label13);
            this.grp_Facility.Controls.Add(this.label9);
            this.grp_Facility.Controls.Add(this.txtFacilityZip);
            this.grp_Facility.Controls.Add(this.txtFacilityPhone);
            this.grp_Facility.Controls.Add(this.label12);
            this.grp_Facility.Controls.Add(this.label11);
            this.grp_Facility.Controls.Add(this.txtFacilityFax);
            this.grp_Facility.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Facility.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Facility.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_Facility.Location = new System.Drawing.Point(4, 342);
            this.grp_Facility.Name = "grp_Facility";
            this.grp_Facility.Size = new System.Drawing.Size(1162, 152);
            this.grp_Facility.TabIndex = 77;
            this.grp_Facility.TabStop = false;
            this.grp_Facility.Text = "Facility";
            // 
            // txtFacilityCode
            // 
            this.txtFacilityCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityCode.Location = new System.Drawing.Point(145, 16);
            this.txtFacilityCode.Name = "txtFacilityCode";
            this.txtFacilityCode.Size = new System.Drawing.Size(181, 22);
            this.txtFacilityCode.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(59, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 14);
            this.label16.TabIndex = 54;
            this.label16.Text = "Facility Code :";
            // 
            // txtFacilityName
            // 
            this.txtFacilityName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityName.Location = new System.Drawing.Point(434, 17);
            this.txtFacilityName.MaxLength = 35;
            this.txtFacilityName.Name = "txtFacilityName";
            this.txtFacilityName.Size = new System.Drawing.Size(200, 22);
            this.txtFacilityName.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(345, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(85, 14);
            this.label15.TabIndex = 38;
            this.label15.Text = "Facility Name :";
            // 
            // txtFacilityAddress
            // 
            this.txtFacilityAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityAddress.Location = new System.Drawing.Point(771, 16);
            this.txtFacilityAddress.Name = "txtFacilityAddress";
            this.txtFacilityAddress.Size = new System.Drawing.Size(256, 22);
            this.txtFacilityAddress.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(669, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 14);
            this.label10.TabIndex = 48;
            this.label10.Text = "Facility Address :";
            // 
            // txtFacilityCity
            // 
            this.txtFacilityCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityCity.Location = new System.Drawing.Point(145, 45);
            this.txtFacilityCity.Name = "txtFacilityCity";
            this.txtFacilityCity.Size = new System.Drawing.Size(181, 22);
            this.txtFacilityCity.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(67, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 14);
            this.label14.TabIndex = 40;
            this.label14.Text = "Facility City :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(546, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 14);
            this.label8.TabIndex = 52;
            this.label8.Text = "Ext :";
            // 
            // txtFacilityState
            // 
            this.txtFacilityState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityState.Location = new System.Drawing.Point(434, 46);
            this.txtFacilityState.Name = "txtFacilityState";
            this.txtFacilityState.Size = new System.Drawing.Size(198, 22);
            this.txtFacilityState.TabIndex = 4;
            // 
            // txtFacilityExt
            // 
            this.txtFacilityExt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityExt.Location = new System.Drawing.Point(582, 76);
            this.txtFacilityExt.MaxLength = 4;
            this.txtFacilityExt.Name = "txtFacilityExt";
            this.txtFacilityExt.Size = new System.Drawing.Size(49, 22);
            this.txtFacilityExt.TabIndex = 8;
            this.txtFacilityExt.Text = "256";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(346, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 14);
            this.label13.TabIndex = 42;
            this.label13.Text = "Facility State :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(341, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 14);
            this.label9.TabIndex = 50;
            this.label9.Text = "Facility Phone :";
            // 
            // txtFacilityZip
            // 
            this.txtFacilityZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityZip.Location = new System.Drawing.Point(771, 45);
            this.txtFacilityZip.Name = "txtFacilityZip";
            this.txtFacilityZip.Size = new System.Drawing.Size(256, 22);
            this.txtFacilityZip.TabIndex = 5;
            // 
            // txtFacilityPhone
            // 
            this.txtFacilityPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityPhone.Location = new System.Drawing.Point(434, 76);
            this.txtFacilityPhone.MaxLength = 80;
            this.txtFacilityPhone.Name = "txtFacilityPhone";
            this.txtFacilityPhone.Size = new System.Drawing.Size(104, 22);
            this.txtFacilityPhone.TabIndex = 7;
            this.txtFacilityPhone.Text = "321222132";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(696, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 44;
            this.label12.Text = "Facility Zip :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(66, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 14);
            this.label11.TabIndex = 46;
            this.label11.Text = "Facility FAX :";
            // 
            // txtFacilityFax
            // 
            this.txtFacilityFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacilityFax.Location = new System.Drawing.Point(145, 75);
            this.txtFacilityFax.Name = "txtFacilityFax";
            this.txtFacilityFax.Size = new System.Drawing.Size(181, 22);
            this.txtFacilityFax.TabIndex = 6;
            this.txtFacilityFax.Text = "213215121";
            // 
            // grp_IllnessDetails
            // 
            this.grp_IllnessDetails.Controls.Add(this.dtpReturnToWork);
            this.grp_IllnessDetails.Controls.Add(this.label24);
            this.grp_IllnessDetails.Controls.Add(this.dtpLastDayWorked);
            this.grp_IllnessDetails.Controls.Add(this.label25);
            this.grp_IllnessDetails.Controls.Add(this.dtpHospitalDischargeDate);
            this.grp_IllnessDetails.Controls.Add(this.label1);
            this.grp_IllnessDetails.Controls.Add(this.dtpHospitalizationDate);
            this.grp_IllnessDetails.Controls.Add(this.label23);
            this.grp_IllnessDetails.Controls.Add(this.dtpOnsetoSimilarSyptomsorillness);
            this.grp_IllnessDetails.Controls.Add(this.label26);
            this.grp_IllnessDetails.Controls.Add(this.dtpOnsetofCurrentSymptomsorillness);
            this.grp_IllnessDetails.Controls.Add(this.label34);
            this.grp_IllnessDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_IllnessDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_IllnessDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grp_IllnessDetails.Location = new System.Drawing.Point(4, 228);
            this.grp_IllnessDetails.Name = "grp_IllnessDetails";
            this.grp_IllnessDetails.Size = new System.Drawing.Size(1162, 114);
            this.grp_IllnessDetails.TabIndex = 79;
            this.grp_IllnessDetails.TabStop = false;
            this.grp_IllnessDetails.Text = "Illness Details";
            // 
            // dtpReturnToWork
            // 
            this.dtpReturnToWork.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpReturnToWork.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpReturnToWork.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpReturnToWork.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpReturnToWork.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpReturnToWork.CustomFormat = "MM/dd/yyyy";
            this.dtpReturnToWork.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturnToWork.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReturnToWork.Location = new System.Drawing.Point(607, 81);
            this.dtpReturnToWork.Name = "dtpReturnToWork";
            this.dtpReturnToWork.Size = new System.Drawing.Size(138, 22);
            this.dtpReturnToWork.TabIndex = 5;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(465, 85);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(134, 14);
            this.label24.TabIndex = 59;
            this.label24.Text = "Return To Work Date :";
            // 
            // dtpLastDayWorked
            // 
            this.dtpLastDayWorked.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpLastDayWorked.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpLastDayWorked.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpLastDayWorked.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpLastDayWorked.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpLastDayWorked.CustomFormat = "MM/dd/yyyy";
            this.dtpLastDayWorked.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpLastDayWorked.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLastDayWorked.Location = new System.Drawing.Point(237, 85);
            this.dtpLastDayWorked.Name = "dtpLastDayWorked";
            this.dtpLastDayWorked.Size = new System.Drawing.Size(138, 22);
            this.dtpLastDayWorked.TabIndex = 4;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(92, 85);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(141, 14);
            this.label25.TabIndex = 58;
            this.label25.Text = "Last Day Of Work Date :";
            // 
            // dtpHospitalDischargeDate
            // 
            this.dtpHospitalDischargeDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpHospitalDischargeDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpHospitalDischargeDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpHospitalDischargeDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpHospitalDischargeDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpHospitalDischargeDate.CustomFormat = "MM/dd/yyyy";
            this.dtpHospitalDischargeDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHospitalDischargeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHospitalDischargeDate.Location = new System.Drawing.Point(607, 52);
            this.dtpHospitalDischargeDate.Name = "dtpHospitalDischargeDate";
            this.dtpHospitalDischargeDate.Size = new System.Drawing.Size(138, 22);
            this.dtpHospitalDischargeDate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(456, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 14);
            this.label1.TabIndex = 55;
            this.label1.Text = "Hospital Discharge Date :";
            // 
            // dtpHospitalizationDate
            // 
            this.dtpHospitalizationDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpHospitalizationDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpHospitalizationDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpHospitalizationDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpHospitalizationDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpHospitalizationDate.CustomFormat = "MM/dd/yyyy";
            this.dtpHospitalizationDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHospitalizationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHospitalizationDate.Location = new System.Drawing.Point(237, 56);
            this.dtpHospitalizationDate.Name = "dtpHospitalizationDate";
            this.dtpHospitalizationDate.Size = new System.Drawing.Size(138, 22);
            this.dtpHospitalizationDate.TabIndex = 2;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(88, 56);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(144, 14);
            this.label23.TabIndex = 54;
            this.label23.Text = "Hospital Admission Date :";
            // 
            // dtpOnsetoSimilarSyptomsorillness
            // 
            this.dtpOnsetoSimilarSyptomsorillness.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpOnsetoSimilarSyptomsorillness.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpOnsetoSimilarSyptomsorillness.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpOnsetoSimilarSyptomsorillness.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpOnsetoSimilarSyptomsorillness.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpOnsetoSimilarSyptomsorillness.CustomFormat = "MM/dd/yyyy";
            this.dtpOnsetoSimilarSyptomsorillness.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpOnsetoSimilarSyptomsorillness.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOnsetoSimilarSyptomsorillness.Location = new System.Drawing.Point(607, 23);
            this.dtpOnsetoSimilarSyptomsorillness.Name = "dtpOnsetoSimilarSyptomsorillness";
            this.dtpOnsetoSimilarSyptomsorillness.Size = new System.Drawing.Size(138, 22);
            this.dtpOnsetoSimilarSyptomsorillness.TabIndex = 1;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(389, 27);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(210, 14);
            this.label26.TabIndex = 51;
            this.label26.Text = "Onset of Similar Symptoms or illness :";
            // 
            // dtpOnsetofCurrentSymptomsorillness
            // 
            this.dtpOnsetofCurrentSymptomsorillness.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpOnsetofCurrentSymptomsorillness.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpOnsetofCurrentSymptomsorillness.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpOnsetofCurrentSymptomsorillness.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpOnsetofCurrentSymptomsorillness.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpOnsetofCurrentSymptomsorillness.CustomFormat = "MM/dd/yyyy";
            this.dtpOnsetofCurrentSymptomsorillness.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpOnsetofCurrentSymptomsorillness.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOnsetofCurrentSymptomsorillness.Location = new System.Drawing.Point(237, 27);
            this.dtpOnsetofCurrentSymptomsorillness.Name = "dtpOnsetofCurrentSymptomsorillness";
            this.dtpOnsetofCurrentSymptomsorillness.Size = new System.Drawing.Size(138, 22);
            this.dtpOnsetofCurrentSymptomsorillness.TabIndex = 0;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(14, 27);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(218, 14);
            this.label34.TabIndex = 49;
            this.label34.Text = "Onset of Current Symptoms or illness :";
            // 
            // grpRefferingProvider
            // 
            this.grpRefferingProvider.Controls.Add(this.txtREF_RefProvIdCode);
            this.grpRefferingProvider.Controls.Add(this.label181);
            this.grpRefferingProvider.Controls.Add(this.txtRefProvIDCode);
            this.grpRefferingProvider.Controls.Add(this.cmbREF_RefPrReferenceCodeQualifier);
            this.grpRefferingProvider.Controls.Add(this.label144);
            this.grpRefferingProvider.Controls.Add(this.label180);
            this.grpRefferingProvider.Controls.Add(this.cmbRefProvCodeQualifier);
            this.grpRefferingProvider.Controls.Add(this.label145);
            this.grpRefferingProvider.Controls.Add(this.label147);
            this.grpRefferingProvider.Controls.Add(this.label148);
            this.grpRefferingProvider.Controls.Add(this.txtRefProvEntityType);
            this.grpRefferingProvider.Controls.Add(this.label149);
            this.grpRefferingProvider.Controls.Add(this.label150);
            this.grpRefferingProvider.Controls.Add(this.txtRefProvEntityIDQualifier);
            this.grpRefferingProvider.Controls.Add(this.label151);
            this.grpRefferingProvider.Controls.Add(this.txtRefProv_UPIN);
            this.grpRefferingProvider.Controls.Add(this.cmbRefferingProvider);
            this.grpRefferingProvider.Controls.Add(this.label152);
            this.grpRefferingProvider.Controls.Add(this.txtRefProvAddress);
            this.grpRefferingProvider.Controls.Add(this.txtRefProvState);
            this.grpRefferingProvider.Controls.Add(this.label153);
            this.grpRefferingProvider.Controls.Add(this.label154);
            this.grpRefferingProvider.Controls.Add(this.txtRefProvCity);
            this.grpRefferingProvider.Controls.Add(this.label155);
            this.grpRefferingProvider.Controls.Add(this.txtRefProvZip);
            this.grpRefferingProvider.Controls.Add(this.label156);
            this.grpRefferingProvider.Controls.Add(this.txtRefProv_NPI_ID);
            this.grpRefferingProvider.Controls.Add(this.label157);
            this.grpRefferingProvider.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpRefferingProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRefferingProvider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.grpRefferingProvider.Location = new System.Drawing.Point(4, 2);
            this.grpRefferingProvider.Name = "grpRefferingProvider";
            this.grpRefferingProvider.Size = new System.Drawing.Size(1162, 226);
            this.grpRefferingProvider.TabIndex = 0;
            this.grpRefferingProvider.TabStop = false;
            this.grpRefferingProvider.Text = "Referring Provider";
            this.grpRefferingProvider.Visible = false;
            // 
            // txtREF_RefProvIdCode
            // 
            this.txtREF_RefProvIdCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtREF_RefProvIdCode.Location = new System.Drawing.Point(478, 191);
            this.txtREF_RefProvIdCode.Name = "txtREF_RefProvIdCode";
            this.txtREF_RefProvIdCode.Size = new System.Drawing.Size(159, 22);
            this.txtREF_RefProvIdCode.TabIndex = 12;
            this.txtREF_RefProvIdCode.Text = "12334344";
            // 
            // label181
            // 
            this.label181.AutoSize = true;
            this.label181.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label181.Location = new System.Drawing.Point(368, 195);
            this.label181.Name = "label181";
            this.label181.Size = new System.Drawing.Size(106, 13);
            this.label181.TabIndex = 73;
            this.label181.Text = "Reference ID Code :";
            // 
            // txtRefProvIDCode
            // 
            this.txtRefProvIDCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefProvIDCode.Location = new System.Drawing.Point(478, 163);
            this.txtRefProvIDCode.Name = "txtRefProvIDCode";
            this.txtRefProvIDCode.Size = new System.Drawing.Size(159, 22);
            this.txtRefProvIDCode.TabIndex = 10;
            this.txtRefProvIDCode.Text = "12332323";
            // 
            // cmbREF_RefPrReferenceCodeQualifier
            // 
            this.cmbREF_RefPrReferenceCodeQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbREF_RefPrReferenceCodeQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbREF_RefPrReferenceCodeQualifier.FormattingEnabled = true;
            this.cmbREF_RefPrReferenceCodeQualifier.Items.AddRange(new object[] {
            "24 - Employer\'s Identification Number",
            "34 - Social Security Number",
            "XX - HCFA-NPI"});
            this.cmbREF_RefPrReferenceCodeQualifier.Location = new System.Drawing.Point(180, 191);
            this.cmbREF_RefPrReferenceCodeQualifier.Name = "cmbREF_RefPrReferenceCodeQualifier";
            this.cmbREF_RefPrReferenceCodeQualifier.Size = new System.Drawing.Size(155, 22);
            this.cmbREF_RefPrReferenceCodeQualifier.TabIndex = 11;
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label144.Location = new System.Drawing.Point(356, 167);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(118, 14);
            this.label144.TabIndex = 73;
            this.label144.Text = "Identification Code :";
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label180.Location = new System.Drawing.Point(20, 194);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(150, 14);
            this.label180.TabIndex = 70;
            this.label180.Text = "Reference Code Qualifier :";
            // 
            // cmbRefProvCodeQualifier
            // 
            this.cmbRefProvCodeQualifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRefProvCodeQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRefProvCodeQualifier.FormattingEnabled = true;
            this.cmbRefProvCodeQualifier.Items.AddRange(new object[] {
            "24 - Employer\'s Identification Number",
            "34 - Social Security Number",
            "XX - HCFA-NPI"});
            this.cmbRefProvCodeQualifier.Location = new System.Drawing.Point(180, 163);
            this.cmbRefProvCodeQualifier.Name = "cmbRefProvCodeQualifier";
            this.cmbRefProvCodeQualifier.Size = new System.Drawing.Size(155, 22);
            this.cmbRefProvCodeQualifier.TabIndex = 9;
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label145.Location = new System.Drawing.Point(27, 166);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(143, 14);
            this.label145.TabIndex = 70;
            this.label145.Text = "Identifier Code Qualifier :";
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label147.Location = new System.Drawing.Point(432, 139);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(42, 14);
            this.label147.TabIndex = 74;
            this.label147.Text = "UPIN :";
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label148.Location = new System.Drawing.Point(563, 27);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(203, 14);
            this.label148.TabIndex = 67;
            this.label148.Text = "(1 - Person , 2 - Non Person Entity)";
            // 
            // txtRefProvEntityType
            // 
            this.txtRefProvEntityType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefProvEntityType.Location = new System.Drawing.Point(515, 23);
            this.txtRefProvEntityType.Name = "txtRefProvEntityType";
            this.txtRefProvEntityType.Size = new System.Drawing.Size(42, 22);
            this.txtRefProvEntityType.TabIndex = 1;
            this.txtRefProvEntityType.Tag = "";
            this.txtRefProvEntityType.Text = "1";
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label149.Location = new System.Drawing.Point(382, 26);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(126, 14);
            this.label149.TabIndex = 66;
            this.label149.Text = "Entity Type Qualifier :";
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label150.Location = new System.Drawing.Point(234, 27);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(142, 14);
            this.label150.TabIndex = 64;
            this.label150.Text = "(DN - Referring Provider)";
            // 
            // txtRefProvEntityIDQualifier
            // 
            this.txtRefProvEntityIDQualifier.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefProvEntityIDQualifier.Location = new System.Drawing.Point(180, 23);
            this.txtRefProvEntityIDQualifier.Name = "txtRefProvEntityIDQualifier";
            this.txtRefProvEntityIDQualifier.Size = new System.Drawing.Size(52, 22);
            this.txtRefProvEntityIDQualifier.TabIndex = 0;
            this.txtRefProvEntityIDQualifier.Tag = "";
            this.txtRefProvEntityIDQualifier.Text = "DN";
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label151.Location = new System.Drawing.Point(60, 26);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(110, 14);
            this.label151.TabIndex = 63;
            this.label151.Text = "Entity ID Qualifier :";
            // 
            // txtRefProv_UPIN
            // 
            this.txtRefProv_UPIN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefProv_UPIN.Location = new System.Drawing.Point(478, 135);
            this.txtRefProv_UPIN.MaxLength = 9;
            this.txtRefProv_UPIN.Name = "txtRefProv_UPIN";
            this.txtRefProv_UPIN.Size = new System.Drawing.Size(159, 22);
            this.txtRefProv_UPIN.TabIndex = 8;
            // 
            // cmbRefferingProvider
            // 
            this.cmbRefferingProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRefferingProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRefferingProvider.FormattingEnabled = true;
            this.cmbRefferingProvider.Location = new System.Drawing.Point(180, 51);
            this.cmbRefferingProvider.Name = "cmbRefferingProvider";
            this.cmbRefferingProvider.Size = new System.Drawing.Size(324, 22);
            this.cmbRefferingProvider.TabIndex = 2;
            this.cmbRefferingProvider.SelectedIndexChanged += new System.EventHandler(this.cmbRefferingProvider_SelectedIndexChanged);
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label152.Location = new System.Drawing.Point(365, 111);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(45, 14);
            this.label152.TabIndex = 60;
            this.label152.Text = "State :";
            // 
            // txtRefProvAddress
            // 
            this.txtRefProvAddress.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefProvAddress.Location = new System.Drawing.Point(180, 76);
            this.txtRefProvAddress.Multiline = true;
            this.txtRefProvAddress.Name = "txtRefProvAddress";
            this.txtRefProvAddress.Size = new System.Drawing.Size(507, 26);
            this.txtRefProvAddress.TabIndex = 3;
            // 
            // txtRefProvState
            // 
            this.txtRefProvState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefProvState.Location = new System.Drawing.Point(414, 107);
            this.txtRefProvState.MaxLength = 6;
            this.txtRefProvState.Name = "txtRefProvState";
            this.txtRefProvState.Size = new System.Drawing.Size(106, 22);
            this.txtRefProvState.TabIndex = 5;
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label153.Location = new System.Drawing.Point(112, 82);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(58, 14);
            this.label153.TabIndex = 30;
            this.label153.Text = "Address :";
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label154.Location = new System.Drawing.Point(124, 54);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(46, 14);
            this.label154.TabIndex = 1;
            this.label154.Text = "Name :";
            // 
            // txtRefProvCity
            // 
            this.txtRefProvCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefProvCity.Location = new System.Drawing.Point(180, 107);
            this.txtRefProvCity.MaxLength = 15;
            this.txtRefProvCity.Name = "txtRefProvCity";
            this.txtRefProvCity.Size = new System.Drawing.Size(113, 22);
            this.txtRefProvCity.TabIndex = 4;
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label155.Location = new System.Drawing.Point(135, 110);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(35, 14);
            this.label155.TabIndex = 3;
            this.label155.Text = "City :";
            // 
            // txtRefProvZip
            // 
            this.txtRefProvZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefProvZip.Location = new System.Drawing.Point(591, 108);
            this.txtRefProvZip.MaxLength = 4;
            this.txtRefProvZip.Name = "txtRefProvZip";
            this.txtRefProvZip.Size = new System.Drawing.Size(96, 22);
            this.txtRefProvZip.TabIndex = 6;
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label156.Location = new System.Drawing.Point(556, 112);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(31, 14);
            this.label156.TabIndex = 26;
            this.label156.Text = "Zip :";
            // 
            // txtRefProv_NPI_ID
            // 
            this.txtRefProv_NPI_ID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefProv_NPI_ID.Location = new System.Drawing.Point(180, 135);
            this.txtRefProv_NPI_ID.MaxLength = 9;
            this.txtRefProv_NPI_ID.Name = "txtRefProv_NPI_ID";
            this.txtRefProv_NPI_ID.Size = new System.Drawing.Size(155, 22);
            this.txtRefProv_NPI_ID.TabIndex = 7;
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label157.Location = new System.Drawing.Point(119, 138);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(51, 14);
            this.label157.TabIndex = 28;
            this.label157.Text = "NPI/ID :";
            // 
            // label187
            // 
            this.label187.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label187.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label187.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label187.Location = new System.Drawing.Point(4, 494);
            this.label187.Name = "label187";
            this.label187.Size = new System.Drawing.Size(1162, 1);
            this.label187.TabIndex = 83;
            this.label187.Text = "label2";
            // 
            // label188
            // 
            this.label188.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label188.Dock = System.Windows.Forms.DockStyle.Left;
            this.label188.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label188.Location = new System.Drawing.Point(3, 2);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(1, 493);
            this.label188.TabIndex = 82;
            this.label188.Text = "label4";
            // 
            // label189
            // 
            this.label189.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label189.Dock = System.Windows.Forms.DockStyle.Right;
            this.label189.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label189.Location = new System.Drawing.Point(1166, 2);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(1, 493);
            this.label189.TabIndex = 81;
            this.label189.Text = "label3";
            // 
            // label190
            // 
            this.label190.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label190.Dock = System.Windows.Forms.DockStyle.Top;
            this.label190.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label190.Location = new System.Drawing.Point(3, 1);
            this.label190.Name = "label190";
            this.label190.Size = new System.Drawing.Size(1164, 1);
            this.label190.TabIndex = 80;
            this.label190.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.c1Transaction);
            this.panel1.Controls.Add(this.label160);
            this.panel1.Controls.Add(this.label161);
            this.panel1.Controls.Add(this.label162);
            this.panel1.Controls.Add(this.label163);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel1.Size = new System.Drawing.Size(1170, 187);
            this.panel1.TabIndex = 83;
            // 
            // c1Transaction
            // 
            this.c1Transaction.AllowEditing = false;
            this.c1Transaction.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.c1Transaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1Transaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.c1Transaction.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1Transaction.ColumnInfo = "1,1,0,0,0,95,Columns:";
            this.c1Transaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1Transaction.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1Transaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1Transaction.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1Transaction.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1Transaction.Location = new System.Drawing.Point(4, 1);
            this.c1Transaction.Name = "c1Transaction";
            this.c1Transaction.Rows.Count = 1;
            this.c1Transaction.Rows.DefaultSize = 19;
            this.c1Transaction.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1Transaction.Size = new System.Drawing.Size(1162, 182);
            this.c1Transaction.StyleInfo = resources.GetString("c1Transaction.StyleInfo");
            this.c1Transaction.TabIndex = 0;
            // 
            // label160
            // 
            this.label160.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label160.Dock = System.Windows.Forms.DockStyle.Right;
            this.label160.Location = new System.Drawing.Point(1166, 1);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(1, 182);
            this.label160.TabIndex = 79;
            // 
            // label161
            // 
            this.label161.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label161.Dock = System.Windows.Forms.DockStyle.Left;
            this.label161.Location = new System.Drawing.Point(3, 1);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(1, 182);
            this.label161.TabIndex = 78;
            // 
            // label162
            // 
            this.label162.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label162.Dock = System.Windows.Forms.DockStyle.Top;
            this.label162.Location = new System.Drawing.Point(3, 0);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(1164, 1);
            this.label162.TabIndex = 77;
            // 
            // label163
            // 
            this.label163.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label163.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label163.Location = new System.Drawing.Point(3, 183);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(1164, 1);
            this.label163.TabIndex = 76;
            // 
            // pnlTransactionDetailsHeader
            // 
            this.pnlTransactionDetailsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTransactionDetailsHeader.Controls.Add(this.panel7);
            this.pnlTransactionDetailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTransactionDetailsHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTransactionDetailsHeader.Name = "pnlTransactionDetailsHeader";
            this.pnlTransactionDetailsHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTransactionDetailsHeader.Size = new System.Drawing.Size(1170, 30);
            this.pnlTransactionDetailsHeader.TabIndex = 84;
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::gloBilling.Properties.Resources.Img_Blue2007;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label159);
            this.panel7.Controls.Add(this.lblTranscationDetails);
            this.panel7.Controls.Add(this.label184);
            this.panel7.Controls.Add(this.label185);
            this.panel7.Controls.Add(this.label186);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1164, 24);
            this.panel7.TabIndex = 2;
            // 
            // label159
            // 
            this.label159.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label159.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label159.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label159.Location = new System.Drawing.Point(1, 23);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(1162, 1);
            this.label159.TabIndex = 12;
            this.label159.Text = "label2";
            // 
            // lblTranscationDetails
            // 
            this.lblTranscationDetails.BackColor = System.Drawing.Color.Transparent;
            this.lblTranscationDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTranscationDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTranscationDetails.ForeColor = System.Drawing.Color.White;
            this.lblTranscationDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTranscationDetails.Location = new System.Drawing.Point(1, 1);
            this.lblTranscationDetails.Name = "lblTranscationDetails";
            this.lblTranscationDetails.Size = new System.Drawing.Size(1162, 23);
            this.lblTranscationDetails.TabIndex = 1;
            this.lblTranscationDetails.Text = "  Transaction Details";
            this.lblTranscationDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label184
            // 
            this.label184.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label184.Dock = System.Windows.Forms.DockStyle.Left;
            this.label184.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label184.Location = new System.Drawing.Point(0, 1);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(1, 23);
            this.label184.TabIndex = 11;
            this.label184.Text = "label4";
            // 
            // label185
            // 
            this.label185.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label185.Dock = System.Windows.Forms.DockStyle.Right;
            this.label185.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label185.Location = new System.Drawing.Point(1163, 1);
            this.label185.Name = "label185";
            this.label185.Size = new System.Drawing.Size(1, 23);
            this.label185.TabIndex = 10;
            this.label185.Text = "label3";
            // 
            // label186
            // 
            this.label186.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label186.Dock = System.Windows.Forms.DockStyle.Top;
            this.label186.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label186.Location = new System.Drawing.Point(0, 0);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(1164, 1);
            this.label186.TabIndex = 9;
            this.label186.Text = "label1";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.GhostWhite;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(798, 528);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage1";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.GhostWhite;
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(798, 528);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage2";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.GhostWhite;
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(798, 528);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "tabPage1";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.GhostWhite;
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(798, 528);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "tabPage2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(15, 324);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(963, 56);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reciever";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(441, 19);
            this.textBox3.MaxLength = 80;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(200, 21);
            this.textBox3.TabIndex = 1;
            this.textBox3.Text = "B1";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(334, 23);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(105, 13);
            this.label27.TabIndex = 15;
            this.label27.Text = "Identification Code :";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(129, 19);
            this.textBox4.MaxLength = 35;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(200, 21);
            this.textBox4.TabIndex = 0;
            this.textBox4.Text = "MEDICARE";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(40, 23);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(86, 13);
            this.label28.TabIndex = 13;
            this.label28.Text = "Reciever Name :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Controls.Add(this.textBox8);
            this.groupBox2.Controls.Add(this.label33);
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.Controls.Add(this.textBox10);
            this.groupBox2.Controls.Add(this.label36);
            this.groupBox2.Controls.Add(this.textBox11);
            this.groupBox2.Controls.Add(this.label37);
            this.groupBox2.Controls.Add(this.textBox12);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.textBox13);
            this.groupBox2.Controls.Add(this.label39);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(15, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(963, 135);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Submitter";
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(130, 19);
            this.textBox5.MaxLength = 35;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(208, 21);
            this.textBox5.TabIndex = 0;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(8, 23);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(118, 13);
            this.label29.TabIndex = 11;
            this.label29.Text = "Submitter/Clinic Name :";
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(130, 54);
            this.textBox6.MaxLength = 80;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(120, 21);
            this.textBox6.TabIndex = 3;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(33, 58);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(93, 13);
            this.label30.TabIndex = 34;
            this.label30.Text = "Submitter Phone :";
            // 
            // textBox7
            // 
            this.textBox7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.Location = new System.Drawing.Point(282, 54);
            this.textBox7.MaxLength = 4;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(56, 21);
            this.textBox7.TabIndex = 18;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(253, 58);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(30, 13);
            this.label31.TabIndex = 36;
            this.label31.Text = "Ext :";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(453, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(188, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(349, 23);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(102, 13);
            this.label32.TabIndex = 9;
            this.label32.Text = "Submitter/Clinic ID :";
            // 
            // textBox8
            // 
            this.textBox8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(789, 19);
            this.textBox8.MaxLength = 60;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(144, 21);
            this.textBox8.TabIndex = 2;
            this.textBox8.Text = "Sarah Parker";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(655, 23);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(131, 13);
            this.label33.TabIndex = 13;
            this.label33.Text = "Submitter Contact Name :";
            // 
            // textBox9
            // 
            this.textBox9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.Location = new System.Drawing.Point(453, 54);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(188, 21);
            this.textBox9.TabIndex = 4;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(349, 58);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(102, 13);
            this.label35.TabIndex = 24;
            this.label35.Text = "Submitter Address :";
            // 
            // textBox10
            // 
            this.textBox10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.Location = new System.Drawing.Point(789, 54);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(144, 21);
            this.textBox10.TabIndex = 5;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(704, 58);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(82, 13);
            this.label36.TabIndex = 15;
            this.label36.Text = "Submitter City :";
            // 
            // textBox11
            // 
            this.textBox11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox11.Location = new System.Drawing.Point(130, 91);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(153, 21);
            this.textBox11.TabIndex = 6;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(37, 95);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(89, 13);
            this.label37.TabIndex = 18;
            this.label37.Text = "Submitter State :";
            // 
            // textBox12
            // 
            this.textBox12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox12.Location = new System.Drawing.Point(453, 91);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(188, 21);
            this.textBox12.TabIndex = 7;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(374, 95);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(77, 13);
            this.label38.TabIndex = 20;
            this.label38.Text = "Submitter Zip :";
            // 
            // textBox13
            // 
            this.textBox13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox13.Location = new System.Drawing.Point(789, 91);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(144, 21);
            this.textBox13.TabIndex = 8;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(704, 95);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(82, 13);
            this.label39.TabIndex = 22;
            this.label39.Text = "Submitter FAX :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox14);
            this.groupBox3.Controls.Add(this.label40);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.textBox15);
            this.groupBox3.Controls.Add(this.textBox16);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.textBox17);
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.label44);
            this.groupBox3.Controls.Add(this.textBox18);
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox3.Location = new System.Drawing.Point(15, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(963, 79);
            this.groupBox3.TabIndex = 69;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Functional Group";
            // 
            // textBox14
            // 
            this.textBox14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.Location = new System.Drawing.Point(130, 45);
            this.textBox14.MaxLength = 4;
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(120, 21);
            this.textBox14.TabIndex = 4;
            this.textBox14.Text = "1300";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(90, 49);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(36, 13);
            this.label40.TabIndex = 64;
            this.label40.Text = "Time :";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(704, 23);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(107, 13);
            this.label41.TabIndex = 62;
            this.label41.Text = "Date  [CC]YYMMDD :";
            // 
            // textBox15
            // 
            this.textBox15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox15.Location = new System.Drawing.Point(813, 19);
            this.textBox15.MaxLength = 6;
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(120, 21);
            this.textBox15.TabIndex = 3;
            this.textBox15.Text = "080515";
            // 
            // textBox16
            // 
            this.textBox16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox16.Location = new System.Drawing.Point(130, 19);
            this.textBox16.MaxLength = 2;
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new System.Drawing.Size(120, 21);
            this.textBox16.TabIndex = 0;
            this.textBox16.Text = "HC";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(476, 24);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(82, 13);
            this.label42.TabIndex = 7;
            this.label42.Text = "Receiver Dept :";
            // 
            // textBox17
            // 
            this.textBox17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox17.Location = new System.Drawing.Point(568, 20);
            this.textBox17.MaxLength = 15;
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(120, 21);
            this.textBox17.TabIndex = 2;
            this.textBox17.Text = "Rec. Department";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(259, 23);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(74, 13);
            this.label43.TabIndex = 5;
            this.label43.Text = "Sender Dept :";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(57, 23);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(69, 13);
            this.label44.TabIndex = 58;
            this.label44.Text = "Function ID :";
            // 
            // textBox18
            // 
            this.textBox18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox18.Location = new System.Drawing.Point(335, 19);
            this.textBox18.MaxLength = 15;
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(120, 21);
            this.textBox18.TabIndex = 1;
            this.textBox18.Text = "Demo Department";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label45);
            this.groupBox4.Controls.Add(this.textBox19);
            this.groupBox4.Controls.Add(this.textBox20);
            this.groupBox4.Controls.Add(this.textBox21);
            this.groupBox4.Controls.Add(this.textBox22);
            this.groupBox4.Controls.Add(this.label46);
            this.groupBox4.Controls.Add(this.label47);
            this.groupBox4.Controls.Add(this.label48);
            this.groupBox4.Controls.Add(this.textBox23);
            this.groupBox4.Controls.Add(this.label49);
            this.groupBox4.Controls.Add(this.textBox24);
            this.groupBox4.Controls.Add(this.label50);
            this.groupBox4.Controls.Add(this.textBox25);
            this.groupBox4.Controls.Add(this.label51);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox4.Location = new System.Drawing.Point(15, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(963, 88);
            this.groupBox4.TabIndex = 68;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ISA Segment";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(475, 28);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(135, 13);
            this.label45.TabIndex = 60;
            this.label45.Text = "Claim Date  [CC]YYMMDD :";
            // 
            // textBox19
            // 
            this.textBox19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox19.Location = new System.Drawing.Point(130, 24);
            this.textBox19.MaxLength = 15;
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(120, 21);
            this.textBox19.TabIndex = 0;
            this.textBox19.Text = "12345";
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(615, 53);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(120, 21);
            this.textBox20.TabIndex = 6;
            this.textBox20.Text = "12121";
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(335, 53);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(120, 21);
            this.textBox21.TabIndex = 5;
            this.textBox21.Text = "1021";
            // 
            // textBox22
            // 
            this.textBox22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox22.Location = new System.Drawing.Point(615, 24);
            this.textBox22.MaxLength = 6;
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(120, 21);
            this.textBox22.TabIndex = 2;
            this.textBox22.Text = "080515";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(254, 57);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(79, 13);
            this.label46.TabIndex = 30;
            this.label46.Text = "Claim Number :";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(64, 28);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(62, 13);
            this.label47.TabIndex = 1;
            this.label47.Text = "Sender ID :";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(532, 57);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(78, 13);
            this.label48.TabIndex = 32;
            this.label48.Text = "Reference ID :";
            // 
            // textBox23
            // 
            this.textBox23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox23.Location = new System.Drawing.Point(335, 24);
            this.textBox23.MaxLength = 15;
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(120, 21);
            this.textBox23.TabIndex = 1;
            this.textBox23.Text = "REC1001";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(263, 28);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(70, 13);
            this.label49.TabIndex = 3;
            this.label49.Text = "Receiver ID :";
            // 
            // textBox24
            // 
            this.textBox24.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox24.Location = new System.Drawing.Point(813, 24);
            this.textBox24.MaxLength = 4;
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(120, 21);
            this.textBox24.TabIndex = 3;
            this.textBox24.Text = "1300";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(747, 28);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(64, 13);
            this.label50.TabIndex = 26;
            this.label50.Text = "Claim Time :";
            // 
            // textBox25
            // 
            this.textBox25.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox25.Location = new System.Drawing.Point(130, 53);
            this.textBox25.MaxLength = 9;
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new System.Drawing.Size(120, 21);
            this.textBox25.TabIndex = 4;
            this.textBox25.Text = "987654321";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(61, 56);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(65, 13);
            this.label51.TabIndex = 28;
            this.label51.Text = "Control No :";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox26);
            this.groupBox5.Controls.Add(this.label60);
            this.groupBox5.Controls.Add(this.textBox27);
            this.groupBox5.Controls.Add(this.label61);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox5.Location = new System.Drawing.Point(3, 472);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(989, 56);
            this.groupBox5.TabIndex = 71;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Reciever";
            // 
            // textBox26
            // 
            this.textBox26.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox26.Location = new System.Drawing.Point(441, 19);
            this.textBox26.MaxLength = 80;
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(200, 21);
            this.textBox26.TabIndex = 1;
            this.textBox26.Text = "B1";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(334, 23);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(105, 13);
            this.label60.TabIndex = 15;
            this.label60.Text = "Identification Code :";
            // 
            // textBox27
            // 
            this.textBox27.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox27.Location = new System.Drawing.Point(129, 19);
            this.textBox27.MaxLength = 35;
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(200, 21);
            this.textBox27.TabIndex = 0;
            this.textBox27.Text = "MEDICARE";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(40, 23);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(86, 13);
            this.label61.TabIndex = 13;
            this.label61.Text = "Reciever Name :";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.comboBox2);
            this.groupBox6.Controls.Add(this.textBox29);
            this.groupBox6.Controls.Add(this.label62);
            this.groupBox6.Controls.Add(this.textBox30);
            this.groupBox6.Controls.Add(this.label63);
            this.groupBox6.Controls.Add(this.textBox31);
            this.groupBox6.Controls.Add(this.label64);
            this.groupBox6.Controls.Add(this.comboBox3);
            this.groupBox6.Controls.Add(this.label65);
            this.groupBox6.Controls.Add(this.textBox32);
            this.groupBox6.Controls.Add(this.label66);
            this.groupBox6.Controls.Add(this.textBox33);
            this.groupBox6.Controls.Add(this.label67);
            this.groupBox6.Controls.Add(this.textBox34);
            this.groupBox6.Controls.Add(this.label68);
            this.groupBox6.Controls.Add(this.textBox35);
            this.groupBox6.Controls.Add(this.textBox36);
            this.groupBox6.Controls.Add(this.label69);
            this.groupBox6.Controls.Add(this.label70);
            this.groupBox6.Controls.Add(this.label71);
            this.groupBox6.Controls.Add(this.textBox37);
            this.groupBox6.Controls.Add(this.label72);
            this.groupBox6.Controls.Add(this.textBox38);
            this.groupBox6.Controls.Add(this.label73);
            this.groupBox6.Controls.Add(this.textBox39);
            this.groupBox6.Controls.Add(this.label74);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox6.Location = new System.Drawing.Point(3, 301);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(989, 171);
            this.groupBox6.TabIndex = 70;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Submitter";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "ED- Electronic Data Interchange number",
            "EM- Electronic Mail",
            "FX- Facimile",
            "TE- Telephone"});
            this.comboBox2.Location = new System.Drawing.Point(453, 130);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(188, 21);
            this.comboBox2.TabIndex = 37;
            // 
            // textBox29
            // 
            this.textBox29.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox29.Location = new System.Drawing.Point(130, 19);
            this.textBox29.MaxLength = 35;
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new System.Drawing.Size(208, 21);
            this.textBox29.TabIndex = 0;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(8, 23);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(118, 13);
            this.label62.TabIndex = 11;
            this.label62.Text = "Submitter/Clinic Name :";
            // 
            // textBox30
            // 
            this.textBox30.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox30.Location = new System.Drawing.Point(130, 54);
            this.textBox30.MaxLength = 80;
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new System.Drawing.Size(120, 21);
            this.textBox30.TabIndex = 3;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(33, 58);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(93, 13);
            this.label63.TabIndex = 34;
            this.label63.Text = "Submitter Phone :";
            // 
            // textBox31
            // 
            this.textBox31.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox31.Location = new System.Drawing.Point(282, 54);
            this.textBox31.MaxLength = 4;
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new System.Drawing.Size(56, 21);
            this.textBox31.TabIndex = 18;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(253, 58);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(30, 13);
            this.label64.TabIndex = 36;
            this.label64.Text = "Ext :";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(453, 19);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(188, 21);
            this.comboBox3.TabIndex = 1;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(349, 23);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(102, 13);
            this.label65.TabIndex = 9;
            this.label65.Text = "Submitter/Clinic ID :";
            // 
            // textBox32
            // 
            this.textBox32.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox32.Location = new System.Drawing.Point(789, 19);
            this.textBox32.MaxLength = 60;
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new System.Drawing.Size(144, 21);
            this.textBox32.TabIndex = 2;
            this.textBox32.Text = "Sarah Parker";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(655, 23);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(131, 13);
            this.label66.TabIndex = 13;
            this.label66.Text = "Submitter Contact Name :";
            // 
            // textBox33
            // 
            this.textBox33.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox33.Location = new System.Drawing.Point(453, 54);
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new System.Drawing.Size(188, 21);
            this.textBox33.TabIndex = 4;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(349, 58);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(102, 13);
            this.label67.TabIndex = 24;
            this.label67.Text = "Submitter Address :";
            // 
            // textBox34
            // 
            this.textBox34.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox34.Location = new System.Drawing.Point(789, 54);
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new System.Drawing.Size(144, 21);
            this.textBox34.TabIndex = 5;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(704, 58);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(82, 13);
            this.label68.TabIndex = 15;
            this.label68.Text = "Submitter City :";
            // 
            // textBox35
            // 
            this.textBox35.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox35.Location = new System.Drawing.Point(130, 132);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new System.Drawing.Size(153, 21);
            this.textBox35.TabIndex = 6;
            // 
            // textBox36
            // 
            this.textBox36.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox36.Location = new System.Drawing.Point(130, 91);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new System.Drawing.Size(153, 21);
            this.textBox36.TabIndex = 6;
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(1, 133);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(124, 13);
            this.label69.TabIndex = 18;
            this.label69.Text = "Contact Function Code :";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(37, 95);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(89, 13);
            this.label70.TabIndex = 18;
            this.label70.Text = "Submitter State :";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(322, 132);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(129, 13);
            this.label71.TabIndex = 20;
            this.label71.Text = "Communication Qualifier :";
            // 
            // textBox37
            // 
            this.textBox37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox37.Location = new System.Drawing.Point(453, 91);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new System.Drawing.Size(188, 21);
            this.textBox37.TabIndex = 7;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(374, 95);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(77, 13);
            this.label72.TabIndex = 20;
            this.label72.Text = "Submitter Zip :";
            // 
            // textBox38
            // 
            this.textBox38.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox38.Location = new System.Drawing.Point(789, 130);
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(144, 21);
            this.textBox38.TabIndex = 8;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(660, 135);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(126, 13);
            this.label73.TabIndex = 22;
            this.label73.Text = "Communication Number :";
            // 
            // textBox39
            // 
            this.textBox39.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox39.Location = new System.Drawing.Point(789, 91);
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new System.Drawing.Size(144, 21);
            this.textBox39.TabIndex = 8;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.Location = new System.Drawing.Point(704, 95);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(82, 13);
            this.label74.TabIndex = 22;
            this.label74.Text = "Submitter FAX :";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.comboBox4);
            this.groupBox7.Controls.Add(this.comboBox5);
            this.groupBox7.Controls.Add(this.textBox40);
            this.groupBox7.Controls.Add(this.label75);
            this.groupBox7.Controls.Add(this.label76);
            this.groupBox7.Controls.Add(this.label77);
            this.groupBox7.Controls.Add(this.textBox41);
            this.groupBox7.Controls.Add(this.label78);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox7.Location = new System.Drawing.Point(3, 222);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(989, 79);
            this.groupBox7.TabIndex = 72;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "BHT";
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "CH-Chargeable",
            "RP-Reporting"});
            this.comboBox4.Location = new System.Drawing.Point(698, 52);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(200, 21);
            this.comboBox4.TabIndex = 17;
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "00-Original",
            "01-Re-issue"});
            this.comboBox5.Location = new System.Drawing.Point(189, 54);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(200, 21);
            this.comboBox5.TabIndex = 16;
            // 
            // textBox40
            // 
            this.textBox40.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox40.Location = new System.Drawing.Point(698, 23);
            this.textBox40.MaxLength = 80;
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new System.Drawing.Size(200, 21);
            this.textBox40.TabIndex = 1;
            this.textBox40.Text = "1234";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(548, 54);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(125, 13);
            this.label75.TabIndex = 15;
            this.label75.Text = "Transaction Type Code :";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.Location = new System.Drawing.Point(561, 26);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(124, 13);
            this.label76.TabIndex = 15;
            this.label76.Text = "Reference Identication :";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Location = new System.Drawing.Point(40, 50);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(96, 13);
            this.label77.TabIndex = 13;
            this.label77.Text = "TS Purpose Code :";
            // 
            // textBox41
            // 
            this.textBox41.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox41.Location = new System.Drawing.Point(189, 20);
            this.textBox41.MaxLength = 35;
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new System.Drawing.Size(200, 21);
            this.textBox41.TabIndex = 0;
            this.textBox41.Text = "0019";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.Location = new System.Drawing.Point(40, 23);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(143, 13);
            this.label78.TabIndex = 13;
            this.label78.Text = "Herarchical Structure Code :";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.textBox42);
            this.groupBox8.Controls.Add(this.label79);
            this.groupBox8.Controls.Add(this.textBox43);
            this.groupBox8.Controls.Add(this.label80);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox8.Location = new System.Drawing.Point(3, 166);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(989, 56);
            this.groupBox8.TabIndex = 72;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Transaction Set";
            // 
            // textBox42
            // 
            this.textBox42.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox42.Location = new System.Drawing.Point(626, 15);
            this.textBox42.MaxLength = 80;
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new System.Drawing.Size(200, 21);
            this.textBox42.TabIndex = 1;
            this.textBox42.Text = "54366";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.Location = new System.Drawing.Point(519, 19);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(104, 13);
            this.label79.TabIndex = 15;
            this.label79.Text = "TS Control Number :";
            // 
            // textBox43
            // 
            this.textBox43.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox43.Location = new System.Drawing.Point(166, 20);
            this.textBox43.MaxLength = 35;
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new System.Drawing.Size(200, 21);
            this.textBox43.TabIndex = 0;
            this.textBox43.Text = "837";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(40, 23);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(120, 13);
            this.label80.TabIndex = 13;
            this.label80.Text = "TS Identification Code :";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.textBox44);
            this.groupBox9.Controls.Add(this.label81);
            this.groupBox9.Controls.Add(this.label82);
            this.groupBox9.Controls.Add(this.textBox45);
            this.groupBox9.Controls.Add(this.textBox46);
            this.groupBox9.Controls.Add(this.label83);
            this.groupBox9.Controls.Add(this.textBox47);
            this.groupBox9.Controls.Add(this.label84);
            this.groupBox9.Controls.Add(this.label85);
            this.groupBox9.Controls.Add(this.textBox48);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox9.Location = new System.Drawing.Point(3, 87);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(989, 79);
            this.groupBox9.TabIndex = 69;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Functional Group";
            // 
            // textBox44
            // 
            this.textBox44.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox44.Location = new System.Drawing.Point(130, 45);
            this.textBox44.MaxLength = 4;
            this.textBox44.Name = "textBox44";
            this.textBox44.Size = new System.Drawing.Size(120, 21);
            this.textBox44.TabIndex = 4;
            this.textBox44.Text = "1300";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label81.Location = new System.Drawing.Point(90, 49);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(36, 13);
            this.label81.TabIndex = 64;
            this.label81.Text = "Time :";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.Location = new System.Drawing.Point(704, 23);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(107, 13);
            this.label82.TabIndex = 62;
            this.label82.Text = "Date  [CC]YYMMDD :";
            // 
            // textBox45
            // 
            this.textBox45.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox45.Location = new System.Drawing.Point(813, 19);
            this.textBox45.MaxLength = 6;
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new System.Drawing.Size(120, 21);
            this.textBox45.TabIndex = 3;
            this.textBox45.Text = "080515";
            // 
            // textBox46
            // 
            this.textBox46.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox46.Location = new System.Drawing.Point(130, 19);
            this.textBox46.MaxLength = 2;
            this.textBox46.Name = "textBox46";
            this.textBox46.ReadOnly = true;
            this.textBox46.Size = new System.Drawing.Size(120, 21);
            this.textBox46.TabIndex = 0;
            this.textBox46.Text = "HC";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label83.Location = new System.Drawing.Point(476, 24);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(82, 13);
            this.label83.TabIndex = 7;
            this.label83.Text = "Receiver Dept :";
            // 
            // textBox47
            // 
            this.textBox47.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox47.Location = new System.Drawing.Point(568, 20);
            this.textBox47.MaxLength = 15;
            this.textBox47.Name = "textBox47";
            this.textBox47.Size = new System.Drawing.Size(120, 21);
            this.textBox47.TabIndex = 2;
            this.textBox47.Text = "Rec. Department";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.Location = new System.Drawing.Point(259, 23);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(74, 13);
            this.label84.TabIndex = 5;
            this.label84.Text = "Sender Dept :";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(57, 23);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(69, 13);
            this.label85.TabIndex = 58;
            this.label85.Text = "Function ID :";
            // 
            // textBox48
            // 
            this.textBox48.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox48.Location = new System.Drawing.Point(335, 19);
            this.textBox48.MaxLength = 15;
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new System.Drawing.Size(120, 21);
            this.textBox48.TabIndex = 1;
            this.textBox48.Text = "Demo Department";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label86);
            this.groupBox10.Controls.Add(this.textBox49);
            this.groupBox10.Controls.Add(this.textBox50);
            this.groupBox10.Controls.Add(this.textBox51);
            this.groupBox10.Controls.Add(this.textBox52);
            this.groupBox10.Controls.Add(this.label87);
            this.groupBox10.Controls.Add(this.label88);
            this.groupBox10.Controls.Add(this.label89);
            this.groupBox10.Controls.Add(this.textBox53);
            this.groupBox10.Controls.Add(this.label90);
            this.groupBox10.Controls.Add(this.textBox54);
            this.groupBox10.Controls.Add(this.label91);
            this.groupBox10.Controls.Add(this.textBox55);
            this.groupBox10.Controls.Add(this.label92);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox10.Location = new System.Drawing.Point(3, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(989, 84);
            this.groupBox10.TabIndex = 68;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "ISA Segment";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(475, 28);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(135, 13);
            this.label86.TabIndex = 60;
            this.label86.Text = "Claim Date  [CC]YYMMDD :";
            // 
            // textBox49
            // 
            this.textBox49.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox49.Location = new System.Drawing.Point(130, 24);
            this.textBox49.MaxLength = 15;
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new System.Drawing.Size(120, 21);
            this.textBox49.TabIndex = 0;
            this.textBox49.Text = "12345";
            // 
            // textBox50
            // 
            this.textBox50.Location = new System.Drawing.Point(615, 53);
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new System.Drawing.Size(120, 21);
            this.textBox50.TabIndex = 6;
            this.textBox50.Text = "12121";
            // 
            // textBox51
            // 
            this.textBox51.Location = new System.Drawing.Point(335, 53);
            this.textBox51.Name = "textBox51";
            this.textBox51.Size = new System.Drawing.Size(120, 21);
            this.textBox51.TabIndex = 5;
            this.textBox51.Text = "1021";
            // 
            // textBox52
            // 
            this.textBox52.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox52.Location = new System.Drawing.Point(615, 24);
            this.textBox52.MaxLength = 6;
            this.textBox52.Name = "textBox52";
            this.textBox52.Size = new System.Drawing.Size(120, 21);
            this.textBox52.TabIndex = 2;
            this.textBox52.Text = "080515";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label87.Location = new System.Drawing.Point(254, 57);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(79, 13);
            this.label87.TabIndex = 30;
            this.label87.Text = "Claim Number :";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(64, 28);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(62, 13);
            this.label88.TabIndex = 1;
            this.label88.Text = "Sender ID :";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(532, 57);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(78, 13);
            this.label89.TabIndex = 32;
            this.label89.Text = "Reference ID :";
            // 
            // textBox53
            // 
            this.textBox53.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox53.Location = new System.Drawing.Point(335, 24);
            this.textBox53.MaxLength = 15;
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new System.Drawing.Size(120, 21);
            this.textBox53.TabIndex = 1;
            this.textBox53.Text = "REC1001";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(263, 28);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(70, 13);
            this.label90.TabIndex = 3;
            this.label90.Text = "Receiver ID :";
            // 
            // textBox54
            // 
            this.textBox54.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox54.Location = new System.Drawing.Point(813, 24);
            this.textBox54.MaxLength = 4;
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new System.Drawing.Size(120, 21);
            this.textBox54.TabIndex = 3;
            this.textBox54.Text = "1300";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(747, 28);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(64, 13);
            this.label91.TabIndex = 26;
            this.label91.Text = "Claim Time :";
            // 
            // textBox55
            // 
            this.textBox55.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox55.Location = new System.Drawing.Point(130, 53);
            this.textBox55.MaxLength = 9;
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new System.Drawing.Size(120, 21);
            this.textBox55.TabIndex = 4;
            this.textBox55.Text = "987654321";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.Location = new System.Drawing.Point(61, 56);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(65, 13);
            this.label92.TabIndex = 28;
            this.label92.Text = "Control No :";
            // 
            // frmSetupEDIData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1178, 795);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ts_Commands);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetupEDIData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EDI Data";
            this.Load += new System.EventHandler(this.frmSetupEDIData_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSetupEDIData_FormClosed);
            this.ts_Commands.ResumeLayout(false);
            this.ts_Commands.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.tb_EDIDataDetails.ResumeLayout(false);
            this.tbpg_EDIDetails.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Transaction1)).EndInit();
            this.grp_Receiver.ResumeLayout(false);
            this.grp_Receiver.PerformLayout();
            this.grp_Submitter.ResumeLayout(false);
            this.grp_Submitter.PerformLayout();
            this.grp_BHT.ResumeLayout(false);
            this.grp_BHT.PerformLayout();
            this.grp_TransactionSet.ResumeLayout(false);
            this.grp_TransactionSet.PerformLayout();
            this.grp_FunctionalGroup.ResumeLayout(false);
            this.grp_FunctionalGroup.PerformLayout();
            this.grp_ISASegment.ResumeLayout(false);
            this.grp_ISASegment.PerformLayout();
            this.tbpg_BillingProvider.ResumeLayout(false);
            this.grp_REF.ResumeLayout(false);
            this.grp_REF.PerformLayout();
            this.grp_PayToProvider.ResumeLayout(false);
            this.grp_PayToProvider.PerformLayout();
            this.grp_BillingProvider.ResumeLayout(false);
            this.grp_Address.ResumeLayout(false);
            this.grp_Address.PerformLayout();
            this.tbpg_PatientDetails.ResumeLayout(false);
            this.pnlPatientInsurence.ResumeLayout(false);
            this.pnlPatientInsurence.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.pnlPatientInsuranceHeader.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.grp_PatientDetails.ResumeLayout(false);
            this.grp_PatientDetails.PerformLayout();
            this.tbpg_TransactionDetails.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.grp_Facility.ResumeLayout(false);
            this.grp_Facility.PerformLayout();
            this.grp_IllnessDetails.ResumeLayout(false);
            this.grp_IllnessDetails.PerformLayout();
            this.grpRefferingProvider.ResumeLayout(false);
            this.grpRefferingProvider.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1Transaction)).EndInit();
            this.pnlTransactionDetailsHeader.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal gloGlobal.gloToolStripIgnoreFocus ts_Commands;
        internal System.Windows.Forms.ToolStripButton tsb_OK;
        internal System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabControl tb_EDIDataDetails;
        private System.Windows.Forms.TabPage tbpg_EDIDetails;
        private System.Windows.Forms.TabPage tbpg_TransactionDetails;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox grp_Receiver;
        internal System.Windows.Forms.TextBox txtRecieverIdentificationCode;
        private System.Windows.Forms.Label label18;
        internal System.Windows.Forms.TextBox txtRecieverName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox grp_Submitter;
        private System.Windows.Forms.Label lblSubmitterName;
        internal System.Windows.Forms.TextBox txtSubmitterPhone;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtSubmitterExt;
        private System.Windows.Forms.Label lblEXT;
        internal System.Windows.Forms.TextBox txtSubmitterContactName;
        private System.Windows.Forms.Label lblSubmitterContactName;
        internal System.Windows.Forms.TextBox txtSubmitterAddress;
        private System.Windows.Forms.Label lblSubmitterAddress;
        internal System.Windows.Forms.TextBox txtSubmitterCity;
        private System.Windows.Forms.Label lblSubmitterCity;
        internal System.Windows.Forms.TextBox txtSubmitterState;
        private System.Windows.Forms.Label lblSubmitterState;
        internal System.Windows.Forms.TextBox txtSubmitterZip;
        private System.Windows.Forms.Label lblSubmitterZIP;
        internal System.Windows.Forms.TextBox txtSubmitterFax;
        private System.Windows.Forms.Label lblSubmitterFAX;
        private System.Windows.Forms.GroupBox grp_FunctionalGroup;
        internal System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.TextBox txtFunctionID;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtReceiverDept;
        private System.Windows.Forms.Label lblSenderDept;
        private System.Windows.Forms.Label lblFunctionID;
        internal System.Windows.Forms.TextBox txtSenderDept;
        private System.Windows.Forms.GroupBox grp_ISASegment;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtSenderID;
        internal System.Windows.Forms.TextBox txtReferenceID;
        internal System.Windows.Forms.TextBox txtClaimNo;
        internal System.Windows.Forms.TextBox txtClaimDate;
        private System.Windows.Forms.Label lblClaimNumber;
        private System.Windows.Forms.Label lblSenderID;
        private System.Windows.Forms.Label lblReferenceID;
        internal System.Windows.Forms.TextBox txtReceiverID;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtClaimTime;
        private System.Windows.Forms.Label lblClaimTime;
        internal System.Windows.Forms.TextBox txtControlNo;
        private System.Windows.Forms.Label lblControlNo;
        internal System.Windows.Forms.ToolStripButton tlb_btnGenerateEDI;
        private System.Windows.Forms.GroupBox grp_Facility;
        internal System.Windows.Forms.TextBox txtFacilityCode;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.TextBox txtFacilityName;
        private System.Windows.Forms.Label label15;
        internal System.Windows.Forms.TextBox txtFacilityAddress;
        private System.Windows.Forms.Label label10;
        internal System.Windows.Forms.TextBox txtFacilityCity;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.TextBox txtFacilityState;
        internal System.Windows.Forms.TextBox txtFacilityExt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.TextBox txtFacilityZip;
        internal System.Windows.Forms.TextBox txtFacilityPhone;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txtFacilityFax;
        private System.Windows.Forms.GroupBox grp_IllnessDetails;
        internal System.Windows.Forms.DateTimePicker dtpReturnToWork;
        private System.Windows.Forms.Label label24;
        internal System.Windows.Forms.DateTimePicker dtpLastDayWorked;
        private System.Windows.Forms.Label label25;
        internal System.Windows.Forms.DateTimePicker dtpHospitalDischargeDate;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.DateTimePicker dtpHospitalizationDate;
        private System.Windows.Forms.Label label23;
        internal System.Windows.Forms.DateTimePicker dtpOnsetoSimilarSyptomsorillness;
        private System.Windows.Forms.Label label26;
        internal System.Windows.Forms.DateTimePicker dtpOnsetofCurrentSymptomsorillness;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label27;
        internal System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label29;
        internal System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label30;
        internal System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label31;
        internal System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label32;
        internal System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label33;
        internal System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label35;
        internal System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label36;
        internal System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label37;
        internal System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label38;
        internal System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        internal System.Windows.Forms.TextBox textBox15;
        internal System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.Label label42;
        internal System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        internal System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label45;
        internal System.Windows.Forms.TextBox textBox19;
        internal System.Windows.Forms.TextBox textBox20;
        internal System.Windows.Forms.TextBox textBox21;
        internal System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        internal System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.Label label49;
        internal System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.Label label50;
        internal System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.GroupBox grp_TransactionSet;
        internal System.Windows.Forms.TextBox txtTSControlNumber;
        private System.Windows.Forms.Label lblTSControlNumber;
        internal System.Windows.Forms.TextBox txtTSIdCode;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.GroupBox grp_BHT;
        internal System.Windows.Forms.TextBox txtBHTRefIdentification;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label55;
        internal System.Windows.Forms.TextBox txtBHT_HerarchicalStrCode;
        private System.Windows.Forms.Label label54;
        internal System.Windows.Forms.ComboBox cmbBHT_TSPurposeCode;
        internal System.Windows.Forms.ComboBox cmbBHT_TSTypeCode;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        internal System.Windows.Forms.ComboBox cmb_CommunicationQualifier;
        internal System.Windows.Forms.TextBox txtCommunicationNumber;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.TabPage tbpg_PatientDetails;
        private System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.Label label60;
        internal System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.GroupBox groupBox6;
        internal System.Windows.Forms.ComboBox comboBox2;
        internal System.Windows.Forms.TextBox textBox29;
        private System.Windows.Forms.Label label62;
        internal System.Windows.Forms.TextBox textBox30;
        private System.Windows.Forms.Label label63;
        internal System.Windows.Forms.TextBox textBox31;
        private System.Windows.Forms.Label label64;
        internal System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label65;
        internal System.Windows.Forms.TextBox textBox32;
        private System.Windows.Forms.Label label66;
        internal System.Windows.Forms.TextBox textBox33;
        private System.Windows.Forms.Label label67;
        internal System.Windows.Forms.TextBox textBox34;
        private System.Windows.Forms.Label label68;
        internal System.Windows.Forms.TextBox textBox35;
        internal System.Windows.Forms.TextBox textBox36;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label71;
        internal System.Windows.Forms.TextBox textBox37;
        private System.Windows.Forms.Label label72;
        internal System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.Label label73;
        internal System.Windows.Forms.TextBox textBox39;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.GroupBox groupBox7;
        internal System.Windows.Forms.ComboBox comboBox4;
        internal System.Windows.Forms.ComboBox comboBox5;
        internal System.Windows.Forms.TextBox textBox40;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        internal System.Windows.Forms.TextBox textBox41;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.GroupBox groupBox8;
        internal System.Windows.Forms.TextBox textBox42;
        private System.Windows.Forms.Label label79;
        internal System.Windows.Forms.TextBox textBox43;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.GroupBox groupBox9;
        internal System.Windows.Forms.TextBox textBox44;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label82;
        internal System.Windows.Forms.TextBox textBox45;
        internal System.Windows.Forms.TextBox textBox46;
        private System.Windows.Forms.Label label83;
        internal System.Windows.Forms.TextBox textBox47;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label85;
        internal System.Windows.Forms.TextBox textBox48;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label86;
        internal System.Windows.Forms.TextBox textBox49;
        internal System.Windows.Forms.TextBox textBox50;
        internal System.Windows.Forms.TextBox textBox51;
        internal System.Windows.Forms.TextBox textBox52;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label89;
        internal System.Windows.Forms.TextBox textBox53;
        private System.Windows.Forms.Label label90;
        internal System.Windows.Forms.TextBox textBox54;
        private System.Windows.Forms.Label label91;
        internal System.Windows.Forms.TextBox textBox55;
        private System.Windows.Forms.Label label92;
        internal System.Windows.Forms.TextBox txtPatientRelationship;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label94;
        internal System.Windows.Forms.TextBox txtPayerID;
        private System.Windows.Forms.Label label99;
        internal System.Windows.Forms.TextBox txtPayerName;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.TabPage tbpg_BillingProvider;
        private System.Windows.Forms.GroupBox grp_PayToProvider;
        private System.Windows.Forms.Label label118;
        internal System.Windows.Forms.TextBox txtPTPAddress;
        internal System.Windows.Forms.TextBox txtPTPState;
        private System.Windows.Forms.Label label122;
        internal System.Windows.Forms.TextBox txtPTPCity;
        private System.Windows.Forms.Label label127;
        internal System.Windows.Forms.TextBox txtPTPZip;
        private System.Windows.Forms.Label label128;
        internal System.Windows.Forms.TextBox txtPTPNPI_ID;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.GroupBox grp_BillingProvider;
        private System.Windows.Forms.Label label119;
        internal System.Windows.Forms.TextBox txtBLProviderAddress;
        internal System.Windows.Forms.TextBox txtBLProviderState;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.Label label121;
        internal System.Windows.Forms.TextBox txtBLProviderCity;
        private System.Windows.Forms.Label label123;
        internal System.Windows.Forms.TextBox txtBLProviderZip;
        private System.Windows.Forms.Label label124;
        internal System.Windows.Forms.TextBox txtBLProvNPI_ID;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.GroupBox grp_PatientDetails;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label label108;
        internal System.Windows.Forms.TextBox txtPatientLName;
        internal System.Windows.Forms.TextBox txtPatientMName;
        internal System.Windows.Forms.TextBox txtPatientFName;
        private System.Windows.Forms.Label label95;
        internal System.Windows.Forms.TextBox txtPatientDOB;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label98;
        internal System.Windows.Forms.TextBox txtPatientAddress;
        private System.Windows.Forms.Label label100;
        internal System.Windows.Forms.TextBox txtPatientCity;
        private System.Windows.Forms.Label label101;
        internal System.Windows.Forms.TextBox txtPatientState;
        private System.Windows.Forms.Label label103;
        internal System.Windows.Forms.TextBox txtPatientZip;
        private System.Windows.Forms.Label label105;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Transaction;
        private C1.Win.C1FlexGrid.C1FlexGrid c1Transaction1;
        private System.Windows.Forms.Panel pnlPatientInsurence;
        private System.Windows.Forms.CheckBox chkInsurenceIsPrimary;
        private System.Windows.Forms.Label lblSubscriberName;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblSubscribePolicy;
        private System.Windows.Forms.TextBox txtInsurenceGroup;
        private System.Windows.Forms.TextBox txtInsurenceSubscriberLastName;
        private System.Windows.Forms.TextBox txtInsurenceID;
        private System.Windows.Forms.TreeView trvPatientInsurences;
        internal System.Windows.Forms.ComboBox cmbClinic;
        internal System.Windows.Forms.TextBox txtSubmitterContactPersonPhone;
        private System.Windows.Forms.Label label102;
        internal System.Windows.Forms.TextBox txtSubmitterContactPersonPhoneExt;
        private System.Windows.Forms.Label label104;
        internal System.Windows.Forms.TextBox txtSubmitterAdd2;
        private System.Windows.Forms.Label label106;
        internal System.Windows.Forms.ComboBox cmbContactFunctionCode;
        internal System.Windows.Forms.TextBox txtSubIdentificationCode;
        private System.Windows.Forms.Label lblIdentifierCode;
        internal System.Windows.Forms.ComboBox cmbSubmitterIdentifierCodeQualifier;
        private System.Windows.Forms.Label lblSubmitterIdCode;
        internal System.Windows.Forms.TextBox txtSubEntityIDQualifier;
        private System.Windows.Forms.Label lblEntityIDQualifier;
        private System.Windows.Forms.Label label110;
        internal System.Windows.Forms.TextBox txtSubEntityTypeQualifier;
        private System.Windows.Forms.Label lblEntityType;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label label111;
        internal System.Windows.Forms.TextBox txtRecEntityTypeQualifier;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label113;
        internal System.Windows.Forms.TextBox txtEntityIDQualRec;
        private System.Windows.Forms.Label label114;
        internal System.Windows.Forms.ComboBox cmbRecIDCodeQual;
        private System.Windows.Forms.Label label115;
        internal System.Windows.Forms.ComboBox cmbBillingProvider;
        private System.Windows.Forms.Label label116;
        internal System.Windows.Forms.TextBox txtBLProvEntityType;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label130;
        internal System.Windows.Forms.TextBox txtBLProvEntitiyIDQual;
        private System.Windows.Forms.Label label131;
        internal System.Windows.Forms.TextBox txtBLProviderUPIN;
        internal System.Windows.Forms.TextBox txtBLIdentificationCode;
        private System.Windows.Forms.Label label133;
        internal System.Windows.Forms.ComboBox cmbBLCodeQualifier;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.Label label135;
        internal System.Windows.Forms.TextBox txtPTPType;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Label label137;
        internal System.Windows.Forms.TextBox txtPTPIDQualifier;
        private System.Windows.Forms.Label label138;
        private System.Windows.Forms.Label label139;
        internal System.Windows.Forms.ComboBox cmbPTPName;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.Label label143;
        internal System.Windows.Forms.TextBox txtPTPIDCode;
        internal System.Windows.Forms.TextBox txtPTPUPIN;
        private System.Windows.Forms.Label label126;
        internal System.Windows.Forms.ComboBox cmbPTPIDCodeQualifier;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.GroupBox grpRefferingProvider;
        internal System.Windows.Forms.TextBox txtRefProvIDCode;
        private System.Windows.Forms.Label label144;
        internal System.Windows.Forms.ComboBox cmbRefProvCodeQualifier;
        private System.Windows.Forms.Label label145;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.Label label148;
        internal System.Windows.Forms.TextBox txtRefProvEntityType;
        private System.Windows.Forms.Label label149;
        private System.Windows.Forms.Label label150;
        internal System.Windows.Forms.TextBox txtRefProvEntityIDQualifier;
        private System.Windows.Forms.Label label151;
        internal System.Windows.Forms.TextBox txtRefProv_UPIN;
        internal System.Windows.Forms.ComboBox cmbRefferingProvider;
        private System.Windows.Forms.Label label152;
        internal System.Windows.Forms.TextBox txtRefProvAddress;
        internal System.Windows.Forms.TextBox txtRefProvState;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.Label label154;
        internal System.Windows.Forms.TextBox txtRefProvCity;
        private System.Windows.Forms.Label label155;
        internal System.Windows.Forms.TextBox txtRefProvZip;
        private System.Windows.Forms.Label label156;
        internal System.Windows.Forms.TextBox txtRefProv_NPI_ID;
        private System.Windows.Forms.Label label157;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label160;
        private System.Windows.Forms.Label label161;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.Label label163;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label168;
        private System.Windows.Forms.Label label169;
        private System.Windows.Forms.Label label170;
        private System.Windows.Forms.Label label171;
        private System.Windows.Forms.Label label175;
        private System.Windows.Forms.Label label174;
        private System.Windows.Forms.Label label173;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.Label label179;
        private System.Windows.Forms.Panel pnlTransactionDetailsHeader;
        private System.Windows.Forms.Label lblTranscationDetails;
        private System.Windows.Forms.Panel pnlPatientInsuranceHeader;
        private System.Windows.Forms.Label lblPatientInsurance;
        private System.Windows.Forms.Label label178;
        private System.Windows.Forms.Label label177;
        private System.Windows.Forms.GroupBox grp_REF;
        internal System.Windows.Forms.TextBox txtREFIdentificationRefNo;
        private System.Windows.Forms.Label label165;
        internal System.Windows.Forms.ComboBox cmbREF_ReferenceIdCode;
        private System.Windows.Forms.Label label166;
        private System.Windows.Forms.Label label164;
        internal System.Windows.Forms.TextBox txtPatientGender;
        internal System.Windows.Forms.TextBox txtREF_RefProvIdCode;
        private System.Windows.Forms.Label label181;
        internal System.Windows.Forms.ComboBox cmbREF_RefPrReferenceCodeQualifier;
        private System.Windows.Forms.Label label180;
        private System.Windows.Forms.Label label167;
        internal System.Windows.Forms.DateTimePicker dtpInsuranceExpiryDate;
        internal System.Windows.Forms.DateTimePicker dtpInsuranceEffectiveDate;
        internal System.Windows.Forms.DateTimePicker dtpSubscriberDOB;
        internal System.Windows.Forms.ToolStripButton tsb_ShowHCFA1500;
        private System.Windows.Forms.Label label183;
        private System.Windows.Forms.TextBox txtSubscriberMiddleName;
        private System.Windows.Forms.Label label182;
        private System.Windows.Forms.TextBox txtSubscriberFirstName;
        private System.Windows.Forms.CheckBox chkUseSecondaryInsurance;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbl_BottomBrd;
        private System.Windows.Forms.Label lbl_LeftBrd;
        private System.Windows.Forms.Label lbl_RightBrd;
        private System.Windows.Forms.Label lbl_TopBrd;
        private System.Windows.Forms.GroupBox grp_Address;
        private System.Windows.Forms.RadioButton rdb_Company;
        private System.Windows.Forms.RadioButton rdb_Practice;
        private System.Windows.Forms.RadioButton rdb_Business;
        internal System.Windows.Forms.ToolStripButton tls_btnValidate;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label158;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label159;
        private System.Windows.Forms.Label label184;
        private System.Windows.Forms.Label label185;
        private System.Windows.Forms.Label label186;
        private System.Windows.Forms.Label label187;
        private System.Windows.Forms.Label label188;
        private System.Windows.Forms.Label label189;
        private System.Windows.Forms.Label label190;
        internal System.Windows.Forms.ToolStripButton ToolStripButton1;
    }
}