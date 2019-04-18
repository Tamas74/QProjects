using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;

namespace gloEmdeonInterface.Forms
{
    public partial class frmEmdeonPrintRequisition : Form
    {
        /// <summary>
        /// Added by madan on 20100617
        /// This form is used to print emdeon order 
        /// </summary>

        #region Variables

        private string _OrderReferanceId = string.Empty;
        private string _URL = string.Empty;
        private string _URL1 = string.Empty;
        private string _strLogout = string.Empty;
        bool _IsLabManifest = false; //Added by madan on 20100831
        System.Uri _Uri;

        #endregion

        #region Constructor

        public frmEmdeonPrintRequisition(string OrderReferanceID)
        {
            _OrderReferanceId = OrderReferanceID;
            InitializeComponent();
        }

        /// <summary>
        /// This constructor used for labManifest
        /// </summary>
        /// Added by madan on 20100831 for labManifest Functionality.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public frmEmdeonPrintRequisition(bool IsLabManifest)
        {
            _IsLabManifest = IsLabManifest;
            InitializeComponent();
        }

        #endregion

        #region Events & Methods

        private void frmEmdeonPrintRequisition_Load(object sender, EventArgs e)
        {
            try
            {
                //if condition is to validate for loading lab manifest or print reqestiton.
                //modified by madan on 20100831
                if (_IsLabManifest)
                {
                    this.Text = "Lab Manifest";
                    this.Icon = Properties.Resources.Lab_Manifest01;
                    pictureBoxWait.Top = Convert.ToInt32(this.Height / 2 - pictureBoxWait.Height / 2);
                    pictureBoxWait.Left = Convert.ToInt32(this.Width / 2 - pictureBoxWait.Width / 2);

                    _URL = "&target=jsp/lab/order/Manifest.jsp";
                    _URL1 = clsEmdeonGeneral.emdeonURL + "/servlet/DxLogin?userid=" + clsEmdeonGeneral.emdeonUserName + "&PW=" + clsEmdeonGeneral.emdeonUserPassword + _URL;
                    _Uri = new System.Uri(_URL1);

                    webBrowserEmdeon.Url = _Uri;
                    pictureBoxWait.Visible = false;

                }
                else
                {

                    pictureBoxWait.Top = Convert.ToInt32(this.Height / 2 - pictureBoxWait.Height / 2);
                    pictureBoxWait.Left = Convert.ToInt32(this.Width / 2 - pictureBoxWait.Width / 2);
                    if (_OrderReferanceId != "" && _OrderReferanceId != null && _OrderReferanceId.Trim().Length > 0)
                    {
                        _URL = "&target=servlet/servlets.apiOrderServlet&apiuserid=" + clsEmdeonGeneral.emdeonUserName + "&actionCommand=print&orderid=" + _OrderReferanceId.ToString();
                        _URL1 = clsEmdeonGeneral.emdeonURL + "/servlet/DxLogin?userid=" + clsEmdeonGeneral.emdeonUserName + "&PW=" + clsEmdeonGeneral.emdeonUserPassword + _URL;
                        _Uri = new System.Uri(_URL1);

                        webBrowserEmdeon.Url = _Uri;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _Uri = null;
                //28-Dec-15 Aniket: Resolving Bug #91893: glo EMR >> On Print Requisition screen loading symbol is still display after loading page 
                pictureBoxWait.Visible = false;
            }
        }

        private void webBrowserEmdeon_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (webBrowserEmdeon.ReadyState == WebBrowserReadyState.Interactive)
            {
                pictureBoxWait.Visible = false;
            }

        }
        private void LogOutBrowserSession()
        {
            try
            {
                _strLogout = clsEmdeonGeneral.emdeonURL + "/servlet/lab.security.DxLogout?userid=" + clsEmdeonGeneral.emdeonUserName.ToString() + "&BaseUrl=" + _URL + "&LogoutPath=/html/AutoPrintFinished.html";
                _Uri = new System.Uri(_strLogout);

                webBrowserEmdeon.Url = _Uri;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _Uri = null;
            }

        }

        private void frmEmdeonPrintRequisition_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                //if (_OrderReferanceId != "" && _OrderReferanceId != null && _OrderReferanceId.ToString().Trim().Length != 0)
                //{
                LogOutBrowserSession();
                Application.DoEvents();

                //Added by madan on 20100925
                if (_IsLabManifest)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "LabManifest viewed by the user", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                }
                //End madan changes
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }


        private void tlbbtn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
