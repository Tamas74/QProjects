using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace gloEMR.Help
{
	/// <summary>
	/// Help component.
	/// </summary>
    public class HelpComponent : System.ComponentModel.Component, IMessageFilter
	{
		#region Enums
		/// <summary>
		/// Provider mode.
		/// </summary>
		public enum ProviderMode 
		{
			/// <summary>
			/// Client mode.
			/// </summary>
			Client,

			/// <summary>
			/// Builder mode.
			/// </summary>
			Builder 
		};


       
		#endregion

		#region Variables
		private System.ComponentModel.Container _components;

		private IHelpProcessor _helpProcessor;
		#endregion

		#region Constructor & destructor
		/// <summary>
		/// Initializes a new instance of the <see cref="HelpComponent"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		public HelpComponent(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}


        public void showHelpComponent()
        {
           
           // InitializeComponent();
            if (_helpProcessor == null || _helpProcessor.GetType() != typeof(EndUserHelp))
                _helpProcessor = new EndUserHelp();
         
        
        }
        
        /// <summary>
		/// Initializes a new instance of the <see cref="HelpComponent"/> class.
		/// </summary>
		public HelpComponent()
		{
			InitializeComponent();
		}
		#endregion

		#region Properties
		/// <summary>
        /// Gets the current help file.
        /// </summary>
        public string HelpFile
        {
            get
            {
                if (ResourceHelper.HelpDescription != HelpDescription.Empty)
                {
                    return ResourceHelper.HelpDescription.HelpFilePath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
		#endregion

		#region Methods
		/// <summary>
		/// Initializes the component.
		/// </summary>
        private void InitializeComponent()
        {
			_components = new System.ComponentModel.Container();

            // dodaj globalny filtr komunikatów
            try
            {
                Application.AddMessageFilter(this);
            }
            catch { }
        }

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                Application.RemoveMessageFilter(this);
                if (_components != null)
					_components.Dispose();

                
            }
            base.Dispose(disposing);
        }
        #endregion

        #region HelperProcessor

		/// <summary>
		/// Gets or sets the mode.
		/// </summary>
		/// <value>The mode.</value>
		public ProviderMode Mode { get; set; }

		/// <summary>
		/// Gets the help processor.
		/// </summary>
		/// <value>The help processor.</value>
        /// 


        public IHelpProcessor HelpProcessor
        {
            get
            {
                switch (Mode)
                {
                    //default: if (_helpProcessor == null || _helpProcessor.GetType() != typeof(TechWriterProcessor))
                    //        _helpProcessor = new TechWriterProcessor();
                    //    break;

                    case ProviderMode.Client:
                        if (_helpProcessor == null || _helpProcessor.GetType() != typeof(EndUserHelp))
                            _helpProcessor = new EndUserHelp();
                        break;
                    case ProviderMode.Builder:
                        if (_helpProcessor == null || _helpProcessor.GetType() != typeof(TechWriterProcessor))
                            _helpProcessor = new TechWriterProcessor();
                        break;
                }

               


				return _helpProcessor;
            }
        }
        #endregion

        #region Builder mode predicate
		/// <summary>
		/// BuilderMode delegate.
		/// </summary>
        public delegate bool BuilderModeDelegate();

		/// <summary>
		/// Predicate for the BuilderMode.
		/// </summary>
        public BuilderModeDelegate BuilderModePredicate = DefaultBuilderModePredicate;

		/// <summary>
		/// Defaults the builder mode predicate.
		/// </summary>
		/// <returns></returns>
       public static bool blnbuildmode =false;  
        private static bool DefaultBuilderModePredicate()
        {
            return blnbuildmode;

             //   Control.ModifierKeys == Keys.Control &&
                //Environment.CommandLine.ToLower().Contains("helpbuilder");
        }
        #endregion

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        public void ShowHelp(Form frm)
        {
            IntPtr handle = GetActiveWindow();
            if (handle == null)
            {

                if ((frm.ActiveMdiChild != null))
                {
                    ProcessRequest(frm.ActiveMdiChild.Handle);
                }
                else
                {
                    if ((Form.ActiveForm != null))
                    {
                        ProcessRequest(Form.ActiveForm.Handle);
                    }
                    else
                    {
                     ProcessRequest(frm.Handle);
                    }
                }
            }

            else
            {
             ProcessRequest(handle);
            }
        
        }
        
        
        #region IMessageFilter Members
		/// <summary>
		/// Filters out a message before it is dispatched.
		/// </summary>
		/// <param name="m">The message to be dispatched. You cannot modify this message.</param>
		/// <returns>
		/// true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.
		/// </returns>
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x100: // WM_KEYDOWN
                    if ((int)m.WParam == (int)Keys.F1)
                    {
                        try
                        {
                            Mode = BuilderModePredicate() ? ProviderMode.Builder : ProviderMode.Client;

                            ProcessRequest(m.HWnd);
                            return true;
                        }
                        catch 
                        { 
                        
                        }
                    }
                    break;
            }
            return false;
        }
        #endregion

        #region HelpProcessing
		/// <summary>
		/// Processes the request.
		/// </summary>
		/// <param name="hWND">The h WND.</param>
        public void ProcessRequest(IntPtr hWND)
        {
           
            Control NetControl = ControlFromHandle(hWND);
			if (NetControl != null && HelpProcessor != null)
			{
				HelpProcessor.ProcessControlHelp(NetControl);
			}
        }
        #endregion

        #region ControlFromHandle
		/// <summary>
		/// Controls from handle.
		/// </summary>
		/// <param name="hWND">The h WND.</param>
		/// <returns></returns>
        private static Control ControlFromHandle(IntPtr hWND)
        {
            while (hWND != IntPtr.Zero)
            {
                Control control = Control.FromChildHandle(hWND);
                if (control != null)
                {
                    if (control is Form)
                    {
                        Control childControl =((Form) control).ActiveControl;
                        if (childControl != null)
                        {
                            control = childControl;
                        }
                    }
                }
                if (control != null)
                    return control;

                hWND = GetParent(hWND);

              //  Control control2 = System.Windows.Forms.Control.FromChildHandle(hWND);
               // IntPtr hwnd = (IntPtr)this.Handle.ToPointer();
            }

            return null;
        }

		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <param name="hWnd">The h WND.</param>
		/// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        #endregion




    }

    public static class ResourceHelper
    {

        private static HelpDescription _helpDescription;

        public static HelpDescription HelpDescription
        {
            get
            {
                if (_helpDescription == null)
                {
                    _helpDescription = HelpDescription.Empty;

                    try
                    {
                        Stream stream = null;

                        try
                        {
                            stream = File.Open(PathHelper.FullHelpMappingPath, FileMode.Open, FileAccess.Read);
                        }
                        catch { }

                        // if (stream == null)
                        //  stream = GetApplicationStream(PathHelper.RelativeHelpMappingPath);

                        // serializer
                        XmlSerializer xs = new XmlSerializer(typeof(HelpDescription));

                        if (stream != null)
                        {
                            _helpDescription = (HelpDescription)xs.Deserialize(stream);
                            stream.Close();
                            stream.Dispose();
                            stream = null;
                        }

                        // Clear empty descriptions
                        _helpDescription.ClearEmptyDescriptions();
                    }
                    catch //(Exception ex)
                    {
                        // Trace.TraceError(ex.Message);
                    }
                }

                return _helpDescription;
            }
        }


        #region Methods - builder
        /// <summary>
        /// Saves the help description.
        /// </summary>
        /// <param name="helpDescription">The help description.</param>
        public static void SaveHelpDescription(HelpDescription helpDescription)
        {
            if (helpDescription != null)
            {
                using (FileStream fs = File.Create(PathHelper.FullHelpMappingPath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(HelpDescription));
                    xs.Serialize(fs, helpDescription);
                    fs.Close();
                }
            }
        }
        #endregion


        //static readonly List<string> _examinedAssemblies = new List<string>();

        /// <summary>
        /// Gets the application stream.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        //public static Stream GetApplicationStream(string resourceName)
        //{
        //    _examinedAssemblies.Clear();
        //    return GetResource(Assembly.GetEntryAssembly(), resourceName);
        //}



        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <param name="TheAssembly">The assembly.</param>
        /// <param name="ResourceName">Name of the resource.</param>
        /// <returns></returns>
        //private static Stream GetResource(Assembly TheAssembly, string ResourceName)
        //{

        //    foreach (string resName in TheAssembly.GetManifestResourceNames())
        //        if (resName.EndsWith(ResourceName))
        //            return TheAssembly.GetManifestResourceStream(resName);


        //    foreach (AssemblyName RefAssembly in TheAssembly.GetReferencedAssemblies())
        //    {
        //        if (!_examinedAssemblies.Contains(RefAssembly.FullName))
        //        {
        //            _examinedAssemblies.Add(RefAssembly.FullName);
        //            try
        //            {
        //                Assembly ChildAssembly = Assembly.Load(RefAssembly);
        //                if (ChildAssembly != null)
        //                {
        //                    Stream resStream = GetResource(ChildAssembly, ResourceName);
        //                    if (resStream != null)
        //                        return resStream;
        //                }
        //            }
        //            catch { }
        //        }
        //    }

        //    return null;
        //}

    }
    public static class PathHelper
    {
        #region Properties
        /// <summary>
        /// Relative filename of the help mapping file.
        /// </summary>

        public static string RelativeHelpMappingPath
        {
            get
            {
                //if (Assembly.GetEntryAssembly().GetName().Name.Equals("gloEMR"))
                //{
                //    return Path.Combine(HelpDirectory + "\\gloEMR", "help.mapping");
                //}
                //else if (Assembly.GetEntryAssembly().GetName().Name.Equals("gloPM"))
                //{
                //    return Path.Combine(HelpDirectory + "\\gloPM", "help.mapping");
                //}
                //else
                //{
                   
                //}
                return Path.Combine(HelpDirectory, "help.mapping");
            }
        }

        /// <summary>
        /// Full filename of the help mapping file.
        /// </summary>
        public static string FullHelpMappingPath
        {
            get
            {
                return Path.Combine(Application.StartupPath, RelativeHelpMappingPath);
            }
        }

        /// <summary>
        /// Relative filename of the tip of the day file.
        /// </summary>
        public static string RelativeTipOfTheDayPath
        {
            get
            {
                return Path.Combine(HelpDirectory, "tipoftheday.xml");
            }
        }

        /// <summary>
        /// Full filename of the tip of the day file.
        /// </summary>
        public static string FullTipOfTheDayPath
        {
            get
            {
                return Path.Combine(Application.StartupPath, RelativeTipOfTheDayPath);
            }
        }
        #endregion
        public const string HelpDirectory = "Help";
    }

}
