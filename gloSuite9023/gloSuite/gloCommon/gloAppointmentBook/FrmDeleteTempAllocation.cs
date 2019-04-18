using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAppointmentBook.Books;
using C1.Win.C1FlexGrid;
using gloAppointmentBook;
using System.IO;
using gloAuditTrail;
namespace gloAppointmentBook
{
    public partial class DeleteTemplateAllocation : Form
    {
        #region "Variable Declaration"

        private string _MessageBoxCaption = String.Empty;
        private string _databaseConnectionString = "";
        //private Int64 _AllocationID = 0;
        private Int64 _ProviderID = 0;
        //private Int32 _rowIndex = 0;
        private DataTable _dtTemplates = new DataTable();
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        //private Int64 _nTemplateAllocationMasterID = 0;
       
        #endregion

         public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public DeleteTemplateAllocation()
        {
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }
        public DeleteTemplateAllocation(Int64 ProviderID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
            _ProviderID = ProviderID;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            //Bug #47154: 00000410 : Scheduling
            //code added to show tooltip for combo box.
            cmbTemplates.DrawMode = DrawMode.OwnerDrawFixed;
            cmbTemplates.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion

        }

        private void frmDeleteTempAllocation_Load(object sender, EventArgs e)
        {
            FillTemplates();
            //FillProviders();
        }

        public void FillProviders()
        {
            try
            {
                gloAppointmentTemplate ogloProvider = new gloAppointmentTemplate(_databaseConnectionString);
                DataTable dtProviders = ogloProvider.GetProviders(_ProviderID);
                if (dtProviders != null)
                {
                    cmbProviders.DataSource = dtProviders;
                    cmbProviders.DisplayMember = dtProviders.Columns["sASBaseDesc"].ColumnName;

                }
                cmbProviders.SelectedIndex = -1;
                if (ogloProvider != null)
                {
                    ogloProvider.Dispose();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
           
 
        }

        private void FillTemplates()
        {
            try
            {
                gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
                DataTable dtTemplates = ogloTemplate.GetAllocatedTemplates(_ProviderID );
                if (dtTemplates != null)
                {
                    cmbTemplates.DataSource = dtTemplates;
                    cmbTemplates.DisplayMember = dtTemplates.Columns["sTemplateName"].ColumnName;
                    cmbTemplates.ValueMember = dtTemplates.Columns["sTemplateName"].ColumnName;
                }
                cmbTemplates.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //DeleteTemplateAllocation frmdelete = new DeleteTemplateAllocation();
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Delete":
                        if (cmbTemplates.SelectedItem == null)
                        {
                            MessageBox.Show("Select a template.  ",_MessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                            cmbTemplates.Focus(); 
                            return ; 
                        }

                        if (dtpStartDate.Value.Date  > dtpEndDate.Value.Date )
                        {
                            MessageBox.Show("End date should be after start date.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpStartDate.Focus();
                            return; 
                        }

                        gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
                        using (DataTable dtAllocations = ogloTemplate.GetTemplateAllocations(_ProviderID, dtpStartDate.Value, dtpEndDate.Value))
                        {
                            if (dtAllocations.Rows.Count > 0)
                            {
                                //gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate(_databaseConnectionString);
                                bool result = ogloTemplate.DeleteTemplateAllocation(_ProviderID, cmbTemplates.SelectedValue.ToString(), dtpStartDate.Value, dtpEndDate.Value);
                                //ogloTemplate.DeleteTemplateAllocation(_ProviderID, cmbTemplates.SelectedValue.ToString(), StartDate.Value, EndDate.Value);
                                if (result == true)
                                {
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.TemplateAllocation, ActivityType.Delete, "Delete Template Allocation ", 0, _ProviderID, _ProviderID, ActivityOutCome.Success);
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("No template associated within selected date range.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dtpStartDate.Focus();
                                return;
                            }
                        }
                        ogloTemplate.Dispose();
                       break;
                     
                    case "Cancel":
                        this.Close();   
                        break;
                }

            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Delete_Click(object sender, EventArgs e)
        {

        }
        //Bug #47154: 00000410 : Scheduling
        //Event added to show tooltip for combo box.
        private void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {
            try
            {
                ComboBox combo = new ComboBox();
                combo = (ComboBox)sender;
                if (combo.Items.Count > 0 && e.Index >= 0)
                {
                    e.DrawBackground();
                    using (SolidBrush br = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(combo.GetItemText(combo.Items[e.Index]).ToString(), e.Font, br, e.Bounds);
                    }

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        if (combo.DroppedDown)
                        {
                            string txt = combo.GetItemText(combo.Items[e.Index]).ToString();
                            if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 20)
                            {
                                if (tooltip_Combobox.GetToolTip(combo) != txt)
                                {
                                    this.tooltip_Combobox.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                                }
                            }
                            else
                            {
                                this.tooltip_Combobox.SetToolTip(combo, "");
                            }
                        }
                        else
                        {
                            this.tooltip_Combobox.Hide(combo);
                        }
                    }
                    else
                    {
                    }
                    e.DrawFocusRectangle();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        //Bug #47154: 00000410 : Scheduling
        //code added to show tooltip for combo box.
        private int getWidthofListItems(string _text, ComboBox combo)
        {
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g != null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }

            return width;
        }
        //Bug #47154: 00000410 : Scheduling
        //code added to show tooltip for combo box.
        private void cmbTemplates_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = new ComboBox();
                combo = (ComboBox)sender;

                if (cmbTemplates.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbTemplates.Items[cmbTemplates.SelectedIndex])["sTemplateName"]), cmbTemplates) >= cmbTemplates.DropDownWidth - 20)
                    {
                        tooltip_Combobox.SetToolTip(cmbTemplates, Convert.ToString(((DataRowView)cmbTemplates.Items[cmbTemplates.SelectedIndex])["sTemplateName"]));
                    }
                    else
                    {
                        this.tooltip_Combobox.Hide(cmbTemplates);
                    }
                }
                else
                {
                    this.tooltip_Combobox.Hide(cmbTemplates);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }
       
        

        }
}