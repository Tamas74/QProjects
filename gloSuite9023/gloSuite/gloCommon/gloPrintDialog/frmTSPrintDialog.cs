using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace gloPrintDialog
{
    public partial class frmTSPrintDialog : Form
    {
        int currIndex = -1;
        bool isOkClcked = false;
        bool isModifyExtSettings = false;
        String orgPrinterFileName = "";
        String ActualPrinterName = "";

        public bool cancelPirnt = false;
        public String PrinterModule = "DefaultPrinter";

        public String currPrinterName = "";
        public String currPrinterFile = "";
        public String currSize = "";
        public String currTray = "";
        public int NoOfCopies = 1;
        public Boolean isLandscape = false;
        public Boolean isCollete = true;
        public int pageFrom = 1;
        public int pageTo = 1;
        public int duplex = 0;
        public int maxPage = 0;

        public bool bIsShowExtended = false;

        public gloExtendedPropertiesControl ExtendedPropertiesControl = null;
        
        private gloExtendedPrinterSettings _CustomPrinterExtendedSettings = null;
        public gloExtendedPrinterSettings CustomPrinterExtendedSettings
        {
            get
            {
                return _CustomPrinterExtendedSettings;
            }
            set
            {
                if (_CustomPrinterExtendedSettings != null)
                {
                    _CustomPrinterExtendedSettings.Dispose();
                    _CustomPrinterExtendedSettings = null;
                }
                _CustomPrinterExtendedSettings = value;
                _CustomPrinterExtendedSettings.FooterFont = value.FooterFont;
            }
        }

        public frmTSPrintDialog()
        {
            InitializeComponent();
        }

        private void chkCollete_CheckedChanged(object sender, EventArgs e)
        {
            bool flag =chkCollete.Checked;
            pnlCollate_Enabled.Visible = flag;
            pnlCollateDisabled.Visible = !flag;
        }

        private void btnShowExtendedSettings_Click(object sender, EventArgs e)
        {
            isModifyExtSettings = true;
            ExtendedPropertiesControl = new gloExtendedPropertiesControl();
            ExtendedPropertiesControl.Left = grbName.Left;
            pnlMiddlePrinter.Controls.Clear();
            pnlMiddlePrinter.Controls.Add(ExtendedPropertiesControl);
            LoadExtendedSettings();
            if (CustomPrinterExtendedSettings == null)
            {
                CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                setDefaultMargins();
            }
            ExtendedPropertiesControl.SetPrinterParametersExtended(CustomPrinterExtendedSettings);
            btnShowExtendedSettings.Visible = false;
            this.Size = new Size(440, 605);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Refresh();
        }

        private void frmTSPrintDialog_Load(object sender, EventArgs e)
        {
            try
            {
                //this.Size = new Size(440, 283);
                loadPrinters(false);
                rbPrintRange_All.Checked = true;
                //if (gloGlobal.gloTSPrint.NoOfPages > 0)
                //{
                //    rbPrintRange_Pages.Enabled = false;
                //}
                //else
                { 
                    if (maxPage != 0)
                    {
                        numUpDownPageTo.Maximum = maxPage;
                    }
                }
                rbPrintRange_Pages_CheckedChanged(null, null);
                chkCollete.Checked = true;
                //if (bIsShowExtended)  //Commented condition to always show extended setting
                {
                    btnShowExtendedSettings_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void loadPrinters(Boolean isRefresh)
        {
            cmbPrinterName.Items.Clear();
            cmbPrinterName.Items.Add("default");
            if (MyLocalPrinters.printerList == null || isRefresh)
            {
                MyLocalPrinters.reloadPrinterList();
            }
            if (MyLocalPrinters.printerList != null)
            {
                for (int i = 0; i < MyLocalPrinters.printerList.Printer.Length; i++)
                {
                    cmbPrinterName.Items.Add(MyLocalPrinters.printerList.Printer[i].Name);
                }
            }
            setPrinter();
        }

        private void setPrinter()
        {
            try
            {
                String PrinterName = "";
                String PrinterFile = "";
                if (MyLocalPrinters.dictModuleConfig != null)
                {
                    MyLocalPrinters.dictModuleConfig.TryGetValue(PrinterModule + "_PrinterName", out PrinterName);
                    MyLocalPrinters.dictModuleConfig.TryGetValue(PrinterModule + "_SettingsFile", out PrinterFile);
                }
                if (String.IsNullOrWhiteSpace(PrinterName))
                {
                    PrinterName = "default";
                    PrinterFile = "";
                }
                int index = cmbPrinterName.Items.IndexOf(PrinterName);
                if (index == -1)
                {
                    index = 0;
                    PrinterFile = "";
                }
                orgPrinterFileName = PrinterName;
                cmbPrinterName.SelectedIndex = index;
                currPrinterFile = PrinterFile;
                if (isModifyExtSettings==true && ExtendedPropertiesControl != null)
                {
                    LoadExtendedSettings();
                    ExtendedPropertiesControl.SetPrinterParametersExtended(CustomPrinterExtendedSettings);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            
        }

        private void LoadExtendedSettings()
        {
            try
            {
                if (MyLocalPrinters.CustomPrinterExtendedSettings != null)
                {
                    if (CustomPrinterExtendedSettings == null)
                    {
                        CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                    }
                    CustomPrinterExtendedSettings.Copy(MyLocalPrinters.CustomPrinterExtendedSettings);
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(currPrinterFile) && (currPrinterFile != "0") && (MyLocalPrinters.masterConfig != null))
                    {
                        currPrinterFile = MyLocalPrinters.masterConfig.CurrentDefaultPrinter;
                    }
                    if (!String.IsNullOrWhiteSpace(currPrinterFile))
                    {
                        DataSet ds = gloGlobal.gloTSPrint.readPrinterSettingsToDataSet(gloGlobal.gloTSPrint.mappedPath, currPrinterFile);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            gloPrintDialog gpd = new gloPrintDialog();
                            gpd.LoadExtendedSettingsForUser(ds.Tables[0]);
                            if (CustomPrinterExtendedSettings == null)
                            {
                                CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                            }
                            CustomPrinterExtendedSettings.Copy(gpd.CustomPrinterExtendedSettings);
                            if (currPrinterFile == MyLocalPrinters.masterConfig.CurrentDefaultPrinter)
                            {
                                setDefaultMargins();
                            }
                            MyLocalPrinters.CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                            MyLocalPrinters.CustomPrinterExtendedSettings.Copy(CustomPrinterExtendedSettings);
                            gpd.Dispose();
                            gpd = null;

                            ds.Dispose();
                            ds = null;
                            return;
                        }
                        else
                        {
                            if (ds != null)
                            {
                                ds.Dispose();
                                ds = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancelPirnt = true;
            this.Close();
        }

        private void frmTSPrintDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isOkClcked)
            {
                cancelPirnt = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (isModifyExtSettings)
            {
                String validateMsg;
                if (!ExtendedPropertiesControl.IsValidPrinterExtendedParameters(out validateMsg))
                {
                    return;
                }
            }
            if (rbPrintRange_Pages.Checked)
            {
                if (numUpDownPageFrom.Value > numUpDownPageTo.Value)
                {
                    MessageBox.Show("Please enter valid page range", "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    numUpDownPageFrom.Focus();
                    return;
                }

                pageFrom = (int)numUpDownPageFrom.Value;
                pageTo = (int)numUpDownPageTo.Value;
            }
            else
            {
                pageFrom = 0;
                pageTo = 0;
            }
            NoOfCopies = (int)numUpDownNoOfCopies.Value;
            //Orientation
            if (rbOrientation_Landscape.Checked)
            {
                isLandscape = true;
            }
            else
            {
                isLandscape = false;
            }
            try
            {
                duplex = (int)(Duplex)Enum.Parse(typeof(Duplex), cmbDuplex.Text);
            }
            catch 
            {
                duplex = -1;
            }
            isCollete = chkCollete.Checked;
            currSize = (cmbPaperSize.Items.Count > 0) ? cmbPaperSize.Text : "";
            currTray = (cmbTray.Items.Count > 0) ? cmbTray.Text : "";

            currPrinterFile = getCurrentPrinterFile();

            isOkClcked = true;
            this.Close();
        }

        private string getCurrentPrinterFile()
        {
            String fileName = currPrinterFile;
            try
            {
                if ((orgPrinterFileName != currPrinterName) || isModifyExtSettings)
                {
                    if (String.IsNullOrWhiteSpace(ActualPrinterName))
                    {
                        ActualPrinterName = currPrinterName;
                    }
                    if (ExtendedPropertiesControl !=null)
                    {
                        CustomPrinterExtendedSettings = ExtendedPropertiesControl.GetPrinterParametersExtended();
                        if (MyLocalPrinters.CustomPrinterExtendedSettings == null)
                        {
                            MyLocalPrinters.CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                        }
                        MyLocalPrinters.CustomPrinterExtendedSettings.Copy(CustomPrinterExtendedSettings);
                    }
                    if (CustomPrinterExtendedSettings == null)
                    {
                        CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                        setDefaultMargins();
                    }
                    string Newxml = Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloTSPrint.PrinterConfigDirectory, System.Guid.NewGuid().ToString() + ".xml");
                        
                       
                    using (gloPrintDialog oPrintDialog = new gloPrintDialog())
                    {
                        oPrintDialog.RegistryModuleName = PrinterModule;
                        oPrintDialog.CustomPrinterExtendedSettings = CustomPrinterExtendedSettings;

                        //oPrintDialog.DisplayRectangle = oDocumentDisplayRectangle;

                        System.Data.DataTable dtPrinterSettings = new System.Data.DataTable();
                        clsPrinterSettings.CreateSchemadtPrinterSettingsDetails(ref dtPrinterSettings);
                        dtPrinterSettings.TableName = oPrintDialog.RegistryModuleName;
                        oPrintDialog.AddPrinterSettingsToTable(ref dtPrinterSettings);

                        dtPrinterSettings.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                        dtPrinterSettings.Rows[0]["PrinterName"] = ActualPrinterName;
                        dtPrinterSettings.Columns.Add("PrinterSettings", System.Type.GetType("System.String"));
                        dtPrinterSettings.Rows[0]["PrinterSettings"] = "";
                        dtPrinterSettings.Columns.Add("UpdateSettings", System.Type.GetType("System.String"));
                        dtPrinterSettings.Rows[0]["UpdateSettings"] = "1";
                        dtPrinterSettings.WriteXml(Newxml);

                        dtPrinterSettings.Clear();
                        dtPrinterSettings.Dispose();
                        dtPrinterSettings = null;

                        fileName =Path.GetFileName(Newxml);
                    }
                    //Update master config file
                    String PrinterType, SettingFile;
                    if (currPrinterName == "default")
                    {
                        PrinterType = "default";
                        SettingFile = "0";
                    }
                    else
                    {
                        PrinterType = "selected";
                        SettingFile = fileName;
                    }
                    String MasterConfigFilePath = Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloTSPrint.PrinterConfigDirectory, gloGlobal.gloTSPrint.MasterConfig);
                    for (int i = 0; i < MyLocalPrinters.masterConfig.ModulePrinters.Length; i++)
                    {
                        if (MyLocalPrinters.masterConfig.ModulePrinters[i].ModuleName.ToLower() == PrinterModule.ToLower())
                        {
                            MyLocalPrinters.masterConfig.ModulePrinters[i].PrinterName = currPrinterName;
                            MyLocalPrinters.masterConfig.ModulePrinters[i].PrinterType = PrinterType;
                            MyLocalPrinters.masterConfig.ModulePrinters[i].SettingFile = SettingFile;

                            try
                            {
                                MyLocalPrinters.dictModuleConfig[PrinterModule + "_PrinterName"] = currPrinterName;
                                MyLocalPrinters.dictModuleConfig[PrinterModule + "_PrinterType"] = PrinterType;
                                MyLocalPrinters.dictModuleConfig[PrinterModule + "_SettingsFile"] = SettingFile;
                            }
                            catch 
                            {
                            }
                            break;
                        }
                    }
                    gloClinicalQueueGeneral.gloQueueMetadatawriter.UpdateMasterConfigFileData(MyLocalPrinters.masterConfig, MasterConfigFilePath);
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return fileName;
        }

        private void cmbPrinterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currPrinterName = cmbPrinterName.Text;
                currIndex = getSelectedPrinterIndex(currPrinterName);
                LoadPrinterSettings(currIndex);
                if (isModifyExtSettings == false && currPrinterName == "default")
                {
                    CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                    setDefaultMargins();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private int getSelectedPrinterIndex(String strPrinterName,bool provideDefault = true)
        {
            int selIndex = -1;
            int defaultIndex = -1;
            if (MyLocalPrinters.printerList != null)
            {
                for (int i = 0; i < MyLocalPrinters.printerList.Printer.Length; i++)
                {
                    if (strPrinterName == MyLocalPrinters.printerList.Printer[i].Name)
                    {
                        selIndex = i;
                        break;
                    }
                    if (provideDefault)
                    {
                        if (MyLocalPrinters.printerList.Printer[i].IsDefault)
                        {
                            defaultIndex = i;
                        }
                    }
                    
                }
                if (selIndex == -1 && provideDefault == true)
                {
                    selIndex = defaultIndex;
                }
            }
            return selIndex;
        }

        private void LoadPrinterSettings(int printerIndex)
        {
            try
            {
                if (MyLocalPrinters.printerList != null && printerIndex != -1)
                {
                    gloClinicalQueueGeneral.InstalledPrintersPrinter selPrinter = MyLocalPrinters.printerList.Printer[printerIndex];
                    ActualPrinterName = selPrinter.Name;
                    // Check can duplex
                    cmbDuplex.Items.Clear();
                    cmbDuplex.Items.Add(Duplex.Default.ToString());
                    if (selPrinter.CanDuplex)
                    {
                        cmbDuplex.Items.Add(Duplex.Simplex.ToString());
                        cmbDuplex.Items.Add(Duplex.Vertical.ToString());
                        cmbDuplex.Items.Add(Duplex.Horizontal.ToString());
                    }
                    cmbDuplex.SelectedIndex = 0;

                    //Add Paper Size and Tray
                    cmbPaperSize.Items.Clear();
                    cmbTray.Items.Clear();
                    if (selPrinter.SizeTray != null)
                    {
                        for (int i = 0; i < selPrinter.SizeTray.Length; i++)
                        {
                            if (selPrinter.SizeTray[i].Size != null)
                            {
                                for (int k = 0; k < selPrinter.SizeTray[i].Size.Length; k++)
                                {
                                    cmbPaperSize.Items.Add(selPrinter.SizeTray[i].Size[k].Name);
                                }
                            }
                            if (selPrinter.SizeTray[i].Tray != null)
                            {
                                for (int m = 0; m < selPrinter.SizeTray[i].Tray.Length; m++)
                                {
                                    cmbTray.Items.Add(selPrinter.SizeTray[i].Tray[m].Name);
                                }
                            }
                        }
                    }
                    
                    if (cmbPaperSize.Items.Count > 0)
                    {
                        int index = cmbPaperSize.Items.IndexOf(selPrinter.Size);
                        if (index == -1)
                        {
                            index = 0;
                        }
                        cmbPaperSize.SelectedIndex = index;
                    }
                    if (cmbTray.Items.Count > 0)
                    {
                        int index2 = cmbTray.Items.IndexOf(selPrinter.Tray);
                        if (index2 == -1)
                        {
                            index2 = 0;
                        }
                        cmbTray.SelectedIndex = index2;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void cmbPaperSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbPaperSize.Text.ToLower() == "custom")
            //{
            //    pnlCustomSize.Visible = true;
            //}
            //else
            //{
            //    pnlCustomSize.Visible = false;
            //}
        }

        private void btnRefreshPrinters_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnRefreshPrinters.Enabled = false;
                btnOK.Enabled = false;
                cmbPrinterName.Enabled = false;
                Application.DoEvents();
                if (RefreshPrinters() == true)
                {
                    loadPrinters(true);
                    MessageBox.Show("Printer list updated","Printers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }

            finally
            {
                this.Cursor = Cursors.Default;
                btnRefreshPrinters.Enabled = true;
                btnOK.Enabled = true;
                cmbPrinterName.Enabled = true;
                Application.DoEvents();
            }
        }

        public bool RefreshPrinters()
        {
            Boolean refreshed = false;
            try
            {
                String printerConfigDir = Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloTSPrint.PrinterConfigDirectory);
                //Delete existing .ups files
                System.IO.DirectoryInfo di = new DirectoryInfo(printerConfigDir);
                foreach (FileInfo file in di.GetFiles("*.ups"))
                {
                    file.Delete();
                }
                using (var myFile = File.Create(Path.Combine(printerConfigDir, System.Guid.NewGuid().ToString() + ".psg")))
                {
                    try
                    {
                        myFile.Close(); //SLR: it should be closed, if somebody has to delete
                        // interact with myFile here, it will be disposed automatically
                    }
                    catch
                    {
                    }
                }
                bool exist = Directory.EnumerateFiles(printerConfigDir, "*.ups").Any();
                Int16 attempts = 0;
                while (!exist && attempts < 20)
                {
                    attempts++;
                    System.Threading.Thread.Sleep(1000);
                    Application.DoEvents();
                    exist = Directory.EnumerateFiles(printerConfigDir, "*.ups").Any();
                }
                if (exist == true)
                {
                    refreshed = true;
                }
                else
                {
                    MessageBox.Show("Unable to update printer list, Please try after some time.", "Printers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refreshed = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            return refreshed;
        }

        private void rbPrintRange_Pages_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrintRange_Pages.Checked)
            {
                numUpDownPageFrom.Value = 1;
                numUpDownPageTo.Value = 1;
                numUpDownPageFrom.Enabled = true;
                numUpDownPageTo.Enabled = true;
            }
            else
            {
                numUpDownPageFrom.Enabled = false;
                numUpDownPageTo.Enabled = false;
            }
        }

        private void setDefaultMargins()
        {
            if (PrinterModule == "DefaultPrinter")
            {
                if (CustomPrinterExtendedSettings != null)
                {
                    CustomPrinterExtendedSettings.CurrentPageSize = gloExtendedPrinterSettings.PageSize.ActualPageSize;
                    if (CustomPrinterExtendedSettings.PrinterMarginsTop == 0)
                    {
                        CustomPrinterExtendedSettings.PrinterMarginsTop = -10;
                    }
                    if (CustomPrinterExtendedSettings.PrinterMarginsLeft == 0)
                    {
                        CustomPrinterExtendedSettings.PrinterMarginsLeft = -10;
                    }
                }
            }
        }

        private void frmTSPrintDialog_Shown(object sender, EventArgs e)
        {
            try
            {
                btnOK.Focus();
            }
            catch 
            {
            }
        }
    }
}
