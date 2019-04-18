using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;  
namespace gloRxHub
{
    public partial class uctlFormularyAlert : UserControl
    {
        ClsFormularyCheck oFormularyCheck;
       
        public delegate void LinkClick(object sender, LinkLabelLinkClickedEventArgs e);
        public event LinkClick EventLinkClick;
        public uctlFormularyAlert(ClsFormularyCheck  objFormularyCheck)
        {
            InitializeComponent();
            oFormularyCheck = objFormularyCheck;
            InitialiseControls();
            if (objFormularyCheck.FormularyType == FormularyCheck.eBenefits)
            {
                grpCoverage.Visible = true;
                grpCopay.Visible = true;
                grpFormulary.Visible = false;
                grpAlternates.Visible = false;
            }
            else if (objFormularyCheck.FormularyType == FormularyCheck.eFormulary)
            {
                grpCoverage.Visible = false ;
                grpCopay.Visible = false ;
                grpFormulary.Visible = true ;
                grpAlternates.Visible = true ;
            }

        }
       public void InitialiseControls()
       {
           //Get the formulary status for the drug
           if (oFormularyCheck.ProductExclusionCount == 0)
           {
               lblFormularyStatus.Text = oFormularyCheck.FormularyStatus;
               if (oFormularyCheck.RxH_271MasterObject.CoverageId == "")
               {
                   lnkCoverageDetails.Text = lnkCoverageDetails + " not available ";
                   lnkCoverageDetails.LinkBehavior = LinkBehavior.AlwaysUnderline;
               }
               else
               {
                   lnkCoverageDetails.Text = lnkCoverageDetails + "available,click here for more details ";
                   lnkCoverageDetails.LinkBehavior = LinkBehavior.AlwaysUnderline;
               }
           }
           else
           {
               
               lnkCoverageDetails.Text = lnkCoverageDetails.Text + " not available";
               lnkCoverageDetails.LinkBehavior = LinkBehavior.NeverUnderline;
           }
       }

        private void lnkCoverageDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EventLinkClick(sender, e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EventLinkClick(sender, e);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EventLinkClick(sender, e);
        }
    }
}
