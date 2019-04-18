using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using SKYPE4COMLib;
using gloCommunity.Classes;
////using SkypeControl;

namespace gloCommunity.Forms
{

    public partial class frmgloSkypeChat : Form
    {
        
        public SKYPE4COMLib.Skype oskype;
        public SKYPE4COMLib.User ui;
        public Boolean setfl = false;
        public string skypercdnm;
        private string status = string.Empty;
     
        public SKYPE4COMLib.Call oCall;
        public static int scnt = 0;
       public string _strMobileUser;
        public int _intIncomingCallId;
        public int _intOutgoingCallId;
        public Boolean frmdashbrd = false;
        SKYPE4COMLib.TUserStatus cCallStatus_Finished = new SKYPE4COMLib.TUserStatus();
        IChatMessageCollection ochat;
        public frmgloSkypeChat(string skpcdnm,Boolean isfromdashboard,Boolean isvoice)
        {
            InitializeComponent();
            try
            {

                oskype = new SKYPE4COMLib.Skype();
                oCall = new Call();
                skypercdnm = skpcdnm;
                oskype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(SkypeMessageStatusChanged);
                if (isfromdashboard)
                {
                    frmdashbrd = true;
                    rtxt_callstatus.Visible = false;
                    pnl_Videocall.Visible = false;
                    pnl_Videocall.Dock = DockStyle.None;
                    Panel_textmessage.Visible = true;
                    Panel_textmessage.Dock = DockStyle.Bottom;
                }
                if (isvoice)
                {
                    frmdashbrd = false;
                    rtxt_callstatus.Visible = true;
                    txtChat.Visible = false;
                    pnl_Videocall.Visible = true;
                    pnl_Videocall.Dock = DockStyle.Top;
                    Panel_textmessage.Visible = false;
                    Panel_textmessage.Dock = DockStyle.None;
                }
                else
                {
                    frmdashbrd = false;
                    rtxt_callstatus.Visible = false;
                    pnl_Videocall.Visible = false;
                    pnl_Videocall.Dock = DockStyle.None;
                    Panel_textmessage.Visible = true;
                    Panel_textmessage.Dock = DockStyle.Bottom;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());
            }
            
        }

