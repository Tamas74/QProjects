using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Threading;
using gloPrintDialog;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;

namespace gloPM.Forms
{
    public partial class frmgloPrintClaimsProgress : Form
    {
        string sBatchName = "";
        public delegate void UpdateProgressControlsDelegate(int nClaimPrintingnumber);

        public delegate void PauseProgressControlsDelegate();
        public event PauseProgressControlsDelegate PausePrinting;

        public delegate void PlayProgressControlsDelegate();
        public event PlayProgressControlsDelegate PlayPrinting;

        public delegate void CloseProgressControlsDelegate();
        public event CloseProgressControlsDelegate ClosePrinting;

        private Control myCaller = null;
        bool IsCancel = false;        
                
        public frmgloPrintClaimsProgress(int MaxCount, string sBatchName)
        {            
            InitializeComponent();                           
            pbDocument.Maximum = MaxCount;
            this.sBatchName = sBatchName;
        }

        const int CP_NOCLOSE_BUTTON = 512;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void frmgloPrintClaimsProgress_Load(object sender, EventArgs e)
        {
            try
            {
                if (gloGlobal.gloTSPrint.isCopyPrint)
                {
                    lblPrinterNameValue.Text = this.sBatchName;
                }
                else
                {
                    lblPrinterNameValue.Text = this.sBatchName;
                }
            }
            catch (Exception ex) { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); }            
        }

        public void InvokeProgressUpdateControls(int nClaimPrintNumber)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new UpdateProgressControlsDelegate(UpdateProgressControls), nClaimPrintNumber);
                }
                else
                {
                    UpdateProgressControls(nClaimPrintNumber);
                }         
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }
        
        public void UpdateProgressControls(int nClaimPrintingNumber)
        {
            try
            {
                if ((pbDocument.Value <= pbDocument.Maximum))
                {
                    if (this.InvokeRequired)
                    { }

                    pbDocument.Value = nClaimPrintingNumber;
                    pbDocument.Refresh();
                    if (gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        if (nClaimPrintingNumber == pbDocument.Maximum)
                        {
                            lblPages.Text = "Sending documents to local printer ";
                        }
                        else
                        {
                            lblPages.Text = "Processing " + nClaimPrintingNumber.ToString() + " of " + pbDocument.Maximum.ToString();
                        }
                    }
                    else
                    {
                        lblPages.Text = "Printing " + nClaimPrintingNumber.ToString() + " of " + pbDocument.Maximum.ToString();
                    }

                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        #region "Code Not Used - Pause/Play & Stop of Progrssbar Buttons"

        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                btnPlay.Visible = true;
                btnPause.Visible = false;
                PausePrinting();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                btnPause.Visible = true;
                btnPlay.Visible = false;
                PlayPrinting();                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClosePrinting();                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        #endregion
    }
}
