using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace WordToolStrip
{
    public partial class gloWordToolStrip : UserControl
    {
        public gloWordToolStrip()
        {
            InitializeComponent();
            
        }

        //20-May-13 Aniket: Resolving Memory Leaks
        ToolStripSplitButton tlsProviderSignatureButtonItem;
        public delegate void MyToolStripClickEventHandler(object sender, ToolStripItemClickedEventArgs e);
        public event MyToolStripClickEventHandler ToolStripClick;

        public delegate void MyToolStripButtonClickEventHandler(object sender, EventArgs e, String _Tag);
        public event MyToolStripButtonClickEventHandler ToolStripButtonClick;

        public DataTable dtInput { get; set; }
        public DataTable dtAssignUsers { get; set; }
        public string ptProvider { get; set; }
        public Int64 ptProviderId { get; set; }
        public Int64 AddedndumExamProviderId { get; set; }
        public string ExamProvider { get; set; }
        public bool _IsExamAddedndum { get; set; }

        public bool _IsSecureMsgEnabled = false;

        #region "Tool Strip Implementaion with in the user control"

        private bool _IsCoSignEnabled;
        private enumControlType _FormType;
        
        private string _ConnectionString;
        private Int64 _UserID;
       // private bool _IsSignAuthority = false;
        private bool _UseSignatureDelegate = false;
        public bool _isFinished_Lab = false;
          public bool _isFinishedLabButton = false;
      
        
        
        public bool IsCoSignEnabled
        {
            get
            {
                return _IsCoSignEnabled;
            }

            set
            {
                _IsCoSignEnabled = value;
            }
        }

        public bool IsEMAssociated
        {
            get
            {
                // SUDHIR 20090702 // TO HIDE AssociateEMField BUTTON //
                if (MyToolStrip != null)
                {
                    foreach (ToolStripItem oItem in MyToolStrip.Items)
                    {
                        if (oItem.Name == "AssociateEMField")
                        {
                            return oItem.Visible;
                        }
                    }
                }
                return false; // IF NOT FOUND //
                //return MyToolStrip.Items[17].Visible; 
            }
            set
            {
                // SUDHIR 20090702 // TO HIDE AssociateEMField BUTTON //
                if (MyToolStrip != null)
                {
                    foreach (ToolStripItem oItem in MyToolStrip.Items)
                    {
                        if (oItem.Name == "AssociateEMField")
                        {
                            oItem.Visible = value;
                            //code added by dipak 20091228 to  make AssociateEMField button show or Hide from customise menu
                            if (value == true)
                            {
                                MyToolStrip.ButtonsToHide.Remove(oItem.Name);
                            }
                            else
                            {
                                if (MyToolStrip.ButtonsToHide.Contains(oItem.Name) == false)
                                { 
                                    MyToolStrip.ButtonsToHide.Add(oItem.Name); 
                                }
                            }
                            //end code added by dipak 20091228
                        }
                    }
                }
                //MyToolStrip.Items[17].Visible = value;
                // END SUDHIR //
            }
        }
        public enumControlType FormType
        {
            get
            {
                return _FormType;
            }
            set
            {
                _FormType = value;
            }
        }
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; MyToolStrip.ConnectionString = value; }
        }

        public Boolean  IsSecureMsgEnabled
        {
            get { return _IsSecureMsgEnabled; }
            set { _IsSecureMsgEnabled = value;}
        }

        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; MyToolStrip.UserID = value; }
        }
        public ArrayList ButtonsToHide
        {
            get { return MyToolStrip.ButtonsToHide; }
            set { MyToolStrip.ButtonsToHide = value; }
        }

        protected virtual void OnToolStripClick(ToolStripItemClickedEventArgs e, object sender=null )
        {
            if (ToolStripClick != null)
            {
                ToolStripClick(sender==null? this: sender, e);
            }
        }
        protected virtual void OnToolStripButtonClick(EventArgs e, String _Tag, object sender=null )
        {
            if (ToolStripButtonClick != null)
            {
                ToolStripButtonClick(sender == null ? this : sender, e, _Tag);
            }
        }
        private void MyToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //e.ClickedItem.Name
            //ToolStripItem sndr = (ToolStripItem)sender;
            // MessageBox.Show("Sender: " + e.ClickedItem.Name.ToString());
            //Added on 20101007 by sanjog for signature
            if (e != null)
            {
                if ( ( e.ClickedItem != null ) && (e.ClickedItem.Tag != null) )
                {
                    if (e.ClickedItem.Tag.ToString() != "Insert Associated Provider Signature" ) // && e.ClickedItem.Tag != null)
                    {
                        OnToolStripClick(e,sender);
                    }

                    else if (e.ClickedItem.Pressed == false)    //Fire only when the Provider Sign Button is Clicke (not on dropdown of slit button)
                        OnToolStripClick(e,sender);
                }
            }
        }
        private void MyToolStrip_ButtonClicked(object sender, EventArgs e)
        {
            if ( (sender != null) && (sender is ToolStripItem) )
            {
                OnToolStripButtonClick(e, ((ToolStripItem)sender).Tag.ToString(),sender);
            }
            else
            {
                OnToolStripButtonClick(e, String.Empty,sender);
            }
        }


       

        /// <summary>
        /// Fill the Toolstip items based on the Form type
        /// </summary>
        /// <param name="_ControlType"></param>
        private void Fill_ToolStrip(enumControlType _ControlType)
        {
            MyToolStrip.Visible = true;
            MyToolStrip.Items.Clear();
            switch (_ControlType)
            {
                case enumControlType.Addendum:
                    AddAddendumItems();
                    AddProviderSignItems();
                   // AddSecureMessageItems();
                    break;
                case enumControlType.MessageAddendum:
                    AddMessageItems();
                    AddAddendumItems();
                    AddProviderSignItems();
                   // AddSecureMessageItems();
                    break;
                case enumControlType.Exams:
                    AddExamItems();
                    break;
                case enumControlType.ExamAddendum:
                    AddExamAddendum();
                    AddProviderSignItems();
                    //AddSecureMessageItems();
                    break;
                case enumControlType.FormGallery:
                    AddStandardItems();
                    AddPrintItems();
                    AddProviderSignItems();
                    AddExportDocumentItems();
                    AddSecureMessageItems();
                    break;
                case enumControlType.Messages:
                    AddMessageItems();
                    AddStandardItems();
                    AddDataItems();
                    AddMessageTimeStamp();
                    AddFinishItems();
                    AddProviderSignItems();
                    AddExportDocumentItems();
                    //AddSecureMessageItems();
                    break;
                case enumControlType.DiseaseManagement:
                    AddStandardItems();
                    AddProviderSignItems();
                    break;
                case enumControlType.PatientEducation:
                    AddStandardItems();
                    AddProviderSignItems();
                    AddExportDocumentItems();
                    AddEducationItems();
                    AddSendToPotalItems();
                   // AddSecureMessageItems();
                    break;
                case enumControlType.PatientGuidelines:
                    AddStandardItems();
                    AddProviderSignItems();
                    AddExportDocumentItems();
                    break;
                case enumControlType.PatientLetters:
                    AddStandardItems();
                    AddDataItems();
                    AddFinishItems();
                    AddProviderSignItems();
                    AddExportDocumentItems();
                    //AddSecureMessageItems();
                    break;
                case enumControlType.PatientConsent:
                    AddStandardItems();
                    AddDataItems();
                    AddFinishItems();
                    AddProviderSignItems();
                    AddExportDocumentItems();
                   // AddSecureMessageItems();
                    break;
                case enumControlType.DisclosureManagement:
                    AddStandardItems();
                   // AddDataItems();
                    AddDisclosureItems();
                    AddFinishItems();
                    AddProviderSignItems();
                    AddExportDocumentItems();
                    AddSecureMessageItems();
                    break;
                case enumControlType.PTProtocols:
                    AddStandardItems();
                    AddDataItems();
                    AddFinishItems();
                    AddProviderSignItems();
                    AddExportDocumentItems();
                    AddSecureMessageItems();
                    break;
                case enumControlType.Orders:
                    AddStandardItems();
                    AddFinishItems();
                    AddProviderSignItems();
                    //AddAcknowledment();
                    AddExportDocumentItems();
                    if (IsSecureMsgEnabled)
                    {
                        AddSecureMessageItems();
                    }
                 
                    break;
                case enumControlType.OrdersAddendum:
                    AddAddendumItems();
                    AddProviderSignItems();
                    if (IsSecureMsgEnabled)
                    {
                        AddSecureMessageItems();
                    }
                    break;
                case enumControlType.Others:
                    AddStandardItems();
                    AddDataItems();
                    break;
                case enumControlType.Referrals:
                    AddReferralItems();
                    break;
                case enumControlType.Standard:
                    AddStandardItems();
                    break;
                case enumControlType.TemplateGallery:
                    AddTemplateItems();
                    break;
                case enumControlType.OrdersComments:
                    AddViewItems();
                    break;
                case enumControlType.Triage:
                    AddStandardItems();
                    AddDataItems();
                    AddFinishItems();
                    AddProviderSignItems();
                    AddExportDocumentItems();
                    AddSecureMessageItems();
                    break;
                case enumControlType.TriageAddendum:
                    AddAddendumItems();
                    AddProviderSignItems();
                    AddSecureMessageItems();
                    break;
                case enumControlType.LabOrder:
                    AddStandardItems();
                    AddSecureMessageItems();
                    AddProviderSignItems();
                   // AddExportDocumentItems();
                    break;
                case enumControlType.DisclosureManagementAddendum:
                     AddAddendumItems();
                    AddProviderSignItems();
                    AddSecureMessageItems();
                    break;
                case enumControlType.PTProtocolAddendum:
                    AddAddendumItems();
                    AddProviderSignItems();
                    AddSecureMessageItems();
                    break;
                default: AddStandardItems();
                    break;       // break necessary on default
            }

            MyToolStrip.settooltip(MyToolStrip);   //settooltip function called for setting focus while printing
        
        }

        private void AddEducationItems()
        {
            ToolStripButton tlsButtonItem;
            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Reference Information";
            tlsButtonItem.Text = "&Ref.Info.";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.About_us;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ToolTipText = "Reference Information";
            tlsButtonItem.Tag = "Reference Information";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            tlsButtonItem.Visible = false;
            //if (MyToolStrip.ButtonsToHide.Contains(tlsButtonItem.Name) == false)
            //{
            //    MyToolStrip.ButtonsToHide.Add(tlsButtonItem.Name);
            //}
            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;
        }

        private void AddSendToPotalItems()
        {
            ToolStripButton tlsButtonItem;
            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Send To Portal";
            tlsButtonItem.Text = "&Send To Portal";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.SendtoPortal;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ToolTipText = "Send To Portal";
            tlsButtonItem.Tag = "Send To Portal";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            tlsButtonItem.Visible = true;
            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;
        }

        private void InitializeToolStrip()
        {
            MyToolStrip.ConnectionString = _ConnectionString;
            MyToolStrip.UserID = _UserID;
            MyToolStrip.ModuleName = "gloWordToolStrip_" + _FormType.ToString();
            
            if (MyToolStrip.ModuleName == "gloWordToolStrip_LabOrder" && _isFinished_Lab == false )
            {
                MyToolStrip.FinishTemplate = true;
            }
           
            MyToolStrip.InitializeContextMenu();
        }

        private void AddViewItems()
        {
            ToolStripButton tlsButtonItem;


            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Close";
            tlsButtonItem.Text = "&Close";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Close01;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Close";
            tlsButtonItem.Tag = "Close";
            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;


          
         
        }

        private void AddAcknowledment()
        {
            ToolStripButton tlsAcknowledmentButtonItem;

            tlsAcknowledmentButtonItem = new ToolStripButton();
            tlsAcknowledmentButtonItem.Name = "Acknowledment";
            tlsAcknowledmentButtonItem.Text = "&Ackw";
            tlsAcknowledmentButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsAcknowledmentButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsAcknowledmentButtonItem.Image = global::WordToolStrip.Properties.Resources.Acknowledgement;
            tlsAcknowledmentButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsAcknowledmentButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsAcknowledmentButtonItem.ToolTipText = "Acknowledment";
            tlsAcknowledmentButtonItem.Tag = "Acknowledment";
            
            MyToolStrip.Items.Add(tlsAcknowledmentButtonItem);
            tlsAcknowledmentButtonItem = null;


            ToolStripButton tlsViewAcknowledmentButtonItem;

            tlsViewAcknowledmentButtonItem = new ToolStripButton();
            tlsViewAcknowledmentButtonItem.Name = "View Acknowledment";
            tlsViewAcknowledmentButtonItem.Text = "&View Ackw";
            tlsViewAcknowledmentButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsViewAcknowledmentButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsViewAcknowledmentButtonItem.Image = global::WordToolStrip.Properties.Resources.View_Acknowledgement;
            tlsViewAcknowledmentButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsViewAcknowledmentButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsViewAcknowledmentButtonItem.ToolTipText = "View Acknowledment";
            tlsViewAcknowledmentButtonItem.Tag = "View Acknowledment";
            MyToolStrip.Items.Add(tlsViewAcknowledmentButtonItem);
            tlsViewAcknowledmentButtonItem = null;
        }

        /// <summary>
        /// To add the standard Items in ToolStrip
        /// </summary>
        private void AddStandardItems()
        {
            ToolStripButton tlsButtonItem;

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Name = "Mic";
            tlsButtonItem.Text = "&Mic";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.Image = global:: WordToolStrip.Properties.Resources.Mic_OFF;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ToolTipText = "Microphone Off";
            tlsButtonItem.Tag = "Microphone Off";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            tlsButtonItem.Visible = false;
            if (MyToolStrip.ButtonsToHide.Contains(tlsButtonItem.Name) == false)
            {
                MyToolStrip.ButtonsToHide.Add(tlsButtonItem.Name);
            }

            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;


            if (_isFinished_Lab == true)
            {

                tlsButtonItem = new ToolStripButton();
                tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tlsButtonItem.Name = "Finish";
                tlsButtonItem.Text = "&Finish";
                tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
                //tlsButtonItem.Image = (System.Drawing.Image)global::WordToolStrip.Properties.Resources.Save;
                tlsButtonItem.Image = global:: WordToolStrip.Properties.Resources.Finish_Red;
                tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
                //            tlsButtonItem.ImageAlign = ContentAlignment;
                tlsButtonItem.ToolTipText = "Finish";
                tlsButtonItem.Tag = "Finish";
                tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
                //  tlsButtonItem.Visible = false;
                tlsButtonItem.Visible = true;
                MyToolStrip.Items.Add(tlsButtonItem);
            }
            // else
            // {
            //     tlsButtonItem.Visible = false;
            //     if (MyToolStrip.ButtonsToHide.Contains(tlsButtonItem.Name) == false)
            //     {
            //         MyToolStrip.ButtonsToHide.Add(tlsButtonItem.Name);
            //     }
            //}
              tlsButtonItem = null;


              if (_isFinishedLabButton == true)
              {
                  tlsButtonItem = new ToolStripButton();
                  tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                  tlsButtonItem.Name = "Addendum";
                  tlsButtonItem.Text = "&Addendum";
                  tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
                  //tlsButtonItem.Image = (System.Drawing.Image)global::WordToolStrip.Properties.Resources.Save;
                  tlsButtonItem.Image = global:: WordToolStrip.Properties.Resources.Add_Addendum;
                  tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
                  //tlsButtonItem.ImageAlign = ContentAlignment;
                  tlsButtonItem.ToolTipText = "Addendum";
                  tlsButtonItem.Tag = "Addendum";
                  tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
                  tlsButtonItem.Visible = true;
                  MyToolStrip.Items.Add(tlsButtonItem);
              }
              //if (_isFinishedLabButton == true)
              //{
              //    tlsButtonItem.Visible = false;
              //}


              tlsButtonItem = null;


            
                  tlsButtonItem = new ToolStripButton();
                  tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                  tlsButtonItem.Name = "Save";
                  tlsButtonItem.Text = "Sa&ve";
                  tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
                  //tlsButtonItem.Image = (System.Drawing.Image)global::WordToolStrip.Properties.Resources.Save;
                  tlsButtonItem.Image = global:: WordToolStrip.Properties.Resources.Save_Green01;
                  tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
                  //            tlsButtonItem.ImageAlign = ContentAlignment;
                  tlsButtonItem.ToolTipText = "Save";
                  tlsButtonItem.Tag = "Save";
                  tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
                  MyToolStrip.Items.Add(tlsButtonItem);
            
            if(_isFinishedLabButton==true  )
            {
                tlsButtonItem.Visible = false;
            }
            
            tlsButtonItem = null;

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Save & Close";
            tlsButtonItem.Text = "&Save&&Cls";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Save_Cls;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ToolTipText = "Save and Close";
            tlsButtonItem.Tag = "Save and Close";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Print";
            tlsButtonItem.Text = "&Print";


            tlsButtonItem.Text = "&Print"; 
            tlsButtonItem.ToolTipText = "Print"; 
            
            

            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Print;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            
            tlsButtonItem.Tag = "Print";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "FAX";
            tlsButtonItem.Text = "&Fax";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Fax;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Fax";
            tlsButtonItem.Tag = "FAX";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Insert Sign";
            tlsButtonItem.Text = "&Sign";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert_Signature1;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Insert User Signature";
            tlsButtonItem.Tag = "Insert User Signature";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            
            
            if (_isFinishedLabButton == true)
            {
                tlsButtonItem.Visible = false;
            }

            tlsButtonItem = null;
            
            if  (_IsCoSignEnabled)
            {
                tlsButtonItem = new ToolStripButton();
                tlsButtonItem.Name = "Insert CoSign";
                tlsButtonItem.Text = "&Co-Sign";
                tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
                tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.CO_Signature;
                tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
                tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
                tlsButtonItem.ToolTipText = "Insert Co-Signature";
                tlsButtonItem.Tag = "Insert Co-Signature";
                tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
                MyToolStrip.Items.Add(tlsButtonItem);
                tlsButtonItem = null;
            }

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Capture Sign";
            tlsButtonItem.Text = "&CapSign";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Capture_Signature;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Capture Signature";
            tlsButtonItem.Tag = "Capture Signature";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Undo";
            tlsButtonItem.Text = "&Undo";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Undo;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Undo Typing";;
            tlsButtonItem.Tag = "Undo Typing";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
          

            if (_isFinishedLabButton == true)
            {
                tlsButtonItem.Visible = false;
            }
            tlsButtonItem = null;

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Redo";
            tlsButtonItem.Text = "&Redo";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Redo;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Redo Typing";
            tlsButtonItem.Tag = "Redo Typing";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            
            if (_isFinishedLabButton == true)
            {
                tlsButtonItem.Visible = false;
            }
            tlsButtonItem = null;

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Insert File";
            tlsButtonItem.Text = "&IntFile";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert_File;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Insert File";
            tlsButtonItem.Tag = "Insert File";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            
            if (_isFinishedLabButton == true)
            {
                tlsButtonItem.Visible = false;
            }
            tlsButtonItem = null;

            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Scan Documents";
            tlsButtonItem.Text = "&ScnImgs";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Sacn;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Scan Images";
            tlsButtonItem.Tag = "Scan Images";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            
            if (_isFinishedLabButton == true)
            {
                tlsButtonItem.Visible = false;
            }
            tlsButtonItem = null;



            // chetan added on 23-oct-2010 for strike through feature
            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "tblbtn_StrikeThrough";
            tlsButtonItem.Text = "&Strike Through";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.StrikeThtough;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Strike Through";
            tlsButtonItem.Tag = "StrikeThrough";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;

            // chetan added on 23-oct-2010 for strike through feature
         
            tlsButtonItem = new ToolStripButton();
            tlsButtonItem.Name = "Close";
            tlsButtonItem.Text = "&Close";
            tlsButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsButtonItem.Image = global::WordToolStrip.Properties.Resources.Close01;
            tlsButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsButtonItem.ToolTipText = "Close";
            tlsButtonItem.Tag = "Close";
            tlsButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsButtonItem);
            tlsButtonItem = null;


         


          

        }

        /// <summary>
        /// To add the Finish Items in Main ToolStrip
        /// </summary>
        private void AddFinishItems()
        {
            ToolStripButton tlsFinishButtonItem;

            tlsFinishButtonItem = new ToolStripButton();
            tlsFinishButtonItem.Name = "Save & Finish";
            tlsFinishButtonItem.Text = "&Finish";
            tlsFinishButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsFinishButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsFinishButtonItem.Image = global::WordToolStrip.Properties.Resources.Finish_Red;
            tlsFinishButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsFinishButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsFinishButtonItem.ToolTipText = "Save and Finish";
            tlsFinishButtonItem.Tag = "Save and Finish";
            tlsFinishButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsFinishButtonItem);
            tlsFinishButtonItem = null;

        }

        private void AddProviderSignItemsOld()
        {
            ToolStripButton tlsFinishButtonItem;

            tlsFinishButtonItem = new ToolStripButton();
            tlsFinishButtonItem.Name = "Insert Associated Provider Signature";
            tlsFinishButtonItem.Text = "&Provider Sign";
            tlsFinishButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsFinishButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsFinishButtonItem.Image = global::WordToolStrip.Properties.Resources.CO_Signature;
            tlsFinishButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsFinishButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsFinishButtonItem.ToolTipText = "Insert Associated Provider Signature";
            tlsFinishButtonItem.Tag = "Insert Associated Provider Signature";
            tlsFinishButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsFinishButtonItem);
            tlsFinishButtonItem = null;

        }
        private void AddProviderSignItems()
        {
            //ToolStripButton tlsFinishButtonItem;

            //20-May-13 Aniket: Resolving Memory Leaks
            tlsProviderSignatureButtonItem = new ToolStripSplitButton();
            tlsProviderSignatureButtonItem.Name = "Insert Associated Provider Signature";
            tlsProviderSignatureButtonItem.Text = "&Provider Sign";
            tlsProviderSignatureButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsProviderSignatureButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsProviderSignatureButtonItem.Image = global::WordToolStrip.Properties.Resources.CO_Signature;
            tlsProviderSignatureButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsProviderSignatureButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsProviderSignatureButtonItem.ToolTipText = "Insert Associated Provider Signature";
            tlsProviderSignatureButtonItem.Tag = "Insert Associated Provider Signature";
            tlsProviderSignatureButtonItem.ForeColor = Color.FromArgb(31, 73, 125);

            tlsProviderSignatureButtonItem.DropDownOpening += new EventHandler(ShowProviderSignatures);

            MyToolStrip.Items.Add(tlsProviderSignatureButtonItem);

            

            //AddProviderList(tlsFinishButtonItem);
            //if (_IsSignAuthority == true && _UseSignatureDelegate == true)
            //{
            //    MyToolStrip.Items["Insert Associated Provider Signature"].Enabled = true;

            //}
            //else if (_IsSignAuthority == false && _UseSignatureDelegate == true)
            //{
            //    MyToolStrip.Items["Insert Associated Provider Signature"].Enabled = false;

            //}
            //else if (_UseSignatureDelegate == false)
            //{
            //    MyToolStrip.Items["Insert Associated Provider Signature"].Enabled = true;

            //}
            //else
            //{
            //    MyToolStrip.Items["Insert Associated Provider Signature"].Enabled = false;

            //}
            if (_UseSignatureDelegate == true)
            {
                MyToolStrip.Items["Insert Associated Provider Signature"].Enabled = true;
            }
            //tlsProviderSignatureButtonItem = null;

            
            if (_isFinishedLabButton == true)
            {
                tlsProviderSignatureButtonItem.Visible = false;
            }
        }

        //20-May-13 Aniket: Resolving Memory Leaks
        private void ShowProviderSignatures(object sender, EventArgs e)
        {
            AddProviderList();
        }

        private void AddExportDocumentItems()
        {
            //ToolStripButton tlsFinishButtonItem;
            ToolStripButton tlsFinishButtonItem;
            tlsFinishButtonItem = new ToolStripButton();
            tlsFinishButtonItem.Name = "Export";
            tlsFinishButtonItem.Text = "&Export";
            tlsFinishButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsFinishButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsFinishButtonItem.Image = global::WordToolStrip.Properties.Resources.Export01 ;
            tlsFinishButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsFinishButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsFinishButtonItem.ToolTipText = "Export";
            tlsFinishButtonItem.Tag = "Export";
            tlsFinishButtonItem.ForeColor = Color.FromArgb(31, 73, 125);

            MyToolStrip.Items.Add(tlsFinishButtonItem);
                      
            tlsFinishButtonItem = null;

        }

        private void AddSecureMessageItems()
        {
          
                ToolStripButton tlsSecureMessageButtonItem;
                tlsSecureMessageButtonItem = new ToolStripButton();
                tlsSecureMessageButtonItem.Name = "SecureMsg";
                tlsSecureMessageButtonItem.Text = "&Provider DIRECT Msg";
                tlsSecureMessageButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
                tlsSecureMessageButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tlsSecureMessageButtonItem.Image = global::WordToolStrip.Properties.Resources.Send_secure_message;
                tlsSecureMessageButtonItem.ImageScaling = ToolStripItemImageScaling.None;
                tlsSecureMessageButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
                tlsSecureMessageButtonItem.ToolTipText = "Provider DIRECT Message";
                tlsSecureMessageButtonItem.Tag = "Provider DIRECT Msg";
                tlsSecureMessageButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
                if (MyToolStrip.ButtonsToHide.Contains(tlsSecureMessageButtonItem.Name) == false)
                {
                    MyToolStrip.Items.Add(tlsSecureMessageButtonItem);
                }
                tlsSecureMessageButtonItem = null;
                              
        }
        /// <summary>
        /// To load the Addendum Items in Main ToolStrip
        /// </summary>
        private void AddAddendumItems()
        {

            ToolStripButton tlsAddendumButtonItem;

            tlsAddendumButtonItem = new ToolStripButton();
            tlsAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsAddendumButtonItem.Name = "Save";
            tlsAddendumButtonItem.Text = "Sa&ve";
            tlsAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Save_Green01;
            tlsAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsAddendumButtonItem.ToolTipText = "Save";
            tlsAddendumButtonItem.Tag = "Save";
            tlsAddendumButtonItem.Visible = false;
            tlsAddendumButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsAddendumButtonItem);
            tlsAddendumButtonItem = null;


            tlsAddendumButtonItem = new ToolStripButton();
            tlsAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsAddendumButtonItem.Name = "Save & Finish";
            tlsAddendumButtonItem.Text = "&Save&&Cls";
            tlsAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Save_Cls;
            tlsAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsAddendumButtonItem.ToolTipText = "Save and Close";
            tlsAddendumButtonItem.Tag = "Save and Close";
            tlsAddendumButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsAddendumButtonItem);
            tlsAddendumButtonItem = null;

            tlsAddendumButtonItem = new ToolStripButton();
            tlsAddendumButtonItem.Name = "Print";
            
            tlsAddendumButtonItem.Text = " Print "; 
            tlsAddendumButtonItem.ToolTipText = "Print";

            tlsAddendumButtonItem.Text = "&Print";
            tlsAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Print;
            tlsAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            
            tlsAddendumButtonItem.Tag = "Print";
            tlsAddendumButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsAddendumButtonItem);
            tlsAddendumButtonItem = null;

            tlsAddendumButtonItem = new ToolStripButton();
            tlsAddendumButtonItem.Name = "FAX";
            tlsAddendumButtonItem.Text = "&Fax";
            tlsAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Fax;
            tlsAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsAddendumButtonItem.ToolTipText = "Fax";
            tlsAddendumButtonItem.Tag = "FAX";
            tlsAddendumButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsAddendumButtonItem);
            tlsAddendumButtonItem = null;

            tlsAddendumButtonItem = new ToolStripButton();
            tlsAddendumButtonItem.Name = "Add Addendum";
            tlsAddendumButtonItem.Text = "&Addendum";
            tlsAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Add_Addendum;
            tlsAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsAddendumButtonItem.ToolTipText = "Add Addendum";
            tlsAddendumButtonItem.Tag = "Add Addendum";
            tlsAddendumButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsAddendumButtonItem);
            tlsAddendumButtonItem = null;



            // chetan added on 23-oct-2010 for strike through feature
            tlsAddendumButtonItem = new ToolStripButton();
            tlsAddendumButtonItem.Name = "tblbtn_StrikeThrough";
            tlsAddendumButtonItem.Text = "&Strike Through";
            tlsAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.StrikeThtough;
            tlsAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsAddendumButtonItem.ToolTipText = "Strike Through";
            tlsAddendumButtonItem.Tag = "StrikeThrough";
            tlsAddendumButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsAddendumButtonItem);
            tlsAddendumButtonItem = null;

            // chetan added on 23-oct-2010 for strike through feature
            
            tlsAddendumButtonItem = new ToolStripButton();
            tlsAddendumButtonItem.Name = "Close";
            tlsAddendumButtonItem.Text = "&Close";
            tlsAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Close01;
            tlsAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsAddendumButtonItem.ToolTipText = "Close";
            tlsAddendumButtonItem.Tag = "Close";
            tlsAddendumButtonItem.ForeColor = Color.FromArgb(31, 73, 125);
            MyToolStrip.Items.Add(tlsAddendumButtonItem);
            tlsAddendumButtonItem = null;



        
        }

        /// <summary>
        /// To Sdd the Print Items in Main ToolStrip
        /// </summary>
        private void AddPrintItems()
        {
            ToolStripButton tlsPrintButtonItem;

            tlsPrintButtonItem = new ToolStripButton();
            tlsPrintButtonItem.Name = "PrintAll";
            tlsPrintButtonItem.Text = "&PrintAll";
            tlsPrintButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsPrintButtonItem.Image = global::WordToolStrip.Properties.Resources.Print_All;
            tlsPrintButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsPrintButtonItem.ToolTipText = "Print All";
            tlsPrintButtonItem.Tag = "Print All";
            MyToolStrip.Items.Add(tlsPrintButtonItem);
            tlsPrintButtonItem = null;


            tlsPrintButtonItem = new ToolStripButton();
            tlsPrintButtonItem.Name = "FaxAll";
            tlsPrintButtonItem.Text = "&Fax All";
            tlsPrintButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsPrintButtonItem.Image = global::WordToolStrip.Properties.Resources.Fax_All;
            tlsPrintButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsPrintButtonItem.ToolTipText = "Fax All";
            tlsPrintButtonItem.Tag = "Fax All";
            MyToolStrip.Items.Add(tlsPrintButtonItem);
            tlsPrintButtonItem = null;

        }

        /// <summary>
        /// To Sdd the Referral Items in Main ToolStrip
        /// </summary>
        private void AddReferralItems()
        {

            MyToolStrip.Items.Clear();

            ToolStripButton tlsReferralButtonItem;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Print";
            tlsReferralButtonItem.Text = "&Print";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Print;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsReferralButtonItem.ToolTipText = "Print";
            tlsReferralButtonItem.Tag = "Print";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "FAX";
            tlsReferralButtonItem.Text = "&Fax";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Fax;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsReferralButtonItem.ToolTipText = "Fax";
            tlsReferralButtonItem.Tag = "FAX";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Insert Sign";
            tlsReferralButtonItem.Text = "&Sign";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert_Signature1;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsReferralButtonItem.ToolTipText = "Insert User Signature";
            tlsReferralButtonItem.Tag = "Insert User Signature";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            if (_IsCoSignEnabled)
            {
                tlsReferralButtonItem = new ToolStripButton();
                tlsReferralButtonItem.Name = "Insert CoSign";
                tlsReferralButtonItem.Text = "&Co-Sign";
                tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
                tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.CO_Signature;
                tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
                tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
                tlsReferralButtonItem.ToolTipText = "Insert Co-Signature";
                tlsReferralButtonItem.Tag = "Insert Co-Signature";
                MyToolStrip.Items.Add(tlsReferralButtonItem);
                tlsReferralButtonItem = null;
            }

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Capture Sign";
            tlsReferralButtonItem.Text = "&CapSign";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Capture_Signature;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsReferralButtonItem.ToolTipText = "Capture Signature";
            tlsReferralButtonItem.Tag = "Capture Signature";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Undo";
            tlsReferralButtonItem.Text = "&Undo";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Undo;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsReferralButtonItem.ToolTipText = "Undo Typing";
            tlsReferralButtonItem.Tag = "Undo Typing";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Redo";
            tlsReferralButtonItem.Text = "&Redo";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Redo;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsReferralButtonItem.ToolTipText = "Redo Typing";
            tlsReferralButtonItem.Tag = "Redo Typing";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Insert File";
            tlsReferralButtonItem.Text = "&IntFile";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert_Signature1;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsReferralButtonItem.ToolTipText = "Insert File";
            tlsReferralButtonItem.Tag = "Insert File";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Scan Documents";
            tlsReferralButtonItem.Text = "&ScnImgs";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Sacn;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsReferralButtonItem.ToolTipText = "Scan Images";
            tlsReferralButtonItem.Tag = "Scan Images";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Previous";
            tlsReferralButtonItem.Text = "&Previous";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Previous;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ToolTipText = "Previous";
            tlsReferralButtonItem.Tag = "Previous";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Next";
            tlsReferralButtonItem.Text = "&Next";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Next02;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ToolTipText = "Next";
            tlsReferralButtonItem.Tag = "Next";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;

            tlsReferralButtonItem = new ToolStripButton();
            tlsReferralButtonItem.Name = "Close";
            tlsReferralButtonItem.Text = "&Close";
            tlsReferralButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsReferralButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsReferralButtonItem.Image = global::WordToolStrip.Properties.Resources.Close01;
            tlsReferralButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsReferralButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsReferralButtonItem.ToolTipText = "Close";
            tlsReferralButtonItem.Tag = "Close";
            MyToolStrip.Items.Add(tlsReferralButtonItem);
            tlsReferralButtonItem = null;


        }

        /// <summary>
        /// To Add the Message Items in Main ToolStrip
        /// </summary>
        private void AddMessageItems()
        {
            ToolStripButton tlsMessageButtonItem;

            tlsMessageButtonItem = new ToolStripButton();
            tlsMessageButtonItem.Name = "Show/Hide";
            tlsMessageButtonItem.Text = "&Show";
            tlsMessageButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsMessageButtonItem.Image = global::WordToolStrip.Properties.Resources.Show;
            tlsMessageButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsMessageButtonItem.ToolTipText = "Show/Hide";
            tlsMessageButtonItem.Tag = "Show/Hide";
            MyToolStrip.Items.Add(tlsMessageButtonItem);
            tlsMessageButtonItem = null;
        }

        /// <summary>
        /// To add the Message Time Stamp button
        /// </summary>
        private void AddMessageTimeStamp()
        {
            ToolStripButton tlsMessageButtonItem;

            tlsMessageButtonItem = new ToolStripButton();
            tlsMessageButtonItem.Name = "DateTimeStamp";
            tlsMessageButtonItem.Text = "&DtStamp";
            tlsMessageButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsMessageButtonItem.Image = global::WordToolStrip.Properties.Resources.Time_Stamp;
            tlsMessageButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsMessageButtonItem.ToolTipText = " Date Time Stamp ";
            tlsMessageButtonItem.Tag = " Date Time Stamp ";
            MyToolStrip.Items.Add(tlsMessageButtonItem);
            tlsMessageButtonItem = null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddExamItems()
        {
            MyToolStrip.Items.Clear();
            MyToolStrip.Visible = false;
        }

        private void AddExamAddendum()
        {
            MyToolStrip.Items.Clear();

            ToolStripButton tlsExamAddendumButtonItem;

            tlsExamAddendumButtonItem = new ToolStripButton();
            tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsExamAddendumButtonItem.Name = "Mic";
            tlsExamAddendumButtonItem.Text = "&Mic";
            tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsExamAddendumButtonItem.Image = global:: WordToolStrip.Properties.Resources.Mic_OFF;
            tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsExamAddendumButtonItem.ToolTipText = "Microphone Off";
            tlsExamAddendumButtonItem.Tag = "Microphone Off";
            tlsExamAddendumButtonItem.Visible = false;
            if (MyToolStrip.ButtonsToHide.Contains(tlsExamAddendumButtonItem.Name) == false)
            {
                MyToolStrip.ButtonsToHide.Add(tlsExamAddendumButtonItem.Name);
            }
            MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
            tlsExamAddendumButtonItem = null;

            tlsExamAddendumButtonItem = new ToolStripButton();
            tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsExamAddendumButtonItem.Name = "Save";
            tlsExamAddendumButtonItem.Text = "&Save&&Cls";
            tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsExamAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Save_Cls;
            tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsExamAddendumButtonItem.ToolTipText = "Save and Close";
            tlsExamAddendumButtonItem.Tag = "Save and Close";
            MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
            tlsExamAddendumButtonItem = null;

            tlsExamAddendumButtonItem = new ToolStripButton();
            tlsExamAddendumButtonItem.Name = "Insert Sign";
            tlsExamAddendumButtonItem.Text = "&Sign";
            tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsExamAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert_Signature1;
            tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsExamAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsExamAddendumButtonItem.ToolTipText = "Insert User Signature";
            tlsExamAddendumButtonItem.Tag = "Insert User Signature";
            MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
            tlsExamAddendumButtonItem = null;

            if (_IsCoSignEnabled)
            {
                tlsExamAddendumButtonItem = new ToolStripButton();
                tlsExamAddendumButtonItem.Name = "Insert CoSign";
                tlsExamAddendumButtonItem.Text = "&Co-Sign";
                tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
                tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tlsExamAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.CO_Signature;
                tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
                tlsExamAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
                tlsExamAddendumButtonItem.ToolTipText = "Insert Co-Signature";
                tlsExamAddendumButtonItem.Tag = "Insert Co-Signature";
                MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
                tlsExamAddendumButtonItem = null;
            }

            tlsExamAddendumButtonItem = new ToolStripButton();
            tlsExamAddendumButtonItem.Name = "Capture Sign";
            tlsExamAddendumButtonItem.Text = "&CapSign";
            tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsExamAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Capture_Signature;
            tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsExamAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsExamAddendumButtonItem.ToolTipText = "Capture Signature";
            tlsExamAddendumButtonItem.Tag = "Capture Signature";
            MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
            tlsExamAddendumButtonItem = null;

            tlsExamAddendumButtonItem = new ToolStripButton();
            tlsExamAddendumButtonItem.Name = "Undo";
            tlsExamAddendumButtonItem.Text = "&Undo";
            tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsExamAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Undo;
            tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsExamAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsExamAddendumButtonItem.ToolTipText = "Undo Typing";
            tlsExamAddendumButtonItem.Tag = "Undo Typing";
            MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
            tlsExamAddendumButtonItem = null;

            tlsExamAddendumButtonItem = new ToolStripButton();
            tlsExamAddendumButtonItem.Name = "Redo";
            tlsExamAddendumButtonItem.Text = "&Redo";
            tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsExamAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Redo;
            tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsExamAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsExamAddendumButtonItem.ToolTipText = "Redo Typing";
            tlsExamAddendumButtonItem.Tag = "Redo Typing";
            MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
            tlsExamAddendumButtonItem = null;

            tlsExamAddendumButtonItem = new ToolStripButton();
            tlsExamAddendumButtonItem.Name = "Insert File";
            tlsExamAddendumButtonItem.Text = "&IntFile";
            tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsExamAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert_File;
            tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsExamAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsExamAddendumButtonItem.ToolTipText = "Insert File";
            tlsExamAddendumButtonItem.Tag = "Insert File";
            MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
            tlsExamAddendumButtonItem = null;

            tlsExamAddendumButtonItem = new ToolStripButton();
            tlsExamAddendumButtonItem.Name = "Scan Documents";
            tlsExamAddendumButtonItem.Text = "&ScnImgs";
            tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsExamAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Sacn;
            tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsExamAddendumButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsExamAddendumButtonItem.ToolTipText = "Scan Images";
            tlsExamAddendumButtonItem.Tag = "Scan Images";
            MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
            tlsExamAddendumButtonItem = null;

            tlsExamAddendumButtonItem = new ToolStripButton();
            tlsExamAddendumButtonItem.Name = "Close";
            tlsExamAddendumButtonItem.Text = "&Close";
            tlsExamAddendumButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsExamAddendumButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsExamAddendumButtonItem.Image = global::WordToolStrip.Properties.Resources.Close01;
            tlsExamAddendumButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsExamAddendumButtonItem.ToolTipText = "Close";
            tlsExamAddendumButtonItem.Tag = "Close";
            MyToolStrip.Items.Add(tlsExamAddendumButtonItem);
            tlsExamAddendumButtonItem = null;
        }

        /// <summary>
        /// To Add Data Items to Main tool Strip
        /// </summary>
        private void AddDataItems()
        {
            ToolStripButton tlsDataButtonItem;

            tlsDataButtonItem = new ToolStripButton();
            tlsDataButtonItem.Name = "Prescription";
            tlsDataButtonItem.Text = "&Rx-Meds";
            tlsDataButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsDataButtonItem.Image = global::WordToolStrip.Properties.Resources.RXMed;
            tlsDataButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsDataButtonItem.ToolTipText = "Prescription & Medication";
            tlsDataButtonItem.Tag = "Prescription & Medication";
            MyToolStrip.Items.Add(tlsDataButtonItem);
            tlsDataButtonItem = null;

            tlsDataButtonItem = new ToolStripButton();
            tlsDataButtonItem.Name = "OrderTemplates";
            tlsDataButtonItem.Text = "&Order Templates";
            tlsDataButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsDataButtonItem.Image = global::WordToolStrip.Properties.Resources.Radiology_Order_;
            tlsDataButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsDataButtonItem.ToolTipText = "Order Templates";
            tlsDataButtonItem.Tag = "Order Templates";
            MyToolStrip.Items.Add(tlsDataButtonItem);
            tlsDataButtonItem = null;

            //tlsDataButtonItem = new ToolStripButton();
            //tlsDataButtonItem.Name = "Labs";
            //tlsDataButtonItem.Text = "Labs";
            //tlsDataButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            //tlsDataButtonItem.Image = global::WordToolStrip.Properties.Resources.Labs;
            //tlsDataButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            //tlsDataButtonItem.ToolTipText = "Labs";
            //MyToolStrip.Items.Add(tlsDataButtonItem);
            //tlsDataButtonItem = null;
        }
        
        /// <summary>
        /// To Add Template Items to Main tool Strip
        /// </summary>
        private void AddTemplateItems()
        {
            MyToolStrip.Items.Clear();

            ToolStripButton tlsTemplateButtonItem;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Name = "Mic";
            tlsTemplateButtonItem.Text = "&Mic";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.Image = global:: WordToolStrip.Properties.Resources.Mic_OFF;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ToolTipText = "Microphone Off";
            tlsTemplateButtonItem.Tag = "Microphone Off";
            tlsTemplateButtonItem.Visible = false;
            if (MyToolStrip.ButtonsToHide.Contains(tlsTemplateButtonItem.Name) == false)
            {
                MyToolStrip.ButtonsToHide.Add(tlsTemplateButtonItem.Name);
            }
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;


            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "New";
            tlsTemplateButtonItem.Text = "&New";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.New;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ToolTipText = "New";
            tlsTemplateButtonItem.Tag = "New";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Open";
            tlsTemplateButtonItem.Text = "&Open";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Open;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ToolTipText = "Open";
            tlsTemplateButtonItem.Tag = "Open";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Name = "SaveClose";
            tlsTemplateButtonItem.Text = "&Save&&Cls";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Save_Cls;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ToolTipText = "Save and Close";
            tlsTemplateButtonItem.Tag = "Save and Close";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Name = "Save";
            tlsTemplateButtonItem.Text = "Sa&ve";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Save_Green01;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ToolTipText = "Save";
            tlsTemplateButtonItem.Tag = "Save";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Name = "SaveAs";
            tlsTemplateButtonItem.Text = "&SaveAs";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Save_As_Green;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ToolTipText = "SaveAs";
            tlsTemplateButtonItem.Tag = "SaveAs";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Close";
            tlsTemplateButtonItem.Text = "&Close";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Close01;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsTemplateButtonItem.ToolTipText = "Close";
            tlsTemplateButtonItem.Tag = "Close";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Insert CheckBox";
            tlsTemplateButtonItem.Text = "&InsCB";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert_CheckBox;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ToolTipText = "Insert CheckBox";
            tlsTemplateButtonItem.Tag = "Insert CheckBox";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Insert DropDownlist";
            tlsTemplateButtonItem.Text = "&InsDDL";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert_DropDownList;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ToolTipText = "Insert DropDownlist";
            tlsTemplateButtonItem.Tag = "Insert DropDownlist";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Insert Header";
            tlsTemplateButtonItem.Text = "&Header";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert__Header;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ToolTipText = "Insert Header";
            tlsTemplateButtonItem.Tag = "Insert Header";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Insert File";
            tlsTemplateButtonItem.Text = "&IntFile";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Insert_File;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsTemplateButtonItem.ToolTipText = "Insert File";
            tlsTemplateButtonItem.Tag = "Insert File";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Undo";
            tlsTemplateButtonItem.Text = "&Undo";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Undo;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsTemplateButtonItem.ToolTipText = "Undo Typing";
            tlsTemplateButtonItem.Tag = "Undo Typing";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Redo";
            tlsTemplateButtonItem.Text = "&Redo";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Redo;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsTemplateButtonItem.ToolTipText = "Redo Typing";
            tlsTemplateButtonItem.Tag = "Redo Typing";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Capture Sign";
            tlsTemplateButtonItem.Text = "&CapSign";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Capture_Signature;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsTemplateButtonItem.ToolTipText = "Capture Signature";
            tlsTemplateButtonItem.Tag = "Capture Signature";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "Scan Documents";
            tlsTemplateButtonItem.Text = "&ScnImgs";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Sacn;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsTemplateButtonItem.ToolTipText = "Scan Images";
            tlsTemplateButtonItem.Tag = "Scan Images";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

          

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "AddBM";
            tlsTemplateButtonItem.Text = "&AddBkmrk";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Add_BookMark;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            //tlsTemplateButtonItem.BackgroundImage = global::WordToolStrip.Properties.Resources.blubtn;
            tlsTemplateButtonItem.ToolTipText = "Add BookMark";
            tlsTemplateButtonItem.Tag = "Add BookMark";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "RemoveBM";
            tlsTemplateButtonItem.Text = "&DelBkmrk";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Remove_BookMark1;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            //tlsTemplateButtonItem.BackgroundImage = global::WordToolStrip.Properties.Resources.blubtn;

            //tlsTemplateButtonItem.ToolTipText = "Remove BookMarks"; Commented Sandip Darade 20090306
            tlsTemplateButtonItem.ToolTipText = "Delete All Bookmarks"; //Added Sandip Darade 20090306
            tlsTemplateButtonItem.Tag = "Delete All Bookmarks"; 
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

            tlsTemplateButtonItem = new ToolStripButton();
            tlsTemplateButtonItem.Name = "ShowHideBM";
            tlsTemplateButtonItem.Text = "&Show/Hide Bkmrk";
            tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Show;
            tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            //tlsTemplateButtonItem.BackgroundImage = global::WordToolStrip.Properties.Resources.blubtn;
            tlsTemplateButtonItem.ToolTipText = "Show/Hide BookMarks";
            tlsTemplateButtonItem.Tag = "Show/Hide BookMarks";
            MyToolStrip.Items.Add(tlsTemplateButtonItem);
            tlsTemplateButtonItem = null;

           
                tlsTemplateButtonItem = new ToolStripButton();
                tlsTemplateButtonItem.Name = "AssociateEMField";
                tlsTemplateButtonItem.Text = "&Associate EM Field";
                tlsTemplateButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
                tlsTemplateButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tlsTemplateButtonItem.Image = global::WordToolStrip.Properties.Resources.Associate_EM_code01;
                tlsTemplateButtonItem.ImageScaling = ToolStripItemImageScaling.None;
                tlsTemplateButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
                //tlsTemplateButtonItem.BackgroundImage = global::WordToolStrip.Properties.Resources.blubtn;
                tlsTemplateButtonItem.ToolTipText = "Associate EM Field";
                tlsTemplateButtonItem.Tag = "Associate EM Field";
                tlsTemplateButtonItem.Visible = false;
                MyToolStrip.Items.Add(tlsTemplateButtonItem);
                tlsTemplateButtonItem = null;


        }

        /// <summary>
        /// To add the Disclosure Info Items in Main ToolStrip
        /// </summary>
        private void AddDisclosureItems()
        {
            ToolStripButton tlsDisclosureButtonItem;

            tlsDisclosureButtonItem = new ToolStripButton();
            tlsDisclosureButtonItem.Name = "DisclosureSets";
            tlsDisclosureButtonItem.Text = "&Discl Sets";
            tlsDisclosureButtonItem.TextImageRelation = TextImageRelation.ImageAboveText;
            tlsDisclosureButtonItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tlsDisclosureButtonItem.Image = global::WordToolStrip.Properties.Resources.Disclosure_Set;
            tlsDisclosureButtonItem.ImageScaling = ToolStripItemImageScaling.None;
            tlsDisclosureButtonItem.ImageAlign = ContentAlignment.MiddleCenter;
            tlsDisclosureButtonItem.ToolTipText = "Disclosure Sets";
            tlsDisclosureButtonItem.Tag = "Disclosure Sets";
            MyToolStrip.Items.Add(tlsDisclosureButtonItem);
            tlsDisclosureButtonItem = null;

        }
                       
        # endregion

        private void gloWordToolStrip_Load(object sender, EventArgs e)
        {
            
            if (_ConnectionString !=null  &&  _ConnectionString.Trim() != "")
            {
                _UseSignatureDelegate = CheckSignDelegateStatus();

                //SLR: Free dtAssignUsers before assigning a new memory.
                if (dtAssignUsers != null)
                {
                    dtAssignUsers.Dispose();
                    dtAssignUsers = null;
                }

                dtAssignUsers = GetAllAssignProviders(_UserID);
            }

            Fill_ToolStrip(_FormType);
            InitializeToolStrip();
            
        }

         //'Added On 20101006 by sanjog 
        public DataTable GetAllAssignProviders(Int64 userid)
        {
            DataTable dt; //SLR: new is not needed
            SqlConnection conn = new SqlConnection(_ConnectionString);
            SqlDataAdapter da = default(SqlDataAdapter);
            string _sqlstr = "";

            dt = new DataTable();
            try
            {
                if (_ConnectionString.Trim() != "")
                {
                    //ADD THE CONDITION FOR MIDDLE NAME WHEHTER IT IS BLANK 20100619
                    _sqlstr = "select DISTINCT pr.nProviderID ,ISNULL(pr.sFirstName,'') + ' ' + CASE ISNULL(pr.sMiddleName,'') WHEN  '' THEN '' When ISNULL(pr.sMiddleName,'') then  ISNULL(pr.sMiddleName,'') + ' 'END + ISNULL(pr.sLastName,'') AS sProviderName from ProviderSignature_DTl p inner join provider_mst pr on p.nproviderid=pr.nproviderid where p.nUSerID =" + userid + "";
                    da = new SqlDataAdapter(_sqlstr, conn);
                    da.Fill(dt);
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch //(Exception ex)
            {
                return dt;
            }
            finally
            {
                //SLR: Finaly free conn, da
                if (da != null)
                    da.Dispose();
                da = null;
                if (conn != null)
                    if (conn.State != ConnectionState.Closed) { conn.Close(); }
                    conn.Dispose();
                   conn = null;
            }
        }

        public bool CheckSignDelegateStatus()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            string strSQL = "";
            object result;
            try
            {
                if (_ConnectionString.Trim()!= "")
                {
                    strSQL = "SELECT ISNULL(sSettingsValue,0) AS sSettingsValue from settings where sSettingsName Like 'USESIGNATUREDELEGATES%'";
                    oDB.Connect(false);
                    result = oDB.ExecuteScalar_Query(strSQL);
                    oDB.Disconnect();
                    if (result != null)
                    {
                        if (result.ToString() != "0")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        //Added on 20101019
        public Int64 GetPatientProviderRights(long nUserID)
        {

            gloDatabaseLayer.DBLayer  oDB ;
            oDB = new gloDatabaseLayer.DBLayer(_ConnectionString);
            string strSQL = null;
            object nProviderId;

            try
            {
                oDB.Connect(false);
                //strSQL = "Select nProviderId from Patient where nPatientID = " + nPatientID;
              strSQL ="select user_mst.nproviderid from user_mst INNER JOIN Provider_mst ON user_mst.nproviderid=Provider_mst.nproviderid WHERE user_mst.nuserid=" +_UserID ;

                
                    nProviderId = oDB.ExecuteScalar_Query(strSQL);
                    oDB.Disconnect();
                    if (nProviderId.ToString() != "0" && nProviderId.ToString() != "")
                    {
                        return Convert .ToInt64 (nProviderId );
                    }
                    else
                    {
                        return 0;
                    }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                //SLR: Disposed ODB
                if (oDB != null)
                {                    
                    oDB.Dispose();
                }
                oDB = null;
            }

        }


        //20-May-13 Aniket: Resolving Memory Leaks
        private void AddProviderList()
        {
            
           bool rslt =false ;
           
            ToolStripMenuItem oItem;
            Int64 nProviderID;
            Int32 i;
            bool blnFlag1 = false;
            bool prvPresent = false;

            
            //string exmProvider = "";

            try
            {
                rslt = CheckSignDelegateStatus();
                if (rslt == true)
                {
                    //20-May-13 Aniket: Resolving Memory Leaks
                    if (tlsProviderSignatureButtonItem != null)
                    {
                        
                        tlsProviderSignatureButtonItem.DropDownItems.Clear();
                        nProviderID = GetPatientProviderRights(_UserID);
                        if (_IsExamAddedndum == true)
                        {
                            //if (nProviderID == AddedndumExamProviderId)
                            //{
                                blnFlag1 = true;
                                oItem = new ToolStripMenuItem();
                                //oItem.Text="Patient Provider : " + dtInput.Rows[i][1].ToString();
                                oItem.Text = "&Exam Provider : " + ExamProvider.ToString().Trim();
                                oItem.Tag = AddedndumExamProviderId;
                                oItem.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
                                oItem.ForeColor = Color.FromArgb(31, 73, 125);
                                oItem.Click += new EventHandler(MyToolStrip_ButtonClicked);
                                //20-May-13 Aniket: Resolving Memory Leaks
                                tlsProviderSignatureButtonItem.DropDownItems.Add(oItem);
                                oItem = null;
                            //}
                            //else
                            //{
                            //    if (dtInput != null)
                            //    {
                            //        for (i = 0; i < dtInput.Rows.Count; i++)
                            //        {
                            //            if (ptProvider.Trim() == dtInput.Rows[i][1].ToString().Trim())
                            //            {
                            //                prvPresent = true;
                            //                break;
                            //            }
                            //        }
                            //        if (blnFlag1 == false && prvPresent==true )
                            //        {
                            //            blnFlag1 = true;
                            //            oItem = new ToolStripMenuItem();
                            //            oItem.Text = "&Exam Provider : " + dtInput.Rows[i][1].ToString();
                            //            //oItem.Text = "Exam Provider : " + ExamProvider.ToString().Trim();
                            //            oItem.Tag = dtInput.Rows[i][0].ToString();
                            //            oItem.Font = gloGlobal.clsgloFont.gFont; // new Font("Tahoma", 9, FontStyle.Regular);
                            //            oItem.ForeColor = Color.FromArgb(31, 73, 125);
                            //            oItem.Click += new EventHandler(MyToolStrip_ButtonClicked);
                            //            tlsProviderSignatureButtonItem.DropDownItems.Add(oItem);
                            //            oItem = null;
                            //        }
                            //    }

                            //}
                            if (dtInput != null)
                            {
                                for (int iRow = 0; iRow < dtInput.Rows.Count; iRow++)
                                {
                                    if (ExamProvider.Trim() != dtInput.Rows[iRow][1].ToString().Trim())
                                    {
                                        oItem = new ToolStripMenuItem();
                                        oItem.Text = dtInput.Rows[iRow][1].ToString();
                                        oItem.Tag = dtInput.Rows[iRow][0].ToString();
                                        oItem.Font = gloGlobal.clsgloFont.gFont; // new Font("Tahoma", 9, FontStyle.Regular);
                                        oItem.ForeColor = Color.FromArgb(31, 73, 125);
                                        oItem.Click += new EventHandler(MyToolStrip_ButtonClicked);
                                        tlsProviderSignatureButtonItem.DropDownItems.Add(oItem);
                                        oItem = null;
                                    }
                                }
                            }
                        }
                        if (_IsExamAddedndum == false)
                        {

                            bool ptPrvInsert = false;
                            if (nProviderID == ptProviderId)
                            {
                                ptPrvInsert = true;
                                oItem = new ToolStripMenuItem();
                                //oItem.Text="Patient Provider : " + dtInput.Rows[i][1].ToString();
                                oItem.Text = "&Patient Provider : " + ptProvider.ToString().Trim();
                                oItem.Tag = ptProviderId;
                                oItem.Font = gloGlobal.clsgloFont.gFont; // new Font("Tahoma", 9, FontStyle.Regular);
                                oItem.ForeColor = Color.FromArgb(31, 73, 125);
                                oItem.Click += new EventHandler(MyToolStrip_ButtonClicked);
                                tlsProviderSignatureButtonItem.DropDownItems.Add(oItem);
                                oItem = null;
                            }
                            if (dtInput != null)
                            {


                                //Int32 i;
                                //for (i = 0; i < dtInput.Rows.Count; i++)
                                //{
                                //    if (ptProvider.Trim() == dtInput.Rows[i][1].ToString().Trim())
                                //    {
                                //        prvPresent = true;
                                //        break;
                                //    }
                                //}
                                //if (prvPresent==true )
                                //{
                                // GetPatientProvider()

                                // }

                                //Sanjog-Start- 20101102
                                if (ptPrvInsert == false)
                                {
                                    for (i = 0; i < dtInput.Rows.Count; i++)
                                    {
                                        if (ptProvider.Trim() == dtInput.Rows[i][1].ToString().Trim())
                                        {
                                            prvPresent = true;
                                            break;
                                        }
                                    }
                                    if (prvPresent == true)
                                    {
                                        oItem = new ToolStripMenuItem();
                                        oItem.Text = "&Patient Provider : " + dtInput.Rows[i][1].ToString();
                                        oItem.Tag = dtInput.Rows[i][0].ToString();
                                        oItem.Font = gloGlobal.clsgloFont.gFont; // new Font("Tahoma", 9, FontStyle.Regular);
                                        oItem.ForeColor = Color.FromArgb(31, 73, 125);
                                        oItem.Click += new EventHandler(MyToolStrip_ButtonClicked);
                                        tlsProviderSignatureButtonItem.DropDownItems.Add(oItem);
                                        oItem = null;
                                    }
                                }
                                //Sanjog-End- 20101102
                                for (int iRow = 0; iRow < dtInput.Rows.Count; iRow++)
                                {
                                    if (ptProvider.Trim() != dtInput.Rows[iRow][1].ToString().Trim())
                                    {
                                        oItem = new ToolStripMenuItem();
                                        oItem.Text = dtInput.Rows[iRow][1].ToString();
                                        oItem.Tag = dtInput.Rows[iRow][0].ToString();
                                        oItem.Font = gloGlobal.clsgloFont.gFont; // new Font("Tahoma", 9, FontStyle.Regular);
                                        oItem.ForeColor = Color.FromArgb(31, 73, 125);
                                        oItem.Click += new EventHandler(MyToolStrip_ButtonClicked);
                                        tlsProviderSignatureButtonItem.DropDownItems.Add(oItem);
                                        oItem = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }


    }


    
    public enum enumControlType
    {
        Standard,
        FormGallery,
        Referrals,
        TemplateGallery,
        Messages,
        Exams,
        Addendum,
        Orders,
        OrdersComments,
        PatientConsent,
        PTProtocols,
        PatientLetters,
        PatientGuidelines,
        PatientEducation,
        DiseaseManagement,
        Others,
        ExamAddendum,
        MessageAddendum,
        DisclosureManagement,
        Triage,
        TriageAddendum,
        LabOrder,
        DisclosureManagementAddendum,
        PTProtocolAddendum,
        OrdersAddendum
    }
}
