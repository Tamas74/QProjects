using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloBilling.Collections
{
    public partial class frmBatchFollowUpStatus : Form
    {
        public DataTable dtFollowUpStatus { get; set; }
        public CollectionEnums.FollowUpType FollowUpAction { get; set; }
        public DataTable dtClaimRemittanceInfo { get; set; }
        private string sCalledFrom = string.Empty;
        public string CalledFrom
        {
            get
            {
                return sCalledFrom;
            }
            set
            {
                sCalledFrom = value;
            }
        }
        public frmBatchFollowUpStatus()
        {
            InitializeComponent();
        }

        private void frmBatchFollowUpStatus_Load(object sender, EventArgs e)
        {
            if (FollowUpAction==CollectionEnums.FollowUpType.Claim)
            {
                c1ClaimFollowUp.DataSource = dtFollowUpStatus;
                c1ClaimFollowUp.Cols[0].Width = 250;
                c1ClaimFollowUp.Cols[1].Width = 150;
                c1ClaimFollowUp.Cols[2].Width = 80;
                c1ClaimFollowUp.Cols[3].Width = 80;
                c1ClaimFollowUp.Cols[4].Width = 150;
                
                c1ClaimFollowUp.Cols[3].Visible = false;
                c1ClaimFollowUp.Cols[4].Visible = false;

                pnlAccountFollowUp.Visible = false;
                lblAccounts.Visible = false;
                pnlClaimFollowUp.Visible = true;
                lblClaims.Visible = true;
                c1ClaimFollowUp.AllowEditing = false;
            }
            else if (FollowUpAction==CollectionEnums.FollowUpType.PatientAccount)
            {
                c1AccountFollowUp.DataSource = dtFollowUpStatus;
                c1AccountFollowUp.Cols[0].Width = 170;
                c1AccountFollowUp.Cols[1].Width = 150;
                c1AccountFollowUp.Cols[2].Width = 150;
                c1AccountFollowUp.Cols[3].Width = 80;
                c1AccountFollowUp.Cols[4].Width = 150;

                c1AccountFollowUp.Cols[3].Visible = false;
                c1AccountFollowUp.Cols[4].Visible = false;

                pnlAccountFollowUp.Visible = true;
                lblAccounts.Visible = true;
                pnlClaimFollowUp.Visible = false;
                lblClaims.Visible = false;
                c1ClaimFollowUp.AllowEditing = false;
            }
            else if (FollowUpAction == CollectionEnums.FollowUpType.BadDebt)
            {
                c1AccountFollowUp.DataSource = dtFollowUpStatus;
                c1AccountFollowUp.Cols[0].Width = 170;
                c1AccountFollowUp.Cols[1].Width = 150;
                c1AccountFollowUp.Cols[2].Width = 150;
                c1AccountFollowUp.Cols[3].Width = 80;
                c1AccountFollowUp.Cols[4].Width = 150;

                c1AccountFollowUp.Cols[3].Visible = false;
                c1AccountFollowUp.Cols[4].Visible = false;

                pnlAccountFollowUp.Visible = true;
                lblAccounts.Visible = true;
                pnlClaimFollowUp.Visible = false;
                lblClaims.Visible = false;
                c1ClaimFollowUp.AllowEditing = false;
            }
            else if (FollowUpAction == 0 && CalledFrom == "RebillRemitanceNo")
            {
                tlsOK.Visible = true;
                tlsbtn_Cancel.Visible = false;
                lblAccounts.Text = "Enter Claim Remittance Ref#";
                this.Text = "Batch Rebill";
                c1AccountFollowUp.DataSource = dtFollowUpStatus;
                c1AccountFollowUp.Cols[0].Width = 0;
                c1AccountFollowUp.Cols[1].Width = 0;
                c1AccountFollowUp.Cols[2].Width = 0;
                c1AccountFollowUp.Cols[3].Width = 0;               
                
                 
                c1AccountFollowUp.Cols[4].Width = 250;
                c1AccountFollowUp.Cols[5].Width = 150;
                c1AccountFollowUp.Cols[6].Width = 150;              
                c1AccountFollowUp.Cols[7].Width = 80;

                c1AccountFollowUp.Cols[4].AllowEditing = false;
                c1AccountFollowUp.Cols[5].AllowEditing = false;
                c1AccountFollowUp.Cols[6].AllowEditing = false;
                c1AccountFollowUp.Cols[7].AllowEditing = true;
                c1AccountFollowUp.SetupEditor += new C1.Win.C1FlexGrid.RowColEventHandler(c1AccountFollowUp_SetupEditor);
            }
            else if (FollowUpAction == 0 && CalledFrom != "RebillRemitanceNo")
            {
                switch (CalledFrom)
                {
                    case "Resend": 
                         lblAccounts.Text = "Batch Resend Status";
                         this.Text = "Batch Resend Status";
                        break;
                    case "Rebill":
                        lblAccounts.Text = "Batch Rebill Status";
                        this.Text = "Batch Rebill Status";
                        break;
                    case "TransferToSelf": 
                        lblAccounts.Text = "Batch Transfer to Self Status";
                        this.Text = "Batch Transfer to Self Status";
                        break;
                    default: lblAccounts.Text = "Batch Action Status";
                        break;
                }
                c1AccountFollowUp.DataSource = dtFollowUpStatus;
                c1AccountFollowUp.Cols[0].Width = 250;
                c1AccountFollowUp.Cols[1].Width = 150;
                c1AccountFollowUp.Cols[2].Width = 150;
                c1AccountFollowUp.Cols[3].Width = 80;
                c1AccountFollowUp.AllowEditing = false;
               // c1AccountFollowUp.Cols[4].Width = 250;

                pnlAccountFollowUp.Visible = true;
                lblAccounts.Visible = true;
                pnlClaimFollowUp.Visible = false;
                lblClaims.Visible = false;
                
            }
        }

        void c1AccountFollowUp_SetupEditor(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col>0&&e.Row>0)
            {
                TextBox txt = (TextBox)this.c1AccountFollowUp.Editor;
                txt.MaxLength = 30;
            }
        }

        private void c1ClaimFollowUp_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(c1SuperTooltip1, (C1.Win.C1FlexGrid.C1FlexGrid)sender, e.Location);
        }

        private void c1AccountFollowUp_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(c1SuperTooltip1, (C1.Win.C1FlexGrid.C1FlexGrid)sender, e.Location);
        }

        private void tlsbtn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlsOK_Click(object sender, EventArgs e)
        {
            c1AccountFollowUp.FinishEditing();
            c1AccountFollowUp.EndUpdate();
            dtClaimRemittanceInfo = (DataTable)c1AccountFollowUp.DataSource;
            this.Close();
            
            //DataTable dtClaimRemittanceRef = null;
            //DataRow[] dr = null;
            //DialogResult _dialogResult;
            //try
            //{
                
            //    c1AccountFollowUp.FinishEditing();
            //    c1AccountFollowUp.EndUpdate();

            //    dtClaimRemittanceInfo = (DataTable)c1AccountFollowUp.DataSource;
            //    if (dtClaimRemittanceInfo.Columns["Claim Remittance Ref #"].ColumnName != null)
            //    {
            //        dtClaimRemittanceInfo.Columns["Claim Remittance Ref #"].ColumnName = "ClaimRemittanceRefNo";
            //    }
            //    dr = dtClaimRemittanceInfo.Select("ClaimRemittanceRefNo=''");
            //    if (dr != null && dr.Length > 0)
            //    {
            //        dtClaimRemittanceRef = dr.CopyToDataTable<DataRow>();
            //    }

            //    if (dtClaimRemittanceInfo.Rows.Count == dtClaimRemittanceRef.Rows.Count)
            //    {
            //        string msgBrokenrule = "Rebilling normally requires a Claim Remittance Ref #, but none has been entered.\nEnter a Claim Remittance Ref # now?";

            //        _dialogResult = MessageBox.Show(msgBrokenrule, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            //        switch (_dialogResult)
            //        {
            //            case DialogResult.Yes:
            //                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.Yes, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            //                break;
            //            case DialogResult.No:
            //                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.No, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            //                this.Close();
            //                break;
            //            case DialogResult.Cancel:
            //                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCM, gloAuditTrail.ActivityCategory.RebillBatchClaims, gloAuditTrail.ActivityType.Cancle, msgBrokenrule, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            //                this.Close();
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        this.Close();
            //    }
               
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{

            //}
        }
           

    }
}
