using System;
using System.Text;
using System.Resources;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Printing;
using System.IO;
using System.Data;
using gloAuditTrail;

/// <summary>
/// Customized print dialog control which provides the following features:
/// - Topmost attribute
/// - Persistent Window Location
/// - Background printing
/// - Show/Hides print status dialog
/// - Extended User Control
/// </summary>
namespace gloPrintDialog
{
    public sealed class gloPrintDialog : IDisposable
    {
        public string TITLE = "Print";

        public string ConnectionString { get; set; }

        public Boolean ShowPrinterProfileDialog { get; set; }
        private Boolean ModifyPrinter;
        private Boolean PrinterSelectorCanceled;
        private string PrinterSelectorSetPrinter = "";

        /// <summary>
        /// Default PrintDlg width
        /// </summary>
        private const int DEFAULT_WIDTH = 510;
        /// <summary>
        /// Default PrintDlg height
        /// </summary>
        private const int DEFAULT_HEIGHT = 315;

        /// <summary>
        /// Padding space sdded in the bottom of this dialog
        /// </summary>
        private int nHeightOfUserControl = 80;

        public long ClinicId;
        private string _messageBoxCaption = "";
        private Int64 _UserID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public bool bUseDefaultPrinter = false;
        public bool bEnableLocalPrinter = gloGlobal.gloTSPrint.isCopyPrint;
        public bool bGetSettingsFromDB = true;
        public static DataSet dsPrinters = new DataSet();
        public Int64 PrinterSettingsID { get; set; }
        public Int64 PrinterSettingsDetailsID { get; set; }

