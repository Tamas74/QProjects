namespace gloPatient
{
    partial class gloPatientDemographicsControl
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
            System.Windows.Forms.DateTimePicker[] dtpControls = { dtpBirth };
            System.Windows.Forms.Control[] cntControls = { dtpBirth };
            if (disposing && (components != null))
            {

                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    if (dlgPhotoBrowser != null)
                    {
                         
                        dlgPhotoBrowser.Dispose();
                        dlgPhotoBrowser = null;
                    }
                }
                catch
                {
                }
                //try
                //{
                //    if (colorDialog1 != null)
                //    {

                //        colorDialog1.Dispose();
                //        colorDialog1 = null;
                //    }
                //}
                //catch
                //{
                //}
                if (oPatientDemo != null) //added for bugid 71512
                {
                    oPatientDemo.Dispose();
                    oPatientDemo = null;
                }
                if (oListControl != null)
                {
                    oListControl.Dispose();
                    oListControl = null;
                }
               
                if (_PatientGuardian != null)
                {
                    _PatientGuardian.Dispose();
                    _PatientGuardian = null;
                }
                if (_PatientOccupation != null)
                {
                    _PatientOccupation.Dispose();
                    _PatientOccupation= null;
                }
                if (_PatientInsurance != null)
                {
                    _PatientInsurance.Dispose();
                    _PatientInsurance= null;
                }

                //Other Details 
                if (_PatientDemographicOtherInfo != null)
                {
                 
                    _PatientDemographicOtherInfo.Dispose();
                    _PatientDemographicOtherInfo= null;
                }
                if (_PatientWorkersComp != null)
                {
                    _PatientWorkersComp.Clear();
                    _PatientWorkersComp.Dispose();
                    _PatientWorkersComp = null;
                }
                //
                if (oPatientPharmacies != null)
                {
                    oPatientPharmacies.Clear();
                    oPatientPharmacies.Dispose();
                    oPatientPharmacies = null;
                }
                if (oPatientReferrals != null)
                {
                    oPatientReferrals.Clear();
                    oPatientReferrals.Dispose();
                    oPatientReferrals = null;
                }
                if (oPatientCareTeam != null)
                {
                    oPatientCareTeam.Clear();
                    oPatientCareTeam.Dispose();
                    oPatientCareTeam = null;
                }
                if (oPrimaryCarePhysicians != null)
                {
                    oPrimaryCarePhysicians.Clear();
                    oPrimaryCarePhysicians.Dispose();
                    oPrimaryCarePhysicians = null;
                }
                if (oPatientGuarantors != null)
                {
                    oPatientGuarantors.Clear();
                    oPatientGuarantors.Dispose();
                    oPatientGuarantors = null;
                    
                }
                if (oAccount != null)
                {
                    oAccount.Dispose();
                    oAccount = null;
                }
                if (oPatientAccount != null)
                {
                    oPatientAccount.Dispose();
                    oPatientAccount= null;
                }
                if (oPatientAccounts != null)
                {
                    oPatientAccounts.Clear();
                    oPatientAccounts.Dispose();
                    oPatientAccounts=null;
                }
                if (objgloAccount != null)
                {
                    objgloAccount.Dispose();
                    objgloAccount = null;
                }
                if (oPatientOtherGuarantors != null)
                {
                    oPatientOtherGuarantors.Clear();
                    oPatientOtherGuarantors.Dispose();
                    oPatientOtherGuarantors= null;
                }
                if (oPatientRepresentatives != null)
                {
                    oPatientRepresentatives.Clear();
                    oPatientRepresentatives.Dispose();
                    oPatientRepresentatives = null;
                }
                if (oAPIRepresentatives != null)
                {
                    oAPIRepresentatives.Clear();
                    oAPIRepresentatives.Dispose();
                    oAPIRepresentatives = null;
                }
                if (oToolTip != null)
                {
                    oToolTip.Dispose();
                    oToolTip= null;
                }
                if (_deletedInsurances != null)
                {
                    _deletedInsurances.Clear();
                    _deletedInsurances = null;
                }
                 components.Dispose();
                
                //ogloPatientGuarantor.Dispose();
                //ogloPatientGuardian.Dispose();
                //ogloPatientInsuranceControl.Dispose();
                //ogloPatientOccupation.Dispose(); 
            }
            base.Dispose(disposing);

 

            if (dtpControls != null)
            {
                if (dtpControls.Length > 0)
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ref dtpControls);

                }
            }

            if (cntControls != null)
            {
                if (cntControls.Length > 0)
                {
                    gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                }
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gloPatientDemographicsControl));
            this.pnlDemographicInfo = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlPatientOtherGuarantorInfo = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbOtherGuarantor = new System.Windows.Forms.ComboBox();
            this.btnotherNewGuarantor = new System.Windows.Forms.Button();
            this.btnOtherGuarantorExistingPatientBrowse = new System.Windows.Forms.Button();
            this.pnlBusinessCenter = new System.Windows.Forms.Panel();
            this.lblBusinessCenter = new System.Windows.Forms.Label();
            this.cmbBusinessCenter = new System.Windows.Forms.ComboBox();
            this.lblBusinessCenterCpt = new System.Windows.Forms.Label();
            this.lblAccountDescription = new System.Windows.Forms.Label();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.pnlGuarantorDetails = new System.Windows.Forms.Panel();
            this.btnGuarantorClear = new System.Windows.Forms.Button();
            this.lblGuarantorDetails = new System.Windows.Forms.Label();
            this.txtAccGuarantor = new System.Windows.Forms.TextBox();
            this.cmbSameAsGuardian = new System.Windows.Forms.ComboBox();
            this.lblSameAsGuardian = new System.Windows.Forms.Label();
            this.btnNewGuarantor = new System.Windows.Forms.Button();
            this.btnGuarantorExistingPatientBrowse = new System.Windows.Forms.Button();
            this.label64 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.chkExcludefromStatement = new System.Windows.Forms.CheckBox();
            this.chkSetToCollection = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.lblGuarMandatory = new System.Windows.Forms.Label();
            this.lblGuarantor = new System.Windows.Forms.Label();
            this.txtAccountDescription = new System.Windows.Forms.TextBox();
            this.lblAccountNo = new System.Windows.Forms.Label();
            this.rbAccountExisting = new System.Windows.Forms.RadioButton();
            this.rbAccountNew = new System.Windows.Forms.RadioButton();
            this.label61 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.txtPAInsuranceNotes = new System.Windows.Forms.TextBox();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.cmbAccounts = new System.Windows.Forms.ComboBox();
            this.btnEditAccount = new System.Windows.Forms.Button();
            this.btnExistingAccountSelect = new System.Windows.Forms.Button();
            this.btnCopyAccount = new System.Windows.Forms.Button();
            this.btnExistingAccountDelete = new System.Windows.Forms.Button();
            this.lblAccMandatory = new System.Windows.Forms.Label();
            this.pnlOtherDetail = new System.Windows.Forms.Panel();
            this.btnbrhosp = new System.Windows.Forms.Button();
            this.btnMUTransaction = new System.Windows.Forms.Button();
            this.cmbPACareTeam = new System.Windows.Forms.ComboBox();
            this.btn_PACareTeamDel = new System.Windows.Forms.Button();
            this.btn_PACareTeamBr = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.pnl_Bottom = new System.Windows.Forms.Panel();
            this.btnOtherDetails = new System.Windows.Forms.Button();
            this.chkYesNoLab = new System.Windows.Forms.CheckBox();
            this.chkSignatureOnFile = new System.Windows.Forms.CheckBox();
            this.chkExempt = new System.Windows.Forms.CheckBox();
            this.chkdirective = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtPAProvider = new System.Windows.Forms.TextBox();
            this.cmbPAPharma = new System.Windows.Forms.ComboBox();
            this.cmbPAReferrals = new System.Windows.Forms.ComboBox();
            this.cmbPAOccupation = new System.Windows.Forms.ComboBox();
            this.cmbPALocation = new System.Windows.Forms.ComboBox();
            this.cmbGaurdian = new System.Windows.Forms.ComboBox();
            this.cmbGenInfoInsurance = new System.Windows.Forms.ComboBox();
            this.txtPAPrimaryCarePhy = new System.Windows.Forms.TextBox();
            this.txtPAPharma = new System.Windows.Forms.TextBox();
            this.lblGeneralInfoOtherDetails = new System.Windows.Forms.Label();
            this.btnOccupationDelete = new System.Windows.Forms.Button();
            this.btnGuardianSelect = new System.Windows.Forms.Button();
            this.btnGuardianDelete = new System.Windows.Forms.Button();
            this.btnClrInsurance = new System.Windows.Forms.Button();
            this.btnOccupationSelect = new System.Windows.Forms.Button();
            this.btnInsurInfo = new System.Windows.Forms.Button();
            this.btnProviderDelete = new System.Windows.Forms.Button();
            this.btn_PAPharmaDel = new System.Windows.Forms.Button();
            this.btnProviderSelect = new System.Windows.Forms.Button();
            this.btn_PrimaryCareDel = new System.Windows.Forms.Button();
            this.btn_PAReferralsDel = new System.Windows.Forms.Button();
            this.btn_PrimaryCareBr = new System.Windows.Forms.Button();
            this.btn_PAPharmaBr = new System.Windows.Forms.Button();
            this.btn_PAReferralsBr = new System.Windows.Forms.Button();
            this.lblOccupation = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblGaurdian = new System.Windows.Forms.Label();
            this.lblInsurance = new System.Windows.Forms.Label();
            this.lblPatientCare = new System.Windows.Forms.Label();
            this.lbPatientReferrals = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.lbPatientPharma = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.pnl_AddressDetails = new System.Windows.Forms.Panel();
            this.pnlEmergencyConDetails = new System.Windows.Forms.Panel();
            this.cmbRelationship = new System.Windows.Forms.ComboBox();
            this.mtxtEmergencyMobile = new gloMaskControl.gloMaskBox();
            this.label47 = new System.Windows.Forms.Label();
            this.mtxtEmergencyPhone = new gloMaskControl.gloMaskBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtEmergencyContact = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.pnlConDetails = new System.Windows.Forms.Panel();
            this.pnlAPIActivationEmail = new System.Windows.Forms.Panel();
            this.cbSendAPIInvitation = new System.Windows.Forms.CheckBox();
            this.pnlPortalInvitaitonEmail = new System.Windows.Forms.Panel();
            this.cbSendPatientPortalActivationEmail = new System.Windows.Forms.CheckBox();
            this.pnlPortalAccount = new System.Windows.Forms.Panel();
            this.lblPatientPortalAccountStatus = new System.Windows.Forms.Label();
            this.lblPortalAccountStatus = new System.Windows.Forms.Label();
            this.btnPatientPortalAccountStatus = new System.Windows.Forms.Button();
            this.mtxtPAMobile = new gloMaskControl.gloMaskBox();
            this.mtxtPAPhone = new gloMaskControl.gloMaskBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtPAEmail = new System.Windows.Forms.TextBox();
            this.txtPAFax = new gloMaskControl.gloMaskBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblFax = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblContactDetails = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.pnlAPIAccount = new System.Windows.Forms.Panel();
            this.lblAPIActivationStatus = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.btnAPIActivation = new System.Windows.Forms.Button();
            this.pnlAddDetails = new System.Windows.Forms.Panel();
            this.pnlAddresssControl = new System.Windows.Forms.Panel();
            this.lblAddr1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbPACountry = new System.Windows.Forms.ComboBox();
            this.cmbPAState = new System.Windows.Forms.ComboBox();
            this.txtPAZip = new System.Windows.Forms.TextBox();
            this.txtPACounty = new System.Windows.Forms.TextBox();
            this.txtPACity = new System.Windows.Forms.TextBox();
            this.txtPAAddress2 = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.txtPAAddress1 = new System.Windows.Forms.TextBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.lblZip = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblAddr2 = new System.Windows.Forms.Label();
            this.lblAddressDetails = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlPadDetails = new System.Windows.Forms.Panel();
            this.btnOrientation = new System.Windows.Forms.Button();
            this.btn_EthnicityDel = new System.Windows.Forms.Button();
            this.btn_Ethnicity = new System.Windows.Forms.Button();
            this.txtPatSuffix = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.dtpBirth = new System.Windows.Forms.DateTimePicker();
            this.btn_RaceDel = new System.Windows.Forms.Button();
            this.btn_Race = new System.Windows.Forms.Button();
            this.cmbCommPref = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.lbPatientbirthtime = new System.Windows.Forms.Label();
            this.txtPACode = new System.Windows.Forms.MaskedTextBox();
            this.cmbPAEthn = new System.Windows.Forms.ComboBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.cmbPALang = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.txtmPASSN = new gloMaskControl.gloMaskBox();
            this.mtxtPADOB = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbPAHandDom = new System.Windows.Forms.ComboBox();
            this.cmbPARace = new System.Windows.Forms.ComboBox();
            this.cmbPAMarital = new System.Windows.Forms.ComboBox();
            this.txtPAMName = new System.Windows.Forms.TextBox();
            this.txtPALName = new System.Windows.Forms.TextBox();
            this.txtPAFname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbPatientRace = new System.Windows.Forms.Label();
            this.lblPatientSSN = new System.Windows.Forms.Label();
            this.lbPatientMarital = new System.Windows.Forms.Label();
            this.lbPatientDOB = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblPatientCode = new System.Windows.Forms.Label();
            this.lblPersonalInfo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lblPALName = new System.Windows.Forms.Label();
            this.lblPAMName = new System.Windows.Forms.Label();
            this.lblPAFName = new System.Windows.Forms.Label();
            this.pnlPAPhoto = new System.Windows.Forms.Panel();
            this.TrackbarPlus = new System.Windows.Forms.Button();
            this.TrackbarMinus = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.rbWebCam = new System.Windows.Forms.RadioButton();
            this.rbBrowsePhoto = new System.Windows.Forms.RadioButton();
            this.btn_PAClearPhoto = new System.Windows.Forms.Button();
            this.btn_PAPhotoBrowse = new System.Windows.Forms.Button();
            this.btn_PACapturePhoto = new System.Windows.Forms.Button();
            this.myNewTrackBar = new System.Windows.Forms.TrackBar();
            this.lbPatientFDom = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlGeneralInfoHeader = new System.Windows.Forms.Panel();
            this.txtPatientPrefix = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblGeneralInfo = new System.Windows.Forms.Label();
            this.dlgPhotoBrowser = new System.Windows.Forms.OpenFileDialog();
            this.pnlInternalControl = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.picPAPhoto = new gloPictureBox.gloPictureBox();
            this.myPictureBox = new gloPictureBox.gloCameraBox();
            this.pnlDemographicInfo.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlPatientOtherGuarantorInfo.SuspendLayout();
            this.pnlBusinessCenter.SuspendLayout();
            this.pnlGuarantorDetails.SuspendLayout();
            this.pnlOtherDetail.SuspendLayout();
            this.pnl_Bottom.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_AddressDetails.SuspendLayout();
            this.pnlEmergencyConDetails.SuspendLayout();
            this.pnlConDetails.SuspendLayout();
            this.pnlAPIActivationEmail.SuspendLayout();
            this.pnlPortalInvitaitonEmail.SuspendLayout();
            this.pnlPortalAccount.SuspendLayout();
            this.pnlAPIAccount.SuspendLayout();
            this.pnlAddDetails.SuspendLayout();
            this.pnlPadDetails.SuspendLayout();
            this.pnlPAPhoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myNewTrackBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlGeneralInfoHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDemographicInfo
            // 
            this.pnlDemographicInfo.Controls.Add(this.panel3);
            this.pnlDemographicInfo.Controls.Add(this.pnlOtherDetail);
            this.pnlDemographicInfo.Controls.Add(this.panel2);
            this.pnlDemographicInfo.Controls.Add(this.pnl_AddressDetails);
            this.pnlDemographicInfo.Controls.Add(this.pnlPadDetails);
            this.pnlDemographicInfo.Controls.Add(this.panel1);
            this.pnlDemographicInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDemographicInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlDemographicInfo.Name = "pnlDemographicInfo";
            this.pnlDemographicInfo.Size = new System.Drawing.Size(780, 735);
            this.pnlDemographicInfo.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlPatientOtherGuarantorInfo);
            this.panel3.Controls.Add(this.pnlBusinessCenter);
            this.panel3.Controls.Add(this.lblAccountDescription);
            this.panel3.Controls.Add(this.btnAddAccount);
            this.panel3.Controls.Add(this.label51);
            this.panel3.Controls.Add(this.lblAccount);
            this.panel3.Controls.Add(this.pnlGuarantorDetails);
            this.panel3.Controls.Add(this.txtAccountDescription);
            this.panel3.Controls.Add(this.lblAccountNo);
            this.panel3.Controls.Add(this.rbAccountExisting);
            this.panel3.Controls.Add(this.rbAccountNew);
            this.panel3.Controls.Add(this.label61);
            this.panel3.Controls.Add(this.label52);
            this.panel3.Controls.Add(this.label53);
            this.panel3.Controls.Add(this.label54);
            this.panel3.Controls.Add(this.label55);
            this.panel3.Controls.Add(this.txtPAInsuranceNotes);
            this.panel3.Controls.Add(this.txtAccountNo);
            this.panel3.Controls.Add(this.cmbAccounts);
            this.panel3.Controls.Add(this.btnEditAccount);
            this.panel3.Controls.Add(this.btnExistingAccountSelect);
            this.panel3.Controls.Add(this.btnCopyAccount);
            this.panel3.Controls.Add(this.btnExistingAccountDelete);
            this.panel3.Controls.Add(this.lblAccMandatory);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 547);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel3.Size = new System.Drawing.Size(780, 188);
            this.panel3.TabIndex = 3;
            // 
            // pnlPatientOtherGuarantorInfo
            // 
            this.pnlPatientOtherGuarantorInfo.Controls.Add(this.label6);
            this.pnlPatientOtherGuarantorInfo.Controls.Add(this.cmbOtherGuarantor);
            this.pnlPatientOtherGuarantorInfo.Controls.Add(this.btnotherNewGuarantor);
            this.pnlPatientOtherGuarantorInfo.Controls.Add(this.btnOtherGuarantorExistingPatientBrowse);
            this.pnlPatientOtherGuarantorInfo.Location = new System.Drawing.Point(365, 152);
            this.pnlPatientOtherGuarantorInfo.Name = "pnlPatientOtherGuarantorInfo";
            this.pnlPatientOtherGuarantorInfo.Size = new System.Drawing.Size(346, 25);
            this.pnlPatientOtherGuarantorInfo.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoEllipsis = true;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 14);
            this.label6.TabIndex = 1012;
            this.label6.Text = "Other Guarantors :";
            // 
            // cmbOtherGuarantor
            // 
            this.cmbOtherGuarantor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbOtherGuarantor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOtherGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOtherGuarantor.FormattingEnabled = true;
            this.cmbOtherGuarantor.Location = new System.Drawing.Point(116, 2);
            this.cmbOtherGuarantor.Name = "cmbOtherGuarantor";
            this.cmbOtherGuarantor.Size = new System.Drawing.Size(172, 22);
            this.cmbOtherGuarantor.TabIndex = 77;
            // 
            // btnotherNewGuarantor
            // 
            this.btnotherNewGuarantor.AutoEllipsis = true;
            this.btnotherNewGuarantor.BackColor = System.Drawing.Color.Transparent;
            this.btnotherNewGuarantor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnotherNewGuarantor.BackgroundImage")));
            this.btnotherNewGuarantor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnotherNewGuarantor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnotherNewGuarantor.Image = ((System.Drawing.Image)(resources.GetObject("btnotherNewGuarantor.Image")));
            this.btnotherNewGuarantor.Location = new System.Drawing.Point(316, 3);
            this.btnotherNewGuarantor.Name = "btnotherNewGuarantor";
            this.btnotherNewGuarantor.Size = new System.Drawing.Size(22, 21);
            this.btnotherNewGuarantor.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnotherNewGuarantor, "Add Guarantors");
            this.btnotherNewGuarantor.UseVisualStyleBackColor = false;
            this.btnotherNewGuarantor.Click += new System.EventHandler(this.btnotherNewGuarantor_Click);
            this.btnotherNewGuarantor.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnotherNewGuarantor.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnOtherGuarantorExistingPatientBrowse
            // 
            this.btnOtherGuarantorExistingPatientBrowse.AutoEllipsis = true;
            this.btnOtherGuarantorExistingPatientBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnOtherGuarantorExistingPatientBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOtherGuarantorExistingPatientBrowse.BackgroundImage")));
            this.btnOtherGuarantorExistingPatientBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOtherGuarantorExistingPatientBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtherGuarantorExistingPatientBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnOtherGuarantorExistingPatientBrowse.Image")));
            this.btnOtherGuarantorExistingPatientBrowse.Location = new System.Drawing.Point(291, 3);
            this.btnOtherGuarantorExistingPatientBrowse.Name = "btnOtherGuarantorExistingPatientBrowse";
            this.btnOtherGuarantorExistingPatientBrowse.Size = new System.Drawing.Size(22, 21);
            this.btnOtherGuarantorExistingPatientBrowse.TabIndex = 0;
            this.btnOtherGuarantorExistingPatientBrowse.UseVisualStyleBackColor = false;
            this.btnOtherGuarantorExistingPatientBrowse.Click += new System.EventHandler(this.btnOtherGuarantorExistingPatientBrowse_Click);
            this.btnOtherGuarantorExistingPatientBrowse.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnOtherGuarantorExistingPatientBrowse.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnlBusinessCenter
            // 
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.cmbBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCenterCpt);
            this.pnlBusinessCenter.Location = new System.Drawing.Point(639, 51);
            this.pnlBusinessCenter.Name = "pnlBusinessCenter";
            this.pnlBusinessCenter.Size = new System.Drawing.Size(132, 26);
            this.pnlBusinessCenter.TabIndex = 8;
            // 
            // lblBusinessCenter
            // 
            this.lblBusinessCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBusinessCenter.AutoEllipsis = true;
            this.lblBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCenter.Location = new System.Drawing.Point(41, 6);
            this.lblBusinessCenter.Name = "lblBusinessCenter";
            this.lblBusinessCenter.Size = new System.Drawing.Size(86, 14);
            this.lblBusinessCenter.TabIndex = 1008;
            this.toolTip1.SetToolTip(this.lblBusinessCenter, "Business Center");
            // 
            // cmbBusinessCenter
            // 
            this.cmbBusinessCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbBusinessCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBusinessCenter.FormattingEnabled = true;
            this.cmbBusinessCenter.Location = new System.Drawing.Point(41, 3);
            this.cmbBusinessCenter.Name = "cmbBusinessCenter";
            this.cmbBusinessCenter.Size = new System.Drawing.Size(87, 22);
            this.cmbBusinessCenter.TabIndex = 0;
            // 
            // lblBusinessCenterCpt
            // 
            this.lblBusinessCenterCpt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBusinessCenterCpt.AutoEllipsis = true;
            this.lblBusinessCenterCpt.AutoSize = true;
            this.lblBusinessCenterCpt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCenterCpt.Location = new System.Drawing.Point(2, 6);
            this.lblBusinessCenterCpt.Name = "lblBusinessCenterCpt";
            this.lblBusinessCenterCpt.Size = new System.Drawing.Size(37, 14);
            this.lblBusinessCenterCpt.TabIndex = 1007;
            this.lblBusinessCenterCpt.Text = "BUS :";
            this.toolTip1.SetToolTip(this.lblBusinessCenterCpt, "Business Center");
            // 
            // lblAccountDescription
            // 
            this.lblAccountDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAccountDescription.AutoEllipsis = true;
            this.lblAccountDescription.AutoSize = true;
            this.lblAccountDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountDescription.Location = new System.Drawing.Point(358, 58);
            this.lblAccountDescription.Name = "lblAccountDescription";
            this.lblAccountDescription.Size = new System.Drawing.Size(78, 14);
            this.lblAccountDescription.TabIndex = 66;
            this.lblAccountDescription.Text = "Acct. Desc. :";
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.AutoEllipsis = true;
            this.btnAddAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnAddAccount.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnAddAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddAccount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnAddAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAccount.Image = ((System.Drawing.Image)(resources.GetObject("btnAddAccount.Image")));
            this.btnAddAccount.Location = new System.Drawing.Point(667, 5);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(22, 22);
            this.btnAddAccount.TabIndex = 3;
            this.btnAddAccount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.btnAddAccount, "Add Patient Account");
            this.btnAddAccount.UseVisualStyleBackColor = false;
            this.btnAddAccount.Visible = false;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            this.btnAddAccount.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnAddAccount.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label51
            // 
            this.label51.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label51.AutoEllipsis = true;
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(7, 4);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(103, 14);
            this.label51.TabIndex = 61;
            this.label51.Text = " Patient Notes :";
            // 
            // lblAccount
            // 
            this.lblAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccount.AutoEllipsis = true;
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(357, 6);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(66, 14);
            this.lblAccount.TabIndex = 61;
            this.lblAccount.Text = "Account :";
            // 
            // pnlGuarantorDetails
            // 
            this.pnlGuarantorDetails.Controls.Add(this.btnGuarantorClear);
            this.pnlGuarantorDetails.Controls.Add(this.lblGuarantorDetails);
            this.pnlGuarantorDetails.Controls.Add(this.txtAccGuarantor);
            this.pnlGuarantorDetails.Controls.Add(this.cmbSameAsGuardian);
            this.pnlGuarantorDetails.Controls.Add(this.lblSameAsGuardian);
            this.pnlGuarantorDetails.Controls.Add(this.btnNewGuarantor);
            this.pnlGuarantorDetails.Controls.Add(this.btnGuarantorExistingPatientBrowse);
            this.pnlGuarantorDetails.Controls.Add(this.label64);
            this.pnlGuarantorDetails.Controls.Add(this.textBox2);
            this.pnlGuarantorDetails.Controls.Add(this.chkExcludefromStatement);
            this.pnlGuarantorDetails.Controls.Add(this.chkSetToCollection);
            this.pnlGuarantorDetails.Controls.Add(this.textBox1);
            this.pnlGuarantorDetails.Controls.Add(this.radioButton2);
            this.pnlGuarantorDetails.Controls.Add(this.radioButton1);
            this.pnlGuarantorDetails.Controls.Add(this.lblGuarMandatory);
            this.pnlGuarantorDetails.Controls.Add(this.lblGuarantor);
            this.pnlGuarantorDetails.Location = new System.Drawing.Point(355, 77);
            this.pnlGuarantorDetails.Name = "pnlGuarantorDetails";
            this.pnlGuarantorDetails.Size = new System.Drawing.Size(377, 75);
            this.pnlGuarantorDetails.TabIndex = 9;
            // 
            // btnGuarantorClear
            // 
            this.btnGuarantorClear.AutoEllipsis = true;
            this.btnGuarantorClear.BackColor = System.Drawing.Color.Transparent;
            this.btnGuarantorClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuarantorClear.BackgroundImage")));
            this.btnGuarantorClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuarantorClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuarantorClear.Image = ((System.Drawing.Image)(resources.GetObject("btnGuarantorClear.Image")));
            this.btnGuarantorClear.Location = new System.Drawing.Point(336, 2);
            this.btnGuarantorClear.Name = "btnGuarantorClear";
            this.btnGuarantorClear.Size = new System.Drawing.Size(21, 21);
            this.btnGuarantorClear.TabIndex = 3;
            this.btnGuarantorClear.UseVisualStyleBackColor = false;
            this.btnGuarantorClear.Click += new System.EventHandler(this.btnGuarantorClear_Click);
            this.btnGuarantorClear.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnGuarantorClear.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // lblGuarantorDetails
            // 
            this.lblGuarantorDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGuarantorDetails.AutoEllipsis = true;
            this.lblGuarantorDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuarantorDetails.Location = new System.Drawing.Point(83, 6);
            this.lblGuarantorDetails.Name = "lblGuarantorDetails";
            this.lblGuarantorDetails.Size = new System.Drawing.Size(96, 15);
            this.lblGuarantorDetails.TabIndex = 70;
            this.lblGuarantorDetails.Text = "GuarantorDetails";
            this.lblGuarantorDetails.Visible = false;
            // 
            // txtAccGuarantor
            // 
            this.txtAccGuarantor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccGuarantor.Location = new System.Drawing.Point(84, 2);
            this.txtAccGuarantor.Name = "txtAccGuarantor";
            this.txtAccGuarantor.ReadOnly = true;
            this.txtAccGuarantor.Size = new System.Drawing.Size(199, 22);
            this.txtAccGuarantor.TabIndex = 0;
            // 
            // cmbSameAsGuardian
            // 
            this.cmbSameAsGuardian.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbSameAsGuardian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSameAsGuardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSameAsGuardian.FormattingEnabled = true;
            this.cmbSameAsGuardian.Location = new System.Drawing.Point(84, 27);
            this.cmbSameAsGuardian.Name = "cmbSameAsGuardian";
            this.cmbSameAsGuardian.Size = new System.Drawing.Size(199, 22);
            this.cmbSameAsGuardian.TabIndex = 4;
            this.cmbSameAsGuardian.SelectedIndexChanged += new System.EventHandler(this.cmbSameAsGuardian_SelectedIndexChanged);
            // 
            // lblSameAsGuardian
            // 
            this.lblSameAsGuardian.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSameAsGuardian.AutoEllipsis = true;
            this.lblSameAsGuardian.AutoSize = true;
            this.lblSameAsGuardian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSameAsGuardian.Location = new System.Drawing.Point(19, 31);
            this.lblSameAsGuardian.Name = "lblSameAsGuardian";
            this.lblSameAsGuardian.Size = new System.Drawing.Size(62, 14);
            this.lblSameAsGuardian.TabIndex = 72;
            this.lblSameAsGuardian.Text = "Same As :";
            // 
            // btnNewGuarantor
            // 
            this.btnNewGuarantor.AutoEllipsis = true;
            this.btnNewGuarantor.BackColor = System.Drawing.Color.Transparent;
            this.btnNewGuarantor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewGuarantor.BackgroundImage")));
            this.btnNewGuarantor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewGuarantor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewGuarantor.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGuarantor.Image")));
            this.btnNewGuarantor.Location = new System.Drawing.Point(312, 2);
            this.btnNewGuarantor.Name = "btnNewGuarantor";
            this.btnNewGuarantor.Size = new System.Drawing.Size(21, 21);
            this.btnNewGuarantor.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnNewGuarantor, "Add Guarantors");
            this.btnNewGuarantor.UseVisualStyleBackColor = false;
            this.btnNewGuarantor.Click += new System.EventHandler(this.btnNewGuarantor_Click);
            this.btnNewGuarantor.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnNewGuarantor.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnGuarantorExistingPatientBrowse
            // 
            this.btnGuarantorExistingPatientBrowse.AutoEllipsis = true;
            this.btnGuarantorExistingPatientBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnGuarantorExistingPatientBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuarantorExistingPatientBrowse.BackgroundImage")));
            this.btnGuarantorExistingPatientBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuarantorExistingPatientBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuarantorExistingPatientBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnGuarantorExistingPatientBrowse.Image")));
            this.btnGuarantorExistingPatientBrowse.Location = new System.Drawing.Point(287, 2);
            this.btnGuarantorExistingPatientBrowse.Name = "btnGuarantorExistingPatientBrowse";
            this.btnGuarantorExistingPatientBrowse.Size = new System.Drawing.Size(22, 21);
            this.btnGuarantorExistingPatientBrowse.TabIndex = 1;
            this.btnGuarantorExistingPatientBrowse.UseVisualStyleBackColor = false;
            this.btnGuarantorExistingPatientBrowse.Click += new System.EventHandler(this.btnGuarantorSelect_Click);
            this.btnGuarantorExistingPatientBrowse.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnGuarantorExistingPatientBrowse.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label64
            // 
            this.label64.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label64.AutoEllipsis = true;
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(14, -75);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(61, 14);
            this.label64.TabIndex = 61;
            this.label64.Text = "Account :";
            // 
            // textBox2
            // 
            this.textBox2.AcceptsReturn = true;
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.textBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(81, -27);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(222, 22);
            this.textBox2.TabIndex = 67;
            // 
            // chkExcludefromStatement
            // 
            this.chkExcludefromStatement.AutoSize = true;
            this.chkExcludefromStatement.Location = new System.Drawing.Point(77, 51);
            this.chkExcludefromStatement.Name = "chkExcludefromStatement";
            this.chkExcludefromStatement.Size = new System.Drawing.Size(158, 18);
            this.chkExcludefromStatement.TabIndex = 5;
            this.chkExcludefromStatement.Text = "Exclude from statement";
            this.chkExcludefromStatement.UseVisualStyleBackColor = true;
            // 
            // chkSetToCollection
            // 
            this.chkSetToCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSetToCollection.AutoSize = true;
            this.chkSetToCollection.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSetToCollection.Location = new System.Drawing.Point(235, 51);
            this.chkSetToCollection.Name = "chkSetToCollection";
            this.chkSetToCollection.Size = new System.Drawing.Size(123, 18);
            this.chkSetToCollection.TabIndex = 6;
            this.chkSetToCollection.Text = "Sent to collection";
            this.chkSetToCollection.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(81, -53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(169, 22);
            this.textBox1.TabIndex = 65;
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(163, -77);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(66, 18);
            this.radioButton2.TabIndex = 63;
            this.radioButton2.Text = "Existing";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(103, -77);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 18);
            this.radioButton1.TabIndex = 62;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "New";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // lblGuarMandatory
            // 
            this.lblGuarMandatory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGuarMandatory.AutoEllipsis = true;
            this.lblGuarMandatory.AutoSize = true;
            this.lblGuarMandatory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuarMandatory.ForeColor = System.Drawing.Color.Red;
            this.lblGuarMandatory.Location = new System.Drawing.Point(1, 5);
            this.lblGuarMandatory.Name = "lblGuarMandatory";
            this.lblGuarMandatory.Size = new System.Drawing.Size(14, 14);
            this.lblGuarMandatory.TabIndex = 1006;
            this.lblGuarMandatory.Text = "*";
            // 
            // lblGuarantor
            // 
            this.lblGuarantor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGuarantor.AutoEllipsis = true;
            this.lblGuarantor.AutoSize = true;
            this.lblGuarantor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGuarantor.Location = new System.Drawing.Point(7, 5);
            this.lblGuarantor.MaximumSize = new System.Drawing.Size(74, 14);
            this.lblGuarantor.MinimumSize = new System.Drawing.Size(74, 14);
            this.lblGuarantor.Name = "lblGuarantor";
            this.lblGuarantor.Size = new System.Drawing.Size(74, 14);
            this.lblGuarantor.TabIndex = 12;
            this.lblGuarantor.Text = "Guarantor :";
            this.lblGuarantor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAccountDescription
            // 
            this.txtAccountDescription.AcceptsReturn = true;
            this.txtAccountDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccountDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountDescription.Location = new System.Drawing.Point(438, 54);
            this.txtAccountDescription.Name = "txtAccountDescription";
            this.txtAccountDescription.Size = new System.Drawing.Size(200, 22);
            this.txtAccountDescription.TabIndex = 7;
            // 
            // lblAccountNo
            // 
            this.lblAccountNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAccountNo.AutoEllipsis = true;
            this.lblAccountNo.AutoSize = true;
            this.lblAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNo.Location = new System.Drawing.Point(383, 33);
            this.lblAccountNo.Name = "lblAccountNo";
            this.lblAccountNo.Size = new System.Drawing.Size(53, 14);
            this.lblAccountNo.TabIndex = 64;
            this.lblAccountNo.Text = "Acct.# :";
            // 
            // rbAccountExisting
            // 
            this.rbAccountExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbAccountExisting.AutoSize = true;
            this.rbAccountExisting.Location = new System.Drawing.Point(494, 8);
            this.rbAccountExisting.Name = "rbAccountExisting";
            this.rbAccountExisting.Size = new System.Drawing.Size(66, 18);
            this.rbAccountExisting.TabIndex = 2;
            this.rbAccountExisting.Text = "Existing";
            this.rbAccountExisting.UseVisualStyleBackColor = true;
            this.rbAccountExisting.CheckedChanged += new System.EventHandler(this.rbAccountExisting_CheckedChanged);
            // 
            // rbAccountNew
            // 
            this.rbAccountNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbAccountNew.AutoSize = true;
            this.rbAccountNew.Checked = true;
            this.rbAccountNew.Location = new System.Drawing.Point(439, 8);
            this.rbAccountNew.Name = "rbAccountNew";
            this.rbAccountNew.Size = new System.Drawing.Size(50, 18);
            this.rbAccountNew.TabIndex = 1;
            this.rbAccountNew.TabStop = true;
            this.rbAccountNew.Text = "New";
            this.rbAccountNew.UseVisualStyleBackColor = true;
            this.rbAccountNew.CheckedChanged += new System.EventHandler(this.rbAccountNew_CheckedChanged);
            // 
            // label61
            // 
            this.label61.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label61.Location = new System.Drawing.Point(350, -3);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(1, 187);
            this.label61.TabIndex = 6000;
            this.label61.Text = "label61";
            // 
            // label52
            // 
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label52.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label52.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label52.Location = new System.Drawing.Point(4, 184);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(772, 1);
            this.label52.TabIndex = 12;
            this.label52.Text = "label2";
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label53.Dock = System.Windows.Forms.DockStyle.Left;
            this.label53.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(3, 1);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(1, 184);
            this.label53.TabIndex = 11;
            this.label53.Text = "label4";
            // 
            // label54
            // 
            this.label54.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label54.Dock = System.Windows.Forms.DockStyle.Right;
            this.label54.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label54.Location = new System.Drawing.Point(776, 1);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(1, 184);
            this.label54.TabIndex = 10;
            this.label54.Text = "label3";
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label55.Dock = System.Windows.Forms.DockStyle.Top;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(3, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(774, 1);
            this.label55.TabIndex = 9;
            this.label55.Text = "label1";
            // 
            // txtPAInsuranceNotes
            // 
            this.txtPAInsuranceNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPAInsuranceNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAInsuranceNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAInsuranceNotes.Location = new System.Drawing.Point(13, 22);
            this.txtPAInsuranceNotes.MaxLength = 1500;
            this.txtPAInsuranceNotes.Multiline = true;
            this.txtPAInsuranceNotes.Name = "txtPAInsuranceNotes";
            this.txtPAInsuranceNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPAInsuranceNotes.Size = new System.Drawing.Size(333, 156);
            this.txtPAInsuranceNotes.TabIndex = 0;
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtAccountNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountNo.Location = new System.Drawing.Point(438, 29);
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(200, 22);
            this.txtAccountNo.TabIndex = 4;
            // 
            // cmbAccounts
            // 
            this.cmbAccounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccounts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAccounts.FormattingEnabled = true;
            this.cmbAccounts.Location = new System.Drawing.Point(438, 29);
            this.cmbAccounts.Name = "cmbAccounts";
            this.cmbAccounts.Size = new System.Drawing.Size(200, 22);
            this.cmbAccounts.TabIndex = 65;
            this.cmbAccounts.Visible = false;
            this.cmbAccounts.SelectedIndexChanged += new System.EventHandler(this.cmbAccounts_SelectedIndexChanged);
            // 
            // btnEditAccount
            // 
            this.btnEditAccount.AutoEllipsis = true;
            this.btnEditAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnEditAccount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditAccount.BackgroundImage")));
            this.btnEditAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditAccount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnEditAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditAccount.Image = ((System.Drawing.Image)(resources.GetObject("btnEditAccount.Image")));
            this.btnEditAccount.Location = new System.Drawing.Point(642, 29);
            this.btnEditAccount.Name = "btnEditAccount";
            this.btnEditAccount.Size = new System.Drawing.Size(22, 22);
            this.btnEditAccount.TabIndex = 5;
            this.btnEditAccount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.btnEditAccount, "Edit Patient Account");
            this.btnEditAccount.UseVisualStyleBackColor = false;
            this.btnEditAccount.Visible = false;
            this.btnEditAccount.Click += new System.EventHandler(this.btnEditAccount_Click);
            this.btnEditAccount.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnEditAccount.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnExistingAccountSelect
            // 
            this.btnExistingAccountSelect.AutoEllipsis = true;
            this.btnExistingAccountSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnExistingAccountSelect.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnExistingAccountSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExistingAccountSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExistingAccountSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnExistingAccountSelect.Image")));
            this.btnExistingAccountSelect.Location = new System.Drawing.Point(642, 29);
            this.btnExistingAccountSelect.Name = "btnExistingAccountSelect";
            this.btnExistingAccountSelect.Size = new System.Drawing.Size(22, 22);
            this.btnExistingAccountSelect.TabIndex = 5;
            this.btnExistingAccountSelect.UseVisualStyleBackColor = false;
            this.btnExistingAccountSelect.Visible = false;
            this.btnExistingAccountSelect.Click += new System.EventHandler(this.btnExistingAccountSelect_Click);
            this.btnExistingAccountSelect.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnExistingAccountSelect.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnCopyAccount
            // 
            this.btnCopyAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnCopyAccount.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyAccount.BackgroundImage")));
            this.btnCopyAccount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCopyAccount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnCopyAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyAccount.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyAccount.Image")));
            this.btnCopyAccount.Location = new System.Drawing.Point(667, 29);
            this.btnCopyAccount.Name = "btnCopyAccount";
            this.btnCopyAccount.Size = new System.Drawing.Size(22, 22);
            this.btnCopyAccount.TabIndex = 6;
            this.btnCopyAccount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.btnCopyAccount, "Copy Patient Account");
            this.btnCopyAccount.UseVisualStyleBackColor = false;
            this.btnCopyAccount.Visible = false;
            this.btnCopyAccount.Click += new System.EventHandler(this.btnCopyAccount_Click);
            this.btnCopyAccount.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnCopyAccount.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnExistingAccountDelete
            // 
            this.btnExistingAccountDelete.AutoEllipsis = true;
            this.btnExistingAccountDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnExistingAccountDelete.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnExistingAccountDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExistingAccountDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExistingAccountDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnExistingAccountDelete.Image")));
            this.btnExistingAccountDelete.Location = new System.Drawing.Point(667, 29);
            this.btnExistingAccountDelete.Name = "btnExistingAccountDelete";
            this.btnExistingAccountDelete.Size = new System.Drawing.Size(22, 22);
            this.btnExistingAccountDelete.TabIndex = 6;
            this.btnExistingAccountDelete.UseVisualStyleBackColor = false;
            this.btnExistingAccountDelete.Visible = false;
            this.btnExistingAccountDelete.Click += new System.EventHandler(this.btnExistingAccountDelete_Click);
            this.btnExistingAccountDelete.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnExistingAccountDelete.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // lblAccMandatory
            // 
            this.lblAccMandatory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccMandatory.AutoEllipsis = true;
            this.lblAccMandatory.AutoSize = true;
            this.lblAccMandatory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccMandatory.ForeColor = System.Drawing.Color.Red;
            this.lblAccMandatory.Location = new System.Drawing.Point(373, 33);
            this.lblAccMandatory.Name = "lblAccMandatory";
            this.lblAccMandatory.Size = new System.Drawing.Size(14, 14);
            this.lblAccMandatory.TabIndex = 74;
            this.lblAccMandatory.Text = "*";
            // 
            // pnlOtherDetail
            // 
            this.pnlOtherDetail.Controls.Add(this.btnbrhosp);
            this.pnlOtherDetail.Controls.Add(this.btnMUTransaction);
            this.pnlOtherDetail.Controls.Add(this.cmbPACareTeam);
            this.pnlOtherDetail.Controls.Add(this.btn_PACareTeamDel);
            this.pnlOtherDetail.Controls.Add(this.btn_PACareTeamBr);
            this.pnlOtherDetail.Controls.Add(this.label20);
            this.pnlOtherDetail.Controls.Add(this.pnl_Bottom);
            this.pnlOtherDetail.Controls.Add(this.label19);
            this.pnlOtherDetail.Controls.Add(this.txtPAProvider);
            this.pnlOtherDetail.Controls.Add(this.cmbPAPharma);
            this.pnlOtherDetail.Controls.Add(this.cmbPAReferrals);
            this.pnlOtherDetail.Controls.Add(this.cmbPAOccupation);
            this.pnlOtherDetail.Controls.Add(this.cmbPALocation);
            this.pnlOtherDetail.Controls.Add(this.cmbGaurdian);
            this.pnlOtherDetail.Controls.Add(this.cmbGenInfoInsurance);
            this.pnlOtherDetail.Controls.Add(this.txtPAPrimaryCarePhy);
            this.pnlOtherDetail.Controls.Add(this.txtPAPharma);
            this.pnlOtherDetail.Controls.Add(this.lblGeneralInfoOtherDetails);
            this.pnlOtherDetail.Controls.Add(this.btnOccupationDelete);
            this.pnlOtherDetail.Controls.Add(this.btnGuardianSelect);
            this.pnlOtherDetail.Controls.Add(this.btnGuardianDelete);
            this.pnlOtherDetail.Controls.Add(this.btnClrInsurance);
            this.pnlOtherDetail.Controls.Add(this.btnOccupationSelect);
            this.pnlOtherDetail.Controls.Add(this.btnInsurInfo);
            this.pnlOtherDetail.Controls.Add(this.btnProviderDelete);
            this.pnlOtherDetail.Controls.Add(this.btn_PAPharmaDel);
            this.pnlOtherDetail.Controls.Add(this.btnProviderSelect);
            this.pnlOtherDetail.Controls.Add(this.btn_PrimaryCareDel);
            this.pnlOtherDetail.Controls.Add(this.btn_PAReferralsDel);
            this.pnlOtherDetail.Controls.Add(this.btn_PrimaryCareBr);
            this.pnlOtherDetail.Controls.Add(this.btn_PAPharmaBr);
            this.pnlOtherDetail.Controls.Add(this.btn_PAReferralsBr);
            this.pnlOtherDetail.Controls.Add(this.lblOccupation);
            this.pnlOtherDetail.Controls.Add(this.lblLocation);
            this.pnlOtherDetail.Controls.Add(this.lblGaurdian);
            this.pnlOtherDetail.Controls.Add(this.lblInsurance);
            this.pnlOtherDetail.Controls.Add(this.lblPatientCare);
            this.pnlOtherDetail.Controls.Add(this.lbPatientReferrals);
            this.pnlOtherDetail.Controls.Add(this.lblDoctor);
            this.pnlOtherDetail.Controls.Add(this.lbPatientPharma);
            this.pnlOtherDetail.Controls.Add(this.label4);
            this.pnlOtherDetail.Controls.Add(this.label7);
            this.pnlOtherDetail.Controls.Add(this.label18);
            this.pnlOtherDetail.Controls.Add(this.label41);
            this.pnlOtherDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOtherDetail.Location = new System.Drawing.Point(0, 378);
            this.pnlOtherDetail.Name = "pnlOtherDetail";
            this.pnlOtherDetail.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlOtherDetail.Size = new System.Drawing.Size(780, 169);
            this.pnlOtherDetail.TabIndex = 2;
            // 
            // btnbrhosp
            // 
            this.btnbrhosp.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnbrhosp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnbrhosp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnbrhosp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnbrhosp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbrhosp.Image = ((System.Drawing.Image)(resources.GetObject("btnbrhosp.Image")));
            this.btnbrhosp.Location = new System.Drawing.Point(752, 42);
            this.btnbrhosp.Name = "btnbrhosp";
            this.btnbrhosp.Size = new System.Drawing.Size(22, 22);
            this.btnbrhosp.TabIndex = 66;
            this.toolTip1.SetToolTip(this.btnbrhosp, "Inbound Hospital");
            this.btnbrhosp.UseVisualStyleBackColor = true;
            this.btnbrhosp.Click += new System.EventHandler(this.btnbrhosp_Click);
            // 
            // btnMUTransaction
            // 
            this.btnMUTransaction.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnMUTransaction.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMUTransaction.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMUTransaction.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMUTransaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMUTransaction.Image = ((System.Drawing.Image)(resources.GetObject("btnMUTransaction.Image")));
            this.btnMUTransaction.Location = new System.Drawing.Point(728, 42);
            this.btnMUTransaction.Name = "btnMUTransaction";
            this.btnMUTransaction.Size = new System.Drawing.Size(22, 22);
            this.btnMUTransaction.TabIndex = 49;
            this.toolTip1.SetToolTip(this.btnMUTransaction, "Inbound Transition of Care");
            this.btnMUTransaction.UseVisualStyleBackColor = true;
            this.btnMUTransaction.Click += new System.EventHandler(this.btnMUTransaction_Click);
            // 
            // cmbPACareTeam
            // 
            this.cmbPACareTeam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPACareTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPACareTeam.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPACareTeam.FormattingEnabled = true;
            this.cmbPACareTeam.Location = new System.Drawing.Point(513, 92);
            this.cmbPACareTeam.Name = "cmbPACareTeam";
            this.cmbPACareTeam.Size = new System.Drawing.Size(158, 22);
            this.cmbPACareTeam.TabIndex = 59;
            // 
            // btn_PACareTeamDel
            // 
            this.btn_PACareTeamDel.AutoEllipsis = true;
            this.btn_PACareTeamDel.BackColor = System.Drawing.Color.Transparent;
            this.btn_PACareTeamDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PACareTeamDel.BackgroundImage")));
            this.btn_PACareTeamDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PACareTeamDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PACareTeamDel.Image = ((System.Drawing.Image)(resources.GetObject("btn_PACareTeamDel.Image")));
            this.btn_PACareTeamDel.Location = new System.Drawing.Point(704, 92);
            this.btn_PACareTeamDel.Name = "btn_PACareTeamDel";
            this.btn_PACareTeamDel.Size = new System.Drawing.Size(22, 22);
            this.btn_PACareTeamDel.TabIndex = 61;
            this.btn_PACareTeamDel.UseVisualStyleBackColor = false;
            this.btn_PACareTeamDel.Click += new System.EventHandler(this.btn_PACareTeamDel_Click);
            // 
            // btn_PACareTeamBr
            // 
            this.btn_PACareTeamBr.AutoEllipsis = true;
            this.btn_PACareTeamBr.BackColor = System.Drawing.Color.Transparent;
            this.btn_PACareTeamBr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PACareTeamBr.BackgroundImage")));
            this.btn_PACareTeamBr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PACareTeamBr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PACareTeamBr.Image = ((System.Drawing.Image)(resources.GetObject("btn_PACareTeamBr.Image")));
            this.btn_PACareTeamBr.Location = new System.Drawing.Point(678, 92);
            this.btn_PACareTeamBr.Name = "btn_PACareTeamBr";
            this.btn_PACareTeamBr.Size = new System.Drawing.Size(22, 22);
            this.btn_PACareTeamBr.TabIndex = 60;
            this.btn_PACareTeamBr.UseVisualStyleBackColor = false;
            this.btn_PACareTeamBr.Click += new System.EventHandler(this.btn_PACareTeamBr_Click);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoEllipsis = true;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(405, 96);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(106, 14);
            this.label20.TabIndex = 65;
            this.label20.Text = "Other CareTeam :";
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.Controls.Add(this.btnOtherDetails);
            this.pnl_Bottom.Controls.Add(this.chkYesNoLab);
            this.pnl_Bottom.Controls.Add(this.chkSignatureOnFile);
            this.pnl_Bottom.Controls.Add(this.chkExempt);
            this.pnl_Bottom.Controls.Add(this.chkdirective);
            this.pnl_Bottom.Location = new System.Drawing.Point(378, 118);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Size = new System.Drawing.Size(394, 46);
            this.pnl_Bottom.TabIndex = 63;
            // 
            // btnOtherDetails
            // 
            this.btnOtherDetails.AutoEllipsis = true;
            this.btnOtherDetails.BackColor = System.Drawing.Color.Transparent;
            this.btnOtherDetails.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOtherDetails.BackgroundImage")));
            this.btnOtherDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOtherDetails.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnOtherDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtherDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOtherDetails.Location = new System.Drawing.Point(301, 4);
            this.btnOtherDetails.Name = "btnOtherDetails";
            this.btnOtherDetails.Size = new System.Drawing.Size(88, 24);
            this.btnOtherDetails.TabIndex = 3;
            this.btnOtherDetails.Text = "Other Details";
            this.btnOtherDetails.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOtherDetails.UseVisualStyleBackColor = false;
            this.btnOtherDetails.Click += new System.EventHandler(this.btnOtherDetails_Click);
            this.btnOtherDetails.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnOtherDetails.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // chkYesNoLab
            // 
            this.chkYesNoLab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkYesNoLab.AutoSize = true;
            this.chkYesNoLab.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkYesNoLab.Location = new System.Drawing.Point(136, 23);
            this.chkYesNoLab.Name = "chkYesNoLab";
            this.chkYesNoLab.Size = new System.Drawing.Size(69, 18);
            this.chkYesNoLab.TabIndex = 5;
            this.chkYesNoLab.Text = "Yes Lab";
            this.chkYesNoLab.UseVisualStyleBackColor = true;
            this.chkYesNoLab.CheckedChanged += new System.EventHandler(this.chkYesNoLab_CheckedChanged);
            // 
            // chkSignatureOnFile
            // 
            this.chkSignatureOnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSignatureOnFile.AutoSize = true;
            this.chkSignatureOnFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSignatureOnFile.Location = new System.Drawing.Point(7, 23);
            this.chkSignatureOnFile.Name = "chkSignatureOnFile";
            this.chkSignatureOnFile.Size = new System.Drawing.Size(115, 18);
            this.chkSignatureOnFile.TabIndex = 4;
            this.chkSignatureOnFile.Text = "Signature on file";
            this.chkSignatureOnFile.UseVisualStyleBackColor = true;
            // 
            // chkExempt
            // 
            this.chkExempt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkExempt.AutoSize = true;
            this.chkExempt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExempt.Location = new System.Drawing.Point(136, 4);
            this.chkExempt.Name = "chkExempt";
            this.chkExempt.Size = new System.Drawing.Size(135, 18);
            this.chkExempt.TabIndex = 2;
            this.chkExempt.Text = "Exempt from report";
            this.chkExempt.UseVisualStyleBackColor = true;
            // 
            // chkdirective
            // 
            this.chkdirective.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkdirective.AutoSize = true;
            this.chkdirective.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdirective.Location = new System.Drawing.Point(7, 4);
            this.chkdirective.Name = "chkdirective";
            this.chkdirective.Size = new System.Drawing.Size(123, 18);
            this.chkdirective.TabIndex = 1;
            this.chkdirective.Text = "Advance directive";
            this.chkdirective.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(4, 165);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(772, 1);
            this.label19.TabIndex = 38;
            // 
            // txtPAProvider
            // 
            this.txtPAProvider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAProvider.Location = new System.Drawing.Point(99, 17);
            this.txtPAProvider.Name = "txtPAProvider";
            this.txtPAProvider.ReadOnly = true;
            this.txtPAProvider.Size = new System.Drawing.Size(223, 22);
            this.txtPAProvider.TabIndex = 37;
            // 
            // cmbPAPharma
            // 
            this.cmbPAPharma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPAPharma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPAPharma.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPAPharma.FormattingEnabled = true;
            this.cmbPAPharma.Location = new System.Drawing.Point(459, 17);
            this.cmbPAPharma.Name = "cmbPAPharma";
            this.cmbPAPharma.Size = new System.Drawing.Size(215, 22);
            this.cmbPAPharma.TabIndex = 40;
            // 
            // cmbPAReferrals
            // 
            this.cmbPAReferrals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPAReferrals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPAReferrals.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPAReferrals.FormattingEnabled = true;
            this.cmbPAReferrals.Location = new System.Drawing.Point(459, 42);
            this.cmbPAReferrals.Name = "cmbPAReferrals";
            this.cmbPAReferrals.Size = new System.Drawing.Size(215, 22);
            this.cmbPAReferrals.TabIndex = 46;
            // 
            // cmbPAOccupation
            // 
            this.cmbPAOccupation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPAOccupation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPAOccupation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPAOccupation.FormattingEnabled = true;
            this.cmbPAOccupation.Location = new System.Drawing.Point(99, 42);
            this.cmbPAOccupation.Name = "cmbPAOccupation";
            this.cmbPAOccupation.Size = new System.Drawing.Size(223, 22);
            this.cmbPAOccupation.TabIndex = 43;
            // 
            // cmbPALocation
            // 
            this.cmbPALocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPALocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPALocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPALocation.FormattingEnabled = true;
            this.cmbPALocation.Location = new System.Drawing.Point(99, 117);
            this.cmbPALocation.Name = "cmbPALocation";
            this.cmbPALocation.Size = new System.Drawing.Size(223, 22);
            this.cmbPALocation.TabIndex = 62;
            // 
            // cmbGaurdian
            // 
            this.cmbGaurdian.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbGaurdian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGaurdian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGaurdian.FormattingEnabled = true;
            this.cmbGaurdian.Location = new System.Drawing.Point(99, 67);
            this.cmbGaurdian.Name = "cmbGaurdian";
            this.cmbGaurdian.Size = new System.Drawing.Size(223, 22);
            this.cmbGaurdian.TabIndex = 50;
            // 
            // cmbGenInfoInsurance
            // 
            this.cmbGenInfoInsurance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbGenInfoInsurance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenInfoInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGenInfoInsurance.FormattingEnabled = true;
            this.cmbGenInfoInsurance.Location = new System.Drawing.Point(99, 92);
            this.cmbGenInfoInsurance.Name = "cmbGenInfoInsurance";
            this.cmbGenInfoInsurance.Size = new System.Drawing.Size(223, 22);
            this.cmbGenInfoInsurance.TabIndex = 56;
            // 
            // txtPAPrimaryCarePhy
            // 
            this.txtPAPrimaryCarePhy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAPrimaryCarePhy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAPrimaryCarePhy.Location = new System.Drawing.Point(513, 67);
            this.txtPAPrimaryCarePhy.Name = "txtPAPrimaryCarePhy";
            this.txtPAPrimaryCarePhy.ReadOnly = true;
            this.txtPAPrimaryCarePhy.Size = new System.Drawing.Size(158, 22);
            this.txtPAPrimaryCarePhy.TabIndex = 53;
            // 
            // txtPAPharma
            // 
            this.txtPAPharma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAPharma.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAPharma.Location = new System.Drawing.Point(459, 17);
            this.txtPAPharma.Name = "txtPAPharma";
            this.txtPAPharma.ReadOnly = true;
            this.txtPAPharma.Size = new System.Drawing.Size(215, 22);
            this.txtPAPharma.TabIndex = 40;
            this.txtPAPharma.Visible = false;
            // 
            // lblGeneralInfoOtherDetails
            // 
            this.lblGeneralInfoOtherDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGeneralInfoOtherDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGeneralInfoOtherDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneralInfoOtherDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGeneralInfoOtherDetails.Location = new System.Drawing.Point(4, 2);
            this.lblGeneralInfoOtherDetails.Name = "lblGeneralInfoOtherDetails";
            this.lblGeneralInfoOtherDetails.Size = new System.Drawing.Size(772, 13);
            this.lblGeneralInfoOtherDetails.TabIndex = 11;
            this.lblGeneralInfoOtherDetails.Text = " Other Details :";
            // 
            // btnOccupationDelete
            // 
            this.btnOccupationDelete.AutoEllipsis = true;
            this.btnOccupationDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnOccupationDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOccupationDelete.BackgroundImage")));
            this.btnOccupationDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOccupationDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOccupationDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnOccupationDelete.Image")));
            this.btnOccupationDelete.Location = new System.Drawing.Point(352, 42);
            this.btnOccupationDelete.Name = "btnOccupationDelete";
            this.btnOccupationDelete.Size = new System.Drawing.Size(22, 22);
            this.btnOccupationDelete.TabIndex = 45;
            this.btnOccupationDelete.UseVisualStyleBackColor = false;
            this.btnOccupationDelete.Click += new System.EventHandler(this.btnOccupationDelete_Click);
            this.btnOccupationDelete.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnOccupationDelete.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnGuardianSelect
            // 
            this.btnGuardianSelect.AutoEllipsis = true;
            this.btnGuardianSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnGuardianSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuardianSelect.BackgroundImage")));
            this.btnGuardianSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardianSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardianSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardianSelect.Image")));
            this.btnGuardianSelect.Location = new System.Drawing.Point(326, 67);
            this.btnGuardianSelect.Name = "btnGuardianSelect";
            this.btnGuardianSelect.Size = new System.Drawing.Size(22, 22);
            this.btnGuardianSelect.TabIndex = 51;
            this.btnGuardianSelect.UseVisualStyleBackColor = false;
            this.btnGuardianSelect.Click += new System.EventHandler(this.btnGuardianSelect_Click);
            this.btnGuardianSelect.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnGuardianSelect.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnGuardianDelete
            // 
            this.btnGuardianDelete.AutoEllipsis = true;
            this.btnGuardianDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnGuardianDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuardianDelete.BackgroundImage")));
            this.btnGuardianDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardianDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardianDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardianDelete.Image")));
            this.btnGuardianDelete.Location = new System.Drawing.Point(352, 67);
            this.btnGuardianDelete.Name = "btnGuardianDelete";
            this.btnGuardianDelete.Size = new System.Drawing.Size(22, 22);
            this.btnGuardianDelete.TabIndex = 52;
            this.btnGuardianDelete.UseVisualStyleBackColor = false;
            this.btnGuardianDelete.Click += new System.EventHandler(this.btnGuardianDelete_Click);
            this.btnGuardianDelete.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnGuardianDelete.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnClrInsurance
            // 
            this.btnClrInsurance.AutoEllipsis = true;
            this.btnClrInsurance.BackColor = System.Drawing.Color.Transparent;
            this.btnClrInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClrInsurance.BackgroundImage")));
            this.btnClrInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClrInsurance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClrInsurance.Image = ((System.Drawing.Image)(resources.GetObject("btnClrInsurance.Image")));
            this.btnClrInsurance.Location = new System.Drawing.Point(352, 92);
            this.btnClrInsurance.Name = "btnClrInsurance";
            this.btnClrInsurance.Size = new System.Drawing.Size(22, 22);
            this.btnClrInsurance.TabIndex = 58;
            this.btnClrInsurance.UseVisualStyleBackColor = false;
            this.btnClrInsurance.Click += new System.EventHandler(this.btnClrInsurance_Click);
            this.btnClrInsurance.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnClrInsurance.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnOccupationSelect
            // 
            this.btnOccupationSelect.AutoEllipsis = true;
            this.btnOccupationSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnOccupationSelect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOccupationSelect.BackgroundImage")));
            this.btnOccupationSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOccupationSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOccupationSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnOccupationSelect.Image")));
            this.btnOccupationSelect.Location = new System.Drawing.Point(326, 42);
            this.btnOccupationSelect.Name = "btnOccupationSelect";
            this.btnOccupationSelect.Size = new System.Drawing.Size(22, 22);
            this.btnOccupationSelect.TabIndex = 44;
            this.btnOccupationSelect.UseVisualStyleBackColor = false;
            this.btnOccupationSelect.Click += new System.EventHandler(this.btnOccupationSelect_Click);
            this.btnOccupationSelect.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnOccupationSelect.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnInsurInfo
            // 
            this.btnInsurInfo.AutoEllipsis = true;
            this.btnInsurInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnInsurInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsurInfo.BackgroundImage")));
            this.btnInsurInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInsurInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsurInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnInsurInfo.Image")));
            this.btnInsurInfo.Location = new System.Drawing.Point(326, 92);
            this.btnInsurInfo.Name = "btnInsurInfo";
            this.btnInsurInfo.Size = new System.Drawing.Size(22, 22);
            this.btnInsurInfo.TabIndex = 57;
            this.btnInsurInfo.UseVisualStyleBackColor = false;
            this.btnInsurInfo.Click += new System.EventHandler(this.btnInsurInfo_Click);
            this.btnInsurInfo.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnInsurInfo.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnProviderDelete
            // 
            this.btnProviderDelete.AutoEllipsis = true;
            this.btnProviderDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnProviderDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProviderDelete.BackgroundImage")));
            this.btnProviderDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProviderDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProviderDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnProviderDelete.Image")));
            this.btnProviderDelete.Location = new System.Drawing.Point(352, 17);
            this.btnProviderDelete.Name = "btnProviderDelete";
            this.btnProviderDelete.Size = new System.Drawing.Size(22, 22);
            this.btnProviderDelete.TabIndex = 39;
            this.btnProviderDelete.UseVisualStyleBackColor = false;
            this.btnProviderDelete.Click += new System.EventHandler(this.btnProviderDelete_Click);
            this.btnProviderDelete.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnProviderDelete.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_PAPharmaDel
            // 
            this.btn_PAPharmaDel.AutoEllipsis = true;
            this.btn_PAPharmaDel.BackColor = System.Drawing.Color.Transparent;
            this.btn_PAPharmaDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PAPharmaDel.BackgroundImage")));
            this.btn_PAPharmaDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PAPharmaDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PAPharmaDel.Image = ((System.Drawing.Image)(resources.GetObject("btn_PAPharmaDel.Image")));
            this.btn_PAPharmaDel.Location = new System.Drawing.Point(704, 17);
            this.btn_PAPharmaDel.Name = "btn_PAPharmaDel";
            this.btn_PAPharmaDel.Size = new System.Drawing.Size(22, 22);
            this.btn_PAPharmaDel.TabIndex = 42;
            this.btn_PAPharmaDel.UseVisualStyleBackColor = false;
            this.btn_PAPharmaDel.Click += new System.EventHandler(this.btn_PAPharmaDel_Click);
            this.btn_PAPharmaDel.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_PAPharmaDel.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btnProviderSelect
            // 
            this.btnProviderSelect.AutoEllipsis = true;
            this.btnProviderSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnProviderSelect.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnProviderSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnProviderSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProviderSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnProviderSelect.Image")));
            this.btnProviderSelect.Location = new System.Drawing.Point(326, 17);
            this.btnProviderSelect.Name = "btnProviderSelect";
            this.btnProviderSelect.Size = new System.Drawing.Size(22, 22);
            this.btnProviderSelect.TabIndex = 38;
            this.btnProviderSelect.UseVisualStyleBackColor = false;
            this.btnProviderSelect.Click += new System.EventHandler(this.btnProviderSelect_Click);
            this.btnProviderSelect.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btnProviderSelect.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_PrimaryCareDel
            // 
            this.btn_PrimaryCareDel.AutoEllipsis = true;
            this.btn_PrimaryCareDel.BackColor = System.Drawing.Color.Transparent;
            this.btn_PrimaryCareDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PrimaryCareDel.BackgroundImage")));
            this.btn_PrimaryCareDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PrimaryCareDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PrimaryCareDel.Image = ((System.Drawing.Image)(resources.GetObject("btn_PrimaryCareDel.Image")));
            this.btn_PrimaryCareDel.Location = new System.Drawing.Point(704, 67);
            this.btn_PrimaryCareDel.Name = "btn_PrimaryCareDel";
            this.btn_PrimaryCareDel.Size = new System.Drawing.Size(22, 22);
            this.btn_PrimaryCareDel.TabIndex = 55;
            this.btn_PrimaryCareDel.UseVisualStyleBackColor = false;
            this.btn_PrimaryCareDel.Click += new System.EventHandler(this.btn_PrimaryCareDel_Click);
            this.btn_PrimaryCareDel.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_PrimaryCareDel.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_PAReferralsDel
            // 
            this.btn_PAReferralsDel.AutoEllipsis = true;
            this.btn_PAReferralsDel.BackColor = System.Drawing.Color.Transparent;
            this.btn_PAReferralsDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PAReferralsDel.BackgroundImage")));
            this.btn_PAReferralsDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PAReferralsDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PAReferralsDel.Image = ((System.Drawing.Image)(resources.GetObject("btn_PAReferralsDel.Image")));
            this.btn_PAReferralsDel.Location = new System.Drawing.Point(704, 42);
            this.btn_PAReferralsDel.Name = "btn_PAReferralsDel";
            this.btn_PAReferralsDel.Size = new System.Drawing.Size(22, 22);
            this.btn_PAReferralsDel.TabIndex = 48;
            this.btn_PAReferralsDel.UseVisualStyleBackColor = false;
            this.btn_PAReferralsDel.Click += new System.EventHandler(this.btn_PAReferralsDel_Click);
            this.btn_PAReferralsDel.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_PAReferralsDel.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_PrimaryCareBr
            // 
            this.btn_PrimaryCareBr.AutoEllipsis = true;
            this.btn_PrimaryCareBr.BackColor = System.Drawing.Color.Transparent;
            this.btn_PrimaryCareBr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PrimaryCareBr.BackgroundImage")));
            this.btn_PrimaryCareBr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PrimaryCareBr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PrimaryCareBr.Image = ((System.Drawing.Image)(resources.GetObject("btn_PrimaryCareBr.Image")));
            this.btn_PrimaryCareBr.Location = new System.Drawing.Point(678, 67);
            this.btn_PrimaryCareBr.Name = "btn_PrimaryCareBr";
            this.btn_PrimaryCareBr.Size = new System.Drawing.Size(22, 22);
            this.btn_PrimaryCareBr.TabIndex = 54;
            this.btn_PrimaryCareBr.UseVisualStyleBackColor = false;
            this.btn_PrimaryCareBr.Click += new System.EventHandler(this.btn_PrimaryCareBr_Click);
            this.btn_PrimaryCareBr.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_PrimaryCareBr.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_PAPharmaBr
            // 
            this.btn_PAPharmaBr.AutoEllipsis = true;
            this.btn_PAPharmaBr.BackColor = System.Drawing.Color.Transparent;
            this.btn_PAPharmaBr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PAPharmaBr.BackgroundImage")));
            this.btn_PAPharmaBr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PAPharmaBr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PAPharmaBr.Image = ((System.Drawing.Image)(resources.GetObject("btn_PAPharmaBr.Image")));
            this.btn_PAPharmaBr.Location = new System.Drawing.Point(678, 17);
            this.btn_PAPharmaBr.Name = "btn_PAPharmaBr";
            this.btn_PAPharmaBr.Size = new System.Drawing.Size(22, 22);
            this.btn_PAPharmaBr.TabIndex = 41;
            this.btn_PAPharmaBr.UseVisualStyleBackColor = false;
            this.btn_PAPharmaBr.Click += new System.EventHandler(this.btn_PAPharmaBr_Click);
            this.btn_PAPharmaBr.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_PAPharmaBr.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_PAReferralsBr
            // 
            this.btn_PAReferralsBr.AutoEllipsis = true;
            this.btn_PAReferralsBr.BackColor = System.Drawing.Color.Transparent;
            this.btn_PAReferralsBr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PAReferralsBr.BackgroundImage")));
            this.btn_PAReferralsBr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PAReferralsBr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PAReferralsBr.Image = ((System.Drawing.Image)(resources.GetObject("btn_PAReferralsBr.Image")));
            this.btn_PAReferralsBr.Location = new System.Drawing.Point(678, 42);
            this.btn_PAReferralsBr.Name = "btn_PAReferralsBr";
            this.btn_PAReferralsBr.Size = new System.Drawing.Size(22, 22);
            this.btn_PAReferralsBr.TabIndex = 47;
            this.btn_PAReferralsBr.UseVisualStyleBackColor = false;
            this.btn_PAReferralsBr.Click += new System.EventHandler(this.btn_PAReferralsBr_Click);
            this.btn_PAReferralsBr.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_PAReferralsBr.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // lblOccupation
            // 
            this.lblOccupation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOccupation.AutoEllipsis = true;
            this.lblOccupation.AutoSize = true;
            this.lblOccupation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccupation.Location = new System.Drawing.Point(21, 46);
            this.lblOccupation.Name = "lblOccupation";
            this.lblOccupation.Size = new System.Drawing.Size(77, 14);
            this.lblOccupation.TabIndex = 4;
            this.lblOccupation.Text = "Occupation :";
            // 
            // lblLocation
            // 
            this.lblLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocation.AutoEllipsis = true;
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(37, 121);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 14);
            this.lblLocation.TabIndex = 29;
            this.lblLocation.Text = "Location :";
            // 
            // lblGaurdian
            // 
            this.lblGaurdian.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGaurdian.AutoEllipsis = true;
            this.lblGaurdian.AutoSize = true;
            this.lblGaurdian.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGaurdian.Location = new System.Drawing.Point(36, 71);
            this.lblGaurdian.Name = "lblGaurdian";
            this.lblGaurdian.Size = new System.Drawing.Size(62, 14);
            this.lblGaurdian.TabIndex = 8;
            this.lblGaurdian.Text = "Guardian :";
            // 
            // lblInsurance
            // 
            this.lblInsurance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInsurance.AutoEllipsis = true;
            this.lblInsurance.AutoSize = true;
            this.lblInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsurance.Location = new System.Drawing.Point(30, 96);
            this.lblInsurance.Name = "lblInsurance";
            this.lblInsurance.Size = new System.Drawing.Size(68, 14);
            this.lblInsurance.TabIndex = 31;
            this.lblInsurance.Text = "Insurance :";
            // 
            // lblPatientCare
            // 
            this.lblPatientCare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientCare.AutoEllipsis = true;
            this.lblPatientCare.AutoSize = true;
            this.lblPatientCare.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCare.Location = new System.Drawing.Point(377, 71);
            this.lblPatientCare.Name = "lblPatientCare";
            this.lblPatientCare.Size = new System.Drawing.Size(134, 14);
            this.lblPatientCare.TabIndex = 25;
            this.lblPatientCare.Text = "Primary Care Physician :";
            // 
            // lbPatientReferrals
            // 
            this.lbPatientReferrals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatientReferrals.AutoEllipsis = true;
            this.lbPatientReferrals.AutoSize = true;
            this.lbPatientReferrals.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientReferrals.Location = new System.Drawing.Point(396, 46);
            this.lbPatientReferrals.Name = "lbPatientReferrals";
            this.lbPatientReferrals.Size = new System.Drawing.Size(61, 14);
            this.lbPatientReferrals.TabIndex = 21;
            this.lbPatientReferrals.Text = "Referrals :";
            // 
            // lblDoctor
            // 
            this.lblDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDoctor.AutoEllipsis = true;
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoctor.Location = new System.Drawing.Point(39, 21);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(59, 14);
            this.lblDoctor.TabIndex = 0;
            this.lblDoctor.Text = "Provider :";
            // 
            // lbPatientPharma
            // 
            this.lbPatientPharma.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatientPharma.AutoEllipsis = true;
            this.lbPatientPharma.AutoSize = true;
            this.lbPatientPharma.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientPharma.Location = new System.Drawing.Point(390, 21);
            this.lbPatientPharma.Name = "lbPatientPharma";
            this.lbPatientPharma.Size = new System.Drawing.Size(67, 14);
            this.lbPatientPharma.TabIndex = 17;
            this.lbPatientPharma.Text = "Pharmacy :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1, 164);
            this.label4.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(776, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1, 164);
            this.label7.TabIndex = 36;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(3, 1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(774, 1);
            this.label18.TabIndex = 37;
            // 
            // label41
            // 
            this.label41.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label41.AutoEllipsis = true;
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.Red;
            this.label41.Location = new System.Drawing.Point(25, 21);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(14, 14);
            this.label41.TabIndex = 64;
            this.label41.Text = "*";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label48);
            this.panel2.Controls.Add(this.label42);
            this.panel2.Controls.Add(this.label43);
            this.panel2.Controls.Add(this.label44);
            this.panel2.Controls.Add(this.label45);
            this.panel2.Controls.Add(this.label46);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 378);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Size = new System.Drawing.Size(780, 0);
            this.panel2.TabIndex = 68;
            // 
            // label48
            // 
            this.label48.AutoEllipsis = true;
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.Red;
            this.label48.Location = new System.Drawing.Point(5, 3);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(25, 13);
            this.label48.TabIndex = 33;
            this.label48.Text = "    *";
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label42.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label42.Location = new System.Drawing.Point(4, -4);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(772, 1);
            this.label42.TabIndex = 12;
            this.label42.Text = "label2";
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(3, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 0);
            this.label43.TabIndex = 11;
            this.label43.Text = "label4";
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label44.Location = new System.Drawing.Point(776, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 0);
            this.label44.TabIndex = 10;
            this.label44.Text = "label3";
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label45.Dock = System.Windows.Forms.DockStyle.Top;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(3, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(774, 1);
            this.label45.TabIndex = 9;
            this.label45.Text = "label1";
            // 
            // label46
            // 
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(20, 1);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(106, 18);
            this.label46.TabIndex = 31;
            this.label46.Text = "Required fields ";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_AddressDetails
            // 
            this.pnl_AddressDetails.Controls.Add(this.pnlEmergencyConDetails);
            this.pnl_AddressDetails.Controls.Add(this.pnlConDetails);
            this.pnl_AddressDetails.Controls.Add(this.pnlAddDetails);
            this.pnl_AddressDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_AddressDetails.Location = new System.Drawing.Point(0, 226);
            this.pnl_AddressDetails.Name = "pnl_AddressDetails";
            this.pnl_AddressDetails.Size = new System.Drawing.Size(780, 152);
            this.pnl_AddressDetails.TabIndex = 1;
            // 
            // pnlEmergencyConDetails
            // 
            this.pnlEmergencyConDetails.Controls.Add(this.cmbRelationship);
            this.pnlEmergencyConDetails.Controls.Add(this.mtxtEmergencyMobile);
            this.pnlEmergencyConDetails.Controls.Add(this.label47);
            this.pnlEmergencyConDetails.Controls.Add(this.mtxtEmergencyPhone);
            this.pnlEmergencyConDetails.Controls.Add(this.label30);
            this.pnlEmergencyConDetails.Controls.Add(this.txtEmergencyContact);
            this.pnlEmergencyConDetails.Controls.Add(this.label32);
            this.pnlEmergencyConDetails.Controls.Add(this.label33);
            this.pnlEmergencyConDetails.Controls.Add(this.label34);
            this.pnlEmergencyConDetails.Controls.Add(this.label35);
            this.pnlEmergencyConDetails.Controls.Add(this.label36);
            this.pnlEmergencyConDetails.Controls.Add(this.label37);
            this.pnlEmergencyConDetails.Controls.Add(this.label38);
            this.pnlEmergencyConDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEmergencyConDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlEmergencyConDetails.Location = new System.Drawing.Point(356, 85);
            this.pnlEmergencyConDetails.Name = "pnlEmergencyConDetails";
            this.pnlEmergencyConDetails.Padding = new System.Windows.Forms.Padding(0, 1, 3, 3);
            this.pnlEmergencyConDetails.Size = new System.Drawing.Size(424, 67);
            this.pnlEmergencyConDetails.TabIndex = 31;
            // 
            // cmbRelationship
            // 
            this.cmbRelationship.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelationship.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRelationship.FormattingEnabled = true;
            this.cmbRelationship.Location = new System.Drawing.Point(80, 40);
            this.cmbRelationship.Name = "cmbRelationship";
            this.cmbRelationship.Size = new System.Drawing.Size(190, 22);
            this.cmbRelationship.TabIndex = 34;
            this.cmbRelationship.SelectedIndexChanged += new System.EventHandler(this.cmbRelationship_SelectedIndexChanged);
            // 
            // mtxtEmergencyMobile
            // 
            this.mtxtEmergencyMobile.AllowValidate = true;
            this.mtxtEmergencyMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtEmergencyMobile.IncludeLiteralsAndPrompts = false;
            this.mtxtEmergencyMobile.Location = new System.Drawing.Point(319, 40);
            this.mtxtEmergencyMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mtxtEmergencyMobile.Name = "mtxtEmergencyMobile";
            this.mtxtEmergencyMobile.ReadOnly = false;
            this.mtxtEmergencyMobile.Size = new System.Drawing.Size(94, 22);
            this.mtxtEmergencyMobile.TabIndex = 35;
            // 
            // label47
            // 
            this.label47.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label47.AutoEllipsis = true;
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(1, 44);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(79, 14);
            this.label47.TabIndex = 2;
            this.label47.Text = "Relationship :";
            // 
            // mtxtEmergencyPhone
            // 
            this.mtxtEmergencyPhone.AllowValidate = true;
            this.mtxtEmergencyPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtEmergencyPhone.IncludeLiteralsAndPrompts = false;
            this.mtxtEmergencyPhone.Location = new System.Drawing.Point(319, 16);
            this.mtxtEmergencyPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxtEmergencyPhone.Name = "mtxtEmergencyPhone";
            this.mtxtEmergencyPhone.ReadOnly = false;
            this.mtxtEmergencyPhone.Size = new System.Drawing.Size(94, 22);
            this.mtxtEmergencyPhone.TabIndex = 33;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label30.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(1, 63);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(419, 1);
            this.label30.TabIndex = 28;
            // 
            // txtEmergencyContact
            // 
            this.txtEmergencyContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtEmergencyContact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmergencyContact.Location = new System.Drawing.Point(80, 16);
            this.txtEmergencyContact.MaxLength = 50;
            this.txtEmergencyContact.Name = "txtEmergencyContact";
            this.txtEmergencyContact.Size = new System.Drawing.Size(190, 22);
            this.txtEmergencyContact.TabIndex = 32;
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label32.AutoEllipsis = true;
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(22, 20);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(58, 14);
            this.label32.TabIndex = 4;
            this.label32.Text = "Contact :";
            // 
            // label33
            // 
            this.label33.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label33.AutoEllipsis = true;
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(271, 44);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(49, 14);
            this.label33.TabIndex = 2;
            this.label33.Text = "Mobile :";
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoEllipsis = true;
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(270, 20);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(50, 14);
            this.label34.TabIndex = 0;
            this.label34.Text = "Phone :";
            // 
            // label35
            // 
            this.label35.Dock = System.Windows.Forms.DockStyle.Top;
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(1, 2);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(419, 14);
            this.label35.TabIndex = 8;
            this.label35.Text = "Emergency Contact Details  :";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Right;
            this.label36.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(420, 2);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 62);
            this.label36.TabIndex = 27;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(1, 1);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(420, 1);
            this.label37.TabIndex = 29;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Left;
            this.label38.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(0, 1);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(1, 63);
            this.label38.TabIndex = 37;
            // 
            // pnlConDetails
            // 
            this.pnlConDetails.Controls.Add(this.pnlAPIActivationEmail);
            this.pnlConDetails.Controls.Add(this.pnlAPIAccount);
            this.pnlConDetails.Controls.Add(this.pnlPortalInvitaitonEmail);
            this.pnlConDetails.Controls.Add(this.pnlPortalAccount);
            this.pnlConDetails.Controls.Add(this.mtxtPAMobile);
            this.pnlConDetails.Controls.Add(this.mtxtPAPhone);
            this.pnlConDetails.Controls.Add(this.label22);
            this.pnlConDetails.Controls.Add(this.txtPAEmail);
            this.pnlConDetails.Controls.Add(this.txtPAFax);
            this.pnlConDetails.Controls.Add(this.lblEmail);
            this.pnlConDetails.Controls.Add(this.lblFax);
            this.pnlConDetails.Controls.Add(this.lblMobile);
            this.pnlConDetails.Controls.Add(this.lblPhone);
            this.pnlConDetails.Controls.Add(this.lblContactDetails);
            this.pnlConDetails.Controls.Add(this.label8);
            this.pnlConDetails.Controls.Add(this.label23);
            this.pnlConDetails.Controls.Add(this.label25);
            this.pnlConDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlConDetails.Location = new System.Drawing.Point(356, 0);
            this.pnlConDetails.Name = "pnlConDetails";
            this.pnlConDetails.Padding = new System.Windows.Forms.Padding(0, 1, 3, 3);
            this.pnlConDetails.Size = new System.Drawing.Size(424, 85);
            this.pnlConDetails.TabIndex = 27;
            // 
            // pnlAPIActivationEmail
            // 
            this.pnlAPIActivationEmail.Controls.Add(this.cbSendAPIInvitation);
            this.pnlAPIActivationEmail.Location = new System.Drawing.Point(234, 62);
            this.pnlAPIActivationEmail.Name = "pnlAPIActivationEmail";
            this.pnlAPIActivationEmail.Size = new System.Drawing.Size(142, 16);
            this.pnlAPIActivationEmail.TabIndex = 33;
            this.pnlAPIActivationEmail.Visible = false;
            // 
            // cbSendAPIInvitation
            // 
            this.cbSendAPIInvitation.AutoSize = true;
            this.cbSendAPIInvitation.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbSendAPIInvitation.Location = new System.Drawing.Point(0, 0);
            this.cbSendAPIInvitation.Name = "cbSendAPIInvitation";
            this.cbSendAPIInvitation.Size = new System.Drawing.Size(135, 16);
            this.cbSendAPIInvitation.TabIndex = 0;
            this.cbSendAPIInvitation.Text = "API Activation";
            this.cbSendAPIInvitation.UseVisualStyleBackColor = true;
            // 
            // pnlPortalInvitaitonEmail
            // 
            this.pnlPortalInvitaitonEmail.Controls.Add(this.cbSendPatientPortalActivationEmail);
            this.pnlPortalInvitaitonEmail.Location = new System.Drawing.Point(10, 62);
            this.pnlPortalInvitaitonEmail.Name = "pnlPortalInvitaitonEmail";
            this.pnlPortalInvitaitonEmail.Size = new System.Drawing.Size(190, 16);
            this.pnlPortalInvitaitonEmail.TabIndex = 32;
            this.pnlPortalInvitaitonEmail.Visible = false;
            // 
            // cbSendPatientPortalActivationEmail
            // 
            this.cbSendPatientPortalActivationEmail.AutoSize = true;
            this.cbSendPatientPortalActivationEmail.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbSendPatientPortalActivationEmail.Location = new System.Drawing.Point(0, 0);
            this.cbSendPatientPortalActivationEmail.Name = "cbSendPatientPortalActivationEmail";
            this.cbSendPatientPortalActivationEmail.Size = new System.Drawing.Size(187, 16);
            this.cbSendPatientPortalActivationEmail.TabIndex = 0;
            this.cbSendPatientPortalActivationEmail.Text = "Send Patient Portal Invitation";
            this.cbSendPatientPortalActivationEmail.UseVisualStyleBackColor = true;
            // 
            // pnlPortalAccount
            // 
            this.pnlPortalAccount.Controls.Add(this.lblPatientPortalAccountStatus);
            this.pnlPortalAccount.Controls.Add(this.lblPortalAccountStatus);
            this.pnlPortalAccount.Controls.Add(this.btnPatientPortalAccountStatus);
            this.pnlPortalAccount.Location = new System.Drawing.Point(9, 59);
            this.pnlPortalAccount.Name = "pnlPortalAccount";
            this.pnlPortalAccount.Size = new System.Drawing.Size(182, 21);
            this.pnlPortalAccount.TabIndex = 31;
            this.pnlPortalAccount.Visible = false;
            // 
            // lblPatientPortalAccountStatus
            // 
            this.lblPatientPortalAccountStatus.AutoEllipsis = true;
            this.lblPatientPortalAccountStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPatientPortalAccountStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientPortalAccountStatus.Location = new System.Drawing.Point(85, 0);
            this.lblPatientPortalAccountStatus.Name = "lblPatientPortalAccountStatus";
            this.lblPatientPortalAccountStatus.Size = new System.Drawing.Size(69, 21);
            this.lblPatientPortalAccountStatus.TabIndex = 81;
            this.lblPatientPortalAccountStatus.Text = "Not Invited";
            this.lblPatientPortalAccountStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPortalAccountStatus
            // 
            this.lblPortalAccountStatus.AutoEllipsis = true;
            this.lblPortalAccountStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPortalAccountStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortalAccountStatus.Location = new System.Drawing.Point(0, 0);
            this.lblPortalAccountStatus.Name = "lblPortalAccountStatus";
            this.lblPortalAccountStatus.Size = new System.Drawing.Size(85, 21);
            this.lblPortalAccountStatus.TabIndex = 82;
            this.lblPortalAccountStatus.Text = "Portal Status :";
            this.lblPortalAccountStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPatientPortalAccountStatus
            // 
            this.btnPatientPortalAccountStatus.AutoEllipsis = true;
            this.btnPatientPortalAccountStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnPatientPortalAccountStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPatientPortalAccountStatus.BackgroundImage")));
            this.btnPatientPortalAccountStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPatientPortalAccountStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPatientPortalAccountStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientPortalAccountStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnPatientPortalAccountStatus.Image")));
            this.btnPatientPortalAccountStatus.Location = new System.Drawing.Point(160, 0);
            this.btnPatientPortalAccountStatus.Name = "btnPatientPortalAccountStatus";
            this.btnPatientPortalAccountStatus.Size = new System.Drawing.Size(22, 21);
            this.btnPatientPortalAccountStatus.TabIndex = 80;
            this.toolTip1.SetToolTip(this.btnPatientPortalAccountStatus, "Portal Account");
            this.btnPatientPortalAccountStatus.UseVisualStyleBackColor = false;
            this.btnPatientPortalAccountStatus.Click += new System.EventHandler(this.btnPatientPortalAccountStatus_Click);
            // 
            // mtxtPAMobile
            // 
            this.mtxtPAMobile.AllowValidate = true;
            this.mtxtPAMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPAMobile.IncludeLiteralsAndPrompts = false;
            this.mtxtPAMobile.Location = new System.Drawing.Point(318, 15);
            this.mtxtPAMobile.MaskType = gloMaskControl.gloMaskType.Mobile;
            this.mtxtPAMobile.Name = "mtxtPAMobile";
            this.mtxtPAMobile.ReadOnly = false;
            this.mtxtPAMobile.Size = new System.Drawing.Size(94, 22);
            this.mtxtPAMobile.TabIndex = 28;
            // 
            // mtxtPAPhone
            // 
            this.mtxtPAPhone.AllowValidate = true;
            this.mtxtPAPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPAPhone.IncludeLiteralsAndPrompts = false;
            this.mtxtPAPhone.Location = new System.Drawing.Point(59, 15);
            this.mtxtPAPhone.MaskType = gloMaskControl.gloMaskType.Phone;
            this.mtxtPAPhone.Name = "mtxtPAPhone";
            this.mtxtPAPhone.ReadOnly = false;
            this.mtxtPAPhone.Size = new System.Drawing.Size(94, 22);
            this.mtxtPAPhone.TabIndex = 27;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(1, 81);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(419, 1);
            this.label22.TabIndex = 28;
            // 
            // txtPAEmail
            // 
            this.txtPAEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAEmail.Location = new System.Drawing.Point(59, 37);
            this.txtPAEmail.MaxLength = 50;
            this.txtPAEmail.Name = "txtPAEmail";
            this.txtPAEmail.Size = new System.Drawing.Size(208, 22);
            this.txtPAEmail.TabIndex = 29;
            this.txtPAEmail.TextChanged += new System.EventHandler(this.txtPAEmail_TextChanged);
            this.txtPAEmail.Validating += new System.ComponentModel.CancelEventHandler(this.txtPAEmail_Validating);
            // 
            // txtPAFax
            // 
            this.txtPAFax.AllowValidate = true;
            this.txtPAFax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAFax.IncludeLiteralsAndPrompts = false;
            this.txtPAFax.Location = new System.Drawing.Point(318, 37);
            this.txtPAFax.MaskType = gloMaskControl.gloMaskType.Fax;
            this.txtPAFax.Name = "txtPAFax";
            this.txtPAFax.ReadOnly = false;
            this.txtPAFax.Size = new System.Drawing.Size(94, 22);
            this.txtPAFax.TabIndex = 30;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmail.AutoEllipsis = true;
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(14, 41);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(42, 14);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email :";
            // 
            // lblFax
            // 
            this.lblFax.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFax.AutoEllipsis = true;
            this.lblFax.AutoSize = true;
            this.lblFax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFax.Location = new System.Drawing.Point(282, 41);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new System.Drawing.Size(33, 14);
            this.lblFax.TabIndex = 4;
            this.lblFax.Text = "Fax :";
            // 
            // lblMobile
            // 
            this.lblMobile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMobile.AutoEllipsis = true;
            this.lblMobile.AutoSize = true;
            this.lblMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobile.Location = new System.Drawing.Point(266, 19);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(49, 14);
            this.lblMobile.TabIndex = 2;
            this.lblMobile.Text = "Mobile :";
            // 
            // lblPhone
            // 
            this.lblPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhone.AutoEllipsis = true;
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(6, 19);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(50, 14);
            this.lblPhone.TabIndex = 0;
            this.lblPhone.Text = "Phone :";
            // 
            // lblContactDetails
            // 
            this.lblContactDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblContactDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblContactDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactDetails.Location = new System.Drawing.Point(1, 2);
            this.lblContactDetails.Name = "lblContactDetails";
            this.lblContactDetails.Size = new System.Drawing.Size(419, 12);
            this.lblContactDetails.TabIndex = 8;
            this.lblContactDetails.Text = " Contact Details :";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Right;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(420, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(1, 80);
            this.label8.TabIndex = 27;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(1, 1);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(420, 1);
            this.label23.TabIndex = 29;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(0, 1);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 81);
            this.label25.TabIndex = 37;
            // 
            // pnlAPIAccount
            // 
            this.pnlAPIAccount.Controls.Add(this.lblAPIActivationStatus);
            this.pnlAPIAccount.Controls.Add(this.label67);
            this.pnlAPIAccount.Controls.Add(this.btnAPIActivation);
            this.pnlAPIAccount.Location = new System.Drawing.Point(221, 59);
            this.pnlAPIAccount.Name = "pnlAPIAccount";
            this.pnlAPIAccount.Size = new System.Drawing.Size(191, 21);
            this.pnlAPIAccount.TabIndex = 83;
            // 
            // lblAPIActivationStatus
            // 
            this.lblAPIActivationStatus.AutoEllipsis = true;
            this.lblAPIActivationStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAPIActivationStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAPIActivationStatus.Location = new System.Drawing.Point(75, 0);
            this.lblAPIActivationStatus.Name = "lblAPIActivationStatus";
            this.lblAPIActivationStatus.Size = new System.Drawing.Size(91, 21);
            this.lblAPIActivationStatus.TabIndex = 81;
            this.lblAPIActivationStatus.Text = "Not Activated";
            this.lblAPIActivationStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label67
            // 
            this.label67.AutoEllipsis = true;
            this.label67.Dock = System.Windows.Forms.DockStyle.Left;
            this.label67.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(0, 0);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(75, 21);
            this.label67.TabIndex = 82;
            this.label67.Text = "API Status :";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAPIActivation
            // 
            this.btnAPIActivation.AutoEllipsis = true;
            this.btnAPIActivation.BackColor = System.Drawing.Color.Transparent;
            this.btnAPIActivation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAPIActivation.BackgroundImage")));
            this.btnAPIActivation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAPIActivation.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAPIActivation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAPIActivation.Image = ((System.Drawing.Image)(resources.GetObject("btnAPIActivation.Image")));
            this.btnAPIActivation.Location = new System.Drawing.Point(169, 0);
            this.btnAPIActivation.Name = "btnAPIActivation";
            this.btnAPIActivation.Size = new System.Drawing.Size(22, 21);
            this.btnAPIActivation.TabIndex = 80;
            this.toolTip1.SetToolTip(this.btnAPIActivation, "API Activation");
            this.btnAPIActivation.UseVisualStyleBackColor = false;

            this.btnAPIActivation.Click += new System.EventHandler(this.btnAPIActivation_Click);

            // 
            // pnlAddDetails
            // 
            this.pnlAddDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlAddDetails.Controls.Add(this.pnlAddresssControl);
            this.pnlAddDetails.Controls.Add(this.lblAddr1);
            this.pnlAddDetails.Controls.Add(this.label17);
            this.pnlAddDetails.Controls.Add(this.cmbPACountry);
            this.pnlAddDetails.Controls.Add(this.cmbPAState);
            this.pnlAddDetails.Controls.Add(this.txtPAZip);
            this.pnlAddDetails.Controls.Add(this.txtPACounty);
            this.pnlAddDetails.Controls.Add(this.txtPACity);
            this.pnlAddDetails.Controls.Add(this.txtPAAddress2);
            this.pnlAddDetails.Controls.Add(this.label49);
            this.pnlAddDetails.Controls.Add(this.txtPAAddress1);
            this.pnlAddDetails.Controls.Add(this.lblCountry);
            this.pnlAddDetails.Controls.Add(this.lblZip);
            this.pnlAddDetails.Controls.Add(this.lblState);
            this.pnlAddDetails.Controls.Add(this.lblCity);
            this.pnlAddDetails.Controls.Add(this.lblAddr2);
            this.pnlAddDetails.Controls.Add(this.lblAddressDetails);
            this.pnlAddDetails.Controls.Add(this.label2);
            this.pnlAddDetails.Controls.Add(this.label16);
            this.pnlAddDetails.Controls.Add(this.label24);
            this.pnlAddDetails.Controls.Add(this.label5);
            this.pnlAddDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAddDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlAddDetails.Name = "pnlAddDetails";
            this.pnlAddDetails.Padding = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.pnlAddDetails.Size = new System.Drawing.Size(356, 152);
            this.pnlAddDetails.TabIndex = 20;
            // 
            // pnlAddresssControl
            // 
            this.pnlAddresssControl.Location = new System.Drawing.Point(10, 18);
            this.pnlAddresssControl.Name = "pnlAddresssControl";
            this.pnlAddresssControl.Size = new System.Drawing.Size(331, 127);
            this.pnlAddresssControl.TabIndex = 107;
            // 
            // lblAddr1
            // 
            this.lblAddr1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddr1.AutoEllipsis = true;
            this.lblAddr1.AutoSize = true;
            this.lblAddr1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddr1.Location = new System.Drawing.Point(7, 23);
            this.lblAddr1.Name = "lblAddr1";
            this.lblAddr1.Size = new System.Drawing.Size(69, 14);
            this.lblAddr1.TabIndex = 1;
            this.lblAddr1.Text = "Address 1 :";
            this.lblAddr1.Visible = false;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(352, 147);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(0, 1);
            this.label17.TabIndex = 110;
            // 
            // cmbPACountry
            // 
            this.cmbPACountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPACountry.FormattingEnabled = true;
            this.cmbPACountry.Location = new System.Drawing.Point(225, 97);
            this.cmbPACountry.MaxDropDownItems = 3;
            this.cmbPACountry.MaxLength = 20;
            this.cmbPACountry.Name = "cmbPACountry";
            this.cmbPACountry.Size = new System.Drawing.Size(91, 22);
            this.cmbPACountry.TabIndex = 26;
            this.cmbPACountry.Visible = false;
            // 
            // cmbPAState
            // 
            this.cmbPAState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPAState.FormattingEnabled = true;
            this.cmbPAState.Location = new System.Drawing.Point(274, 71);
            this.cmbPAState.MaxLength = 20;
            this.cmbPAState.Name = "cmbPAState";
            this.cmbPAState.Size = new System.Drawing.Size(42, 22);
            this.cmbPAState.TabIndex = 25;
            this.cmbPAState.Visible = false;
            // 
            // txtPAZip
            // 
            this.txtPAZip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAZip.Location = new System.Drawing.Point(79, 97);
            this.txtPAZip.MaxLength = 10;
            this.txtPAZip.Name = "txtPAZip";
            this.txtPAZip.Size = new System.Drawing.Size(78, 22);
            this.txtPAZip.TabIndex = 23;
            this.txtPAZip.Visible = false;
            this.txtPAZip.TextChanged += new System.EventHandler(this.txtZip_TextChanged);
            this.txtPAZip.Enter += new System.EventHandler(this.txtZip_GotFocus);
            this.txtPAZip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtZip_KeyDown);
            this.txtPAZip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZip_KeyPress);
            this.txtPAZip.Leave += new System.EventHandler(this.txtZip_LostFocus);
            // 
            // txtPACounty
            // 
            this.txtPACounty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPACounty.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPACounty.Location = new System.Drawing.Point(79, 123);
            this.txtPACounty.MaxLength = 50;
            this.txtPACounty.Name = "txtPACounty";
            this.txtPACounty.Size = new System.Drawing.Size(147, 22);
            this.txtPACounty.TabIndex = 27;
            this.txtPACounty.Visible = false;
            // 
            // txtPACity
            // 
            this.txtPACity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPACity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPACity.Location = new System.Drawing.Point(79, 71);
            this.txtPACity.MaxLength = 50;
            this.txtPACity.Name = "txtPACity";
            this.txtPACity.Size = new System.Drawing.Size(147, 22);
            this.txtPACity.TabIndex = 24;
            this.txtPACity.Visible = false;
            // 
            // txtPAAddress2
            // 
            this.txtPAAddress2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAAddress2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAAddress2.Location = new System.Drawing.Point(79, 45);
            this.txtPAAddress2.MaxLength = 50;
            this.txtPAAddress2.Name = "txtPAAddress2";
            this.txtPAAddress2.Size = new System.Drawing.Size(237, 22);
            this.txtPAAddress2.TabIndex = 22;
            this.txtPAAddress2.Visible = false;
            // 
            // label49
            // 
            this.label49.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label49.AutoEllipsis = true;
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(165, 101);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(58, 14);
            this.label49.TabIndex = 7;
            this.label49.Text = "Country :";
            this.label49.Visible = false;
            // 
            // txtPAAddress1
            // 
            this.txtPAAddress1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAAddress1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAAddress1.Location = new System.Drawing.Point(79, 19);
            this.txtPAAddress1.MaxLength = 100;
            this.txtPAAddress1.Name = "txtPAAddress1";
            this.txtPAAddress1.Size = new System.Drawing.Size(237, 22);
            this.txtPAAddress1.TabIndex = 21;
            this.txtPAAddress1.Visible = false;
            // 
            // lblCountry
            // 
            this.lblCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountry.AutoEllipsis = true;
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.Location = new System.Drawing.Point(22, 127);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(54, 14);
            this.lblCountry.TabIndex = 11;
            this.lblCountry.Text = "County :";
            this.lblCountry.Visible = false;
            // 
            // lblZip
            // 
            this.lblZip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblZip.AutoEllipsis = true;
            this.lblZip.AutoSize = true;
            this.lblZip.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZip.Location = new System.Drawing.Point(45, 101);
            this.lblZip.Name = "lblZip";
            this.lblZip.Size = new System.Drawing.Size(31, 14);
            this.lblZip.TabIndex = 7;
            this.lblZip.Text = "Zip :";
            this.lblZip.Visible = false;
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.AutoEllipsis = true;
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(229, 75);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(45, 14);
            this.lblState.TabIndex = 9;
            this.lblState.Text = "State :";
            this.lblState.Visible = false;
            // 
            // lblCity
            // 
            this.lblCity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCity.AutoEllipsis = true;
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCity.Location = new System.Drawing.Point(41, 75);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(35, 14);
            this.lblCity.TabIndex = 5;
            this.lblCity.Text = "City :";
            this.lblCity.Visible = false;
            // 
            // lblAddr2
            // 
            this.lblAddr2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddr2.AutoEllipsis = true;
            this.lblAddr2.AutoSize = true;
            this.lblAddr2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddr2.Location = new System.Drawing.Point(7, 49);
            this.lblAddr2.Name = "lblAddr2";
            this.lblAddr2.Size = new System.Drawing.Size(69, 14);
            this.lblAddr2.TabIndex = 3;
            this.lblAddr2.Text = "Address 2 :";
            this.lblAddr2.Visible = false;
            // 
            // lblAddressDetails
            // 
            this.lblAddressDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAddressDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAddressDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddressDetails.Location = new System.Drawing.Point(4, 2);
            this.lblAddressDetails.Name = "lblAddressDetails";
            this.lblAddressDetails.Size = new System.Drawing.Size(348, 146);
            this.lblAddressDetails.TabIndex = 0;
            this.lblAddressDetails.Text = " Address Details :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 146);
            this.label2.TabIndex = 108;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Right;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(352, 2);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1, 146);
            this.label16.TabIndex = 109;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(3, 1);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(350, 1);
            this.label24.TabIndex = 111;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(350, 1);
            this.label5.TabIndex = 112;
            // 
            // pnlPadDetails
            // 
            this.pnlPadDetails.Controls.Add(this.btnOrientation);
            this.pnlPadDetails.Controls.Add(this.btn_EthnicityDel);
            this.pnlPadDetails.Controls.Add(this.btn_Ethnicity);
            this.pnlPadDetails.Controls.Add(this.txtPatSuffix);
            this.pnlPadDetails.Controls.Add(this.label62);
            this.pnlPadDetails.Controls.Add(this.dtpBirth);
            this.pnlPadDetails.Controls.Add(this.btn_RaceDel);
            this.pnlPadDetails.Controls.Add(this.btn_Race);
            this.pnlPadDetails.Controls.Add(this.cmbCommPref);
            this.pnlPadDetails.Controls.Add(this.label50);
            this.pnlPadDetails.Controls.Add(this.label60);
            this.pnlPadDetails.Controls.Add(this.lbPatientbirthtime);
            this.pnlPadDetails.Controls.Add(this.txtPACode);
            this.pnlPadDetails.Controls.Add(this.cmbPAEthn);
            this.pnlPadDetails.Controls.Add(this.cmbGender);
            this.pnlPadDetails.Controls.Add(this.cmbPALang);
            this.pnlPadDetails.Controls.Add(this.label57);
            this.pnlPadDetails.Controls.Add(this.label58);
            this.pnlPadDetails.Controls.Add(this.label56);
            this.pnlPadDetails.Controls.Add(this.txtmPASSN);
            this.pnlPadDetails.Controls.Add(this.mtxtPADOB);
            this.pnlPadDetails.Controls.Add(this.label14);
            this.pnlPadDetails.Controls.Add(this.cmbPAHandDom);
            this.pnlPadDetails.Controls.Add(this.cmbPARace);
            this.pnlPadDetails.Controls.Add(this.cmbPAMarital);
            this.pnlPadDetails.Controls.Add(this.txtPAMName);
            this.pnlPadDetails.Controls.Add(this.txtPALName);
            this.pnlPadDetails.Controls.Add(this.txtPAFname);
            this.pnlPadDetails.Controls.Add(this.label1);
            this.pnlPadDetails.Controls.Add(this.lbPatientRace);
            this.pnlPadDetails.Controls.Add(this.lblPatientSSN);
            this.pnlPadDetails.Controls.Add(this.lbPatientMarital);
            this.pnlPadDetails.Controls.Add(this.lbPatientDOB);
            this.pnlPadDetails.Controls.Add(this.lblPatientName);
            this.pnlPadDetails.Controls.Add(this.lblPatientCode);
            this.pnlPadDetails.Controls.Add(this.lblPersonalInfo);
            this.pnlPadDetails.Controls.Add(this.label3);
            this.pnlPadDetails.Controls.Add(this.label9);
            this.pnlPadDetails.Controls.Add(this.label15);
            this.pnlPadDetails.Controls.Add(this.label31);
            this.pnlPadDetails.Controls.Add(this.label40);
            this.pnlPadDetails.Controls.Add(this.lblPALName);
            this.pnlPadDetails.Controls.Add(this.lblPAMName);
            this.pnlPadDetails.Controls.Add(this.lblPAFName);
            this.pnlPadDetails.Controls.Add(this.pnlPAPhoto);
            this.pnlPadDetails.Controls.Add(this.lbPatientFDom);
            this.pnlPadDetails.Controls.Add(this.label59);
            this.pnlPadDetails.Controls.Add(this.label39);
            this.pnlPadDetails.Controls.Add(this.label21);
            this.pnlPadDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPadDetails.Location = new System.Drawing.Point(0, 30);
            this.pnlPadDetails.Name = "pnlPadDetails";
            this.pnlPadDetails.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlPadDetails.Size = new System.Drawing.Size(780, 196);
            this.pnlPadDetails.TabIndex = 0;
            // 
            // btnOrientation
            // 
            this.btnOrientation.AutoEllipsis = true;
            this.btnOrientation.BackColor = System.Drawing.Color.Transparent;
            this.btnOrientation.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btnOrientation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOrientation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrientation.Image = ((System.Drawing.Image)(resources.GetObject("btnOrientation.Image")));
            this.btnOrientation.Location = new System.Drawing.Point(226, 114);
            this.btnOrientation.Name = "btnOrientation";
            this.btnOrientation.Size = new System.Drawing.Size(47, 23);
            this.btnOrientation.TabIndex = 1009;
            this.btnOrientation.UseVisualStyleBackColor = false;
            this.btnOrientation.Click += new System.EventHandler(this.btnOrientation_Click);
            // 
            // btn_EthnicityDel
            // 
            this.btn_EthnicityDel.AutoEllipsis = true;
            this.btn_EthnicityDel.BackColor = System.Drawing.Color.Transparent;
            this.btn_EthnicityDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_EthnicityDel.BackgroundImage")));
            this.btn_EthnicityDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_EthnicityDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_EthnicityDel.Image = ((System.Drawing.Image)(resources.GetObject("btn_EthnicityDel.Image")));
            this.btn_EthnicityDel.Location = new System.Drawing.Point(488, 140);
            this.btn_EthnicityDel.Name = "btn_EthnicityDel";
            this.btn_EthnicityDel.Size = new System.Drawing.Size(22, 22);
            this.btn_EthnicityDel.TabIndex = 16;
            this.btn_EthnicityDel.UseVisualStyleBackColor = false;
            this.btn_EthnicityDel.Visible = false;
            this.btn_EthnicityDel.Click += new System.EventHandler(this.btn_EthnicityDel_Click);
            // 
            // btn_Ethnicity
            // 
            this.btn_Ethnicity.AutoEllipsis = true;
            this.btn_Ethnicity.BackColor = System.Drawing.Color.Transparent;
            this.btn_Ethnicity.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btn_Ethnicity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Ethnicity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Ethnicity.Image = ((System.Drawing.Image)(resources.GetObject("btn_Ethnicity.Image")));
            this.btn_Ethnicity.Location = new System.Drawing.Point(463, 140);
            this.btn_Ethnicity.Name = "btn_Ethnicity";
            this.btn_Ethnicity.Size = new System.Drawing.Size(22, 22);
            this.btn_Ethnicity.TabIndex = 15;
            this.btn_Ethnicity.UseVisualStyleBackColor = false;
            this.btn_Ethnicity.Visible = false;
            this.btn_Ethnicity.Click += new System.EventHandler(this.btn_Ethnicity_Click);
            // 
            // txtPatSuffix
            // 
            this.txtPatSuffix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPatSuffix.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatSuffix.Location = new System.Drawing.Point(460, 45);
            this.txtPatSuffix.MaxLength = 10;
            this.txtPatSuffix.Name = "txtPatSuffix";
            this.txtPatSuffix.Size = new System.Drawing.Size(46, 22);
            this.txtPatSuffix.TabIndex = 6;
            // 
            // label62
            // 
            this.label62.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label62.AutoEllipsis = true;
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label62.Location = new System.Drawing.Point(464, 68);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(38, 12);
            this.label62.TabIndex = 1007;
            this.label62.Text = "(Suffix)";
            // 
            // dtpBirth
            // 
            this.dtpBirth.CustomFormat = "HH:mm:ss";
            this.dtpBirth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBirth.Location = new System.Drawing.Point(339, 85);
            this.dtpBirth.Name = "dtpBirth";
            this.dtpBirth.ShowCheckBox = true;
            this.dtpBirth.ShowUpDown = true;
            this.dtpBirth.Size = new System.Drawing.Size(121, 22);
            this.dtpBirth.TabIndex = 8;
            this.dtpBirth.Validating += new System.ComponentModel.CancelEventHandler(this.dtpBirth_Validating);
            // 
            // btn_RaceDel
            // 
            this.btn_RaceDel.AutoEllipsis = true;
            this.btn_RaceDel.BackColor = System.Drawing.Color.Transparent;
            this.btn_RaceDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_RaceDel.BackgroundImage")));
            this.btn_RaceDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_RaceDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RaceDel.Image = ((System.Drawing.Image)(resources.GetObject("btn_RaceDel.Image")));
            this.btn_RaceDel.Location = new System.Drawing.Point(251, 141);
            this.btn_RaceDel.Name = "btn_RaceDel";
            this.btn_RaceDel.Size = new System.Drawing.Size(22, 22);
            this.btn_RaceDel.TabIndex = 13;
            this.btn_RaceDel.UseVisualStyleBackColor = false;
            this.btn_RaceDel.Visible = false;
            this.btn_RaceDel.Click += new System.EventHandler(this.btn_RaceDel_Click);
            this.btn_RaceDel.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_RaceDel.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_Race
            // 
            this.btn_Race.AutoEllipsis = true;
            this.btn_Race.BackColor = System.Drawing.Color.Transparent;
            this.btn_Race.BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            this.btn_Race.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Race.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Race.Image = ((System.Drawing.Image)(resources.GetObject("btn_Race.Image")));
            this.btn_Race.Location = new System.Drawing.Point(226, 141);
            this.btn_Race.Name = "btn_Race";
            this.btn_Race.Size = new System.Drawing.Size(22, 22);
            this.btn_Race.TabIndex = 12;
            this.btn_Race.UseVisualStyleBackColor = false;
            this.btn_Race.Visible = false;
            this.btn_Race.Click += new System.EventHandler(this.btn_Race_Click);
            this.btn_Race.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_Race.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // cmbCommPref
            // 
            this.cmbCommPref.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbCommPref.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCommPref.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbCommPref.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommPref.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCommPref.FormattingEnabled = true;
            this.cmbCommPref.Location = new System.Drawing.Point(625, 168);
            this.cmbCommPref.Name = "cmbCommPref";
            this.cmbCommPref.Size = new System.Drawing.Size(143, 22);
            this.cmbCommPref.TabIndex = 20;
            this.cmbCommPref.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCommPref_KeyDown);
            this.cmbCommPref.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.cmbCommPref_PreviewKeyDown);
            // 
            // label50
            // 
            this.label50.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label50.AutoEllipsis = true;
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(520, 172);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(103, 14);
            this.label50.TabIndex = 1005;
            this.label50.Text = "Com Preference :";
            this.toolTip1.SetToolTip(this.label50, "Communication Preference ");
            // 
            // label60
            // 
            this.label60.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label60.AutoEllipsis = true;
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(270, 102);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(51, 11);
            this.label60.TabIndex = 1002;
            this.label60.Text = "(hh:mm:ss)";
            // 
            // lbPatientbirthtime
            // 
            this.lbPatientbirthtime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatientbirthtime.AutoEllipsis = true;
            this.lbPatientbirthtime.AutoSize = true;
            this.lbPatientbirthtime.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientbirthtime.Location = new System.Drawing.Point(252, 88);
            this.lbPatientbirthtime.Name = "lbPatientbirthtime";
            this.lbPatientbirthtime.Size = new System.Drawing.Size(86, 14);
            this.lbPatientbirthtime.TabIndex = 1001;
            this.lbPatientbirthtime.Text = "Time of birth :";
            // 
            // txtPACode
            // 
            this.txtPACode.Location = new System.Drawing.Point(65, 19);
            this.txtPACode.Mask = "AAA-AAAAAAAAAA";
            this.txtPACode.Name = "txtPACode";
            this.txtPACode.PromptChar = ' ';
            this.txtPACode.ResetOnSpace = false;
            this.txtPACode.ShortcutsEnabled = false;
            this.txtPACode.Size = new System.Drawing.Size(156, 22);
            this.txtPACode.TabIndex = 1;
            this.txtPACode.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtPACode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtPACode_MouseClick);
            this.txtPACode.TextChanged += new System.EventHandler(this.txtPACode_TextChanged);
            this.txtPACode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPACode_KeyDown);
            this.txtPACode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPACode_KeyPress);
            this.txtPACode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPACode_KeyUp);
            this.txtPACode.Validating += new System.ComponentModel.CancelEventHandler(this.txtPACode_Validating);
            // 
            // cmbPAEthn
            // 
            this.cmbPAEthn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbPAEthn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPAEthn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPAEthn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPAEthn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPAEthn.FormattingEnabled = true;
            this.cmbPAEthn.Location = new System.Drawing.Point(339, 141);
            this.cmbPAEthn.MaxLength = 50;
            this.cmbPAEthn.Name = "cmbPAEthn";
            this.cmbPAEthn.Size = new System.Drawing.Size(121, 22);
            this.cmbPAEthn.TabIndex = 14;
            this.cmbPAEthn.SelectedIndexChanged += new System.EventHandler(this.cmbPAEthn_SelectedIndexChanged);
            this.cmbPAEthn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPAEthn_KeyDown);
            this.cmbPAEthn.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.cmbPAEthn_PreviewKeyDown);
            // 
            // cmbGender
            // 
            this.cmbGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(111, 114);
            this.cmbGender.MaxLength = 50;
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(112, 22);
            this.cmbGender.TabIndex = 9;
            // 
            // cmbPALang
            // 
            this.cmbPALang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbPALang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPALang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPALang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPALang.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPALang.FormattingEnabled = true;
            this.cmbPALang.Location = new System.Drawing.Point(339, 168);
            this.cmbPALang.MaxLength = 50;
            this.cmbPALang.Name = "cmbPALang";
            this.cmbPALang.Size = new System.Drawing.Size(121, 22);
            this.cmbPALang.TabIndex = 18;
            this.cmbPALang.SelectedIndexChanged += new System.EventHandler(this.cmbPALang_SelectedIndexChanged);
            this.cmbPALang.TextChanged += new System.EventHandler(this.cmbPALang_TextChanged);
            this.cmbPALang.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPALang_KeyDown);
            this.cmbPALang.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.cmbPALang_PreviewKeyDown);
            this.cmbPALang.Validating += new System.ComponentModel.CancelEventHandler(this.cmbPALang_Validating);
            // 
            // label57
            // 
            this.label57.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label57.AutoEllipsis = true;
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(270, 173);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(68, 14);
            this.label57.TabIndex = 35;
            this.label57.Text = "Language :";
            // 
            // label58
            // 
            this.label58.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label58.AutoEllipsis = true;
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(55, 119);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(55, 14);
            this.label58.TabIndex = 32;
            this.label58.Text = "Gender :";
            // 
            // label56
            // 
            this.label56.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label56.AutoEllipsis = true;
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(276, 145);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(62, 14);
            this.label56.TabIndex = 32;
            this.label56.Text = "Ethnicity :";
            // 
            // txtmPASSN
            // 
            this.txtmPASSN.AllowValidate = true;
            this.txtmPASSN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmPASSN.IncludeLiteralsAndPrompts = false;
            this.txtmPASSN.Location = new System.Drawing.Point(303, 19);
            this.txtmPASSN.MaskType = gloMaskControl.gloMaskType.SSN;
            this.txtmPASSN.Name = "txtmPASSN";
            this.txtmPASSN.ReadOnly = false;
            this.txtmPASSN.Size = new System.Drawing.Size(80, 22);
            this.txtmPASSN.TabIndex = 2;
            // 
            // mtxtPADOB
            // 
            this.mtxtPADOB.Location = new System.Drawing.Point(111, 85);
            this.mtxtPADOB.Mask = "00/00/0000";
            this.mtxtPADOB.Name = "mtxtPADOB";
            this.mtxtPADOB.Size = new System.Drawing.Size(112, 22);
            this.mtxtPADOB.TabIndex = 7;
            this.mtxtPADOB.ValidatingType = typeof(System.DateTime);
            this.mtxtPADOB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskTextBox_MouseClick);
            this.mtxtPADOB.TextChanged += new System.EventHandler(this.mtxtPADOB_TextChanged);
            this.mtxtPADOB.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtPADOB_Validating);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 192);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(772, 1);
            this.label14.TabIndex = 28;
            // 
            // cmbPAHandDom
            // 
            this.cmbPAHandDom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPAHandDom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPAHandDom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPAHandDom.FormattingEnabled = true;
            this.cmbPAHandDom.Location = new System.Drawing.Point(111, 168);
            this.cmbPAHandDom.Name = "cmbPAHandDom";
            this.cmbPAHandDom.Size = new System.Drawing.Size(112, 22);
            this.cmbPAHandDom.TabIndex = 17;
            this.cmbPAHandDom.SelectedIndexChanged += new System.EventHandler(this.cmbPAHandDom_SelectedIndexChanged);
            // 
            // cmbPARace
            // 
            this.cmbPARace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbPARace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPARace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPARace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPARace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPARace.FormattingEnabled = true;
            this.cmbPARace.Location = new System.Drawing.Point(111, 141);
            this.cmbPARace.Name = "cmbPARace";
            this.cmbPARace.Size = new System.Drawing.Size(112, 22);
            this.cmbPARace.TabIndex = 11;
            this.cmbPARace.SelectedIndexChanged += new System.EventHandler(this.cmbPARace_SelectedIndexChanged);
            this.cmbPARace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPARace_KeyDown);
            this.cmbPARace.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.cmbPARace_PreviewKeyDown);
            // 
            // cmbPAMarital
            // 
            this.cmbPAMarital.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.cmbPAMarital.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPAMarital.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPAMarital.FormattingEnabled = true;
            this.cmbPAMarital.Location = new System.Drawing.Point(339, 114);
            this.cmbPAMarital.Name = "cmbPAMarital";
            this.cmbPAMarital.Size = new System.Drawing.Size(121, 22);
            this.cmbPAMarital.TabIndex = 10;
            // 
            // txtPAMName
            // 
            this.txtPAMName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAMName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAMName.Location = new System.Drawing.Point(223, 45);
            this.txtPAMName.MaxLength = 35;
            this.txtPAMName.Name = "txtPAMName";
            this.txtPAMName.Size = new System.Drawing.Size(77, 22);
            this.txtPAMName.TabIndex = 4;
            this.txtPAMName.MouseHover += new System.EventHandler(this.txtPAMName_MouseHover);
            // 
            // txtPALName
            // 
            this.txtPALName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPALName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPALName.Location = new System.Drawing.Point(302, 45);
            this.txtPALName.MaxLength = 50;
            this.txtPALName.Name = "txtPALName";
            this.txtPALName.Size = new System.Drawing.Size(156, 22);
            this.txtPALName.TabIndex = 5;
            this.txtPALName.TextChanged += new System.EventHandler(this.txtPALName_TextChanged);
            this.txtPALName.Leave += new System.EventHandler(this.txtPALName_Leave);
            // 
            // txtPAFname
            // 
            this.txtPAFname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.txtPAFname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAFname.Location = new System.Drawing.Point(65, 45);
            this.txtPAFname.MaxLength = 50;
            this.txtPAFname.Name = "txtPAFname";
            this.txtPAFname.Size = new System.Drawing.Size(156, 22);
            this.txtPAFname.TabIndex = 3;
            this.txtPAFname.TextChanged += new System.EventHandler(this.txtPAFname_TextChanged);
            this.txtPAFname.Leave += new System.EventHandler(this.txtPAFname_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 11);
            this.label1.TabIndex = 13;
            this.label1.Text = "(mm/dd/yyyy)";
            // 
            // lbPatientRace
            // 
            this.lbPatientRace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatientRace.AutoEllipsis = true;
            this.lbPatientRace.AutoSize = true;
            this.lbPatientRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientRace.Location = new System.Drawing.Point(69, 146);
            this.lbPatientRace.Name = "lbPatientRace";
            this.lbPatientRace.Size = new System.Drawing.Size(41, 14);
            this.lbPatientRace.TabIndex = 17;
            this.lbPatientRace.Text = "Race :";
            // 
            // lblPatientSSN
            // 
            this.lblPatientSSN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientSSN.AutoEllipsis = true;
            this.lblPatientSSN.AutoSize = true;
            this.lblPatientSSN.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSSN.Location = new System.Drawing.Point(264, 23);
            this.lblPatientSSN.Name = "lblPatientSSN";
            this.lblPatientSSN.Size = new System.Drawing.Size(37, 14);
            this.lblPatientSSN.TabIndex = 3;
            this.lblPatientSSN.Text = "SSN :";
            // 
            // lbPatientMarital
            // 
            this.lbPatientMarital.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatientMarital.AutoEllipsis = true;
            this.lbPatientMarital.AutoSize = true;
            this.lbPatientMarital.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientMarital.Location = new System.Drawing.Point(289, 118);
            this.lbPatientMarital.Name = "lbPatientMarital";
            this.lbPatientMarital.Size = new System.Drawing.Size(49, 14);
            this.lbPatientMarital.TabIndex = 15;
            this.lbPatientMarital.Text = "Marital :";
            // 
            // lbPatientDOB
            // 
            this.lbPatientDOB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatientDOB.AutoEllipsis = true;
            this.lbPatientDOB.AutoSize = true;
            this.lbPatientDOB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientDOB.Location = new System.Drawing.Point(26, 88);
            this.lbPatientDOB.Name = "lbPatientDOB";
            this.lbPatientDOB.Size = new System.Drawing.Size(85, 14);
            this.lbPatientDOB.TabIndex = 12;
            this.lbPatientDOB.Text = "Date of birth :";
            // 
            // lblPatientName
            // 
            this.lblPatientName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientName.AutoEllipsis = true;
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.Location = new System.Drawing.Point(18, 49);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(46, 14);
            this.lblPatientName.TabIndex = 5;
            this.lblPatientName.Text = "Name :";
            // 
            // lblPatientCode
            // 
            this.lblPatientCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPatientCode.AutoEllipsis = true;
            this.lblPatientCode.AutoSize = true;
            this.lblPatientCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientCode.Location = new System.Drawing.Point(21, 23);
            this.lblPatientCode.Name = "lblPatientCode";
            this.lblPatientCode.Size = new System.Drawing.Size(43, 14);
            this.lblPatientCode.TabIndex = 1;
            this.lblPatientCode.Text = "Code :";
            // 
            // lblPersonalInfo
            // 
            this.lblPersonalInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPersonalInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonalInfo.Location = new System.Drawing.Point(4, 1);
            this.lblPersonalInfo.Name = "lblPersonalInfo";
            this.lblPersonalInfo.Size = new System.Drawing.Size(153, 13);
            this.lblPersonalInfo.TabIndex = 0;
            this.lblPersonalInfo.Text = " Personal Information :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 192);
            this.label3.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Right;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(776, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(1, 192);
            this.label9.TabIndex = 27;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(774, 1);
            this.label15.TabIndex = 29;
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(11, 23);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(14, 14);
            this.label31.TabIndex = 30;
            this.label31.Text = "*";
            // 
            // label40
            // 
            this.label40.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label40.AutoEllipsis = true;
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.Red;
            this.label40.Location = new System.Drawing.Point(16, 88);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(14, 14);
            this.label40.TabIndex = 30;
            this.label40.Text = "*";
            // 
            // lblPALName
            // 
            this.lblPALName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPALName.AutoEllipsis = true;
            this.lblPALName.AutoSize = true;
            this.lblPALName.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.lblPALName.Location = new System.Drawing.Point(351, 68);
            this.lblPALName.Name = "lblPALName";
            this.lblPALName.Size = new System.Drawing.Size(58, 12);
            this.lblPALName.TabIndex = 11;
            this.lblPALName.Text = "(Last Name)";
            // 
            // lblPAMName
            // 
            this.lblPAMName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPAMName.AutoEllipsis = true;
            this.lblPAMName.AutoSize = true;
            this.lblPAMName.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.lblPAMName.Location = new System.Drawing.Point(249, 68);
            this.lblPAMName.Name = "lblPAMName";
            this.lblPAMName.Size = new System.Drawing.Size(25, 12);
            this.lblPAMName.TabIndex = 10;
            this.lblPAMName.Text = "(MI)";
            // 
            // lblPAFName
            // 
            this.lblPAFName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPAFName.AutoEllipsis = true;
            this.lblPAFName.AutoSize = true;
            this.lblPAFName.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.lblPAFName.Location = new System.Drawing.Point(113, 68);
            this.lblPAFName.Name = "lblPAFName";
            this.lblPAFName.Size = new System.Drawing.Size(60, 12);
            this.lblPAFName.TabIndex = 9;
            this.lblPAFName.Text = "(First Name)";
            // 
            // pnlPAPhoto
            // 
            this.pnlPAPhoto.Controls.Add(this.TrackbarPlus);
            this.pnlPAPhoto.Controls.Add(this.TrackbarMinus);
            this.pnlPAPhoto.Controls.Add(this.label29);
            this.pnlPAPhoto.Controls.Add(this.label28);
            this.pnlPAPhoto.Controls.Add(this.label27);
            this.pnlPAPhoto.Controls.Add(this.label26);
            this.pnlPAPhoto.Controls.Add(this.rbWebCam);
            this.pnlPAPhoto.Controls.Add(this.picPAPhoto);
            this.pnlPAPhoto.Controls.Add(this.myPictureBox);
            this.pnlPAPhoto.Controls.Add(this.rbBrowsePhoto);
            this.pnlPAPhoto.Controls.Add(this.btn_PAClearPhoto);
            this.pnlPAPhoto.Controls.Add(this.btn_PAPhotoBrowse);
            this.pnlPAPhoto.Controls.Add(this.btn_PACapturePhoto);
            this.pnlPAPhoto.Controls.Add(this.myNewTrackBar);
            this.pnlPAPhoto.Location = new System.Drawing.Point(513, 6);
            this.pnlPAPhoto.Name = "pnlPAPhoto";
            this.pnlPAPhoto.Size = new System.Drawing.Size(258, 152);
            this.pnlPAPhoto.TabIndex = 19;
            // 
            // TrackbarPlus
            // 
            this.TrackbarPlus.FlatAppearance.BorderSize = 0;
            this.TrackbarPlus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TrackbarPlus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TrackbarPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TrackbarPlus.Image = ((System.Drawing.Image)(resources.GetObject("TrackbarPlus.Image")));
            this.TrackbarPlus.Location = new System.Drawing.Point(103, 8);
            this.TrackbarPlus.Name = "TrackbarPlus";
            this.TrackbarPlus.Size = new System.Drawing.Size(18, 16);
            this.TrackbarPlus.TabIndex = 44;
            this.toolTip1.SetToolTip(this.TrackbarPlus, "Zoom In");
            this.TrackbarPlus.UseVisualStyleBackColor = true;
            this.TrackbarPlus.Visible = false;
            this.TrackbarPlus.Click += new System.EventHandler(this.TrackbarPlus_Click);
            // 
            // TrackbarMinus
            // 
            this.TrackbarMinus.FlatAppearance.BorderSize = 0;
            this.TrackbarMinus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TrackbarMinus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TrackbarMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TrackbarMinus.Image = ((System.Drawing.Image)(resources.GetObject("TrackbarMinus.Image")));
            this.TrackbarMinus.Location = new System.Drawing.Point(103, 129);
            this.TrackbarMinus.Name = "TrackbarMinus";
            this.TrackbarMinus.Size = new System.Drawing.Size(18, 15);
            this.TrackbarMinus.TabIndex = 38;
            this.TrackbarMinus.TabStop = false;
            this.toolTip1.SetToolTip(this.TrackbarMinus, "Zoom Out");
            this.TrackbarMinus.UseVisualStyleBackColor = true;
            this.TrackbarMinus.Visible = false;
            this.TrackbarMinus.Click += new System.EventHandler(this.TrackbarMinus_Click);
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label29.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label29.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(1, 151);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(256, 1);
            this.label29.TabIndex = 41;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Top;
            this.label28.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(1, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(256, 1);
            this.label28.TabIndex = 40;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(257, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1, 152);
            this.label27.TabIndex = 39;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Left;
            this.label26.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 152);
            this.label26.TabIndex = 38;
            // 
            // rbWebCam
            // 
            this.rbWebCam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbWebCam.AutoSize = true;
            this.rbWebCam.Location = new System.Drawing.Point(16, 38);
            this.rbWebCam.Name = "rbWebCam";
            this.rbWebCam.Size = new System.Drawing.Size(73, 18);
            this.rbWebCam.TabIndex = 2;
            this.rbWebCam.TabStop = true;
            this.rbWebCam.Text = "Webcam";
            this.rbWebCam.UseVisualStyleBackColor = true;
            this.rbWebCam.CheckedChanged += new System.EventHandler(this.rbWebCam_CheckedChanged);
            // 
            // rbBrowsePhoto
            // 
            this.rbBrowsePhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbBrowsePhoto.AutoSize = true;
            this.rbBrowsePhoto.Checked = true;
            this.rbBrowsePhoto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBrowsePhoto.Location = new System.Drawing.Point(16, 15);
            this.rbBrowsePhoto.Name = "rbBrowsePhoto";
            this.rbBrowsePhoto.Size = new System.Drawing.Size(70, 18);
            this.rbBrowsePhoto.TabIndex = 1;
            this.rbBrowsePhoto.TabStop = true;
            this.rbBrowsePhoto.Text = "Browse";
            this.rbBrowsePhoto.UseVisualStyleBackColor = true;
            this.rbBrowsePhoto.CheckedChanged += new System.EventHandler(this.rbBrowsePhoto_CheckedChanged);
            // 
            // btn_PAClearPhoto
            // 
            this.btn_PAClearPhoto.AutoEllipsis = true;
            this.btn_PAClearPhoto.BackColor = System.Drawing.Color.Transparent;
            this.btn_PAClearPhoto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PAClearPhoto.BackgroundImage")));
            this.btn_PAClearPhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PAClearPhoto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_PAClearPhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PAClearPhoto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PAClearPhoto.Location = new System.Drawing.Point(5, 94);
            this.btn_PAClearPhoto.Margin = new System.Windows.Forms.Padding(0);
            this.btn_PAClearPhoto.Name = "btn_PAClearPhoto";
            this.btn_PAClearPhoto.Size = new System.Drawing.Size(98, 23);
            this.btn_PAClearPhoto.TabIndex = 4;
            this.btn_PAClearPhoto.Text = "Clear Photo";
            this.btn_PAClearPhoto.UseVisualStyleBackColor = false;
            this.btn_PAClearPhoto.Click += new System.EventHandler(this.btn_PAClearPhoto_Click);
            this.btn_PAClearPhoto.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_PAClearPhoto.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_PAPhotoBrowse
            // 
            this.btn_PAPhotoBrowse.AutoEllipsis = true;
            this.btn_PAPhotoBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btn_PAPhotoBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PAPhotoBrowse.BackgroundImage")));
            this.btn_PAPhotoBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PAPhotoBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_PAPhotoBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PAPhotoBrowse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PAPhotoBrowse.Location = new System.Drawing.Point(5, 67);
            this.btn_PAPhotoBrowse.Margin = new System.Windows.Forms.Padding(0);
            this.btn_PAPhotoBrowse.Name = "btn_PAPhotoBrowse";
            this.btn_PAPhotoBrowse.Size = new System.Drawing.Size(98, 23);
            this.btn_PAPhotoBrowse.TabIndex = 3;
            this.btn_PAPhotoBrowse.Text = "Browse Photo";
            this.btn_PAPhotoBrowse.UseVisualStyleBackColor = false;
            this.btn_PAPhotoBrowse.Click += new System.EventHandler(this.btn_PAPhotoBrowse_Click);
            this.btn_PAPhotoBrowse.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_PAPhotoBrowse.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_PACapturePhoto
            // 
            this.btn_PACapturePhoto.BackColor = System.Drawing.Color.Transparent;
            this.btn_PACapturePhoto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_PACapturePhoto.BackgroundImage")));
            this.btn_PACapturePhoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_PACapturePhoto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btn_PACapturePhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PACapturePhoto.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PACapturePhoto.Location = new System.Drawing.Point(5, 121);
            this.btn_PACapturePhoto.Margin = new System.Windows.Forms.Padding(0);
            this.btn_PACapturePhoto.Name = "btn_PACapturePhoto";
            this.btn_PACapturePhoto.Size = new System.Drawing.Size(98, 23);
            this.btn_PACapturePhoto.TabIndex = 5;
            this.btn_PACapturePhoto.Text = "From Clipboard";
            this.toolTip1.SetToolTip(this.btn_PACapturePhoto, "Capture from Clipboard");
            this.btn_PACapturePhoto.UseVisualStyleBackColor = false;
            this.btn_PACapturePhoto.Click += new System.EventHandler(this.btn_PACapturePhoto_Click);
            this.btn_PACapturePhoto.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_PACapturePhoto.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // myNewTrackBar
            // 
            this.myNewTrackBar.Location = new System.Drawing.Point(103, 8);
            this.myNewTrackBar.Maximum = 44;
            this.myNewTrackBar.Minimum = -44;
            this.myNewTrackBar.Name = "myNewTrackBar";
            this.myNewTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.myNewTrackBar.Size = new System.Drawing.Size(45, 137);
            this.myNewTrackBar.TabIndex = 43;
            this.myNewTrackBar.Visible = false;
            this.myNewTrackBar.ValueChanged += new System.EventHandler(this.myNewTrackBar_ValueChanged);
            // 
            // lbPatientFDom
            // 
            this.lbPatientFDom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPatientFDom.AutoEllipsis = true;
            this.lbPatientFDom.AutoSize = true;
            this.lbPatientFDom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPatientFDom.Location = new System.Drawing.Point(4, 172);
            this.lbPatientFDom.Name = "lbPatientFDom";
            this.lbPatientFDom.Size = new System.Drawing.Size(106, 14);
            this.lbPatientFDom.TabIndex = 19;
            this.lbPatientFDom.Text = "Hand dominance :";
            // 
            // label59
            // 
            this.label59.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.Red;
            this.label59.Location = new System.Drawing.Point(45, 119);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(14, 14);
            this.label59.TabIndex = 36;
            this.label59.Text = "*";
            // 
            // label39
            // 
            this.label39.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label39.AutoEllipsis = true;
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(102, 68);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(11, 12);
            this.label39.TabIndex = 30;
            this.label39.Text = "*";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoEllipsis = true;
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 7.25F);
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(340, 68);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(11, 12);
            this.label21.TabIndex = 1008;
            this.label21.Text = "*";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlGeneralInfoHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.panel1.Size = new System.Drawing.Size(780, 30);
            this.panel1.TabIndex = 67;
            // 
            // pnlGeneralInfoHeader
            // 
            this.pnlGeneralInfoHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlGeneralInfoHeader.BackgroundImage = global::gloPatient.Properties.Resources.Img_Blue2007;
            this.pnlGeneralInfoHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGeneralInfoHeader.Controls.Add(this.txtPatientPrefix);
            this.pnlGeneralInfoHeader.Controls.Add(this.label13);
            this.pnlGeneralInfoHeader.Controls.Add(this.label12);
            this.pnlGeneralInfoHeader.Controls.Add(this.label11);
            this.pnlGeneralInfoHeader.Controls.Add(this.label10);
            this.pnlGeneralInfoHeader.Controls.Add(this.lblGeneralInfo);
            this.pnlGeneralInfoHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGeneralInfoHeader.Location = new System.Drawing.Point(3, 2);
            this.pnlGeneralInfoHeader.Name = "pnlGeneralInfoHeader";
            this.pnlGeneralInfoHeader.Size = new System.Drawing.Size(774, 25);
            this.pnlGeneralInfoHeader.TabIndex = 4;
            // 
            // txtPatientPrefix
            // 
            this.txtPatientPrefix.Location = new System.Drawing.Point(200, 3);
            this.txtPatientPrefix.Name = "txtPatientPrefix";
            this.txtPatientPrefix.Size = new System.Drawing.Size(64, 22);
            this.txtPatientPrefix.TabIndex = 37;
            this.txtPatientPrefix.Visible = false;
            this.txtPatientPrefix.TextChanged += new System.EventHandler(this.txtPatientPrefix_TextChanged);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(772, 1);
            this.label13.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(772, 1);
            this.label12.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1, 25);
            this.label11.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label10.Dock = System.Windows.Forms.DockStyle.Right;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(773, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1, 25);
            this.label10.TabIndex = 27;
            // 
            // lblGeneralInfo
            // 
            this.lblGeneralInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblGeneralInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGeneralInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGeneralInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneralInfo.ForeColor = System.Drawing.Color.White;
            this.lblGeneralInfo.Location = new System.Drawing.Point(0, 0);
            this.lblGeneralInfo.Name = "lblGeneralInfo";
            this.lblGeneralInfo.Size = new System.Drawing.Size(774, 25);
            this.lblGeneralInfo.TabIndex = 0;
            this.lblGeneralInfo.Text = " General Information";
            this.lblGeneralInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dlgPhotoBrowser
            // 
            this.dlgPhotoBrowser.FileName = "openFileDialog1";
            // 
            // pnlInternalControl
            // 
            this.pnlInternalControl.Location = new System.Drawing.Point(188, 291);
            this.pnlInternalControl.Name = "pnlInternalControl";
            this.pnlInternalControl.Size = new System.Drawing.Size(15, 10);
            this.pnlInternalControl.TabIndex = 103;
            this.pnlInternalControl.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // picPAPhoto
            // 
            this.picPAPhoto.AutoScroll = true;
            this.picPAPhoto.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.picPAPhoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.picPAPhoto.byteImage = null;
            this.picPAPhoto.Image = null;
            this.picPAPhoto.IsMovable = true;
            this.picPAPhoto.IsPAPhotomodified = false;
            this.picPAPhoto.Location = new System.Drawing.Point(126, 7);
            this.picPAPhoto.Name = "picPAPhoto";
            this.picPAPhoto.PictBoxHeight = ((short)(137));
            this.picPAPhoto.PictBoxWidth = ((short)(123));
            this.picPAPhoto.PLocation = new System.Drawing.Point(0, 0);
            this.picPAPhoto.Rotation = 0;
            this.picPAPhoto.Size = new System.Drawing.Size(123, 137);
            this.picPAPhoto.sZoomVersion = "5X";
            this.picPAPhoto.TabIndex = 0;
            this.picPAPhoto.TabStop = false;
            this.picPAPhoto.Zoom = 10;
            this.picPAPhoto.ZoomValueForTrackBar = 0;
            // 
            // myPictureBox
            // 
            this.myPictureBox.AutoScroll = true;
            this.myPictureBox.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.myPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.myPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myPictureBox.byteImage = null;
            this.myPictureBox.ELocation = new System.Drawing.Point(0, 0);
            this.myPictureBox.Image = null;
            this.myPictureBox.IsMovable = true;
            this.myPictureBox.IsPAPhotomodified = false;
            this.myPictureBox.Location = new System.Drawing.Point(126, 7);
            this.myPictureBox.Name = "myPictureBox";
            this.myPictureBox.OtherOperations = false;
            this.myPictureBox.PictBoxHeight = ((short)(137));
            this.myPictureBox.PictBoxWidth = ((short)(123));
            this.myPictureBox.PLocation = new System.Drawing.Point(0, 0);
            this.myPictureBox.Rotation = 0;
            this.myPictureBox.Size = new System.Drawing.Size(123, 137);
            this.myPictureBox.sZoomVersion = "5X";
            this.myPictureBox.TabIndex = 0;
            this.myPictureBox.TabStop = false;
            this.myPictureBox.Zoom = 10;
            this.myPictureBox.ZoomValueForTrackBar = 0;
            // 
            // gloPatientDemographicsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnlDemographicInfo);
            this.Controls.Add(this.pnlInternalControl);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Name = "gloPatientDemographicsControl";
            this.Size = new System.Drawing.Size(780, 735);
            this.Load += new System.EventHandler(this.gloPatientDemographicsControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.gloPatientDemographicsControl_Paint);
            this.pnlDemographicInfo.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlPatientOtherGuarantorInfo.ResumeLayout(false);
            this.pnlPatientOtherGuarantorInfo.PerformLayout();
            this.pnlBusinessCenter.ResumeLayout(false);
            this.pnlBusinessCenter.PerformLayout();
            this.pnlGuarantorDetails.ResumeLayout(false);
            this.pnlGuarantorDetails.PerformLayout();
            this.pnlOtherDetail.ResumeLayout(false);
            this.pnlOtherDetail.PerformLayout();
            this.pnl_Bottom.ResumeLayout(false);
            this.pnl_Bottom.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnl_AddressDetails.ResumeLayout(false);
            this.pnlEmergencyConDetails.ResumeLayout(false);
            this.pnlEmergencyConDetails.PerformLayout();
            this.pnlConDetails.ResumeLayout(false);
            this.pnlConDetails.PerformLayout();
            this.pnlAPIActivationEmail.ResumeLayout(false);
            this.pnlAPIActivationEmail.PerformLayout();
            this.pnlPortalInvitaitonEmail.ResumeLayout(false);
            this.pnlPortalInvitaitonEmail.PerformLayout();
            this.pnlPortalAccount.ResumeLayout(false);
            this.pnlAPIAccount.ResumeLayout(false);
            this.pnlAddDetails.ResumeLayout(false);
            this.pnlAddDetails.PerformLayout();
            this.pnlPadDetails.ResumeLayout(false);
            this.pnlPadDetails.PerformLayout();
            this.pnlPAPhoto.ResumeLayout(false);
            this.pnlPAPhoto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myNewTrackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlGeneralInfoHeader.ResumeLayout(false);
            this.pnlGeneralInfoHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDemographicInfo;
        private System.Windows.Forms.OpenFileDialog dlgPhotoBrowser;
        private System.Windows.Forms.Panel pnlPadDetails;
        private System.Windows.Forms.Panel pnlPAPhoto;
        private System.Windows.Forms.RadioButton rbWebCam;
        private System.Windows.Forms.RadioButton rbBrowsePhoto;
        private System.Windows.Forms.Button btn_PAClearPhoto;
        private System.Windows.Forms.Button btn_PAPhotoBrowse;
        private System.Windows.Forms.Button btn_PACapturePhoto;
        private System.Windows.Forms.ComboBox cmbPAHandDom;
        private System.Windows.Forms.ComboBox cmbPARace;
        private System.Windows.Forms.Label lblPALName;
        private System.Windows.Forms.Label lblPAMName;
        private System.Windows.Forms.Label lblPAFName;
        private System.Windows.Forms.TextBox txtPAMName;
        private System.Windows.Forms.TextBox txtPALName;
        private System.Windows.Forms.TextBox txtPAFname;
        private System.Windows.Forms.Label lbPatientFDom;
        private System.Windows.Forms.Label lbPatientRace;
        private System.Windows.Forms.Label lblPatientSSN;
        private System.Windows.Forms.Label lbPatientDOB;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatientCode;
        private System.Windows.Forms.Label lblPersonalInfo;
        private System.Windows.Forms.Panel pnlAddDetails;
        private System.Windows.Forms.TextBox txtPAZip;
        private System.Windows.Forms.TextBox txtPACounty;
        private System.Windows.Forms.TextBox txtPACity;
        private System.Windows.Forms.TextBox txtPAAddress2;
        private System.Windows.Forms.TextBox txtPAAddress1;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblZip;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblAddr2;
        private System.Windows.Forms.Label lblAddr1;
        private System.Windows.Forms.Label lblAddressDetails;
        private System.Windows.Forms.Label lblGeneralInfo;
        private System.Windows.Forms.Panel pnlGeneralInfoHeader;
        private System.Windows.Forms.Panel pnl_AddressDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
      //  internal System.Windows.Forms.PictureBox picWebCamPatient;
        private System.Windows.Forms.TrackBar myNewTrackBar;
        private System.Windows.Forms.Panel pnlEmergencyConDetails;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtEmergencyContact;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MaskedTextBox mtxtPADOB;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private gloMaskControl.gloMaskBox mtxtEmergencyMobile;
        private gloMaskControl.gloMaskBox mtxtEmergencyPhone;
        private gloMaskControl.gloMaskBox txtmPASSN;
        private System.Windows.Forms.Panel pnlConDetails;
        private gloMaskControl.gloMaskBox mtxtPAPhone;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtPAEmail;
        private gloMaskControl.gloMaskBox txtPAFax;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblFax;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblContactDetails;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private gloMaskControl.gloMaskBox mtxtPAMobile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cmbPAState;
        private System.Windows.Forms.ComboBox cmbRelationship;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.ComboBox cmbPACountry;
        private System.Windows.Forms.Label label49;
        internal System.Windows.Forms.Panel pnlInternalControl;
        internal System.Windows.Forms.Panel pnlAddresssControl;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.ComboBox cmbPALang;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.ComboBox cmbPAEthn;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.ComboBox cmbPAMarital;
        private System.Windows.Forms.Label lbPatientMarital;
        internal System.Windows.Forms.MaskedTextBox txtPACode;
        internal System.Windows.Forms.TextBox txtPatientPrefix;
        //internal System.Windows.Forms.PictureBox picPAPhoto;
        private gloPictureBox.gloPictureBox picPAPhoto;
       // private gloWebcam.gloWebcam myPictureBox;
        private gloPictureBox.gloCameraBox myPictureBox;
        private System.Windows.Forms.DateTimePicker dtpBirth;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label lbPatientbirthtime;
        private System.Windows.Forms.ComboBox cmbCommPref;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Button TrackbarMinus;
        private System.Windows.Forms.Button TrackbarPlus;
        //private System.Windows.Forms.Label label51;
        //private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblAccountDescription;
        private System.Windows.Forms.Panel pnlPatientOtherGuarantorInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbOtherGuarantor;
        private System.Windows.Forms.Button btnotherNewGuarantor;
        private System.Windows.Forms.Button btnOtherGuarantorExistingPatientBrowse;
        private System.Windows.Forms.Button btnEditAccount;
        private System.Windows.Forms.Label lblAccMandatory;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Panel pnlGuarantorDetails;
        private System.Windows.Forms.Button btnGuarantorClear;
        private System.Windows.Forms.Label lblGuarantorDetails;
        private System.Windows.Forms.TextBox txtAccGuarantor;
        private System.Windows.Forms.ComboBox cmbSameAsGuardian;
        private System.Windows.Forms.Label lblSameAsGuardian;
        private System.Windows.Forms.Button btnNewGuarantor;
        private System.Windows.Forms.Button btnGuarantorExistingPatientBrowse;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox chkExcludefromStatement;
        private System.Windows.Forms.CheckBox chkSetToCollection;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label lblGuarMandatory;
        private System.Windows.Forms.Label lblGuarantor;
        private System.Windows.Forms.TextBox txtAccountDescription;
        private System.Windows.Forms.Label lblAccountNo;
        private System.Windows.Forms.RadioButton rbAccountExisting;
        private System.Windows.Forms.RadioButton rbAccountNew;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox txtPAInsuranceNotes;
        private System.Windows.Forms.Button btnCopyAccount;
        private System.Windows.Forms.Button btnExistingAccountDelete;
        private System.Windows.Forms.Button btnExistingAccountSelect;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.ComboBox cmbAccounts;
        private System.Windows.Forms.Panel pnlOtherDetail;
        private System.Windows.Forms.Panel pnl_Bottom;
        private System.Windows.Forms.Button btnOtherDetails;
        private System.Windows.Forms.CheckBox chkYesNoLab;
        private System.Windows.Forms.CheckBox chkSignatureOnFile;
        private System.Windows.Forms.CheckBox chkExempt;
        private System.Windows.Forms.CheckBox chkdirective;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtPAProvider;
        private System.Windows.Forms.ComboBox cmbPAReferrals;
        private System.Windows.Forms.ComboBox cmbPAOccupation;
        private System.Windows.Forms.ComboBox cmbPALocation;
        private System.Windows.Forms.ComboBox cmbGaurdian;
        private System.Windows.Forms.ComboBox cmbGenInfoInsurance;
        private System.Windows.Forms.TextBox txtPAPrimaryCarePhy;
        private System.Windows.Forms.TextBox txtPAPharma;
        private System.Windows.Forms.Label lblGeneralInfoOtherDetails;
        private System.Windows.Forms.Button btnOccupationDelete;
        private System.Windows.Forms.Button btnGuardianSelect;
        private System.Windows.Forms.Button btnGuardianDelete;
        private System.Windows.Forms.Button btnClrInsurance;
        private System.Windows.Forms.Button btnOccupationSelect;
        private System.Windows.Forms.Button btnInsurInfo;
        private System.Windows.Forms.Button btnProviderDelete;
        private System.Windows.Forms.Button btn_PAPharmaDel;
        private System.Windows.Forms.Button btnProviderSelect;
        private System.Windows.Forms.Button btn_PrimaryCareDel;
        private System.Windows.Forms.Button btn_PAReferralsDel;
        private System.Windows.Forms.Button btn_PrimaryCareBr;
        private System.Windows.Forms.Button btn_PAPharmaBr;
        private System.Windows.Forms.Button btn_PAReferralsBr;
        private System.Windows.Forms.Label lblOccupation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblGaurdian;
        private System.Windows.Forms.Label lblInsurance;
        private System.Windows.Forms.Label lblPatientCare;
        private System.Windows.Forms.Label lbPatientReferrals;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.Label lbPatientPharma;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.ComboBox cmbPAPharma;
        private System.Windows.Forms.Panel pnlBusinessCenter;
        private System.Windows.Forms.Label lblBusinessCenter;
        private System.Windows.Forms.ComboBox cmbBusinessCenter;
        private System.Windows.Forms.Label lblBusinessCenterCpt;
        private System.Windows.Forms.Button btn_RaceDel;
        private System.Windows.Forms.Button btn_Race;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPACareTeam;
        private System.Windows.Forms.Button btn_PACareTeamDel;
        private System.Windows.Forms.Button btn_PACareTeamBr;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnMUTransaction;
        private System.Windows.Forms.TextBox txtPatSuffix;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label lblPatientPortalAccountStatus;
        private System.Windows.Forms.Button btnPatientPortalAccountStatus;
        private System.Windows.Forms.Label lblPortalAccountStatus;
        private System.Windows.Forms.Panel pnlPortalInvitaitonEmail;
        private System.Windows.Forms.CheckBox cbSendPatientPortalActivationEmail;
        private System.Windows.Forms.Panel pnlPortalAccount;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btn_EthnicityDel;
        private System.Windows.Forms.Button btn_Ethnicity;
        private System.Windows.Forms.Button btnOrientation;
        private System.Windows.Forms.Button btnbrhosp;
        private System.Windows.Forms.Panel pnlAPIActivationEmail;
        private System.Windows.Forms.CheckBox cbSendAPIInvitation;
        private System.Windows.Forms.Panel pnlAPIAccount;
        private System.Windows.Forms.Label lblAPIActivationStatus;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Button btnAPIActivation;
        //private System.Windows.Forms.Label label5;
        //private System.Windows.Forms.Label label6;
    }
}
