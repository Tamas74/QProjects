using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKYPE4COMLib;

namespace gloCommunity.Forms
{
    public partial class frmgloSkypeInvitations : Form
    {
        public SKYPE4COMLib.Skype oskype;
        public SKYPE4COMLib.User ui;
        public frmgloSkypeInvitations()
        {
            InitializeComponent();
        }

        private void frmgloSkypeInvitations_Load(object sender, EventArgs e)
        {
            oskype = new SKYPE4COMLib.Skype();
            try
            {
                if (!(oskype.Client.IsRunning))
                {
                    oskype.Client.Start(true, true);
                }
                oskype.Attach(9, true);
                IUserCollection usernm = oskype.UsersWaitingAuthorization;

                if ((usernm.Count > 0))
                {
                    int i = 0;
                    for (i = 1; i <= usernm.Count; i += 1)
                    {
                        if (((usernm[i].BuddyStatus != TBuddyStatus.budFriend) & (usernm[i].BuddyStatus != TBuddyStatus.budPendingAuthorization)))
                        {
                            ListBox_inviations.Items.Add((usernm[i].Handle.ToString()));
                        }
                    }
                }
                else
                {
                    Lblstatus.Text = "There are no invitations";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Btn_Addcontact_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListBox_inviations.SelectedItem != string.Empty)
                {
                    Lblstatus.ResetText();
                    ui = oskype.User[ListBox_inviations.SelectedItem.ToString()];
                    ui.BuddyStatus = TBuddyStatus.budPendingAuthorization;
                    ui.IsAuthorized = true;
                    Lblstatus.ResetText();
                    Lblstatus.Text = "The invitation from " + ui.Handle + "  is Accepted";
                    ListBox_inviations.Items.Remove(ListBox_inviations.SelectedItem);
                }
                else
                {
                    Lblstatus.ResetText();
                    Lblstatus.Text = "Please Select the Invite";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Btn_viewprofie_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListBox_inviations.SelectedItem != string.Empty)
                {
                    Lblstatus.ResetText();
                    Pnl_Profile.Visible = true;

                    ui = oskype.User[ListBox_inviations.SelectedItem.ToString()];
                    Lbl_profile.Text = string.Empty;
                    Lbl_profile.Text += "\n" + "Skype Name     : " + ui.Handle.ToString();
                    Lbl_profile.Text += "\n" + "Skype Country  : " + ui.Country.ToString();
                    Lbl_profile.Text += "\n" + "Skype City     : " + ui.City.ToString();
                    Lbl_profile.Text += "\n" + "Skype Birthday : " + ui.Birthday.ToString();
                }
                else
                {
                    Lblstatus.ResetText();
                    Lblstatus.Text = "Please Select the Invite";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Lbl_close_Click(object sender, EventArgs e)
        {
            try
            {
                Pnl_Profile.Visible = false;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString ());
            }
        }

        private void Btn_block_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListBox_inviations.SelectedItem != string.Empty)
                {
                    Lblstatus.ResetText();
                    ui = oskype.User[ListBox_inviations.SelectedItem.ToString()];
                    ui.IsBlocked = true;
                    Lblstatus.ResetText();
                    Lblstatus.Text = "The invitation from " + ui.Handle + "  is Blocked";
                    ListBox_inviations.Items.Remove(ListBox_inviations.SelectedItem);
                }
                else
                {
                    Lblstatus.ResetText();
                    Lblstatus.Text = "Please Select the Invite";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }

        private void Btn_Ignore_Click(object sender, EventArgs e)
        {

            try
            {
                if (ListBox_inviations.SelectedItem != string.Empty)
                {
                    Lblstatus.ResetText();
                    ui = oskype.User[ListBox_inviations.SelectedItem.ToString()];
                    ui.IsAuthorized = false;
                    Lblstatus.ResetText();
                    Lblstatus.Text = "The invitation from " + ui.Handle + "  is Ignored";
                    ListBox_inviations.Items.Remove(ListBox_inviations.SelectedItem);
                }
                else
                {
                    Lblstatus.ResetText();
                    Lblstatus.Text = "Please Select the Invite";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }
    }
}
