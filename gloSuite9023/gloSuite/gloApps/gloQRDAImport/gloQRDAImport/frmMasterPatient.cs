using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Data.SqlClient;
using gloCCDLibrary;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using System.Collections;
//using Newtonsoft.Json;

namespace gloQRDAImport
{
    public partial class frmMasterPatient : Form
    {
        public string _Connstring = "";
        public frmMasterPatient()
        {
            InitializeComponent();
        }
        string[] files = new string[1000];
        string[] xmlfiles = new string[1000];
        List<string> lstjsoncpt = new List<string>();
        List<string> lstjsonsnomed = new List<string>();
        List<string> lstjsonhcpcs = new List<string>();
        List<string> lstjsondiag = new List<string>();
        List<string> lstjsonBloodPressureSittingMin = new List<string>();
        List<string> lstjsonBloodPressureSittingMax = new List<string>();
        private void cmdImport_Click(object sender, EventArgs e)
        {
          
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show( "Please Select the XML file Path","gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning  );
                return;
            }
            if (txtPath.Text.Trim() == "")
            {
                MessageBox.Show("Please Select the JSON file Path","gloEMR", MessageBoxButtons.OK,MessageBoxIcon.Warning );
                return;
            }
            try
            {

            this.Cursor = Cursors.WaitCursor;    
            cmdImport.Enabled = false;
            ImportXMLFiles();
            dtJsonXMl.Columns.Clear();
            dtJsonXMl.Rows.Clear();
            dtJsonXMl.Columns.Add("Json");
            dtJsonXMl.Columns.Add("XML");
          
                if (txtPath.Text != "")
                {
                    files = Directory.GetFiles(txtPath.Text);
                }

                if (files.Length > 0)
                {

                    for (int i = 0; i < files.Length; i++)
                    {

                        lstjsoncpt.Clear();
                        lstjsonsnomed.Clear();
                        lstjsonhcpcs.Clear();
                        lstjsondiag.Clear();
                        lstjsonBloodPressureSittingMin.Clear();
                        lstjsonBloodPressureSittingMax.Clear();
                        string filename = "";
                        string actualfilename = "";
                        actualfilename = Path.GetFileName(files[i]);
                        filename = Path.GetFullPath(files[i]);
                        if (filename.Contains("json"))

                            InsertjsonFileIntoDB(filename);
                        else
                        {
                            MessageBox.Show("Invalid File Format");
                            return;

                        }
                    }
                    dataGridView1.DataSource = dtJsonXMl;
                    if (dataGridView1.Columns.Count > 1)
                    {
                        dataGridView1.Columns[0].Width = 150;
                        dataGridView1.Columns[1].Width = 450;
                    }
                    Array.Clear(files, 0, files.Length);
                    MessageBox.Show("Processed Data Successfully");
                }

            }
            finally
            {
                cmdImport.Enabled = true;
                this.Cursor = Cursors.Arrow;   
            }
            //}
        }

        string JSONDeserialized { get; set; }
        public int indentLevel;
        DataTable dtJson = new DataTable();




