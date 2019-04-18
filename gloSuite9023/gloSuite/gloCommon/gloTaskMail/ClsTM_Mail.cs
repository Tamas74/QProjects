using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace gloTaskMail
{
    public class gloMail : IDisposable
    {
        #region " Declarations "

            private string _databaseconnectionstring = "";
            //private string _messageBoxCaption = "gloPMS";
            private string _messageBoxCaption = String.Empty;
            private Int64 _userId = 0;
            private Int64 _mailId = 0;
            private Int64 _nClinicId = 0;

        #endregion " Declarations "

        #region "Constructor & Distructor"


        public gloMail(string DatabaseConnectionString)
         {
             _databaseconnectionstring = DatabaseConnectionString;
             System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
             _userId = Convert.ToInt64(appSettings["UserID"]);


             //Added By Pramod Nair For Messagebox Caption 
             #region " Retrieve MessageBoxCaption from AppSettings "

             if (appSettings["MessageBOXCaption"] != null)
             {
                 if (appSettings["MessageBOXCaption"] != "")
                 {
                     _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                 }
                 else
                 {
                     _messageBoxCaption = "gloPM";
                 }
             }
             else
             { _messageBoxCaption = "gloPM"; }

             #endregion

             if (appSettings["ClinicID"] != null)
             {
                 if (appSettings["ClinicID"] != "")
                 {
                     _nClinicId = Convert.ToInt64(appSettings["ClinicID"]);
                 }
                 else { _nClinicId = 0; }
             }
             else
             { _nClinicId = 0; }
         }

         private bool disposed = false;

         public void Dispose()
         {
             Dispose(true);
             GC.SuppressFinalize(this);
         }
         protected virtual void Dispose(bool disposing)
         {
             if (!this.disposed)
             {
                 if (disposing)
                 {

                 }
             }
             disposed = true;
         }

        ~gloMail()
         {
             Dispose(false);
         }

     #endregion

        #region " Property Procedure "

            public string DatabaseConnectionString
            {
                get { return _databaseconnectionstring; }
                set { _databaseconnectionstring = value; }
            }

            public Int64 UserID
            {
                get { return _userId; }
                set { _userId = value; }
            }

            public Int64 MailID
            {
                get { return _mailId; }
                set { _mailId = value; }
            }
            
         #endregion " Property Procedure "

        #region " Private & Public Methods "

        public void AddMail(Mail oMail)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object oResult = new object();

            try
            {
                oDB.Connect(false);

                #region " Sent Mail "

                //First we will add Values to TM_Mail_SentItem Table.
                //i.e. Entry of the mail against the owner of the mail is made as the sent item of the owner.

                //TM_Mail_SentItem -> @nMailID,@sSubject,@sBody,@nOwnerID,@nPriorityID,@nCategoryID,@iAttachment1,@iAttachment2,
                // @iAttachment3,@iAttachment4,@iAttachment5

                oParameters.Add("@nMailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sSubject", oMail.Subject, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@iBody", oMail.Body, ParameterDirection.Input, SqlDbType.Image);
                oParameters.Add("@nOwnerID", oMail.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPriorityID", oMail.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCategoryID", oMail.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                if (oMail.Attachment1 != null)
                {
                    oParameters.Add("@iAttachment1", oMail.Attachment1, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment1", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }
                if (oMail.Attachment2 != null)
                {
                    oParameters.Add("@iAttachment2", oMail.Attachment2, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment2", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }

                if (oMail.Attachment3 != null)
                {
                    oParameters.Add("@iAttachment3", oMail.Attachment3, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment3", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }

                if (oMail.Attachment4 != null)
                {
                    oParameters.Add("@iAttachment4", oMail.Attachment4, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment4", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }

                if (oMail.Attachment5 != null)
                {
                    oParameters.Add("@iAttachment5", oMail.Attachment5, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment5", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }
                oParameters.Add("@sAttachmentName1", oMail.AttachmentName1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@sAttachmentName2", oMail.AttachmentName2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@sAttachmentName3", oMail.AttachmentName3, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@sAttachmentName4", oMail.AttachmentName4, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@sAttachmentName5", oMail.AttachmentName5, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@nClinicID", oMail.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                int _result = oDB.Execute("TM_Mail_IN_Mail", oParameters, out oResult);

                if (_result <= 0)
                {
                    MessageBox.Show("ERROR : Sending Mail Record ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (oResult != null)
                    {
                        MailID = Convert.ToInt64(oResult);
                    }

                    //Check for Return MailID if 0 return else continue
                    if (MailID <= 0)
                    {
                        MessageBox.Show("Return MailID error");
                        return;
                    }
                }

                #endregion " Sent Mail "

                #region " Mail Transaction "

                // * Make Entry for 
                //TM_Mail_Transaction -> nMailID numeric(18, 0),nMailerID numeric(18, 0),nMailFlag smallint

                #region " Mail From Entries - SentItems "

                //Make entries for From in Transaction Table
                //Make entry for From who this mail is been sent
                oParameters.Clear();
                try
                {
                    oParameters.Add("@nMailID", MailID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nMailerID", oMail.FromID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("nMailFlag", Convert.ToInt16(gloTasksMails.Common.MailFlag.From), ParameterDirection.Input, SqlDbType.SmallInt);

                    oDB.Execute("TM_Mail_IN_Transaction", oParameters);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }


                #endregion " Mail From Entries - SentItems "

                #region " Mail To Entries - SentItems "

                //Make entries for To list in Transaction Table
                //Entry for the list of persons to whom this mail is Addressed directly(i.e the To list of Mail)
                oParameters.Clear();

                for (int i = 0; i < oMail.To.Count; i++)
                {
                    try
                    {
                        oParameters.Add("@nMailID", MailID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nMailerID", oMail.To[i].ID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nMailFlag", Convert.ToInt16(gloTasksMails.Common.MailFlag.To), ParameterDirection.Input, SqlDbType.SmallInt);

                        oDB.Execute("TM_Mail_IN_Transaction", oParameters);
                        oParameters.Clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }//for (int i = 0; i < oMail.To.Count ; i++)


                #endregion " Mail To Entries - SentItems "

                #region " Mail Cc Entries - SentItems "

                //Make entries for Cc list in Transaction Table
                oParameters.Clear();

                if (oMail.Cc != null)
                {
                    for (int i = 0; i < oMail.Cc.Count; i++)
                    {
                        try
                        {
                            oParameters.Add("@nMailID", MailID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nMailerID", oMail.Cc[i].ID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nMailFlag", Convert.ToInt16(gloTasksMails.Common.MailFlag.Cc), ParameterDirection.Input, SqlDbType.SmallInt);

                            oDB.Execute("TM_Mail_IN_Transaction", oParameters);
                            oParameters.Clear();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }


                #endregion " Mail Cc Entries - SentItems "

                #region " Mail BCc Entries - SentItems "

                //Make entries for BCc list in Transaction Table
                oParameters.Clear();

                if (oMail.BCc != null)
                {
                    for (int i = 0; i < oMail.BCc.Count; i++)
                    {
                        try
                        {
                            oParameters.Add("@nMailID", MailID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nMailerID", oMail.BCc[i].ID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nMailFlag", Convert.ToInt16(gloTasksMails.Common.MailFlag.BCc), ParameterDirection.Input, SqlDbType.SmallInt);

                            oDB.Execute("TM_Mail_IN_Transaction", oParameters);
                            oParameters.Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }

                }

                #endregion " Mail BCc Entries - SentItems "


                #endregion " Mail Transaction "

                #region  " Mail Inbox "

                // * Make Entry for 
                // TM_Mail_Inbox -> nMailID	numeric(18, 0),sSubject	varchar(255),sBody	varchar(2000),nOwnerID	numeric(18, 0),	
                //nMailLinkID	numeric(18, 0),nPriorityID	numeric(18, 0),nCategoryID	numeric(18, 0),iAttachment1	image,
                //iAttachment2	image,iAttachment3	image,iAttachment4	image,iAttachment5	image

                #region  " Mail To Entries - Inbox "

                //Copy of this mail is saved against the Reciever individually in the Inbox Table for each To List person.


                oParameters.Clear();
                for (int i = 0; i < oMail.To.Count; i++)
                {
                    try
                    {
                        oParameters.Add("@nMailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                        oParameters.Add("@sSubject", oMail.Subject, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        oParameters.Add("@iBody", oMail.Body, ParameterDirection.Input, SqlDbType.Image);
                        oParameters.Add("@nOwnerID", oMail.To[i].ID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nPriorityID", oMail.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nCategoryID", oMail.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                        //oParameters.Add("@iAttachment1", oMail.Attachment1, ParameterDirection.Input, SqlDbType.Image);
                        //oParameters.Add("@iAttachment2", oMail.Attachment2, ParameterDirection.Input, SqlDbType.Image);
                        //oParameters.Add("@iAttachment3", oMail.Attachment3, ParameterDirection.Input, SqlDbType.Image);
                        //oParameters.Add("@iAttachment4", oMail.Attachment4, ParameterDirection.Input, SqlDbType.Image);
                        //oParameters.Add("@iAttachment5", oMail.Attachment5, ParameterDirection.Input, SqlDbType.Image);
                        if (oMail.Attachment1 != null)
                        {
                            oParameters.Add("@iAttachment1", oMail.Attachment1, ParameterDirection.Input, SqlDbType.Image);
                        }
                        else
                        {
                            oParameters.Add("@iAttachment1", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                        }
                        if (oMail.Attachment2 != null)
                        {
                            oParameters.Add("@iAttachment2", oMail.Attachment2, ParameterDirection.Input, SqlDbType.Image);
                        }
                        else
                        {
                            oParameters.Add("@iAttachment2", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                        }

                        if (oMail.Attachment3 != null)
                        {
                            oParameters.Add("@iAttachment3", oMail.Attachment3, ParameterDirection.Input, SqlDbType.Image);
                        }
                        else
                        {
                            oParameters.Add("@iAttachment3", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                        }

                        if (oMail.Attachment4 != null)
                        {
                            oParameters.Add("@iAttachment4", oMail.Attachment4, ParameterDirection.Input, SqlDbType.Image);
                        }
                        else
                        {
                            oParameters.Add("@iAttachment4", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                        }

                        if (oMail.Attachment5 != null)
                        {
                            oParameters.Add("@iAttachment5", oMail.Attachment5, ParameterDirection.Input, SqlDbType.Image);
                        }
                        else
                        {
                            oParameters.Add("@iAttachment5", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                        }
                        oParameters.Add("@sAttachmentName1", oMail.AttachmentName1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        oParameters.Add("@sAttachmentName2", oMail.AttachmentName2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        oParameters.Add("@sAttachmentName3", oMail.AttachmentName3, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        oParameters.Add("@sAttachmentName4", oMail.AttachmentName4, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        oParameters.Add("@sAttachmentName5", oMail.AttachmentName5, ParameterDirection.Input, SqlDbType.VarChar, 255);
                        oParameters.Add("@nMailLinkID", MailID, ParameterDirection.Input, SqlDbType.BigInt);

                        oParameters.Add("@bIsRead", oMail.IsRead, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nClinicID", oMail.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                        oDB.Execute("TM_IN_Mail_Inbox", oParameters);
                        oParameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                #endregion  " Mail To Entries - Inbox "

                #region  " Mail Cc Entries - Inbox "

                if (oMail.Cc != null)
                {
                    oParameters.Clear();
                    for (int i = 0; i < oMail.Cc.Count; i++)
                    {
                        try
                        {
                            oParameters.Add("@nMailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oParameters.Add("@sSubject", oMail.Subject, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@iBody", oMail.Body, ParameterDirection.Input, SqlDbType.Image);
                            oParameters.Add("@nOwnerID", oMail.Cc[i].ID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPriorityID", oMail.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nCategoryID", oMail.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                            //oParameters.Add("@iAttachment1", oMail.Attachment1, ParameterDirection.Input, SqlDbType.Image);
                            //oParameters.Add("@iAttachment2", oMail.Attachment2, ParameterDirection.Input, SqlDbType.Image);
                            //oParameters.Add("@iAttachment3", oMail.Attachment3, ParameterDirection.Input, SqlDbType.Image);
                            //oParameters.Add("@iAttachment4", oMail.Attachment4, ParameterDirection.Input, SqlDbType.Image);
                            //oParameters.Add("@iAttachment5", oMail.Attachment5, ParameterDirection.Input, SqlDbType.Image);
                            if (oMail.Attachment1 != null)
                            {
                                oParameters.Add("@iAttachment1", oMail.Attachment1, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment1", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }
                            if (oMail.Attachment2 != null)
                            {
                                oParameters.Add("@iAttachment2", oMail.Attachment2, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment2", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }

                            if (oMail.Attachment3 != null)
                            {
                                oParameters.Add("@iAttachment3", oMail.Attachment3, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment3", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }

                            if (oMail.Attachment4 != null)
                            {
                                oParameters.Add("@iAttachment4", oMail.Attachment4, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment4", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }

                            if (oMail.Attachment5 != null)
                            {
                                oParameters.Add("@iAttachment5", oMail.Attachment5, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment5", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }
                            oParameters.Add("@sAttachmentName1", oMail.AttachmentName1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@sAttachmentName2", oMail.AttachmentName2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@sAttachmentName3", oMail.AttachmentName3, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@sAttachmentName4", oMail.AttachmentName4, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@sAttachmentName5", oMail.AttachmentName5, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@nMailLinkID", MailID, ParameterDirection.Input, SqlDbType.BigInt);

                            oParameters.Add("@bIsRead", oMail.IsRead, ParameterDirection.Input, SqlDbType.Bit);
                            oParameters.Add("@nClinicID", oMail.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                            oDB.Execute("TM_IN_Mail_Inbox", oParameters);
                            oParameters.Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }


                #endregion  " Mail Cc Entries - Inbox "

                #region  " Mail BCc Entries - Inbox "

                if (oMail.BCc != null)
                {
                    oParameters.Clear();
                    for (int i = 0; i < oMail.BCc.Count; i++)
                    {
                        try
                        {
                            oParameters.Add("@nMailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oParameters.Add("@sSubject", oMail.Subject, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@iBody", oMail.Body, ParameterDirection.Input, SqlDbType.Image);
                            oParameters.Add("@nOwnerID", oMail.BCc[i].ID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nPriorityID", oMail.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                            oParameters.Add("@nCategoryID", oMail.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                            //oParameters.Add("@iAttachment1", oMail.Attachment1, ParameterDirection.Input, SqlDbType.Image);
                            //oParameters.Add("@iAttachment2", oMail.Attachment2, ParameterDirection.Input, SqlDbType.Image);
                            //oParameters.Add("@iAttachment3", oMail.Attachment3, ParameterDirection.Input, SqlDbType.Image);
                            //oParameters.Add("@iAttachment4", oMail.Attachment4, ParameterDirection.Input, SqlDbType.Image);
                            //oParameters.Add("@iAttachment5", oMail.Attachment5, ParameterDirection.Input, SqlDbType.Image);
                            if (oMail.Attachment1 != null)
                            {
                                oParameters.Add("@iAttachment1", oMail.Attachment1, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment1", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }
                            if (oMail.Attachment2 != null)
                            {
                                oParameters.Add("@iAttachment2", oMail.Attachment2, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment2", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }

                            if (oMail.Attachment3 != null)
                            {
                                oParameters.Add("@iAttachment3", oMail.Attachment3, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment3", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }

                            if (oMail.Attachment4 != null)
                            {
                                oParameters.Add("@iAttachment4", oMail.Attachment4, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment4", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }

                            if (oMail.Attachment5 != null)
                            {
                                oParameters.Add("@iAttachment5", oMail.Attachment5, ParameterDirection.Input, SqlDbType.Image);
                            }
                            else
                            {
                                oParameters.Add("@iAttachment5", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                            }
                            oParameters.Add("@sAttachmentName1", oMail.AttachmentName1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@sAttachmentName2", oMail.AttachmentName2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@sAttachmentName3", oMail.AttachmentName3, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@sAttachmentName4", oMail.AttachmentName4, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@sAttachmentName5", oMail.AttachmentName5, ParameterDirection.Input, SqlDbType.VarChar, 255);
                            oParameters.Add("@nMailLinkID", MailID, ParameterDirection.Input, SqlDbType.BigInt);

                            oParameters.Add("@bIsRead", oMail.IsRead, ParameterDirection.Input, SqlDbType.Bit);
                            oParameters.Add("@nClinicID", oMail.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                            oDB.Execute("TM_IN_Mail_Inbox", oParameters);
                            oParameters.Clear();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }


                #endregion  " Mail Cc Entries - Inbox "

                #endregion  " Mail Inbox "

                MessageBox.Show("Mail sent successfully.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                oMail.Dispose();
            }

        }//private void AddMail(Mail oMail)

        public bool DeleteInboxMail(Int64 Mailid)
        {
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; ;
            }
            finally
            {

            }

        }

        public bool DeleteSentMail(Int64 Mailid)
        {

            try
            {
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {

            }

        }

        public bool DeleteDraftMail(Int64 DraftMailId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = "delete from TM_Mail_Drafts where nDraftMailID = "+ DraftMailId;
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log("Delete Draft Mail : " + dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public Mails getInboxMails(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMails = new DataTable();
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTaskMail.Mails oMails = new Mails();
            gloTaskMail.Mail oMail;
            String strQuery = "";

            try
            {
                oDB.Connect(false);
                if (UserId <= 0)
                {
                    return null;
                }

                //strQuery = " SELECT  TM_Mail_Inbox.nMailID, TM_Mail_Inbox.sSubject, TM_Mail_Inbox.iBody, TM_Mail_Inbox.nOwnerID, TM_Mail_Inbox.nMailLinkID, "
                //         + " TM_Mail_Inbox.nPriorityID, TM_Mail_Inbox.nCategoryID, TM_Mail_Inbox.iAttachment1, TM_Mail_Inbox.iAttachment2, TM_Mail_Inbox.iAttachment3, "
                //         + " TM_Mail_Inbox.iAttachment4, TM_Mail_Inbox.iAttachment5, TM_Mail_Inbox.sAttachmentName1, TM_Mail_Inbox.sAttachmentName2, "
                //         + " TM_Mail_Inbox.sAttachmentName3, TM_Mail_Inbox.sAttachmentName4, TM_Mail_Inbox.sAttachmentName5, TM_Mail_Transaction.nMailerID "
                //         + " FROM TM_Mail_Inbox INNER JOIN TM_Mail_Transaction ON TM_Mail_Inbox.nMailLinkID = TM_Mail_Transaction.nMailID "
                //         + " AND TM_Mail_Inbox.nMailLinkID = TM_Mail_Transaction.nMailID WHERE TM_Mail_Transaction.nMailFlag = 1 AND TM_Mail_Inbox.nOwnerID ="+UserId+" ";

                strQuery = " SELECT  TM_Mail_Inbox.nMailID, TM_Mail_Inbox.sSubject, TM_Mail_Inbox.iBody, TM_Mail_Inbox.nOwnerID, TM_Mail_Inbox.nMailLinkID, "
                         + " TM_Mail_Inbox.nPriorityID, TM_Mail_Inbox.nCategoryID, TM_Mail_Inbox.iAttachment1, TM_Mail_Inbox.iAttachment2, TM_Mail_Inbox.iAttachment3, "
                         + " TM_Mail_Inbox.iAttachment4, TM_Mail_Inbox.iAttachment5, TM_Mail_Inbox.sAttachmentName1, TM_Mail_Inbox.sAttachmentName2, "
                         + " TM_Mail_Inbox.sAttachmentName3, TM_Mail_Inbox.sAttachmentName4, TM_Mail_Inbox.sAttachmentName5, TM_Mail_Transaction.nMailerID,TM_Mail_Inbox.bIsRead,TM_Mail_Inbox.nClinicID "
                         + " FROM TM_Mail_Inbox INNER JOIN TM_Mail_Transaction ON TM_Mail_Inbox.nMailLinkID = TM_Mail_Transaction.nMailID "
                         + " AND TM_Mail_Inbox.nMailLinkID = TM_Mail_Transaction.nMailID WHERE TM_Mail_Transaction.nMailFlag = 1 AND TM_Mail_Inbox.nOwnerID =" + UserId + " ";

                oDB.Retrive_Query(strQuery, out dtMails);

                if (dtMails != null && dtMails.Rows.Count > 0)
                {

                    //dbo.TM_Mail_Inbox:-nMailID,sSubject,iBody,nOwnerID,nMailLinkID,nPriorityID,nCategoryID
                    //iAttachment1,iAttachment2,iAttachment3,iAttachment4,iAttachment5,
                    //sAttachmentName1,sAttachmentName2,sAttachmentName3,sAttachmentName4,sAttachmentName5

                    for (int i = 0; i < dtMails.Rows.Count; i++)
                    {
                        oMail = new Mail();
                        oMail.MailID = Convert.ToInt64(dtMails.Rows[i]["nMailID"]);

                        oMail.FromID = Convert.ToInt64(dtMails.Rows[i]["nMailerID"]);
                        gloTaskMail.gloTask ogloTask = new gloTask(_databaseconnectionstring);
                        oMail.FromName = ogloTask.GetUserName(oMail.FromID);

                        oMail.Subject = dtMails.Rows[i]["sSubject"].ToString();

                        oMail.Body = (Object)dtMails.Rows[i]["iBody"];

                        oMail.OwnerID = Convert.ToInt64(dtMails.Rows[i]["nOwnerID"]);
                        gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
                        oMail.OwnerName = oTask.GetUserName(oMail.OwnerID);
                        oTask.Dispose();


                        oMail.MailLinkID = Convert.ToInt64(dtMails.Rows[i]["nMailLinkID"]);

                        oMail.PriorityID = Convert.ToInt64(dtMails.Rows[i]["nPriorityID"]);
                        gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                        oPriority = oTaskMail.GetPriority(oMail.PriorityID);
                        oMail.Priority = oPriority.Description;
                        oPriority.Dispose();

                        oMail.CategoryID = Convert.ToInt64(dtMails.Rows[i]["nCategoryID"]);
                        gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                        //oCategory = oTaskMail.GetCategory(oMail.CategoryID);

                        oCategory = oTaskMail.GetCategory(2);   //Hard Coded for Testing ---
                        oMail.Category = oCategory.Description;
                        oCategory.Dispose();
                        oCategory = null;

                        //oMail.Attachment1 = (Object)dtMails.Rows[i]["iAttachment1"];
                        //oMail.Attachment2 = (Object)dtMails.Rows[i]["iAttachment2"];
                        //oMail.Attachment3 = (Object)dtMails.Rows[i]["iAttachment3"];
                        //oMail.Attachment4 = (Object)dtMails.Rows[i]["iAttachment4"];
                        //oMail.Attachment5 = (Object)dtMails.Rows[i]["iAttachment5"];
                        //Check for Attachments if exists set ,else set null

                        //Attachment1
                        if (dtMails.Rows[i]["iAttachment1"].ToString() == "")
                            oMail.Attachment1 = null;
                        else
                            oMail.Attachment1 = (Object)dtMails.Rows[i]["iAttachment1"];

                        //Attachment2
                        if (dtMails.Rows[i]["iAttachment2"].ToString() == "")
                            oMail.Attachment2 = null;
                        else
                            oMail.Attachment2 = (Object)dtMails.Rows[i]["iAttachment2"];

                        //Attachment3
                        if (dtMails.Rows[i]["iAttachment3"].ToString() == "")
                            oMail.Attachment3 = null;
                        else
                            oMail.Attachment3 = (Object)dtMails.Rows[i]["iAttachment3"];

                        //Attachment4
                        if (dtMails.Rows[i]["iAttachment4"].ToString() == "")
                            oMail.Attachment4 = null;
                        else
                            oMail.Attachment4 = (Object)dtMails.Rows[i]["iAttachment4"];

                        //Attachment5
                        if (dtMails.Rows[i]["iAttachment5"].ToString() == "")
                            oMail.Attachment5 = null;
                        else
                            oMail.Attachment5 = (Object)dtMails.Rows[i]["iAttachment5"];
                        //


                        oMail.AttachmentName1 = dtMails.Rows[i]["sAttachmentName1"].ToString();
                        oMail.AttachmentName2 = dtMails.Rows[i]["sAttachmentName2"].ToString();
                        oMail.AttachmentName3 = dtMails.Rows[i]["sAttachmentName3"].ToString();
                        oMail.AttachmentName4 = dtMails.Rows[i]["sAttachmentName4"].ToString();
                        oMail.AttachmentName5 = dtMails.Rows[i]["sAttachmentName5"].ToString();

                        oMail.IsRead = Convert.ToBoolean(dtMails.Rows[i]["bIsRead"]);
                        if (dtMails.Rows[i]["nClinicID"].ToString() != DBNull.Value.ToString())
                        {
                            oMail.ClinicID = Convert.ToInt64(dtMails.Rows[i]["nClinicID"]);
                        }

                        oMails.Add(oMail);
                        oMail = null;
                    }
                    return oMails;

                }

                return null;

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR :Retrive Inbox failed " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtMails.Dispose();
                oTaskMail.Dispose();

            }
        }

        public Mails getSentMails(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMails = new DataTable();
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTaskMail.Mails oMails = new Mails();
            gloTaskMail.Mail oMail;
            String strQuery = "";

            try
            {
                oDB.Connect(false);
                if (UserId <= 0)
                {
                    return null;
                }

                //strQuery = "select * from TM_Mail_SentItem where nOwnerID=" + UserId; //Remove select *
                  strQuery = "select nMailID, sSubject, iBody, nOwnerID, nPriorityID, nCategoryID, iAttachment1, iAttachment2, iAttachment3, iAttachment4, iAttachment5, " + 
                            " sAttachmentName1, sAttachmentName2, sAttachmentName3, sAttachmentName4, sAttachmentName5, nClinicID from TM_Mail_SentItem where nOwnerID=" + UserId;

                oDB.Retrive_Query(strQuery, out dtMails);

                if (dtMails != null && dtMails.Rows.Count > 0)
                {

                    //TM_Mail_SentItem -> nMailID,sSubject,iBody,nOwnerID,nPriorityID,nCategoryID,iAttachment1,iAttachment2
                    //iAttachment3,iAttachment4,iAttachment5,sAttachmentName1,sAttachmentName2,sAttachmentName3
                    //sAttachmentName4,sAttachmentName5

                    for (int i = 0; i < dtMails.Rows.Count; i++)
                    {
                        oMail = new Mail();
                        oMail.MailID = Convert.ToInt64(dtMails.Rows[i]["nMailID"]);

                        // Get the List of Users to whom this mail has been sent.
                        oMail.To = getToListOfMail(oMail.MailID);
                        oMail.Cc = getCcListOfMail(oMail.MailID);
                        oMail.BCc = getBCcListOfMail(oMail.MailID);
                        //

                        oMail.Subject = dtMails.Rows[i]["sSubject"].ToString();

                        oMail.Body = (Object)dtMails.Rows[i]["iBody"];

                        oMail.OwnerID = Convert.ToInt64(dtMails.Rows[i]["nOwnerID"]);
                        gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
                        oMail.OwnerName = oTask.GetUserName(oMail.OwnerID);
                        oTask.Dispose();


                        //oMail.MailLinkID = Convert.ToInt64(dtMails.Rows[i]["nMailLinkID"]);

                        oMail.PriorityID = Convert.ToInt64(dtMails.Rows[i]["nPriorityID"]);
                        gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                        oPriority = oTaskMail.GetPriority(oMail.PriorityID);
                        oMail.Priority = oPriority.Description;
                        oPriority.Dispose();

                        oMail.CategoryID = Convert.ToInt64(dtMails.Rows[i]["nCategoryID"]);
                        gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                        oCategory = oTaskMail.GetCategory(oMail.CategoryID);
                        if (oCategory != null)
                        {
                            oMail.Category = oCategory.Description;
                        }
                        oCategory.Dispose();
                        oCategory = null;

                        //oMail.Attachment1 = (Object)dtMails.Rows[i]["iAttachment1"];
                        //oMail.Attachment2 = (Object)dtMails.Rows[i]["iAttachment2"];
                        //oMail.Attachment3 = (Object)dtMails.Rows[i]["iAttachment3"];
                        //oMail.Attachment4 = (Object)dtMails.Rows[i]["iAttachment4"];
                        //oMail.Attachment5 = (Object)dtMails.Rows[i]["iAttachment5"];
                        //Check for Attachments if exists set ,else set null

                        //Attachment1
                        if (dtMails.Rows[i]["iAttachment1"].ToString() == "")
                            oMail.Attachment1 = null;
                        else
                            oMail.Attachment1 = (Object)dtMails.Rows[i]["iAttachment1"];

                        //Attachment2
                        if (dtMails.Rows[i]["iAttachment2"].ToString() == "")
                            oMail.Attachment2 = null;
                        else
                            oMail.Attachment2 = (Object)dtMails.Rows[i]["iAttachment2"];

                        //Attachment3
                        if (dtMails.Rows[i]["iAttachment3"].ToString() == "")
                            oMail.Attachment3 = null;
                        else
                            oMail.Attachment3 = (Object)dtMails.Rows[i]["iAttachment3"];

                        //Attachment4
                        if (dtMails.Rows[i]["iAttachment4"].ToString() == "")
                            oMail.Attachment4 = null;
                        else
                            oMail.Attachment4 = (Object)dtMails.Rows[i]["iAttachment4"];

                        //Attachment5
                        if (dtMails.Rows[i]["iAttachment5"].ToString() == "")
                            oMail.Attachment5 = null;
                        else
                            oMail.Attachment5 = (Object)dtMails.Rows[i]["iAttachment5"];
                        //

                        oMail.AttachmentName1 = dtMails.Rows[i]["sAttachmentName1"].ToString();
                        oMail.AttachmentName2 = dtMails.Rows[i]["sAttachmentName2"].ToString();
                        oMail.AttachmentName3 = dtMails.Rows[i]["sAttachmentName3"].ToString();
                        oMail.AttachmentName4 = dtMails.Rows[i]["sAttachmentName4"].ToString();
                        oMail.AttachmentName5 = dtMails.Rows[i]["sAttachmentName5"].ToString();

                        oMail.ClinicID = Convert.ToInt64(dtMails.Rows[i]["nClinicID"]);

                        oMails.Add(oMail);
                        oMail = null;
                    }
                    return oMails;

                }

                return null;

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR :Retrive Sent Failed " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtMails.Dispose();
                oTaskMail.Dispose();

            }

        }

        public Mails getDrafts(Int64 UserId)
        { 
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMails = new DataTable();
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTaskMail.Mails oMails = new Mails();
            gloTaskMail.Mail oMail;
            String strQuery = "";

            try
            {
                oDB.Connect(false);
                if (UserId <= 0)
                {
                    return null;
                }

                // strQuery = "select * from TM_Mail_Drafts where nOwnerID= " + UserId; //Remove select *
                strQuery = "select  nDraftMailID, nOwnerID, sToList, sCcList, sBCcList, sSubject, iBody, nPriorityID, nCategoryID, iAttachment1, iAttachment2, iAttachment3, iAttachment4, " +
                           " iAttachment5, sAttachmentName1, sAttachmentName2, sAttachmentName3, sAttachmentName4, sAttachmentName5, nClinicID from TM_Mail_Drafts where nOwnerID= " + UserId;

                oDB.Retrive_Query(strQuery, out dtMails);

                if (dtMails != null && dtMails.Rows.Count > 0)
                { 
                    //TM_Mail_Drafts --> nDraftMailID,nOwnerID,sToList,sCcList,sBCcList,sSubject,iBody,nPriorityID,nCategoryID,
                    //iAttachment1,iAttachment2,iAttachment3,iAttachment4,iAttachment5,sAttachmentName1,sAttachmentName2
                    //sAttachmentName3,sAttachmentName4,sAttachmentName5,nClinicID

                    for (int i = 0; i < dtMails.Rows.Count ; i++)
                    {
                        oMail = new Mail();

                        oMail.MailID = Convert.ToInt64(dtMails.Rows[i]["nDraftMailID"]);

                        oMail.OwnerID = Convert.ToInt64(dtMails.Rows[i]["nOwnerID"]);
                                               
                        gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
                        oMail.OwnerName = oTask.GetUserName(oMail.OwnerID);
                        oTask.Dispose();

                        oMail.To = GetCSVInList(Convert.ToString(dtMails.Rows[i]["sToList"]));
                        oMail.Cc = GetCSVInList(Convert.ToString(dtMails.Rows[i]["sCcList"]));
                        oMail.BCc = GetCSVInList(Convert.ToString(dtMails.Rows[i]["sBCcList"]));


                        oMail.Subject = dtMails.Rows[i]["sSubject"].ToString();

                        //oMail.MailLinkID = Convert.ToInt64(dtMails.Rows[i]["nMailLinkID"]);

                        oMail.PriorityID = Convert.ToInt64(dtMails.Rows[i]["nPriorityID"]);
                        gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                        oPriority = oTaskMail.GetPriority(oMail.PriorityID);
                        oMail.Priority = oPriority.Description;
                        oPriority.Dispose();

                        oMail.CategoryID = Convert.ToInt64(dtMails.Rows[i]["nCategoryID"]);
                        gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                        oCategory = oTaskMail.GetCategory(oMail.CategoryID);
                        if (oCategory != null)
                        {
                            oMail.Category = oCategory.Description;
                        }
                        oCategory.Dispose();
                        oCategory = null;


                        //Attachment1
                        if (dtMails.Rows[i]["iAttachment1"].ToString() == "")
                            oMail.Attachment1 = null;
                        else
                            oMail.Attachment1 = (Object)dtMails.Rows[i]["iAttachment1"];

                        //Attachment2
                        if (dtMails.Rows[i]["iAttachment2"].ToString() == "")
                            oMail.Attachment2 = null;
                        else
                            oMail.Attachment2 = (Object)dtMails.Rows[i]["iAttachment2"];

                        //Attachment3
                        if (dtMails.Rows[i]["iAttachment3"].ToString() == "")
                            oMail.Attachment3 = null;
                        else
                            oMail.Attachment3 = (Object)dtMails.Rows[i]["iAttachment3"];

                        //Attachment4
                        if (dtMails.Rows[i]["iAttachment4"].ToString() == "")
                            oMail.Attachment4 = null;
                        else
                            oMail.Attachment4 = (Object)dtMails.Rows[i]["iAttachment4"];

                        //Attachment5
                        if (dtMails.Rows[i]["iAttachment5"].ToString() == "")
                            oMail.Attachment5 = null;
                        else
                            oMail.Attachment5 = (Object)dtMails.Rows[i]["iAttachment5"];
                        //

                        oMail.AttachmentName1 = dtMails.Rows[i]["sAttachmentName1"].ToString();
                        oMail.AttachmentName2 = dtMails.Rows[i]["sAttachmentName2"].ToString();
                        oMail.AttachmentName3 = dtMails.Rows[i]["sAttachmentName3"].ToString();
                        oMail.AttachmentName4 = dtMails.Rows[i]["sAttachmentName4"].ToString();
                        oMail.AttachmentName5 = dtMails.Rows[i]["sAttachmentName5"].ToString();

                        oMail.ClinicID = Convert.ToInt64(dtMails.Rows[i]["nClinicID"]);

                        oMails.Add(oMail);
                        oMail = null;

                    }//end - for (int i = 0; i < dtMails.Rows.Count ; i++)

                    return oMails;

                }// end - if (dtMails != null && dtMails.Rows.Count > 0)

                return null;

            } // end - try
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log("Mails - GetDrafts " + dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public Mail getDraft(Int64 DraftId)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMails = new DataTable();
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTaskMail.Mail oMail;
            String strQuery = "";

            try
            {
                oDB.Connect(false);
                if (DraftId <= 0)
                {
                    return null;
                }

               // strQuery = "select * from TM_Mail_Drafts where nDraftMailID= " + DraftId; //Remove select *
                  strQuery = "select  nDraftMailID, nOwnerID, sToList, sCcList, sBCcList, sSubject, iBody, nPriorityID, nCategoryID, iAttachment1, iAttachment2, iAttachment3, iAttachment4, " +
                     " iAttachment5, sAttachmentName1, sAttachmentName2, sAttachmentName3, sAttachmentName4, sAttachmentName5, nClinicID " +
                     " from TM_Mail_Drafts where nDraftMailID= " + DraftId;

                oDB.Retrive_Query(strQuery, out dtMails);

                if (dtMails != null && dtMails.Rows.Count > 0)
                {
                    //TM_Mail_Drafts --> nDraftMailID,nOwnerID,sToList,sCcList,sBCcList,sSubject,iBody,nPriorityID,nCategoryID,
                    //iAttachment1,iAttachment2,iAttachment3,iAttachment4,iAttachment5,sAttachmentName1,sAttachmentName2
                    //sAttachmentName3,sAttachmentName4,sAttachmentName5,nClinicID


                    oMail = new Mail();

                    oMail.MailID = Convert.ToInt64(dtMails.Rows[0]["nDraftMailID"]);

                    oMail.OwnerID = Convert.ToInt64(dtMails.Rows[0]["nOwnerID"]);

                    gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
                    oMail.OwnerName = oTask.GetUserName(oMail.OwnerID);
                    oTask.Dispose();

                    oMail.To = GetCSVInList(Convert.ToString(dtMails.Rows[0]["sToList"]));
                    oMail.Cc = GetCSVInList(Convert.ToString(dtMails.Rows[0]["sCcList"]));
                    oMail.BCc = GetCSVInList(Convert.ToString(dtMails.Rows[0]["sBCcList"]));

                    oMail.Subject = dtMails.Rows[0]["sSubject"].ToString();

                    if (dtMails.Rows[0]["iBody"] != null)
                    {
                        oMail.Body = (Object)dtMails.Rows[0]["iBody"];
                    }
                    
                    //oMail.MailLinkID = Convert.ToInt64(dtMails.Rows[0]["nMailLinkID"]);

                    oMail.PriorityID = Convert.ToInt64(dtMails.Rows[0]["nPriorityID"]);
                    gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                    oPriority = oTaskMail.GetPriority(oMail.PriorityID);
                    oMail.Priority = oPriority.Description;
                    oPriority.Dispose();

                    oMail.CategoryID = Convert.ToInt64(dtMails.Rows[0]["nCategoryID"]);
                    gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                    oCategory = oTaskMail.GetCategory(oMail.CategoryID);
                    if (oCategory != null)
                    {
                        oMail.Category = oCategory.Description;
                    }
                    oCategory.Dispose();
                    oCategory = null;

                    //Attachment1
                    if (dtMails.Rows[0]["iAttachment1"].ToString() == "")
                        oMail.Attachment1 = null;
                    else
                        oMail.Attachment1 = (Object)dtMails.Rows[0]["iAttachment1"];

                    //Attachment2
                    if (dtMails.Rows[0]["iAttachment2"].ToString() == "")
                        oMail.Attachment2 = null;
                    else
                        oMail.Attachment2 = (Object)dtMails.Rows[0]["iAttachment2"];

                    //Attachment3
                    if (dtMails.Rows[0]["iAttachment3"].ToString() == "")
                        oMail.Attachment3 = null;
                    else
                        oMail.Attachment3 = (Object)dtMails.Rows[0]["iAttachment3"];

                    //Attachment4
                    if (dtMails.Rows[0]["iAttachment4"].ToString() == "")
                        oMail.Attachment4 = null;
                    else
                        oMail.Attachment4 = (Object)dtMails.Rows[0]["iAttachment4"];

                    //Attachment5
                    if (dtMails.Rows[0]["iAttachment5"].ToString() == "")
                        oMail.Attachment5 = null;
                    else
                        oMail.Attachment5 = (Object)dtMails.Rows[0]["iAttachment5"];
                    //

                    oMail.AttachmentName1 = dtMails.Rows[0]["sAttachmentName1"].ToString();
                    oMail.AttachmentName2 = dtMails.Rows[0]["sAttachmentName2"].ToString();
                    oMail.AttachmentName3 = dtMails.Rows[0]["sAttachmentName3"].ToString();
                    oMail.AttachmentName4 = dtMails.Rows[0]["sAttachmentName4"].ToString();
                    oMail.AttachmentName5 = dtMails.Rows[0]["sAttachmentName5"].ToString();

                    oMail.ClinicID = Convert.ToInt64(dtMails.Rows[0]["nClinicID"]);

                    return oMail;

                }// end - if (dtMails != null && dtMails.Rows.Count > 0)

                return null;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log("Mail - getDraft :" + dbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :Retrive Sent Failed " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                dtMails.Dispose();
                oTaskMail.Dispose();
                strQuery = null;
                oDB.Disconnect();
                oDB.Dispose();
            }
        }
        
        public Mail getInboxMail(Int64 MailId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMail = new DataTable();
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTaskMail.Mail oMail;
            String strQuery = "";

            try
            {
                oDB.Connect(false);
                if (MailId <= 0)
                {
                    return null;
                }

                // strQuery = "select *  from TM_Mail_Inbox where nMailID =" + MailId;
                   strQuery = "select nMailID, sSubject, iBody, nOwnerID, nMailLinkID, nPriorityID, nCategoryID, iAttachment1, iAttachment2, iAttachment3, iAttachment4, iAttachment5, " +
                              " sAttachmentName1, sAttachmentName2, sAttachmentName3, sAttachmentName4, sAttachmentName5, bIsRead, nClinicID " +
                              " from TM_Mail_Inbox where nMailID =" + MailId;

                oDB.Retrive_Query(strQuery, out dtMail);

                if (dtMail != null && dtMail.Rows.Count > 0)
                {

                    //dbo.TM_Mail_Inbox:-nMailID,sSubject,iBody,nOwnerID,nMailLinkID,nPriorityID,nCategoryID
                    //iAttachment1,iAttachment2,iAttachment3,iAttachment4,iAttachment5,
                    //sAttachmentName1,sAttachmentName2,sAttachmentName3,sAttachmentName4,sAttachmentName5


                    oMail = new Mail();
                    oMail.MailID = Convert.ToInt64(dtMail.Rows[0]["nMailID"]);

                    // Get the List of Users to whom this mail has been sent.
                    oMail.To = getToListOfMail(oMail.MailID);
                    oMail.Cc = getCcListOfMail(oMail.MailID);
                    oMail.BCc = getBCcListOfMail(oMail.MailID);
                    //

                    oMail.Subject = dtMail.Rows[0]["sSubject"].ToString();

                    oMail.Body = (Object)dtMail.Rows[0]["iBody"];

                    oMail.OwnerID = Convert.ToInt64(dtMail.Rows[0]["nOwnerID"]);
                    gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
                    oMail.OwnerName = oTask.GetUserName(oMail.OwnerID);
                    oTask.Dispose();


                    oMail.MailLinkID = Convert.ToInt64(dtMail.Rows[0]["nMailLinkID"]);

                    oMail.PriorityID = Convert.ToInt64(dtMail.Rows[0]["nPriorityID"]);
                    gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                    oPriority = oTaskMail.GetPriority(oMail.PriorityID);
                    oMail.Priority = oPriority.Description;
                    oPriority.Dispose();

                    oMail.CategoryID = Convert.ToInt64(dtMail.Rows[0]["nCategoryID"]);
                    gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                    oCategory = oTaskMail.GetCategory(oMail.CategoryID);
                    if (oCategory != null)
                    {
                        oMail.Category = oCategory.Description;
                    }
                    oCategory.Dispose();
                    oCategory = null;

                    //Check for Attachments if exists set ,else set null

                    //Attachment1
                    if (dtMail.Rows[0]["iAttachment1"].ToString() == "")
                         oMail.Attachment1 = null;                        
                    else
                        oMail.Attachment1 = (Object)dtMail.Rows[0]["iAttachment1"];

                    //Attachment2
                    if (dtMail.Rows[0]["iAttachment2"].ToString() == "")
                        oMail.Attachment2 = null;
                    else
                        oMail.Attachment2 = (Object)dtMail.Rows[0]["iAttachment2"];

                    //Attachment3
                    if (dtMail.Rows[0]["iAttachment3"].ToString() == "")
                        oMail.Attachment3 = null;
                    else 
                        oMail.Attachment3 = (Object)dtMail.Rows[0]["iAttachment3"];

                    //Attachment4
                    if (dtMail.Rows[0]["iAttachment4"].ToString() == "")
                        oMail.Attachment4 = null;
                    else 
                        oMail.Attachment4 = (Object)dtMail.Rows[0]["iAttachment4"];

                    //Attachment5
                    if (dtMail.Rows[0]["iAttachment5"].ToString() == "")
                        oMail.Attachment5 = null;
                    else 
                        oMail.Attachment5 = (Object)dtMail.Rows[0]["iAttachment5"];

                    oMail.AttachmentName1 = dtMail.Rows[0]["sAttachmentName1"].ToString();
                    oMail.AttachmentName2 = dtMail.Rows[0]["sAttachmentName2"].ToString();
                    oMail.AttachmentName3 = dtMail.Rows[0]["sAttachmentName3"].ToString();
                    oMail.AttachmentName4 = dtMail.Rows[0]["sAttachmentName4"].ToString();
                    oMail.AttachmentName5 = dtMail.Rows[0]["sAttachmentName5"].ToString();

                    oMail.IsRead = Convert.ToBoolean(dtMail.Rows[0]["bIsRead"]);
                    oMail.ClinicID = Convert.ToInt64(dtMail.Rows[0]["nClinicID"]);

                    return oMail;

                }

                return null;

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR :Retrive Inbox failed " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtMail.Dispose();
                oTaskMail.Dispose();

            }
        }

        public Mail getSentMail(Int64 MailId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtMail = new DataTable();
            gloTasksMails.gloTaskMail oTaskMail = new gloTasksMails.gloTaskMail(_databaseconnectionstring);
            gloTaskMail.Mail oMail;
            String strQuery = "";

            try
            {
                oDB.Connect(false);
                if (MailId <= 0)
                {
                    return null;
                }

                // strQuery = "select * from TM_Mail_SentItem where nMailID=" + MailId; //Remove select *
                   strQuery = "select  nMailID, sSubject, iBody, nOwnerID, nPriorityID, nCategoryID, iAttachment1, iAttachment2, iAttachment3, iAttachment4, iAttachment5, " +
                              " sAttachmentName1, sAttachmentName2, sAttachmentName3, sAttachmentName4, sAttachmentName5, nClinicID from TM_Mail_SentItem where nMailID=" + MailId;

                oDB.Retrive_Query(strQuery, out dtMail);

                if (dtMail != null && dtMail.Rows.Count > 0)
                {

                    //TM_Mail_SentItem -> nMailID,sSubject,iBody,nOwnerID,nPriorityID,nCategoryID,iAttachment1,iAttachment2
                    //iAttachment3,iAttachment4,iAttachment5,sAttachmentName1,sAttachmentName2,sAttachmentName3
                    //sAttachmentName4,sAttachmentName5

                    oMail = new Mail();
                    oMail.MailID = Convert.ToInt64(dtMail.Rows[0]["nMailID"]);

                    // Get the List of Users to whom this mail has been sent.
                    oMail.To = getToListOfMail(oMail.MailID);
                    oMail.Cc = getCcListOfMail(oMail.MailID);
                    oMail.BCc = getBCcListOfMail(oMail.MailID);
                    //

                    oMail.Subject = dtMail.Rows[0]["sSubject"].ToString();

                    oMail.Body = (Object)dtMail.Rows[0]["iBody"];

                    oMail.OwnerID = Convert.ToInt64(dtMail.Rows[0]["nOwnerID"]);
                    gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
                    oMail.OwnerName = oTask.GetUserName(oMail.OwnerID);
                    oTask.Dispose();


                    //oMail.MailLinkID = Convert.ToInt64(dtMail.Rows[0]["nMailLinkID"]);

                    oMail.PriorityID = Convert.ToInt64(dtMail.Rows[0]["nPriorityID"]);
                    gloTasksMails.Common.Priority oPriority = new gloTasksMails.Common.Priority();
                    oPriority = oTaskMail.GetPriority(oMail.PriorityID);
                    oMail.Priority = oPriority.Description;
                    oPriority.Dispose();

                    oMail.CategoryID = Convert.ToInt64(dtMail.Rows[0]["nCategoryID"]);
                    gloTasksMails.Common.Category oCategory = new gloTasksMails.Common.Category();
                    oCategory = oTaskMail.GetCategory(oMail.CategoryID);
                    oMail.Category = oCategory.Description;
                    oCategory.Dispose();
                    oCategory = null;

                    //oMail.Attachment1 = (Object)dtMail.Rows[0]["iAttachment1"];
                    //oMail.Attachment2 = (Object)dtMail.Rows[0]["iAttachment2"];
                    //oMail.Attachment3 = (Object)dtMail.Rows[0]["iAttachment3"];
                    //oMail.Attachment4 = (Object)dtMail.Rows[0]["iAttachment4"];
                    //oMail.Attachment5 = (Object)dtMail.Rows[0]["iAttachment5"];

                    //Check for Attachments if exists set ,else set null

                    //Attachment1
                    if (dtMail.Rows[0]["iAttachment1"].ToString() == "")
                        oMail.Attachment1 = null;
                    else
                        oMail.Attachment1 = (Object)dtMail.Rows[0]["iAttachment1"];

                    //Attachment2
                    if (dtMail.Rows[0]["iAttachment2"].ToString() == "")
                        oMail.Attachment2 = null;
                    else
                        oMail.Attachment2 = (Object)dtMail.Rows[0]["iAttachment2"];

                    //Attachment3
                    if (dtMail.Rows[0]["iAttachment3"].ToString() == "")
                        oMail.Attachment3 = null;
                    else
                        oMail.Attachment3 = (Object)dtMail.Rows[0]["iAttachment3"];

                    //Attachment4
                    if (dtMail.Rows[0]["iAttachment4"].ToString() == "")
                        oMail.Attachment4 = null;
                    else
                        oMail.Attachment4 = (Object)dtMail.Rows[0]["iAttachment4"];

                    //Attachment5
                    if (dtMail.Rows[0]["iAttachment5"].ToString() == "")
                        oMail.Attachment5 = null;
                    else
                        oMail.Attachment5 = (Object)dtMail.Rows[0]["iAttachment5"];
                    //

                    oMail.AttachmentName1 = dtMail.Rows[0]["sAttachmentName1"].ToString();
                    oMail.AttachmentName2 = dtMail.Rows[0]["sAttachmentName2"].ToString();
                    oMail.AttachmentName3 = dtMail.Rows[0]["sAttachmentName3"].ToString();
                    oMail.AttachmentName4 = dtMail.Rows[0]["sAttachmentName4"].ToString();
                    oMail.AttachmentName5 = dtMail.Rows[0]["sAttachmentName5"].ToString();

                    oMail.ClinicID = Convert.ToInt64(dtMail.Rows[0]["nClinicID"]);

                    return oMail;

                }

                return null;

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR :Retrive Sent Failed " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtMail.Dispose();
                oTaskMail.Dispose();

            }

        }

        public Int64 getInboxCount(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Object _result = new object();
            try
            {
                oDB.Connect(false);

                //Get the Count of Unread Messages
                //  strQuery = "select count(*) from dbo.TM_Mail_Inbox where bIsRead = '" + false + "' AND nOwnerID = " + UserId; //Remove select *
                strQuery = "select count(nMailID) from dbo.TM_Mail_Inbox where bIsRead = '" + false + "' AND nOwnerID = " + UserId;
               
                _result = oDB.ExecuteScalar_Query(strQuery);

                return Convert.ToInt64(_result);


            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), _messageBoxCaption);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _result = null;
            }

        }

        public Int64 getDraftCount(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Object _result = new object();
            try
            {
                oDB.Connect(false);
                //strQuery = "select count(*) from TM_Mail_Drafts where nOwnerID = " + UserId + " "; //Remove select *
                strQuery = "select count(nDraftMailID) from TM_Mail_Drafts where nOwnerID = " + UserId + " ";

                _result = oDB.ExecuteScalar_Query(strQuery);
                return Convert.ToInt64(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                _result = null;
            }
        }

        public gloGeneralItem.gloItems getToListOfMail(Int64 MailId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtToList = null;
            gloGeneralItem.gloItems ToList = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem ToItem;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //strQuery = "select nMailerID from TM_Mail_Transaction where nMailID="+MailId+" and nMailFlag="+Convert.ToInt16(gloTasksMails.Common.MailFlag.To) +"";
                
                //strQuery = "select nMailerID from dbo.TM_Mail_Inbox,dbo.TM_Mail_Transaction where TM_Mail_Inbox.nMailID = " + MailId + " "
                //         + "AND (TM_Mail_Transaction.nMailID = TM_Mail_Inbox.nMailLinkID AND TM_Mail_Transaction.nMailFlag =" + Convert.ToInt64(gloTasksMails.Common.MailFlag.To) + ") ";

                strQuery = "select nMailerID from TM_Mail_Inbox,TM_Mail_Transaction where TM_Mail_Inbox.nMailID = "+MailId+" AND  "
                          +"(TM_Mail_Transaction.nMailID = TM_Mail_Inbox.nMailLinkID AND  "
                          +"((TM_Mail_Transaction.nMailFlag =" +Convert.ToInt64(gloTasksMails.Common.MailFlag.From)+") OR  "
                          +"TM_Mail_Transaction.nMailFlag = " + Convert.ToInt64(gloTasksMails.Common.MailFlag.To) + ")) AND  "
                          +"nMailerID <> " + this.UserID + " ";


                oDB.Retrive_Query(strQuery, out dtToList);
                if (dtToList != null && dtToList.Rows.Count > 0)
                {
                    for (int i = 0; i < dtToList.Rows.Count; i++)
                    {
                        ToItem = new gloGeneralItem.gloItem();
                        ToItem.ID = Convert.ToInt64(dtToList.Rows[i]["nMailerID"]);
                        gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
                        ToItem.Code = oTask.GetUserEmail(ToItem.ID);
                        ToItem.Description = oTask.GetUserName(ToItem.ID);

                        ToList.Add(ToItem);
                        ToItem.Dispose();
                        ToItem = null;
                        oTask.Dispose();
                        oTask = null;
                    }
                    if (dtToList != null)
                    {
                        dtToList.Dispose();
                        dtToList = null;
                    }
                    return ToList;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR  :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }                
            }
        }

        public gloGeneralItem.gloItems getCcListOfMail(Int64 MailId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtCcList = null;
            gloGeneralItem.gloItems CcList = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem CcItem;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //strQuery = "select nMailerID from TM_Mail_Transaction where nMailID=" + MailId + " and nMailFlag=" + Convert.ToInt16(gloTasksMails.Common.MailFlag.Cc) + "";
                strQuery = "select nMailerID from dbo.TM_Mail_Inbox,dbo.TM_Mail_Transaction where TM_Mail_Inbox.nMailID = " + MailId + " "
                         + "AND (TM_Mail_Transaction.nMailID = TM_Mail_Inbox.nMailLinkID AND TM_Mail_Transaction.nMailFlag =" + Convert.ToInt64(gloTasksMails.Common.MailFlag.Cc) + ") ";
                oDB.Retrive_Query(strQuery, out dtCcList);
                if (dtCcList != null && dtCcList.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCcList.Rows.Count; i++)
                    {
                        CcItem = new gloGeneralItem.gloItem();
                        CcItem.ID = Convert.ToInt64(dtCcList.Rows[i]["nMailerID"]);
                        gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
                        CcItem.Code = oTask.GetUserEmail(CcItem.ID);
                        CcItem.Description = oTask.GetUserName(CcItem.ID);

                        CcList.Add(CcItem);
                        CcItem.Dispose();
                        CcItem = null;
                        oTask.Dispose();
                        oTask = null;
                    }
                    if (dtCcList != null)
                    {
                        dtCcList.Dispose();
                        dtCcList = null;
                    }
                    return CcList;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR  :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public gloGeneralItem.gloItems getBCcListOfMail(Int64 MailId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtBCcList = null;
            gloGeneralItem.gloItems BCcList = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem BCcItem;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                // strQuery = "select nMailerID from TM_Mail_Transaction where nMailID=" + MailId + " and nMailFlag=" + Convert.ToInt16(gloTasksMails.Common.MailFlag.BCc) + "";

                strQuery = "select nMailerID from dbo.TM_Mail_Inbox,dbo.TM_Mail_Transaction where TM_Mail_Inbox.nMailID = " + MailId + " "
                         + "AND (TM_Mail_Transaction.nMailID = TM_Mail_Inbox.nMailLinkID AND TM_Mail_Transaction.nMailFlag =" + Convert.ToInt64(gloTasksMails.Common.MailFlag.BCc) + ") ";

                oDB.Retrive_Query(strQuery, out dtBCcList);
                if (dtBCcList != null && dtBCcList.Rows.Count > 0)
                {
                    for (int i = 0; i < dtBCcList.Rows.Count; i++)
                    {
                        BCcItem = new gloGeneralItem.gloItem();
                        BCcItem.ID = Convert.ToInt64(dtBCcList.Rows[i]["nMailerID"]);
                        gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);
                        BCcItem.Code = oTask.GetUserEmail(BCcItem.ID);
                        BCcItem.Description = oTask.GetUserName(BCcItem.ID);

                        BCcList.Add(BCcItem);
                        BCcItem.Dispose();
                        BCcItem = null;
                    }
                    if (dtBCcList != null)
                    {
                        dtBCcList.Dispose();
                        dtBCcList = null;
                    }
                    return BCcList;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR  :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public Int64 SaveDraftMail(Mail oMail)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string strToList = "";
            string strCcList = "";
            string strBCcList = "";
            Object _oResult = null;

            try
            {
                //TM_Mail_Drafts --> nDraftMailID,nOwnerID,sToList,sCcList,sBCcList,sSubject,iBody,nPriorityID,nCategoryID,
                //iAttachment1,iAttachment2,iAttachment3,iAttachment4,iAttachment5,sAttachmentName1,sAttachmentName2
                //sAttachmentName3,sAttachmentName4,sAttachmentName5,nClinicID

                oDB.Connect(false);

                //oParameters.Add("@nDraftMailID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@nDraftMailID", oMail.MailID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@nOwnerID", oMail.OwnerID, ParameterDirection.Input, SqlDbType.BigInt);

                strToList = GetListInCSV(oMail.To);
                oParameters.Add("@sToList", strToList, ParameterDirection.Input, SqlDbType.VarChar, 100);

                if (oMail.Cc != null)
                {
                    strCcList = GetListInCSV(oMail.Cc);
                    oParameters.Add("@sCcList", strCcList, ParameterDirection.Input, SqlDbType.VarChar, 100);
                }
                else
                {
                    oParameters.Add("@sCcList", "", ParameterDirection.Input, SqlDbType.VarChar, 100);
                }

                if (oMail.BCc != null)
                {
                    strBCcList = GetListInCSV(oMail.BCc);
                    oParameters.Add("@sBCcList", strBCcList, ParameterDirection.Input, SqlDbType.VarChar, 100);
                }
                else
                {
                    oParameters.Add("@sBCcList", "", ParameterDirection.Input, SqlDbType.VarChar, 100);
                }

                oParameters.Add("@sSubject", oMail.Subject, ParameterDirection.Input, SqlDbType.VarChar, 255);
                if (oMail.Body == null)
                {
                    oParameters.Add("@iBody",DBNull.Value , ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iBody", oMail.Body, ParameterDirection.Input, SqlDbType.Image);
                }
                oParameters.Add("@nPriorityID", oMail.PriorityID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCategoryID", oMail.CategoryID, ParameterDirection.Input, SqlDbType.BigInt);

                if (oMail.Attachment1 != null)
                {
                    oParameters.Add("@iAttachment1", oMail.Attachment1, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment1", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }
                if (oMail.Attachment2 != null)
                {
                    oParameters.Add("@iAttachment2", oMail.Attachment2, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment2", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }

                if (oMail.Attachment3 != null)
                {
                    oParameters.Add("@iAttachment3", oMail.Attachment3, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment3", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }

                if (oMail.Attachment4 != null)
                {
                    oParameters.Add("@iAttachment4", oMail.Attachment4, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment4", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }

                if (oMail.Attachment5 != null)
                {
                    oParameters.Add("@iAttachment5", oMail.Attachment5, ParameterDirection.Input, SqlDbType.Image);
                }
                else
                {
                    oParameters.Add("@iAttachment5", DBNull.Value, ParameterDirection.Input, SqlDbType.Image);
                }
                oParameters.Add("@sAttachmentName1", oMail.AttachmentName1, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@sAttachmentName2", oMail.AttachmentName2, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@sAttachmentName3", oMail.AttachmentName3, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@sAttachmentName4", oMail.AttachmentName4, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@sAttachmentName5", oMail.AttachmentName5, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@nClinicID", oMail.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);


                oDB.Execute("TM_IN_Draft", oParameters, out _oResult);

                if (Convert.ToInt64(_oResult) > 0)
                {
                    MessageBox.Show("Draft saved successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                return Convert.ToInt64(_oResult);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log("Draft Mail Save :" + dbEx.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                oMail.Dispose();
                _oResult = null;
            }
        }

        public bool DownLoadAttachment(string AttachmentName, Object Attachment)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
        }

        private string GetListInCSV(gloGeneralItem.gloItems oItems)
        {
            try
            {
                string strList = "";

                for (int i = 0; i < oItems.Count; i++)
                {
                    strList += oItems[i].ID.ToString() + ",";
    
                }
                strList = strList.Substring(0, strList.Length - 1);
                return strList;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                oItems.Dispose();
            }
        }

        private gloGeneralItem.gloItems GetCSVInList(string strList)
        {

            gloGeneralItem.gloItems oList = new gloGeneralItem.gloItems();
            gloGeneralItem.gloItem oItem;
            gloTaskMail.gloTask oTask = new gloTask(_databaseconnectionstring);

            try
            {
                String[] ar_strList;
                ar_strList = strList.Split(',');
                                
                for (int i = 0; i < ar_strList.Length ; i++)
                {
                    oItem = new gloGeneralItem.gloItem();
                    oItem.ID = Convert.ToInt64(ar_strList[i]);
                    oItem.Code = oTask.GetUserEmail(oItem.ID);
                    oItem.Description = oTask.GetUserName(oItem.ID);

                    oList.Add(oItem);
                    oItem.Dispose();
                    oItem = null;
                }

                return oList;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                strList = null;
                oTask.Dispose();                
            }
        }

        #endregion " Private & Public Methods "
       
    }//public class gloMail : IDisposable

    public class Mail : IDisposable
    {
        #region " Declarations "

        private Int64   _MailID=0;
        private Int64 _MailLinkID=0;

        private Int64   _FromID=0;
        private string _FromName = "";

        private gloGeneralItem.gloItems _To;
        private gloGeneralItem.gloItems _Cc;
        private gloGeneralItem.gloItems _BCc;

        private string _Subject = "";
        private object _Body;

        private object _Attachment1;
        private object _Attachment2;
        private object _Attachment3;
        private object _Attachment4;
        private object _Attachment5;

        private string _AttachmentName1="";
        private string _AttachmentName2 = "";
        private string _AttachmentName3 = "";
        private string _AttachmentName4 = "";
        private string _AttachmentName5 = "";

        private Int64 _OwnerID=0;
        private string _OwnerName = "";

        private Int64 _PriorityID=0;
        private string _Priority = "";

        private Int64 _CategoryID=0;
        private string _Category = "";

        private Int64 _UserID=0;
        private string _UserName = "";

        private bool _IsRead = false;
        private Int64 _ClinicID = 0;

        private DateTime _MailDate = DateTime.Now;

        #endregion " Declarations "

        #region "Constructor & Distructor"


        public Mail()
        {
            
           
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~Mail()
        {
            Dispose(false);
        }

        #endregion

        #region " Mail Properties "

        public Int64 MailID
        {
            get { return _MailID; }
            set { _MailID = value; }

        }

        public Int64 MailLinkID
        {
            get { return _MailLinkID; }
            set { _MailLinkID = value; }
        }

        public Int64 FromID
        {
            get { return _FromID; }
            set { _FromID = value; }
        }
        public string FromName
        {
            get { return _FromName; }
            set { _FromName = value; }
        }

        public gloGeneralItem.gloItems To
        {
            get { return _To; }
            set { _To = value; }
        }
        public gloGeneralItem.gloItems Cc
        {
            get { return _Cc; }
            set { _Cc = value; }

        }
        public gloGeneralItem.gloItems BCc
        {
            get { return _BCc; }
            set { _BCc = value; }

        }
        
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        public object  Body
        {
            get { return _Body; }
            set { _Body = value; }
        }


        public object Attachment1
        {
            get { return _Attachment1; }
            set { _Attachment1 = value; }
        }
        public object Attachment2
        {
            get { return _Attachment2; }
            set { _Attachment2 = value; }
        }
        public object Attachment3
        {
            get { return _Attachment3; }
            set { _Attachment3 = value; }
        }
        public object Attachment4
        {
            get { return _Attachment4; }
            set { _Attachment4 = value; }
        }
        public object Attachment5
        {
            get { return _Attachment5; }
            set { _Attachment5 = value; }
        }

        public string  AttachmentName1
        {
            get { return _AttachmentName1 ; }
            set { _AttachmentName1 = value; }
        }
        public string AttachmentName2
        {
            get { return _AttachmentName2; }
            set { _AttachmentName2 = value; }
        }
        public string AttachmentName3
        {
            get { return _AttachmentName3; }
            set { _AttachmentName3 = value; }
        }
        public string AttachmentName4
        {
            get { return _AttachmentName4; }
            set { _AttachmentName4 = value; }
        }
        public string AttachmentName5
        {
            get { return _AttachmentName5; }
            set { _AttachmentName5 = value; }
        }

        public Int64 OwnerID
        {
            get { return _OwnerID; }
            set { _OwnerID = value; }
        }
        public string OwnerName
        {
            get { return _OwnerName; }
            set { _OwnerName = value; }
        }
        
        public Int64 PriorityID
        {
            get { return _PriorityID; }
            set { _PriorityID = value; }
        }
        public string Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        public Int64 CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public bool IsRead
        {
            get { return _IsRead; }
            set { _IsRead = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        
        public DateTime MailDate
        {
            get { return _MailDate; }
            set { _MailDate = value; }
        }

        #endregion " Mail Properties "

        
    }

    public class Mails:IDisposable
    {
        protected ArrayList _innerlist;
        

        #region "Constructor & Destructor"

        public Mails()
        {
            _innerlist = new ArrayList();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~Mails()
        {
            Dispose(false);
        }

        #endregion

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(Mail item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(Mail item)
        {
            bool result = false;
            Mail obj;

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
                obj = new Mail();
                obj = (Mail)_innerlist[i];
                if (obj.MailID == item.MailID)
                {
                    _innerlist.RemoveAt(i);
                    result = true;
                    break;
                }
                obj = null;
            }

            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public Mail this[int index]
        {
            get
            {
                return (Mail)_innerlist[index];
            }
        }

        public bool Contains(Mail item)
        {
            return _innerlist.Contains(item);

        }

        public int IndexOf(Mail item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(Mail[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }
}
