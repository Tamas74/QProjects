using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCCDLibrary;
using System.Data.SqlClient;
using System.IO;

namespace gloQRDAImport
{
    public partial class CompareQRDA : Form
    {
        string[] files;
        string[] filesdest;
        private static int CMSNo = 0;
        public CompareQRDA()
        {
            InitializeComponent();
        }
        private void btnCompare_Click(object sender, EventArgs e)
        {
            gloCCDLibrary.gloQRDAReader qrdareader = new gloQRDAReader();
            ReconcileList oreconsilelist = null;
            ReconcileList oreconsilelist1 = null;
            DataTable dtMeasureCodes = null;
            StringBuilder str = new StringBuilder();
            StringBuilder strMisMatch = new StringBuilder();
            StringBuilder strMulitpleMatch = new StringBuilder();
            DataView dv = new DataView();
            DataTable dtPatient = null;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                String Sourcefilename = "";
                String DestFilename = "";
                if (txtSourcePath.Text.ToString() != "" && txtDestinationPath.Text != "")
                {

                    #region "Read files"

                    bool _isMatched = false;
                    gloCCDLibrary.Patient occdpatient1 = null;
                    if (txtSourcePath.Text != "")
                    {
                        files = Directory.GetFiles(txtSourcePath.Text);
                    }
                    if (txtDestinationPath.Text != "")
                    {
                        filesdest = Directory.GetFiles(txtDestinationPath.Text);

                        if (filesdest.Length > 0)
                        {
                            
                            dtPatient = new DataTable();
                            dtPatient.Columns.Add("FirstName");
                            dtPatient.Columns.Add("LastName");
                            dtPatient.Columns.Add("DOB");
                            dtPatient.Columns.Add("XMLFileName");

                            for (int j = 0; j < filesdest.Length; j++)
                            {
                                DestFilename = Path.GetFullPath(filesdest[j]);
                                if (System.IO.Path.GetExtension(DestFilename) == ".xml")
                                {
                                    // oreconsilelist = qrdareader.ExtractCDA_DemographicsOnly(Sourcefilename, 0);
                                    oreconsilelist1 = qrdareader.ExtractCDA_DemographicsOnly(DestFilename, 0);
                                    occdpatient1 = oreconsilelist1.mPatient;
                                    DataRow dr = null;
                                    dr = dtPatient.NewRow();
                                    dr[0] = occdpatient1.PatientDemographics.DemographicsDetail.PatientFirstName;
                                    dr[1] = occdpatient1.PatientDemographics.DemographicsDetail.PatientLastName;
                                    dr[2] = occdpatient1.PatientDemographics.DemographicsDetail.PatientDOB;
                                    dr[3] = DestFilename;
                                    dtPatient.Rows.Add(dr);
                                }
                            }
                        }

                        dv = dtPatient.DefaultView;
                        if (occdpatient1 != null)
                        {
                            occdpatient1.Dispose();
                            occdpatient1 = null;
                        }
                        if (oreconsilelist1 != null)
                        {
                            oreconsilelist1.Dispose();
                            oreconsilelist1 = null;
                        }
                    }
                    if (files.Length > 0)
                    {
                       
                        strMisMatch.AppendLine("MisMatched :");
                        dtMeasureCodes = qrdareader.GetCQMMeasureCodes();
                        str.AppendLine("------------------------Start Compare-----------------------");
                        str.AppendLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond);
                        for (int m = 0; m < files.Length; m++)
                        {

                            string actualfilename = "";
                            actualfilename = Path.GetFileName(files[m]);
                            
                            Sourcefilename = Path.GetFullPath(files[m]);
                             if (System.IO.Path.GetExtension(Sourcefilename) == ".xml")
                            {
                            oreconsilelist = qrdareader.ExtractCDA_DemographicsOnly(Sourcefilename, 0);
                            gloCCDLibrary.Patient occdpatient = oreconsilelist.mPatient;
                            
                            _isMatched = false;
                           
                            dv.RowFilter = "FirstName='" + occdpatient.PatientDemographics.DemographicsDetail.PatientFirstName + "' and LastName='" + occdpatient.PatientDemographics.DemographicsDetail.PatientLastName + "' and DOB = '" + occdpatient.PatientDemographics.DemographicsDetail.PatientDOB + "'";
                            if (dv != null)
                            {
                                if (dv.Count == 1)
                                {
                                    DestFilename = Convert.ToString(dv.ToTable().Rows[0]["XMLFileName"]);

                                    str.AppendLine("---------------------------------****************--------------------------------------");
                                    str.AppendLine(Path.GetFileName(files[m]));
                                    _isMatched = true;

                                    #region "Compare Files"

                                    qrdareader.CompareQRDA = true;
                                    oreconsilelist = qrdareader.ExtractCDA(Sourcefilename, 0);
                                    oreconsilelist1 = qrdareader.ExtractCDA(DestFilename, 0);
                                    //bool _isHCPCS = false;
                                    bool _isEncounter = false;
                                    //bool _isSnomed = false;
                                    string strmsg = "";
                                    #region Encounters
                                    if (oreconsilelist.mPatient.PatientEncounters != null)
                                    {
                                        if (oreconsilelist.mPatient.PatientEncounters.Count > 0)
                                        {
                                            if (oreconsilelist1.mPatient.PatientEncounters != null)
                                            {
                                                if (oreconsilelist1.mPatient.PatientEncounters.Count > 0)
                                                {


                                                    int encountercount = 0;
                                                    int found = 0;


                                                    foreach (Encounters enc in oreconsilelist.mPatient.PatientEncounters)
                                                    {
                                                        found = encountercount;
                                                        //_isHCPCS = false;
                                                        _isEncounter = false;
                                                        //_isSnomed = false;
                                                        foreach (Encounters enc1 in oreconsilelist1.mPatient.PatientEncounters)
                                                        {

                                                            if (enc.DateOfService == enc1.DateOfService && enc.DischargeDate == enc1.DischargeDate)
                                                            {
                                                                if (enc.EncounterCode != null && enc.EncounterCode != "")
                                                                {
                                                                    if (enc.EncounterCode == enc1.EncounterCode)
                                                                    {
                                                                        _isEncounter = true;
                                                                    }
                                                                }
                                                                if (enc.HcpcsCode != null && enc.HcpcsCode != "")
                                                                {
                                                                    if (enc.HcpcsCode == enc1.HcpcsCode)
                                                                    {
                                                                        _isEncounter = true;
                                                                    }
                                                                }
                                                                if (enc.SnomedCode != null && enc.SnomedCode != "")
                                                                {
                                                                    if (enc.SnomedCode == enc1.SnomedCode)
                                                                    {
                                                                        _isEncounter = true;
                                                                    }
                                                                }

                                                            }

                                                            //   if (enc.EncounterCode != "" && enc.EncounterCode != null)
                                                            //   {
                                                            //       if (enc.EncounterCode == enc1.EncounterCode && enc.DateOfService == enc1.DateOfService && enc.DischargeDate == enc1.DischargeDate )
                                                            //       {
                                                            //           _isEncounter = true;
                                                            //         //  break;
                                                            //       }
                                                            //   }
                                                            //   if (enc.HcpcsCode != "" && enc.HcpcsCode != null)
                                                            //   {
                                                            //       if (enc.HcpcsCode == enc1.HcpcsCode && enc.DateOfService == enc1.DateOfService && enc.DischargeDate == enc1.DischargeDate )
                                                            //       {
                                                            //           _isEncounter = true;
                                                            //          // break;
                                                            //       }
                                                            //   }
                                                            //if (enc.SnomedCode != "" && enc.SnomedCode != null)
                                                            //   {
                                                            //       if (enc.DateOfService == enc1.DateOfService && enc.DischargeDate == enc1.DischargeDate && enc.SnomedCode == enc1.SnomedCode)
                                                            //       {
                                                            //           _isEncounter = true;
                                                            //          // break;
                                                            //       }
                                                            //   }




                                                        }
                                                        bool contains = false;
                                                        if (enc.EncounterCode != null)
                                                        {
                                                            if (_isEncounter == false)
                                                            {
                                                                if (Convert.ToString(enc.EncounterCode) != "" && enc.EncounterCode != null)
                                                                {

                                                                    contains = dtMeasureCodes.AsEnumerable().Any(row => enc.EncounterCode == row.Field<String>("Code"));
                                                                }
                                                                if (contains)
                                                                {
                                                                    strmsg = strmsg + "\n " + "Encounter Code  '" + enc.EncounterCode + "' HCPCS '" + enc.HcpcsCode + "' Snomed Code '" + enc.SnomedCode + "'  Date Of Service  '" + enc.DateOfService + "' Discharge Date  '" + enc.DischargeDate;
                                                                }

                                                            }
                                                        }

                                                        //if (found == encountercount)
                                                        //{
                                                        //    bool contains = false;
                                                        //    if (Convert.ToString(enc.EncounterCode) != "" && enc.EncounterCode != null)
                                                        //    {
                                                        //        contains = dtMeasureCodes.AsEnumerable().Any(row => enc.EncounterCode == row.Field<String>("Code"));
                                                        //    }
                                                        //    else if (Convert.ToString(enc.SnomedCode) != "" && enc.SnomedCode != null)
                                                        //    {
                                                        //        contains = dtMeasureCodes.AsEnumerable().Any(row => enc.SnomedCode == row.Field<String>("Code"));
                                                        //    }
                                                        //    else if (Convert.ToString(enc.HcpcsCode) != "" && enc.HcpcsCode != null)
                                                        //    {
                                                        //        contains = dtMeasureCodes.AsEnumerable().Any(row => enc.HcpcsCode == row.Field<String>("Code"));
                                                        //    }
                                                        //    if (contains)
                                                        //    {
                                                        //        strmsg = strmsg + "\n "+ "Encounter Code  '" + enc.EncounterCode + "' Date Of Service  '" + enc.DateOfService + "' Discharge Date  '" + enc.DischargeDate + "' Snomed Code  '" + enc.SnomedCode + "' HCPCS '"+ enc.HcpcsCode ;
                                                        //    }
                                                        //}
                                                    }

                                                    if (strmsg != "")
                                                    {
                                                        strmsg = "\n" + "Encounter Data is missing!!!!!!!" + "\n" + strmsg;
                                                        // MessageBox.Show(strmsg, "Encounter Data is missing!!!!!!!");
                                                    }
                                                    else
                                                    {
                                                        strmsg = strmsg + "\n" + "All Encounters are present!!!!!!!";
                                                        //    MessageBox.Show("All Encounters are present!!!!!!!");
                                                    }

                                                }
                                                else
                                                {
                                                    strmsg = strmsg + "\n" + " Encounters Section is missing!!!!!!";
                                                    // MessageBox.Show("Encounters Section is missing!!!!!!");
                                                }
                                            }
                                        }
                                    }
                                    if (strmsg != "")
                                    {
                                        str.AppendLine(strmsg);

                                    }
                                    #endregion

                                    #region Diagnosis
                                    string strdiagmsg = "";
                                    if (oreconsilelist.mPatient.PatientProblems != null)
                                    {
                                        if (oreconsilelist.mPatient.PatientProblems.Count > 0)
                                        {
                                            if (oreconsilelist1.mPatient.PatientProblems != null)
                                            {
                                                if (oreconsilelist1.mPatient.PatientProblems.Count > 0)
                                                {


                                                    foreach (Problems Prob in oreconsilelist.mPatient.PatientProblems)
                                                    {
                                                        bool isconceptidPresent = false;
                                                        bool isICDcodePresent = false;
                                                        foreach (Problems Prob1 in oreconsilelist1.mPatient.PatientProblems)
                                                        {
                                                            if (Prob.DateOfService == Prob1.DateOfService && Prob.DischargeDate == Prob1.DischargeDate && Prob.ConceptID == Prob1.ConceptID && Prob.ReasonConceptID == Prob1.ReasonConceptID)
                                                            {
                                                                isconceptidPresent = true;
                                                            }

                                                            if (Prob.DateOfService == Prob1.DateOfService && Prob.DischargeDate == Prob1.DischargeDate && Prob.ReasonConceptID == Prob1.ReasonConceptID)
                                                            {
                                                                if (Prob.ICD9Code != null)
                                                                {
                                                                    if (Prob.ICD9Code == Prob1.ICD9Code)
                                                                    {
                                                                        isICDcodePresent = true;
                                                                    }
                                                                }
                                                                if (Prob.ICD10Code != null)
                                                                {
                                                                    if (Prob.ICD10Code == Prob1.ICD10Code)
                                                                    {
                                                                        isICDcodePresent = true;
                                                                    }
                                                                }

                                                            }


                                                        }
                                                        bool contains = false;
                                                        if (Prob.ConceptID != null)
                                                        {
                                                            if (isconceptidPresent == false)
                                                            {
                                                                if (Convert.ToString(Prob.ConceptID) != "" && Prob.ConceptID != null)
                                                                {
                                                                    contains = dtMeasureCodes.AsEnumerable().Any(row => Prob.ConceptID == row.Field<String>("Code"));
                                                                }
                                                                if (contains)
                                                                {
                                                                    strdiagmsg = strdiagmsg + "\n Concept ID  '" + Prob.ConceptID + "' Date Of Service  '" + Prob.DateOfService + "' Discharge Date  '" + Prob.DischargeDate + "'";
                                                                }


                                                            }
                                                        }
                                                        if (Prob.ICD9Code != null || Prob.ICD10Code != null)
                                                        {
                                                            if (isICDcodePresent == false)
                                                            {
                                                                contains = dtMeasureCodes.AsEnumerable().Any(row => Prob.ICD9Code == row.Field<String>("Code") || Prob.ICD10Code == row.Field<String>("Code"));

                                                                if (contains)
                                                                {
                                                                    strdiagmsg = strdiagmsg + "\n ICD9 Code  '" + Prob.ICD9Code + "'  OR  ICD10Code '" + Prob.ICD10Code + "' Date Of Service  '" + Prob.DateOfService + "' Discharge Date  '" + Prob.DischargeDate + "'";
                                                                }

                                                                //namrata

                                                            }
                                                        }

                                                    }

                                                    if (strdiagmsg != "")
                                                    {
                                                        strdiagmsg = "Diagnosis Data is missing!!!!!!!" + "\n" + strdiagmsg;
                                                        // MessageBox.Show(strdiagmsg, "Diagnosis Data is missing!!!!!!!");
                                                    }
                                                    else
                                                    {
                                                        strdiagmsg = strdiagmsg + "All diagnosis are present!!!!!!!";
                                                        // MessageBox.Show("All diagnosis are present!!!!!!!");
                                                    }

                                                }

                                                else
                                                {
                                                    strdiagmsg = strdiagmsg + "Diagnosis Section is missing!!!!!!";
                                                    // MessageBox.Show("Diagnosis Section is missing!!!!!!");
                                                }
                                            }
                                        }
                                    }
                                    if (strdiagmsg != "")
                                    {
                                        str.AppendLine(strdiagmsg);
                                    }
                                    #endregion

                                    #region History
                                    if (oreconsilelist.mPatient.PatientHistory != null)
                                    {
                                        string strHistmsg = "";
                                        if (oreconsilelist.mPatient.PatientHistory.Count > 0)
                                        {
                                            if (oreconsilelist1.mPatient.PatientHistory != null)
                                            {
                                                if (oreconsilelist1.mPatient.PatientHistory.Count > 0)
                                                {


                                                    foreach (gloPatientHistory Hist in oreconsilelist.mPatient.PatientHistory)
                                                    {

                                                        bool isconceptidPresent = false;
                                                        bool isICDcodePresent = false;
                                                        bool isCPTcodePresent = false;
                                                        foreach (gloPatientHistory Hist1 in oreconsilelist1.mPatient.PatientHistory)
                                                        {


                                                            if (Hist.ConceptId != null)
                                                            {
                                                                if (Hist1.HistoryCategory == "Allergies")
                                                                {
                                                                    if (Hist.ConceptId == Hist1.ConceptId && Hist.ReasonConceptId == Hist1.ReasonConceptId)
                                                                    {
                                                                        isconceptidPresent = true;

                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (Hist.ConceptId == Hist1.ConceptId && Hist.OnsetDate == Hist1.OnsetDate && Hist.ReasonConceptId == Hist1.ReasonConceptId)
                                                                    {
                                                                        isconceptidPresent = true;

                                                                    }
                                                                }
                                                            }
                                                            if (Hist.CPT != null)
                                                            {
                                                                if (Hist.CPT == Hist1.CPT && Hist.OnsetDate == Hist1.OnsetDate && Hist.ReasonConceptId == Hist1.ReasonConceptId)
                                                                {
                                                                    isCPTcodePresent = true;
                                                                }
                                                            }
                                                            if (Hist.HCPCS != null)
                                                            {
                                                                if (Hist.HCPCS == Hist1.HCPCS && Hist.OnsetDate == Hist1.OnsetDate && Hist.ReasonConceptId == Hist1.ReasonConceptId)
                                                                {
                                                                    isCPTcodePresent = true;
                                                                }
                                                            }
                                                            if (Hist.ICD10 != null)
                                                            {
                                                                if (Hist.ICD10 == Hist1.ICD10 && Hist.OnsetDate == Hist1.OnsetDate && Hist.ReasonConceptId == Hist1.ReasonConceptId)
                                                                {
                                                                    isICDcodePresent = true;
                                                                }

                                                            }
                                                            if (Hist.ICD9 != null)
                                                            {
                                                                if (Hist.ICD9 == Hist1.ICD9 && Hist.OnsetDate == Hist1.OnsetDate && Hist.ReasonConceptId == Hist1.ReasonConceptId)
                                                                {
                                                                    isICDcodePresent = true;
                                                                }
                                                            }

                                                        }
                                                        bool contains = false;
                                                        if (Hist.ConceptId != null)
                                                        {
                                                            if (isconceptidPresent == false)
                                                            {

                                                                contains = dtMeasureCodes.AsEnumerable().Any(row => Hist.ConceptId == row.Field<String>("Code"));

                                                                if (contains)
                                                                {
                                                                    strHistmsg = strHistmsg + "\n Snomed Code  '" + Hist.ConceptId + "' Date Of Service  '" + Hist.OnsetDate + "'";
                                                                }

                                                            }
                                                        }
                                                        if (Hist.ICD9 != null || Hist.ICD10 != null)
                                                        {
                                                            if (isICDcodePresent == false)
                                                            {
                                                                contains = dtMeasureCodes.AsEnumerable().Any(row => Hist.ICD9 == row.Field<String>("Code") || Hist.ICD10 == row.Field<String>("Code"));

                                                                if (contains)
                                                                {
                                                                    strHistmsg = strHistmsg + "\n Date Of Service  '" + Hist.OnsetDate + "' ICD9 Code  '" + Hist.ICD9 + "'  OR  ICD10Code '" + Hist.ICD10 + "'";
                                                                }

                                                            }
                                                        }

                                                        if (Hist.CPT != null || Hist.HCPCS != null)
                                                        {
                                                            if (isCPTcodePresent == false)
                                                            {
                                                                contains = dtMeasureCodes.AsEnumerable().Any(row => Hist.HCPCS == row.Field<String>("Code") || Hist.CPT == row.Field<String>("Code"));



                                                                if (contains)
                                                                {
                                                                    strHistmsg = strHistmsg + "\n Date Of Service  '" + Hist.OnsetDate + "' CPT Code  '" + Hist.CPT + "' OR  HCPCSCode '" + Hist.HCPCS + "'";
                                                                }


                                                            }
                                                        }

                                                    }
                                                    //if (isconceptidPresent)
                                                    //{
                                                    //    strmsg = strmsg + "History Missing\n Snomed Code  '" + Hist.ConceptId + "' Date Of Service  '" + Hist.OnsetDate + "' CPT Code  '" + Hist.CPT + "'" + "ICD";
                                                    //}

                                                    if (strHistmsg != "")
                                                    {
                                                        strHistmsg = "History Data is missing!!!!!!!" + "\n" + strHistmsg;
                                                        // MessageBox.Show(strHistmsg, "History Data is missing!!!!!!!");
                                                    }
                                                    else
                                                    {
                                                        strHistmsg = strHistmsg + " All History are present!!!!!!!";
                                                        //  MessageBox.Show("All History are present!!!!!!!");
                                                    }
                                                }
                                                else
                                                {
                                                    strHistmsg = strHistmsg + " History Section is missing!!!!!!!";
                                                    //MessageBox.Show("History Section is missing!!!!!!!");
                                                }

                                            }

                                        }
                                        if (strHistmsg != "")
                                        {
                                            str.AppendLine(strHistmsg);
                                        }
                                    }

                                    #endregion

                                    #region "Immunizations"
                                    strmsg = "";
                                    if (oreconsilelist.mPatient.PatientImmunizations != null)
                                    {
                                        if (oreconsilelist.mPatient.PatientImmunizations.Count > 0)
                                        {
                                            if (oreconsilelist1.mPatient.PatientImmunizations != null)
                                            {
                                                if (oreconsilelist1.mPatient.PatientImmunizations.Count > 0)
                                                {
                                                    int imcount = 0;
                                                    int found = 0;
                                                    strmsg = "";

                                                    foreach (Immunization imm in oreconsilelist.mPatient.PatientImmunizations)
                                                    {

                                                        found = imcount;

                                                        foreach (Immunization imm1 in oreconsilelist1.mPatient.PatientImmunizations)
                                                        {

                                                            if (imm.VaccineCode == imm1.VaccineCode && (imm.ConceptID == imm1.ConceptID || imm.CPTCode == imm1.CPTCode) && imm.ImmunizationDate == imm1.ImmunizationDate && imm.ReasonConceptID == imm1.ReasonConceptID)
                                                            {
                                                                imcount++;
                                                                break;
                                                            }

                                                        }
                                                        if (found == imcount)
                                                        {
                                                            bool contains = false;

                                                            contains = dtMeasureCodes.AsEnumerable().Any(row => imm.VaccineCode == row.Field<String>("Code") || imm.ConceptID == row.Field<String>("Code") || imm.CPTCode == row.Field<String>("Code"));

                                                            if (contains)
                                                            {
                                                                strmsg = strmsg + "\n VaccineCode Code  '" + imm.VaccineCode + "' or conceptid '" + imm.ConceptID + "' or CPT '" + imm.CPTCode + "' Date Of Service  '" + imm.ImmunizationDate + " or Reason '" + imm.ReasonConceptID;
                                                            }

                                                        }

                                                    }
                                                    if (imcount < oreconsilelist.mPatient.PatientImmunizations.Count)
                                                    {
                                                        strmsg = " Immunization Data is missing!!!!!!!" + "\n" + strmsg;
                                                        // MessageBox.Show(strmsg, "Immunization Data is missing!!!!!!!");
                                                    }
                                                    else
                                                    {
                                                        strmsg = strmsg + " All Immunizations are present!!!!!!!";
                                                        //  MessageBox.Show("All Immunizations are present!!!!!!!");
                                                    }
                                                    //if (strmsg != "")
                                                    //{
                                                    //    MessageBox.Show(strmsg, "immunization Data is missing!!!!!!!");
                                                    //}
                                                    //else
                                                    //{
                                                    //    MessageBox.Show("All immunization are present!!!!!!!");
                                                    //}

                                                }
                                                else
                                                {
                                                    strmsg = strmsg + " Immunization Section is missing!!!!!!";
                                                    // MessageBox.Show("Immunization Section is missing!!!!!!");
                                                }
                                            }
                                        }
                                    }
                                    if (strmsg != "")
                                    {
                                        str.AppendLine(strmsg);
                                    }
                                    #endregion

                                    #region Medication
                                    string strmedmsg = "";
                                    if (oreconsilelist.mPatient.PatientMedications != null)
                                    {
                                        if (oreconsilelist.mPatient.PatientMedications.Count > 0)
                                        {
                                            if (oreconsilelist1.mPatient.PatientMedications != null)
                                            {

                                                if (oreconsilelist1.mPatient.PatientMedications.Count > 0)
                                                {
                                                    int medicationcnt = 0;
                                                    int found = 0;

                                                    foreach (Medication med in oreconsilelist.mPatient.PatientMedications)
                                                    {
                                                        found = medicationcnt;
                                                        foreach (Medication med1 in oreconsilelist1.mPatient.PatientMedications)
                                                        {
                                                            if (!string.IsNullOrEmpty(med.RxNormCode) && !string.IsNullOrEmpty(med1.RxNormCode))
                                                            {
                                                                if (med.Valueset == med1.Valueset && med.ReasonConceptID == med1.ReasonConceptID && med.StartDate == med1.StartDate && med.EndDate == med1.EndDate)
                                                                {
                                                                    medicationcnt++;
                                                                    break;
                                                                }
                                                            }

                                                        }
                                                        if (found == medicationcnt)
                                                        {
                                                            bool contains = false;
                                                            if (Convert.ToString(med.RxNormCode) != "" && med.RxNormCode != null)
                                                            {
                                                                contains = dtMeasureCodes.AsEnumerable().Any(row => med.RxNormCode == row.Field<String>("Code") && med.Valueset == row.Field<String>("ValueSetOID"));
                                                            }

                                                            if (contains)
                                                            {
                                                                strmedmsg = strmedmsg + "\n RxNorm Code  '" + med.RxNormCode + "' Date Of Service  '" + med.StartDate + "' Discharge Date  '" + med.EndDate + "' Reason Concept Id  '" + med.ReasonConceptID + "'";
                                                            }
                                                        }
                                                    }
                                                    if (strmedmsg != "")
                                                    {
                                                        strmsg = " Medication is Missing \n" + strmedmsg;
                                                        //  MessageBox.Show(strmedmsg, "Medication is Missing");
                                                    }
                                                    else
                                                    {
                                                        strmsg = strmsg + " All Medications are present";
                                                        // MessageBox.Show("All Medications are present");
                                                    }
                                                }
                                                else
                                                {


                                                    for (int i = 0; i < oreconsilelist.mPatient.PatientMedications.Count; i++)
                                                    {
                                                        bool contains = false;
                                                        if (oreconsilelist.mPatient.PatientMedications.ElementAt(i) != null)
                                                        {
                                                            if (Convert.ToString(oreconsilelist.mPatient.PatientMedications.ElementAt(i).RxNormCode) != "" && oreconsilelist.mPatient.PatientMedications.ElementAt(i).RxNormCode != null)
                                                            {
                                                                contains = dtMeasureCodes.AsEnumerable().Any(row => oreconsilelist.mPatient.PatientMedications.ElementAt(i).RxNormCode == row.Field<String>("Code") && oreconsilelist.mPatient.PatientMedications.ElementAt(i).Valueset == row.Field<String>("ValueSetOID"));
                                                            }
                                                            if (contains)
                                                            {
                                                                strmedmsg = strmedmsg + "\n RxNorm Code  '" + oreconsilelist.mPatient.PatientMedications.ElementAt(i).RxNormCode + "' Date Of Service  '" + oreconsilelist.mPatient.PatientMedications.ElementAt(i).StartDate + "' Discharge Date  '" + oreconsilelist.mPatient.PatientMedications.ElementAt(i).EndDate + "' Reason Concept Id  '" + oreconsilelist.mPatient.PatientMedications.ElementAt(i).ReasonConceptID + "'";
                                                            }
                                                        }

                                                    }
                                                    if (strmedmsg != "")
                                                    {
                                                        strmedmsg = " Medication Section is missing \n" + strmedmsg;
                                                        //MessageBox.Show("Medication Section is missing");
                                                    }

                                                }
                                            }
                                        }
                                        if (strmedmsg != "")
                                        {
                                            str.AppendLine(strmedmsg);
                                        }
                                    }

                                    #endregion

                                    #region LAbResults
                                    if (oreconsilelist.mPatient.PatientLabResult != null)
                                    {
                                        if (oreconsilelist.mPatient.PatientLabResult.Count > 0)
                                        {
                                            if (oreconsilelist1.mPatient.PatientLabResult != null)
                                            {
                                                string strlabmsg = "";
                                                int found = 0;
                                                int labcount = 0;
                                                if (oreconsilelist1.mPatient.PatientLabResult.Count > 0)
                                                {
                                                    foreach (LabResults lab in oreconsilelist.mPatient.PatientLabResult)
                                                    {
                                                        found = labcount;
                                                        foreach (LabResults lab1 in oreconsilelist1.mPatient.PatientLabResult)
                                                        {
                                                            if (lab.ResultValue == lab1.ResultValue && lab.ResultLOINCID == lab1.ResultLOINCID && lab.ResultReasonLOINC == lab1.ResultReasonLOINC && lab.ResultReasonICD9 == lab1.ResultReasonICD9 && lab.ResultReasonICD10 == lab1.ResultReasonICD10 && lab.ResultReasonConceptID == lab1.ResultReasonConceptID && lab.SpecimenDate == lab1.SpecimenDate && lab.ResultValue == lab1.ResultValue)
                                                            {
                                                                labcount++;
                                                                break;
                                                            }
                                                        }

                                                        if (found == labcount)
                                                        {
                                                            bool contains = false;

                                                            contains = dtMeasureCodes.AsEnumerable().Any(row => lab.ResultLOINCID == row.Field<String>("Code"));

                                                            if (contains)
                                                            {
                                                                strlabmsg = strlabmsg + "\n ResultLOINC ID   '" + lab.ResultLOINCID + " \t Specimen Date " + lab.SpecimenDate + " or Reason ConceptID " + lab.ResultReasonConceptID;
                                                            }
                                                        }
                                                    }

                                                    if (Convert.ToString(strlabmsg) != "")
                                                    {
                                                        strlabmsg = " Labresult Data is missing!!!!!!! \n " + strlabmsg;
                                                        //  MessageBox.Show(strlabmsg, "Labresult Data is missing!!!!!!!");
                                                    }
                                                    else
                                                    {
                                                        strlabmsg = strlabmsg + " All Labresults are present!!!!!!!";
                                                        // MessageBox.Show("All Labresults are present!!!!!!!");
                                                    }
                                                }
                                                if (strlabmsg != "")
                                                {
                                                    str.AppendLine(strlabmsg);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region "Vitals"
                                    strmsg = "";
                                    if (oreconsilelist.mPatient.PatientVitals != null)
                                    {
                                        if (oreconsilelist.mPatient.PatientVitals.Count > 0)
                                        {
                                            if (oreconsilelist1.mPatient.PatientVitals != null)
                                            {
                                                if (oreconsilelist1.mPatient.PatientVitals.Count > 0)
                                                {
                                                    int vitcount = 0;
                                                    int found = 0;
                                                    // strmsg = "";

                                                    foreach (Vitals vit in oreconsilelist.mPatient.PatientVitals)
                                                    {
                                                        bool _isBMI = false;
                                                        bool _isBP = false;
                                                        bool _isBPmax = false;
                                                        found = vitcount;

                                                        foreach (Vitals vit1 in oreconsilelist1.mPatient.PatientVitals)
                                                        {


                                                            if (vit.BloodPressureSittingMin != null)
                                                            {
                                                                if ((Convert.ToDecimal(vit.BloodPressureSittingMin) == Convert.ToDecimal(vit1.BloodPressureSittingMin)) && vit.VitalDate == vit1.VitalDate)
                                                                {
                                                                    _isBP = true;
                                                                }

                                                            }
                                                            if (vit.BloodPressureSittingMax != null)
                                                            {
                                                                if ((Convert.ToDecimal(vit.BloodPressureSittingMax) == Convert.ToDecimal(vit1.BloodPressureSittingMax)) && vit.VitalDate == vit1.VitalDate)
                                                                {
                                                                    _isBPmax = true;
                                                                }

                                                            }
                                                            if (vit.BMI != null)
                                                            {
                                                                if ((Convert.ToDecimal(vit.BMI) == Convert.ToDecimal(vit1.BMI)) && vit.VitalDate == vit1.VitalDate)
                                                                {
                                                                    _isBMI = true;
                                                                }
                                                                ;
                                                            }



                                                        }
                                                        if (_isBMI == false)
                                                        {
                                                            if (vit.BMI != null)
                                                            {

                                                                strmsg = strmsg + vit.BMI + "\t Vital Date : '" + vit.VitalDate;

                                                            }
                                                        }
                                                        if (_isBP == false)
                                                        {
                                                            if (vit.BloodPressureSittingMin != null)
                                                            {

                                                                strmsg = strmsg + vit.BloodPressureSittingMin + "\t Vital Date : '" + vit.VitalDate;

                                                            }
                                                        }
                                                        if (_isBPmax == false)
                                                        {
                                                            if (vit.BloodPressureSittingMax != null)
                                                            {

                                                                strmsg = strmsg + vit.BloodPressureSittingMax + "\t Vital Date : '" + vit.VitalDate;

                                                            }
                                                        }

                                                    }
                                                    if (strmsg != "")
                                                    {
                                                        strmsg = "Vitals Data is missing!!!!!!! \n" + strmsg;
                                                        // MessageBox.Show(strmsg, "Vitals Data is missing!!!!!!!");
                                                    }
                                                    else
                                                    {
                                                        strmsg = strmsg + "All Vitals are present!!!!!!!";
                                                        //MessageBox.Show("All Vitals are present!!!!!!!");
                                                    }


                                                }
                                            }
                                        }
                                    }
                                    //if (str.ToString() != "")
                                    //{
                                    //    str.AppendLine (Path.GetFileName(files[m]) + strmsg);
                                    //}
                                    #endregion

                                    #endregion "Compare Files"

                                }
                                else if (dv.Count > 1)
                                {
                                    strMulitpleMatch.AppendLine(Path.GetFileName(files[m]));
                                }
                            }
                            }
                            

                            if (_isMatched == false)
                            {
                                strMisMatch.AppendLine(Path.GetFileName(files[m]));
                              
                            }
                           

                        }
                        String exceptionLogPath = Application.StartupPath + "\\Log\\CompareQRDADetails";
                        if (CreateDirectoryIfNotExists(exceptionLogPath))
                        {
                            CMSNo++;
                            string _fileName = "";
                            _fileName = Environment.MachineName.ToString() + "-" + DateTime.Now.Date.ToString("yyyyMMdd") + "-" + CMSNo.ToString() + ".log";
                            strMulitpleMatch.AppendLine("----------------------------------End Compare--------------------------------------");
                            //str.AppendLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                            //File.AppendAllText(Application.StartupPath + "\\Log\\ExceptionLog\\" + _fileName, strLogMessage);
                            File.AppendAllText(exceptionLogPath + "\\" + _fileName, str.ToString() + "\n" + strMisMatch.ToString() + "\n" + "Multiple Matches :" + strMulitpleMatch +"\n" );

                            //str.AppendLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
                            //str.AppendLine("--------------------***End Compare***----------------------");
                            System.Diagnostics.Process.Start(exceptionLogPath + "\\" + _fileName);
                            
                        }
                      

                    }
                    #endregion

                   // MessageBox.Show("Files Compared Successfully !!!");
                }
                else
                {
                    MessageBox.Show("Please select Source path and Destination path to compare the files");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                this.Cursor = Cursors.Default;
                if (files != null)
                {
                    files = null;
                }
                if (filesdest  != null)
                {
                    filesdest = null;
                }
                if (dtMeasureCodes != null)
                {
                    dtMeasureCodes.Dispose();
                    dtMeasureCodes = null;
                }
                if (strMulitpleMatch != null)
                {
                    strMulitpleMatch.Clear();
                    strMulitpleMatch = null;
                }
                if (strMisMatch  != null)
                {
                    strMisMatch.Clear();
                    strMisMatch = null;
                }
                if (str != null)
                {
                    str.Clear();
                    str = null;
                }
                if (dtPatient != null)
                {
                    dtPatient.Dispose();
                    dtPatient = null;
                }
                if (dv != null)
                {
                    dv.Dispose();
                    dv = null;
                }
                if (oreconsilelist1!=null)
                {
                    oreconsilelist.Dispose();
                }
                if (oreconsilelist1!=null)
                {
                    oreconsilelist1.Dispose();
                }
                if (qrdareader !=null)
                {
                    qrdareader.Dispose();
                }
          
            }
           
        }


        public static bool CreateDirectoryIfNotExists(string dirName)
        {
            bool exists = false;
            try
            {
                try
                {
                    exists = System.IO.Directory.Exists(dirName);
                    if (exists)
                    {
                        return true;
                    }
                }
                catch
                {
                }
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(dirName);
                }
            }
            catch
            {
            }
            try
            {
                exists = System.IO.Directory.Exists(dirName);
            }
            catch
            {
            }
            return exists;
        }

        private void btnAddDestPath_Click(object sender, EventArgs e)
        {
            //OpenFileDialog fbd  =null;
            //try
            //{
            //    fbd = new OpenFileDialog();
            //    fbd.ShowDialog();
            //    txtDestinationPath.Text = fbd.FileName;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
               
            //}
            //finally
            //{
            //    if (fbd != null)
            //    {
            //        fbd.Dispose();
            //    }
            
            //}

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            try
            {

                DialogResult result = fbd.ShowDialog();

                if (fbd.SelectedPath != "")
                {

                    files = Directory.GetFiles(fbd.SelectedPath);
                    txtDestinationPath.Text = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fbd != null)
                {
                    fbd.Dispose();
                }

            }
          
        }

        private void btnAddSourcePath_Click(object sender, EventArgs e)
        {
            //OpenFileDialog fbd = null;
            //try
            //{
            //    fbd = new OpenFileDialog();
            //    fbd.ShowDialog();
            //    txtSourcePath.Text = fbd.FileName;
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    if (fbd != null)
            //    {
            //        fbd.Dispose();
            //    }

            //}
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            try
            {

                DialogResult result = fbd.ShowDialog();

                if (fbd.SelectedPath != "")
                {

                    files = Directory.GetFiles(fbd.SelectedPath);
                    txtSourcePath.Text = fbd.SelectedPath;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fbd != null)
                {
                    fbd.Dispose();
                }

            }
         
        }

        private void btnMouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloQRDAImport.Properties.Resources.Img_Orange;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloQRDAImport.Properties.Resources.Img_Button;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

    
    }
}
