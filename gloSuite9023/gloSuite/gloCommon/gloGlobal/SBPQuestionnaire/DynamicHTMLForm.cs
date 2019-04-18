using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gloGlobal.Common//DynamicHTMLFormSBP
{


    public partial class frmDynamicHTMLForm : Form
    {


        DataSet SBPData;//used when form is opened for first time for a patient.
        DataSet SBPTransactionData;//used to retrieve data against a patient.
        DataView dvSBPDataQue = null; //in SBPdataset table(0) is questions table
        DataView dvSBPDataAns = null;//in SBPdataset table(1) is answers table
        DataTable dtPatientData = null;

        String PatientCode = "";
        String PatientName = "";
        String PatientDOB = "";
        //    String PatientAge = "";
        String PatientGender = "";
        String PatientMaritalStatus = "";
        bool isDataModified = false;//for adding is audit

       

        public frmDynamicHTMLForm()
        {
            InitializeComponent();
        }


        private void frmDynamicHTMLForm_Load(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            try
            {
                wbBrowserCtrl.IsWebBrowserContextMenuEnabled = false;
                wbBrowserCtrl.AllowWebBrowserDrop = false;
                wbBrowserCtrl.WebBrowserShortcutsEnabled = false;

                oDB = new gloDatabaseLayer.DBLayer(DynamicFormData.AppConnectionString);
                oDB.Connect(false);

                oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Add("@PatientID", DynamicFormData.nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_getPatientName", oDBParameters, out dtPatientData);

                SetWindowTitle(dtPatientData);

                oDBParameters.Clear();

                oDBParameters.Add("@nPatientId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_FillSBPQuestionnariedata", oDBParameters, out SBPData);

                oDB.Disconnect();

                if (SBPData.Tables.Count > 1)
                {
                    dvSBPDataQue = new DataView(SBPData.Tables[0]);
                    dvSBPDataAns = new DataView(SBPData.Tables[1]);

                }
                else
                {
                    //SetSBPTransactionData(SBPData.Tables[0]);
                }


                int maxVal = 0;
                maxVal = Convert.ToInt32(SBPData.Tables[0].Compute("MAX([nDomainSeqno])", ""));

                string webpage = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'> " +
                                "<html xmlns='http://www.w3.org/1999/xhtml'> <head>" +
                                "<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />" +
                                "<title></title> " +
                                @"<style type='text/css'>
                                h3 {
	                                margin: 5px;
	                                padding: 0px;
                                }
                                body {
	                                border: 0;
	                                font-family: arial;
	                                font-size: 12px;
	                                color: #0069aa;
	                                background: #fefdfd;
	                                margin: 0px;
	                                padding: 8px;
                                }
                                table {
	                                border-collapse: collapse;
	                                border-spacing: 0;
	                                max-width: 100%;
	                                margin-bottom: 20px;
	                                width: 100%;
	                                border: 1px solid #DDDDDD;
                                }
                                .table tr, {
                                 border: 1px solid #DDDDDD;
                                 line-height: 20px;
                                 padding: 5px;
                                 text-align: left;
                                 vertical-align: top;
                                }
                                .table td {
	                                border-left: 0px solid #DDDDDD;
	                                border-right: 0px solid #DDDDDD;
	                                border-top: 0px solid #DDDDDD;
                                 border-bottom:: 0px solid #DDDDDD;
	                                line-height: 20px;
	                                padding: 5px;
	                                text-align: left;
	                                vertical-align: top;
	                                padding-left: 20px;
                                }
                                li {
	                                padding: 6px;
	                                text-outline: none;
                                }
                                ul li a:link {
	                                font-size: 14px;
	                                font-family: Arial, sans-serif;
	                                color : #026CAA;
	                                font-weight: bold;
	                                text-decoration: none;
                                }
                                ul li a:hover {
	                                font-size: 14px;
	                                font-family: Arial, sans-serif;
	                                color : #F1A812;
	                                font-weight: bold;
	                                text-decoration: none;
                                }
                                ul li a:active {
	                                font-size: 14px;
	                                font-family: Arial, sans-serif;
	                                color : #000;
	                                font-weight: bold;
	                                text-decoration: none;
                                }
                                .table-bordered-header {
	                                border: 2px solid #0072BC;
                                }
                                th {
	                                background: #E9E9E9;
	                                color: #000;
	                                font-size: 12px;
	                                padding: 2px;
	                                text-align: left;
	                                border-left: 0px solid #c7c7c7;
                                }
                                .page-header {
	                                background-color: #E9E9E9;
	                                border-bottom: 1px solid #DDDDDD;
	                                margin: 0 0 20px;
	                                padding: 8px 10px 8px;
                                }
                                .logoCenter {
	                                background: none repeat scroll 0 0 #026CAA;
	                                color: #FFFFFF;
	                                filter: none;
	                                padding: 1px;
                                }
                                .contenth2 {
	                                font-size: 18px;
	                                font-family: Arial, sans-serif;
	                                color: #333333;
                                }
                                .contenth3 {
	                                font-size: 24px;
	                                font-family: Arial, sans-serif;
                                }
                                a:link {
	                                font-size: 16px;
	                                font-family: Arial, sans-serif;
	                                color : #000;
	                                font-weight: bold;
	                                text-decoration: none;
                                }
                                a:visited {
	                                font-size: 16px;
	                                font-family: Arial, sans-serif;
	                                color : #000;
	                                font-weight: bold;
	                                text-decoration: none;
                                }
                                a:active {
	                                color: #000;
	                                text-decoration: none;
                                }
                                a:hover {
	                                font-size: 16px;
	                                font-family: Arial, sans-serif;
	                                color : #F1A812;
	                                font-weight: bold;
	                                text-decoration: none;
                                }
                                select {
	                                padding: 3px;
	                                margin: 0;
	                                border-radius: 2px;
	                              
	                               
	                                border: 1px solid #ccc;
	                                outline: none;
	                                display: inline-block;
	                                appearance: none;
	                                cursor: pointer;
	                                width: 100%
                                }
                                input[type=text] {
	                               
	                                padding: 3px;
	                                border: 1px solid #ccc;
	                                -webkit-border-radius: 2px;
	                                border-radius: 2px;
	                                width: 100%
                                }
                                input[type=text]:focus {
	                                border-color: #DDDDDD;
	                               
                                }
                                input[type=submit] {
	                                padding: 3px;
	                                background: #ccc;
	                                border: 1px solid #ccc;
	                                cursor: pointer;
	                                -webkit-border-radius: 5px;
	                                border-radius: 2px;
	                                width: 100%
                                }
                                </style>" +
                                               @"             </head><body>
                                               <div class='logoCenter'>
                                                  <h3 align='center' class='contenth3'  > Social, Psychological, and Behavioral Data </h3>
                                                </div>               
                                <table width='100%' class='table table-condensed table-bordered-header'   >
                                  <tr >
                                    <td width='5%'  valign='top' > Patient: </td>
                                    <td width='45%'  valign='top' ><b> " + PatientName + @" </b></td>
                                    <td width='15%'  valign='top'> Date of Birth: </td>
                                    <td   width='35%'  valign='top'><b> " + PatientDOB + @"</b></td>
                                    </tr>
                                    <tr> 
                                    <td width='5%'  valign='top'> Gender: </td>
                                    <td   width='45%'  valign='top'><b> " + PatientGender + @"</b></td>
                                    <td width='10%'  valign='top'> Provider: </td>
                                    <td   width='40%'  valign='top'><b> " + DynamicFormData.PatientProviderName + @"</b></td>
                                    </tr>
                                </table>";

                for (int i = 1; i <= maxVal; i++)
                {

                    dvSBPDataQue.RowFilter = "ndomainseqno=" + i; // query example = "id = 10"
                    dvSBPDataQue.Sort = "nQuestionSeqNo";

                    int groupid = 0;

                    foreach (DataRowView rowView in dvSBPDataQue)
                    {
                        DataRow row = rowView.Row;
                        if (row["sProperty"].ToString() == "-")
                        {
                            continue;
                        }
                        if (groupid == 0)
                        {
                            webpage = webpage +
                                  "<table class='table'> <tr>" +
                                  " <th width='80%' >" + row["sDomainName"] + "</th>" +
                                  "<th width='20%'>" +
                                    "<label>" +
                                      "<input type='checkbox' name='checkbox' value='checkbox' id='" + row["sLOINCCode"].ToString() + "_chk_" + row["nDomainSeqNo"].ToString() + "' />" +
                                      "Decline to specify</label>" +
                                   "</th>  " +
                                "</tr>";

                        }
                        groupid = 1;


                        if (row["sInputType"].ToString() == "ComboBox" || row["sInputType"].ToString() == "TextBox")
                        {
                            webpage = webpage + "<tr>   <td>" +
                                row["sQuestion"] + "</td> </tr>";
                        }
                        if (row["sInputType"].ToString() == "Score")
                        {
                            webpage = webpage + "<tr>   <td>" +
                              row["sQuestion"] + "</td>";
                        }

                        if (row["sInputType"].ToString() == "ComboBox")
                        {

                            dvSBPDataAns.RowFilter = "Loinc='" + row["sLOINCCode"].ToString() + "'";
                            dvSBPDataAns.Sort = "Sequenceno";
                            string option = "<option value=''>Please select</option>";

                            foreach (DataRowView rowView1 in dvSBPDataAns)
                            {
                                DataRow row1 = rowView1.Row;
                                option = option + "<option value=" + row1["AnswerStringID"].ToString() + "_" + row1["Score"].ToString() + ">" + row1["DisplayText"].ToString() + " </option>";
                            }


                            webpage = webpage + "<tr> <td>" +
                                   "<label>" +
                                     "<select name=" + row["sLOINCCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString() + ">" +
                                       option +
                                     "</select>" +
                                     "</label>" +
                                    "</td></tr>";


                        }

                        if (row["sInputType"].ToString() == "Score")
                        {
                            webpage = webpage + "<td style='font-weight: bolder'>" +
                                 "<label id='" + row["sLOINCCode"].ToString() + "_Score'>" +
                                   "</label>" +
                                  "</td> </tr>";
                        }

                        if (row["sInputType"].ToString() == "TextBox")
                        {
                            string txttype = "";
                            if (row["sProperty"].ToString() == "Score")
                            {
                                txttype = "readonly";
                            }
                            webpage = webpage + "<tr><td>" +
                             "<label>" +
                               "<input " + txttype + " type='text'  id='" + row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString() + "' onkeydown='return ( event.ctrlKey || event.altKey " +
           "|| (47<event.keyCode && event.keyCode<58 && event.shiftKey==false) " +
           "|| (95<event.keyCode && event.keyCode<106)" +
           "|| (event.keyCode==8) || (event.keyCode==9) " +
           "|| (event.keyCode>34 && event.keyCode<40) " +
           "|| (event.keyCode==46) )';" +
 "/> </label> </td> <td>" + row["sUCUMUnits"].ToString() +
                             "  " +
                           "</td> </tr>";


                        }

                        //if (row["sProperty"].ToString() != "-")
                        //{
                        //    webpage = webpage + "<td>&nbsp;</td><td>&nbsp;</td></tr>";
                        //}
                    }
                    webpage = webpage + "</table>";
                    dvSBPDataQue.RowFilter = "";//clear previously set row filter value

                }

                dvSBPDataQue.RowFilter = "";//clear previously set row filter value



                //wbBrowserCtrl.DocumentText = webpage + "</table><button id='submit' type='button'>Submit</button></body></html>";
                wbBrowserCtrl.DocumentText = webpage + "</table></body></html>";




            }
            catch (Exception ex)
            {
                oDB.Disconnect();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDB != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (SBPData != null)
                {
                    SBPData.Dispose();
                    SBPData = null;
                }
            }
            finally
            {
                oDB.Disconnect();
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityCategory.ViewedSocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityType.Open, "Viewed Social Psychological Behavioral observations form", DynamicFormData.nPatientId, 0, DynamicFormData.nPatientProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
            }


        }

        private void SetWindowTitle(DataTable PatientData)
        {


            try
            {

                if ((PatientData == null) == false)
                {
                    if (PatientData.Rows.Count > 0)
                    {
                        PatientCode = Convert.ToString(PatientData.Rows[0]["sPatientCode"]);
                        PatientName = Convert.ToString(PatientData.Rows[0]["PatientName"]);
                        PatientDOB = Convert.ToString(PatientData.Rows[0]["dtDOB"]);
                        PatientGender = Convert.ToString(PatientData.Rows[0]["sGender"]);
                        PatientMaritalStatus = Convert.ToString(PatientData.Rows[0]["sMaritalStatus"]);

                        this.Text = this.Text + " - " + PatientName + "( " + PatientCode + " )";

                    }
                }

            }
            catch (Exception ex)
            {
                if (PatientData != null)
                {
                    PatientData.Dispose();
                    PatientData = null;
                }
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (PatientData != null)
                {
                    PatientData.Dispose();
                    PatientData = null;
                }

            }


        }


        public void SetMaxLengthAttributes()
        {
            try
            {
                //((mshtml.HTMLDocumentClass)(wbBrowserCtrl.Document.DomDocument)).lastModified;
                if (dvSBPDataQue != null)
                {

                    dvSBPDataQue.RowFilter = "sInputType like" + "'" + "TextBox" + "'";
                    dvSBPDataQue.Sort = "nDomainSeqNo";
                    foreach (DataRowView rowView in dvSBPDataQue)
                    {

                        DataRow row = rowView.Row;

                        switch (row["sLOINCCode"].ToString())
                        {
                            //Domain Name - Physical Activity
                            //Question 1 - How many days of moderate to strenuous exercise, like a brisk walk, did you do in the last 7D? 
                            //Unit - d/(7.d) OR days/7 days - therfore the min accepted value can be 0 zero and max value can be 7
                            case "68515-6":
                                wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).SetAttribute("maxlength", "1");

                                //there is no minimum and maximum property to be set text box
                                //wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text").SetAttribute("MinimumValue", "0");
                                //wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text").SetAttribute("MaximumValue", "7");
                                break;
                            case "68516-4":
                                wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).SetAttribute("maxlength", "4");
                                break;
                            case "76508-1":
                                //Domain Name - Social connection and isolation
                                //Question 2 - In a typical week, how many times do you talk on the telephone with family, friends, or neighbors? 
                                //Unit - {#}/wk - as discussed the min accepted value will be zero and max value can be 999 i.e. 3 digits
                                wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).SetAttribute("maxlength", "3");
                                break;
                            case "76509-9":
                                //Domain Name - Social connection and isolation
                                //Question 3 - How often do you get together with friends or relatives?  
                                //Unit - /wk - as discussed the min accepted value will be zero and max value can be 999 i.e. 3 digits
                                wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).SetAttribute("maxlength", "3");
                                break;
                            case "76510-7":
                                //Domain Name - Social connection and isolation
                                //Question 4 - How often do you attend church or religious services? 
                                //Unit - /a - as discussed the min accepted value will be zero and max value can be 999 i.e. 3 digits
                                wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).SetAttribute("maxlength", "3");
                                break;
                        }

                    }
                    dvSBPDataQue.RowFilter = "";

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                dvSBPDataQue.RowFilter = "";
            }
        }
        public void FillFormData(long PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(DynamicFormData.AppConnectionString);
                oDB.Connect(false);

                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nPatientId", PatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_FillSBPQuestionnariedata", oDBParameters, out SBPTransactionData);

                oDB.Disconnect();

                if (SBPTransactionData.Tables[0].Rows.Count > 0)
                {
                    isDataModified = true;
                }
                else
                {
                    isDataModified = false;
                }

                DataView dv = new DataView(SBPTransactionData.Tables[0]);
                foreach (DataRowView rowView in dv)
                {
                    DataRow row = rowView.Row;
                    switch (row["sInputType"].ToString())
                    {
                        case "ComboBox":
                            if (row["RecordedAnswer"].ToString() == "ASKU")
                            {
                                if (wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_chk_" + Convert.ToInt16(row["nDomainSeqno"]).ToString()) != null)
                                {
                                    wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_chk_" + row["nDomainSeqno"].ToString()).SetAttribute("checked", "True");
                                }
                                wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).SetAttribute("value", "");
                                wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).Enabled=false;
                            }
                            else
                            {
                                wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).SetAttribute("value", row["RecordedAnswer"].ToString());
                            }
                            break;
                        case "TextBox":
                            if(row["RecordedAnswer"].ToString() == "ASKU")
                            {
                                if (wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_chk_" + row["nDomainSeqno"].ToString()) != null)
                                {
                                wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_chk_" + row["nDomainSeqno"].ToString()).SetAttribute("checked", "True");
                                }
                                wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).SetAttribute("Value", "");
                                wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Enabled =false;

                            }
                            else
                            {
                            wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).SetAttribute("Value", row["RecordedAnswer"].ToString());
                            }
                            break;
                        case "Score":
                            if (row["RecordedAnswer"].ToString() != "")
                            {
                                wbBrowserCtrl.Document.GetElementById(row["QuestionLoincCode"].ToString() + "_Score").SetAttribute("innerHTML", row["RecordedAnswer"].ToString() + " Score");
                            }

                            break;
                    }
                }





            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDB != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (SBPTransactionData != null)
                {
                    SBPData.Dispose();
                    SBPData = null;
                }
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDB != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }


        }


        private void DataValidate()
        {

            try
            {
                var element = "";
                string loincquestion;
                string[] domainno;
                int totalScore = 0;
                int totaldata = 0;

                switch (wbBrowserCtrl.Document.ActiveElement.TagName)
                {
                    case "INPUT"://Checkbox, Text box


                        //decline to specify is selected
                        if (wbBrowserCtrl.Document.ActiveElement.Name == "checkbox")
                        {

                            string DomParentChkbxId = wbBrowserCtrl.Document.ActiveElement.Id;

                            string[] DomParentSeqNo = DomParentChkbxId.Split('_');


                            dvSBPDataQue.RowFilter = "nDomainSeqNo=" + DomParentSeqNo[2];
                            dvSBPDataQue.Sort = "nQuestionSeqNo";

                            foreach (DataRowView rowView in dvSBPDataQue)
                            {

                                DataRow row = rowView.Row;

                                switch (row["sInputType"].ToString())
                                {

                                    case "ComboBox":
                                        if (wbBrowserCtrl.Document.GetElementById(wbBrowserCtrl.Document.ActiveElement.Id).GetAttribute("checked") == "True")
                                        {
                                            wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).Enabled = false;
                                            wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).SetAttribute("value", ""); 
                                        }
                                        else
                                        {
                                            wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).Enabled = true;
                                        }
                                        break;
                                    case "TextBox":
                                        if (wbBrowserCtrl.Document.GetElementById(wbBrowserCtrl.Document.ActiveElement.Id).GetAttribute("checked") == "True")
                                        {
                                            wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Enabled = false;
                                            wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).SetAttribute("Value","") ;
                                        }
                                        else
                                        {
                                            wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Enabled = true;
                                        }

                                        break;
                                    case "Score":
                                        if (wbBrowserCtrl.Document.GetElementById(wbBrowserCtrl.Document.ActiveElement.Id).GetAttribute("checked") == "True")
                                        {
                                            wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_Score").SetAttribute("innerHTML", "");
                                        }
                                        break;
                                }
                            }

                            dvSBPDataQue.RowFilter = "";//clear previously set row filter value
                        }
                        else
                        {
                            loincquestion = wbBrowserCtrl.Document.ActiveElement.Id;
                            domainno = loincquestion.Split('_');

                            DataView question = new DataView(SBPData.Tables[0]);
                            question.RowFilter = "nDomainSeqNo=" + domainno[2];
                            question.Sort = "nQuestionSeqNo";

                            foreach (DataRowView rowView in question)
                            {
                                DataRow row = rowView.Row;
                                if (row["sProperty"].ToString() == "Score")
                                {
                                    wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_Score").SetAttribute("innerHTML", totalScore.ToString() + " Score");
                                }
                                if (row["sInputType"].ToString() == "TextBox")
                                {
                                    element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                    if (row["sLOINCCode"].ToString() == "76508-1")
                                    {
                                        if (element != "")
                                        {
                                            totaldata = totaldata + Convert.ToInt16(element);
                                        }
                                    }
                                    if (row["sLOINCCode"].ToString() == "76509-9")
                                    {
                                        if (element != "")
                                        {
                                            totaldata = totaldata + Convert.ToInt16(element);
                                        }
                                        if (totaldata >= 3)
                                        {
                                            totalScore = totalScore + 1;
                                        }


                                    }

                                    if (row["sLOINCCode"].ToString() == "76510-7")
                                    {
                                        if (element != "")
                                        {
                                            if (Convert.ToInt16(element) >= 4)
                                            {
                                                totalScore = totalScore + 1;
                                            }
                                        }



                                    }
                                }
                                if (row["sInputType"].ToString() == "ComboBox")
                                {
                                    element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                    if (element != "")
                                    {
                                        string[] datastr = element.Split('_');
                                        if (datastr[1] != "")
                                        {
                                            totalScore = totalScore + Convert.ToInt16(datastr[1]);
                                        }
                                    }

                                    if (row["sLOINCCode"].ToString() == "63503-7")
                                    {


                                        if (element != "")
                                        {
                                            string[] datastr = element.Split('_');
                                            if (datastr[0] == "LA48-4" || datastr[0] == "LA15605-1")
                                            {
                                                totalScore = totalScore + 1;
                                            }
                                        }

                                    }

                                }
                            }
                        }


                        break;
                    case "SELECT"://Combo box

                        loincquestion = wbBrowserCtrl.Document.ActiveElement.Name;
                        domainno = loincquestion.Split('_');


                        dvSBPDataQue.RowFilter = "nDomainSeqNo=" + domainno[2];
                        dvSBPDataQue.Sort = "nQuestionSeqNo";


                        foreach (DataRowView rowView in dvSBPDataQue)
                        {
                            DataRow row = rowView.Row;
                            if (row["sProperty"].ToString() == "Score")
                            {
                                wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_Score").SetAttribute("innerHTML", totalScore.ToString() + " Score");
                            }
                            if (row["sInputType"].ToString() == "ComboBox")
                            {
                                element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                if (element != "")
                                {
                                    string[] datastr = element.Split('_');
                                    if (datastr[1] != "")
                                    {
                                        totalScore = totalScore + Convert.ToInt16(datastr[1]);
                                    }
                                }

                                if (row["sLOINCCode"].ToString() == "63503-7")
                                {
                                    element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                    if (element != "")
                                    {
                                        string[] datastr = element.Split('_');
                                        if (datastr[0] == "LA48-4" || datastr[0] == "LA15605-1")
                                        {
                                            totalScore = totalScore + 1;
                                        }
                                    }

                                }
                            }
                            if (row["sInputType"].ToString() == "TextBox")
                            {
                                element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                if (row["sLOINCCode"].ToString() == "76508-1")
                                {
                                    if (element != "")
                                    {
                                        totaldata = totaldata + Convert.ToInt16(element);
                                    }
                                }
                                if (row["sLOINCCode"].ToString() == "76509-9")
                                {
                                    if (element != "")
                                    {
                                        totaldata = totaldata + Convert.ToInt16(element);
                                    }
                                    if (totaldata >= 3)
                                    {
                                        totalScore = totalScore + 1;
                                    }
                                }
                                if (row["sLOINCCode"].ToString() == "76510-7")
                                {
                                    if (element != "")
                                    {
                                        if (Convert.ToInt16(element) >= 4)
                                        {
                                            totalScore = totalScore + 1;
                                        }
                                    }
                                }

                            }
                        }


                        dvSBPDataQue.RowFilter = "";//clear previously set row filter value

                        break;
                    case "BUTTON":

                        #region "Submit button click - commented code"
                        //MessageBox.Show("BUTTON");
                        //if (wbBrowserCtrl.Document.ActiveElement.Id == "submit")
                        //{
                        //    bool IsValidAns = ValidateRecordedAnswer();
                        //    if (IsValidAns == false)
                        //    {
                        //        break;
                        //    }

                        //    int maxVal = 0;
                        //    maxVal = Convert.ToInt32(SBPData.Tables[0].Compute("MAX([nDomainSeqno])", ""));
                        //    QuestionAnswer[] queans = new QuestionAnswer[24];
                        //    int cnt = 0;
                        //    QuestionAnswer qa = new QuestionAnswer();
                        //    DataTable _myDataTable = new DataTable();
                        //    _myDataTable.Columns.Add(new DataColumn("sQuestionLoincCode"));
                        //    _myDataTable.Columns.Add(new DataColumn("sRecordedAnswer"));
                        //    for (int i = 1; i <= maxVal; i++)
                        //    {

                        //        dvSBPDataQue.RowFilter = "ndomainseqno=" + i; // query example = "id = 10"
                        //        dvSBPDataQue.Sort = " nQuestionSeqNo";
                        //        int groupid = 0;
                        //        Boolean IsDecline = false;
                        //        foreach (DataRowView rowView in dvSBPDataQue)
                        //        {
                        //            //var element1 = "";
                        //            DataRow row = rowView.Row;
                        //            if (groupid == 0)
                        //            {
                        //                IsDecline = false;
                        //                element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_chk_" + row["nDomainSeqno"].ToString()).GetAttribute("checked");
                        //                if (element == "True")
                        //                {
                        //                    IsDecline = true;
                        //                }

                        //            }
                        //            groupid = 1;

                        //            if (row["sInputType"].ToString() == "ComboBox")
                        //            {

                        //                element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                        //                DataRow r = _myDataTable.NewRow();
                        //                r[0] = row["sLOINCCode"].ToString();
                        //                r[1] = element;
                        //                if (IsDecline == true)
                        //                {
                        //                    r[1] = "ASKU";
                        //                }
                        //                else
                        //                {
                        //                    r[1] = element;
                        //                }

                        //                _myDataTable.Rows.Add(r);

                        //            }

                        //            if (row["sInputType"].ToString() == "TextBox")
                        //            {
                        //                element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                        //                DataRow r = _myDataTable.NewRow();
                        //                r[0] = row["sLOINCCode"].ToString();
                        //                if (IsDecline == true)
                        //                {
                        //                    r[1] = "ASKU";
                        //                }
                        //                else
                        //                {
                        //                    r[1] = element;
                        //                }
                        //                _myDataTable.Rows.Add(r);

                        //            }

                        //            if (row["sInputType"].ToString() == "Score")
                        //            {
                        //                element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_Score").GetAttribute("innerhtml");
                        //                DataRow r = _myDataTable.NewRow();
                        //                r[0] = row["sLOINCCode"].ToString();


                        //                qa = new QuestionAnswer();
                        //                qa.sQuestionLoincCode = row["sLOINCCode"].ToString();
                        //                if (element != "")
                        //                {
                        //                    string[] datastr = element.Split(' ');
                        //                    // qa.sRecordedAnswer = datastr[0];
                        //                    r[1] = datastr[0];
                        //                }
                        //                else
                        //                {
                        //                    //qa.sRecordedAnswer = "";
                        //                    r[1] = "";
                        //                }
                        //                //queans[cnt] = qa;
                        //                //cnt = cnt + 1;
                        //                _myDataTable.Rows.Add(r);
                        //            }

                        //        }

                        //        dvSBPDataQue.RowFilter = "";//clear previously set row filter value
                        //    }



                        //    oDB = new gloDatabaseLayer.DBLayer(DynamicFormData.AppConnectionString);

                        //    oDB.Connect(false);
                        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                        //    //gloDatabaseLayer.

                        //    oDBParameters.Add("@nPatientId", DynamicFormData.nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                        //    //visit id generation logic handled in stored procedure itself
                        //    //oDBParameters.Add("@nVisitId", DynamicFormData.nVisitId, ParameterDirection.Input, SqlDbType.BigInt);
                        //    oDBParameters.Add("@nExamId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        //    oDBParameters.Add("@nPatientProviderId", DynamicFormData.nPatientProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                        //    oDBParameters.Add("@nLoginsessionId", DynamicFormData.nLoginsessionId, ParameterDirection.Input, SqlDbType.BigInt);
                        //    oDBParameters.Add("@tvpsbptransaction", _myDataTable, ParameterDirection.Input, SqlDbType.Structured);

                        //    int nRetval = oDB.Execute("SBP_Transaction_TVP", oDBParameters);

                        //    oDBParameters.Clear();

                        //    if (nRetval > 0)
                        //    {


                        //        if (isDataModified == true)
                        //        {
                        //            DynamicFormData.nAuditId = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityCategory.SavedSocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityType.Open, "Modified Social Psychological Behavioral observations form", DynamicFormData.nPatientId, 0, DynamicFormData.nPatientProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                        //        }
                        //        else
                        //        {
                        //            DynamicFormData.nAuditId = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityCategory.SavedSocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityType.Open, "Saved Social Psychological Behavioral observations form", DynamicFormData.nPatientId, 0, DynamicFormData.nPatientProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                        //        }


                        //        oDBParameters.Add("@nPatientId", DynamicFormData.nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                        //        oDBParameters.Add("@nAuditId", DynamicFormData.nAuditId, ParameterDirection.Input, SqlDbType.BigInt);


                        //        int nVal = oDB.Execute("SBP_Audit", oDBParameters);
                        //        oDBParameters.Clear();
                        //        oDB.Disconnect();

                        //        if (nVal > 0)
                        //        {
                        //            MessageBox.Show("Patient social psychological and behavioral information saved successfully ", "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        }
                        //        else
                        //        {
                        //            //already written message in exception
                        //        }


                        //    }
                        //    else
                        //    {
                        //        //already written message in exception
                        //    }



                        //}
                        #endregion
                        
                        break;


                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                
                
            }
            finally
            {
                
            }
        }


        void Document_Click(object sender, HtmlElementEventArgs e)
         {
             DataValidate();
        }

        private void wbBrowserCtrl_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                //this function will set max, min and boundary values to text box controls. This will be only called once when the form is loaded.
                SetMaxLengthAttributes();

                FillFormData(DynamicFormData.nPatientId);

                wbBrowserCtrl.Document.Click += new HtmlElementEventHandler(Document_Click);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }

        }


        /// <summary>
        /// since for text box we cannot set min, max value hence boundary values cannot be set.
        /// therefore we will validate the text box answer when user hits Submit button
        /// hence when we only text box are tthis function will validate 
        /// </summary>
        public bool ValidateRecordedAnswer()
        {
            //StringBuilder strbldrValidationMessage = null;
            try
            {

                //strbldrValidationMessage = new StringBuilder();
                if (dvSBPDataQue != null)
                {
                    string txtValue;
                    Int16 nAns;
                    bool IsCntrlEnabled = true;//variable will hold value if control is disabled due to decline to specify setting

                    dvSBPDataQue.RowFilter = "sInputType like" + "'" + "TextBox" + "'";
                    dvSBPDataQue.Sort = "nDomainSeqNo";
                    foreach (DataRowView rowView in dvSBPDataQue)
                    {
                        txtValue = string.Empty;
                        nAns = 0;

                        DataRow row = rowView.Row;

                        switch (row["sLOINCCode"].ToString())
                        {
                            case "68515-6":
                                //Domain Name - Physical Activity
                                //Question 1 - How many days of moderate to strenuous exercise, like a brisk walk, did you do in the last 7D? 
                                //Unit - d/(7.d) OR days/7 days - therfore the min accepted value can be 0 zero and max value can be 7

                                txtValue = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                IsCntrlEnabled = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Enabled;

                                if ((IsCntrlEnabled == true) && (txtValue != ""))
                                {
                                    nAns = Convert.ToInt16(txtValue);
                                    if (nAns >= 0 && nAns <= 7)
                                    {

                                    }
                                    else
                                    {
                                        //strbldrValidationMessage.Append("Invalid value. Insert numeric value between 0 to 7 days only");
                                        MessageBox.Show("Invalid value. Insert numeric value between 0 to 7 days only", "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Focus();
                                        return false;
                                    }

                                }


                                break;
                            case "68516-4":
                                //Domain Name - Physical Activity
                                //Question 2 - On those days that you engage in moderate to strenuous exercise, how many minutes, on average, do you exercise? 
                                //Unit - min/d - therfore the min accepted value can be 0 zero minutest and max value can be 1440 i.e. 24 hours

                                txtValue = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                IsCntrlEnabled = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Enabled;

                                if ((IsCntrlEnabled == true) && (txtValue != ""))
                                {
                                    nAns = Convert.ToInt16(txtValue);
                                    if (nAns >= 0 && nAns <= 1440)
                                    {

                                    }
                                    else
                                    {
                                        //strbldrValidationMessage.Append("Invalid value. Insert numeric value between 0 to 1440 minutes only");
                                        MessageBox.Show("Invalid value. Insert numeric value between 0 to 1440 minutes only", "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Focus();
                                        return false;
                                    }
                                }


                                break;
                            case "76508-1":
                                //Domain Name - Social connection and isolation
                                //case "76508-1": 
                                //Question 2 - In a typical week, how many times do you talk on the telephone with family, friends, or neighbors? 
                                //Unit - {#}/wk - as discussed the min accepted value will be zero and max value can be 999 i.e. 3 digits

                                txtValue = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                IsCntrlEnabled = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Enabled;

                                if ((IsCntrlEnabled == true) && (txtValue != ""))
                                {
                                    nAns = Convert.ToInt16(txtValue);
                                    if (nAns >= 0 && nAns <= 999)
                                    {

                                    }
                                    else
                                    {
                                        //strbldrValidationMessage.Append("Invalid value. Insert numeric value between 0 to 1440 minutes only");
                                        MessageBox.Show("Invalid value. Insert numeric value between 0 to 999 only", "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Focus();
                                        return false;
                                    }
                                }


                                break;

                            case "76509-9":
                                //Domain Name - Social connection and isolation
                                //Question 3 - How often do you get together with friends or relatives?  
                                //Unit - /wk - as discussed the min accepted value will be zero and max value can be 999 i.e. 3 digits

                                txtValue = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                IsCntrlEnabled = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Enabled;

                                if ((IsCntrlEnabled == true) && (txtValue != ""))
                                {
                                    nAns = Convert.ToInt16(txtValue);
                                    if (nAns >= 0 && nAns <= 999)
                                    {

                                    }
                                    else
                                    {
                                        //strbldrValidationMessage.Append("Invalid value. Insert numeric value between 0 to 1440 minutes only");
                                        MessageBox.Show("Invalid value. Insert numeric value between 0 to 999 only", "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Focus();
                                        return false;
                                    }
                                }


                                break;

                            case "76510-7":
                                //Domain Name - Social connection and isolation
                                //Question 4 - How often do you attend church or religious services? 
                                //Unit - /a - as discussed the min accepted value will be zero and max value can be 999 i.e. 3 digits

                                txtValue = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                                IsCntrlEnabled = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Enabled;

                                if ((IsCntrlEnabled == true) && (txtValue != ""))
                                {
                                    nAns = Convert.ToInt16(txtValue);
                                    if (nAns >= 0 && nAns <= 999)
                                    {

                                    }
                                    else
                                    {
                                        //strbldrValidationMessage.Append("Invalid value. Insert numeric value between 0 to 1440 minutes only");
                                        MessageBox.Show("Invalid value. Insert numeric value between 0 to 999 only", "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).Focus();
                                        return false;
                                    }
                                }
                                break;


                        }

                    }

                    dvSBPDataQue.RowFilter = "";

                }
                return true;


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }

        }


        private void frmDynamicHTMLForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (SBPData != null)
                {
                    SBPData.Dispose();
                    SBPData = null;
                }
                if (dvSBPDataQue != null)
                {
                    dvSBPDataQue.Dispose();
                    dvSBPDataQue = null;
                }
                if (dvSBPDataAns != null)
                {
                    dvSBPDataAns.Dispose();
                    dvSBPDataAns = null;
                }
                if (SBPTransactionData != null)
                {
                    SBPTransactionData.Dispose();
                    SBPTransactionData = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void tlstrpBtnClose_Click(object sender, EventArgs e)
        {
            try
            {
              
                tlstrpBtnSaveClose_Click(sender, e);
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                
            }

            

        }

        private void tlstrpbtnSBPHistory_Click(object sender, EventArgs e)
        {
            try
            {
                gloGlobal.SBPQuestionnaire.frmSBPHistory ofrmSBPhx = new gloGlobal.SBPQuestionnaire.frmSBPHistory();
                gloGlobal.SBPQuestionnaire.frmSBPHxData.AppConnectionString = DynamicFormData.AppConnectionString;
                gloGlobal.SBPQuestionnaire.frmSBPHxData.nPatientId = DynamicFormData.nPatientId;
                ofrmSBPhx.ShowDialog();
                ofrmSBPhx.Dispose();
                ofrmSBPhx = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void wbBrowserCtrl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            DataValidate();
        }

        private void tlstrpBtnSaveClose_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;

            try
            {
               
                var element = "";
                bool IsValidAns = ValidateRecordedAnswer();
                if (IsValidAns == false)
                {
                    return;
                }

                int maxVal = 0;
                maxVal = Convert.ToInt32(SBPData.Tables[0].Compute("MAX([nDomainSeqno])", ""));
                QuestionAnswer[] queans = new QuestionAnswer[24];
              
                QuestionAnswer qa = new QuestionAnswer();
                DataTable _myDataTable = new DataTable();
                _myDataTable.Columns.Add(new DataColumn("sQuestionLoincCode"));
                _myDataTable.Columns.Add(new DataColumn("sRecordedAnswer"));
               Boolean Isgroupans  = true;
               Boolean Isans = false;
               Boolean IsOldans = false;
                StringBuilder str = new StringBuilder();


                for (int i = 1; i <= maxVal; i++)
                {

                    dvSBPDataQue.RowFilter = "ndomainseqno=" + i; // query example = "id = 10"
                    dvSBPDataQue.Sort = " nQuestionSeqNo";
                    int groupid = 0;
                    Boolean IsDecline = false;
                    foreach (DataRowView rowView in dvSBPDataQue)
                    {
                        //var element1 = "";
                        DataRow row = rowView.Row;
                        if (row["sProperty"].ToString() == "-")
                        {
                            continue;
                        }
                        if (groupid == 0)
                        {
                            IsDecline = false;
                            element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_chk_" + row["nDomainSeqno"].ToString()).GetAttribute("checked");
                            if (element == "True")
                            {
                                IsDecline = true;
                            }


                        }
                        groupid = 1;

                        if (row["sInputType"].ToString() == "ComboBox")
                        {

                            element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_combo_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                            DataRow r = _myDataTable.NewRow();
                            r[0] = row["sLOINCCode"].ToString();
                            r[1] = element;
                            if (IsDecline == true)
                            {
                                r[1] = "ASKU";
                                Isans = true;
                            }
                            else
                            {
                                r[1] = element;
                                if (element == "")
                                {
                                    Isgroupans = false;
                                }
                                else
                                {
                                    Isans = true;
                                }

                            }
                            if (SBPTransactionData.Tables[0].Select("QuestionLoincCode='" + row["sLOINCCode"].ToString() + "'").Length > 0)
                            {
                                DataRow foundRows;
                                foundRows = SBPTransactionData.Tables[0].Select("QuestionLoincCode='" + row["sLOINCCode"].ToString() + "'")[0];
                                if (foundRows[1].ToString() != r[1].ToString())
                                {
                                    IsOldans = true;
                                }
                            }
                            else
                            {
                                IsOldans = true;
                            }
                            _myDataTable.Rows.Add(r);

                        }

                        if (row["sInputType"].ToString() == "TextBox")
                        {
                            element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_text_" + row["nDomainSeqNo"].ToString()).GetAttribute("Value");
                            DataRow r = _myDataTable.NewRow();
                            r[0] = row["sLOINCCode"].ToString();
                            if (IsDecline == true)
                            {
                                r[1] = "ASKU";
                                Isans = true;
                            }
                            else
                            {
                                r[1] = element;
                                 if(element == "")
                                {
                                    Isgroupans=false;
                                }
                                 else
                                 {
                                     Isans = true;
                                 }
                            }

                            if (SBPTransactionData.Tables[0].Select("QuestionLoincCode='" + row["sLOINCCode"].ToString() + "'").Length > 0)
                            {
                                DataRow foundRows;
                                foundRows = SBPTransactionData.Tables[0].Select("QuestionLoincCode='" + row["sLOINCCode"].ToString() + "'")[0];
                                if (foundRows[1].ToString() != r[1].ToString())
                                {
                                    IsOldans = true;
                                }
                            }
                            else
                            {
                                IsOldans = true;
                            }
                            _myDataTable.Rows.Add(r);

                        }

                        if (row["sInputType"].ToString() == "Score")
                        {
                            element = wbBrowserCtrl.Document.GetElementById(row["sLOINCCode"].ToString() + "_Score").GetAttribute("innerhtml");
                            DataRow r = _myDataTable.NewRow();
                            r[0] = row["sLOINCCode"].ToString();


                            qa = new QuestionAnswer();
                            qa.sQuestionLoincCode = row["sLOINCCode"].ToString();
                            if (element != "")
                            {
                                string[] datastr = element.Split(' ');
                                // qa.sRecordedAnswer = datastr[0];
                                r[1] = datastr[0];
                            }
                            else
                            {
                                //qa.sRecordedAnswer = "";
                                r[1] = "";
                            }
                            //queans[cnt] = qa;
                            //cnt = cnt + 1;
                            _myDataTable.Rows.Add(r);
                        }

                    }
                    if (Isgroupans == false)
                    {
                    str.Append("- " + dvSBPDataQue[0]["sDomainName"]);
                    str.Append(Environment.NewLine);
                    Isgroupans=true;
                    }

                  

                    dvSBPDataQue.RowFilter = "";//clear previously set row filter value
                }
                if (IsOldans == false)
                {
                    StringBuilder strbldMsg = new StringBuilder();
                    strbldMsg.Append("You have not made any changes to the form, do you want to close the form without making any changes?" + Environment.NewLine);
                    strbldMsg.Append("'Yes' - Continue to make changes to form." + Environment.NewLine);
                    strbldMsg.Append("'No'  - Close the form.");
                    DialogResult res = MessageBox.Show(strbldMsg.ToString(), "QEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        strbldMsg = null;
                        return;
                    }
                    else
                    {
                        strbldMsg = null;
                        this.Close();
                        return;
                    }
                    //MessageBox.Show(, "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;
                }
                if (Isans == false)
                {
                    StringBuilder strbldMsg = new StringBuilder();
                    strbldMsg.Append("You have not made any changes to the form, do you want to close the form without making any changes?" + Environment.NewLine);
                    strbldMsg.Append("'Yes' - Continue to make changes to form." + Environment.NewLine);
                    strbldMsg.Append("'No'  - Close the form.");
                    DialogResult res = MessageBox.Show(strbldMsg.ToString(), "QEMR", MessageBoxButtons.YesNo , MessageBoxIcon.Warning);
                    if (res == DialogResult.Yes)
                    {
                        strbldMsg = null;
                        return;
                    }
                    else
                    {
                        strbldMsg = null;
                        this.Close();
                        return;
                    }
                    //MessageBox.Show("No changes in form found","QEMR",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    //return;
                }
              
                if (str.Length > 0)
                {
                    StringBuilder strbldMsg = new StringBuilder();
                    strbldMsg.Append("Answers to following domains question is not given, do you want to continue to save the form?" + Environment.NewLine);
                    strbldMsg.Append(str.ToString() + Environment.NewLine);
                    strbldMsg.Append("'Yes' - Continue to save data." + Environment.NewLine);
                    strbldMsg.Append("'No'  - Close form without saving data.");
                    DialogResult res = MessageBox.Show(strbldMsg.ToString(), "QEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        //do nothing and save data.
                        strbldMsg = null;
                    }
                    else
                    {
                        strbldMsg = null;
                        this.Close(); //Close form without saving data.
                        return;
                    }
                }
               

                //SummaryofSBP();

                oDB = new gloDatabaseLayer.DBLayer(DynamicFormData.AppConnectionString);

                oDB.Connect(false);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                //gloDatabaseLayer.

                oDBParameters.Add("@nPatientId", DynamicFormData.nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                //visit id generation logic handled in stored procedure itself
                //oDBParameters.Add("@nVisitId", DynamicFormData.nVisitId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nExamId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPatientProviderId", DynamicFormData.nPatientProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nLoginsessionId", DynamicFormData.nLoginsessionId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@tvpsbptransaction", _myDataTable, ParameterDirection.Input, SqlDbType.Structured);

                int nRetval = oDB.Execute("SBP_Transaction_TVP", oDBParameters);

                oDBParameters.Clear();

                if (nRetval > 0)
                {


                    if (isDataModified == true)
                    {
                        DynamicFormData.nAuditId = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityCategory.SavedSocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityType.Modify, "Changed Social Psychological Behavioral observations ", DynamicFormData.nPatientId, 0, DynamicFormData.nPatientProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                    }
                    else
                    {
                        DynamicFormData.nAuditId = gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityCategory.SavedSocialPsychologicalBehavioralobservations, gloAuditTrail.ActivityType.Add, "Added Social Psychological Behavioral observations", DynamicFormData.nPatientId, 0, DynamicFormData.nPatientProviderId, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                    }


                    oDBParameters.Add("@nPatientId", DynamicFormData.nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nAuditId", DynamicFormData.nAuditId, ParameterDirection.Input, SqlDbType.BigInt);


                    int nVal = oDB.Execute("SBP_Audit", oDBParameters);
                    oDBParameters.Clear();
                    oDB.Disconnect();

                    if (nVal > 0)
                    {
                        MessageBox.Show("Patient social psychological and behavioral information saved successfully ", "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //already written message in exception
                    }


                }
                else
                {
                    //already written message in exception
                }
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                MessageBox.Show("Cannot save patient social psychological and behavioral information.", "QEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

    }

 

    public static class DynamicFormData
    {
        public static string AppConnectionString { get; set; }
        public static long nPatientId { get; set; }
        public static long nVisitId { get; set; }
        public static long nPatientProviderId { get; set; }
        public static long nLoginsessionId { get; set; }
        public static long nAuditId { get; set; }
        public static string PatientProviderName { get; set; }
    }

    public class QuestionAnswer
    {
        public string sQuestionLoincCode { get; set; }
        public string sRecordedAnswer { get; set; }
    }

}
