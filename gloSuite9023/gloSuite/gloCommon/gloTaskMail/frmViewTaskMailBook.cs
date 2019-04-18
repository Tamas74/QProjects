using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloTaskMail
{
    public partial class frmViewTaskMailBook : Form
    {
        public delegate void gloCommunityHandler();   //added delegate for calling gloCommunityViewDataform for Taskmail Download.
        public event gloCommunityHandler EvntgloCommunityHandler; //added event for calling gloCommunityViewDataform for Taskmail Download.

        #region "Declarations"

        private string _databaseconnectionstring = "";
        //private DataView _dv;
        public Int16 SelectedView;
        //private string _messageBoxCaption = "gloPM";

        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;

        private Int64 _UserId = 0;
        private Int64 _ClinicID = 0;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        //Added By Pramod Nair For UserRights 20090720
        private string _UserName = "";

        private static frmViewTaskMailBook frm;
        private bool blnDisposed;

        #endregion "Declarations"

        #region " Properties "

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        #endregion " Properties "

        #region " Constructor "

        private frmViewTaskMailBook()
        {
            InitializeComponent();

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

           


            #region "Retreive UserId & UserName "

            //Added By Pramod Nair For UserRights 20090720

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserId = Convert.ToInt64(appSettings["UserID"]); }
                else { _UserId = 0; }
            }
            else
            { _UserId = 0; }

            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }
            


            #endregion


            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

        }

        #endregion " Constructor "

        public static frmViewTaskMailBook GetInstance()
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmViewTaskMailBook();
                }
            }
            finally
            {

            }
            return frm;
        }



        #region " Form Load event "
        private void frmViewTaskMailBook_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1FlexCategory, false);

            Fill_TaskMailBook();
            //Fill_CategoryTypes();
            lblSearch.Text = "Follow Up :";

            #region "Code Start added by kanchan on 20120102 for gloCommunity integration"
            ts_gloCommunityDownload.Visible = false;
            if (_messageBoxCaption.ToLower() != "gloPM".ToLower())
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                oDB.Connect(false);
                string strQueryRight = "SELECT  Rights_MST.sRightsName AS RightsName FROM UserRights_DTL INNER JOIN Rights_MST ON UserRights_DTL.nRightsID = Rights_MST.nRightsID INNER JOIN User_MST ON UserRights_DTL.nUserID = User_MST.nUserID WHERE User_MST.sLoginName = '" + _UserName + "' AND Rights_MST.sRightsName = 'Share' AND ISNULL(ApplicationType, 0) = 0";
                if (Convert.ToString(oDB.ExecuteScalar_Query(strQueryRight)) != string.Empty)//Added condition to fixed Bug # : 37984 on 20120928
                {
                    string strQuery = "Select sSettingsValue as SettingsValue from Settings where sSettingsName='gloCommunity Feature' ";
                    Boolean Result = false;
                    Boolean.TryParse(Convert.ToString(oDB.ExecuteScalar_Query(strQuery)), out Result);
                    ts_gloCommunityDownload.Visible = Result;
                }
                oDB.Disconnect();
            }
            #endregion "Code end added by kanchan on 20120102 for gloCommunity integration"

        }

        private void frmViewTaskMailBook_Shown(object sender, EventArgs e)
        {
            Fill_FollowUpflex("");
            AssignUserRights();
        }
        #endregion

        #region " Private Methods "

        private void Fill_TaskMailBook()
        {
            TreeNode oNode;
            trvTaskMailBook.Nodes.Clear();

            //oNode = new TreeNode();
            //oNode.Text = "Category Type";
            //oNode.Tag = 1;
            //oNode.ImageIndex = 0;
            //oNode.SelectedImageIndex = 0;
            //trvTaskMailBook.Nodes.Add(oNode);
            //// Default Select the "Resource Type"
            //trvTaskMailBook.SelectedNode = oNode;

            oNode = new TreeNode();
            oNode.Text = "Follow Up";
            oNode.Tag = 2;
            oNode.ImageIndex = 1;
            oNode.SelectedImageIndex = 1;
            trvTaskMailBook.Nodes.Add(oNode);
            trvTaskMailBook.SelectedNode = oNode;

            oNode = new TreeNode();
            oNode.Text = "Status Type";
            oNode.Tag = 3;
            oNode.ImageIndex = 2;
            oNode.SelectedImageIndex = 2;
            trvTaskMailBook.Nodes.Add(oNode);

            oNode = new TreeNode();
            oNode.Text = "Priority Type";
            oNode.Tag = 4;
            oNode.ImageIndex = 3;
            oNode.SelectedImageIndex = 3;
            trvTaskMailBook.Nodes.Add(oNode);


            oNode = new TreeNode();
            oNode.Text = "Signature";
            oNode.Tag = 5;
            oNode.ImageIndex = 4;
            oNode.SelectedImageIndex = 4;
            trvTaskMailBook.Nodes.Add(oNode);


            //oNode = new TreeNode();
            //oNode.Text = "Task";
            //oNode.Tag = 6;
            //oNode.ImageIndex = 5;
            //oNode.SelectedImageIndex = 5;
            //trvTaskMailBook.Nodes.Add(oNode);


        }


        //Added By Pramod Nair For UserRights 20090720
        private void AssignUserRights()
        {
            gloUserRights.ClsgloUserRights oClsgloUserRights = null;
            try
            {
                

                if (_UserName.Trim() != "")
                {
                    oClsgloUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);
                    oClsgloUserRights.CheckForUserRights(_UserName);

                    if (trvTaskMailBook.Nodes.Count > 0)
                    {

                        for (int j = trvTaskMailBook.Nodes.Count - 1; j >= 0; j--)
                        {

                            if (trvTaskMailBook.Nodes[j].Text == "Follow Up")
                            {
                                if (!oClsgloUserRights.Followup)
                                    trvTaskMailBook.Nodes[j].Remove();

                            }
                            else if (trvTaskMailBook.Nodes[j].Text == "Status Type")
                            {
                                if (!oClsgloUserRights.StatusType)
                                    trvTaskMailBook.Nodes[j].Remove();

                            }
                            else if (trvTaskMailBook.Nodes[j].Text == "Priority Type")
                            {
                                if (!oClsgloUserRights.PriorityType)
                                    trvTaskMailBook.Nodes[j].Remove();

                            }
                            else if (trvTaskMailBook.Nodes[j].Text == "Signature")
                            {
                                if (!oClsgloUserRights.Signature)
                                    trvTaskMailBook.Nodes[j].Remove();

                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                try
                {
                    if (oClsgloUserRights != null)
                    {
                        oClsgloUserRights.Dispose();
                        oClsgloUserRights = null;
                    }
                }
                catch
                {
                }
            }
        }


        public void Fill_CategoryTypes()
        {

            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Categories oCategories = new gloTasksMails.Common.Categories();
            try
            {
                oCategories = oTaskMail.GetCategories();


                //nCategoryID,sDescription,sColorCode
                c1FlexCategory.Clear(ClearFlags.All);
                c1FlexCategory.Cols.Count = 5;
                c1FlexCategory.Rows.Count = 1;
                c1FlexCategory.Cols[0].Caption = "nCategoryID";
                c1FlexCategory.Cols[1].Caption = "Description Type";
                //c1FlexCategory.Cols[2].Caption = "sColorCode";
                //c1FlexCategory.Cols[3].Caption = "IsSystem";
                //c1FlexCategory.Cols[3].Name = "IsSystem";
                c1FlexCategory.Cols[2].Caption = "Record Type";
                c1FlexCategory.Cols[2].Name = "Record Type";
                c1FlexCategory.Cols[3].Caption = "Color Code";
                c1FlexCategory.Cols[4].Caption = "Record Type";



                c1FlexCategory.Cols[0].Visible = false;
                c1FlexCategory.Cols[1].Visible = true;
                c1FlexCategory.Cols[2].Visible = true;
                c1FlexCategory.Cols[3].Visible = true;

                int nWidth = c1FlexCategory.Width;
                c1FlexCategory.Cols[0].Width = 0;
                c1FlexCategory.Cols[1].Width = (int)(0.5 * (nWidth) - 10);
                c1FlexCategory.Cols[2].Width = (int)(0.2 * (nWidth) - 10);
                c1FlexCategory.Cols[3].Width = (int)(0.3 * (nWidth) - 10);

                c1FlexCategory.Cols[1].Width = 514;
                c1FlexCategory.Cols[2].Width = 199;
                c1FlexCategory.Cols[3].Width = 304;

                c1FlexCategory.DrawMode = DrawModeEnum.Normal;
                c1FlexCategory.AllowEditing = false;

                c1FlexCategory.BringToFront();
                c1FlexCategory.Cols[2].Width = 0;
                c1FlexCategory.Cols[2].Visible = false;
                if (oCategories != null && oCategories.Count > 0)
                {
                    int i;
                    for (i = 0; i <= oCategories.Count - 1; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = c1FlexCategory.Rows.Add();

                        //nCategoryID,sDescription,sColorCode
                        c1FlexCategory.SetData(NewRow.Index, 0, oCategories[i].ID);
                        c1FlexCategory.SetData(NewRow.Index, 1, oCategories[i].Description);
                        //c1FlexCategory.SetData(i + 1, 2, oCategories[i].ColorCode);
                        //c1FlexCategory.SetData(i + 1, 3, oCategories[i].IsSystem);
                        c1FlexCategory.SetData(NewRow.Index, 3, oCategories[i].ColorCode);
                        if (oCategories[i].IsSystem == true)
                        {
                            c1FlexCategory.SetData(NewRow.Index, 2, "System");
                            c1FlexCategory.SetData(NewRow.Index, 4, "System");
                        }
                        else
                        {
                            c1FlexCategory.SetData(NewRow.Index, 2, "User");
                            c1FlexCategory.SetData(NewRow.Index, 4, "User");
                        }

                        C1.Win.C1FlexGrid.CellStyle cStyle;
                        //c1FlexCategory.Cols[2].UserData =oCategories[i].ColorCode;
                        //c1FlexCategory.SetData(i + 1, 2, "     ");
                        c1FlexCategory.Cols[3].UserData = oCategories[i].ColorCode;
                        c1FlexCategory.SetData(NewRow.Index, 3, "     ");


                        //C1.Win.C1FlexGrid.CellRange rgBubbleValues = c1FlexCategory.GetCellRange(i + 1, 2);
                        //cStyle = c1FlexCategory.Styles.Add("BubbleValues" + i);
                        //cStyle.BackColor = Color.FromArgb(oCategories[i].ColorCode);  // Color.Blue;

                        C1.Win.C1FlexGrid.CellRange rgBubbleValues = c1FlexCategory.GetCellRange(NewRow.Index, 3);
                        //cStyle = c1FlexCategory.Styles.Add("BubbleValues" + i);
                        try
                        {
                            if (c1FlexCategory.Styles.Contains("BubbleValues" + i))
                            {
                                cStyle = c1FlexCategory.Styles["BubbleValues" + i];
                            }
                            else
                            {
                                cStyle = c1FlexCategory.Styles.Add("BubbleValues" + i);

                            }

                        }
                        catch
                        {
                            cStyle = c1FlexCategory.Styles.Add("BubbleValues" + i);



                        }
                        cStyle.BackColor = Color.FromArgb(oCategories[i].ColorCode);  // Color.Blue;

                        rgBubbleValues.Style = cStyle;
                    }
                }
                c1FlexCategory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;

                if (c1FlexCategory.Rows.Count > 1)
                {

                    tsb_Modify.Enabled = true;
                    tsb_Delete.Enabled = true;
                }
                if (c1FlexCategory.Rows.Count <= 1)
                {
                    tsb_Modify.Enabled = false;
                    tsb_Delete.Enabled = false;
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oTaskMail.Dispose();
                oCategories.Dispose();
            }
        }

        public void Fill_PriorityTypesflex(String sFilter)
        {

            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Priorities oPriorities = new gloTasksMails.Common.Priorities();

            try
            {
                oPriorities = oTaskMail.GetPriorities(sFilter);

                //nCategoryID,sDescription,sColorCode

                c1FlexCategory.Clear(ClearFlags.All);
                c1FlexCategory.Cols.Count = 3;
                c1FlexCategory.Rows.Count = 1;

                c1FlexCategory.Cols[0].Caption = "nPriorityID";
                c1FlexCategory.Cols[1].Caption = "Priority Type";
                c1FlexCategory.Cols[2].Caption = "Record Type";
                c1FlexCategory.Cols[2].Name = "Record Type";


                c1FlexCategory.Cols[0].Visible = false;
                c1FlexCategory.Cols[1].Visible = true;
                c1FlexCategory.Cols[2].Visible = true;


                int nWidth = c1FlexCategory.Width;
                c1FlexCategory.Cols[0].Width = 0;
                c1FlexCategory.Cols[1].Width = (int)(0.5 * (nWidth) - 10);
                c1FlexCategory.Cols[2].Width = (int)(0.2 * (nWidth) - 10);

                c1FlexCategory.DrawMode = DrawModeEnum.Normal;
                c1FlexCategory.AllowEditing = false;

                c1FlexCategory.BringToFront();
                if (oPriorities != null && oPriorities.Count > 0)
                {
                    int i;
                    for (i = 0; i <= oPriorities.Count - 1; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = c1FlexCategory.Rows.Add();
                        //nCategoryID,sDescription,sColorCode
                        c1FlexCategory.SetData(NewRow.Index, 0, oPriorities[i].ID);
                        c1FlexCategory.SetData(NewRow.Index, 1, oPriorities[i].Description);
                        if (oPriorities[i].IsSystem == true)
                        {
                            c1FlexCategory.SetData(NewRow.Index, 2, "System");
                        }
                        else
                        {
                            c1FlexCategory.SetData(NewRow.Index, 2, "User");
                        }

                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oTaskMail.Dispose();
                oPriorities.Dispose();
            }
        }

        public void Fill_FollowUpflex(String sFilter)
        {

            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Followups oFollowUps = new gloTasksMails.Common.Followups();

            try
            {
                oFollowUps = oTaskMail.GetFollowUps(sFilter);
                //if (oFollowUps != null && oFollowUps.Count > 0)
                //{


                //dbo.TM_FollowUp_MST - ,nFollowUpID,sDescription

                c1FlexCategory.Clear(ClearFlags.All);
                c1FlexCategory.Cols.Count = 3;
                c1FlexCategory.Rows.Count = 1;

                c1FlexCategory.Cols[0].Caption = "nFollowUpID";
                c1FlexCategory.Cols[1].Caption = "Follow Up Type";
                c1FlexCategory.Cols[2].Caption = "Record Type";
                c1FlexCategory.Cols[2].Name = "Record Type";


                c1FlexCategory.Cols[0].Visible = false;
                c1FlexCategory.Cols[1].Visible = true;
                c1FlexCategory.Cols[2].Visible = true;

                int nWidth = c1FlexCategory.Width;
                c1FlexCategory.Cols[0].Width = 0;
                c1FlexCategory.Cols[1].Width = (int)(0.5 * (nWidth) - 10);
                c1FlexCategory.Cols[2].Width = (int)(0.2 * (nWidth) - 10);

                c1FlexCategory.DrawMode = DrawModeEnum.Normal;
                c1FlexCategory.AllowEditing = false;
                c1FlexCategory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

                c1FlexCategory.BringToFront();
                if (oFollowUps != null && oFollowUps.Count > 0)
                {
                    int i;
                    for (i = 0; i <= oFollowUps.Count - 1; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = c1FlexCategory.Rows.Add();

                        //nCategoryID,sDescription,sColorCode
                        c1FlexCategory.SetData(NewRow.Index, 0, oFollowUps[i].ID);
                        c1FlexCategory.SetData(NewRow.Index, 1, oFollowUps[i].Description);
                        if (oFollowUps[i].IsSystem == true)
                        {
                            c1FlexCategory.SetData(NewRow.Index, 2, "System");
                        }
                        else
                        {
                            c1FlexCategory.SetData(NewRow.Index, 2, "User");
                        }

                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oTaskMail.Dispose();
                oFollowUps.Dispose();
            }
        }

        public void Fill_StatusTypesflex(String sFilter)
        {

            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTasksMails.Common.Statuses oStatuses = new gloTasksMails.Common.Statuses();
            try
            {
                oStatuses = oTaskMail.GetStatuses(sFilter);


                c1FlexCategory.Clear();
                //dbo.TM_Status_MST,nStatusID,sDescription
                c1FlexCategory.Clear(ClearFlags.All);
                c1FlexCategory.Cols.Count = 3;
                c1FlexCategory.Rows.Count = 1;

                c1FlexCategory.Cols[0].Caption = "nStatusID";
                c1FlexCategory.Cols[1].Caption = "Status Type";
                c1FlexCategory.Cols[2].Caption = "Record Type";
                c1FlexCategory.Cols[2].Name = "Record Type";


                c1FlexCategory.Cols[0].Visible = false;
                c1FlexCategory.Cols[1].Visible = true;
                c1FlexCategory.Cols[2].Visible = true;

                int nWidth = c1FlexCategory.Width;
                c1FlexCategory.Cols[0].Width = 0;
                c1FlexCategory.Cols[1].Width = (int)(0.5 * (nWidth) - 10);
                c1FlexCategory.Cols[2].Width = (int)(0.2 * (nWidth) - 10);

                c1FlexCategory.DrawMode = DrawModeEnum.Normal;
                c1FlexCategory.AllowEditing = false;

                c1FlexCategory.BringToFront();
                if (oStatuses != null && oStatuses.Count > 0)
                {
                    int i;
                    for (i = 0; i <= oStatuses.Count - 1; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = c1FlexCategory.Rows.Add();

                        //dbo.TM_Status_MST,nStatusID,sDescription
                        c1FlexCategory.SetData(NewRow.Index, 0, oStatuses[i].ID);
                        c1FlexCategory.SetData(NewRow.Index, 1, oStatuses[i].Description);
                        if (oStatuses[i].IsSystem == true)
                        {
                            c1FlexCategory.SetData(NewRow.Index, 2, "System");
                        }
                        else
                        {
                            c1FlexCategory.SetData(NewRow.Index, 2, "User");
                        }

                    }
                }
                c1FlexCategory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                oTaskMail.Dispose();
                oStatuses.Dispose();
            }
        }

        private void Fill_Signatures(String sFilter)
        {
            clsSignature oSignature = new clsSignature(_databaseconnectionstring);
            try
            {

                DataTable dtSignature = null;
                dtSignature = oSignature.getAllSignatures(_UserId, 0, sFilter);

                c1FlexCategory.Clear(ClearFlags.All);
                c1FlexCategory.Cols.Count = 2;
                c1FlexCategory.Rows.Count = 1;

                c1FlexCategory.Cols[0].Caption = "nSignatureID";
                c1FlexCategory.Cols[1].Caption = "Signature Name";


                c1FlexCategory.Cols[0].Visible = false;
                c1FlexCategory.Cols[1].Visible = true;


                int nWidth = c1FlexCategory.Width;
                c1FlexCategory.Cols[0].Width = 0;
                c1FlexCategory.Cols[1].Width = (int)(0.5 * (nWidth) - 10);

                c1FlexCategory.DrawMode = DrawModeEnum.Normal;
                c1FlexCategory.AllowEditing = false;
                c1FlexCategory.Cols[1].TextAlign = TextAlignEnum.LeftCenter;

                c1FlexCategory.BringToFront();

                if (dtSignature != null)
                {
                    for (int i = 0; i < dtSignature.Rows.Count; i++)
                    {
                        C1.Win.C1FlexGrid.Row NewRow = c1FlexCategory.Rows.Add();
                        c1FlexCategory.SetData(NewRow.Index, 0, Convert.ToString(dtSignature.Rows[i]["nSignatureID"]));
                        c1FlexCategory.SetData(NewRow.Index, 1, Convert.ToString(dtSignature.Rows[i]["sSignatureName"]));

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion " Private Methods "

        #region " Tree View Events "

        private void trvTaskMailBook_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                if (trvTaskMailBook.SelectedNode != null)
                {
                    SelectedView = Convert.ToInt16(trvTaskMailBook.SelectedNode.Tag.ToString());


                    //if (SelectedView == 5)
                    //{
                    //    c1FlexCategory.Visible = false;
                    //    tsb_ADD.Visible = false;
                    //    tsb_Modify.Visible = false;
                    //    tsb_Delete.Visible = false;
                    //    tsb_Refresh.Visible = false;
                    //    // panel2.Visible = false;
                    //    pictureBox1.Visible = false;
                    //    lblSearch.Visible = false;
                    //    txtSearch.Visible = false;
                    //}
                    //else
                    //{
                    //    c1FlexCategory.Visible = true;
                    //    tsb_ADD.Visible = true;
                    //    tsb_Modify.Visible = true;
                    //    tsb_Delete.Visible = true;
                    //    tsb_Refresh.Visible = true;
                    //    //panel2.Visible = true;
                    //    pictureBox1.Visible = true;
                    //    lblSearch.Visible = true;
                    //    txtSearch.Visible = true;
                    //}

                    switch (SelectedView)
                    {


                        case 1: // Category Type

                        //lblSearch.Text = "Category Type";
                        //Fill_CategoryTypes();
                        //break;

                        case 2: // Follow Up Type

                            lblSearch.Text = "Follow Up :";
                            Fill_FollowUpflex("");
                            break;

                        case 3: // Status Type

                            lblSearch.Text = "Status Type :";
                            Fill_StatusTypesflex("");
                            break;

                        case 4: // Priority Type

                            lblSearch.Text = "Priority Type :";
                            Fill_PriorityTypesflex("");
                            break;

                        case 5:

                            lblSearch.Text = "Signature :";
                            Fill_Signatures("");
                            break;

                        case 6:
                            lblSearch.Text = "Task :`1";
                            //frmViewTask ofrmViewTask = new frmViewTask(_databaseconnectionstring);
                            frmViewTask ofrmViewTask = frmViewTask.GetInstance();
                            ofrmViewTask.Show();
                            ofrmViewTask.BringToFront();
                            //frmTask ofrmTask = new frmTask(_databaseconnectionstring,0);
                            //ofrmTask.ShowDialog();

                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //throw;
            }
        }


        #endregion " Tree View Events "

        #region "Tool Strip Commands "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                Int64 ID = 0;
                String Description = "";
                Int32 IsSystem = 0;
                if (e.ClickedItem.Tag.ToString() == "Delete" || e.ClickedItem.Tag.ToString() == "Modify")
                {
                    // Check only for Modify & Delete 

                    if (Convert.ToInt32(trvTaskMailBook.SelectedNode.Tag) != 0)
                    {
                        if (c1FlexCategory.RowSel == -1)
                        {
                            return;
                        }
                        else if (c1FlexCategory != null && c1FlexCategory.Rows.Count > 1)
                        {
                            if (SelectedView != 5) // 5 = Signature
                            {
                                ID = Convert.ToInt64(c1FlexCategory.GetData(c1FlexCategory.RowSel, 0));
                                Description = Convert.ToString(c1FlexCategory.GetData(c1FlexCategory.RowSel, 1));

                                if (c1FlexCategory.GetData(c1FlexCategory.RowSel, 2).ToString().ToUpper() == "SYSTEM")
                                {
                                    IsSystem = Convert.ToInt32(1);
                                }
                                else
                                {
                                    IsSystem = Convert.ToInt32(0);
                                }
                            }
                            else
                            {
                                ID = Convert.ToInt64(c1FlexCategory.GetData(c1FlexCategory.RowSel, 0));
                                Description = Convert.ToString(c1FlexCategory.GetData(c1FlexCategory.RowSel, 1));
                            }
                        }
                    }

                }


                switch (SelectedView)
                {
                    #region " Case Category Type "
                    case 1: // Category Type 
                        frmSetupCategory ofrmSetupCategory;

                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":

                                ofrmSetupCategory = new frmSetupCategory(0, _databaseconnectionstring);
                                ofrmSetupCategory.ShowDialog(this);
                                ofrmSetupCategory.Dispose();
                                ofrmSetupCategory = null;
                                Fill_CategoryTypes();
                                break;

                            case "Modify":
                                if (c1FlexCategory.Rows.Count > 1)
                                {
                                    if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                    {
                                        if (Convert.ToBoolean(IsSystem))
                                        {
                                            MessageBox.Show("Can not modify system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }

                                        ofrmSetupCategory = new frmSetupCategory(ID, _databaseconnectionstring);
                                        ofrmSetupCategory.CategoryName = Description;
                                        ofrmSetupCategory.ShowDialog(this);
                                        ofrmSetupCategory.Dispose();
                                        ofrmSetupCategory = null;
                                        Fill_CategoryTypes();
                                    }
                                }

                                break;
                            case "Delete":
                                {
                                    if (c1FlexCategory.Rows.Count > 1)
                                    {
                                        if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                        {
                                            if (Convert.ToBoolean(IsSystem))
                                            {
                                                MessageBox.Show("Can not delete system type", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }

                                            if (MessageBox.Show("Are you sure you want to delete this category type?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {

                                                gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

                                                //if (oTaskMail.DeleteCategory(ID) == false)
                                                //{
                                                //    MessageBox.Show("Category Type not delete successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    break;
                                                //}
                                                //if (oTaskMail.BlockCategory(ID) == false)
                                                if (oTaskMail.DeleteCategory(ID) == false)
                                                {
                                                    MessageBox.Show("Item not deleted.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                }
                                                Fill_CategoryTypes();
                                                oTaskMail.Dispose();
                                            }
                                        }
                                    }

                                } // END Delete
                                break;
                            case "Refresh":
                                Fill_CategoryTypes();
                                break;
                            case "Close":
                                this.Close();
                                break;
                            case "Help":

                                break;

                            default:
                                break;
                        }
                        break;

                    #endregion " Case Category Type "

                    #region " Case FollowUp Type "
                    case 2: //  Follow Up Type

                        frmSetupFollowUp ofrmSetupFollowUp;


                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":
                                {
                                    ofrmSetupFollowUp = new frmSetupFollowUp(0, _databaseconnectionstring);
                                    ofrmSetupFollowUp.ShowDialog(this);
                                    ofrmSetupFollowUp.Dispose();
                                    ofrmSetupFollowUp = null;
                                    //Fill_FollowUp();
                                    Fill_FollowUpflex("");


                                } //  END ADD
                                break;
                            case "Modify":
                                if (c1FlexCategory.Rows.Count > 1)
                                {
                                    if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                    {
                                        if (Convert.ToBoolean(IsSystem))
                                        {
                                            MessageBox.Show("Can not modify system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        else
                                        {

                                            ofrmSetupFollowUp = new frmSetupFollowUp(ID, _databaseconnectionstring);
                                            ofrmSetupFollowUp.FollowUpName = Description;
                                            //Code Added by Mayuri:20091103
                                            //To make invisible button save if form gets open for modify
                                            ofrmSetupFollowUp.tsb_Save.Visible = false;
                                            //End Code Added by Mayuri:20091103
                                            ofrmSetupFollowUp.ShowDialog(this);
                                            ofrmSetupFollowUp.Dispose();
                                            ofrmSetupFollowUp = null;
                                            //Fill_FollowUp();
                                            Fill_FollowUpflex("");
                                        }
                                    }
                                } //  END Modify
                                break;
                            case "Delete":
                                {
                                    if (c1FlexCategory.Rows.Count > 1)
                                    {
                                        if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                        {
                                            if (Convert.ToBoolean(IsSystem))
                                            {
                                                MessageBox.Show("Can not delete system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            if (MessageBox.Show("Are you sure you want to delete this follow up type?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {

                                                gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

                                                //if (oTaskMail.DeleteFollowUp(ID) == false)
                                                //{
                                                //    MessageBox.Show("FollowUp Type not delete successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    break;
                                                //}
                                                // if (oTaskMail.BlockFollowUp(ID) == false)
                                                if (oTaskMail.DeleteFollowUp(ID) == false)
                                                {
                                                    MessageBox.Show("Item not deleted.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                }
                                                //Fill_FollowUp();
                                                Fill_FollowUpflex("");
                                                oTaskMail.Dispose();
                                            }
                                        }
                                    }

                                } // END Delete
                                break;
                            case "Refresh":
                                {
                                    //Fill_FollowUp();
                                    Fill_FollowUpflex("");

                                } // END Refresh
                                break;
                            case "Close":
                                {
                                    this.Close();
                                }
                                break;

                            case "Help":
                                {
                                    frmViewMail ofrmViewMail = new frmViewMail(_databaseconnectionstring);
                                    ofrmViewMail.MdiParent = this.MdiParent;
                                    //ofrmViewMail.WindowState = FormWindowState.Maximized ;
                                    ofrmViewMail.Show();
                                    ofrmViewMail.BringToFront();

                                    //ofrmViewMail.Dispose();
                                }
                                break;

                        } // END Switch of Follow Up Type
                        break;

                    #endregion " Case FollowUp Type "

                    #region " Case Status Type "
                    case 3: // Status Type

                        frmSetupStatus ofrmSetupStatus;
                        c1FlexCategory.Visible = true;

                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":
                                {
                                    ofrmSetupStatus = new frmSetupStatus(0, _databaseconnectionstring);
                                    ofrmSetupStatus.ShowDialog(this);
                                    ofrmSetupStatus.Dispose();
                                    ofrmSetupStatus = null;
                                    // Fill_StatusTypes();
                                    Fill_StatusTypesflex("");

                                } //  END ADD
                                break;
                            case "Modify":
                                if (c1FlexCategory.Rows.Count > 1)
                                {
                                    if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                    {
                                        if (Convert.ToBoolean(IsSystem))
                                        {
                                            MessageBox.Show("Cannot modify system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        else
                                        {
                                            ofrmSetupStatus = new frmSetupStatus(ID, _databaseconnectionstring);
                                            ofrmSetupStatus.StatusName = Description;
                                            //Code Added by Mayuri:20091103
                                            //To make invisible button save if form gets open for modify
                                            ofrmSetupStatus.tsb_Save.Visible = false;
                                            //End Code Added by Mayuri:20091103
                                            ofrmSetupStatus.ShowDialog(this);
                                            ofrmSetupStatus.Dispose();
                                            ofrmSetupStatus = null;
                                            //Fill_StatusTypes();
                                            Fill_StatusTypesflex("");

                                        }
                                    }
                                }//  END Modify
                                break;
                            case "Delete":
                                {
                                    if (c1FlexCategory.Rows.Count > 1)
                                    {
                                        if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                        {
                                            if (Convert.ToBoolean(IsSystem))
                                            {
                                                MessageBox.Show("Cannot delete system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            if (MessageBox.Show("Are you sure you want to delete this status type?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {

                                                gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

                                                //if (oTaskMail.DeleteStatus(ID) == false)
                                                //{
                                                //    MessageBox.Show("Status Type not delete successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    break;
                                                //}
                                                //if (oTaskMail.BlockStatus(ID) == false)
                                                if (oTaskMail.DeleteStatus(ID) == false)
                                                {
                                                    MessageBox.Show("Status type not deleted.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                //Fill_StatusTypes();
                                                Fill_StatusTypesflex("");
                                                oTaskMail.Dispose();
                                            }
                                        }
                                    }
                                } // END Delete
                                break;
                            case "Refresh":
                                {
                                    //Fill_StatusTypes();
                                    Fill_StatusTypesflex("");
                                } // END Refresh
                                break;
                            case "Close":
                                {
                                    this.Close();
                                }
                                break;
                        } // END Switch of Status Type
                        break;

                    #endregion " Case Status Type "

                    #region " Case Priority Type "
                    case 4: // Priority Type 
                        frmSetupPriority ofrmSetupPriority;

                        switch (e.ClickedItem.Tag.ToString())
                        {
                            case "Add":
                                {
                                    ofrmSetupPriority = new frmSetupPriority(0, _databaseconnectionstring);
                                    ofrmSetupPriority.ShowDialog(this);
                                    ofrmSetupPriority.Dispose();
                                    ofrmSetupPriority = null;
                                    //Fill_PriorityTypes();
                                    Fill_PriorityTypesflex("");
                                } //  END ADD
                                break;
                            case "Modify":
                                if (c1FlexCategory.Rows.Count > 1)
                                {
                                    if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                    {
                                        if (Convert.ToBoolean(IsSystem))
                                        {
                                            MessageBox.Show("Cannot modify system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                        else
                                        {

                                            ofrmSetupPriority = new frmSetupPriority(ID, _databaseconnectionstring);
                                            ofrmSetupPriority.PriorityName = Description;
                                            //Code Added by Mayuri:20091103
                                            //To make invisible button save if form gets open for modify
                                            ofrmSetupPriority.tsb_Save.Visible = false;
                                            //End Code Added by Mayuri:20091103
                                            ofrmSetupPriority.ShowDialog(this);
                                            ofrmSetupPriority.Dispose();
                                            ofrmSetupPriority = null;
                                            //Fill_PriorityTypes();
                                            Fill_PriorityTypesflex("");
                                        }
                                    }
                                } //  END Modify
                                break;
                            case "Delete":
                                {
                                    if (c1FlexCategory.Rows.Count > 1)
                                    {
                                        if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                        {
                                            if (Convert.ToBoolean(IsSystem))
                                            {
                                                MessageBox.Show("Cannot delete system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            if (MessageBox.Show("Are you sure you want to delete this priority type?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                            {
                                                gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);

                                                //if (oTaskMail.DeletePriority(ID) == false)
                                                //{
                                                //    MessageBox.Show("Priority Type not delete successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    break;
                                                //}
                                                //if (oTaskMail.BlockPriority(ID) == false)
                                                if (oTaskMail.DeletePriority(ID) == false)
                                                {
                                                    MessageBox.Show("Priority type not deleted.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    break;
                                                }
                                                //Fill_PriorityTypes();
                                                Fill_PriorityTypesflex("");
                                                oTaskMail.Dispose();
                                            }
                                        }
                                    }

                                } // END Delete
                                break;
                            case "Refresh":
                                {
                                    //Fill_PriorityTypes();
                                    Fill_PriorityTypesflex("");
                                } // END Refresh
                                break;
                            case "Close":
                                {
                                    this.Close();
                                }
                                break;
                        } // END Switch of Priority Types
                        break;

                    #endregion " Case Priority Type "

                    case 5: //Signature
                        {
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Add":
                                    {
                                        frmSignature ofrmSign = new frmSignature(_databaseconnectionstring);
                                        ofrmSign.ShowDialog(this);
                                        ofrmSign.Dispose();
                                        ofrmSign = null;
                                        Fill_Signatures("");
                                    } //  END ADD
                                    break;
                                case "Modify":
                                    if (c1FlexCategory.Rows.Count > 1)
                                    {
                                        if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                        {
                                            frmSignature ofrmSign = new frmSignature(_databaseconnectionstring, ID);
                                            //Code Added by Mayuri:20091103
                                            //To make invisible button save if form gets open for modify
                                            ofrmSign.tsb_Save.Visible = false;
                                            //End Code Added by Mayuri:20091103
                                            ofrmSign.ShowDialog(this);
                                            ofrmSign.Dispose();
                                            ofrmSign = null;
                                            Fill_Signatures("");
                                        }
                                    } //  END Modify
                                    break;
                                case "Delete":
                                    {
                                        if (c1FlexCategory.Rows.Count > 1)
                                        {
                                            if (c1FlexCategory.Rows[0].ToString() != "" || c1FlexCategory.Rows[0].ToString() != "0")
                                            {
                                                if (MessageBox.Show("Are you sure you want to delete this signature ?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                                {
                                                    clsSignature oSignature = new clsSignature(_databaseconnectionstring);
                                                    oSignature.deleteSignature(ID);
                                                    Fill_Signatures("");
                                                }
                                            }
                                        }

                                    } // END Delete
                                    break;
                                case "Refresh":
                                    {
                                        Fill_Signatures("");
                                    } // END Refresh
                                    break;
                                case "Close":
                                    this.Close();
                                    break;
                            }
                            break;
                        }
                    case 6: //Task
                        {
                            switch (e.ClickedItem.Tag.ToString())
                            {
                                case "Close":
                                    this.Close();
                                    break;
                            }
                            break;
                        }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion "Tool Strip Commands "

        #region " Flex Grid Events "

        private void c1FlexCategory_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Int64 ID = 0;
                String Description = "";
                Int32 IsSystem = 0;
                if (c1FlexCategory.RowSel <= 0)
                {
                    return;
                }

                ID = Convert.ToInt64(c1FlexCategory.GetData(c1FlexCategory.RowSel, 0));
                Description = Convert.ToString(c1FlexCategory.GetData(c1FlexCategory.RowSel, 1));
                if (SelectedView != 5) //Signature
                {
                    if (c1FlexCategory.GetData(c1FlexCategory.RowSel, 2).ToString().ToUpper() == "SYSTEM")
                    {
                        IsSystem = Convert.ToInt32(1);
                    }
                    else
                    {
                        IsSystem = Convert.ToInt32(0);
                    }

                }

                switch (SelectedView)
                {
                    #region " Case Category Type "

                    case 1: // Category Type 
                        {
                            frmSetupCategory ofrmSetupCategory;
                            if (Convert.ToBoolean(IsSystem))
                            {
                                MessageBox.Show("Can not modify system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            ofrmSetupCategory = new frmSetupCategory(ID, _databaseconnectionstring);
                            ofrmSetupCategory.CategoryName = Description;
                            ofrmSetupCategory.ShowDialog(this);
                            ofrmSetupCategory.Dispose();
                            ofrmSetupCategory = null;
                            Fill_CategoryTypes();
                        }
                        break;


                    #endregion " Case Category Type "

                    #region " Case FollowUp Type "

                    case 2: //  Follow Up Type
                        {
                            frmSetupFollowUp ofrmSetupFollowUp;
                            if (Convert.ToBoolean(IsSystem))
                            {
                                MessageBox.Show("Can not modify system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                ofrmSetupFollowUp = new frmSetupFollowUp(ID, _databaseconnectionstring);
                                ofrmSetupFollowUp.FollowUpName = Description;
                                //Code Added by Mayuri:20091103
                                //To make invisible button save if form gets open for modify
                                ofrmSetupFollowUp.tsb_Save.Visible = false;
                                //End Code Added by Mayuri:20091103
                                ofrmSetupFollowUp.ShowDialog(this);
                                ofrmSetupFollowUp.Dispose();
                                ofrmSetupFollowUp = null;
                                //Fill_FollowUp();
                                Fill_FollowUpflex("");
                            } //  END Modify

                        } // END Switch of Follow Up Type
                        break;

                    #endregion " Case FollowUp Type "

                    #region " Case Status Type "

                    case 3: // Status Type
                        {
                            frmSetupStatus ofrmSetupStatus;
                            c1FlexCategory.Visible = true;

                            if (Convert.ToBoolean(IsSystem))
                            {
                                MessageBox.Show("Can not modify system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                ofrmSetupStatus = new frmSetupStatus(ID, _databaseconnectionstring);
                                ofrmSetupStatus.StatusName = Description;
                                //Code Added by Mayuri:20091103
                                //To make invisible button save if form gets open for modify
                                ofrmSetupStatus.tsb_Save.Visible = false;
                                //End Code Added by Mayuri:20091103
                                ofrmSetupStatus.ShowDialog(this);
                                ofrmSetupStatus.Dispose();
                                ofrmSetupStatus = null;
                                Fill_StatusTypesflex("");

                            } //  END Modify

                        } // END Switch of Status Type
                        break;

                    #endregion " Case Status Type "

                    #region " Case Priority Type "

                    case 4: // Priority Type 
                        {
                            frmSetupPriority ofrmSetupPriority;
                            if (Convert.ToBoolean(IsSystem))
                            {
                                MessageBox.Show("Cannot modify system type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                ofrmSetupPriority = new frmSetupPriority(ID, _databaseconnectionstring);
                                ofrmSetupPriority.PriorityName = Description;
                                //Code Added by Mayuri:20091103
                                //To make invisible button save if form gets open for modify
                                ofrmSetupPriority.tsb_Save.Visible = false;
                                //End Code Added by Mayuri:20091103
                                ofrmSetupPriority.ShowDialog(this);
                                ofrmSetupPriority.Dispose();
                                ofrmSetupPriority = null;
                                Fill_PriorityTypesflex("");

                            } //  END Modify

                        } // END Switch of Priority Types
                        break;

                    #endregion " Case Priority Type "

                    #region " Case Signature "

                    case 5:
                        {
                            if (ID > 0)
                            {
                                frmSignature ofrmSign = new frmSignature(_databaseconnectionstring, ID);
                                //Code Added by Mayuri:20091103
                                //To make invisible button save if form gets open for modify
                                ofrmSign.tsb_Save.Visible = false;
                                //End Code Added by Mayuri:20091103
                                ofrmSign.ShowDialog(this);
                                ofrmSign.Dispose();
                                ofrmSign = null;
                                Fill_Signatures("");
                            }
                        }
                        break;

                    #endregion " Case Signature "

                }

                // Code added to clear the reset search text 
                txtSearch.TextChanged -= new EventHandler(txtSearch_TextChanged);
                txtSearch.Text = string.Empty;
                txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void pnlC1Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string[] strSearchArray = null;
                string sFilter = "";

                string strSearch = txtSearch.Text.Trim();
                //strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");
                
                //Added By Mukesh for instring search 2009-08-29
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");


                if (strSearch.Trim() != "")
                {
                    strSearchArray = strSearch.Split(',');
                }

                switch (SelectedView)
                {
                    case 1: // Category
                        {
                            int RowIndex = c1FlexCategory.FindRow(strSearch, 1, 1, false, false, false);
                            c1FlexCategory.Row = RowIndex;
                        }
                        break;
                    case 2: // Follow Up
                        //{
                        //    int RowIndex = c1FlexCategory.FindRow(strSearch, 1, 1, false, false, false);
                        //    c1FlexCategory.Row = RowIndex;
                        //}
                        {
                            //Added By Mukesh For Implementing search 2009-08-19
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {                                    
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    sFilter += " ( sDescription Like '" + strSearch + "%' OR case when bIsSystem=1 then 'System' else 'User' end  Like '" + strSearch + "%' )";
                                    Fill_FollowUpflex(sFilter);
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( sDescription Like '" + strSearch + "%' OR case when bIsSystem=1 then 'System' else 'User' end Like '" + strSearch + "%') ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (sDescription Like '" + strSearch + "%' OR case when bIsSystem=1 then 'System' else 'User' end Like '" + strSearch + "%' ) ";
                                            }
                                        }
                                    }
                                    Fill_FollowUpflex(sFilter);
                                }
                            }
                            else
                            {
                                Fill_FollowUpflex("");
                            }
                        }
                        break;
                    case 3: // Status  
                        //{
                        //    int RowIndex = c1FlexCategory.FindRow(strSearch, 1, 1, false, false, false);
                        //    c1FlexCategory.Row = RowIndex;
                        //}
                        {
                            //Added By Mukesh For Implementing search 2009-08-19
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    sFilter += " ( sDescription Like '" + strSearch + "%' OR case when bIsSystem=1 then 'System' else 'User' end  Like '" + strSearch + "%' )";
                                    Fill_StatusTypesflex(sFilter);
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( sDescription Like '" + strSearch + "%' OR case when bIsSystem=1 then 'System' else 'User' end Like '" + strSearch + "%') ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (sDescription Like '" + strSearch + "%' OR case when bIsSystem=1 then 'System' else 'User' end Like '" + strSearch + "%' ) ";
                                            }
                                        }
                                    }
                                    Fill_StatusTypesflex(sFilter);
                                }
                            }
                            else
                            {
                                Fill_StatusTypesflex("");
                            }
                        }
                        break;
                    case 4: // Priority 
                        {
                        //    int RowIndex = c1FlexCategory.FindRow(strSearch, 1, 1, false, false, false);
                        //    c1FlexCategory.Row = RowIndex;
                        //}
                        if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    sFilter += " ( sDescription Like '" + strSearch + "%' OR case when bIsSystem=1 then 'System' else 'User' end  Like '" + strSearch + "%' )";
                                    Fill_PriorityTypesflex(sFilter);
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " ( sDescription Like '" + strSearch + "%' OR case when bIsSystem=1 then 'System' else 'User' end Like '" + strSearch + "%') ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (sDescription Like '" + strSearch + "%' OR case when bIsSystem=1 then 'System' else 'User' end Like '" + strSearch + "%' ) ";
                                            }
                                        }
                                    }
                                    Fill_PriorityTypesflex(sFilter);
                                }
                            }
                            else
                            {
                                Fill_PriorityTypesflex("");
                            }
                        }
                        break;
                    case 5: // Signature 
                        {
                        //    int RowIndex = c1FlexCategory.FindRow(strSearch, 1, 1, false, false, false);
                        //    c1FlexCategory.Row = RowIndex;
                        //}
                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    strSearch = strSearchArray[0];
                                    strSearch = strSearch.Trim();
                                    sFilter += " (sSignatureName Like '" + strSearch + "%')";
                                    Fill_Signatures(sFilter);
                                }
                                else
                                {
                                    //For Comma separated  value search
                                    for (int i = 0; i < strSearchArray.Length; i++)
                                    {
                                        strSearch = strSearchArray[i];
                                        strSearch = strSearch.Trim();
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (i == 0)
                                            {
                                                sFilter = " (sSignatureName Like '" + strSearch + "%') ";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (sSignatureName Like '" + strSearch + "%') ";
                                            }
                                        }
                                    }
                                    Fill_Signatures(sFilter);
                                }
                            }
                            else
                            {
                                Fill_Signatures("");
                            }
                        }
                        break;

                }



            }


            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1FlexCategory_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        #endregion " Flex Grid Events "

        #region " Form Events"

        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    // Dispose managed resources. 
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                    //frm = Nothing 
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed. 

                // Note that this is not thread safe. 
                // Another thread could start disposing the object 
                // after the managed resources are disposed, 
                // but before the disposed flag is set to true. 
                // If thread safety is necessary, it must be 
                // implemented by the client. 
            }
            frm = null;
            this.blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }

        ~frmViewTaskMailBook()
        {
            Dispose(false);
        }

        private void frmViewTaskMailBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        private void ts_gloCommunityDownload_Click(object sender, EventArgs e)
        {
            //'Code Start added by kanchan on 20120102 for gloCommunity integration
            if (EvntgloCommunityHandler != null)
                EvntgloCommunityHandler();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            txtSearch.Focus();
        }

       

    }
}