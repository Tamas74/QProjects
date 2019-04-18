using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    /// <summary>
    /// Anil 20080103
    /// Day Master Form for Setting Working and OFF days for clinic
    /// </summary>
    public partial class frmSetupClinicDays : Form
    {

        #region " Enumerations "

        //Enumerate the day category as working and OFF day
        private enum DayCategory { WorkingDay = 1, OFFDay = 2 };

        #endregion " Enumerations "

        #region " Declarations "

        private string _MessageBoxCaption = string.Empty;
        //private Int64 _DayID = 0;
        private string _databaseconnectionstring = "";
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        #region " Constructor "

        public frmSetupClinicDays()
        {
            InitializeComponent();
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }


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

        public frmSetupClinicDays(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }


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

        #endregion " Constructor "

        #region " Form Load "

        /// <summary>
        /// Form Load
        /// To load working days and OFF days information from database
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void frmSetupClinicDays_Load(object sender, EventArgs e)
        {
            DataTable dt=null;
            Books.ClinicDays oCD = new global::gloAppointmentBook.Books.ClinicDays(_databaseconnectionstring);
            GroupBox gbTemp = new GroupBox();
            try
            {
                //dt = new DataTable();
                dt = oCD.GetList();
                //nDayID, sDayName, nCategory
                if (dt != null)
                {
                    int i;
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        // Set Working Days
                        if (Convert.ToInt32(dt.Rows[i]["nCategory"]) == DayCategory.WorkingDay.GetHashCode())
                        {
                            gbTemp = gbWorkingDays;
                        }
                        else if (Convert.ToInt32(dt.Rows[i]["nCategory"]) == DayCategory.OFFDay.GetHashCode())
                        // For OFF Days
                        {
                            gbTemp = gbOffDays;
                        }

                        //Check state of each checkbox in Groupbox
                        foreach (Control cntr in gbTemp.Controls)
                        {
                            //check whether control is checkbox or not
                            if (cntr is CheckBox)
                            {
                                //Declare new checkbox object
                                CheckBox chk = (CheckBox)cntr; //new CheckBox();
                                //Assign control values to checkbox
                              //  chk = (CheckBox)cntr;

                                //If Day name in table is equal to checkbox text then Check the checkbox state
                                if (dt.Rows[i]["sDayName"].ToString() == chk.Text.ToString())
                                {
                                    chk.CheckState = CheckState.Checked;
                                    break;
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dt != null) { dt.Dispose(); dt = null; }
                if (oCD != null) { oCD.Dispose(); oCD = null; }
                if (gbTemp != null) { gbTemp.Dispose(); gbTemp = null; }
            }
        }
        
        #endregion " Form Load "

        #region " Tool Strip Event "


        #endregion " Tool Strip Event "

        #region " Button Click Events "

        private void btnOK_Click(object sender, EventArgs e)
        {

            try
            {
                //Save state of each Checkbox present in OFF days groupbox in Database
                //Check state of each checkbox in Working days Groupbox
                foreach (Control cntr in gbWorkingDays.Controls)
                {
                    //check whether control is checkbox or not
                    if (cntr is CheckBox)
                    {
                        //Declare new checkbox object
                        CheckBox chk = (CheckBox)cntr;//new CheckBox();
                        //assign the control properties to new checkbox
                      //  chk = 
                        Int32 nCat = 0;
                        Int32 _return;
                        string _strDay;
                        //check the State of checkbox
                        if (chk.CheckState == CheckState.Checked)
                        {
                            //get the value in tag
                            nCat = Convert.ToInt32(chk.Tag);
                            //get the text of checkbox
                            _strDay = chk.Text;
                            //declare object of a class to save the checkboxes info into database
                            Books.ClinicDays oCD = new global::gloAppointmentBook.Books.ClinicDays(_databaseconnectionstring);
                            //declare object of a class to save the checkboxes info into database
                            _return = oCD.Modify(nCat, _strDay, DayCategory.WorkingDay.GetHashCode());
                        }
                        _strDay = null;
                    }
                }

                //Save state of each Checkbox present in OFF days groupbox in Database
                //Check state of each checkbox in OFF days Groupbox
                foreach (Control cntr in gbOffDays.Controls)
                {
                    //check whether control is checkbox or not
                    if (cntr is CheckBox)
                    {
                        //Declare new checkbox object
                        CheckBox chk =  (CheckBox)cntr;//new CheckBox();
                        //assign the control properties to new checkbox
                      //  chk =
                        Int32 nCat = 0;
                        string _strDay;
                        Int32 _return;
                        //check the State of checkbox
                        if (chk.CheckState == CheckState.Checked)
                        {
                            //get the value in tag
                            nCat = Convert.ToInt32(chk.Tag);
                            //get the text of checkbox
                            _strDay = chk.Text;
                            //declare object of a class to save the checkboxes info into database
                            Books.ClinicDays oCD = new global::gloAppointmentBook.Books.ClinicDays(_databaseconnectionstring);
                            //call the function to save data
                            _return = oCD.Modify(nCat, _strDay, DayCategory.OFFDay.GetHashCode());
                        }
                        _strDay = null;
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {   
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Button to close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion " Button Click Events "

        #region  " Check Box Events "

        /// <summary>
        /// Validation for checked condition of checkboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkWork_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //declare new checkbox object
                CheckBox chk =  (CheckBox)sender;//new CheckBox();
                //assign the properties of sender object to new checkbox declared
          //      chk =

                //Check for each control/checkbox in OFF Days groupbox
                foreach (Control cntrl in gbOffDays.Controls)
                {
                    //Check whether the control is checkbox or not
                    if (cntrl is CheckBox)
                    {
                        //Declare new checkbox object
                        CheckBox chk1 =(CheckBox)cntrl; //new CheckBox();
                        //Assign the properties of control to new checkbox declared
                     //   chk1 = 

                        //Check the Tag and checked State of checkbox
                        if (chk1.Tag == chk.Tag && chk1.CheckState == CheckState.Checked)
                            if (chk.CheckState == CheckState.Checked)
                            {
                                //if state is checked make it Unchecked
                                chk.CheckState = CheckState.Unchecked;
                                MessageBox.Show(chk.Text + " is Off Day");
                                return;
                            }
                    }
                }
            }
            catch (Exception ex)    
            {
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void chkOFF_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //declare new checkbox object
                CheckBox chk = (CheckBox)sender;//new CheckBox();
                //assign the properties of sender object to new checkbox declared
                //chk = 
                //Check for each control/checkbox in Working Days groupbox
                foreach (Control cntrl in gbWorkingDays.Controls)
                {
                    //Check whether the control is checkbox or not
                    if (cntrl is CheckBox)
                    {
                        //Declare new checkbox object
                        CheckBox chk1 =(CheckBox)cntrl;// new CheckBox();
                        //Assign the properties of control to new checkbox declared
                    //    chk1 = 

                        //Check the Tag and checked State of checkbox
                        if (chk1.Tag == chk.Tag && chk1.CheckState == CheckState.Checked)
                            if (chk.CheckState == CheckState.Checked)
                            {
                                //if state is checked make it Unchecked
                                chk.CheckState = CheckState.Unchecked;
                                MessageBox.Show(chk.Text + " is Working Day", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                    
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion  " Check Box Events "

    }
}