        #region Defines the signature of hook procedures that can be called by the PrintDlg
        /// <summary>
        /// Defines the shape of hook procedures that can be called by the PrintDlg
        /// </summary>
        internal delegate IntPtr PrintHookProc(IntPtr hWnd, UInt16 msg, Int32 wParam, Int32 lParam);

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto)]
        static extern bool PrintDlg([In, Out] PRINTDLG lppd);

        /// <summary>
        /// Values that can be placed in the PRINTDLG structure, we don't use all of them
        /// </summary>
        internal class PrintFlag
        {
            public const Int32 PD_ALLPAGES = 0x00000000;
            public const Int32 PD_SELECTION = 0x00000001;
            public const Int32 PD_PAGENUMS = 0x00000002;
            public const Int32 PD_NOSELECTION = 0x00000004;
            public const Int32 PD_NOPAGENUMS = 0x00000008;
            public const Int32 PD_COLLATE = 0x00000010;
            public const Int32 PD_PRINTTOFILE = 0x00000020;
            public const Int32 PD_PRINTSETUP = 0x00000040;
            public const Int32 PD_NOWARNING = 0x00000080;
            public const Int32 PD_RETURNDC = 0x00000100;
            public const Int32 PD_RETURNIC = 0x00000200;
            public const Int32 PD_RETURNDEFAULT = 0x00000400;
            public const Int32 PD_SHOWHELP = 0x00000800;
            public const Int32 PD_ENABLEPRINTHOOK = 0x00001000;
            public const Int32 PD_ENABLESETUPHOOK = 0x00002000;
            public const Int32 PD_ENABLEPRINTTEMPLATE = 0x00004000;
            public const Int32 PD_ENABLESETUPTEMPLATE = 0x00008000;
            public const Int32 PD_ENABLEPRINTTEMPLATEHANDLE = 0x00010000;
            public const Int32 PD_ENABLESETUPTEMPLATEHANDLE = 0x00020000;
            public const Int32 PD_USEDEVMODECOPIES = 0x00040000;
            public const Int32 PD_USEDEVMODECOPIESANDCOLLATE = 0x00040000;
            public const Int32 PD_DISABLEPRINTTOFILE = 0x00080000;
            public const Int32 PD_HIDEPRINTTOFILE = 0x00100000;
            public const Int32 PD_NONETWORKBUTTON = 0x00200000;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        [System.Runtime.InteropServices.ComVisible(false)]
        internal class PRINTPAGERANGE
        {
            public Int32 nFromPage = 0;
            public Int32 nToPage = 0;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        [System.Runtime.InteropServices.ComVisible(false)]
        internal class PRINTDLG
        {
            public Int32 lStructSize;
            public IntPtr hwndOwner;
            public IntPtr hDevMode;
            public IntPtr hDevNames;
            public IntPtr hDC = IntPtr.Zero;
            public Int32 Flags;
            public Int16 FromPage = 0;
            public Int16 ToPage = 0;
            public Int16 MinPage = 0;
            public Int16 MaxPage = 0;
            public Int16 Copies = 0;
            public IntPtr hInstance = IntPtr.Zero;
            public IntPtr lCustData = IntPtr.Zero;
            public PrintHookProc lpfnPrintHook;
            public IntPtr lpfnSetupHook = IntPtr.Zero;
            public IntPtr lpPrintTemplateName = IntPtr.Zero;
            public IntPtr lpSetupTemplateName = IntPtr.Zero;
            public IntPtr hPrintTemplate = IntPtr.Zero;
            public IntPtr hSetupTemplate = IntPtr.Zero;
        }

        // The "control ID" of the content window inside the PrintDlg

        private const int _CONTENT_PANEL_ID = 0x0430;

        // OK button
        private const int IDOK = 0x0001;

        // Cancel button
        private const int ID_CANCEL = 0x0002;

        // Page Range Group
        private const int ID_PAGERANGE = 0x0433;

        // Collate checkbox
        private const int ID_COLLATE = 0x411;

        // Property button
        private const int ID_PROPERTY = 0x0401;

        // A constant that determines the spacing between panels inside the PrintDlg
        private const int _PANEL_GAP_FACTOR = 3;

        // user-supplied control that gets placed inside the PrintDlg
        public gloExtendedPropertiesControl ExtendedPropertiesControl = null;

        // unmanaged memory buffer that holds the Win32 dialog template
        //		private IntPtr _ipTemplate = IntPtr.Zero;

        // resource object list
        //	private ArrayList  m_oResourceList;

        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        //OpenForBatchPrint parameter added for checking whether it was open for batchprinting 
        public gloPrintDialog(bool OpenForBatchPrint = false, string strRegModule = "", string strModuleName = "")
        {
            //this.m_oResourceList = new ArrayList();
            //	this.m_oDefaultArea = new Rectangle(500, 70, DEFAULT_WIDTH, DEFAULT_HEIGHT);
            //if (_pPrinterSettings == null)
            //{
            //    try
            //    {
            //        _pPrinterSettings = new PrinterSettings();
            //    }
            //    catch
            //    {
            //    }
            //}
            RegistryModuleName = strRegModule;
            ModuleName = strModuleName;

            if (!string.IsNullOrEmpty(RegistryModuleName))
            {
                //Aniket: Commented
                GetPrinter();
                LoadExtendedSettings();
            }
            else
            {
                this.CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
            }
            GetExtPanel(OpenForBatchPrint);



            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    this.ClinicId = System.Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    this.ClinicId = 1;
                }
            }
            else
            {
                this.ClinicId = 1;
            }

            #endregion //" Retrieve ClinicID from AppSettings "

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    this._messageBoxCaption = System.Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    this._messageBoxCaption = "";
                }
            }
            else
            {
                this._messageBoxCaption = "";
            }

            #endregion // " Retrieve MessageBoxCaption from AppSettings "

            #region " Retrieve UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    this._UserID = System.Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                this._UserID = 0;
            }

            #endregion // " Retrieve UserID from appSettings "

            #region " Retrieve Default Printer setting from AppSettings "

            if (appSettings["DefaultPrinter"] != null)
            {
                if (appSettings["DefaultPrinter"] != "")
                {
                    this.bUseDefaultPrinter = System.Convert.ToBoolean(appSettings["DefaultPrinter"]);
                }
            }

            #endregion //" Retrieve Default Printer setting from AppSettings "
        }

        /// <summary>
        /// Copy constructor.
        /// given print dialog properties are copied to
        /// this new print dialog.
        /// </summary>
        /// <param name="oPrintDlg"></param>
        public gloPrintDialog(gloPrintDialog oPrintDlg)
            : this()
        {
            this.ShowNetwork = oPrintDlg.ShowNetwork;
            this.TopMost = oPrintDlg.TopMost;
            this.AllowPrintToFile = oPrintDlg.AllowPrintToFile;
            this.ShowHelp = oPrintDlg.ShowHelp;
            this.Parent = oPrintDlg.Parent;
            this.PrinterSettings = oPrintDlg.PrinterSettings;
            this.AllowSelection = oPrintDlg.AllowSelection;
            this.AllowSomePages = oPrintDlg.AllowSomePages;
            this.DefaultArea = oPrintDlg.DefaultArea;

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    this.ClinicId = System.Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    this.ClinicId = 1;
                }
            }
            else
            {
                this.ClinicId = 1;
            }

            #endregion //" Retrieve ClinicID from AppSettings "

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    this._messageBoxCaption = System.Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    this._messageBoxCaption = "";
                }
            }
            else
            {
                this._messageBoxCaption = "";
            }

            #endregion // " Retrieve MessageBoxCaption from AppSettings "

            #region " Retrieve UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    this._UserID = System.Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                this._UserID = 0;
            }

            #endregion // " Retrieve UserID from appSettings "
        }

        /// <summary>
        /// The finalizer will release the unmanaged memory.
        /// </summary>
        ~gloPrintDialog()
        {
            Dispose(false);
        }


        #region "PrintDialog extension properties"

        /// <summary>
        /// Return this print dialog window handle.
        /// </summary>
        public IntPtr Handle { get; set; }



        /// <summary>
        /// Returns this dialogs parent window handle.
        /// </summary>
        public IntPtr Parent { get; set; }


        /// <summary>
        /// Printer  Setting
        /// </summary>
        private static PrinterSettings _pPrinterSettings = null; 

        public PrinterSettings PrinterSettings { get; set; }
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


        /// <summary>
        /// Hides/Show the Print to File check box.
        /// </summary>
        public bool AllowPrintToFile { get; set; }



        /// <summary>
        /// [page setting]
        /// If this flag is set, the Pages radio button is selected.  
        /// </summary>
        public bool AllowSomePages { get; set; }



        /// <summary>
        /// [... From PrintPage ... To PrintPage] 
        /// If this flag is set, From/To page edit box is enabled.  
        /// </summary>
        public bool AllowSelection { get; set; }



        /// <summary>
        /// Causes the dialog box to display the Help button. 
        /// The hwndOwner member must specify the window to receive the 
        /// HELPMSGSTRING registered messages that the dialog box sends when 
        /// the user clicks the Help button.
        /// </summary>
        public bool ShowHelp { get; set; }



        /// <summary>
        /// Hides/Show and disables the Network button.
        /// </summary>
        public bool ShowNetwork { get; set; }



        public bool TopMost { get; set; }


        public Rectangle DefaultArea { get; set; }

        #endregion



        /// <summary>
        /// Displays print dialog.
        /// </summary>
        /// <returns>DialogResult.OK or DialogResult.Cancel value.</returns>
        public DialogResult ShowDialog(IWin32Window iOwner = null)
        {
            while (true)
            {

                DataTable dtSelectedPrinter = null;
                DialogResult nRet = DialogResult.Cancel;
                ModifyPrinter = false;

                //Aniket: Shifted here
                if (IsSaveExtendedSettings != true)
                {
                    this.CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                }



                //Default Printer ON

                if ((bUseDefaultPrinter || bEnableLocalPrinter) && (IsSaveExtendedSettings != true))
                {
                    if (this.PrinterSettings == null)
                    {
                        if (!bEnableLocalPrinter)
                        {
                            GetPrinter();
                        }
                        //if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {

                            if (this.PrinterSettings == null)
                            {

                                //this.PrinterSettings = new PrinterSettings(); //SLR: let us not create everytime
                                if (_pPrinterSettings == null)
                                {
                                    try
                                    {
                                        _pPrinterSettings = new PrinterSettings();
                                    }
                                    catch
                                    {
                                    }
                                }
                                this.PrinterSettings = _pPrinterSettings;
                            }
                        }
                    }

                    //Added to get fit to page if default printer is ON
                    if (String.Compare(this.ModuleName, "PrintWorkersCompForms", true) == 0 ||
                       String.Compare(this.ModuleName, "PrintDMSDocuments", true) == 0 ||
                       String.Compare(this.ModuleName, "PrintRCMDocuments", true) == 0)
                    {
                        //Forced to FitToPage in case default printing is ON for any of above module.
                        this.CustomPrinterExtendedSettings.CurrentPageSize = gloExtendedPrinterSettings.PageSize.FitToPage;
                        this.CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin = 0;
                        this.CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin = 0;

                        //Aniket: This needs to be set
                        this.CustomPrinterExtendedSettings.IsActualLandscape = false;
                        this.CustomPrinterExtendedSettings.IsActualMultiPage = false;
                        this.CustomPrinterExtendedSettings.IsHorizontalFlow = true;
                    }
                    else
                    {
                        if (IsSaveExtendedSettings != true)
                        {
                            if (PrinterSettingsDetailsID != 0)
                            {
                                dtSelectedPrinter = clsPrinterSettings.GetPrinterSettings(this.ConnectionString, PrinterSettingsDetailsID);

                                if (dtSelectedPrinter.Rows.Count > 0)
                                {
                                    this.PrinterSettings = clsPrinterSettings.StringToPrinterSettings(dtSelectedPrinter.Rows[0]["PrinterSettings"].ToString());
                                }
                                else
                                {
                                    dtSelectedPrinter = clsPrinterSettings.GetPrinterSettings(this.ConnectionString, this.RegistryModuleName, 0, this._UserID, PrinterSettings.PrinterName);
                                }
                            }
                            else
                            {
                                //Use Set by whatever in dialog or user last choice
                                dtSelectedPrinter = clsPrinterSettings.GetPrinterSettings(this.ConnectionString, this.RegistryModuleName, 0, this._UserID, PrinterSettings.PrinterName);
                            }

                            if (PrinterSettingsDetailsID == 0)
                            {
                                if (dtSelectedPrinter.Rows.Count > 0)
                                {
                                    PrinterSettingsID = Convert.ToInt64(dtSelectedPrinter.Rows[0]["PrinterSettingsID"]);
                                    PrinterSettingsDetailsID = Convert.ToInt64(dtSelectedPrinter.Rows[0]["PrinterSettingsDetailsID"]);
                                }
                            }
                            //this.PrinterSettings = clsPrinterSettings.StringToPrinterSettings(dtSelectedPrinter.Rows[0]["PrinterSettings"].ToString());
                            LoadExtendedSettingsForUser(dtSelectedPrinter);
                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {
                                this.ExtendedPropertiesControl.SetPrinterParametersExtended(CustomPrinterExtendedSettings);
                            }
                        }
                    }
                    return DialogResult.OK;
                }
                else
                {
                    if (ShowPrinterProfileDialog == true)
                    {
                        //Display the Printer Profiles Screen
                        gloPrinterSelector frmPrinterSelector = new gloPrinterSelector();
                        frmPrinterSelector.ModuleName = this.RegistryModuleName;
                        frmPrinterSelector.ConnectionString = this.ConnectionString;
                        frmPrinterSelector.SetPrinter = PrinterSelectorSetPrinter;

                        frmPrinterSelector.ShowDialog(iOwner);
                        dtSelectedPrinter = frmPrinterSelector.SelectedPrinter;

                        if (frmPrinterSelector.ModifyPrinter == true)
                        {
                            PrinterSelectorSetPrinter = frmPrinterSelector.SetPrinter;
                            ModifyPrinter = true;
                        }

                        else if (frmPrinterSelector.CancelClicked == true)
                        {
                            PrinterSelectorCanceled = true;
                            PrinterSettingsID = -1;
                            PrinterSettingsDetailsID = -1;
                        }



                        frmPrinterSelector.Dispose();
                        frmPrinterSelector = null;

                        if (dtSelectedPrinter != null && dtSelectedPrinter.Rows.Count > 0)
                        {
                            PrinterSettingsID = Convert.ToInt64(dtSelectedPrinter.Rows[0]["PrinterSettingsID"]);
                            PrinterSettingsDetailsID = Convert.ToInt64(dtSelectedPrinter.Rows[0]["PrinterSettingsDetailsID"]);

                            int topage = 0;
                            if (this.PrinterSettings != null)
                            {
                                topage = this.PrinterSettings.ToPage;
                            }


                            //Aniket: If the printer is redirected and the name has changed, the get new printer settings
                            if (dtSelectedPrinter.Rows[0]["PrinterSettings"].ToString() != "DELETED")
                            {
                                this.PrinterSettings = clsPrinterSettings.StringToPrinterSettings(dtSelectedPrinter.Rows[0]["PrinterSettings"].ToString());
                            }
                            else
                            {
                                this.PrinterSettings.PrinterName = dtSelectedPrinter.Rows[0]["PrinterName"].ToString();
                            }

                            if (this.PrinterSettings != null)
                            {

                                this.PrinterSettings.ToPage = topage;
                                if (topage < 1)
                                {
                                    this.PrinterSettings.FromPage = 0;
                                }
                                else
                                {
                                    this.PrinterSettings.FromPage = 1;
                                }
                            }
                            LoadExtendedSettingsForUser(dtSelectedPrinter);
                            if (String.Compare(this.ModuleName, "PrintClinicalCharts", true) == 0 ||
                         String.Compare(this.ModuleName, "ClinicalCharts", true) == 0)
                            {
                                this.CustomPrinterExtendedSettings.IsShowProgress = false;
                                this.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                if (!gloGlobal.gloTSPrint.isCopyPrint)
                                {

                                    this.ExtendedPropertiesControl.DisablePrintMethod(false);
                                }
                            }

                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {

                                this.ExtendedPropertiesControl.SetPrinterParametersExtended(CustomPrinterExtendedSettings);
                            }

                            nRet = DialogResult.OK;
                        }
                    }

                    else
                    {

                        //Aniket: Commented
                        if (String.Compare(this.ModuleName, "PrintWorkersCompForms", true) == 0 ||
                         String.Compare(this.ModuleName, "PrintDMSDocuments", true) == 0 ||
                         String.Compare(this.ModuleName, "PrintRCMDocuments", true) == 0)
                        {
                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {

                                this.ExtendedPropertiesControl.DisableActualSizeControlsOnDlg();
                            }
                        }
                    }
                }

                if (iOwner != null)
                {
                    this.Parent = iOwner.Handle;
                }

                //Aniket: Commented
                if (ShowPrinterProfileDialog == false || ModifyPrinter == true)
                {
                    GetExtPanel();
                }


                //Aniket: Shifted above
                //if (IsSaveExtendedSettings != true)
                //{
                //    this.CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                //}

                if (this.PrinterSettings == null)
                {
                    GetPrinter();
                    if (this.PrinterSettings == null)
                    {
                        this.PrinterSettings = new PrinterSettings();
                    }
                }

                PrinterSettings oSettings = this.PrinterSettings;

                //Aniket: Commented
                if (ShowPrinterProfileDialog == false || ModifyPrinter == true)
                {
                    nRet = InvokePrintDlg(ref oSettings);
                }



                if (nRet == DialogResult.OK)
                {
                    this.PrinterSettings = oSettings;

                    //Aniket: Commented. Now saved in the database
                    if (IsSaveExtendedSettings != true)
                    {
                        SetPrinter();
                    }
                }
                else
                {
                    if (IsSaveExtendedSettings == true)
                    {
                        this.PrinterSettings = null;
                    }
                }

                if (dtSelectedPrinter != null)
                {
                    dtSelectedPrinter.Dispose();
                    dtSelectedPrinter = null;
                }

                if (ShowPrinterProfileDialog == false)
                {
                    return nRet;
                }
                if ((ShowPrinterProfileDialog == true && nRet == DialogResult.OK) || PrinterSelectorCanceled == true)
                {
                    return nRet;
                }

            }
        }

        public static void SaveSelectedSettings(ref gloExtendedPrinterSettings objDocumentExtendedSetting, string documentprinter, PrinterSettings oDocumentPrinterSettings, Rectangle oDocumentDisplayRectangle, bool bDefaultPrinter = false)
        {
            try
            {
                if (bDefaultPrinter)
                {
                    DeleteRegistryKey(documentprinter);
                    if (gloGlobal.gloTSPrint.isTSPrintServiceCall)
                    {
                        System.Data.DataTable dtPrinterSettings = new System.Data.DataTable();
                        dtPrinterSettings.TableName = documentprinter;
                        dtPrinterSettings.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                        dtPrinterSettings.Rows.Add();
                        dtPrinterSettings.Rows[0]["PrinterName"] = "default";
                        dsPrinters.Tables.Add(dtPrinterSettings);
                    }
                }
                else
                {
                    if (objDocumentExtendedSetting != null)
                    {

                        using (gloPrintDialog oPrintDialog = new gloPrintDialog())
                        {
                            //oPrintDialog.RegistryModuleName = gloClinicalQueueGeneral.clsClinicalQueuePrint.ModulePrintType.Documentprinter.ToString();
                            //string RegistryPath = @"Software\gloEMR\gloPrintDialog" + @"\" + gloClinicalQueueGeneral.clsClinicalQueuePrint.ModulePrintType.Documentprinter.ToString();
                            oPrintDialog.RegistryModuleName = documentprinter;
                            oPrintDialog.PrinterSettings = oDocumentPrinterSettings;
                            if (oPrintDialog.PrinterSettings == null)
                            {
                                oPrintDialog.PrinterSettings = new PrinterSettings();
                            }
                            oPrintDialog.CustomPrinterExtendedSettings = objDocumentExtendedSetting;
                            oPrintDialog.bGetSettingsFromDB = false;
                            oPrintDialog.DisplayRectangle = oDocumentDisplayRectangle;
                            oPrintDialog.IsSaveExtendedSettings = true;
                            oPrintDialog.SaveExtendedSettings();
                            oPrintDialog.SetPrinter();
                        }

                    }
                }
                if (objDocumentExtendedSetting != null)
                {
                    objDocumentExtendedSetting.Dispose();
                    objDocumentExtendedSetting = null;
                }
            }
            catch( Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Information");
            }
        }

        public static Boolean generateCurrentDefaultPrinterFile(String fileName)
        {
            Boolean result = false;
            try
            {
                using (gloPrintDialog oPrintDialog = new gloPrintDialog())
                {
                    oPrintDialog.RegistryModuleName = "CurrentDefault";
                    oPrintDialog.PrinterSettings = new PrinterSettings();
                    oPrintDialog.CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                    //oPrintDialog.DisplayRectangle = oDocumentDisplayRectangle;

                    System.Data.DataTable dtPrinterSettings = new System.Data.DataTable();
                    clsPrinterSettings.CreateSchemadtPrinterSettingsDetails(ref dtPrinterSettings);
                    dtPrinterSettings.TableName = oPrintDialog.RegistryModuleName;
                    oPrintDialog.SaveGraphicsBound();
                    oPrintDialog.AddPrinterSettingsToTable(ref dtPrinterSettings);
                    dtPrinterSettings.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                    dtPrinterSettings.Rows[0]["PrinterName"] = oPrintDialog.PrinterSettings.PrinterName;
                    dtPrinterSettings.Columns.Add("PrinterSettings", System.Type.GetType("System.String"));
                    dtPrinterSettings.Rows[0]["PrinterSettings"] = clsPrinterSettings.PrinterSettingsToString(oPrintDialog.PrinterSettings);

                    dtPrinterSettings.WriteXml(fileName);
                }
                result = true;
            }
            catch
            {
               
            }
            return result;
        }

        public static string ReadSelectedSettings(string sRegistryModuleName, out string sPrinterName, ref gloExtendedPrinterSettings objExtendedPrinterSetting, ref PrinterSettings thisPrinter, ref Rectangle displayRect, string strDialogTitle, bool disallowSettings = false)
        {
            string resultSetting = string.Empty;
            sPrinterName = string.Empty;

            try
            {

                using (gloPrintDialog ClaimPrintDialog = new gloPrintDialog(false))
                {
                    ClaimPrintDialog.bGetSettingsFromDB = false;
                    ClaimPrintDialog.RegistryModuleName = sRegistryModuleName;
                    ClaimPrintDialog.IsSaveExtendedSettings = true;

                    if (thisPrinter == null)
                    {
                        ClaimPrintDialog.GetPrinter();
                        thisPrinter = ClaimPrintDialog.PrinterSettings;
                    }
                    else
                    {
                        ClaimPrintDialog.PrinterSettings = thisPrinter;
                    }
                    ClaimPrintDialog.CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                    if (objExtendedPrinterSetting != null)
                    {
                        ClaimPrintDialog.CustomPrinterExtendedSettings.Copy(objExtendedPrinterSetting);
                    }
                    else
                    {
                        ClaimPrintDialog.LoadExtendedSettings();
                        objExtendedPrinterSetting = new gloExtendedPrinterSettings();
                        objExtendedPrinterSetting.Copy(ClaimPrintDialog.CustomPrinterExtendedSettings);
                    }

                    ClaimPrintDialog.DisplayRectangle = displayRect;

                    //SLR: This is taken care as part of gloPrintprogresscontroller when running service.
                    //ClaimPrintDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                    //ClaimPrintDialog.CustomPrinterExtendedSettings.IsShowProgress = false;
                    //ClaimPrintDialog.ExtendedPropertiesControl.DisablePrintMethod();
                    ClaimPrintDialog.TITLE = strDialogTitle;
                    ClaimPrintDialog.TopMost = true;
                    if (disallowSettings)
                    {
                        //Forced to FitToPage in case default printing is ON for any of above module.
                        ClaimPrintDialog.CustomPrinterExtendedSettings.CurrentPageSize = gloExtendedPrinterSettings.PageSize.FitToPage;
                        ClaimPrintDialog.CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin = 0;
                        ClaimPrintDialog.CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin = 0;

                        //Aniket: This needs to be set
                        ClaimPrintDialog.CustomPrinterExtendedSettings.IsActualLandscape = false;
                        ClaimPrintDialog.CustomPrinterExtendedSettings.IsActualMultiPage = false;
                        ClaimPrintDialog.CustomPrinterExtendedSettings.IsHorizontalFlow = true;
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {

                            ClaimPrintDialog.ExtendedPropertiesControl.DisableActualSizeControlsOnDlg();
                        }
                    }
                    if (ClaimPrintDialog.ShowDialog(null) == DialogResult.OK)
                    {
                        resultSetting = clsPrinterSettings.PrinterSettingsToString(ClaimPrintDialog.PrinterSettings);
                        sPrinterName = ClaimPrintDialog.PrinterSettings.PrinterName;
                        objExtendedPrinterSetting.Copy(ClaimPrintDialog.CustomPrinterExtendedSettings);

                        displayRect = ClaimPrintDialog.DisplayRectangle;

                    }
                
                }
            }
            catch (Exception Ex)
            {
                sPrinterName = string.Empty;
                objExtendedPrinterSetting = null;
                MessageBox.Show(Ex.ToString(), "Information");

            }
            finally
            {

            }
            return resultSetting;
        }
        public static string UpdatePrinterLabelSettings(string sRegistryModuleName, ref gloExtendedPrinterSettings objExtendedPrinterSetting, ref PrinterSettings thisPrinter, ref Rectangle displayRect, ref Label lblUpdate, bool bDefaultPrinter = false)
        {
            string resultSetting = string.Empty;
            string sPrinterName = string.Empty;

            try
            {

                using (gloPrintDialog ClaimPrintDialog = new gloPrintDialog(false))
                {
                    ClaimPrintDialog.bGetSettingsFromDB = false;
                    ClaimPrintDialog.RegistryModuleName = sRegistryModuleName;
                    ClaimPrintDialog.IsSaveExtendedSettings = true;
                    if (bDefaultPrinter)
                    {
                        //PrinterSettings settings = new PrinterSettings();
                        // settings.PrinterName = gloGlobal.gloTSPrint.GetSystemDefaultPrinter();
                        ClaimPrintDialog.PrinterSettings = gloGlobal.gloTSPrint.GetSystemDefaultPrinter();
                    }
                    else
                    {
                        if (thisPrinter == null)
                        {
                            ClaimPrintDialog.GetPrinter();
                            thisPrinter = ClaimPrintDialog.PrinterSettings;
                        }
                        else
                        {
                            ClaimPrintDialog.PrinterSettings = thisPrinter;
                        }
                    }
                    ClaimPrintDialog.CustomPrinterExtendedSettings = new gloExtendedPrinterSettings();
                    if (bDefaultPrinter)
                    {
                        objExtendedPrinterSetting = new gloExtendedPrinterSettings();
                        if (sRegistryModuleName == gloClinicalQueueGeneral.Classes.clsSettings.ModulePrintType.DefaultPrinter.ToString())
                        {
                            objExtendedPrinterSetting.CurrentPageSize = gloExtendedPrinterSettings.PageSize.ActualPageSize;
                            if (objExtendedPrinterSetting.PrinterMarginsTop == 0)
                            {
                                objExtendedPrinterSetting.PrinterMarginsTop = -10;
                            }
                            if (objExtendedPrinterSetting.PrinterMarginsLeft == 0)
                            {
                                objExtendedPrinterSetting.PrinterMarginsLeft = -10;
                            }
                            ClaimPrintDialog.CustomPrinterExtendedSettings.Copy(objExtendedPrinterSetting);
                        }
                    }
                    else
                    {
                        if (objExtendedPrinterSetting != null)
                        {
                            ClaimPrintDialog.CustomPrinterExtendedSettings.Copy(objExtendedPrinterSetting);
                        }
                        else
                        {
                            ClaimPrintDialog.LoadExtendedSettings();
                            objExtendedPrinterSetting = new gloExtendedPrinterSettings();
                            objExtendedPrinterSetting.Copy(ClaimPrintDialog.CustomPrinterExtendedSettings);
                        }
                    }
                    ClaimPrintDialog.DisplayRectangle = displayRect;

                    //SLR: This is taken care as part of gloPrintprogresscontroller when running service.
                    //ClaimPrintDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                    //ClaimPrintDialog.CustomPrinterExtendedSettings.IsShowProgress = false;
                    //ClaimPrintDialog.ExtendedPropertiesControl.DisablePrintMethod();
                    //ClaimPrintDialog.TITLE = strDialogTitle;

                    //if (ClaimPrintDialog.ShowDialog(null) == DialogResult.OK)
                    {
                        resultSetting = clsPrinterSettings.PrinterSettingsToString(ClaimPrintDialog.PrinterSettings);
                        sPrinterName = ClaimPrintDialog.PrinterSettings.PrinterName;
                        objExtendedPrinterSetting.Copy(ClaimPrintDialog.CustomPrinterExtendedSettings);

                        displayRect = ClaimPrintDialog.DisplayRectangle;
                        lblUpdate.Text = sPrinterName;
                    }

                }
            }
            catch (Exception Ex)
            {
                sPrinterName = string.Empty;
                objExtendedPrinterSetting = null;
                MessageBox.Show(Ex.ToString(), "Information");
            }
            finally
            {

            }
            return resultSetting;
        }

        #region Customized PrintDlg implementation

        /// <summary>
        /// Setup native print dialog resources and display it.
        /// </summary>
        /// <param name="printerSettings">printer settings</param>
        /// <returns>DialogResult: OK or Cancel</returns>
        internal DialogResult InvokePrintDlg(ref PrinterSettings printerSettings)
        {
            // PRINTDLG that will be passed to PrintDialogEx API function.
            PRINTDLG _pd = new PRINTDLG();

            _pd.lStructSize = Marshal.SizeOf(_pd);
            _pd.hwndOwner = this.Parent;
            // Pass the handles to the DEVMODE and DEVNAMES structures from the 
            // printer settings object to the PRINTDLG so that the current
            // printsettings are displayed in the print setup dialog.
            _pd.hDevMode = printerSettings.GetHdevmode();
            _pd.hDevNames = printerSettings.GetHdevnames();
            _pd.Copies = printerSettings.Copies;
            try
            {
                _pd.MaxPage = (short)printerSettings.MaximumPage;
                _pd.MinPage = (short)printerSettings.MinimumPage;
                _pd.FromPage = (short)printerSettings.FromPage;
                _pd.ToPage = (short)printerSettings.ToPage;

            }
            catch
            {
            }


            // indicates that only the print setup dialog should be shown.
            _pd.Flags = PrintFlag.PD_ENABLEPRINTHOOK | PrintFlag.PD_PAGENUMS; // | PrintFlag.PD_NOPAGENUMS;

            // Enables/Disables Collate Checkbox. 
            setFlag(ref _pd.Flags, PrintFlag.PD_COLLATE, printerSettings.Collate);

            // set print to file checkbox visible/not visible
            setFlag(ref _pd.Flags, PrintFlag.PD_HIDEPRINTTOFILE, !this.AllowPrintToFile);
            // set help button visible/not visible
            setFlag(ref _pd.Flags, PrintFlag.PD_SHOWHELP, this.ShowHelp);
            // Hides and disables the Network button.
            setFlag(ref _pd.Flags, PrintFlag.PD_NONETWORKBUTTON, !this.ShowNetwork);
            // Enable/Disables the Selection radio button.
            setFlag(ref _pd.Flags, PrintFlag.PD_NOSELECTION, !this.AllowSelection);
            // Enables/Disables and select Pages radio button. 
            setFlag(ref _pd.Flags, PrintFlag.PD_NOPAGENUMS, !this.AllowSomePages);

            if (this.AllowSelection)
            {
                setFlag(ref _pd.Flags, PrintFlag.PD_SELECTION, true);
            }

            // Create an in-memory Win32 dialog template; this will be a "child" window inside the PrintDlg
            // We have no use for this child window, except that its presence allows us to capture events when
            // the user interacts with the PrintDlg
            //_ipTemplate = BuildDialogTemplate();
            //_pd.hInstance = _ipTemplate;

            _pd.lpfnPrintHook = new PrintHookProc(PrintHookProcImpl);

            // Invoke the dialog.
            this.OnLoad(null, null);
            bool bRet = PrintDlg(_pd);
            if (!gloGlobal.gloTSPrint.isCopyPrint)
            {
                try
                {
                    this.ExtendedPropertiesControl.StopTimer();
                }
                catch
                {
                }
            }
            if (bRet == true)
            {
                ////////msp
                ////_pd.hDevMode = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);               
                ////////---
                if (!gloGlobal.gloTSPrint.isCopyPrint)
                {

                    CustomPrinterExtendedSettings = this.ExtendedPropertiesControl.GetPrinterParametersExtended();

                }
                // Pass the resulting DEVMODE and DEVNAMES structs back to the 
                // caller via the PrinterSettings object that was passed in.
                printerSettings.SetHdevmode(_pd.hDevMode);

                ////msp                  
                printerSettings.DefaultPageSettings.SetHdevmode(_pd.hDevMode);
                ////---

                printerSettings.SetHdevnames(_pd.hDevNames);
                try
                {
                    printerSettings.Collate = (_pd.Flags & PrintFlag.PD_COLLATE) == PrintFlag.PD_COLLATE;
                    printerSettings.PrintRange = (_pd.Flags & PrintFlag.PD_NOPAGENUMS) == PrintFlag.PD_NOPAGENUMS ? PrintRange.AllPages : PrintRange.SomePages;
                    printerSettings.MaximumPage = _pd.MaxPage;
                    printerSettings.MinimumPage = _pd.MinPage;
                    printerSettings.FromPage = _pd.FromPage;
                    printerSettings.ToPage = _pd.ToPage;
                    printerSettings.Copies = _pd.Copies;

                }
                catch
                {

                }
            }

            CancelEventArgs e = null;
            e = new CancelEventArgs(!bRet);
            this.OnClosing(null, e);
            e = null;


            try
            {
                if (_pd.hDevMode != null)
                {
                    Win32.GlobalFree(_pd.hDevMode);
                }
            }
            catch
            {
            }
            try
            {
                if (_pd.hDevNames != null)
                {
                    Win32.GlobalFree(_pd.hDevNames);
                }
            }
            catch
            {
            }
            try
            {
                if (_pd.hDC != null)
                {
                    Win32.DeleteDC(_pd.hDC);
                }
            }
            catch
            {
            }
            try
            {
                if (_pd.hPrintTemplate != null)
                {
                    Win32.GlobalFree(_pd.hPrintTemplate);
                }
            }
            catch
            {
            }
            try
            {
                if (_pd.hSetupTemplate != null)
                {
                    Win32.GlobalFree(_pd.hSetupTemplate);
                }
            }
            catch
            {
            }
            try
            {
                if (_pd.hInstance != null)
                {
                    Win32.GlobalFree(_pd.hInstance);
                }
            }
            catch
            {
            }
            _pd.hwndOwner = default(IntPtr);
            _pd = null;
            return (bRet == true) ? DialogResult.OK : DialogResult.Cancel;
        }

        /// <summary>
        /// Add/remove a flag from the given nFlag argument.
        /// </summary>
        /// <param name="nFlag">source flag</param>
        /// <param name="nValue">flag to add/remove</param>
        /// <param name="bSet">true:set, false:unset</param>
        /// <returns>true no error. otherwise, false.</returns>
        private bool setFlag(ref Int32 nFlag, Int32 nValue, bool bSet)
        {
            bool bRet = true;
            try
            {
                if (bSet)
                {
                    nFlag |= nValue;
                }
                else
                {
                    nFlag &= ~nValue;
                }
            }
            catch (Exception)
            {
                bRet = false;
            }

            return bRet;
        }

        /// <summary>
        /// The hook procedure for window messages generated by the PrintDlg
        /// </summary>
        /// <param name="hWnd">the handle of the window at which this message is targeted</param>
        /// <param name="msg">the message identifier</param>
        /// <param name="wParam">message-specific parameter data</param>
        /// <param name="lParam">mess-specific parameter data</param>
        /// <returns></returns>
        internal IntPtr PrintHookProcImpl(IntPtr hPrintDlgWnd, UInt16 msg, Int32 wParam, Int32 lParam)
        {
            // Evaluates the message parameter to determine the user action.
            switch (msg)
            {
                // WM_INITDIALOG -  pull the user-supplied control into the PrintDlg now, using the SetParent API.
                case WndMsg.WM_INITDIALOG:
                    {
                        this.Handle = hPrintDlgWnd;

                        //System.Diagnostics.Trace.WriteLine("The WM_INITDIALOG message was received.");

                        // window z-order position
                        UInt32 nZOrder = (this.TopMost) ? WndZOrder.HWND_TOPMOST : WndZOrder.HWND_TOP;

                        // set default location
                        UInt16 nDispStatus = (UInt16)WndPos.SWP_SHOWWINDOW;
                        //((this.DefaultArea.IsEmpty) ? (WndPos.SWP_NOSIZE | WndPos.SWP_NOMOVE) : WndPos.SWP_SHOWWINDOW);
                        Rectangle defaultRect = this.DefaultArea;
                        if (!defaultRect.IsEmpty)
                        {	// reset window location
                            this.m_normalLeft = defaultRect.Left;
                            this.m_normalTop = defaultRect.Top;
                            this.m_normalWidth = defaultRect.Width;
                            this.m_normalHeight = defaultRect.Height;
                        }
                        this.GetWindowRect(this.Handle, ref defaultRect);
                        this.m_normalWidth = defaultRect.Width;
                        this.m_normalHeight = defaultRect.Height;
                        Rectangle resolution = Screen.PrimaryScreen.Bounds;
                        // check for invalid print dialog dimension
                        if (((this.m_normalLeft + this.m_normalWidth) > resolution.Width) || ((this.m_normalTop + this.m_normalHeight) > resolution.Height))
                        {
                            // get the default edge of the screen to printdlg size

                            this.m_normalLeft = resolution.Width - this.m_normalWidth;
                            this.m_normalTop = resolution.Height - this.m_normalHeight;
                        }
                        if ((this.m_normalLeft < 0) || (this.m_normalTop < 0))
                        {
                            // get the default common to top of printdlg size

                            this.m_normalLeft = defaultRect.Left;
                            this.m_normalTop = defaultRect.Top;
                        }

                        if (ExtendedPropertiesControl == null)
                        {
                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {

                                GetExtPanel();
                            }
                        }
                        if (ExtendedPropertiesControl != null)
                        {
                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {

                                Rectangle oPosition = new Rectangle();
                                if (GetWindowRect(ExtendedPropertiesControl.Handle, ref oPosition))
                                {
                                    nHeightOfUserControl = oPosition.Height + 10;
                                }
                            }
                        }
                        this.m_normalHeight += nHeightOfUserControl;
                        Win32.SetWindowText(this.Handle, TITLE);

                        // set this dialog z-order to topmost.
                        Win32.SetWindowPos(this.Handle,
                            nZOrder,
                            this.m_normalLeft,
                            this.m_normalTop,
                            this.m_normalWidth,
                            this.m_normalHeight,
                            nDispStatus);

                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            if (this.ExtendedPropertiesControl != null)
                            {
                                Win32.SetParent(ExtendedPropertiesControl.Handle, this.Handle);
                            }
                        }
                        InitExtendedControls(hPrintDlgWnd);
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {

                            if (this.ExtendedPropertiesControl != null)
                            {
                                this.ExtendedPropertiesControl.StartTimer();
                            }
                        }
                        break;
                    }
                case WndMsg.WM_DESTROY:
                    //destroy the handles we have created
                    // Dispose(true);
                    break;
                // WM_MOVE - the PrintDlg has been moved, so we'll resize the content and user-supplied
                // panel to fit nicely
                case WndMsg.WM_MOVE:
                    {
                        if (this.DefaultArea.IsEmpty)
                        {
                            this.OnMove(null, null);
                        }
                        return IntPtr.Zero;
                    }
                case WndMsg.WM_COMMAND:
                    {
                        switch (wParam)
                        {
                            case WndMsg.IDOK:
                                {
                                    string strValidationMessage = "";
                                    bool returnVal = true;
                                    while (returnVal)
                                    {
                                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                                        {

                                            returnVal = this.ExtendedPropertiesControl.IsValidPrinterExtendedParameters(out strValidationMessage);
                                            if (strValidationMessage == "Success")
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                        //MessageBox.Show(strValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return IntPtr.Add(IntPtr.Zero, 1);
                                    }

                                    return IntPtr.Zero;

                                }
                            default:
                                {
                                    //  MessageBox.Show("wParam: " + wParam.ToString());
                                    break;
                                }

                        }
                        break;

                    }
                // WM_NOTIFY - we're only interested in the CDN_SELCHANGE notification message:
                // we grab the currently-selected filename and fire our event
                case WndMsg.WM_NOTIFY:
                    {
                        //IntPtr ipNotify = new IntPtr(lParam);
                        //OfNotify ofNot = (OfNotify)Marshal.PtrToStructure(ipNotify, typeof(OfNotify));
                        //UInt16 code = ofNot.hdr.code;
                        //if (code == ComboBoxSNotif.CBN_SELCHANGE)
                        //{
                        //}
                        return IntPtr.Zero;

                    }



            }
            return IntPtr.Zero;

        }

        /// <summary>
        /// Returns the container panel for this print dialog the extended 
        /// controls.
        /// </summary>
        /// <returns></returns>
        private gloExtendedPropertiesControl GetExtPanel(bool OpenBatchPrint = false)
        {
            if (!gloGlobal.gloTSPrint.isCopyPrint)
            {

                if (this.ExtendedPropertiesControl == null)
                {
                    this.ExtendedPropertiesControl = new gloExtendedPropertiesControl(OpenBatchPrint);
                }
            }
            return this.ExtendedPropertiesControl;
        }

        /// <summary>
        /// Layout the content of the PrintDlg, according to the overall size of the dialog
        /// </summary>
        /// <param name="hWnd">handle of window that received the WM_SIZE message</param>
        private void InitExtendedControls(IntPtr hPrintDlgWnd)
        {
            Rectangle oPosition = new Rectangle();

            // adjust OK and CANCEL button positions
            IntPtr hOKBtn = Win32.GetDlgItem(hPrintDlgWnd, IDOK);


            if (GetWindowRect(hOKBtn, ref oPosition))
            {
                Win32.MoveWindow(
                    hOKBtn,
                    oPosition.X,
                    oPosition.Y + nHeightOfUserControl,
                    oPosition.Width,	// Width
                    oPosition.Height,	// Height
                    true);
            }

            IntPtr hCancelBtn = Win32.GetDlgItem(hPrintDlgWnd, ID_CANCEL);
            Control IDCancelControl = Control.FromChildHandle(hCancelBtn);

            if (GetWindowRect(hCancelBtn, ref oPosition))
            {
                Win32.MoveWindow(
                    hCancelBtn,
                    oPosition.X,
                    oPosition.Y + nHeightOfUserControl,
                    oPosition.Width,	// Width
                    oPosition.Height,	// Height
                    true);
            }

            // Print group box 0x0430 group box.
            Rectangle rcUser = new Rectangle();
            IntPtr hGroupBox = Win32.GetDlgItem(hPrintDlgWnd, ID_PAGERANGE);
            GetWindowRect(hGroupBox, ref rcUser);

            //rcUser.Y += rcUser.Bottom + 10;
            if (ExtendedPropertiesControl != null)
            {
                if (!gloGlobal.gloTSPrint.isCopyPrint)
                {

                    Win32.MoveWindow(ExtendedPropertiesControl.Handle,
                        rcUser.X,
                        oPosition.Y - 5,
                        rcUser.Width,		// Width
                        nHeightOfUserControl - 5,	// Height
                        true);
                    try
                    {
                        // int i = this._userControl.ResetTabIndex(80);
                        IntPtr hCollateCheck = Win32.GetDlgItem(hPrintDlgWnd, ID_COLLATE);

                        //_userControl.SetTabOrder(hCollateCheck, hOKBtn, hCancelBtn);

                        //IntPtr hCopiesGroup = Win32.GetDlgItem(hPrintDlgWnd,0x431);
                        //IntPtr hPrinterMarginsGroup = _userControl.PrinterMarginsGroupHandle();
                        //IntPtr hResolutionGroup = _userControl.ResolutionGroupHandle();
                        //IntPtr hOkButton = Win32.GetDlgItem(hPrintDlgWnd,0x1);
                        //IntPtr hCancelButton = Win32.GetDlgItem(hPrintDlgWnd,0x2);

                        //Win32.SetWindowPos(hPrinterMarginsGroup, (uint)(hCopiesGroup), 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
                        //Win32.SetWindowPos(hResolutionGroup, (uint)(hPrinterMarginsGroup), 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
                        //Win32.SetWindowPos(hOkButton, (uint)(hResolutionGroup), 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
                        //Win32.SetWindowPos(hCancelButton, (uint)(hOkButton), 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
                        ExtendedPropertiesControl.SetTabOrder(hCollateCheck, hOKBtn, hCancelBtn);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK);
                    }
                }

            }


        }



        /// <summary>
        /// The GetWindowRect function retrieves the dimensions of the bounding rectangle of the 
        /// specified window. 
        /// The dimensions are given in screen coordinates that are relative to the upper-left 
        /// PrinterMargins of the screen. 
        /// </summary>
        /// <param name="hWnd">handle to window</param>
        /// <param name="lpRect">window coordinates</param>
        /// <returns>true if there is no error. otherwise, false.</returns>
        private bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect)
        {
            bool bRet = false;
            try
            {
                // convert the wnd screen co-ordinates to client co-ordinates
                RECT rc = new RECT();
                // Find the dimensions of the content panel
                Win32.GetWindowRect(hWnd, ref rc);

                IntPtr hHandle = (hWnd != this.Handle) ? this.Handle : this.Parent;
                // Translate these dimensions into the dialog's coordinate system
                POINT topLeft;
                topLeft.X = rc.left;
                topLeft.Y = rc.top;
                Win32.ScreenToClient(hHandle, ref topLeft);
                POINT bottomRight;
                bottomRight.X = rc.right;
                bottomRight.Y = rc.bottom;
                Win32.ScreenToClient(hHandle, ref bottomRight);
                lpRect.X = topLeft.X;
                lpRect.Width = bottomRight.X - topLeft.X;
                lpRect.Y = topLeft.Y;
                lpRect.Height = bottomRight.Y - topLeft.Y;

                bRet = true;
            }
            catch
            {
            }

            return bRet;
        }



        #endregion

        #region Persist extra window state
        // event info that allows form to persist extra window state data
        //public delegate void WindowStateDelegate(object sender, RegistryKey key);
        public delegate void WindowStateDelegate(object sender);
        public event WindowStateDelegate LoadStateEvent;
        public event WindowStateDelegate SaveStateEvent;

        private int m_normalLeft;
        private int m_normalTop;
        private int m_normalWidth;
        private int m_normalHeight;

        // window state store
        private string m_sRegistryPath;

        // Dialog module wise setting to store.

        public Rectangle DisplayRectangle
        {
            get
            {
                return new Rectangle(m_normalLeft, m_normalTop, m_normalWidth, m_normalHeight);

            }
            set
            {
                m_normalLeft = value.Left;
                m_normalTop = value.Top;
                m_normalWidth = value.Width;
                m_normalHeight = value.Height;
            }
        }


        private string _RegistryModuleName = "";
        public static string _BaseRegistry = @"Software\gloEMR\gloPrintDialog";
        public string RegistryModuleName
        {
            get
            {
                return _RegistryModuleName;
            }
            set
            {
                _RegistryModuleName = value;
                m_sRegistryPath = _BaseRegistry + @"\" + _RegistryModuleName;
            }
        }
        public string ModuleName { get; set; }
        public bool IsSaveExtendedSettings { get; set; }

        public void LoadExtendedSettingsForUser(DataTable dtUserExtendedSettings)
        {

            if (dtUserExtendedSettings != null && dtUserExtendedSettings.Rows.Count > 0)
            {
                this.m_normalLeft = Convert.ToInt16(dtUserExtendedSettings.Rows[0]["DocumentsLeft"]);
                this.m_normalTop = Convert.ToInt16(dtUserExtendedSettings.Rows[0]["DocumentsTop"]);
                this.m_normalWidth = Convert.ToInt16(dtUserExtendedSettings.Rows[0]["DocumentsWidth"]);
                this.m_normalHeight = Convert.ToInt16(dtUserExtendedSettings.Rows[0]["DocumentsHeight"]);

                this.CustomPrinterExtendedSettings.PrinterMarginsTop = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsPrinterMarginsTop"]);
                this.CustomPrinterExtendedSettings.PrinterMarginsRight = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsPrinterMarginsRight"]);
                this.CustomPrinterExtendedSettings.PrinterMarginsLeft = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsPrinterMarginsLeft"]);
                this.CustomPrinterExtendedSettings.PrinterMarginsBottom = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsPrinterMarginsBottom"]);

                this.CustomPrinterExtendedSettings.HorizontalOverlap = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsHorizontalOverlap"]);
                this.CustomPrinterExtendedSettings.HorizontalGutter = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsHorizontalGutter"]);
                this.CustomPrinterExtendedSettings.VerticalOverlap = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsVerticalOverlap"]);
                this.CustomPrinterExtendedSettings.VerticalGutter = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsVerticalGutter"]);

                this.CustomPrinterExtendedSettings.AdjustActualPageHorizontalPageWidthMargin = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsActualExtendedWidth"]);
                this.CustomPrinterExtendedSettings.AdjustActualPageVerticalPageHeightMargin = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsActualExtendedHeight"]);
                this.CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsFitExtendedWidth"]);
                this.CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsFitExtendedHeight"]);

                this.CustomPrinterExtendedSettings.CurrentPageSize = (gloExtendedPrinterSettings.PageSize)Convert.ToInt32(dtUserExtendedSettings.Rows[0]["DocumentsCurrentPageSize"]);

                this.CustomPrinterExtendedSettings.FooterTop = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsFooterTop"]);
                this.CustomPrinterExtendedSettings.FooterRight = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsFooterRight"]);
                this.CustomPrinterExtendedSettings.FooterLeft = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsFooterLeft"]);
                this.CustomPrinterExtendedSettings.FooterBottom = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["DocumentsFooterBottom"]);

                //Verified                    
                FontConverter fcvt = new FontConverter();
                Font thisFont = null;
                try
                {
                    thisFont = fcvt.ConvertFromString(dtUserExtendedSettings.Rows[0]["DocumentsFooterFont"].ToString()) as Font;
                }
                catch
                {
                }
                this.CustomPrinterExtendedSettings.FooterFont = thisFont;
                try
                {
                    if (thisFont != null)
                    {
                        thisFont.Dispose();
                        thisFont = null;
                    }
                    else
                    {
                        this.CustomPrinterExtendedSettings.FooterFont = System.Drawing.SystemFonts.CaptionFont;
                    }
                }
                catch
                {
                }
                fcvt = null;




                //Verified                    
                ColorConverter ccvt = new ColorConverter();
                Color thisColor = Color.Black;
                try
                {
                    thisColor = (Color)ccvt.ConvertFromString(dtUserExtendedSettings.Rows[0]["DocumentsFooterColor"].ToString());
                }
                catch
                {
                }
                this.CustomPrinterExtendedSettings.FooterColor = thisColor;
                ccvt = null;



                this.CustomPrinterExtendedSettings.CustomDPI = Convert.ToInt16(dtUserExtendedSettings.Rows[0]["DocumentsCustomDPI"]);
                this.CustomPrinterExtendedSettings.IsCustomDPI = Convert.ToBoolean(dtUserExtendedSettings.Rows[0]["DocumentsIsCustomDPI"]);
                this.CustomPrinterExtendedSettings.IsShowProgress = Convert.ToBoolean(dtUserExtendedSettings.Rows[0]["IsShowProgress"]);
                this.CustomPrinterExtendedSettings.IsBackGroundPrint = Convert.ToBoolean(dtUserExtendedSettings.Rows[0]["IsBackGroundPrint"]);

                //Aniket: This needs to be set Done
                this.CustomPrinterExtendedSettings.IsHorizontalFlow = Convert.ToBoolean(dtUserExtendedSettings.Rows[0]["IsHorizontalFlow"]);
                this.CustomPrinterExtendedSettings.IsActualLandscape = Convert.ToBoolean(dtUserExtendedSettings.Rows[0]["IsActualLandscape"]);
                this.CustomPrinterExtendedSettings.IsActualMultiPage = Convert.ToBoolean(dtUserExtendedSettings.Rows[0]["IsActualMultiPage"]);

                //Graphic Settings
                this.CustomPrinterExtendedSettings.NormalSettings.Top = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["NormalSettingsTop"]);
                this.CustomPrinterExtendedSettings.NormalSettings.Right = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["NormalSettingsRight"]);
                this.CustomPrinterExtendedSettings.NormalSettings.Left = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["NormalSettingsLeft"]);
                this.CustomPrinterExtendedSettings.NormalSettings.Bottom = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["NormalSettingsBottom"]);
                this.CustomPrinterExtendedSettings.NormalSettings.DpiX = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["NormalSettingsDpiX"]);
                this.CustomPrinterExtendedSettings.NormalSettings.DpiY = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["NormalSettingsDpiY"]);
                this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[0] = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["NormalSettingsF0"]);
                this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[1] = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["NormalSettingsF1"]);
                this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[2] = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["NormalSettingsF2"]);
                //this.CustomPrinterExtendedSettings.NormalSettings.bValuesAssigned = (this.CustomPrinterExtendedSettings.NormalSettings.DpiX == 0) || (this.CustomPrinterExtendedSettings.NormalSettings.DpiY == 0);

                this.CustomPrinterExtendedSettings.FlatSettings.Top = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["FlatSettingsTop"]);
                this.CustomPrinterExtendedSettings.FlatSettings.Right = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["FlatSettingsRight"]);
                this.CustomPrinterExtendedSettings.FlatSettings.Left = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["FlatSettingsLeft"]);
                this.CustomPrinterExtendedSettings.FlatSettings.Bottom = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["FlatSettingsBottom"]);
                this.CustomPrinterExtendedSettings.FlatSettings.DpiX = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["FlatSettingsDpiX"]);
                this.CustomPrinterExtendedSettings.FlatSettings.DpiY = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["FlatSettingsDpiY"]);
                this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[0] = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["FlatSettingsF0"]);
                this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[1] = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["FlatSettingsF1"]);
                this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[2] = (float)Convert.ToDouble(dtUserExtendedSettings.Rows[0]["FlatSettingsF2"]);
                //this.CustomPrinterExtendedSettings.FlatSettings.bValuesAssigned = (this.CustomPrinterExtendedSettings.FlatSettings.DpiX == 0) || (this.CustomPrinterExtendedSettings.FlatSettings.DpiY == 0);

            }
        }

        public static void DeleteRegistryKey(string strRegistryModuleName)
        {
            RegistryKey key = null;
            try
            {

                {
                    key = Registry.LocalMachine.OpenSubKey(_BaseRegistry, true);
                }
                if (key != null)
                {
                    try
                    {
                        key.DeleteSubKeyTree(strRegistryModuleName);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                    key.Dispose();
                    key = null;
                }
            }
        }
        public void LoadExtendedSettings(String psFileName = "")
        {
            // attempt to read state from registry

            RegistryKey key = null;
            try
            {
                if (gloGlobal.gloTSPrint.isTSPrintServiceCall)
                {
                    try
                    {
                        String PrinterSettingFile = "";
                        if (!String.IsNullOrWhiteSpace(psFileName))
                        {
                            PrinterSettingFile = psFileName;
                        }
                        else
                        {
                            PrinterSettingFile = gloClinicalQueueGeneral.gloQueueMetadatawriter.getPrinterSettingsFileName(gloGlobal.gloTSPrint.mappedLocalPath, RegistryModuleName);
                            //PrinterSettingFile = gloGlobal.gloTSPrint.getSettingFromPrinterConfig(gloGlobal.gloTSPrint.mappedLocalPath, RegistryModuleName + "_SettingsFile");
                        }
                       
                        if (!String.IsNullOrWhiteSpace(PrinterSettingFile))
                        {
                            DataSet ds = gloGlobal.gloTSPrint.readPrinterSettingsToDataSet( gloGlobal.gloTSPrint.mappedLocalPath,  PrinterSettingFile);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                LoadExtendedSettingsForUser(ds.Tables[0]);
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
                    catch 
                    {}
                    
                }
                Rectangle defaultRect = this.DefaultArea;
                if (IsSaveExtendedSettings != true)
                {
                    key = Registry.CurrentUser.OpenSubKey(m_sRegistryPath, true);
                }
                else
                {
                    key = Registry.LocalMachine.OpenSubKey(m_sRegistryPath, true);
                }
                if (key != null)
                {
                    if (defaultRect.IsEmpty)
                    {
                        this.m_normalLeft = (int)key.GetValue("Left", 0);
                        this.m_normalTop = (int)key.GetValue("Top", 0);
                        this.m_normalWidth = (int)key.GetValue("Width", 0);
                        this.m_normalHeight = (int)key.GetValue("Height", 0);
                    }
                    else
                    {
                        this.m_normalLeft = defaultRect.Left;
                        this.m_normalTop = defaultRect.Top;
                        this.m_normalWidth = defaultRect.Width;
                        this.m_normalHeight = defaultRect.Height;
                    }
                    //  object dummy = key.GetValue("PrinterMarginsTop", 0);

                    this.CustomPrinterExtendedSettings.PrinterMarginsTop = (float)Convert.ToDouble(key.GetValue("PrinterMarginsTop", 0));
                    this.CustomPrinterExtendedSettings.PrinterMarginsRight = (float)Convert.ToDouble(key.GetValue("PrinterMarginsRight", 0));
                    this.CustomPrinterExtendedSettings.PrinterMarginsLeft = (float)Convert.ToDouble(key.GetValue("PrinterMarginsLeft", 0));
                    this.CustomPrinterExtendedSettings.PrinterMarginsBottom = (float)Convert.ToDouble(key.GetValue("PrinterMarginsBottom", 0));

                    this.CustomPrinterExtendedSettings.HorizontalGutter = (float)Convert.ToDouble(key.GetValue("HorizontalGutter", 0));
                    this.CustomPrinterExtendedSettings.VerticalGutter = (float)Convert.ToDouble(key.GetValue("VerticalGutter", 0));
                    this.CustomPrinterExtendedSettings.HorizontalOverlap = (float)Convert.ToDouble(key.GetValue("HorizontalOverlap", 0));
                    this.CustomPrinterExtendedSettings.VerticalOverlap = (float)Convert.ToDouble(key.GetValue("VerticalOverlap", 0));

                    this.CustomPrinterExtendedSettings.AdjustActualPageHorizontalPageWidthMargin = (float)Convert.ToDouble(key.GetValue("ActualExtendedWidth", "50.9"));
                    this.CustomPrinterExtendedSettings.AdjustActualPageVerticalPageHeightMargin = (float)Convert.ToDouble(key.GetValue("ActualExtendedHeight", "50.9"));
                    this.CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin = (float)Convert.ToDouble(key.GetValue("FitExtendedWidth", "0"));
                    this.CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin = (float)Convert.ToDouble(key.GetValue("FitExtendedHeight", "0"));

                    try
                    {
                        this.CustomPrinterExtendedSettings.CurrentPageSize = (gloExtendedPrinterSettings.PageSize)Convert.ToInt32(key.GetValue("CurrentPageSize", 0));
                    }
                    catch
                    {
                        this.CustomPrinterExtendedSettings.CurrentPageSize = (gloExtendedPrinterSettings.PageSize.FitToPage);
                    }

                    this.CustomPrinterExtendedSettings.FooterTop = (float)Convert.ToDouble(key.GetValue("FooterTop", 0));
                    this.CustomPrinterExtendedSettings.FooterRight = (float)Convert.ToDouble(key.GetValue("FooterRight", 0));
                    this.CustomPrinterExtendedSettings.FooterLeft = (float)Convert.ToDouble(key.GetValue("FooterLeft", 0));
                    this.CustomPrinterExtendedSettings.FooterBottom = (float)Convert.ToDouble(key.GetValue("FooterBottom", 0));
                    FontConverter fcvt = new FontConverter();
                    string defaultString = fcvt.ConvertToString(SystemFonts.CaptionFont);
                    string defaulter = Convert.ToString(key.GetValue("FooterFont", defaultString));
                    Font storedFont = null;
                    try
                    {
                        storedFont = fcvt.ConvertFromString(defaulter) as Font;
                    }
                    catch
                    {
                    }
                    this.CustomPrinterExtendedSettings.FooterFont = storedFont;
                    try
                    {
                        if (storedFont != null)
                        {
                            storedFont.Dispose();
                            storedFont = null;
                        }
                        else
                        {
                            this.CustomPrinterExtendedSettings.FooterFont = System.Drawing.SystemFonts.CaptionFont;
                        }
                    }
                    catch
                    {
                    }
                    fcvt = null;

                    ColorConverter ccvt = new ColorConverter();

                    defaultString = ccvt.ConvertToString(Color.Black);
                    defaulter = Convert.ToString(key.GetValue("FooterColor", defaultString));
                    Color storedColor = Color.Black;
                    try
                    {
                        storedColor = (Color)ccvt.ConvertFromString(defaulter);
                    }
                    catch
                    {
                    }
                    ccvt = null;

                    this.CustomPrinterExtendedSettings.FooterColor = storedColor;


                    //   this.CustomPrinterExtendedSettings.IsActualPageSize = System.Convert.ToBoolean(key.GetValue("IsActualPageSize", false));

                    this.CustomPrinterExtendedSettings.IsCustomDPI = Convert.ToBoolean(key.GetValue("IsCustomDPI", false)); //(Boolean)(key.GetValue("IsCustomDPI", false));
                    this.CustomPrinterExtendedSettings.CustomDPI = (Int32)key.GetValue("CustomDPI", 0);


                    this.CustomPrinterExtendedSettings.IsShowProgress = System.Convert.ToBoolean(key.GetValue("IsShowProgress", true));
                    this.CustomPrinterExtendedSettings.IsBackGroundPrint = System.Convert.ToBoolean(key.GetValue("IsBackGroundPrint", false));

                    //Aniket: This needs to be set Done
                    this.CustomPrinterExtendedSettings.IsHorizontalFlow = Convert.ToBoolean(key.GetValue("IsHorizontalFlow", true)); //(Boolean)(key.GetValue("IsCustomDPI", false));
                    this.CustomPrinterExtendedSettings.IsActualLandscape = System.Convert.ToBoolean(key.GetValue("IsActualLandscape", false));
                    this.CustomPrinterExtendedSettings.IsActualMultiPage = System.Convert.ToBoolean(key.GetValue("IsActualMultipage", false));


                    this.CustomPrinterExtendedSettings.NormalSettings.Top = (float)Convert.ToDouble(key.GetValue("NormalSettingsTop", 0));
                    this.CustomPrinterExtendedSettings.NormalSettings.Right = (float)Convert.ToDouble(key.GetValue("NormalSettingsRight", 0));
                    this.CustomPrinterExtendedSettings.NormalSettings.Left = (float)Convert.ToDouble(key.GetValue("NormalSettingsLeft", 0));
                    this.CustomPrinterExtendedSettings.NormalSettings.Bottom = (float)Convert.ToDouble(key.GetValue("NormalSettingsBottom", 0));
                    this.CustomPrinterExtendedSettings.NormalSettings.DpiX = (float)Convert.ToDouble(key.GetValue("NormalSettingsDpiX", 0));
                    this.CustomPrinterExtendedSettings.NormalSettings.DpiY = (float)Convert.ToDouble(key.GetValue("NormalSettingsDpiY", 0));
                    this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[0] = (float)Convert.ToDouble(key.GetValue("NormalSettingsF0", 0));
                    this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[1] = (float)Convert.ToDouble(key.GetValue("NormalSettingsF1", 0));
                    this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[2] = (float)Convert.ToDouble(key.GetValue("NormalSettingsF2", 0));
                    this.CustomPrinterExtendedSettings.NormalSettings.bValuesAssigned = (this.CustomPrinterExtendedSettings.NormalSettings.DpiX != 0) && (this.CustomPrinterExtendedSettings.NormalSettings.DpiY != 0);

                    this.CustomPrinterExtendedSettings.FlatSettings.Top = (float)Convert.ToDouble(key.GetValue("FlatSettingsTop", 0));
                    this.CustomPrinterExtendedSettings.FlatSettings.Right = (float)Convert.ToDouble(key.GetValue("FlatSettingsRight", 0));
                    this.CustomPrinterExtendedSettings.FlatSettings.Left = (float)Convert.ToDouble(key.GetValue("FlatSettingsLeft", 0));
                    this.CustomPrinterExtendedSettings.FlatSettings.Bottom = (float)Convert.ToDouble(key.GetValue("FlatSettingsBottom", 0));
                    this.CustomPrinterExtendedSettings.FlatSettings.DpiX = (float)Convert.ToDouble(key.GetValue("FlatSettingsDpiX", 0));
                    this.CustomPrinterExtendedSettings.FlatSettings.DpiY = (float)Convert.ToDouble(key.GetValue("FlatSettingsDpiY", 0));
                    this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[0] = (float)Convert.ToDouble(key.GetValue("FlatSettingsF0", 0));
                    this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[1] = (float)Convert.ToDouble(key.GetValue("FlatSettingsF1", 0));
                    this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[2] = (float)Convert.ToDouble(key.GetValue("FlatSettingsF2", 0));
                    this.CustomPrinterExtendedSettings.FlatSettings.bValuesAssigned = (this.CustomPrinterExtendedSettings.FlatSettings.DpiX != 0) && (this.CustomPrinterExtendedSettings.FlatSettings.DpiY != 0);

                }
                if (bGetSettingsFromDB)
                {
                    //Fetch from Database
                    object _obj = null;

                    //////----Using saved parameters for printer Dialog position on windows ---------------------
                    _obj = null;
                    this.GetSetting(this.ModuleName + "Left", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.m_normalLeft = Convert.ToInt32(_obj);
                    }
                    else if (!defaultRect.IsEmpty)
                    {
                        {
                            this.m_normalLeft = defaultRect.Left;
                        }
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "Top", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.m_normalTop = Convert.ToInt32(_obj);
                    }
                    else if (!defaultRect.IsEmpty)
                    {
                        {
                            this.m_normalTop = defaultRect.Top;
                        }
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "Width", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.m_normalWidth = Convert.ToInt32(_obj);
                    }
                    else if (!defaultRect.IsEmpty)
                    {
                        {
                            this.m_normalWidth = defaultRect.Width;
                        }
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "Height", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.m_normalHeight = Convert.ToInt32(_obj);
                    }
                    else if (!defaultRect.IsEmpty)
                    {
                        {
                            this.m_normalHeight = defaultRect.Height;
                        }
                    }
                    //////----Using saved parameters for printer Dialog position on windows ---------------------


                    //////----Using saved parameters for Extended dialog user control Paremeters --------------
                    _obj = null;
                    this.GetSetting(this.ModuleName + "PrinterMarginsTop", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.PrinterMarginsTop = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "PrinterMarginsRight", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.PrinterMarginsRight = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "PrinterMarginsLeft", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.PrinterMarginsLeft = (float)Convert.ToDecimal(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "PrinterMarginsBottom", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.PrinterMarginsBottom = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "HorizontalOverlap", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.HorizontalOverlap = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "HorizontalGutter", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.HorizontalGutter = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "VerticalOverlap", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.VerticalOverlap = (float)Convert.ToDecimal(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "VerticalGutter", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.VerticalGutter = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "ActualExtendedWidth", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.AdjustActualPageHorizontalPageWidthMargin = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "ActualExtendedHeight", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.AdjustActualPageVerticalPageHeightMargin = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FitExtendedWidth", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin = (float)Convert.ToDecimal(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "FitExtendedHeight", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "CurrentPageSize", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        try
                        {
                            this.CustomPrinterExtendedSettings.CurrentPageSize = (gloExtendedPrinterSettings.PageSize)Convert.ToInt32(_obj);
                        }
                        catch
                        {
                            this.CustomPrinterExtendedSettings.CurrentPageSize = (gloExtendedPrinterSettings.PageSize.FitToPage);

                        }
                    }
                    //////----Using saved parameters for Extended dialog user control Paremeters --------------
                    _obj = null;
                    this.GetSetting(this.ModuleName + "FooterTop", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FooterTop = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FooterRight", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FooterRight = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FooterLeft", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FooterLeft = (float)Convert.ToDecimal(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "FooterBottom", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FooterBottom = (float)Convert.ToDecimal(_obj);
                    }

                    //////----Using saved parameters for Extended dialog user control Paremeters --------------
                    _obj = null;
                    this.GetSetting(this.ModuleName + "FooterFont", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        string defaultString = Convert.ToString(_obj);
                        FontConverter fcvt = new FontConverter();
                        Font thisFont = null;
                        try
                        {
                            thisFont = fcvt.ConvertFromString(defaultString) as Font;
                        }
                        catch
                        {
                        }
                        this.CustomPrinterExtendedSettings.FooterFont = thisFont;
                        try
                        {
                            if (thisFont != null)
                            {
                                thisFont.Dispose();
                                thisFont = null;
                            }
                            else
                            {
                                this.CustomPrinterExtendedSettings.FooterFont = System.Drawing.SystemFonts.CaptionFont;
                            }
                        }
                        catch
                        {
                        }
                        fcvt = null;
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FooterColor", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        string defaultString = Convert.ToString(_obj);
                        ColorConverter ccvt = new ColorConverter();
                        Color thisColor = Color.Black;
                        try
                        {
                            thisColor = (Color)ccvt.ConvertFromString(defaultString);
                        }
                        catch
                        {
                        }
                        this.CustomPrinterExtendedSettings.FooterColor = thisColor;
                        ccvt = null;
                    }
                    //_obj = null;
                    //this.GetSetting(this.ModuleName + "IsActualPageSize", this._UserID, this.ClinicId, out _obj);
                    //if (_obj != null && Convert.ToString(_obj).Length > 0)
                    //{
                    //    this.CustomPrinterExtendedSettings.IsActualPageSize = Convert.ToBoolean(_obj);
                    //}
                    _obj = null;
                    this.GetSetting(this.ModuleName + "CustomDPI", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.CustomDPI = Convert.ToInt32(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "IsCustomDPI", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.IsCustomDPI = Convert.ToBoolean(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "IsShowProgress", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.IsShowProgress = Convert.ToBoolean(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "IsBackGroundPrint", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.IsBackGroundPrint = Convert.ToBoolean(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "IsHorizontalFlow", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.IsHorizontalFlow = Convert.ToBoolean(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "IsActualLandscape", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.IsActualLandscape = Convert.ToBoolean(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "IsActualMultiPage", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.IsActualMultiPage = Convert.ToBoolean(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "NormalSettingsTop", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.NormalSettings.Top = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "NormalSettingsRight", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.NormalSettings.Right = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "NormalSettingsLeft", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.NormalSettings.Left = (float)Convert.ToDecimal(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "NormalSettingsBottom", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.NormalSettings.Bottom = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "NormalSettingsDpiX", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.NormalSettings.DpiX = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "NormalSettingsDpiY", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.NormalSettings.DpiY = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "NormalSettingsF0", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[0] = (float)Convert.ToDecimal(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "NormalSettingsF1", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[1] = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "NormalSettingsF2", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[2] = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FlatSettingsTop", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FlatSettings.Top = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FlatSettingsRight", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FlatSettings.Right = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FlatSettingsLeft", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FlatSettings.Left = (float)Convert.ToDecimal(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "FlatSettingsBottom", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FlatSettings.Bottom = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FlatSettingsDpiX", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FlatSettings.DpiX = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FlatSettingsDpiY", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FlatSettings.DpiY = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FlatSettingsF0", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[0] = (float)Convert.ToDecimal(_obj);
                    }
                    _obj = null;
                    this.GetSetting(this.ModuleName + "FlatSettingsF1", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[1] = (float)Convert.ToDecimal(_obj);
                    }

                    _obj = null;
                    this.GetSetting(this.ModuleName + "FlatSettingsF2", this._UserID, this.ClinicId, out _obj);
                    if (_obj != null && Convert.ToString(_obj).Length > 0)
                    {
                        this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[2] = (float)Convert.ToDecimal(_obj);
                    }

                    //////----Using saved parameters for Extended dialog user control Paremeters --------------
                    _obj = null;
                }

            }
            catch
            {


            }
            finally
            {
                if (key != null)
                {
                    key.Close(); key.Dispose(); key = null;
                }
            }

        }



        private void OnLoad(object sender, System.EventArgs e)
        {
            if (ShowPrinterProfileDialog == false)
            {
                if (IsSaveExtendedSettings != true)
                {
                    LoadExtendedSettings();
                }
                if (!gloGlobal.gloTSPrint.isCopyPrint)
                {

                    this.ExtendedPropertiesControl.SetPrinterParametersExtended(CustomPrinterExtendedSettings);
                }
            }

            if (String.Compare(this.ModuleName, "PrintWorkersCompForms", true) == 0 ||
                String.Compare(this.ModuleName, "PrintDMSDocuments", true) == 0 ||
                String.Compare(this.ModuleName, "PrintRCMDocuments", true) == 0||
                String.Compare(this.ModuleName, "CrystalReports",true) ==0
                )
            {
                if (!gloGlobal.gloTSPrint.isCopyPrint)
                {

                    this.ExtendedPropertiesControl.DisableActualSizeControlsOnDlg();
                }
            }

            //13-Jul-16 Aniket: Resolving Bug #98263: gloPM : Reports (Background printing) : Application does not print data prorperly as user select modify button of printer profiles. 
            if ((String.Compare(this.ModuleName, "SSRSReports", true) == 0) || (String.Compare(this.ModuleName, "CrystalReports", true) == 0))
            {
                if (!gloGlobal.gloTSPrint.isCopyPrint)
                {

                    this.ExtendedPropertiesControl.DisablelandscapeControlsOnDlg();
                }
            }
            if (LoadStateEvent != null)
            {
                LoadStateEvent(this);
            }
        }
        public void GetPrinter(String psFileName ="")
        {
            if ((!bUseDefaultPrinter) || (this.PrinterSettings == null))
            {
                if (gloGlobal.gloTSPrint.isTSPrintServiceCall)
                {
                    try
                    {
                        String PrinterSettingFile = "";
                        if (!String.IsNullOrWhiteSpace(psFileName))
                        {
                            PrinterSettingFile = psFileName;
                        }
                        else
                        {
                            PrinterSettingFile = gloClinicalQueueGeneral.gloQueueMetadatawriter.getPrinterSettingsFileName(gloGlobal.gloTSPrint.mappedLocalPath, RegistryModuleName);
                            //PrinterSettingFile = gloGlobal.gloTSPrint.getSettingFromPrinterConfig(gloGlobal.gloTSPrint.mappedLocalPath,  RegistryModuleName + "_SettingsFile");
                        }
                        if (!String.IsNullOrWhiteSpace(PrinterSettingFile))
                        {
                            DataSet ds = gloGlobal.gloTSPrint.readPrinterSettingsToDataSet(gloGlobal.gloTSPrint.mappedLocalPath, PrinterSettingFile);

                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                this.PrinterSettings = clsPrinterSettings.StringToPrinterSettings(ds.Tables[0].Rows[0]["PrinterSettings"].ToString());
                            }
                            if (ds != null)
                            {
                                ds.Dispose();
                                ds = null;
                            }
                        }
                    }
                    catch
                    {
                        this.PrinterSettings = null;
                    }
                    if (this.PrinterSettings != null)
                    {
                        return;
                    }
                }
                // attempt to read state from registry
                RegistryKey key = null;
                try
                {

                    if (IsSaveExtendedSettings != true)
                    {
                        key = Registry.CurrentUser.OpenSubKey(m_sRegistryPath, true);
                    }
                    else
                    {
                        key = Registry.LocalMachine.OpenSubKey(m_sRegistryPath, true);
                    }
                    if (key != null)
                    {
                        string printerstring = Convert.ToString(key.GetValue("PrinterSettings", ""));
                        if (string.IsNullOrEmpty(printerstring))
                        {
                            if (this.PrinterSettings == null)
                            {
                                this.PrinterSettings = gloGlobal.gloTSPrint.GetSystemDefaultPrinter();
                                // this.PrinterSettings = new PrinterSettings();
                            }
                        }
                        else
                        {
                            this.PrinterSettings = clsPrinterSettings.StringToPrinterSettings(printerstring);
                        }
                    }

                    if (bGetSettingsFromDB)
                    {
                        object _obj = null;

                        //////----Using saved parameters for printer Dialog position on windows ---------------------
                        _obj = null;
                        this.GetSetting(this.ModuleName + "PrinterSettings", this._UserID, this.ClinicId, out _obj);
                        if (_obj != null && Convert.ToString(_obj).Length > 0)
                        {
                            string printerstring = Convert.ToString(_obj);
                            if (string.IsNullOrEmpty(printerstring))
                            {
                                if (this.PrinterSettings == null)
                                {
                                    this.PrinterSettings = gloGlobal.gloTSPrint.GetSystemDefaultPrinter();
                                    //this.PrinterSettings = new PrinterSettings();
                                }
                            }
                            else
                            {
                                this.PrinterSettings = clsPrinterSettings.StringToPrinterSettings(printerstring);
                            }

                        }
                        else if (this.PrinterSettings == null)
                        {
                            {
                                this.PrinterSettings = gloGlobal.gloTSPrint.GetSystemDefaultPrinter();
                                //this.PrinterSettings = new PrinterSettings();
                            }
                        }
                        _obj = null;
                    }
                }
                catch
                {


                }
                finally
                {
                    if (key != null)
                    {
                        key.Close(); key.Dispose(); key = null;
                    }
                }
                if (!bGetSettingsFromDB)
                {
                    if (this.PrinterSettings == null)
                    {
                        {
                            this.PrinterSettings = gloGlobal.gloTSPrint.GetSystemDefaultPrinter();
                            if (this.PrinterSettings == null)
                            {
                                this.PrinterSettings = new PrinterSettings();
                            }
                        }
                    }
                }

            }
        }

        public Boolean readPrinterSettingsFromFile(String PrinterSettingFile)
        {
            Boolean result = false;
            try
            {
                if (!String.IsNullOrWhiteSpace(PrinterSettingFile))
                {
                    DataSet ds = gloGlobal.gloTSPrint.readPrinterSettingsToDataSet(gloGlobal.gloTSPrint.mappedLocalPath, PrinterSettingFile);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Columns.Contains("UpdateSettings") && ds.Tables[0].Rows[0]["UpdateSettings"].ToString() == "1")
                        {
                            // Set printer settings for selected printer
                            PrinterSettings ps = new PrinterSettings();
                            try
                            {
                                if (ds.Tables[0].Rows[0]["PrinterName"].ToString() != "default")
                                {
                                    ps.PrinterName = ds.Tables[0].Rows[0]["PrinterName"].ToString();
                                }
                            }
                            catch
                            {
                            }
                            ds.Tables[0].Rows[0]["PrinterSettings"] = clsPrinterSettings.PrinterSettingsToString(ps);
                            this.PrinterSettings = ps;
                            ps = null;

                            // Set extended printer settings 
                            SaveGraphicsBound();
                            if (this.CustomPrinterExtendedSettings.NormalSettings != null)
                            {
                                ds.Tables[0].Rows[0]["NormalSettingsTop"] = this.CustomPrinterExtendedSettings.NormalSettings.Top;
                                ds.Tables[0].Rows[0]["NormalSettingsRight"] = this.CustomPrinterExtendedSettings.NormalSettings.Right;
                                ds.Tables[0].Rows[0]["NormalSettingsLeft"] = this.CustomPrinterExtendedSettings.NormalSettings.Left;
                                ds.Tables[0].Rows[0]["NormalSettingsBottom"] = this.CustomPrinterExtendedSettings.NormalSettings.Bottom;
                                ds.Tables[0].Rows[0]["NormalSettingsDpiX"] = this.CustomPrinterExtendedSettings.NormalSettings.DpiX;
                                ds.Tables[0].Rows[0]["NormalSettingsDpiY"] = this.CustomPrinterExtendedSettings.NormalSettings.DpiY;
                                ds.Tables[0].Rows[0]["NormalSettingsF0"] = this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[0];
                                ds.Tables[0].Rows[0]["NormalSettingsF1"] = this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[1];
                                ds.Tables[0].Rows[0]["NormalSettingsF2"] = this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[2];
                            }
                            if (this.CustomPrinterExtendedSettings.FlatSettings != null)
                            {
                                ds.Tables[0].Rows[0]["FlatSettingsTop"] = this.CustomPrinterExtendedSettings.FlatSettings.Top;
                                ds.Tables[0].Rows[0]["FlatSettingsRight"] = this.CustomPrinterExtendedSettings.FlatSettings.Right;
                                ds.Tables[0].Rows[0]["FlatSettingsLeft"] = this.CustomPrinterExtendedSettings.FlatSettings.Left;
                                ds.Tables[0].Rows[0]["FlatSettingsBottom"] = this.CustomPrinterExtendedSettings.FlatSettings.Bottom;
                                ds.Tables[0].Rows[0]["FlatSettingsDpiX"] = this.CustomPrinterExtendedSettings.FlatSettings.DpiX;
                                ds.Tables[0].Rows[0]["FlatSettingsDpiY"] = this.CustomPrinterExtendedSettings.FlatSettings.DpiY;
                                ds.Tables[0].Rows[0]["FlatSettingsF0"] = this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[0];
                                ds.Tables[0].Rows[0]["FlatSettingsF1"] = this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[1];
                                ds.Tables[0].Rows[0]["FlatSettingsF2"] = this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[2];
                            }

                            ds.Tables[0].Rows[0]["UpdateSettings"] = "0";

                            // Update Setting file
                            ds.Tables[0].WriteXml(Path.Combine(gloGlobal.gloTSPrint.mappedLocalPath, gloGlobal.gloTSPrint.PrinterConfigDirectory, PrinterSettingFile));
                        }
                        else
                        {
                            this.PrinterSettings = clsPrinterSettings.StringToPrinterSettings(ds.Tables[0].Rows[0]["PrinterSettings"].ToString());
                        }

                        LoadExtendedSettingsForUser(ds.Tables[0]);
                        result = true;
                    }
                    if (ds != null)
                    {
                        ds.Dispose();
                        ds = null;
                    }
                }
            }
            catch 
            {
            }
            return result;
        }

        private enum SettingFlag
        {
            None = 0,
            Clinic = 1,
            User = 2,
        }

        //public bool AddSetting(string Name, string Value, Int64 ClinicID, Int64 UserID, SettingFlag UserClinicFlag)
        private bool AddSetting(string Name, string Value, Int64 ClinicID, Int64 UserID, SettingFlag UserClinicFlag)
        {
            //public bool AddSetting(string Name, string Value, Int64 ClinicID, Int64 UserID, SettingFlag UserClinicFlag)
            if (bGetSettingsFromDB == true && ConnectionString != null && ConnectionString != "")
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    oDB.Connect(false);

                    oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                    oDB.Execute("gsp_InUpSettings", oDBParameters);
                    return true;

                }
                catch (gloDatabaseLayer.DBException)
                {
                    return false;
                }
                catch (Exception)
                {

                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }

            }
            else
            {
                return false;
            }
        }

        private void GetSetting(string SettingName, Int64 UserID, Int64 ClinicID, out object Value)
        {
            //public void GetSetting(string SettingName, Int64 UserID, Int64 ClinicID, out object Value)
            //{
            if (bGetSettingsFromDB)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
                try
                {
                    oDB.Connect(false);
                    Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE sSettingsName = '" + SettingName + "' AND nUserID = " + UserID + " AND nClinicID = " + ClinicID + "");
                }
                catch (gloDatabaseLayer.DBException)
                {
                    Value = null;

                }
                catch (Exception)
                {
                    Value = null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            else
            {
                Value = null;
            }
            //}
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (e != null)
            {
                if (e.Cancel) return;
            }
            if (!gloGlobal.gloTSPrint.isCopyPrint)
            {
                try
                {
                    this.CustomPrinterExtendedSettings = this.ExtendedPropertiesControl.GetPrinterParametersExtended();
                }
                catch
                {
                }
            }
            if (IsSaveExtendedSettings != true)
            {
                if (ShowPrinterProfileDialog == false || ModifyPrinter == true)
                {
                    SaveExtendedSettings();
                }
            }
            try
            {
                // fire SaveState event
                if (SaveStateEvent != null)
                    SaveStateEvent(this);
            }
            catch
            {
            }

        }
        public void SaveGraphicsBound()
        {
            try
            {
                this.CustomPrinterExtendedSettings.NormalSettings = this.CustomPrinterExtendedSettings.GetGraphicsBound(PrinterSettings);

                bool thisLandscape = PrinterSettings.DefaultPageSettings.Landscape;
                PrinterSettings.DefaultPageSettings.Landscape = false;
                if (thisLandscape != PrinterSettings.DefaultPageSettings.Landscape)
                {

                    this.CustomPrinterExtendedSettings.FlatSettings = this.CustomPrinterExtendedSettings.GetGraphicsBound(PrinterSettings);
                    PrinterSettings.DefaultPageSettings.Landscape = thisLandscape;
                }
                else
                {
                    this.CustomPrinterExtendedSettings.FlatSettings = this.CustomPrinterExtendedSettings.NormalSettings;

                }
            }
            catch
            {
            }
        }
        public void SaveExtendedSettings()
        {
            // save position, size and state
            RegistryKey key = null;
            System.Data.DataTable dtPrinterSettings = new System.Data.DataTable();

            try
            {
                SaveGraphicsBound();
                //If the settings are not stored in the database, then store in the registry

                if (bGetSettingsFromDB == false)
                {
                    if (gloGlobal.gloTSPrint.isTSPrintServiceCall)
                    {
                        clsPrinterSettings.CreateSchemadtPrinterSettingsDetails(ref dtPrinterSettings);
                        dtPrinterSettings.TableName = this.RegistryModuleName;
                        AddPrinterSettingsToTable(ref dtPrinterSettings);
                        dtPrinterSettings.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                        dtPrinterSettings.Rows[0]["PrinterName"] = this.PrinterSettings.PrinterName;
                        dtPrinterSettings.Columns.Add("PrinterSettings", System.Type.GetType("System.String"));
                        dtPrinterSettings.Rows[0]["PrinterSettings"] = clsPrinterSettings.PrinterSettingsToString(this.PrinterSettings);
                        dsPrinters.Tables.Add(dtPrinterSettings);
                    }
                    else
                    {
                        if (IsSaveExtendedSettings != true)
                        {
                            key = Registry.CurrentUser.OpenSubKey(m_sRegistryPath, true);
                        }
                        else
                        {
                            key = Registry.LocalMachine.OpenSubKey(m_sRegistryPath, true);
                        }
                        if (key == null)
                        {
                            if (IsSaveExtendedSettings != true)
                            {
                                key = Registry.CurrentUser.CreateSubKey(m_sRegistryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                            }
                            else
                            {
                                key = Registry.LocalMachine.CreateSubKey(m_sRegistryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                            }
                        }

                        key.SetValue("Left", m_normalLeft);
                        key.SetValue("Top", m_normalTop);
                        key.SetValue("Width", m_normalWidth);
                        key.SetValue("Height", m_normalHeight);

                        key.SetValue("PrinterMarginsTop", CustomPrinterExtendedSettings.PrinterMarginsTop, RegistryValueKind.Unknown);
                        key.SetValue("PrinterMarginsRight", CustomPrinterExtendedSettings.PrinterMarginsRight, RegistryValueKind.Unknown);
                        key.SetValue("PrinterMarginsLeft", CustomPrinterExtendedSettings.PrinterMarginsLeft, RegistryValueKind.Unknown);
                        key.SetValue("PrinterMarginsBottom", CustomPrinterExtendedSettings.PrinterMarginsBottom, RegistryValueKind.Unknown);

                        key.SetValue("HorizontalGutter", CustomPrinterExtendedSettings.HorizontalGutter, RegistryValueKind.Unknown);
                        key.SetValue("HorizontalOverlap", CustomPrinterExtendedSettings.HorizontalOverlap, RegistryValueKind.Unknown);
                        key.SetValue("VerticalGutter", CustomPrinterExtendedSettings.VerticalGutter, RegistryValueKind.Unknown);
                        key.SetValue("VerticalOverlap", CustomPrinterExtendedSettings.VerticalOverlap, RegistryValueKind.Unknown);

                        key.SetValue("ActualExtendedWidth", CustomPrinterExtendedSettings.AdjustActualPageHorizontalPageWidthMargin, RegistryValueKind.Unknown);
                        key.SetValue("ActualExtendedHeight", CustomPrinterExtendedSettings.AdjustActualPageVerticalPageHeightMargin, RegistryValueKind.Unknown);
                        key.SetValue("FitExtendedWidth", CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin, RegistryValueKind.Unknown);
                        key.SetValue("FitExtendedHeight", CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin, RegistryValueKind.Unknown);

                        key.SetValue("CurrentPageSize", (Int32)CustomPrinterExtendedSettings.CurrentPageSize, RegistryValueKind.Unknown);

                        key.SetValue("FooterTop", CustomPrinterExtendedSettings.FooterTop, RegistryValueKind.Unknown);
                        key.SetValue("FooterRight", CustomPrinterExtendedSettings.FooterRight, RegistryValueKind.Unknown);
                        key.SetValue("FooterLeft", CustomPrinterExtendedSettings.FooterLeft, RegistryValueKind.Unknown);
                        key.SetValue("FooterBottom", CustomPrinterExtendedSettings.FooterBottom, RegistryValueKind.Unknown);

                        FontConverter fcvt = new FontConverter();
                        string fontString = fcvt.ConvertToString(CustomPrinterExtendedSettings.FooterFont);
                        key.SetValue("FooterFont", fontString, RegistryValueKind.Unknown);
                        fcvt = null;
                        ColorConverter ccvt = new ColorConverter();
                        string colorString = ccvt.ConvertToString(CustomPrinterExtendedSettings.FooterColor);
                        key.SetValue("FooterColor", colorString, RegistryValueKind.Unknown);
                        ccvt = null;



                        key.SetValue("IsCustomDPI", CustomPrinterExtendedSettings.IsCustomDPI, RegistryValueKind.Unknown);
                        key.SetValue("CustomDPI", CustomPrinterExtendedSettings.CustomDPI, RegistryValueKind.Unknown);

                        key.SetValue("IsShowProgress", CustomPrinterExtendedSettings.IsShowProgress, RegistryValueKind.Unknown);
                        key.SetValue("IsBackGroundPrint", CustomPrinterExtendedSettings.IsBackGroundPrint, RegistryValueKind.Unknown);


                        //Aniket: This needs to be added
                        key.SetValue("IsHorizontalFlow", CustomPrinterExtendedSettings.IsHorizontalFlow, RegistryValueKind.Unknown);
                        key.SetValue("IsActualLandscape", CustomPrinterExtendedSettings.IsActualLandscape, RegistryValueKind.Unknown);
                        key.SetValue("IsActualMultiPage", CustomPrinterExtendedSettings.IsActualMultiPage, RegistryValueKind.Unknown);

                        //Graphics Settings
                        key.SetValue("NormalSettingsTop", this.CustomPrinterExtendedSettings.NormalSettings.Top, RegistryValueKind.Unknown);
                        key.SetValue("NormalSettingsRight", this.CustomPrinterExtendedSettings.NormalSettings.Right, RegistryValueKind.Unknown);
                        key.SetValue("NormalSettingsLeft", this.CustomPrinterExtendedSettings.NormalSettings.Left, RegistryValueKind.Unknown);
                        key.SetValue("NormalSettingsBottom", this.CustomPrinterExtendedSettings.NormalSettings.Bottom, RegistryValueKind.Unknown);
                        key.SetValue("NormalSettingsDpiX", this.CustomPrinterExtendedSettings.NormalSettings.DpiX, RegistryValueKind.Unknown);
                        key.SetValue("NormalSettingsDpiY", this.CustomPrinterExtendedSettings.NormalSettings.DpiY, RegistryValueKind.Unknown);
                        key.SetValue("NormalSettingsF0", this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[0], RegistryValueKind.Unknown);
                        key.SetValue("NormalSettingsF1", this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[1], RegistryValueKind.Unknown);
                        key.SetValue("NormalSettingsF2", this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[2], RegistryValueKind.Unknown);
                        key.SetValue("FlatSettingsTop", this.CustomPrinterExtendedSettings.FlatSettings.Top, RegistryValueKind.Unknown);
                        key.SetValue("FlatSettingsRight", this.CustomPrinterExtendedSettings.FlatSettings.Right, RegistryValueKind.Unknown);
                        key.SetValue("FlatSettingsLeft", this.CustomPrinterExtendedSettings.FlatSettings.Left, RegistryValueKind.Unknown);
                        key.SetValue("FlatSettingsBottom", this.CustomPrinterExtendedSettings.FlatSettings.Bottom, RegistryValueKind.Unknown);
                        key.SetValue("FlatSettingsDpiX", this.CustomPrinterExtendedSettings.FlatSettings.DpiX, RegistryValueKind.Unknown);
                        key.SetValue("FlatSettingsDpiY", this.CustomPrinterExtendedSettings.FlatSettings.DpiY, RegistryValueKind.Unknown);
                        key.SetValue("FlatSettingsF0", this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[0], RegistryValueKind.Unknown);
                        key.SetValue("FlatSettingsF1", this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[1], RegistryValueKind.Unknown);
                        key.SetValue("FlatSettingsF2", this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[2], RegistryValueKind.Unknown);
                    }
                }



                else
                {
                    //Save in the database
                    gloAuditTrail.MachineDetails.MachineInfo remoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails();
                    gloAuditTrail.MachineDetails.MachineInfo localMachine = gloAuditTrail.MachineDetails.LocalMachineDetails();

                    clsPrinterSettings.CreateSchemadtPrinterSettingsDetails(ref dtPrinterSettings);
                    dtPrinterSettings.TableName = "PrinterSettings";
                    AddPrinterSettingsToTable(ref dtPrinterSettings);

                    PrinterSettingsDetailsID = SavePrinterSettings(_UserID, remoteMachine.MachineName, localMachine.MachineName, dtPrinterSettings);

                }


            }

            catch (Exception ex)
            {
                //added for bugid 96341
                if (this.CustomPrinterExtendedSettings != null)
                {
                    if (this.CustomPrinterExtendedSettings.NormalSettings == null)
                    {
                        if (ex.Message.ToString().Contains("Object reference not set to an instance of an object"))
                        {
                            throw new Exception("Selected printer does not exist and might be deleted. Please select another printer.");
                            // ex = null;
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }

            finally
            {
                if (dtPrinterSettings != null)
                {
                    dtPrinterSettings.Dispose();
                    dtPrinterSettings = null;
                }

                if (key != null)
                {
                    key.Close(); key.Dispose(); key = null;
                }
            }



        }

        public void AddPrinterSettingsToTable(ref DataTable dtPrinterSettings)
        {
            dtPrinterSettings.Rows.Clear();
            dtPrinterSettings.Rows.Add(dtPrinterSettings.NewRow());
            dtPrinterSettings.Rows[0]["ModuleName"] = this.RegistryModuleName;
            dtPrinterSettings.Rows[0]["DocumentsLeft"] = m_normalLeft;
            dtPrinterSettings.Rows[0]["DocumentsTop"] = m_normalTop;
            dtPrinterSettings.Rows[0]["DocumentsWidth"] = m_normalWidth;
            dtPrinterSettings.Rows[0]["DocumentsHeight"] = m_normalHeight;
            dtPrinterSettings.Rows[0]["DocumentsPrinterMarginsTop"] = CustomPrinterExtendedSettings.PrinterMarginsTop;
            dtPrinterSettings.Rows[0]["DocumentsPrinterMarginsRight"] = CustomPrinterExtendedSettings.PrinterMarginsRight;
            dtPrinterSettings.Rows[0]["DocumentsPrinterMarginsLeft"] = CustomPrinterExtendedSettings.PrinterMarginsLeft;
            dtPrinterSettings.Rows[0]["DocumentsPrinterMarginsBottom"] = CustomPrinterExtendedSettings.PrinterMarginsBottom;
            dtPrinterSettings.Rows[0]["DocumentsHorizontalOverlap"] = CustomPrinterExtendedSettings.HorizontalOverlap;
            dtPrinterSettings.Rows[0]["DocumentsHorizontalGutter"] = CustomPrinterExtendedSettings.HorizontalGutter;
            dtPrinterSettings.Rows[0]["DocumentsVerticalOverlap"] = CustomPrinterExtendedSettings.VerticalOverlap;
            dtPrinterSettings.Rows[0]["DocumentsVerticalGutter"] = CustomPrinterExtendedSettings.VerticalGutter;
            dtPrinterSettings.Rows[0]["DocumentsActualExtendedWidth"] = CustomPrinterExtendedSettings.AdjustActualPageHorizontalPageWidthMargin;
            dtPrinterSettings.Rows[0]["DocumentsActualExtendedHeight"] = CustomPrinterExtendedSettings.AdjustActualPageVerticalPageHeightMargin;
            dtPrinterSettings.Rows[0]["DocumentsFitExtendedWidth"] = CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin;
            dtPrinterSettings.Rows[0]["DocumentsFitExtendedHeight"] = CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin;
            dtPrinterSettings.Rows[0]["DocumentsCurrentPageSize"] = Convert.ToString((Int32)CustomPrinterExtendedSettings.CurrentPageSize);
            dtPrinterSettings.Rows[0]["DocumentsFooterTop"] = CustomPrinterExtendedSettings.FooterTop;
            dtPrinterSettings.Rows[0]["DocumentsFooterRight"] = CustomPrinterExtendedSettings.FooterRight;
            dtPrinterSettings.Rows[0]["DocumentsFooterLeft"] = CustomPrinterExtendedSettings.FooterLeft;
            dtPrinterSettings.Rows[0]["DocumentsFooterBottom"] = CustomPrinterExtendedSettings.FooterBottom;


            FontConverter fcvt = new FontConverter();
            string fontString = fcvt.ConvertToString(CustomPrinterExtendedSettings.FooterFont);
            dtPrinterSettings.Rows[0]["DocumentsFooterFont"] = fontString;

            ColorConverter ccvt = new ColorConverter();
            string colorString = ccvt.ConvertToString(CustomPrinterExtendedSettings.FooterColor);
            dtPrinterSettings.Rows[0]["DocumentsFooterColor"] = colorString;


            dtPrinterSettings.Rows[0]["DocumentsIsCustomDPI"] = CustomPrinterExtendedSettings.IsCustomDPI;
            dtPrinterSettings.Rows[0]["DocumentsCustomDPI"] = CustomPrinterExtendedSettings.CustomDPI;
            dtPrinterSettings.Rows[0]["IsShowProgress"] = CustomPrinterExtendedSettings.IsShowProgress;
            dtPrinterSettings.Rows[0]["IsBackGroundPrint"] = CustomPrinterExtendedSettings.IsBackGroundPrint;
            dtPrinterSettings.Rows[0]["IsHorizontalFlow"] = CustomPrinterExtendedSettings.IsHorizontalFlow;
            dtPrinterSettings.Rows[0]["IsActualLandscape"] = CustomPrinterExtendedSettings.IsActualLandscape;
            dtPrinterSettings.Rows[0]["IsActualMultiPage"] = CustomPrinterExtendedSettings.IsActualMultiPage;


            //Grpahics Settings
            if (this.CustomPrinterExtendedSettings.NormalSettings != null)
            {
                dtPrinterSettings.Rows[0]["NormalSettingsTop"] = this.CustomPrinterExtendedSettings.NormalSettings.Top;
                dtPrinterSettings.Rows[0]["NormalSettingsRight"] = this.CustomPrinterExtendedSettings.NormalSettings.Right;
                dtPrinterSettings.Rows[0]["NormalSettingsLeft"] = this.CustomPrinterExtendedSettings.NormalSettings.Left;
                dtPrinterSettings.Rows[0]["NormalSettingsBottom"] = this.CustomPrinterExtendedSettings.NormalSettings.Bottom;
                dtPrinterSettings.Rows[0]["NormalSettingsDpiX"] = this.CustomPrinterExtendedSettings.NormalSettings.DpiX;
                dtPrinterSettings.Rows[0]["NormalSettingsDpiY"] = this.CustomPrinterExtendedSettings.NormalSettings.DpiY;
                dtPrinterSettings.Rows[0]["NormalSettingsF0"] = this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[0];
                dtPrinterSettings.Rows[0]["NormalSettingsF1"] = this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[1];
                dtPrinterSettings.Rows[0]["NormalSettingsF2"] = this.CustomPrinterExtendedSettings.NormalSettings.FontHeight[2];
            }
            if (this.CustomPrinterExtendedSettings.FlatSettings != null)
            {
                dtPrinterSettings.Rows[0]["FlatSettingsTop"] = this.CustomPrinterExtendedSettings.FlatSettings.Top;
                dtPrinterSettings.Rows[0]["FlatSettingsRight"] = this.CustomPrinterExtendedSettings.FlatSettings.Right;
                dtPrinterSettings.Rows[0]["FlatSettingsLeft"] = this.CustomPrinterExtendedSettings.FlatSettings.Left;
                dtPrinterSettings.Rows[0]["FlatSettingsBottom"] = this.CustomPrinterExtendedSettings.FlatSettings.Bottom;
                dtPrinterSettings.Rows[0]["FlatSettingsDpiX"] = this.CustomPrinterExtendedSettings.FlatSettings.DpiX;
                dtPrinterSettings.Rows[0]["FlatSettingsDpiY"] = this.CustomPrinterExtendedSettings.FlatSettings.DpiY;
                dtPrinterSettings.Rows[0]["FlatSettingsF0"] = this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[0];
                dtPrinterSettings.Rows[0]["FlatSettingsF1"] = this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[1];
                dtPrinterSettings.Rows[0]["FlatSettingsF2"] = this.CustomPrinterExtendedSettings.FlatSettings.FontHeight[2];
            }
        }

        private Int64 SavePrinterSettings(long UserID, string RemoteMachineName, string LocalMachineName, System.Data.DataTable PrinterSettings)
        {

            if (bGetSettingsFromDB == true && ConnectionString != null && ConnectionString != "")
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                string strPrinterSettingsString;
                Int64 PrinterSettingsDetailsID = 0;
                try
                {
                    if (PrinterSettings != null && PrinterSettings.Rows.Count > 0)
                    {
                        oDB.Connect(false);

                        strPrinterSettingsString = clsPrinterSettings.PrinterSettingsToString(this.PrinterSettings);


                        oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);

                        oDBParameters.Add("@RemoteMachineName", RemoteMachineName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@LocalMachineName", LocalMachineName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                        oDBParameters.Add("@PrinterName", this.PrinterSettings.PrinterName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                        oDBParameters.Add("@PrinterSettings", strPrinterSettingsString, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                        oDBParameters.Add("@tvpPrinterSettingsDetails", PrinterSettings, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Structured);
                        PrinterSettingsDetailsID = Convert.ToInt64(oDB.ExecuteScalar("gsp_SavePrinterSettings", oDBParameters));
        
                      //  oDB.Execute("gsp_SavePrinterSettings", oDBParameters);
                    }
                    return PrinterSettingsDetailsID;

                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    throw ex;
                }

                catch (Exception ex)
                {

                    throw ex;
                }

                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }

            }
            else
            {
                return 0;
            }
        }

        public void SetPrinter()
        {
            // save position, size and state
            if (!bGetSettingsFromDB && !gloGlobal.gloTSPrint.isTSPrintServiceCall)
            {
                RegistryKey key = null;
                try
                {

                    if (IsSaveExtendedSettings != true)
                    {
                        key = Registry.CurrentUser.OpenSubKey(m_sRegistryPath, true);
                    }
                    else
                    {
                        key = Registry.LocalMachine.OpenSubKey(m_sRegistryPath, true);
                    }
                    if (key == null)
                    {
                        if (IsSaveExtendedSettings != true)
                        {
                            key = Registry.CurrentUser.CreateSubKey(m_sRegistryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                        }
                        else
                        {
                            key = Registry.LocalMachine.CreateSubKey(m_sRegistryPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                        }
                    }

                    string printerstring = clsPrinterSettings.PrinterSettingsToString(this.PrinterSettings);
                    key.SetValue("PrinterSettings", printerstring);

                    if (bGetSettingsFromDB)
                    {
                        this.AddSetting(this.ModuleName + "PrinterSettings", printerstring, this.ClinicId, this._UserID, SettingFlag.User);
                    }
                }

                finally
                {
                    if (key != null)
                    {
                        key.Close(); key.Dispose(); key = null;
                    }
                }

            }

        }

        private void OnMove(object sender, System.EventArgs e)
        {
            RECT rect = new RECT();
            if (Win32.GetWindowRect(this.Handle, ref rect))
            {
                // save position			
                m_normalLeft = rect.left;
                m_normalTop = rect.top;
            }

        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if called by Dispose, false otherwise</param>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                //       
                if (this.ExtendedPropertiesControl != null)
                {
                    this.ExtendedPropertiesControl.Dispose();
                    this.ExtendedPropertiesControl = null;
                }
                if (_CustomPrinterExtendedSettings != null)
                {
                    _CustomPrinterExtendedSettings.Dispose();
                    _CustomPrinterExtendedSettings = null;
                }
                GC.SuppressFinalize(this);
            }
            //Marshal.FreeCoTaskMem(_ipTemplate);
            //for (int nIndex = 0; nIndex < this.m_oResourceList.Count; nIndex++)
            //{
            //    IntPtr nHandle = (IntPtr)this.m_oResourceList[nIndex];
            //    Win32.DestroyWindow(nHandle);
            //}

            //this.m_oResourceList.Clear();
        }

        #endregion
    }
    /// <summary>
    /// system class names that are available for use by all processes. 
    /// </summary>
    internal class WndType
    {
        public const string LABEL = "static";		// The class for a static control. 
        public const string BUTTON = "button";		// The class for a button. 
        public const string EDITBOX = "edit";		// The class for an edit control. 
        public const string COMBOBOX = "combobox";	// The class for a combo box. 
        public const string LISTBOX = "listbox";	// The class for a list box. 
        public const string ScrollBar = "scrollbar";	// The class for a scroll bar. 
    };

    /// <summary>
    /// Defines the constants for Windows messages.
    /// A small subset of the window messages that can be sent to the PrintDlg
    /// These are just the ones that this implementation is interested in
    /// </summary>
    internal class WndMsg
    {
        public const UInt16 WM_INITDIALOG = 0x0110;
        public const UInt16 WM_DESTROY = 0x0002;
        public const UInt16 WM_SIZE = 0x0005;
        public const UInt16 WM_NOTIFY = 0x004E;
        public const UInt16 WM_SETFOCUS = 0x0007;
        public const UInt16 WM_LBUTTONDOWN = 0x0201;
        public const UInt16 WM_RBUTTONDOWN = 0x0204;
        public const UInt16 WM_MOVE = 0x0003;
        public const UInt16 WM_SETFONT = 0x0030;
        public const UInt16 WM_GETFONT = 0x0031;
        public const UInt16 WM_COMMAND = 0x0111;
        public const UInt16 IDOK = 1;

    };

    internal class WndZOrder
    {
        public const UInt32 HWND_TOP = 0x00000000;
        public const UInt32 HWND_BOTTOM = 0x00000001;
        public const UInt32 HWND_TOPMOST = 0xFFFFFFFF;
        public const UInt32 HWND_NOTOPMOST = 0xFFFFFFFE;
    };

    /*
     * ShowWindow() Commands
     */
    internal class ShowWnd
    {
        public const UInt16 SW_HIDE = 0x0000;
        public const UInt16 SW_SHOWNORMAL = 0x0001;
        public const UInt16 SW_NORMAL = 0x0001;
        public const UInt16 SW_SHOWMINIMIZED = 0x0002;
        public const UInt16 SW_SHOWMAXIMIZED = 0x0003;
        public const UInt16 SW_MAXIMIZE = 0x0003;
        public const UInt16 SW_SHOWNOACTIVATE = 0x0004;
        public const UInt16 SW_SHOW = 0x0005;
        public const UInt16 SW_MINIMIZE = 0x0006;
        public const UInt16 SW_SHOWMINNOACTIVE = 0x0007;
        public const UInt16 SW_SHOWNA = 0x0008;
        public const UInt16 SW_RESTORE = 0x0009;
        public const UInt16 SW_SHOWDEFAULT = 0x0010;
        public const UInt16 SW_FORCEMINIMIZE = 0x0011;
        public const UInt16 SW_MAX = 0x0011;
    };

    /*
    * SetWindowPos Flags
    */
    internal class WndPos
    {
        static public UInt16 SWP_NOSIZE = 0x0001;
        static public UInt16 SWP_NOMOVE = 0x0002;
        static public UInt16 SWP_NOZORDER = 0x0004;
        static public UInt16 SWP_NOREDRAW = 0x0008;
        static public UInt16 SWP_NOACTIVATE = 0x0010;
        static public UInt16 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        static public UInt16 SWP_SHOWWINDOW = 0x0040;
        static public UInt16 SWP_HIDEWINDOW = 0x0080;
        static public UInt16 SWP_NOCOPYBITS = 0x0100;
        static public UInt16 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        static public UInt16 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */
    };

    /// <summary>
    /// Win32 window style constants
    /// We use them to set up our child window
    /// </summary>
    internal class WndStyles
    {
        public const UInt32 WS_VISIBLE = 0x10000000;
        public const UInt32 WS_CHILD = 0x40000000;
        public const UInt32 WS_TABSTOP = 0x00010000;
        public const UInt32 DsSetFont = 0x00000040;
        public const UInt32 Ds3dLook = 0x00000004;
        public const UInt32 DsControl = 0x00000400;
        public const UInt32 WsClipSiblings = 0x04000000;
        public const UInt32 WsGroup = 0x00020000;
        public const UInt32 SsNotify = 0x00000100;
    };

    /// <summary>
    /// Win32 "extended" window style constants
    /// </summary>
    internal class ExStyle
    {
        public const Int32 WsExNoParentNotify = 0x00000004;
        public const Int32 WsExControlParent = 0x00010000;
    };


    /// <summary>
    /// Button Control Styles
    /// </summary>
    internal class BtnStyle
    {
        public const Int32 BS_PUSHBUTTON = 0x00000000;
        public const Int32 BS_DEFPUSHBUTTON = 0x00000001;
        public const Int32 BS_CHECKBOX = 0x00000002;
        public const Int32 BS_AUTOCHECKBOX = 0x00000003;
        public const Int32 BS_RADIOBUTTON = 0x00000004;
        public const Int32 BS_3STATE = 0x00000005;
        public const Int32 BS_AUTO3STATE = 0x00000006;
        public const Int32 BS_GROUPBOX = 0x00000007;
        public const Int32 BS_USERBUTTON = 0x00000008;
        public const Int32 BS_AUTORADIOBUTTON = 0x00000009;
        public const Int32 BS_PUSHBOX = 0x0000000A;
        public const Int32 BS_OWNERDRAW = 0x0000000B;
        public const Int32 BS_TYPEMASK = 0x0000000F;
        public const Int32 BS_LEFTTEXT = 0x00000020;
    }
    /// <summary>
    /// Combo Box Notification Codes
    /// </summary>
    internal class ComboBoxSNotif
    {
        public const UInt16 CBN_ERRSPACE = 0xFFFF;
        public const UInt16 CBN_SELCHANGE = 1;
        public const UInt16 CBN_DBLCLK = 2;
        public const UInt16 CBN_SETFOCUS = 3;
        public const UInt16 CBN_KILLFOCUS = 4;
        public const UInt16 CBN_EDITCHANGE = 5;
        public const UInt16 CBN_EDITUPDATE = 6;
        public const UInt16 CBN_DROPDOWN = 7;
        public const UInt16 CBN_CLOSEUP = 8;
        public const UInt16 CBN_SELENDOK = 9;
        public const UInt16 CBN_SELENDCANCEL = 10;
    };

    /// <summary>
    /// Combo Box styles
    /// </summary>
    internal class ComboBoxStyles
    {
        public const UInt16 CBS_SIMPLE = 0x0001;
        public const UInt16 CBS_DROPDOWN = 0x0002;
        public const UInt16 CBS_DROPDOWNLIST = 0x0003;
        public const UInt16 CBS_OWNERDRAWFIXED = 0x0010;
        public const UInt16 CBS_OWNERDRAWVARIABLE = 0x0020;
        public const UInt16 CBS_AUTOHSCROLL = 0x0040;
        public const UInt16 CBS_OEMCONVERT = 0x0080;
        public const UInt16 CBS_SORT = 0x0100;
        public const UInt16 CBS_HASSTRINGS = 0x0200;
        public const UInt16 CBS_NOINTEGRALHEIGHT = 0x0400;
        public const UInt16 CBS_DISABLENOSCROLL = 0x0800;
    }


    /// <summary>
    /// Combo Box messages
    /// </summary>
    internal class ComboBoxMsg
    {
        public const UInt32 CB_GETEDITSEL = 0x0140;
        public const UInt32 CB_LIMITTEXT = 0x0141;
        public const UInt32 CB_SETEDITSEL = 0x0142;
        public const UInt32 CB_ADDSTRING = 0x0143;
        public const UInt32 CB_DELETESTRING = 0x0144;
        public const UInt32 CB_DIR = 0x0145;
        public const UInt32 CB_GETCOUNT = 0x0146;
        public const UInt32 CB_GETCURSEL = 0x0147;
        public const UInt32 CB_GETLBTEXT = 0x0148;
        public const UInt32 CB_GETLBTEXTLEN = 0x0149;
        public const UInt32 CB_INSERTSTRING = 0x014A;
        public const UInt32 CB_RESETCONTENT = 0x014B;
        public const UInt32 CB_FINDSTRING = 0x014C;
        public const UInt32 CB_SELECTSTRING = 0x014D;
        public const UInt32 CB_SETCURSEL = 0x014E;
        public const UInt32 CB_SHOWDROPDOWN = 0x014F;
        public const UInt32 CB_GETITEMDATA = 0x0150;
        public const UInt32 CB_SETITEMDATA = 0x0151;
        public const UInt32 CB_GETDROPPEDCONTROLRECT = 0x0152;
        public const UInt32 CB_SETITEMHEIGHT = 0x0153;
        public const UInt32 CB_GETITEMHEIGHT = 0x0154;
        public const UInt32 CB_SETEXTENDEDUI = 0x0155;
        public const UInt32 CB_GETEXTENDEDUI = 0x0156;
        public const UInt32 CB_GETDROPPEDSTATE = 0x0157;
        public const UInt32 CB_FINDSTRINGEXACT = 0x0158;
        public const UInt32 CB_SETLOCALE = 0x0159;
        public const UInt32 CB_GETLOCALE = 0x015A;
    }
    /// <summary>
    /// Defines the shape of hook procedures that can be called by the PrintDlg
    /// </summary>
    internal delegate IntPtr OfnHookProc(IntPtr hWnd, UInt16 msg, Int32 wParam, Int32 lParam);

    internal delegate bool CallBack(int hwnd, int lParam);


    /// <summary>
    /// Part of the notification messages sent by the common dialogs
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct NMHDR
    {
        [FieldOffset(0)]
        public IntPtr hWndFrom;
        [FieldOffset(4)]
        public UInt16 idFrom;
        [FieldOffset(8)]
        public UInt16 code;
    };

    /// <summary>
    /// Part of the notification messages sent by the common dialogs
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal struct OfNotify
    {
        [FieldOffset(0)]
        public NMHDR hdr;
        [FieldOffset(12)]
        public IntPtr ipOfn;
        [FieldOffset(16)]
        public IntPtr ipFile;
    };

    /// <summary>
    /// An in-memory Win32 dialog template
    /// Note: this has a very specific structure with a single static "label" control
    /// See documentation for DLGTEMPLATE and DLGITEMTEMPLATE
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class DlgTemplate
    {
        // The dialog template - see documentation for DLGTEMPLATE
        public UInt32 style = WndStyles.Ds3dLook | WndStyles.DsControl | WndStyles.WS_CHILD | WndStyles.WsClipSiblings | WndStyles.SsNotify;
        public Int32 extendedStyle = ExStyle.WsExControlParent;
        public Int16 numItems = 1;
        public Int16 x = 0;
        public Int16 y = 0;
        public Int16 cx = 0;
        public Int16 cy = 0;
        public Int16 reservedMenu = 0;
        public Int16 reservedClass = 0;
        public Int16 reservedTitle = 0;

        // Single dlg item, must be dword-aligned - see documentation for DLGITEMTEMPLATE
        public UInt32 itemStyle = WndStyles.WS_CHILD;
        public Int32 itemExtendedStyle = ExStyle.WsExNoParentNotify;
        public Int16 itemX = 0;
        public Int16 itemY = 0;
        public Int16 itemCx = 0;
        public Int16 itemCy = 0;
        public Int16 itemId = 0;
        public UInt16 itemClassHdr = 0xffff;	// we supply a constant to indicate the class of this control
        public Int16 itemClass = 0x0082;	// static label control
        public Int16 itemText = 0x0000;	// no text for this control
        public Int16 itemData = 0x0000;	// no creation data for this control
    };

    /// <summary>
    /// The rectangle structure used in Win32 API calls
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    };

    /// <summary>
    /// The point structure used in Win32 API calls
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int X;
        public int Y;
    };
    /// <summary>
    /// Structure of DEVMODE
    /// </summary>
    [Serializable()]
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
    public class DEVMODE_TYPE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public String dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public short dmOrientation;
        public short dmPaperSize;
        public short dmPaperLength;
        public short dmPaperWidth;
        public short dmScale;
        public short dmCopies;
        public short dmDefaultSource;
        public short dmPrintQuality;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public String dmFormName;
        public short dmUnusedPadding;
        public short dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;

    }
    /// <summary>
    /// Structure of DevNames
    /// </summary>
    [Serializable()]
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
    public class DEVNAMES_TYPE
    {
        public short wDriverOffset;
        public short wDeviceOffset;
        public short wOutputOffset;
        public short wDefault;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public String extra;
    }




    /// <summary>
    /// Contains all of the p/invoke declarations for the Win32 APIs.
    /// </summary>
    public class Win32
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetDlgItem(IntPtr hWndDlg, Int16 Id);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        internal static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, StringBuilder buffer);

        [DllImport("user32.dll")]
        internal static extern UInt32 SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, UInt32 lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT rc);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetClientRect(IntPtr hWnd, ref RECT rc);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool ScreenToClient(IntPtr hWnd, ref POINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool repaint);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool SetWindowPos(IntPtr hWnd, UInt32 hWndInsertAfter, int X, int Y, int Width, int Height, UInt16 uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("ComDlg32.dll", CharSet = CharSet.Unicode)]
        internal static extern Int32 CommDlgExtendedError();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int hMenu, int hInstance, int lpParam);

        [DllImport("user32.dll")]
        internal static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("user32.Dll")]
        internal static extern int EnumWindows(CallBack x, int y);

        [DllImport("user32.Dll")]
        internal static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpEnumFunc, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("User32.Dll")]
        internal static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);
        [DllImport("User32.Dll")]
        internal static extern void GetClassName(int h, StringBuilder s, int nMaxCount);
        [DllImport("User32.Dll")]
        internal static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GlobalFree(IntPtr hMem);
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        internal static extern bool DeleteDC([In] IntPtr hdc);
        [DllImport("kernel32.dll", EntryPoint = "GlobalAlloc")]
        internal static extern IntPtr GlobalAlloc(int wFlags, int dwBytes);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalUnlock(IntPtr handle);
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int DocumentProperties(IntPtr hWnd, IntPtr hPrinter, string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, fModes fMode);
        [Flags]
        internal enum fModes
        {
            /// <summary>
            /// When used, the DocumentProperties function returns the number
            /// of bytes required by the printer driver's DEVMODE data structure.
            /// </summary>
            DM_SIZEOF = 0,

            /// <summary>
            /// <see cref="DM_OUT_DEFAULT"/>
            /// </summary>
            DM_UPDATE = 1,

            /// <summary>
            /// <see cref="DM_OUT_BUFFER"/>
            /// </summary>
            DM_COPY = 2,

            /// <summary>
            /// <see cref="DM_IN_PROMPT"/>
            /// </summary>
            DM_PROMPT = 4,

            /// <summary>
            /// <see cref="DM_IN_BUFFER"/>
            /// </summary>
            DM_MODIFY = 8,

            /// <summary>
            /// No description available.
            /// </summary>
            DM_OUT_DEFAULT = DM_UPDATE,

            /// <summary>
            /// Output value. The function writes the printer driver's current print settings,
            /// including private data, to the DEVMODE data structure specified by the 
            /// pDevModeOutput parameter. The caller must allocate a buffer sufficiently large
            /// to contain the information. 
            /// If the bit DM_OUT_BUFFER sets is clear, the pDevModeOutput parameter can be NULL.
            /// This value is also defined as <see cref="DM_COPY"/>.
            /// </summary>
            DM_OUT_BUFFER = DM_COPY,

            /// <summary>
            /// Input value. The function presents the printer driver's Print Setup property
            /// sheet and then changes the settings in the printer's DEVMODE data structure
            /// to those values specified by the user. 
            /// This value is also defined as <see cref="DM_PROMPT"/>.
            /// </summary>
            DM_IN_PROMPT = DM_PROMPT,

            /// <summary>
            /// Input value. Before prompting, copying, or updating, the function merges 
            /// the printer driver's current print settings with the settings in the DEVMODE
            /// structure specified by the pDevModeInput parameter. 
            /// The function updates the structure only for those members specified by the
            /// DEVMODE structure's dmFields member. 
            /// This value is also defined as <see cref="DM_MODIFY"/>. 
            /// In cases of conflict during the merge, the settings in the DEVMODE structure 
            /// specified by pDevModeInput override the printer driver's current print settings.
            /// </summary>
            DM_IN_BUFFER = DM_MODIFY,
        }
        //Win32 constants global memory  
        internal const int GMEM_MOVEABLE = 0x2;
        internal const int GMEM_ZEROPOINT = 0x40;
        internal const int GMEM_ZEROINIT = 0x40;
    }
    /// <summary>
    /// This class is a workaround to serialize and deserialize printersettings, because there is (a bug?)
    /// in the dotNetFramework. It´s not possible to serialize the PrinterSettings object.
    /// </summary>
    [Serializable()]
    public class clsPrinterSettings
    {
        string sPrinterName;

        bool bCollate;
        int iCopies;
        int iDuplex;
        int iFromPage;
        int iMaximumPage;
        int iMinimumPage;
        int iPrintRange;
        int iToPage;

        int idevMode;
        byte[] buffer_devMode;

        int idevNames;
        byte[] buffer_devNames;


        public clsPrinterSettings()
        {
        }



        public static DataTable LoadRegistrySettings(DataTable dtPrinterSettingsDetails, string RegistryModuleName, string ModuleName)
        {

            //Load Printer Details
            RegistryKey key = null;
            string m_sRegistryPath = "";


            m_sRegistryPath = gloPrintDialog._BaseRegistry + @"\" + RegistryModuleName;
            //Aniket: To check the following
            //if (IsSaveExtendedSettings != true)
            //{
            key = Registry.CurrentUser.OpenSubKey(m_sRegistryPath, true);
            //}
            //else
            //{
            //    key = Registry.LocalMachine.OpenSubKey(m_sRegistryPath, true);
            //}


            if (key != null)
            {

                dtPrinterSettingsDetails.Rows.Add(dtPrinterSettingsDetails.NewRow());
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["ModuleName"] = ModuleName;
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsLeft"] = (int)key.GetValue("Left", 0);
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsTop"] = (int)key.GetValue("Top", 0);
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsWidth"] = (int)key.GetValue("Width", 0);
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsHeight"] = (int)key.GetValue("Height", 0);


                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsPrinterMarginsTop"] = (float)Convert.ToDouble(key.GetValue("PrinterMarginsTop", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsPrinterMarginsRight"] = (float)Convert.ToDouble(key.GetValue("PrinterMarginsRight", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsPrinterMarginsLeft"] = (float)Convert.ToDouble(key.GetValue("PrinterMarginsLeft", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsPrinterMarginsBottom"] = (float)Convert.ToDouble(key.GetValue("PrinterMarginsBottom", 0));

                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsHorizontalGutter"] = (float)Convert.ToDouble(key.GetValue("HorizontalGutter", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsVerticalGutter"] = (float)Convert.ToDouble(key.GetValue("VerticalGutter", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsHorizontalOverlap"] = (float)Convert.ToDouble(key.GetValue("HorizontalOverlap", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsVerticalOverlap"] = (float)Convert.ToDouble(key.GetValue("VerticalOverlap", 0));

                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsActualExtendedWidth"] = (float)Convert.ToDouble(key.GetValue("ActualExtendedWidth", "50.9"));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsActualExtendedHeight"] = (float)Convert.ToDouble(key.GetValue("ActualExtendedHeight", "50.9"));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsFitExtendedWidth"] = (float)Convert.ToDouble(key.GetValue("FitExtendedWidth", "0"));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsFitExtendedHeight"] = (float)Convert.ToDouble(key.GetValue("FitExtendedHeight", "0"));

                try
                {
                    //29-Jul-16 Aniket: Resolving Incident 00064303 8071 - PROBLEMS PRINTING FROM DMS 
                    dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsCurrentPageSize"] = (gloExtendedPrinterSettings.PageSize)Convert.ToInt32(key.GetValue("CurrentPageSize", 1));
                }
                catch
                {
                    dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsCurrentPageSize"] = (gloExtendedPrinterSettings.PageSize.FitToPage);
                }

                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsFooterTop"] = (float)Convert.ToDouble(key.GetValue("FooterTop", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsFooterRight"] = (float)Convert.ToDouble(key.GetValue("FooterRight", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsFooterLeft"] = (float)Convert.ToDouble(key.GetValue("FooterLeft", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsFooterBottom"] = (float)Convert.ToDouble(key.GetValue("FooterBottom", 0));

                FontConverter fcvt = new FontConverter();
                string defaultString = fcvt.ConvertToString(SystemFonts.CaptionFont);
                string defaulter = Convert.ToString(key.GetValue("FooterFont", defaultString));
                Font storedFont = null;

                try
                {
                    storedFont = fcvt.ConvertFromString(defaulter) as Font;
                }
                catch
                {
                }

                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsFooterFont"] = Convert.ToString(key.GetValue("FooterFont", defaultString));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsFooterColor"] = Convert.ToString(key.GetValue("FooterColor", defaultString));

                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsIsCustomDPI"] = Convert.ToBoolean(key.GetValue("IsCustomDPI", false));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["DocumentsCustomDPI"] = (Int32)key.GetValue("CustomDPI", 0);

                //11-Aug-16 Aniket: As per requirement from John, show the print progress screen and print in background by default.
            
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["IsShowProgress"] = true; //System.Convert.ToBoolean(key.GetValue("IsShowProgress", true));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["IsBackGroundPrint"] = true; //System.Convert.ToBoolean(key.GetValue("IsBackGroundPrint", false));

                //Aniket: This needs to be set Done
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["IsHorizontalFlow"] = Convert.ToBoolean(key.GetValue("IsHorizontalFlow", true));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["IsActualLandscape"] = System.Convert.ToBoolean(key.GetValue("IsActualLandscape", false));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["IsActualMultipage"] = System.Convert.ToBoolean(key.GetValue("IsActualMultipage", false));

                //Graphics Settings
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["NormalSettingsTop"] = (float)Convert.ToDouble(key.GetValue("NormalSettingsTop", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["NormalSettingsRight"] = (float)Convert.ToDouble(key.GetValue("NormalSettingsRight", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["NormalSettingsLeft"] = (float)Convert.ToDouble(key.GetValue("NormalSettingsLeft", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["NormalSettingsBottom"] = (float)Convert.ToDouble(key.GetValue("NormalSettingsBottom", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["NormalSettingsDpiX"] = (float)Convert.ToDouble(key.GetValue("NormalSettingsDpiX", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["NormalSettingsDpiY"] = (float)Convert.ToDouble(key.GetValue("NormalSettingsDpiY", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["NormalSettingsF0"] = (float)Convert.ToDouble(key.GetValue("NormalSettingsF0", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["NormalSettingsF1"] = (float)Convert.ToDouble(key.GetValue("NormalSettingsF1", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["NormalSettingsF2"] = (float)Convert.ToDouble(key.GetValue("NormalSettingsF2", 0));
                //dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["IsActualMultipage"] = (this.CustomPrinterExtendedSettings.NormalSettings.DpiX == 0) || (this.CustomPrinterExtendedSettings.NormalSettings.DpiY == 0);

                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["FlatSettingsTop"] = (float)Convert.ToDouble(key.GetValue("FlatSettingsTop", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["FlatSettingsRight"] = (float)Convert.ToDouble(key.GetValue("FlatSettingsRight", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["FlatSettingsLeft"] = (float)Convert.ToDouble(key.GetValue("FlatSettingsLeft", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["FlatSettingsBottom"] = (float)Convert.ToDouble(key.GetValue("FlatSettingsBottom", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["FlatSettingsDpiX"] = (float)Convert.ToDouble(key.GetValue("FlatSettingsDpiX", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["FlatSettingsDpiY"] = (float)Convert.ToDouble(key.GetValue("FlatSettingsDpiY", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["FlatSettingsF0"] = (float)Convert.ToDouble(key.GetValue("FlatSettingsF0", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["FlatSettingsF1"] = (float)Convert.ToDouble(key.GetValue("FlatSettingsF1", 0));
                dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["FlatSettingsF2"] = (float)Convert.ToDouble(key.GetValue("FlatSettingsF2", 0));
                // dtPrinterSettingsDetails.Rows[dtPrinterSettingsDetails.Rows.Count - 1]["IsActualMultipage"] = (this.CustomPrinterExtendedSettings.FlatSettings.DpiX == 0) || (this.CustomPrinterExtendedSettings.FlatSettings.DpiY == 0);
            }


            return dtPrinterSettingsDetails;
        }



        public static DataTable GetPrinterSettings(string ConnectionString, Int64 PrinterSettingsID)
        {

            DataTable dtPrinterSettings = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
            gloDatabaseLayer.DBParameters oPara = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);

                oPara.Add("@PrinterSettingsDetailsID", PrinterSettingsID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GetPrinterSettingsByPrinterSettingsID", oPara, out dtPrinterSettings);
                oDB.Disconnect();


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                throw ex;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oPara.Dispose();
                oPara = null;

                oDB.Disconnect();
                oDB.Dispose();
            }

            return dtPrinterSettings;
        }

        public static DataTable GetPrinterSettings(string ConnectionString, string ModuleName, Int64 PrinterSettingsID, Int64 UserID, string PrinterName)
        {

            DataTable dtPrinterSettings = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
            gloDatabaseLayer.DBParameters oPara = new gloDatabaseLayer.DBParameters();
            gloAuditTrail.MachineDetails.MachineInfo remoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails();
            gloAuditTrail.MachineDetails.MachineInfo localMachine = gloAuditTrail.MachineDetails.LocalMachineDetails();

            try
            {
                oDB.Connect(false);

                oPara.Add("@PrinterSettingsID", PrinterSettingsID, ParameterDirection.Input, SqlDbType.BigInt);
                oPara.Add("@ModuleName", ModuleName, ParameterDirection.Input, SqlDbType.VarChar);
                oPara.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.VarChar);

                oPara.Add("@RemoteMachineName", remoteMachine.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oPara.Add("@LocalMachineName", localMachine.MachineName, ParameterDirection.Input, SqlDbType.VarChar);

                oPara.Add("@PrinterName", PrinterName, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Retrive("gsp_GetPrinterSettings", oPara, out dtPrinterSettings);
                oDB.Disconnect();


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                throw ex;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oPara.Clear(); 
                oPara.Dispose();
                oPara = null;
                oDB.Disconnect();
                oDB.Dispose();
            }

            return dtPrinterSettings;
        }

        public static DataTable LoadUserPrinters(string ConnectionString, Int64 UserID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
            gloDatabaseLayer.DBParameters oPara = new gloDatabaseLayer.DBParameters();

            gloAuditTrail.MachineDetails.MachineInfo remoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails();
            gloAuditTrail.MachineDetails.MachineInfo localMachine = gloAuditTrail.MachineDetails.LocalMachineDetails();

            DataTable dtPrinters;

            try
            {
                oDB.Connect(false);
                dtPrinters = new DataTable();

                oPara.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);

                oPara.Add("@LocalMachineName", localMachine.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oPara.Add("@RemoteMachineName", remoteMachine.MachineName, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Retrive("gsp_LoadUserPrinters", oPara, out dtPrinters);

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                throw ex;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

            return dtPrinters;
        }

        public static void LoadPrinters(string ConnectionString, Int64 UserID)
        {
            //This function is called at the startup and will migrate the printer extended settings from the registry and save to the database.
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ConnectionString);
            gloDatabaseLayer.DBParameters oPara = null;
            bool blnDefaultPrinterLoaded = true;

            DataTable dtPrinters = new DataTable();
            DataTable dtPrinterSettingsDetails = new DataTable();
            System.Drawing.Printing.PrintDocument pdPrinterSettings = new System.Drawing.Printing.PrintDocument();

            gloAuditTrail.MachineDetails.MachineInfo remoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails();
            gloAuditTrail.MachineDetails.MachineInfo localMachine = gloAuditTrail.MachineDetails.LocalMachineDetails();
            string SessionID = System.Diagnostics.Process.GetCurrentProcess().SessionId.ToString();

            try
            {

                oDB.Connect(false);
                oPara = new gloDatabaseLayer.DBParameters();
                oPara.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oPara.Add("@RemoteMachineName", remoteMachine.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oPara.Add("@LocalMachineName", localMachine.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oPara.Add("@NEWSessionID", SessionID, ParameterDirection.Input, SqlDbType.VarChar);

                blnDefaultPrinterLoaded = Convert.ToBoolean(oDB.ExecuteScalar("gsp_CheckDefaultPrinters", oPara));
                oDB.Disconnect();

                if (oPara != null)
                {
                    oPara.Clear();
                    oPara.Dispose();
                    oPara = null;
                }

                if (blnDefaultPrinterLoaded == false)
                {
                    dtPrinters.Columns.Add("PrinterName", System.Type.GetType("System.String"));
                    dtPrinters.Columns.Add("nUserID", System.Type.GetType("System.Int64"));
                    dtPrinters.Columns.Add("MachineName", System.Type.GetType("System.String"));
                    dtPrinters.Columns.Add("PrinterSettings", System.Type.GetType("System.String"));
                    dtPrinters.Columns.Add("LocalMachineName", System.Type.GetType("System.String"));
                    dtPrinters.Columns.Add("SessionID", System.Type.GetType("System.String"));

                    CreateSchemadtPrinterSettingsDetails(ref dtPrinterSettingsDetails);

                    //Load Printer Master
                    for (int intPrinters = 0; intPrinters <= PrinterSettings.InstalledPrinters.Count - 1; intPrinters++)
                    {
                        pdPrinterSettings.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[intPrinters];

                        dtPrinters.Rows.Add(dtPrinters.NewRow());
                        dtPrinters.Rows[intPrinters]["PrinterName"] = PrinterSettings.InstalledPrinters[intPrinters];
                        dtPrinters.Rows[intPrinters]["nUserID"] = UserID;
                        dtPrinters.Rows[intPrinters]["MachineName"] = remoteMachine.MachineName;
                        dtPrinters.Rows[intPrinters]["PrinterSettings"] = PrinterSettingsToString(pdPrinterSettings.PrinterSettings);
                        dtPrinters.Rows[intPrinters]["LocalMachineName"] = localMachine.MachineName;
                        dtPrinters.Rows[intPrinters]["SessionID"] = SessionID;
                    }


                    dtPrinterSettingsDetails = LoadRegistrySettings(dtPrinterSettingsDetails, "ClinicalCharts", "ClinicalCharts");
                    dtPrinterSettingsDetails = LoadRegistrySettings(dtPrinterSettingsDetails, "PrintBatchDocuments", "PrintBatchDocuments");
                    dtPrinterSettingsDetails = LoadRegistrySettings(dtPrinterSettingsDetails, "RCMDocuments", "RCMDocuments");
                    dtPrinterSettingsDetails = LoadRegistrySettings(dtPrinterSettingsDetails, "DMSDocuments", "DMSDocuments");
                    dtPrinterSettingsDetails = LoadRegistrySettings(dtPrinterSettingsDetails, "NYWCForms", "NYWCForms");

                    //Save to Database
                    oDB.Connect(false);
                    oPara = new gloDatabaseLayer.DBParameters();
                    oPara.Add("@tvpPrinterNames", dtPrinters, ParameterDirection.Input, SqlDbType.Structured);
                    oPara.Add("@tvpPrinterSettingsDetails", dtPrinterSettingsDetails, ParameterDirection.Input, SqlDbType.Structured);

                    oDB.Execute("gsp_InsertPrinters", oPara);
                    oDB.Disconnect();
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);


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

                if ((pdPrinterSettings != null))
                {
                    pdPrinterSettings.Dispose();
                    pdPrinterSettings = null;
                }


            }

        }

        public static void CreateSchemadtPrinterSettingsDetails(ref DataTable dtPrinterSettingsDetails)
        {

            dtPrinterSettingsDetails.Columns.Add("ModuleName", System.Type.GetType("System.String"));

            dtPrinterSettingsDetails.Columns.Add("DocumentsLeft", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsTop", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsWidth", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsHeight", System.Type.GetType("System.Decimal"));

            dtPrinterSettingsDetails.Columns.Add("DocumentsPrinterMarginsTop", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsPrinterMarginsRight", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsPrinterMarginsLeft", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsPrinterMarginsBottom", System.Type.GetType("System.Decimal"));

            dtPrinterSettingsDetails.Columns.Add("DocumentsHorizontalOverlap", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsHorizontalGutter", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsVerticalOverlap", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsVerticalGutter", System.Type.GetType("System.Decimal"));

            dtPrinterSettingsDetails.Columns.Add("DocumentsActualExtendedWidth", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsActualExtendedHeight", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsFitExtendedWidth", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsFitExtendedHeight", System.Type.GetType("System.Decimal"));

            dtPrinterSettingsDetails.Columns.Add("DocumentsCurrentPageSize", System.Type.GetType("System.Decimal"));

            dtPrinterSettingsDetails.Columns.Add("DocumentsFooterTop", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsFooterRight", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsFooterLeft", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsFooterBottom", System.Type.GetType("System.Decimal"));

            dtPrinterSettingsDetails.Columns.Add("DocumentsFooterFont", System.Type.GetType("System.String"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsFooterColor", System.Type.GetType("System.String"));

            dtPrinterSettingsDetails.Columns.Add("DocumentsCustomDPI", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("DocumentsIsCustomDPI", System.Type.GetType("System.Boolean"));

            dtPrinterSettingsDetails.Columns.Add("IsShowProgress", System.Type.GetType("System.Boolean"));
            dtPrinterSettingsDetails.Columns.Add("IsBackGroundPrint", System.Type.GetType("System.Boolean"));

            //Aniket: This needs to be set Done
            dtPrinterSettingsDetails.Columns.Add("IsHorizontalFlow", System.Type.GetType("System.Boolean"));
            dtPrinterSettingsDetails.Columns.Add("IsActualLandscape", System.Type.GetType("System.Boolean"));
            dtPrinterSettingsDetails.Columns.Add("IsActualMultipage", System.Type.GetType("System.Boolean"));

            //Graphics
            dtPrinterSettingsDetails.Columns.Add("NormalSettingsTop", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("NormalSettingsRight", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("NormalSettingsLeft", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("NormalSettingsBottom", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("NormalSettingsDpiX", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("NormalSettingsDpiY", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("NormalSettingsF0", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("NormalSettingsF1", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("NormalSettingsF2", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("FlatSettingsTop", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("FlatSettingsRight", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("FlatSettingsLeft", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("FlatSettingsBottom", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("FlatSettingsDpiX", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("FlatSettingsDpiY", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("FlatSettingsF0", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("FlatSettingsF1", System.Type.GetType("System.Decimal"));
            dtPrinterSettingsDetails.Columns.Add("FlatSettingsF2", System.Type.GetType("System.Decimal"));


        }
        public static string PrinterSettingsToString(PrinterSettings ps)
        {

            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, ps == null ? new PrinterSettings() : ps);
                return Convert.ToBase64String(stream.ToArray());
            }

        }
        public static PrinterSettings StringToPrinterSettings(string value)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(value);

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    PrinterSettings myPrinterSettings = null;
                    try
                    {
                        myPrinterSettings = new BinaryFormatter().Deserialize(stream) as PrinterSettings;
                    }
                    catch
                    {
                    }
                    if (myPrinterSettings != null)
                    {
                        try
                        {
                            if (myPrinterSettings.IsValid) return myPrinterSettings;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            return new PrinterSettings();
        }
        public string clsPrinterSettingsToString()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, this);
                return Convert.ToBase64String(stream.ToArray());
            }

        }
        public clsPrinterSettings StringToclsPrinterSettings(string value)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(value);

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    return new BinaryFormatter().Deserialize(stream) as clsPrinterSettings;
                }
            }
            catch
            {
            }
            return new clsPrinterSettings();
        }
        public void SetPrinterSettings(PrinterSettings ps)
        {
            sPrinterName = ps.PrinterName;
            bCollate = ps.Collate;
            iCopies = ps.Copies;
            iDuplex = (int)ps.Duplex;
            iFromPage = ps.FromPage;
            iMaximumPage = ps.MaximumPage;
            iMinimumPage = ps.MinimumPage;
            sPrinterName = ps.PrinterName;
            iPrintRange = (int)ps.PrintRange;
            iToPage = ps.ToPage;
            //Create structures of the Printer DevMode and the Printer DevNames
            DEVMODE_TYPE devMode = new DEVMODE_TYPE();
            DEVNAMES_TYPE devNames = new DEVNAMES_TYPE();
            //Definitions of variables
            IntPtr lpDevMode = IntPtr.Zero;
            IntPtr lpDevNames = IntPtr.Zero;
            IntPtr hDevMode = IntPtr.Zero;
            IntPtr hDevNames = IntPtr.Zero;
            bool bAllocatedDevMode = false;
            bool bAllocatedDevNames = false;
            bool bLockedDevMode = false;
            bool bLockedDevNames = false;
            try
            {
                //Sets the DevMode structure (stored in the PrinterSettings) to the buffer_devMode ByteArray
                hDevMode = ps.GetHdevmode();
                bAllocatedDevMode = true;
                lpDevMode = Win32.GlobalLock(hDevMode);
                bLockedDevMode = true;
                Marshal.PtrToStructure(lpDevMode, devMode);
                idevMode = devMode.dmSize + devMode.dmDriverExtra;
                buffer_devMode = new byte[idevMode];
                Marshal.Copy(lpDevMode, buffer_devMode, 0, idevMode);
                Win32.GlobalUnlock(hDevMode);
                bLockedDevMode = false;
                Win32.GlobalFree(hDevMode);
                bAllocatedDevMode = false;
                //Sets the DevNames structure (stored in the PrinterSettings) to the buffer_devNames ByteArray
                hDevNames = ps.GetHdevnames();
                bAllocatedDevNames = true;
                lpDevNames = Win32.GlobalLock(hDevNames);
                bLockedDevNames = true;
                Marshal.PtrToStructure(lpDevNames, devNames);
                idevNames = Marshal.SizeOf(devNames);
                buffer_devNames = new byte[idevNames];
                Marshal.Copy(lpDevNames, buffer_devNames, 0, idevNames);
                Win32.GlobalUnlock(hDevNames);
                bLockedDevNames = false;
                Win32.GlobalFree(hDevNames);
                bAllocatedDevNames = false;
            }
            catch
            {
            }
            finally
            {
                if (bLockedDevNames)
                {
                    Win32.GlobalUnlock(hDevNames);
                    bLockedDevNames = false;
                }
                if (bAllocatedDevNames)
                {
                    Win32.GlobalFree(hDevNames);
                    bAllocatedDevNames = false;
                }
                if (bLockedDevMode)
                {
                    Win32.GlobalUnlock(hDevMode);
                    bLockedDevMode = false;
                }
                if (bAllocatedDevMode)
                {
                    Win32.GlobalFree(hDevMode);
                    bAllocatedDevMode = false;
                }
                devMode = null;
                devNames = null;
            }
            return;
            //Constructor
        }

        /// <summary>
        /// Gets the PrinterSettings. The function should called after the deserialize process 
        /// </summary>
        /// <returns>Deserialized PrinterSettings object</returns>
        public PrinterSettings GetPrinterSettings()
        {

            PrinterSettings ps = new PrinterSettings();

            //Get PrinterSettings Informations
            ps.PrinterName = sPrinterName;
            ps.Collate = bCollate;
            ps.Copies = (short)iCopies;
            ps.Duplex = (System.Drawing.Printing.Duplex)iDuplex;
            ps.FromPage = iFromPage;
            ps.MaximumPage = iMaximumPage;
            ps.MinimumPage = iMinimumPage;
            ps.PrinterName = sPrinterName;
            ps.PrintRange = (System.Drawing.Printing.PrintRange)iPrintRange;
            ps.ToPage = iToPage;

            //Create structures of the Printer DevMode and the Printer DevNames
            DEVMODE_TYPE devMode = new DEVMODE_TYPE();
            DEVNAMES_TYPE devNames = new DEVNAMES_TYPE();
            //Definitions of variables
            IntPtr lpDevMode = IntPtr.Zero;
            IntPtr lpDevNames = IntPtr.Zero;
            IntPtr hDevMode = IntPtr.Zero;
            IntPtr hDevNames = IntPtr.Zero;
            bool bAllocatedDevMode = false;
            bool bAllocatedDevNames = false;
            bool bLockedDevMode = false;
            bool bLockedDevNames = false;

            try
            {
                //Sets the DevMode structure (stored in the PrinterSettings) to the buffer_devMode ByteArray
                hDevMode = ps.GetHdevmode();
                bAllocatedDevMode = true;
                lpDevMode = Win32.GlobalLock(hDevMode);
                bLockedDevMode = true;
                Marshal.PtrToStructure(lpDevMode, devMode);
                int curdevMode = devMode.dmSize + devMode.dmDriverExtra;
                Win32.GlobalUnlock(hDevMode);
                bLockedDevMode = false;
                Win32.GlobalFree(hDevMode);
                bAllocatedDevMode = false;

                if (curdevMode == idevMode)
                {
                    hDevMode = Win32.GlobalAlloc(Win32.GMEM_MOVEABLE | Win32.GMEM_ZEROPOINT, idevMode);
                    bAllocatedDevMode = true;
                    lpDevMode = Win32.GlobalLock(hDevMode);
                    bLockedDevMode = true;
                    Marshal.Copy(buffer_devMode, 0, lpDevMode, idevMode);
                    Win32.GlobalUnlock(hDevMode);
                    bLockedDevMode = false;
                    ps.SetHdevmode(hDevMode);
                    ps.DefaultPageSettings.SetHdevmode(hDevMode);
                    Win32.GlobalFree(hDevMode);
                    bAllocatedDevMode = false;
                }

                //Sets the DevNames structure (stored in the PrinterSettings) to the buffer_devNames ByteArray
                hDevNames = ps.GetHdevnames();
                bAllocatedDevNames = true;
                lpDevNames = Win32.GlobalLock(hDevNames);
                bLockedDevNames = true;
                Marshal.PtrToStructure(lpDevNames, devNames);
                int curdevNames = Marshal.SizeOf(devNames);
                Win32.GlobalUnlock(lpDevNames);
                bLockedDevNames = false;
                Win32.GlobalFree(hDevNames);
                bAllocatedDevNames = false;

                if (curdevNames == idevNames)
                {
                    hDevNames = Win32.GlobalAlloc(Win32.GMEM_MOVEABLE | Win32.GMEM_ZEROPOINT, idevNames);
                    bAllocatedDevMode = true;
                    lpDevNames = Win32.GlobalLock(hDevNames);
                    bLockedDevMode = true;
                    Marshal.Copy(buffer_devNames, 0, lpDevNames, idevNames);
                    Win32.GlobalUnlock(hDevNames);
                    bLockedDevNames = false;
                    ps.SetHdevnames(hDevNames);
                    Win32.GlobalFree(hDevNames);
                    bAllocatedDevNames = false;
                }

            }
            catch
            {
            }
            finally
            {
                if (bLockedDevNames)
                {
                    Win32.GlobalUnlock(hDevNames);
                    bLockedDevNames = false;
                }
                if (bAllocatedDevNames)
                {
                    Win32.GlobalFree(hDevNames);
                    bAllocatedDevNames = false;
                }
                if (bLockedDevMode)
                {
                    Win32.GlobalUnlock(hDevMode);
                    bLockedDevMode = false;
                }
                if (bAllocatedDevMode)
                {
                    Win32.GlobalFree(hDevMode);
                    bAllocatedDevMode = false;
                }
                devMode = null;
                devNames = null;

            }
            //Returns the deserialized Printersettings object
            return ps;
        }

    }

    public class MyLocalPrinters
    {
        public static gloClinicalQueueGeneral.InstalledPrinters printerList = null;
        public static gloClinicalQueueGeneral.MasterConfigFileMasterConfig masterConfig = null;
        public static System.Collections.Generic.Dictionary<String, String> dictModuleConfig = null;
        public static gloExtendedPrinterSettings CustomPrinterExtendedSettings = null;

        public static void reloadPrinterList()
        {
            try
            {
                masterConfig = null;
                printerList = null;
                if (dictModuleConfig != null)
                {
                    dictModuleConfig.Clear();
                    dictModuleConfig = null;
                }

                masterConfig = gloClinicalQueueGeneral.gloQueueMetadatawriter.GetMasterConfigFileData(Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloTSPrint.PrinterConfigDirectory, gloGlobal.gloTSPrint.MasterConfig));
                if (masterConfig != null)
                {
                    dictModuleConfig = gloClinicalQueueGeneral.gloQueueMetadatawriter.GenerateDictionaryForModuleConfig(masterConfig);
                    if (!String.IsNullOrWhiteSpace(masterConfig.InstalledPrintersFile))
                    {
                        printerList = gloQueueSchema.gloSerialization.GetInstalledPrintersDocument(System.IO.Path.Combine(gloGlobal.gloTSPrint.mappedPath, gloGlobal.gloTSPrint.PrinterConfigDirectory, masterConfig.InstalledPrintersFile));
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
    }
}
