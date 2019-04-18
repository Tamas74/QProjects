using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using gloUIControlLibrary.Classes.ICD10;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Data;
using System.Windows.Input;
using System.IO;

namespace gloUIControlLibrary.WPFForms
{
    /// <summary>
    /// Interaction logic for frmShowCodingRules.xaml
    /// </summary>
    public partial class frmShowCodingRules : Window
    {    
        public bool NoData { get; set; }

        public frmShowCodingRules(string ICD10Code, string Description,string dbConnectionString)
        {
            InitializeComponent();
     
            this.gloCodingRules.ICD10Code = ICD10Code;
            this.gloCodingRules.ICD10Description = Description;
            this.gloCodingRules.DBConnectionString = dbConnectionString;

            this.Title = "Coding rules for code " + ICD10Code + " " + Description;
        }

        public void LoadNotes()
        {
          
            this.Cursor = System.Windows.Input.Cursors.Wait;

            try 
            {                 
                gloCodingRules.LoadNotes(true);
                this.NoData = gloCodingRules.NoData;
                
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }
            finally 
            { this.Cursor = System.Windows.Input.Cursors.Arrow; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            try
            {
                if (this.gloCodingRules != null)
                {
                    this.gloCodingRules.Dispose();
                    this.gloCodingRules = null;
                }
            }
            catch (Exception Ex)
            { LogException.ExceptionLog(Ex.ToString(), true); }

            
        }

    }
}
