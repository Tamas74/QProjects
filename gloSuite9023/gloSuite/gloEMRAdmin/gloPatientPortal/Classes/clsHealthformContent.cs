using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloPatientPortal.Classes;
using C1.Win.C1FlexGrid;
using System.Drawing;
using System.Windows.Forms;
using System.Web.UI;
using System.IO;
 

namespace gloPatientPortal.Classes
{
    class clsHealthformContent
    {
         string _strConnectionString = string.Empty;
         HtmlTextWriter writer = null;
         TextWriter innerTextWriter = null;
         StringWriter stringWriter = null;

         public clsHealthformContent(string strConnectionString)
        {
            _strConnectionString=strConnectionString;
        }    
       
         public string GetTab(long nPFlistID)
         {
             clsHealthForm oclsHistory = null;
             StringBuilder StrTab = new StringBuilder(16, 2147483647);
             try
             {

                 oclsHistory = new clsHealthForm();                
                 DataSet ds = oclsHistory.getHFDetailsforDiv(_strConnectionString, nPFlistID);
                
                 ds.Tables[0].TableName = "PagesGrps";               
                 string tabTitle = string.Empty;
                 int pageNo = 1;
                 StrTab.AppendLine("<ul>");
                 for (int i = 0; i < ds.Tables["PagesGrps"].Rows.Count; i++)
                 {
                     if (pageNo != Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["nPAgeNo"]))
                     {
                         StrTab.AppendLine("<li tabid='tab"+pageNo+"' style='margin-top:5px;'><a href='#tab" + pageNo + "' data-toggle='tab'  title='" + tabTitle + "'>" + pageNo + "</a></li>");
                         tabTitle = string.Empty;
                     }
                     if (!string.IsNullOrEmpty(tabTitle))
                     {
                         tabTitle = tabTitle + ", ";
                     }

                     tabTitle = tabTitle + ds.Tables["PagesGrps"].Rows[i]["sPublishName"];
                     pageNo = Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["nPAgeNo"]);

                 }
                 StrTab.AppendLine("<li tabid='tab" + pageNo + "' style='margin-top:5px;'><a href='#tab" + pageNo + "' data-toggle='tab' title='" + tabTitle + "'>" + pageNo + "</a></li>");
                 StrTab.AppendLine("</ul>");
                 return StrTab.ToString();

             }
             catch (Exception ex)
             {
                 return "";
             }
             finally
             {
               
                 if (oclsHistory != null)
                     oclsHistory = null;                
             }
         }

         public string GetTabControls(long nPFlistID)
         {
             clsHealthForm oclsHistory = null;
             stringWriter = new StringWriter();
             innerTextWriter = null;
             writer = new HtmlTextWriter(stringWriter);
             try
             {

                 

                 oclsHistory = new clsHealthForm();
                 StringBuilder StrDiv = new StringBuilder(16, 2147483647);
                 StringBuilder StrChildDiv = new StringBuilder(16, 2147483647);
                 DataSet ds = oclsHistory.getHFDetailsforDiv(_strConnectionString, nPFlistID);
                 int cntFinalCheckBox = 4;
                 int cntFinalRadioButton = 4;
                 int cntCheckBox = 0;
                 int cntRadioButton = 0;

                 ds.Tables[0].TableName = "PagesGrps";
                 ds.Tables[1].TableName = "Question";
                 ds.Tables[2].TableName = "Answers";
                 ds.Tables[3].TableName = "PageNo";
                 string tabTitle = string.Empty;
                 int pageNo = 1;

                 for (int npage = 0; npage < ds.Tables["PageNo"].Rows.Count; npage++)
                 {
                     pageNo = Convert.ToInt16(ds.Tables["PageNo"].Rows[npage]["nPageNo"]);


                    // StrDiv.AppendLine("<div class='tab-pane' id='tab" + pageNo + "'><fieldset><legend class='hide'></legend>");


                         //DIV
                         writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab-pane");
                         writer.AddAttribute(HtmlTextWriterAttribute.Id, "tab" + pageNo);
                         writer.RenderBeginTag(HtmlTextWriterTag.Div);

                         //FIELDSET and LEGEND
                         writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
                         writer.AddAttribute(HtmlTextWriterAttribute.Class, "hide");
                         writer.RenderBeginTag(HtmlTextWriterTag.Legend);                        
                         writer.RenderEndTag();


                         for (int i = 0; i < ds.Tables["PagesGrps"].Rows.Count; i++)
                         {
                             if (pageNo != Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["nPageNo"]))
                                 continue;



                            // StrDiv.AppendLine("<div class='container-fluid' controltype='1' id='div_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "' GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "' HistoryCategoryId='" + ds.Tables["PagesGrps"].Rows[i]["nHistoryCategoryId"] + "' HistoryCategory='" + ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"] + "'>
                             //<div class='row-fluid'><div class='span12'><div  style='position: relative;>");

                           //  StrDiv.AppendLine("<div class='box-header'  >" + ds.Tables["PagesGrps"].Rows[i]["sPreText"] + " " + ds.Tables["PagesGrps"].Rows[i]["sPublishName"] + " " + ds.Tables["PagesGrps"].Rows[i]["sPostText"] + "</div></div></div></div></div>");

                             Int16 cntMale = 0;
                             Int16 cntFemale = 0;
                             Int16 cntOthers = 0;        
                             Int16 cntCommon = 0;
                             
                               //All = 0,
                               //Male = 1,
                               //Female = 2,
                               //Others = 3

                             DataRow[] drQuestions = ds.Tables["Question"].Select("[GRPLibID] = " + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]));
                             Int16 cntQuestion = Convert.ToInt16(drQuestions.Length);

                             for (int nQs = 0; nQs < drQuestions.Length; nQs++)
                             {
                                 if (Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) != Convert.ToInt64(drQuestions[nQs]["GRPLibID"]))
                                     continue;

                                 if (drQuestions[nQs]["nGenderType"].ToString().ToLower() == "0")
                                 {
                                     cntCommon++;
                                 }
                                 else if (drQuestions[nQs]["nGenderType"].ToString().ToLower() == "1")
                                 {
                                     cntMale++;
                                 }
                                 else if (drQuestions[nQs]["nGenderType"].ToString().ToLower() == "2")
                                 {
                                     cntFemale++;
                                 }
                                 else if (drQuestions[nQs]["nGenderType"].ToString().ToLower() == "3")
                                 {
                                     cntOthers++;
                                 }

                             }                          


                          
                            //DIV
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "container-fluid");
                            writer.AddAttribute("controltype", "1");
                            writer.AddAttribute(HtmlTextWriterAttribute.Id, "div_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo);
                            writer.AddAttribute("GroupId", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]));
                            writer.AddAttribute("HistoryCategoryId",Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nHistoryCategoryId"]));
                            writer.AddAttribute("HistoryCategory",Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"]));
                            writer.AddAttribute("HistoryCategoryType", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sHistoryCategoryType"]));
                            writer.AddAttribute("GroupLabel", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sPublishName"]));

                            if (Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["IsDataTable"]) == 1)
                            {
                                writer.AddAttribute("datatable", "1");
                            }
                            else
                            {
                                writer.AddAttribute("datatable", "0");
                            }


                            if (cntQuestion > 0)
                            {
                                if (cntQuestion == cntMale)
                                {
                                    writer.AddAttribute("GenderType", "Male");
                                }
                                else if (cntQuestion == cntFemale)
                                {
                                    writer.AddAttribute("GenderType", "Female");
                                }
                                else if (cntQuestion == cntOthers)
                                {
                                    writer.AddAttribute("GenderType", "Other");
                                }
                                else
                                {
                                    writer.AddAttribute("GenderType", "Common");
                                }
                            }

                            writer.RenderBeginTag(HtmlTextWriterTag.Div);


                            //DIV
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "span12");
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "section-title");
                            //writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "relative");
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);

                            //DIV
                            string addRemoveText = string.Empty;
                            if (Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sPreText"]) != "")
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "13px");
                                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "Gray");
                                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                innerTextWriter = null;
                                innerTextWriter = writer.InnerWriter;
                                addRemoveText = Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sPreText"]).Replace("\r\n", "<br />");
                                innerTextWriter.Write(addRemoveText);
                                writer.RenderEndTag();
                            }

                            if (Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sPublishName"]) != "")
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "15px");
                                writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "bold");
                                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "Gray");
                                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                innerTextWriter = null;
                                innerTextWriter = writer.InnerWriter;
                                innerTextWriter.Write(ds.Tables["PagesGrps"].Rows[i]["sPublishName"]);
                                writer.RenderEndTag();
                            }

                            if (Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sPostText"]) != "")
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "13px");
                                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "Gray");
                                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                innerTextWriter = null;
                                innerTextWriter = writer.InnerWriter;
                                addRemoveText = string.Empty;
                                addRemoveText = Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sPostText"]).Replace("\r\n", "<br />");
                                innerTextWriter.Write(addRemoveText);
                                writer.RenderEndTag();
                            }

                            //writer.AddAttribute(HtmlTextWriterAttribute.Class, "main");
                            //writer.RenderBeginTag(HtmlTextWriterTag.Hr);
                            //writer.RenderEndTag();   

                            //innerTextWriter = null;                            
                            //innerTextWriter = writer.InnerWriter;
                            //innerTextWriter.Write(ds.Tables["PagesGrps"].Rows[i]["sPreText"] + " " + ds.Tables["PagesGrps"].Rows[i]["sPublishName"] + " " + ds.Tables["PagesGrps"].Rows[i]["sPostText"]);
                            
                           //CLOSE DIV
                            //writer.RenderEndTag();
                           // writer.RenderEndTag();
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                            writer.RenderEndTag(); 

                         
                             if (ds.Tables["Question"] != null && ds.Tables["Question"].Rows.Count > 0)
                             {

                                 DataRow[] drQuestion = ds.Tables["Question"].Select("[GRPLibID] = " + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]));

                                 


                                 for (int nQues = 0; nQues < drQuestion.Length; nQues++)
                                 {
                                     int enumQuestionType = Convert.ToInt16(drQuestion[nQues]["nAnswerType"]);
                                     string ItemCategory = string.Empty;
                                     int visible = 0;
                                     visible = Convert.ToInt16(drQuestion[nQues]["nGenderType"]);


                                     //StrDiv.AppendLine("<div class='container-fluid' visibleto='" + visible + "' >");
                                     
                                     //StrDiv.AppendLine("<div class='row-fluid' controltype='2'  GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + Convert.ToString(drQuestion[nQues]["nPFAssociationId"]) + "'   AnswerType='" + drQuestion[nQues]["nAnswerType"] + "' HistoryCategoryId='" + ds.Tables["PagesGrps"].Rows[i]["nHistoryCategoryId"] + "' HistoryCategory='" + ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"] + "' HistoryItemId ='" + drQuestion[nQues]["nCategoryID"] + "' HistoryItem='" + drQuestion[nQues]["sHistoryItem"] + "'   QuestionType='" + drQuestion[nQues]["nQuestionType"] + "'   >
                                     //<div class='span11'><strong>");
                                     //StrDiv.AppendLine("<span style="Color:Red"> * </span>");
                                     //StrDiv.AppendLine(Convert.ToString(drQuestion[nQues]["sPreText"]) + " " + Convert.ToString(drQuestion[nQues]["sPublishName"]) + " " + Convert.ToString(drQuestion[nQues]["sPostText"]));
                                     //StrDiv.AppendLine("</strong></div></div>");
                               



                                     //DIV
                                     writer.AddAttribute(HtmlTextWriterAttribute.Class, "container-fluid");
                                     writer.AddAttribute("visibleto", visible.ToString());
                                     writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                     

                                     //DIV
                                     writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                     writer.AddAttribute("controltype", "2");
                                     writer.AddAttribute("GroupId", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]));
                                     writer.AddAttribute("QuestionId", Convert.ToString(drQuestion[nQues]["nPFLibId"]));
                                     writer.AddAttribute("AnswerType", Convert.ToString(drQuestion[nQues]["nAnswerType"]));
                                     writer.AddAttribute("HistoryCategoryId", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nHistoryCategoryId"]));
                                     writer.AddAttribute("HistoryCategory", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"]));
                                     writer.AddAttribute("HistoryCategoryType", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sHistoryCategoryType"]));
                                     writer.AddAttribute("HistoryItemId", Convert.ToString(drQuestion[nQues]["nCategoryID"]));
                                     writer.AddAttribute("HistoryItem", Convert.ToString(drQuestion[nQues]["sHistoryItem"]));
                                     writer.AddAttribute("QuestionType", Convert.ToString(drQuestion[nQues]["nQuestionType"]));
                                     writer.AddAttribute("Mandatary", Convert.ToString(drQuestion[nQues]["bIsRequired"]));
                                     writer.AddAttribute("QuestionLabel", Convert.ToString(drQuestion[nQues]["sPublishName"]));
                                     writer.AddAttribute("SequenceNo", Convert.ToString(drQuestion[nQues]["nOrderID"]));

                                     if (Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["IsDataTable"]) == 1)
                                     {
                                         writer.AddAttribute("datatable", "1");
                                     }
                                     else
                                     {
                                         writer.AddAttribute("datatable", "0");
                                     }

                                     writer.RenderBeginTag(HtmlTextWriterTag.Div);                                 

                                     
                                     //DIV and Strong
                                     writer.AddAttribute(HtmlTextWriterAttribute.Class, "span11");
                                     writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "14px");
                                     writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                     //writer.RenderBeginTag(HtmlTextWriterTag.Strong);

                                     // For Mandatory Start
                                     if (Convert.ToBoolean(drQuestion[nQues]["bIsRequired"]) == true)
                                     {
                                         writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "Red");
                                         writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                         innerTextWriter = null;
                                         innerTextWriter = writer.InnerWriter;
                                         innerTextWriter.Write("*");
                                         writer.RenderEndTag();
                                     }
                                     // For Mandatory End

                                     innerTextWriter = null;
                                     innerTextWriter = writer.InnerWriter;
                                     string addRemovePreText = string.Empty;
                                     string addRemovePostText = string.Empty;
                                     if (Convert.ToString(drQuestion[nQues]["sPreText"]).Trim() != "" && Convert.ToString(drQuestion[nQues]["sPostText"]).Trim() != "")
                                     {
                                         addRemovePreText = Convert.ToString(drQuestion[nQues]["sPreText"]).Replace("\r\n", "<br />");
                                         addRemovePostText = Convert.ToString(drQuestion[nQues]["sPostText"]).Replace("\r\n", "<br />");
                                         innerTextWriter.Write("<span style='font-size: 13px; color: Gray;'>" + addRemovePreText + "</span> <br/>" + Convert.ToString(drQuestion[nQues]["sPublishName"]) + "<br/><span style='font-size: 13px; color: Gray;'>" + addRemovePostText + "</span> <br/><br/>");
                                     }
                                     else if (Convert.ToString(drQuestion[nQues]["sPreText"]).Trim() != "" && Convert.ToString(drQuestion[nQues]["sPostText"]).Trim() == "")
                                     {
                                         addRemovePreText = Convert.ToString(drQuestion[nQues]["sPreText"]).Replace("\r\n", "<br />");
                                         innerTextWriter.Write("<span style='font-size: 13px; color: Gray;'>" + addRemovePreText + "</span> <br/>" + Convert.ToString(drQuestion[nQues]["sPublishName"]) + "<br/><br/>");
                                     }
                                     else if (Convert.ToString(drQuestion[nQues]["sPreText"]).Trim() == "" && Convert.ToString(drQuestion[nQues]["sPostText"]).Trim() != "")
                                     {
                                         addRemovePostText = Convert.ToString(drQuestion[nQues]["sPostText"]).Replace("\r\n", "<br />");
                                         innerTextWriter.Write(Convert.ToString(drQuestion[nQues]["sPublishName"]) + "<br/><span style='font-size: 13px; color: Gray;'>" + addRemovePostText + "</span> <br/><br/>");
                                     }
                                     else if (Convert.ToString(drQuestion[nQues]["sPreText"]).Trim() == "" && Convert.ToString(drQuestion[nQues]["sPostText"]).Trim() == "")
                                     {
                                         innerTextWriter.Write(Convert.ToString(drQuestion[nQues]["sPublishName"]));
                                     }
                                     
                                     //writer.RenderEndTag();
                                     writer.RenderEndTag();
                                     writer.RenderEndTag();


                                     //==========================================TextBox==========================================================================================================
                                     if (enumQuestionType == 0)
                                     {
                                         //StrDiv.AppendLine("<div class='row-fluid'>");
                                         writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                         writer.RenderBeginTag(HtmlTextWriterTag.Div);

                                         // StrDiv.AppendLine("<div class='span11' controltype='3'   GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + drQuestion[nQues]["nPFAssociationId"] + "'  >");

                                         //DIV
                                         writer.AddAttribute(HtmlTextWriterAttribute.Class, "span11");
                                         writer.AddAttribute("controltype", "3");
                                         writer.AddAttribute("GroupId", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]));
                                         writer.AddAttribute("QuestionId", Convert.ToString(drQuestion[nQues]["nPFLibId"]));
                                         writer.RenderBeginTag(HtmlTextWriterTag.Div);


                                         //string txtID = Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"]).Replace(" ", "") + "_Q" + Convert.ToString(drQuestion[nQues]["nOrderID"]);
                                         string txtID = Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_Q" + Convert.ToString(drQuestion[nQues]["nOrderID"]);
                                         ItemCategory = Convert.ToString(drQuestion[nQues]["sHistoryItem"]);

                                         //StrDiv.AppendLine("<input maxlength='100' type='text' id='txt_" + txtID + "' ItemCategory='" + ItemCategory + "' />&nbsp;");
                                         writer.AddAttribute("maxlength", "100");
                                         writer.AddAttribute("type", "text");
                                         writer.AddAttribute("id", "txt_" + txtID);
                                         writer.AddAttribute("ItemCategory", ItemCategory);
                                         writer.RenderBeginTag(HtmlTextWriterTag.Input);
                                         writer.RenderEndTag();

                                         //StrDiv.AppendLine("</div></div>");
                                         writer.RenderEndTag();
                                         writer.RenderEndTag();
                                     }

                                     //==========================================LongTextBox (TextArea)==========================================================================================================
                                     if (enumQuestionType == 1)
                                     {
                                         //StrDiv.AppendLine("<div class='row-fluid'>");
                                         writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                         writer.RenderBeginTag(HtmlTextWriterTag.Div);

                                         //  StrDiv.AppendLine("<div class='span11' controltype='3'   GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + drQuestion[nQues]["nPFAssociationId"] + "'   >");
                                         //DIV
                                         writer.AddAttribute(HtmlTextWriterAttribute.Class, "span11");
                                         writer.AddAttribute("controltype", "3");
                                         writer.AddAttribute("GroupId", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]));
                                         writer.AddAttribute("QuestionId", Convert.ToString(drQuestion[nQues]["nPFLibId"]));
                                         writer.RenderBeginTag(HtmlTextWriterTag.Div);

                                         string txtID = Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_Q" + Convert.ToString(drQuestion[nQues]["nOrderID"]);
                                         ItemCategory = Convert.ToString(drQuestion[nQues]["sHistoryItem"]);

                                         // StrDiv.AppendLine("<textarea style='height:100px' rows='5' maxlength='500' type='text' cols='1' class='textarea' id='txt_" + txtID + "' ItemCategory='" + ItemCategory + "'></textarea>&nbsp;");
                                         writer.AddAttribute("maxlength", "500");
                                         writer.AddAttribute("type", "text");
                                         writer.AddAttribute(HtmlTextWriterAttribute.Class, "textarea");
                                         writer.AddAttribute("rows", "5");
                                         writer.AddAttribute("cols", "1");
                                         writer.AddAttribute("id", "txt_" + txtID);
                                         writer.AddAttribute("ItemCategory", ItemCategory);
                                         writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100px");
                                         writer.RenderBeginTag(HtmlTextWriterTag.Textarea);
                                         writer.RenderEndTag();

                                         // StrDiv.AppendLine("</div></div>");
                                         writer.RenderEndTag();
                                         writer.RenderEndTag();
                                     }

                                     //=========================================================================================================================================================================                                  





                                     if (ds.Tables["Answers"] != null && ds.Tables["Answers"].Rows.Count > 0)
                                     {

                                         Boolean isFollowedQuestion = false;
                                         DataRow[] drAnswers = ds.Tables["Answers"].Select("[QuesID] = " + Convert.ToInt64(drQuestion[nQues]["nPFLibId"]));
                                         for (int nAns = 0; nAns < drAnswers.Length; nAns++)
                                         {
                                             if (drAnswers[nAns]["bIsFollwedText"].ToString().ToLower() == "true")
                                             {
                                                 isFollowedQuestion = true;
                                             }
                                         }

                                         //===================================CHECKBOX==========================================================================================================================

                                         if (enumQuestionType == 2)
                                         {
                                             bool isClosed = true;

                                             cntCheckBox = 0;
                                             for (int nAns = 0; nAns < drAnswers.Length; nAns++, cntCheckBox++)
                                             {
                                                 if ((cntCheckBox == 0) || (cntCheckBox >= cntFinalCheckBox))
                                                 {
                                                     if (isClosed == false && cntCheckBox > 0)
                                                     {
                                                         //StrDiv.AppendLine("</div>");
                                                         writer.RenderEndTag();
                                                         isClosed = true;
                                                         cntCheckBox = 0;
                                                     }

                                                     //StrDiv.AppendLine("<div class='row-fluid'>");
                                                     writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                                     writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                                     isClosed = false;
                                                 }

                                                 string isFQ = "0";
                                                 if (drAnswers[nAns]["bIsFollwedText"].ToString().ToLower() == "true")
                                                 {
                                                     isFQ = "1";
                                                 }

                                                // StrDiv.AppendLine("<div class='span3' controltype='3' isFQ='" + isFQ + "'  GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + drQuestion[nQues]["nPFAssociationId"] + "'   AnswerId='" + drAnswers[nAns]["nPFAnswerID"] + "'  HistoryItemId='" + drAnswers[nAns]["nHistoryItemId"] + "' HistoryItem='" + drAnswers[nAns]["sHistoryItem"] + "' OtherId='" + drAnswers[nAns]["nOtherID"] + "'  >");
                                                 
                                                 //DIV
                                                 writer.AddAttribute(HtmlTextWriterAttribute.Class, "span3");
                                                 writer.AddAttribute("controltype", "3");
                                                 writer.AddAttribute("isFQ", isFQ.ToString());
                                                 writer.AddAttribute("GroupId", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]));
                                                 writer.AddAttribute("QuestionId", Convert.ToString(drQuestion[nQues]["nPFLibId"]));
                                                 writer.AddAttribute("AnswerId", Convert.ToString(drAnswers[nAns]["nPFAnswerID"] ));
                                                 writer.AddAttribute("HistoryItemId", Convert.ToString(drAnswers[nAns]["nHistoryItemId"]));
                                                 writer.AddAttribute("HistoryItem", Convert.ToString(drAnswers[nAns]["nHistoryItemId"]));
                                                 writer.AddAttribute("OtherId", Convert.ToString(drAnswers[nAns]["nOtherID"]));
                                                 writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "13px");
                                                 writer.RenderBeginTag(HtmlTextWriterTag.Div);
 
                                                 string answer = GetAnswer(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], drAnswers[nAns], isFollowedQuestion);
                                                // StrDiv.AppendLine(answer);
                                                // innerTextWriter = null;
                                                // innerTextWriter = writer.InnerWriter;
                                               //  innerTextWriter.Write(answer);

                                                 string followedQuestion = string.Empty;
                                                 if (isFollowedQuestion)
                                                 {
                                                     followedQuestion = GetCHKFollowedQuestion(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], drAnswers[nAns], enumQuestionType);
                                                     ///StrDiv.AppendLine(followedQuestion);
                                                     //innerTextWriter = null;
                                                    // innerTextWriter = writer.InnerWriter;
                                                    // innerTextWriter.Write(followedQuestion);
                                                 }

                                                 //StrDiv.AppendLine("</div>");
                                                 writer.RenderEndTag();

                                                 if ((cntCheckBox >= cntFinalCheckBox))
                                                 {
                                                     if (isClosed == false)
                                                     {
                                                        // StrDiv.AppendLine("</div>");
                                                         writer.RenderEndTag();
                                                         isClosed = true;
                                                         cntCheckBox = 0;
                                                     }
                                                 }
                                             }

                                             if (isClosed == false)
                                             {
                                                 //StrDiv.AppendLine("</div>");
                                                 writer.RenderEndTag();
                                             }
                                         }

                                         //======================================RadioButton=================================================================================================================
                                         if (enumQuestionType == 3)
                                         {
                                             bool isClosed = true;
                                             cntRadioButton = 0;

                                             for (int nAns = 0; nAns < drAnswers.Length; nAns++, cntRadioButton++)
                                             {

                                                 if ((cntRadioButton == 0) || (cntRadioButton >= cntFinalRadioButton))
                                                 {
                                                     if (isClosed == false && cntRadioButton > 0)
                                                     {
                                                         //StrDiv.AppendLine("</div>");
                                                         writer.RenderEndTag();
                                                         isClosed = true;
                                                         cntRadioButton = 0;
                                                     }
                                                     //StrDiv.AppendLine("<div class='row-fluid'>");
                                                     writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                                     writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                                     isClosed = false;
                                                 }

                                                 string isFQ = "0";
                                                 if (drAnswers[nAns]["bIsFollwedText"].ToString().ToLower() == "true")
                                                 {
                                                     isFQ = "1";
                                                 }

                                                // StrDiv.AppendLine("<div class='span3' controltype='3' isFQ='" + isFQ + "'  GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + drQuestion[nQues]["nPFAssociationId"] + "'   AnswerId='" + drAnswers[nAns]["nPFAnswerID"] + "'  HistoryItemId='" + drAnswers[nAns]["nHistoryItemId"] + "' HistoryItem='" + drAnswers[nAns]["sHistoryItem"] + "' OtherId='" + drAnswers[nAns]["nOtherID"] + "' >");                                                

                                                 //DIV
                                                 writer.AddAttribute(HtmlTextWriterAttribute.Class, "span3");
                                                 writer.AddAttribute("controltype", "3");
                                                 writer.AddAttribute("isFQ", isFQ.ToString());
                                                 writer.AddAttribute("GroupId", Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]));
                                                 writer.AddAttribute("QuestionId", Convert.ToString(drQuestion[nQues]["nPFLibId"]));
                                                 writer.AddAttribute("AnswerId", Convert.ToString(drAnswers[nAns]["nPFAnswerID"]));
                                                 writer.AddAttribute("HistoryItemId", Convert.ToString(drAnswers[nAns]["nHistoryItemId"]));
                                                 writer.AddAttribute("HistoryItem", Convert.ToString(drAnswers[nAns]["nHistoryItemId"]));
                                                 writer.AddAttribute("OtherId", Convert.ToString(drAnswers[nAns]["nOtherID"]));
                                                 writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "13px");
                                                 writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                                 
                                                 string answer = GetAnswer(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], drAnswers[nAns], isFollowedQuestion);
                                                // StrDiv.AppendLine(answer);
                                               //  innerTextWriter = null;
                                               //  innerTextWriter = writer.InnerWriter;
                                               //  innerTextWriter.Write(answer);

                                                // StrDiv.AppendLine("</div>");
                                                 writer.RenderEndTag();

                                                 if ((cntRadioButton >= cntFinalRadioButton))
                                                 {
                                                     if (isClosed == false)
                                                     {
                                                         //StrDiv.AppendLine("</div>");
                                                         writer.RenderEndTag();
                                                         isClosed = true;
                                                         cntRadioButton = 0;
                                                     }
                                                 }
                                             }

                                             string followedQuestion = string.Empty;

                                             if (isClosed == false)
                                             {
                                                 //StrDiv.AppendLine("</div>");
                                                 writer.RenderEndTag();
                                             }

                                             if (isFollowedQuestion)
                                             {
                                                 followedQuestion = GetRBTFollowedQuestion(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], null, enumQuestionType);
                                                 //StrDiv.AppendLine(followedQuestion);
                                                // innerTextWriter = null;
                                               //  innerTextWriter = writer.InnerWriter;
                                               //  innerTextWriter.Write(followedQuestion);
                                             }
                                         }
                                         //==========================================DropDown==========================================================================================================
                                         if (enumQuestionType == 4)
                                         {
                                             // StrDiv.AppendLine("<div class='row-fluid'>");
                                             writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                             writer.RenderBeginTag(HtmlTextWriterTag.Div);

                                             //StrDiv.AppendLine("<div class='span11' >");
                                             writer.AddAttribute(HtmlTextWriterAttribute.Class, "span11");
                                             writer.RenderBeginTag(HtmlTextWriterTag.Div);

                                             string answer2 = GetddlAnswer(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], drAnswers, isFollowedQuestion);
                                             //StrDiv.AppendLine(answer2);
                                           //  innerTextWriter = null;
                                          //   innerTextWriter = writer.InnerWriter;
                                          //   innerTextWriter.Write(answer2);

                                             //StrDiv.AppendLine("</div></div>");
                                             writer.RenderEndTag();
                                             writer.RenderEndTag();

                                             if (isFollowedQuestion)
                                             {
                                                 string followedQuestion = GetRBTFollowedQuestion(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], null, enumQuestionType);
                                                 //StrDiv.AppendLine(followedQuestion);
                                               // innerTextWriter = null;
                                              //  innerTextWriter = writer.InnerWriter;
                                               //  innerTextWriter.Write(followedQuestion);
                                             }
                                         }
                                        

                                     }
                                    




                                     //StrDiv.AppendLine("</div>");
                                     writer.RenderEndTag();
                                 }

                             }

                             //// Check IsDataTable Or NOT
                             if (Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["IsDataTable"]) == 1)
                             {
                                 //    StrDiv.AppendLine("<div class='container-fluid'>");
                                 writer.AddAttribute(HtmlTextWriterAttribute.Class, "container-fluid");
                                 if (cntQuestion > 0)
                                 {
                                     if (cntQuestion == cntMale)
                                     {
                                         writer.AddAttribute("GenderType", "Male");
                                     }
                                     else if (cntQuestion == cntFemale)
                                     {
                                         writer.AddAttribute("GenderType", "Female");
                                     }
                                     else if (cntQuestion == cntOthers)
                                     {
                                         writer.AddAttribute("GenderType", "Other");
                                     }
                                     else
                                     {
                                         writer.AddAttribute("GenderType", "Common");
                                     }
                                 }
                                 writer.RenderBeginTag(HtmlTextWriterTag.Div);

                                 //    StrDiv.AppendLine("<div class='row-fluid' class='span11'>");
                                 writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                 writer.AddAttribute(HtmlTextWriterAttribute.Class, "span11");

                              

                                 writer.RenderBeginTag(HtmlTextWriterTag.Div);

                                 //    StrDiv.AppendLine("<input  type='button'  align='right'  value='Save " + ds.Tables["PagesGrps"].Rows[i]["sPublishName"] + "'  onclick=\"savedatatable('dt_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "');\"  id='btn_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "' ></input>&nbsp;");
                                 writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
                                 writer.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-small");
                                 writer.AddAttribute(HtmlTextWriterAttribute.Align, "right");
                                 writer.AddAttribute(HtmlTextWriterAttribute.Value, "Add " + ds.Tables["PagesGrps"].Rows[i]["sPublishName"]);
                                 string onClick = "savedatatable('dt_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "',true);";
                                 writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick, false);
                                 writer.AddAttribute(HtmlTextWriterAttribute.Id, "btn_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo);
                                 writer.RenderBeginTag(HtmlTextWriterTag.Input);
                                 writer.RenderEndTag();


                                 //writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
                                 //writer.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-small");
                                 //writer.AddAttribute(HtmlTextWriterAttribute.Align, "right");
                                 //writer.AddAttribute(HtmlTextWriterAttribute.Value, "Reset");
                                 //onClick = string.Empty;
                                 //onClick = "resetcontrols('dt_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "',true);";
                                 //writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick, false);
                                 //writer.AddAttribute(HtmlTextWriterAttribute.Id, "btnr_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo);
                                 //writer.RenderBeginTag(HtmlTextWriterTag.Input);
                                 //writer.RenderEndTag();

                                 //    StrDiv.AppendLine("</div></div>");
                                 writer.RenderEndTag();
                                 writer.RenderEndTag();



                             }



                             //// Check IsDataTable Or NOT
                             if (Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["IsDataTable"]) == 1)
                             {
                             //    StrDiv.AppendLine("<div class='container-fluid' parentid='div_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "'  id='dt_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "' datatable='1' >");
                                 writer.AddAttribute(HtmlTextWriterAttribute.Class, "container-fluid");
                                 writer.AddAttribute("parentid","div_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo );
                                 writer.AddAttribute(HtmlTextWriterAttribute.Id, "dt_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo);
                                 writer.AddAttribute("datatable", "1" );
                                 writer.RenderBeginTag(HtmlTextWriterTag.Div);                                         

                                 
                                 //    StrDiv.AppendLine("<div class='row-fluid'>");
                                 writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                                 writer.RenderBeginTag(HtmlTextWriterTag.Div);   

                             //    StrDiv.AppendLine("</div></div>");
                                 writer.RenderEndTag();
                                 writer.RenderEndTag();


                             }


                             //<div class='row-fluid'><span class='span12'></span></div>
                             //writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                             //writer.RenderBeginTag(HtmlTextWriterTag.Div);
                             //writer.AddAttribute(HtmlTextWriterAttribute.Class, "span12");
                             //writer.RenderBeginTag(HtmlTextWriterTag.Span);
                             //writer.RenderEndTag();
                             //writer.RenderEndTag();

                             //<hr class='main' />
                             //writer.AddAttribute(HtmlTextWriterAttribute.Class, "main");
                             //writer.RenderBeginTag(HtmlTextWriterTag.Hr);
                             //writer.RenderEndTag();

                             //<div class='row-fluid'><span class='span12'></span></div>
                             //writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
                             //writer.RenderBeginTag(HtmlTextWriterTag.Div);
                             //writer.AddAttribute(HtmlTextWriterAttribute.Class, "span12");
                             //writer.RenderBeginTag(HtmlTextWriterTag.Span);
                             //writer.RenderEndTag();
                             //writer.RenderEndTag(); 
                            
                           
                         }

                         //StrDiv.AppendLine("</fieldset></div>");
                         writer.RenderEndTag();
                         writer.RenderEndTag();   

                      

                       

                 }

                 return stringWriter.ToString();

             }
             catch (Exception ex)
             {
                 return "";
             }
             finally
             {
                 if (oclsHistory != null)
                     oclsHistory = null;
             }
         }


         //public string GetTabControls(long nPFlistID)
         //{
         //    clsHealthForm oclsHistory = null;



         //    try
         //    {



         //        oclsHistory = new clsHealthForm();
         //        StringBuilder StrDiv = new StringBuilder(16, 2147483647);
         //        StringBuilder StrChildDiv = new StringBuilder(16, 2147483647);
         //        DataSet ds = oclsHistory.getHFDetailsforDiv(_strConnectionString, nPFlistID);
         //        int cntFinalCheckBox = 4;
         //        int cntFinalRadioButton = 4;
         //        int cntCheckBox = 0;
         //        int cntRadioButton = 0;

         //        ds.Tables[0].TableName = "PagesGrps";
         //        ds.Tables[1].TableName = "Question";
         //        ds.Tables[2].TableName = "Answers";
         //        ds.Tables[3].TableName = "PageNo";
         //        string tabTitle = string.Empty;
         //        int pageNo = 1;

         //        for (int npage = 0; npage < ds.Tables["PageNo"].Rows.Count; npage++)
         //        {
         //            pageNo = Convert.ToInt16(ds.Tables["PageNo"].Rows[npage]["nPageNo"]);
         //            StrDiv.AppendLine("<div class='tab-pane' id='tab" + pageNo + "'><fieldset><legend class='hide'></legend>");

         //            for (int i = 0; i < ds.Tables["PagesGrps"].Rows.Count; i++)
         //            {
         //                if (pageNo != Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["nPageNo"]))
         //                    continue;

                        
         //                StrDiv.AppendLine("<div class='container-fluid' controltype='1' id='div_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "' GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "' HistoryCategoryId='" + ds.Tables["PagesGrps"].Rows[i]["nHistoryCategoryId"] + "' HistoryCategory='" + ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"] + "'><div class='row-fluid'><div class='span12'><div  style='position: relative;>");

         //                StrDiv.AppendLine("<div class='box-header'  >" + ds.Tables["PagesGrps"].Rows[i]["sPreText"] + " " + ds.Tables["PagesGrps"].Rows[i]["sPublishName"] + " " + ds.Tables["PagesGrps"].Rows[i]["sPostText"] + "</div></div></div></div></div>");










         //                if (ds.Tables["Question"] != null && ds.Tables["Question"].Rows.Count > 0)
         //                {












         //                    DataRow[] drQuestion = ds.Tables["Question"].Select("[GRPLibID] = " + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]));
         //                    for (int nQues = 0; nQues < drQuestion.Length; nQues++)
         //                    {



         //                        int enumQuestionType = Convert.ToInt16(drQuestion[nQues]["nAnswerType"]);
         //                        string ItemCategory = string.Empty;





         //                        int visible = 0;
         //                        visible = Convert.ToInt16(drQuestion[nQues]["nGenderType"]);



                                
         //                        StrDiv.AppendLine("<div class='container-fluid' visibleto='" + visible + "' >");                                 
         //                        StrDiv.AppendLine("<div class='row-fluid' controltype='2'  GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + Convert.ToString(drQuestion[nQues]["nPFAssociationId"]) + "'   AnswerType='" + drQuestion[nQues]["nAnswerType"] + "' HistoryCategoryId='" + ds.Tables["PagesGrps"].Rows[i]["nHistoryCategoryId"] + "' HistoryCategory='" + ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"] + "' HistoryItemId ='" + drQuestion[nQues]["nCategoryID"] + "' HistoryItem='" + drQuestion[nQues]["sHistoryItem"] + "'   QuestionType='" + drQuestion[nQues]["nQuestionType"] + "'   ><div class='span11'><strong>");
         //                        StrDiv.AppendLine(Convert.ToString(drQuestion[nQues]["sPreText"]) + " " + Convert.ToString(drQuestion[nQues]["sPublishName"]) + " " + Convert.ToString(drQuestion[nQues]["sPostText"]));
         //                        StrDiv.AppendLine("</strong></div></div>");
                               
         //                        if (ds.Tables["Answers"] != null && ds.Tables["Answers"].Rows.Count > 0)
         //                        {

         //                            Boolean isFollowedQuestion = false;
         //                            DataRow[] drAnswers = ds.Tables["Answers"].Select("[QuesID] = " + Convert.ToInt64(drQuestion[nQues]["nPFLibId"]));
         //                            for (int nAns = 0; nAns < drAnswers.Length; nAns++)
         //                            {
         //                                if (drAnswers[nAns]["bIsFollwedText"].ToString().ToLower() == "true")
         //                                {
         //                                    isFollowedQuestion = true;



         //                                }
         //                            }

         //                            //===================================CHECKBOX==========================================================================================================================

         //                            if (enumQuestionType == 2)
         //                            {
         //                                bool isClosed = true;

         //                                cntCheckBox = 0;
         //                                for (int nAns = 0; nAns < drAnswers.Length; nAns++, cntCheckBox++)
         //                                {
         //                                    if ((cntCheckBox == 0) || (cntCheckBox >= cntFinalCheckBox))
         //                                    {
         //                                        if (isClosed == false && cntCheckBox > 0)
         //                                        {
         //                                            StrDiv.AppendLine("</div>");
         //                                            isClosed = true;
         //                                            cntCheckBox = 0;




         //                                        }
         //                                        StrDiv.AppendLine("<div class='row-fluid'>");
         //                                        isClosed = false;
         //                                    }

         //                                    string isFQ = "0";
         //                                    if (drAnswers[nAns]["bIsFollwedText"].ToString().ToLower() == "true")
         //                                    {
         //                                        isFQ = "1";

         //                                    }


         //                                    StrDiv.AppendLine("<div class='span3' controltype='3' isFQ='" + isFQ + "'  GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + drQuestion[nQues]["nPFAssociationId"] + "'   AnswerId='" + drAnswers[nAns]["nPFAnswerID"] + "'  HistoryItemId='" + drAnswers[nAns]["nHistoryItemId"] + "' HistoryItem='" + drAnswers[nAns]["sHistoryItem"] + "' OtherId='" + drAnswers[nAns]["nOtherID"] + "'  >");
         //                                    string answer = GetAnswer(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], drAnswers[nAns], isFollowedQuestion);
         //                                    StrDiv.AppendLine(answer);



         //                                    string followedQuestion = string.Empty;
         //                                    if (isFollowedQuestion)
         //                                    {
         //                                        followedQuestion = GetCHKFollowedQuestion(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], drAnswers[nAns], enumQuestionType);
         //                                        StrDiv.AppendLine(followedQuestion);
         //                                    }

         //                                    StrDiv.AppendLine("</div>");














         //                                    if ((cntCheckBox >= cntFinalCheckBox))
         //                                    {
         //                                        if (isClosed == false)
         //                                        {
         //                                            StrDiv.AppendLine("</div>");
         //                                            isClosed = true;
         //                                            cntCheckBox = 0;
         //                                        }
         //                                    }
         //                                }

         //                                if (isClosed == false)
         //                                {
         //                                    StrDiv.AppendLine("</div>");
         //                                }
         //                            }

         //                            //======================================RadioButton=================================================================================================================
         //                            if (enumQuestionType == 3)
         //                            {
         //                                bool isClosed = true;
         //                                cntRadioButton = 0;













         //                                for (int nAns = 0; nAns < drAnswers.Length; nAns++, cntRadioButton++)
         //                                {

         //                                    if ((cntRadioButton == 0) || (cntRadioButton >= cntFinalRadioButton))
         //                                    {
         //                                        if (isClosed == false && cntRadioButton > 0)
         //                                        {
         //                                            StrDiv.AppendLine("</div>");
         //                                            isClosed = true;
         //                                            cntRadioButton = 0;




         //                                        }
         //                                        StrDiv.AppendLine("<div class='row-fluid'>");
         //                                        isClosed = false;
         //                                    }

         //                                    string isFQ = "0";
         //                                    if (drAnswers[nAns]["bIsFollwedText"].ToString().ToLower() == "true")
         //                                    {
         //                                        isFQ = "1";










         //                                    }








         //                                    StrDiv.AppendLine("<div class='span3' controltype='3' isFQ='" + isFQ + "'  GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + drQuestion[nQues]["nPFAssociationId"] + "'   AnswerId='" + drAnswers[nAns]["nPFAnswerID"] + "'  HistoryItemId='" + drAnswers[nAns]["nHistoryItemId"] + "' HistoryItem='" + drAnswers[nAns]["sHistoryItem"] + "' OtherId='" + drAnswers[nAns]["nOtherID"] + "' >");
         //                                    string answer = GetAnswer(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], drAnswers[nAns], isFollowedQuestion);
         //                                    StrDiv.AppendLine(answer);







         //                                    StrDiv.AppendLine("</div>");



         //                                    if ((cntRadioButton >= cntFinalRadioButton))
         //                                    {
         //                                        if (isClosed == false)
         //                                        {
         //                                            StrDiv.AppendLine("</div>");
         //                                            isClosed = true;
         //                                            cntRadioButton = 0;
         //                                        }
         //                                    }
         //                                }





















         //                                string followedQuestion = string.Empty;






         //                                if (isClosed == false)
         //                                {
         //                                    StrDiv.AppendLine("</div>");
         //                                }

         //                                if (isFollowedQuestion)
         //                                {
         //                                    followedQuestion = GetRBTFollowedQuestion(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], null, enumQuestionType);
         //                                    StrDiv.AppendLine(followedQuestion);
         //                                }
         //                            }
         //                            //==========================================DropDown==========================================================================================================
         //                            if (enumQuestionType == 4)
         //                            {
         //                                StrDiv.AppendLine("<div class='row-fluid'>");
         //                                StrDiv.AppendLine("<div class='span11' >");
         //                                string answer2 = GetddlAnswer(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], drAnswers, isFollowedQuestion);
         //                                StrDiv.AppendLine(answer2);

         //                                StrDiv.AppendLine("</div></div>");







         //                                if (isFollowedQuestion)
         //                                {
         //                                    string followedQuestion = GetRBTFollowedQuestion(ds.Tables["PagesGrps"].Rows[i], drQuestion[nQues], null, enumQuestionType);
         //                                    StrDiv.AppendLine(followedQuestion);
















         //                                }
         //                            }
         //                            //==========================================TextBox==========================================================================================================
         //                            if (enumQuestionType == 0)
         //                            {
         //                                StrDiv.AppendLine("<div class='row-fluid'>");
         //                                StrDiv.AppendLine("<div class='span11' controltype='3'   GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + drQuestion[nQues]["nPFAssociationId"] + "'  >");
         //                                string txtID = Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"]).Replace(" ", "") + "_Q" + Convert.ToString(drQuestion[nQues]["nOrderID"]);
         //                                string strName = Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"]).Replace(" ", "") + "_Q" + Convert.ToString(drQuestion[nQues]["nOrderID"]);
         //                                ItemCategory = Convert.ToString(drQuestion[nQues]["sHistoryItem"]);

         //                                StrDiv.AppendLine("<input maxlength='100' type='text' id='txt_" + txtID + "' ItemCategory='" + ItemCategory + "' />&nbsp;");

         //                                StrDiv.AppendLine("</div></div>");
         //                            }

         //                            //==========================================LongTextBox (TextArea)==========================================================================================================
         //                            if (enumQuestionType == 1)
         //                            {
         //                                StrDiv.AppendLine("<div class='row-fluid'>");
         //                                StrDiv.AppendLine("<div class='span11' controltype='3'   GroupId='" + ds.Tables["PagesGrps"].Rows[i]["nPFAssociationId"] + "'   QuestionId='" + drQuestion[nQues]["nPFAssociationId"] + "'   >");
         //                                string txtID = Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"]).Replace(" ", "") + "_Q" + Convert.ToString(drQuestion[nQues]["nOrderID"]);
         //                                string strName = Convert.ToString(ds.Tables["PagesGrps"].Rows[i]["sHistoryCategory"]).Replace(" ", "") + "_Q" + Convert.ToString(drQuestion[nQues]["nOrderID"]);
         //                                ItemCategory = Convert.ToString(drQuestion[nQues]["sHistoryItem"]);

         //                                StrDiv.AppendLine("<textarea style='height:100px' rows='5' maxlength='500' type='text' cols='1' class='textarea' id='txt_" + txtID + "' ItemCategory='" + ItemCategory + "'></textarea>&nbsp;");


         //                                StrDiv.AppendLine("</div></div>");

         //                            }

         //                            //=========================================================================================================================================================================                                  


         //                        }
         //                        // Check IsDataTable Or NOT
         //                        if (Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["IsDataTable"]) == 1)
         //                        {
         //                            StrDiv.AppendLine("<div class='container-fluid'>");
         //                            StrDiv.AppendLine("<div class='row-fluid' class='span11'>");

         //                            StrDiv.AppendLine("<input  type='button'  align='right'  value='Save " + ds.Tables["PagesGrps"].Rows[i]["sPublishName"] + "'  onclick=\"savedatatable('dt_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "');\"  id='btn_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "' ></input>&nbsp;");
         //                            StrDiv.AppendLine("</div></div>");

         //                        }

         //                        StrDiv.AppendLine("</div>");
         //                    }

         //                }





         //                // Check IsDataTable Or NOT
         //                if (Convert.ToInt16(ds.Tables["PagesGrps"].Rows[i]["IsDataTable"]) == 1)
         //                {
         //                    StrDiv.AppendLine("<div class='container-fluid' parentid='div_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "'  id='dt_" + Convert.ToInt64(ds.Tables["PagesGrps"].Rows[i]["nPFLibId"]) + "_" + pageNo + "' datatable='1' >");
         //                    StrDiv.AppendLine("<div class='row-fluid'>");

         //                    StrDiv.AppendLine("</div></div>");


         //                }



         //                StrDiv.AppendLine("</fieldset></div>");
         //            }
         //        }


                 
         //        return StrDiv.ToString();

         //    }
         //    catch (Exception ex)
         //    {
         //        return "";
         //    }
         //    finally
         //    {
         //        if (oclsHistory != null)
         //            oclsHistory = null;
         //    }
         //}





     

         private string GetAnswer(DataRow drGroup, DataRow drQuestion, DataRow drAnswer, bool isFollowedQuestion)
         {
             StringBuilder StrTD = new StringBuilder(16, 2147483647);
             string ID1 = string.Empty;
             string fqID1 = string.Empty;
             string ID2 = string.Empty;
             string fqID2 = string.Empty;
             string ItemCategory = string.Empty;
           
             string onClick = string.Empty;

             ItemCategory = Convert.ToString(drQuestion["sHistoryItem"]);


             switch (Convert.ToInt16(drQuestion["nAnswerType"]))
             {
                 //===================================CHECKBOX=======================================================================================================================
                 case (Int16)clsHealthForm.QuestionType.Checkbox:

                     if (drAnswer != null)
                     {
                         ID1 = Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]) + "_A" + Convert.ToString(drAnswer["nOrderID"]);
                         fqID1 = Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]) + "_A" + Convert.ToString(drAnswer["nOrderID"]);
                         if (drAnswer["bIsFollwedText"].ToString().ToLower() == "true")
                         {

                             //onClick = "onclick=\"showchkfq('" + "chk_" + ID1 + "','" + "div_" + ID1 + "');\"";
                             onClick = "showchkfq('" + "chk_" + ID1 + "','" + "div_" + ID1 + "');";

                         }
                         else
                         {
                             //onClick = "onclick=\"showchkfq('" + "chk_" + ID1 + "','');\"";
                             onClick = "showchkfq('" + "chk_" + ID1 + "','');";
                         }

                        // StrTD.AppendLine("<label><input type='checkbox' id='chk_" + ID1 + "' ItemCategory='" + ItemCategory + "' " + onClick + "  AnswerLabel='" + Convert.ToString(drAnswer["sAnsLabel"]) + "' />&nbsp;" + Convert.ToString(drAnswer["sAnsLabel"]) + "&nbsp;</label>");

                         //Label and Input
                        
                        StringWriter    stringWritertemp = new StringWriter();
                        HtmlTextWriter writertemp = null;
                        writertemp = new HtmlTextWriter(stringWritertemp);

                           writertemp.AddAttribute("type", "checkbox");
                           writertemp.AddAttribute("id", "chk_" + ID1);
                           writertemp.AddAttribute("ItemCategory", ItemCategory);
                           writertemp.AddAttribute("AnswerLabel", Convert.ToString(drAnswer["sAnsLabel"]));
                           writertemp.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick, false);
                           writertemp.RenderBeginTag(HtmlTextWriterTag.Input);
                           writertemp.RenderEndTag();

                           writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "13px");
                         writer.RenderBeginTag(HtmlTextWriterTag.Label);
                         innerTextWriter = null;
                         innerTextWriter = writer.InnerWriter;
                         innerTextWriter.Write(stringWritertemp.ToString() + " " + Convert.ToString(drAnswer["sAnsLabel"] + " "));
                         writer.RenderEndTag(); 
                        
                     }

                     break;
                 //===========================RADIO===================================================================================================================================

                 case (Int16)clsHealthForm.QuestionType.Radio:
                     if (drAnswer != null)
                     {
                         ID1 = Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]) + "_A" + Convert.ToString(drAnswer["nOrderID"]);
                         fqID1 = Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]);

                         if (isFollowedQuestion)
                         {
                             if (drAnswer["bIsFollwedText"].ToString().ToLower() == "true")
                             {
                                 //onClick = "onclick=\"showrbtfq('" + "rbt_" + ID1 + "','" + "div_" + fqID1 + "','" + "fq_" + fqID1 + "','" + Convert.ToString(drAnswer["sFollowedTextLabel"]) + " &nbsp;');\"";
                                 onClick = "showrbtfq('" + "rbt_" + ID1 + "','" + "div_" + fqID1 + "','" + "fq_" + fqID1 + "','" + Convert.ToString(drAnswer["sFollowedTextLabel"]) + " &nbsp;');";
                             }
                             else
                             {
                                 //onClick = "onclick=\"showrbtfq('" + "rbt_" + ID1 + "','" + "div_" + fqID1 + "','','');\"";
                                 onClick = "showrbtfq('" + "rbt_" + ID1 + "','" + "div_" + fqID1 + "','','');";
                             }
                         }

                       //  StrTD.AppendLine("<label><input type='radio' id='rbt_" + ID1 + "' name='" + fqID1 + "'  ItemCategory='" + ItemCategory + "' " + onClick + " AnswerLabel='" + Convert.ToString(drAnswer["sAnsLabel"]) + "' />&nbsp;" + Convert.ToString(drAnswer["sAnsLabel"]) + "&nbsp;</label>");

                         //Label and Input

                         StringWriter stringWritertemp = new StringWriter();
                         HtmlTextWriter writertemp = null;
                         writertemp = new HtmlTextWriter(stringWritertemp);

                         writertemp.AddAttribute("type", "radio");
                         writertemp.AddAttribute("id", "rbt_" + ID1);
                         writertemp.AddAttribute("name", fqID1);
                         writertemp.AddAttribute("ItemCategory", ItemCategory);
                         writertemp.AddAttribute("AnswerLabel", Convert.ToString(drAnswer["sAnsLabel"]));
                         writertemp.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick, false);
                         writertemp.RenderBeginTag(HtmlTextWriterTag.Input);
                         writertemp.RenderEndTag();

                         writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "13px");
                         writer.RenderBeginTag(HtmlTextWriterTag.Label);
                         innerTextWriter = null;
                         innerTextWriter = writer.InnerWriter;
                         innerTextWriter.Write(stringWritertemp.ToString() + " " + Convert.ToString(drAnswer["sAnsLabel"]) + "  ");
                         writer.RenderEndTag();

                    

                     }
                     break;
             }

             return StrTD.ToString();
         }

         private string GetddlAnswer(DataRow drGroup, DataRow drQuestion, DataRow[] drAnswer, bool isFollowedQuestion)
         {
             StringBuilder StrTD = new StringBuilder(16, 2147483647);
             string dropDownID = string.Empty;
             string fqID = string.Empty;
             string optionID = string.Empty;
             string ItemCategory = string.Empty;
             string strValue = string.Empty;
             string onChange = string.Empty;
             string isFQ = "0";

             dropDownID = Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]);
             ItemCategory = Convert.ToString(drQuestion["sHistoryItem"]);

             if (isFollowedQuestion)
             {
                 //onChange = "onchange=\"showDropDown('" + "ddl_" + dropDownID + "');\"";
                 onChange = "showDropDown('" + "ddl_" + dropDownID + "');";
             }

            // StrTD.AppendLine("<select class='dd11' name='' id='ddl_" + dropDownID + "' ItemCategory='" + ItemCategory + "' " + onChange + " >");

             //SELECT

             writer.AddAttribute("class", "dd11");
             writer.AddAttribute("id", "ddl_" + dropDownID);
             writer.AddAttribute("ItemCategory", ItemCategory);
             writer.AddAttribute(HtmlTextWriterAttribute.Onchange, onChange,false);
             writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "13px");
             writer.RenderBeginTag(HtmlTextWriterTag.Select);
             
           

             int j = 0;

             for (int i = 0; i < drAnswer.Length; i++)
             {
                 optionID = Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]);
                 fqID = Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]);
                 isFQ = "0";

                 if (j == 0)
                 {
                     strValue = 0 + "|||" + optionID + "|||" + fqID + "|||";
                     //StrTD.AppendLine("<option value='" + strValue + "' selected> &nbsp;--Select--&nbsp; </option>");                                        
                    
                     writer.AddAttribute(HtmlTextWriterAttribute.Selected, "selected");
                     writer.AddAttribute(HtmlTextWriterAttribute.Value, strValue);                   
                     writer.RenderBeginTag(HtmlTextWriterTag.Option);
                     innerTextWriter = null;
                     innerTextWriter = writer.InnerWriter;
                     innerTextWriter.Write("--Select--");
                     writer.RenderEndTag();

                     j = 1;
                 }

                     

                 if (drAnswer[i]["bIsFollwedText"].ToString().ToLower() == "true")
                 {
                     isFQ = "1";
                 }
                 strValue = isFQ + "|||" + optionID + "|||" + fqID + "|||" + Convert.ToString(drAnswer[i]["sFollowedTextLabel"]) + "|||" + Convert.ToString(drAnswer[i]["nPFAnswerID"]);
                // StrTD.AppendLine("<option value='" + strValue + "' controltype='3'  isFQ='" + isFQ + "'  GroupId='" + drGroup["nPFAssociationId"] + "'    QuestionId='" + drQuestion["nPFAssociationId"] + "'   AnswerId='" + drAnswer[i]["nPFAnswerID"] + "'  HistoryItemId='" + drAnswer[i]["nHistoryItemId"] + "' HistoryItem='" + drAnswer[i]["sHistoryItem"] + "' OtherId='" + drAnswer[i]["nOtherID"] + "'   AnswerLabel='" + Convert.ToString(drAnswer[i]["sAnsLabel"]) + "' > &nbsp;" + Convert.ToString(drAnswer[i]["sAnsLabel"]) + "&nbsp; </option>");
                 
                 //Option
                 writer.AddAttribute("controltype", "3");
                 writer.AddAttribute("isFQ", isFQ);
                 writer.AddAttribute("GroupId", Convert.ToString(drGroup["nPFLibId"]));
                 writer.AddAttribute("QuestionId", Convert.ToString(drQuestion["nPFLibId"]));
                 writer.AddAttribute("AnswerId",  Convert.ToString(drAnswer[i]["nPFAnswerID"]));
                 writer.AddAttribute("HistoryItemId",  Convert.ToString(drAnswer[i]["nHistoryItemId"]));
                 writer.AddAttribute("HistoryItem",  Convert.ToString(drAnswer[i]["sHistoryItem"]));
                 writer.AddAttribute("OtherId",  Convert.ToString(drAnswer[i]["nOtherID"]));
                 writer.AddAttribute("AnswerLabel", Convert.ToString(drAnswer[i]["sAnsLabel"]));
                 writer.AddAttribute(HtmlTextWriterAttribute.Value, strValue);
                 writer.RenderBeginTag(HtmlTextWriterTag.Option);
                 innerTextWriter = null;
                 innerTextWriter = writer.InnerWriter;
                 innerTextWriter.Write(Convert.ToString(drAnswer[i]["sAnsLabel"]));

                 writer.RenderEndTag();

             }
             //StrTD.AppendLine("</select>");
             writer.RenderEndTag();

             return StrTD.ToString();
         }

         private string GetRBTFollowedQuestion(DataRow drGroup, DataRow drQuestion, DataRow drAnswer, int enumQuestionType)
         {
             StringBuilder StrTr = new StringBuilder(16, 2147483647);
             string ID = string.Empty;
             string fqID = string.Empty;
             string ItemCategory = string.Empty;
             string txtID =

             ID = "div_" + Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]);
             fqID = "fq_" + Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]);
             txtID = "txt_" + Convert.ToString(drGroup["nPFLibId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]);

             ItemCategory = Convert.ToString(drQuestion["sHistoryItem"]);

             
            // StrTr.AppendLine("<div id='" + ID + "' class='row-fluid'  style='display: none;'>");
             writer.AddAttribute(HtmlTextWriterAttribute.Class, "row-fluid");
             writer.AddAttribute(HtmlTextWriterAttribute.Id, ID);
             writer.AddStyleAttribute(HtmlTextWriterStyle.Display,"none" );
             writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // StrTr.AppendLine("<div class='span3' controltype='4' GroupId='" + drGroup["nPFAssociationId"] + "'    QuestionId='" + drQuestion["nPFAssociationId"] + "'> ");
             writer.AddAttribute(HtmlTextWriterAttribute.Class, "span3");
             writer.AddAttribute("controltype", "4");
             writer.AddAttribute("GroupId", Convert.ToString(drGroup["nPFLibId"]));
             writer.AddAttribute("QuestionId", Convert.ToString(drQuestion["nPFLibId"]));
             writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "13px");
             writer.RenderBeginTag(HtmlTextWriterTag.Div);

             //StrTr.AppendLine("<label id= '" + fqID + "'></label>");
             writer.AddAttribute(HtmlTextWriterAttribute.Id, fqID);
             writer.RenderBeginTag(HtmlTextWriterTag.Label);
             writer.RenderEndTag();

             //StrTr.AppendLine("<input type='text' maxlength='100' id='" + txtID + "' ItemCategory='" + ItemCategory + "' class='span11' />");
             writer.AddAttribute(HtmlTextWriterAttribute.Class, "span11");
             writer.AddAttribute(HtmlTextWriterAttribute.Id, txtID);
             writer.AddAttribute("maxlength", "100");
             writer.AddAttribute("type", "text");
             writer.AddAttribute("ItemCategory", ItemCategory);
             writer.RenderBeginTag(HtmlTextWriterTag.Input);
             writer.RenderEndTag();

             //StrTr.AppendLine("</div></div>");
             writer.RenderEndTag();
             writer.RenderEndTag();





             return StrTr.ToString();

         }

         private string GetCHKFollowedQuestion(DataRow drGroup, DataRow drQuestion, DataRow drAnswer, int enumQuestionType)
         {
             StringBuilder StrTr = new StringBuilder(16, 2147483647);
             string ID1 = string.Empty;
             string fqID1 = string.Empty;
             string ItemCategory = string.Empty;
             string txtID = string.Empty;
             string divID = string.Empty;

             bool isFollowedQuestion = false;

             if (drAnswer["bIsFollwedText"].ToString().ToLower() == "true")
             {
                 isFollowedQuestion = true;
             }

             if (drAnswer != null)
             {
                 ID1 = Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]) + "_A" + Convert.ToString(drAnswer["nOrderID"]);
                 fqID1 = Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]) + "_A" + Convert.ToString(drAnswer["nOrderID"]);
                 txtID = "txt_" + Convert.ToString(drGroup["nPFLibId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]) + "_A" + Convert.ToString(drAnswer["nOrderID"]);
                 divID = "div_" + Convert.ToString(drQuestion["nPFAssociationId"]) + "_Q" + Convert.ToString(drQuestion["nOrderID"]) + "_A" + Convert.ToString(drAnswer["nOrderID"]);
             }
             ItemCategory = Convert.ToString(drQuestion["sHistoryItem"]);

             if (isFollowedQuestion)
             {
                 //StrTr.AppendLine("<div id='" + divID + "' controltype='4'  GroupId='" + drGroup["nPFAssociationId"] + "'    QuestionId='" + drQuestion["nPFAssociationId"] + "'   AnswerId='" + drAnswer["nPFAnswerID"] + "' style='display: none;'>");
                 writer.AddAttribute(HtmlTextWriterAttribute.Id, divID);
                 writer.AddAttribute("controltype", "4");
                 writer.AddAttribute("GroupId", Convert.ToString(drGroup["nPFLibId"]));
                 writer.AddAttribute("QuestionId", Convert.ToString(drQuestion["nPFLibId"]));
                 writer.AddAttribute("AnswerId", Convert.ToString(drAnswer["nPFAnswerID"]));
                 writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
                 writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "13px");
                 writer.RenderBeginTag(HtmlTextWriterTag.Div);
                 
                 //StrTr.AppendLine("<label>" + Convert.ToString(drAnswer["sFollowedTextLabel"]) + "</label>");
                 writer.RenderBeginTag(HtmlTextWriterTag.Label);
                 innerTextWriter = null;
                 innerTextWriter = writer.InnerWriter;
                 innerTextWriter.Write(Convert.ToString(drAnswer["sFollowedTextLabel"]));                
                 writer.RenderEndTag();

                 //StrTr.AppendLine("<input type='text' maxlength='100' id='" + txtID + "' ItemCategory='" + ItemCategory + "' class='span11' />");
                 writer.AddAttribute(HtmlTextWriterAttribute.Class, "span11");
                 writer.AddAttribute(HtmlTextWriterAttribute.Id, txtID);
                 writer.AddAttribute("maxlength", "100");
                 writer.AddAttribute("type", "text");
                 writer.AddAttribute("ItemCategory", ItemCategory);
                 writer.RenderBeginTag(HtmlTextWriterTag.Input);
                 writer.RenderEndTag();

                 //StrTr.AppendLine("</div>");
                 writer.RenderEndTag();
             }
             return StrTr.ToString();
         }

         
    }  
}