        private bool JSONDictionarytoYAML(Dictionary<string, object> dict, string strkeyType)
        {
            bool bSuccess = false;
            indentLevel++;

            foreach (string strKey in dict.Keys)
            {
                // string strOutput = "".PadLeft(indentLevel * 3) + strKey + ":";
                // JSONDeserialized += "\r\n" + strOutput;
                int cpt = 0;
                int snomed = 0;
                int hcp = 0;
                dynamic o = dict[strKey];
                if (o is Dictionary<string, object>)
                {
                    JSONDictionarytoYAML((Dictionary<string, object>)o, strkeyType);
                }
                else if (o is ArrayList)
                {
                    foreach (dynamic oChild in ((ArrayList)o))
                    {

                        if (oChild is object[])
                        {
                            foreach (string oo in oChild)
                            {
                                if (strKey == "CPT")
                                {


                                    addJsonDT(strKey, Convert.ToString(oo), cpt, strkeyType);
                                }
                                else if ((strKey == "SNOMED-CT") || (strKey == "HCPCS") || (strKey == "ICD-9-CM") || (strKey == "ICD-10-CM"))
                                {
                                    addJsonDT(strKey, Convert.ToString(oo));
                                }
                            }
                        }
                        else if (oChild is string)
                        {
                            if (strKey == "CPT")
                            {


                                addJsonDT(strKey, Convert.ToString(oChild), cpt, strkeyType);
                            }
                            else if ((strKey == "SNOMED-CT") || (strKey == "HCPCS") || (strKey == "ICD-9-CM") || (strKey == "ICD-10-CM"))
                            {
                                addJsonDT(strKey, Convert.ToString(oChild));
                            }
                        }
                        else if (oChild is Dictionary<string, object>)
                        {
                            JSONDictionarytoYAML((Dictionary<string, object>)oChild, strkeyType);
                            JSONDeserialized += "\r\n";
                        }
                    }
                }
                else
                {
                    if (o is object[])
                    {
                        foreach (string oo in o)
                        {
                            if (strKey == "CPT")
                            {


                                addJsonDT(strKey, Convert.ToString(oo), cpt, strkeyType);
                            }
                            else if (strKey == "SNOMED-CT")
                            {
                                snomed += 1;

                                addJsonDT(strKey, Convert.ToString(oo), snomed, strkeyType);
                            }
                            else if (strKey == "HCPCS")
                            {
                                hcp += 1;

                                addJsonDT(strKey, Convert.ToString(oo), hcp, strkeyType);
                            }


                            else if ((strKey == "ICD-9-CM") || (strKey == "ICD-10-CM"))
                            {
                                addJsonDT(strKey, Convert.ToString(oo), 0, strkeyType);
                            }
                        }
                    }
                    else
                    {
                        if (strKey == "CPT")
                        {


                            addJsonDT(strKey, Convert.ToString(o), cpt, strkeyType);
                        }
                        else if (strKey == "SNOMED-CT")
                        {


                            addJsonDT(strKey, Convert.ToString(o), snomed, strkeyType);
                        }
                        else if (strKey == "HCPCS")
                        {


                            addJsonDT(strKey, Convert.ToString(o), hcp, strkeyType);
                        }


                        else if ((strKey == "ICD-9-CM") || (strKey == "ICD-10-CM"))
                        {
                            addJsonDT(strKey, Convert.ToString(o), 0, strkeyType);
                        }
                    }

                }
            }

            indentLevel--;

            return bSuccess;

        }
        private void addJsonDT(string ColumnName, string ColumnValue, int type = 0, string strKeyType = "")
        {
            if (strKeyType == "encounters")
            {
                if (ColumnName == "HCPCS")
                {
                    lstjsonhcpcs.Add(ColumnValue);
                }
                if (ColumnName == "SNOMED-CT")
                {
                    lstjsonsnomed.Add(ColumnValue);
                }
                if (ColumnName == "CPT")
                {
                    lstjsoncpt.Add(ColumnValue);
                }
            }
            else if (strKeyType == "conditions")
            {
                lstjsondiag.Add(ColumnValue);
            }

        }
        DataTable dtJsonXMl = new DataTable();
        StringBuilder sbxmlfilename = new StringBuilder();

