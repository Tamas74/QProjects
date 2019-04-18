using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing; 

namespace gloPrintDialog
{
    public partial class gloPrinterSelector : Form
    {
        //Public variables 
        public string ConnectionString { get; set; }
        public string ModuleName { get; set; }
        public bool  ModifyPrinter { get; set; }
        public bool CancelClicked { get; set; }
        public DataTable SelectedPrinter { get; set; }
        public string  SetPrinter { get; set; }

        //Private variables
        private DataTable dtPrinters;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public gloPrinterSelector()
        {
            InitializeComponent();
        }

        private void gloPrinterSelector_Load(object sender, EventArgs e)
        {
            PrinterSettings settings = new PrinterSettings();
            CancelClicked = false;
            

            try
            {
                dtPrinters = clsPrinterSettings.LoadUserPrinters(ConnectionString, System.Convert.ToInt64(appSettings["UserID"]));

                cmbPrinterList.DisplayMember = "PrinterName";
                cmbPrinterList.ValueMember = "PrinterSettingsID";
                cmbPrinterList.DataSource = dtPrinters;

                if (SetPrinter == "")
                {
                    cmbPrinterList.Text = settings.PrinterName;
                }
                else 
                {
                    cmbPrinterList.Text = SetPrinter;
                }

                if (settings != null)
                {
                    settings = null;
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
        }

       



        private void btnOK_Click(object sender, EventArgs e)
        {
            

            try
            {
                SelectedPrinter = clsPrinterSettings.GetPrinterSettings(ConnectionString, ModuleName, Convert.ToInt64(cmbPrinterList.SelectedValue), System.Convert.ToInt64(appSettings["UserID"]),  cmbPrinterList.Text);
                this.Close();
            }
            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
        }

        private void gloPrinterSelector_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (dtPrinters != null)
                {
                    dtPrinters.Dispose();
                    dtPrinters = null;
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
        }

        private void btnModifyPrinter_Click(object sender, EventArgs e)
        {
            
            try
            {
                SetPrinter = cmbPrinterList.Text;
                SelectedPrinter = clsPrinterSettings.GetPrinterSettings(ConnectionString, ModuleName, Convert.ToInt64(cmbPrinterList.SelectedValue), System.Convert.ToInt64(appSettings["UserID"]),  cmbPrinterList.Text  );
                ModifyPrinter = true;
                this.Close();
            }
            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CancelClicked = true;
                this.Close();
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
        }


        private bool RefreshPrinters()
        {
            bool ReloadPrinters = false;

            PrinterSettings.StringCollection ps = PrinterSettings.InstalledPrinters;
            System.Drawing.Printing.PrintDocument pdPrinterSettings = null;
            DataTable dtRefreshPrinters = null;
            
            gloAuditTrail.MachineDetails.MachineInfo remoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails();
            gloAuditTrail.MachineDetails.MachineInfo localMachine = gloAuditTrail.MachineDetails.LocalMachineDetails();

            int ind = 0;
            PrinterSettings pt=null;
            int intPrintersAdded = 0;
            int intPrintersDeleted = 0;

            try
            {

                string SessionID = System.Diagnostics.Process.GetCurrentProcess().SessionId.ToString();

                dtRefreshPrinters = new DataTable();
                pdPrinterSettings = new System.Drawing.Printing.PrintDocument();
                pt = new PrinterSettings();

                dtRefreshPrinters.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                dtRefreshPrinters.Columns.Add("nUserID", System.Type.GetType("System.Int64"));
                dtRefreshPrinters.Columns.Add("MachineName", System.Type.GetType("System.String"));
                dtRefreshPrinters.Columns.Add("PrinterSettings", System.Type.GetType("System.String"));
                dtRefreshPrinters.Columns.Add("LocalMachineName", System.Type.GetType("System.String"));
                dtRefreshPrinters.Columns.Add("SessionID", System.Type.GetType("System.String"));

                //Add new printers
                foreach (string str in ps)
                {
                    ind = cmbPrinterList.FindString(str);
                    if (ind == -1)
                    {
                        pdPrinterSettings.PrinterSettings.PrinterName = str;
                        dtRefreshPrinters.Rows.Add(str, System.Convert.ToInt64(appSettings["UserID"]), remoteMachine.MachineName, clsPrinterSettings.PrinterSettingsToString(pdPrinterSettings.PrinterSettings), localMachine.MachineName, SessionID);
                        intPrintersAdded++;
                    }
                }

                //Delete invalid printers
                for (int intCount = 0; intCount <= dtPrinters.Rows.Count-1; intCount++)
                {
                    pt.PrinterName = dtPrinters.Rows[intCount]["PrinterName"].ToString();

                    if (pt.IsValid == false)
                    {
                        dtRefreshPrinters.Rows.Add(dtPrinters.Rows[intCount]["PrinterName"].ToString(), System.Convert.ToInt64(appSettings["UserID"]), remoteMachine.MachineName, "DELETED", localMachine.MachineName, SessionID);
                        intPrintersDeleted++;
                    }
                }

                if (dtRefreshPrinters != null)
                {
                    if (intPrintersAdded == 0 && intPrintersDeleted == 0)
                    {
                        ReloadPrinters = false;
                        MessageBox.Show("No new printers found.", "Printers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        InsertPrinterToDB(dtRefreshPrinters);
                        ReloadPrinters = true;
                        MessageBox.Show(intPrintersAdded.ToString()  + " new printer(s) found and " +  intPrintersDeleted.ToString()  + " invalid printer(s) deleted.", "Printers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                return ReloadPrinters;
            }

            catch (Exception ex)
            {   
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return false;
            }


            finally
            {

                ps = null;

                if (pt != null)
                {
                    pt = null;
                }

                if (dtRefreshPrinters != null)
                {
                    dtRefreshPrinters.Dispose();
                    dtRefreshPrinters = null;
                }

                if (pdPrinterSettings != null)
                {
                    pdPrinterSettings.Dispose();
                    pdPrinterSettings = null;
                }

               
            }
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; 
                SetPrinter = cmbPrinterList.Text;

                if (RefreshPrinters() == true)
                {
                    dtPrinters = clsPrinterSettings.LoadUserPrinters(ConnectionString, System.Convert.ToInt64(appSettings["UserID"]));

                    cmbPrinterList.DisplayMember = "PrinterName";
                    cmbPrinterList.ValueMember = "PrinterSettingsID";
                    cmbPrinterList.DataSource = dtPrinters;

                    cmbPrinterList.Text = SetPrinter;
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        public void InsertPrinterToDB(DataTable dtPrinters)
        {
            //This function is called at the startup and will migrate the printer extended settings from the registry and save to the database.
            Int64 UserID = System.Convert.ToInt64(appSettings["UserID"]);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            DataTable dtPrinterSettingsDetails = new DataTable();
            

            try
            {
                clsPrinterSettings.CreateSchemadtPrinterSettingsDetails(ref dtPrinterSettingsDetails);


                //Save to Database
                oDB.Connect(false);
                oPara = new gloDatabaseLayer.DBParameters();
                oPara.Add("@tvpPrinterNames", dtPrinters, ParameterDirection.Input, SqlDbType.Structured);
                oPara.Add("@tvpPrinterSettingsDetails", dtPrinterSettingsDetails, ParameterDirection.Input, SqlDbType.Structured);

                oDB.Execute("gsp_InsertPrinters", oPara);
                oDB.Disconnect();

            }

            catch (Exception ex)
            {
                throw ex;


            }
            finally
            {
                if (oPara != null)
                {
                    oPara.Clear();
                    oPara.Dispose();
                    oPara = null;
                }

                if ((oDB != null))
                {
                    oDB.Dispose();
                    oDB = null;
                }


                if ((dtPrinterSettingsDetails != null))
                {
                    dtPrinterSettingsDetails.Dispose();
                    dtPrinterSettingsDetails = null;
                }


                if ((dtPrinters != null))
                {
                    dtPrinters.Dispose();
                    dtPrinters = null;
                }

            }

        }

        private void gloPrinterSelector_Shown(object sender, EventArgs e)
        {
            //added for setting default printer changes made for incident 67769
            if (gloGlobal.gloTSPrint._OldSharedPrinterName.Trim() == "")
            {
                gloGlobal.gloTSPrint._OldSharedPrinterName = cmbPrinterList.Text;
            }
            else
            {

                cmbPrinterList.Text = gloGlobal.gloTSPrint._OldSharedPrinterName;

            }
        }
        
    
    }
}
