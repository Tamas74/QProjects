using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloSettings;

namespace gloSurescriptSecureMessage
{
    public partial class frmReferralsList : Form
    {

        private gloListControl.gloListControl oListControl;

        private string _DatabaseConnection = "";
        public string DatabaseConnection
        {
            get { return _DatabaseConnection; }
            set { _DatabaseConnection = value; }
        }

        private DataTable _dtRefferal;
        public DataTable dtRefferal
        {
            get { return _dtRefferal; }
            set { _dtRefferal = value; }
        }



        public frmReferralsList()
        {
            InitializeComponent();
        }

     

        #region Referrals

        private void frmReferralsList_Load(object sender, EventArgs e)
        {

       
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}


                oListControl = new gloListControl.gloListControl(DatabaseConnection, gloListControl.gloListControlType.Referrals, true, this.Width,"SecureMessage");
                oListControl.ControlHeader = "Referrals";
                oListControl.SecureMessage = "SecureMessage";
                oListControl.UserID = gloSurescriptSecureMessage.SecureMessageProperties.UserID;

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_refferalsSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.IsFromReport = true;
                //oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                //oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                oListControl.Dock = DockStyle.Fill;

              
                 
                this.Controls.Add(oListControl);

               
                //for (int i = 0; i < cmbPAReferrals.Items.Count; i++)
                //{
                //    cmbPAReferrals.SelectedIndex = i;
                //    oListControl.SelectedItems.Add(Convert.ToInt64(cmbPAReferrals.SelectedValue), cmbPAReferrals.Text);
               
                //}
                //if (cmbPAReferrals.Items.Count > 0)
                //    cmbPAReferrals.SelectedIndex = 0;

               
                oListControl.OpenControl();

               
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                   // onDemographicControl_Leave(sender, e);
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }

        //Select Refferal
        private void oListControl_refferalsSelectedClick(object sender, EventArgs e)
        {
            try
            {
                
               // cmbPAReferrals.DataSource = null;
               // cmbPAReferrals.Items.Clear();
                DataTable dtReff = new DataTable();
                dtRefferal = null;
                DataColumn dcId = new DataColumn("ID");
                DataColumn dcDescription = new DataColumn("Description");
                dtReff.Columns.Add(dcId);
                dtReff.Columns.Add(dcDescription);
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtReff.NewRow();
                        drTemp["ID"] = oListControl.SelectedItems[i].ID;
                        drTemp["Description"] = oListControl.SelectedItems[i].Description;
                        dtReff.Rows.Add(drTemp);
                    }

                    dtRefferal = dtReff;
                }
                else { dtRefferal = new DataTable(); }
                
                
                //cmbPAReferrals.DataSource = dtReff;
                //cmbPAReferrals.ValueMember = dtReff.Columns["ID"].ColumnName;
                //cmbPAReferrals.DisplayMember = dtReff.Columns["Description"].ColumnName;
                //cmbPAReferrals.DrawMode = DrawMode.Normal;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oListControl_ItemClosedClick(sender,e);
                //onDemographicControl_Enter(sender, e);
            }
           // _isRefferalsModified = true;

            // added by sandip dhakane 20100715
           // cmbPAReferrals.Focus();
        }


        //Close list control
        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_refferalsSelectedClick);
                oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
 
            }
            SaveColumnWidth();
            this.Close();
            if (oListControl != null)
            {
                oListControl.Dispose();
                oListControl = null;
            }
           // onDemographicControl_Enter(sender, e);
        }


        public void SaveColumnWidth()
        {

            gloSettings.GeneralSettings ogloSettings = new GeneralSettings(_DatabaseConnection );
            try
            {
                ogloSettings.SaveGridColumnWidth(oListControl .dgListView , ModuleOfGridColumn.SecureReferal, gloSurescriptSecureMessage.SecureMessageProperties .UserID );
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (ogloSettings != null) { ogloSettings.Dispose(); }
            }

        }
        #endregion

       

    }
}
