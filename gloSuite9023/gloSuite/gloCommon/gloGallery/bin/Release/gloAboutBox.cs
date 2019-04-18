using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using gloTransparentScreen;

namespace gloTransparentScreen
{
    public partial class gloAboutBox : gloTransparentScreen.gloTransperentForm
    {
        public gloAboutBox()
        {
       

           
           
            //This call is required by the Windows Form Designer.
            InitializeComponent();

            



        }

        public gloAboutBox(Image programPicture):this()
        {

          //  pictureBoxIcon.Image = iconPicture;
            pictureBoxProgram.Image = programPicture;// gloTransparentScreen.Properties.Resources.gloEMRLOGO;
            this.TopMost = true;

        }

        //private void LinkLabel1_LinkClicked(System.Object sender, EventArgs e)
        //{
        //    System.Diagnostics.Process.Start(LinkLabel1.Text);
        //}

        //private void LinkLabel2_LinkClicked(System.Object sender, EventArgs e)
        //{
        //    System.Diagnostics.Process.Start("mailto:" + LinkLabel2.Text);
        //}

        private void frmAboutUs_Load(object sender, System.EventArgs e)
        {
            //09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
            //Dim objGlobalMisc As New gloGlobal.clsMISC
            Label4.Text = gloTransparentScreen.clsgloCopyRightText.gloPreRequisites;
            lblBuildVersion.Visible = true;

            //lblBuildVersion.Text = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion;
            lblBuildVersion.Text = Application.ProductVersion;
            lblProductVersion.Text = System.Windows.Forms.Application.ProductVersion;

            //09-Oct-14 Aniket: Show major version E.g. 8.X on the splash screen
            lbl_mktngversion.Text = gloGlobal.clsMISC.GetMajorVersion(System.Windows.Forms.Application.ProductVersion);
            gloAuditTrail.MachineDetails.MachineInfo localMachine = gloAuditTrail.MachineDetails.LocalMachineDetails();
            gloAuditTrail.MachineDetails.MachineInfo remoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails();
            lblLocalMachine.Text = localMachine.MachineName;
            lblLocalDomain.Text = localMachine.DomainName;
            lblLocalIPAddress.Text = localMachine.MachineIp;
            lblLocalUser.Text = localMachine.UserName;
            lblRemoteMachine.Text = remoteMachine.MachineName;
            lblRemoteDomain.Text = remoteMachine.DomainName;
            lblRemoteIPAddress.Text = remoteMachine.MachineIp;
            lblRemoteUser.Text = remoteMachine.UserName;

        }

        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {

            gloAuditTrail.MachineDetails.MachineInfo remoteMachine = gloAuditTrail.MachineDetails.RemoteMachineDetails(true);
            lblRemoteMachine.Text = remoteMachine.MachineName;
            lblRemoteDomain.Text = remoteMachine.DomainName;
            lblRemoteIPAddress.Text = remoteMachine.MachineIp;
            lblRemoteUser.Text = remoteMachine.UserName;
        }

        private void btnLocalRefresh_Click(System.Object sender, System.EventArgs e)
        {
            gloAuditTrail.MachineDetails.MachineInfo localMachine = gloAuditTrail.MachineDetails.LocalMachineDetails(true);
            lblLocalMachine.Text = localMachine.MachineName;
            lblLocalDomain.Text = localMachine.DomainName;
            lblLocalIPAddress.Text = localMachine.MachineIp;
            lblLocalUser.Text = localMachine.UserName;

        }

        private void btnAboutClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLinkLabel2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + LinkLabel2.Text);
        }

        private void btnLinkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(LinkLabel1.Text);
        }

       
    }




}