        private void frmgloSkypeChat_Load(object sender, EventArgs e)
        {
            try
            {
                if (!(oskype.Client.IsRunning))
                {
                    oskype.Client.Start(true, true);
                }
               // try
                //{
                    oskype.Attach(8, true);

                //}
               // catch (Exception ex)
               // {
                 //   MessageBox.Show("Please Login into Skype : It is currently " +ex.Message.ToString (),clsGeneral.gstrMessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
               // }

                this.oskype.CallStatus += new _ISkypeEvents_CallStatusEventHandler(Skype_CallStatus);
                status = oskype.CurrentUserStatus.ToString();
                label3_status.Text = getstatus(status);


                label2_currusernm.Text = oskype.CurrentUser.FullName.ToString() + "< " + oskype.CurrentUserHandle.ToString() + " >";
                Label_welcome.ResetText();
                Label_welcome.Text = "Welcome" + "  " + oskype.CurrentUser.FullName.ToString();


                if (label3_status.Text != "OffLine")
                {
                    for (int i = 1; i <= oskype.Friends.Count; i++)
                    {
                        if (oskype.Friends[i].OnlineStatus == TOnlineStatus.olsOnline)
                        {
                            listBox_friends.Items.Add(oskype.Friends[i].Handle.ToString());
                        }
                    }


                    if (skypercdnm != string.Empty)
                    {
                        for (int i = 0; i <= listBox_friends.Items.Count - 1; i++)
                        {
                            if (listBox_friends.Items[i].ToString() == skypercdnm)
                            {
                                listBox_friends.SetSelected(i, true);
                                setfl = true;
                                listBox_friends_SelectedIndexChanged(null, null);
                                break; // TODO: might not be correct. Was : Exit For

                            }
                        }
                    }
                    setfl = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Equals("Not attached."))
                {
                     MessageBox.Show("Please Login into Skype : It is currently " +ex.Message.ToString (),clsGeneral.gstrMessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                MessageBox.Show(ex.Message.ToString(),clsGeneral.gstrMessageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        public string getstatus(string stat)
        {

            string tempstr = string.Empty;
            try
            {
                switch (stat)
                {
                    case "cusOnline":
                        {
                            tempstr = "Online";
                         //    Pic_currentusrstatus.Image = new Bitmap(Properties.Resources.Online);
                            break; // TODO: might not be correct. Was : Exit Select
                        }
                    case "cusDoNotDisturb":
                        {
                            tempstr = "Do Not Disturb";
                            //  Pic_currentusrstatus.Image = new Bitmap(Properties.Resources.Do_Not_Disturb);
                            break; // TODO: might not be correct. Was : Exit Select
                        }
                    case "cusOffline":
                        {
                            tempstr = "Offline";
                            break; // TODO: might not be correct. Was : Exit Select
                        }

                    case "cusNotAvailable":
                        {
                            tempstr = "Not Available";
                            break; // TODO: might not be correct. Was : Exit Select
                        }

                    case "cusAway":
                        {
                            tempstr = "Away";
                            // Pic_currentusrstatus.Image = new Bitmap(Properties.Resources.Away);
                            break; // TODO: might not be correct. Was : Exit Select
                        }

                    default:
                        {
                            tempstr = string.Empty;
                            break; // TODO: might not be correct. Was : Exit Select
                        }

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return tempstr;
        }
        public string getstatusur(string stat)
        {
            string tempstr = string.Empty;

            try
            {

                switch (stat)
                {
                    case "olsOnline":
                        {
                            tempstr = "Online";
                            Pic_userstat.Image = new Bitmap(Properties.Resources.Online);
                            break; // TODO: might not be correct. Was : Exit Select
                        }

                    case "olsDoNotDisturb":
                        {
                            tempstr = "Do Not Disturb";
                            Pic_userstat.Image = new Bitmap(Properties.Resources.Do_Not_Disturb);
                            break; // TODO: might not be correct. Was : Exit Select
                        }

                    case "olsOffline":
                        {
                            tempstr = "Offline";
                            break; // TODO: might not be correct. Was : Exit Select
                        }

                    case "olsNotAvailable":
                        {
                            tempstr = "Not Available";
                            break; // TODO: might not be correct. Was : Exit Select
                        }

                    case "olsAway":
                        {
                            tempstr = "Away";
                            Pic_userstat.Image = new Bitmap(Properties.Resources.Away);
                            break; // TODO: might not be correct. Was : Exit Select
                        }

                    default:
                        {
                            tempstr = string.Empty;
                            break; // TODO: might not be correct. Was : Exit Select
                        }


                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return tempstr;
        }
        private void Skype_CallStatus(SKYPE4COMLib.Call aCall, TCallStatus aStatus)
        {
            oskype.ResetCache();
            try
            {
                switch (aStatus)
                {
                    case TCallStatus.clsRinging:
                        //A call is ringing, see if it is us calling or
                        //someone calling us
                        if (aCall.Type == TCallType.cltIncomingP2P && scnt == 0)
                        {

                            if (MessageBox.Show("Inoming Call from" + aCall.PartnerHandle.ToString(), "Skype Incoming Call", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                rtxt_callstatus.Text += "\n" + "Answering call from " + aCall.PartnerHandle;

                                aCall.Answer();
                                scnt = 1;
                            }
                            else
                            {
                                aCall.Finish();
                                scnt = 1;
                            }
                            //  
                            this._intIncomingCallId = aCall.Id;

                        }

                        if (aCall.Type == TCallType.cltOutgoingP2P)
                        {

                            this._intOutgoingCallId = aCall.Id;
                        }
                        break;
                    case TCallStatus.clsInProgress:
                        scnt = 0;
                        if (aCall.VideoReceiveStatus == TCallVideoSendStatus.vssRunning)
                        {
                            aCall.StartVideoReceive();

                        }
                        aCall.set_CaptureMicDevice(SKYPE4COMLib.TCallIoDeviceType.callIoDeviceTypeFile, @"c:\Skype1.wav");
                        aCall.set_OutputDevice(SKYPE4COMLib.TCallIoDeviceType.callIoDeviceTypeFile, @"c:\Skype2.wav");

                        // We have a new call opened. Make sure it's our outgoing call
                        if (aCall.Type == TCallType.cltOutgoingP2P)
                        {

                            rtxt_callstatus.Text += "\n" + "User has answered, attempting to join calls";
                            foreach (SKYPE4COMLib.Call objCall in oskype.ActiveCalls)
                            {
                                if (objCall.Id == this._intIncomingCallId)
                                {
                                    rtxt_callstatus.Text += "Joining the calls...";
                                    objCall.Join(aCall.Id);

                                    rtxt_callstatus.Text += "Taking incoming call off hold";
                                    objCall.Resume();
                                }
                            }


                        }
                        break;
                    case TCallStatus.clsFinished:
                        // Someone has hung up, end the call
                        scnt = 0;
                        rtxt_callstatus.Text += "\n " + "Someone has hung up. Attempting to end the conference";
                        foreach (SKYPE4COMLib.Conference objConf in oskype.Conferences)
                        {
                            foreach (SKYPE4COMLib.Call objCall in objConf.Calls)
                            {
                                if (objCall.Id == this._intIncomingCallId || objCall.Id == this._intOutgoingCallId)
                                {
                                    System.Threading.Thread.Sleep(500);
                                    try
                                    {
                                        objCall.Finish();
                                    }
                                    catch (Exception) { }
                                    try
                                    {
                                        objConf.Finish();
                                    }
                                    catch (Exception) { }
                                }
                            }
                        }
                        break;
                    default:
                        // Something else?
                        if ((aCall.Type == TCallType.cltOutgoingP2P || aCall.Type == TCallType.cltOutgoingPSTN) && (
                                aCall.Status == TCallStatus.clsCancelled ||
                                aCall.Status == TCallStatus.clsFailed ||
                                aCall.Status == TCallStatus.clsMissed ||
                                aCall.Status == TCallStatus.clsRefused ||
                                aCall.Status == TCallStatus.clsVoicemailPlayingGreeting ||
                                aCall.Status == TCallStatus.clsVoicemailRecording
                            )
                           )
                        {

                            foreach (SKYPE4COMLib.Call objCall in oskype.ActiveCalls)
                            {
                                if (objCall.Id == this._intOutgoingCallId)
                                {
                                    try
                                    {
                                        objCall.Finish();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error trying to end voicemail call: " + ex.Message);
                                    }
                                }
                            }
                            // Now redirect the incoming call
                            foreach (SKYPE4COMLib.Call objCall in oskype.ActiveCalls)
                            {
                                if (objCall.Id == this._intIncomingCallId)
                                {
                                    System.Threading.Thread.Sleep(500);
                                    try
                                    {
                                        //objCall.Resume();
                                        objCall.RedirectToVoicemail();
                                        objCall.Finish();
                                        //objCall.Status = TCallStatus.clsFinished;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error trying to divert to voicemail: " + ex.Message);
                                        objCall.Finish();
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listBox_friends_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((setfl == true) && (listBox_friends.SelectedItem.ToString() != null))
                {
                    oskype.ResetCache();
                    Label_chatuser.ResetText();
                    txtChat.ResetText();
                    rtxt_callstatus.ResetText();
                    Label_chatuser.Text = listBox_friends.SelectedItem.ToString();
                    _strMobileUser = string.Empty;
                    _strMobileUser = listBox_friends.SelectedItem.ToString();

                    Label_timeLoc.ResetText();

                    SKYPE4COMLib.User ui = new SKYPE4COMLib.User();
                    ui = oskype.User[listBox_friends.SelectedItem.ToString()];

                    label_typemes.ResetText();
                    label_typemes.Text = "Type Message to " + ui.FullName + " here ";

                    Label_timeLoc.Text = String.Format("{0:t}", DateTime.Now) + " " + ui.City + "," + ui.Country;


                    Lbl_uronlinestatus.ResetText();
                    Lbl_uronlinestatus.Text = getstatusur(ui.OnlineStatus.ToString());


                    IChat ichat = oskype.CreateChatWith(listBox_friends.SelectedItem.ToString());
                    //_strMobileUser = listBox_friends.SelectedItem.ToString();
                    ochat = ichat.Messages;
                    txtChat.Text = "";
                    for (int i = ochat.Count; i > 0; i--)
                    {
                        txtChat.Text += "\n" + ochat[i].FromHandle + ":   \n" + ochat[i].Body + "  ";
                    }


                    if (txtChat.Text.Length > 0)
                    {
                        txtChat.Focus();

                        txtChat.SelectionStart = txtChat.Text.Length;


                    }

                }
                text_sendmsg.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                IChat ichat = oskype.CreateChatWith(listBox_friends.SelectedItem.ToString());
                ChatMessage objc = new ChatMessage();

                //oskype.CreateChatWith(ichat.FriendlyName);

                objc = ichat.SendMessage(text_sendmsg.Text);
                text_sendmsg.Refresh();
                text_sendmsg.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }
        public void SkypeMessageStatusChanged(IChatMessage pMessage, TChatMessageStatus Status)
        {

            try
            {
                if ((Status == TChatMessageStatus.cmsReceived))
                {
                    if ((frmdashbrd == true) && listBox_friends.SelectedItem.ToString() == null)
                    {
                        txtChat.Text += "\n" + pMessage.FromHandle + ":   \n" + pMessage.Body;
                    }
                    else if ((frmdashbrd==false) && (pMessage.FromHandle == listBox_friends.SelectedItem.ToString()))
                    {
                        txtChat.Text += "\n" + pMessage.FromHandle + ":   \n" + pMessage.Body;
                    }
                     

                    
                }

                if ((Status == TChatMessageStatus.cmsSending))
                {
                    txtChat.Text += "\n" + pMessage.FromHandle + ":   \n" + pMessage.Body + " \n";

                }

                if (txtChat.Text.Length > 0)
                {
                    txtChat.Focus();

                    txtChat.SelectionStart = txtChat.Text.Length;


                }
                text_sendmsg.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void But_voicall_Click(object sender, EventArgs e)
        {
            try
            {
                oCall = oskype.PlaceCall(_strMobileUser, null, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
             
        }

        private void But_Hold_Click(object sender, EventArgs e)
        {
            foreach (SKYPE4COMLib.Call objCall in oskype.ActiveCalls)
            {
                try
                {
                    if (objCall.Status == TCallStatus.clsInProgress)
                    {
                        rtxt_callstatus.Text += "\n" + "Some body is attempting to hold calls";
                        try
                        {

                            objCall.Hold();


                            rtxt_callstatus.Text += "\n" + "Call is on Hold ";
                            But_Hold.Visible = false;
                            But_Resume.Visible = true;
                            But_Hold.TextAlign = ContentAlignment.MiddleRight;
                        }
                        catch (Exception ex)
                        {

                        }



                    }
                }
                catch (Exception ex1)
                {
                    rtxt_callstatus.Text += "\n" + "Error trying to end voicemail call: " + ex1.Message.ToString();
                }
            }
        }

        private void But_End_Click(object sender, EventArgs e)
        {
            foreach (SKYPE4COMLib.Call objCall in oskype.ActiveCalls)
            {
                try
                {
                    scnt = 0;
                    objCall.Finish();
                    But_Hold.Visible = true;
                    But_Resume.Visible = false;
                }
                catch (Exception ex)
                {
                    rtxt_callstatus.Text += "Error trying to end voicemail call: " + ex.Message.ToString();
                }
            }

        }

        private void But_Resume_Click(object sender, EventArgs e)
        {
            foreach (SKYPE4COMLib.Call objCall in oskype.ActiveCalls)
            {
                try
                {
                    if (objCall.Status == TCallStatus.clsLocalHold)
                    {
                        rtxt_callstatus.Text += "\n" + "Some body is attempting to Resume calls";
                        try
                        {

                            objCall.Resume();


                            rtxt_callstatus.Text += "\n" + "Call is on Resumed ";
                            But_Resume.Visible = false;
                            But_Hold.Visible = true;

                        }
                        catch (Exception ex)
                        {

                        }



                    }
                    else
                    {
                        rtxt_callstatus.Text += "\n" + "Call is not yet Received: ";
                    }
                }
                catch (Exception ex1)
                {
                    rtxt_callstatus.Text += "\n" + "Error trying to end voicemail call: " + ex1.Message.ToString();
                }
            }
        }

        private void text_sendmsg_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSend_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


    }
}