        private void InsertjsonFileIntoDB(string filename)
        {

            using (StreamReader r = new StreamReader(filename))
            {




                string json = r.ReadToEnd();
                Item item = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Item>(json);
                //Item items = item22; 
                dtJson.Rows.Clear();
                DataRow drjs = dtJson.NewRow();
                dtJson.Rows.Add(drjs);
                dynamic items2 = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<dynamic>(json);
                List<string> keyarray = new List<string>();
                keyarray.Add("encounters");
                keyarray.Add("conditions");
                foreach (string strKey in items2.Keys)
                {
                    if ((strKey == "encounters") || (strKey == "conditions"))
                    {
                        dynamic o = items2[strKey];
                        if (o is object)
                        {
                            foreach (object oChild in ((object[])o))
                            {

                                if (oChild is Dictionary<string, object>)
                                {
                                    JSONDictionarytoYAML((Dictionary<string, object>)oChild, strKey);
                                    //  JSONDeserialized += "\r\n";
                                }
                            }
                        }
                        keyarray.Remove(strKey);
                        if (keyarray.Count == 0)
                            break;
                    }

                    //if (strKey == "conditions")
                    //{
                    //    dynamic o = items2[strKey];
                    //    if (o is object)
                    //    {
                    //        foreach (object oChild in ((object[])o))
                    //        {

                    //            if (oChild is Dictionary<string, object>)
                    //            {
                    //                JSONDictionarytoYAML((Dictionary<string, object>)oChild);
                    //                //  JSONDeserialized += "\r\n";
                    //            }
                    //        }
                    //    }
                    //    keyarray.Remove(strKey);
                    //}
                }

                DateTime birthdate = UnixTimeStampToDateTime(Convert.ToDouble(item.birthdate));
                //if (birthdate.Year == 1945)
                //{
                //    MessageBox.Show("month " + birthdate.Month + ": day " + birthdate.Day);     
                //    MessageBox.Show(filename);  
                //}
                //string search = "";
                DataRow[] drpat = null;
                if (dtpatient.Columns.Contains("birthdate"))
                    drpat = dtpatient.Select("birthdate='" + birthdate + "'");
                List<string> templxmlcpt = new List<string>();
                List<string> templxmlhcpcs = new List<string>();
                List<string> templxmlsnomed = new List<string>();
                List<string> templxmldiag = new List<string>();

                List<string> xmlfilename = new List<string>();
                List<string> tempxmlSystolicbp = new List<string>();
                List<string> tempxmlDiastolicbp = new List<string>();
                xmlfilename.Clear();
                AddSystolicDiastolicBP(item);
                foreach (DataRow dr in dtpatient.Rows )
                {
                    templxmlcpt.Clear();
                    templxmlhcpcs.Clear();
                    templxmlsnomed.Clear();
                    templxmldiag.Clear();
                    tempxmlSystolicbp.Clear();
                    tempxmlDiastolicbp.Clear();
                    templxmlcpt.AddRange(Convert.ToString(dr["CPT"]).Split(','));
                    templxmlhcpcs.AddRange(Convert.ToString(dr["HCPCS"]).Split(','));
                    templxmlsnomed.AddRange(Convert.ToString(dr["SNOMED-CT"]).Split(','));
                    templxmldiag.AddRange(Convert.ToString(dr["DiagCode"]).Split(','));
                    tempxmlSystolicbp.AddRange(Convert.ToString(dr["BloodPressureSittingMax"]).Split(','));
                    tempxmlDiastolicbp.AddRange(Convert.ToString(dr["BloodPressureSittingMin"]).Split(','));
                    templxmlcpt.RemoveAll(str => String.IsNullOrEmpty(str));
                    templxmlhcpcs.RemoveAll(str => String.IsNullOrEmpty(str));
                    templxmlsnomed.RemoveAll(str => String.IsNullOrEmpty(str));
                    templxmldiag.RemoveAll(str => String.IsNullOrEmpty(str));


                    tempxmlSystolicbp.RemoveAll(str => String.IsNullOrEmpty(str));
                    tempxmlDiastolicbp.RemoveAll(str => String.IsNullOrEmpty(str));
                    bool sno = templxmlsnomed.All(x => lstjsonsnomed.Contains(x));
                    bool hcp = templxmlhcpcs.All(x => lstjsonhcpcs.Contains(x));
                    bool cpt = templxmlcpt.All(x => lstjsoncpt.Contains(x));
                    bool diag = templxmldiag.All(x => lstjsondiag.Contains(x));
                    bool bp = tempxmlSystolicbp.All(x => lstjsonBloodPressureSittingMax.Contains(x)) && (tempxmlDiastolicbp.All(x => lstjsonBloodPressureSittingMin.Contains(x)));


                    if ((templxmlcpt.Count > 0) || (templxmlhcpcs.Count > 0) || (templxmlsnomed.Count > 0) || (templxmldiag.Count > 0) || (tempxmlSystolicbp.Count > 0) || (tempxmlDiastolicbp.Count > 0))
                    {
                        if (cpt && hcp && sno && diag && bp)
                        {
                            // MessageBox.Show(filename);  
                            if (xmlfilename.Contains(dr["XMLFileName"]) == false)
                            {
                                xmlfilename.Add(Convert.ToString(dr["XMLFileName"]));
                            }
                        }
                    }
                }  //foreach



                templxmlcpt.Clear();
                templxmlhcpcs.Clear();
                templxmlsnomed.Clear();
                templxmldiag.Clear();
                sbxmlfilename.Clear();
                tempxmlSystolicbp.Clear();
                tempxmlDiastolicbp.Clear();
                foreach (string strfilename in xmlfilename)
                {
                    sbxmlfilename.Append(strfilename);
                    sbxmlfilename.Append(",");
                }
                if (xmlfilename.Count > 0)
                {
                    DataRow drjsxml = dtJsonXMl.Rows.Add();
                    drjsxml[0] = Path.GetFileName(filename); ;
                    drjsxml[1] = sbxmlfilename.ToString();

                    // MessageBox.Show(filename, xmlFileName);
                    InsertMasterPatientList(item, birthdate);
                }
                // }
                // }
            }
        }
        public void AddSystolicDiastolicBP(Item items)
        {
            try
            {
                if (items.source_data_criteria != null)
                    foreach (source_data_criteria scri in items.source_data_criteria)
                    {
                        if (scri.id != null)
                        {
                            if (scri.id == "PhysicalExamPerformedSystolicBloodPressure")
                            {
                                foreach (BPvalues val in scri.value)
                                    if (val.Value != null)
                                    {
                                        if (lstjsonBloodPressureSittingMax.Contains(val.Value) == false)
                                            lstjsonBloodPressureSittingMax.Add(val.Value);
                                    }
                            }

                            else if (scri.id == "PhysicalExamPerformedDiastolicBloodPressure")
                            {
                                foreach (BPvalues val in scri.value)
                                    if (val.Value != null)
                                    {
                                        if (lstjsonBloodPressureSittingMin.Contains(val.Value) == false)
                                            lstjsonBloodPressureSittingMin.Add(val.Value);
                                    }
                            }
                        }
                    }
            }
            catch
            {
            }


        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        private void InsertMasterPatientList(Item items, DateTime birthdate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            DataTable dtmeasurelist = null;
            try
            {
                dtmeasurelist = null;
                dtmeasurelist = new DataTable();
                dtmeasurelist.Columns.Clear();
                dtmeasurelist.Rows.Clear();
                dtmeasurelist.Columns.Add("MeasureID", typeof(string));
                dtmeasurelist.Columns.Add("IPP", typeof(int));
                dtmeasurelist.Columns.Add("DENOM", typeof(int));
                dtmeasurelist.Columns.Add("DENEX", typeof(int));
                dtmeasurelist.Columns.Add("NUMER", typeof(int));
                dtmeasurelist.Columns.Add("DENEXCEP", typeof(int));
                foreach (expected_values except in items.expected_values)
                {
                    if (except.population_index == 0)
                    {
                        dtmeasurelist.Rows.Add(except.measure_id, except.IPP, except.DENOM, except.DENEX, except.NUMER, except.DENEXCEP);
                        //if (except.population_index == 1)
                        //    MessageBox.Show("he");
                    }
                    else
                    {
                        if (except.measure_id == "9A031BB8-3D9B-11E1-8634-00237D5BF174")
                        {
                            dtmeasurelist.Rows.Add(except.measure_id, except.IPP, except.DENOM, except.DENEX, except.NUMER, except.DENEXCEP);

                        }
                    }
                }

                con = new SqlConnection(_Connstring);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd = new SqlCommand("CQM_InsertMasterPatientList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", items.first);
                cmd.Parameters.AddWithValue("@MiddleName", "");
                cmd.Parameters.AddWithValue("@LastName", items.last);
                cmd.Parameters.AddWithValue("@Gender", items.gender);
                cmd.Parameters.AddWithValue("@DateOfBirth", birthdate);

                cmd.Parameters.AddWithValue("@Race", items.race.name);
                cmd.Parameters.AddWithValue("@Ethnicity", items.ethnicity.name);

                SqlParameter para = cmd.Parameters.Add("@tvpMasterPatientListDetails", dtmeasurelist);
                para.SqlDbType = SqlDbType.Structured;
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (con != null)
                {
                    con.Dispose();
                    con = null;
                }
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (dtmeasurelist != null)
                {
                    dtmeasurelist.Dispose();
                    dtmeasurelist = null;
                }

            }
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            try
            {

                DialogResult result = fbd.ShowDialog();

                if (fbd.SelectedPath != "")
                {
                    txtPath.Text = fbd.SelectedPath;
                    cmdImport.Enabled = true;
                }
            }
            catch
            {
            }
        }
        DataTable dtpatient = new DataTable();



        private void button1_Click(object sender, EventArgs e)
        {
            Array.Clear(xmlfiles, 0, xmlfiles.Length);
            Array.Resize(ref xmlfiles, 0);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            dtpatient.Rows.Clear();
            try
            {

                DialogResult result = fbd.ShowDialog();

                if (fbd.SelectedPath != "")
                {
                    textBox1.Text = fbd.SelectedPath;
                    // cmdImport.Enabled = true;
                }
            }
            catch
            {
            }


        }

        private void ImportXMLFiles()
        {
            if (textBox1.Text != "")
            {

                xmlfiles = Directory.GetFiles(textBox1.Text);
            }

            if (xmlfiles.Length > 0)
            {


                gloCCDLibrary.gloQRDAReader qrdareader = new gloQRDAReader();
                ReconcileList oreconsilelist = null;
                dtpatient.Columns.Clear();
                dtpatient.Rows.Clear();
                dtpatient.Columns.Add("birthdate");

                dtpatient.Columns[0].DataType = typeof(DateTime);
                dtpatient.Columns.Add("CPT");
                dtpatient.Columns.Add("SNOMED-CT");
                dtpatient.Columns.Add("HCPCS");
                dtpatient.Columns.Add("ICD-9-CM");
                dtpatient.Columns.Add("ICD-10-CM");
                dtpatient.Columns.Add("XMLFileName");
                dtpatient.Columns.Add("FirstName");
                dtpatient.Columns.Add("MiddleName");
                dtpatient.Columns.Add("LastName");
                dtpatient.Columns.Add("Checkbirthdate");
                dtpatient.Columns.Add("Gender");
                dtpatient.Columns.Add("DiagCode");
                dtpatient.Columns.Add("BloodPressureSittingMin");
                dtpatient.Columns.Add("BloodPressureSittingMax");
                XmlDocument xd = new XmlDocument();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < xmlfiles.Length; i++)
                {

                    DataRow drpatrow = dtpatient.NewRow();
                    foreach (DataColumn dc in dtpatient.Columns)
                    {
                        if (dc.ColumnName != "birthdate")
                        {
                            drpatrow[dc] = "";
                        }
                    }
                    string filename = "";
                    string actualfilename = "";
                    actualfilename = Path.GetFileName(xmlfiles[i]);
                    drpatrow["XMLFileName"] = actualfilename;
                    filename = Path.GetFullPath(xmlfiles[i]);
                    qrdareader.CompareQRDA = true;
                    oreconsilelist = qrdareader.ExtractCDA(filename, 1);
                    int hcp = 0;
                    int cpt = 0;
                    int snomed = 0;
                    lstjsoncpt.Clear();
                    lstjsonhcpcs.Clear();
                    lstjsonsnomed.Clear();
                    lstjsondiag.Clear();
                    lstjsonBloodPressureSittingMin.Clear();
                    lstjsonBloodPressureSittingMax.Clear();
                    if (oreconsilelist != null)
                    {
                        gloCCDLibrary.Patient opatient = null;
                        opatient = oreconsilelist.mPatient;

                        drpatrow["FirstName"] = opatient.PatientDemographics.DemographicsDetail.PatientFirstName; ;
                        drpatrow["MiddleName"] = opatient.PatientDemographics.DemographicsDetail.PatientMiddleName;
                        drpatrow["LastName"] = opatient.PatientDemographics.DemographicsDetail.PatientLastName;
                        drpatrow["Checkbirthdate"] = opatient.PatientDemographics.DemographicsDetail.PatientDOB;
                        drpatrow["Gender"] = opatient.PatientDemographics.DemographicsDetail.PatientGender;
                        //  oreconsilelist.mPatient .PatientDemographics 




                        if (oreconsilelist.mPatient.PatientVitals != null)
                        {
                            if (oreconsilelist.mPatient.PatientVitals.Count > 0)
                            {
                                //int vitcount = 0;
                                //int found = 0;
                                //// strmsg = "";
                                //bool _isBP = false;
                                //bool _isBPmax = false;
                                //bool _isBMI = false;

                                foreach (Vitals vit in oreconsilelist.mPatient.PatientVitals)
                                {


                                    if (vit.BloodPressureSittingMin != null)
                                    {
                                        if ((Convert.ToDecimal(vit.BloodPressureSittingMin) == Convert.ToDecimal(vit.BloodPressureSittingMin)) && vit.VitalDate == vit.VitalDate)
                                        {
                                            if (lstjsonBloodPressureSittingMin.Contains(Convert.ToString(vit.BloodPressureSittingMin)) == false)
                                            {
                                                drpatrow["BloodPressureSittingMin"] = drpatrow["BloodPressureSittingMin"] + "," + Convert.ToString(vit.BloodPressureSittingMin);
                                                lstjsonBloodPressureSittingMin.Add(Convert.ToString(vit.BloodPressureSittingMin));
                                            }
                                        }

                                    }
                                    if (vit.BloodPressureSittingMax != null)
                                    {
                                        if ((Convert.ToDecimal(vit.BloodPressureSittingMax) == Convert.ToDecimal(vit.BloodPressureSittingMax)) && vit.VitalDate == vit.VitalDate)
                                        {
                                            if (lstjsonBloodPressureSittingMax.Contains(Convert.ToString(vit.BloodPressureSittingMax)) == false)
                                            {
                                                drpatrow["BloodPressureSittingMax"] = drpatrow["BloodPressureSittingMax"] + "," + Convert.ToString(vit.BloodPressureSittingMax);
                                                lstjsonBloodPressureSittingMax.Add(Convert.ToString(vit.BloodPressureSittingMax));
                                            }
                                        }

                                    }

                                    //if (vit.BMI != null)
                                    //{
                                    //    if ((Convert.ToDecimal(vit.BMI) == Convert.ToDecimal(vit.BMI)) && vit.VitalDate == vit.VitalDate)
                                    //    {
                                    //        _isBMI = true;
                                    //    }

                                    //}

                                }

                            }
                        }















                        foreach (Problems Prob in oreconsilelist.mPatient.PatientProblems)
                        {
                            if (Prob.ICD9Code != null)
                                if ((Convert.ToString(Prob.ICD9Code).Trim() != ""))
                                {


                                    if (lstjsondiag.Contains(Prob.ICD9Code) == false)
                                    {
                                        drpatrow["DiagCode"] = drpatrow["DiagCode"] + "," + Prob.ICD9Code;
                                        lstjsondiag.Add(Prob.ICD9Code);
                                    }
                                }
                            if (Prob.ICD10Code != null)
                                if ((Convert.ToString(Prob.ICD10Code).Trim() != ""))
                                {


                                    if (lstjsondiag.Contains(Prob.ICD10Code) == false)
                                    {
                                        drpatrow["DiagCode"] = drpatrow["DiagCode"] + "," + Prob.ICD10Code;
                                        lstjsondiag.Add(Prob.ICD10Code);
                                    }
                                }
                            if (Prob.ConceptID != null)
                                if ((Convert.ToString(Prob.ConceptID).Trim() != ""))
                                {


                                    if (lstjsondiag.Contains(Prob.ConceptID) == false)
                                    {
                                        drpatrow["DiagCode"] = drpatrow["DiagCode"] + "," + Prob.ConceptID;
                                        lstjsondiag.Add(Prob.ConceptID);
                                    }
                                }
                        }

                        foreach (Encounters oenc in oreconsilelist.mPatient.PatientEncounters)
                        {
                            if (oenc.HcpcsCode != null)
                                if ((Convert.ToString(oenc.HcpcsCode).Trim() != ""))
                                {
                                    hcp++;

                                    if (lstjsonhcpcs.Contains(oenc.HcpcsCode) == false)
                                    {
                                        drpatrow["HCPCS"] = drpatrow["HCPCS"] + "," + oenc.HcpcsCode;
                                        lstjsonhcpcs.Add(oenc.HcpcsCode);
                                    }
                                }
                            if (oenc.EncounterCode != null)
                                if ((Convert.ToString(oenc.EncounterCode).Trim() != ""))
                                {
                                    cpt++;
                                    if (lstjsoncpt.Contains(oenc.EncounterCode) == false)
                                    {
                                        drpatrow["CPT"] = drpatrow["CPT"] + "," + oenc.EncounterCode;
                                        lstjsoncpt.Add(oenc.EncounterCode);
                                    }
                                }
                            if (oenc.SnomedCode != null)
                                if ((Convert.ToString(oenc.SnomedCode).Trim() != ""))
                                {
                                    snomed++;
                                    if (lstjsonsnomed.Contains(oenc.SnomedCode) == false)
                                    {
                                        drpatrow["SNOMED-CT"] = drpatrow["SNOMED-CT"] + "," + oenc.SnomedCode;
                                        lstjsonsnomed.Add(oenc.SnomedCode);
                                    }
                                }
                        }
                        xd.Load(filename);
                        sb.Clear();
                        sb.Append(xd.OuterXml.ToString());
                        string strbirthtime = sb.ToString().Substring(sb.ToString().IndexOf("birthTime"), 50);
                        string[] strspl = strbirthtime.Split('"');
                        if (strspl.Length > 1)
                        {
                            DateTime dtdob = DateTime.Now;

                            try
                            {

                                dtdob = DateTime.ParseExact(strspl[1].ToString(), "yyyyMMddHHmmss", null);

                            }
                            catch (FormatException )
                            {
                                dtdob = DateTime.ParseExact(strspl[1].ToString(), "yyyyMMdd", null);
                            }
                            drpatrow["birthdate"] = dtdob;
                            DataRow[] drdtpat = dtpatient.Select("FirstName='" + Convert.ToString(drpatrow["FirstName"]) + "' AND MiddleName='" + Convert.ToString(drpatrow["MiddleName"]) + "' AND LastName='" + Convert.ToString(drpatrow["LastName"]) + "' AND Gender='" + Convert.ToString(drpatrow["Gender"]) + "' And Checkbirthdate='" + Convert.ToString(drpatrow["Checkbirthdate"]) + "'");//Checkbirthdate
                            if (drdtpat.Length == 0)
                                dtpatient.Rows.Add(drpatrow);
                            Array.Clear(strspl, 0, strspl.Length);

                        }
                        lstjsoncpt.Clear();
                        lstjsonhcpcs.Clear();
                        lstjsonsnomed.Clear();
                        lstjsondiag.Clear();
                        lstjsonBloodPressureSittingMin.Clear();
                        lstjsonBloodPressureSittingMax.Clear();
                    }

                }
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 & e.RowIndex >= 0)
            {
                toolTip1.SetToolTip(dataGridView1, Convert.ToString(dataGridView1[e.ColumnIndex, e.RowIndex].Value));
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
