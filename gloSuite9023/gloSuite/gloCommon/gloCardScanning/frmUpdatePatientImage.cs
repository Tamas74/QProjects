using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace gloCardScanning
{

    public partial class frmUpdatePatientImage : Form
    {
        #region " Variables Declaration "

        public Image _PatientNewfaceImage;
        public Image _OldImage;
        //private string _Gender;
        private string _DatabaseConnectionString = "";
        public Int64 _patientid;
        public bool _UpadateImage = false;

        #endregion

        #region " Property Procedures "

        public Image PatientNewfaceImage
        {
            get { return _PatientNewfaceImage; }
            set { _PatientNewfaceImage = value; }
        }

        public bool UpadateImage
        {
            get { return _UpadateImage; }
            set { _UpadateImage = value; }
        }

        #endregion " Property Procedures "

        public frmUpdatePatientImage(Image faceImage, string dbconnectionstring, Int64 patientID)
        {
            try
            {
                InitializeComponent();

                _PatientNewfaceImage = faceImage;
                _DatabaseConnectionString = dbconnectionstring;
                _patientid = patientID;



            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        #region " TLStrip Menu Events "

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            _UpadateImage = true;

            //if (_PatientNewfaceImage != null)
            //{
            //    _PatientNewfaceImage.Dispose();
            //    _PatientNewfaceImage = null;
            //}
            if (pbNewFaceImage != null)
            {
                //04-03-2011 : Commperss
                if (pbNewFaceImage.Image != null)
                {
                    pbNewFaceImage.Dispose();
                    pbNewFaceImage.Image = null;
                }
                pbNewFaceImage.Dispose();
                pbNewFaceImage = null;
            }
            if (pbOldImage != null)
            {

                if (pbOldImage.Image != null)
                {
                    pbOldImage.Dispose();
                    pbOldImage.Image = null;
                }
                pbOldImage.Dispose();
                pbOldImage = null;
            }
            this.Close();
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            try
            {


                if (_PatientNewfaceImage != null)
                {
                    _PatientNewfaceImage.Dispose();
                    _PatientNewfaceImage = null;
                }
                if (pbNewFaceImage != null)
                {
                    //04-03-2011 : Commperss
                    if (pbNewFaceImage.Image != null)
                    {
                        pbNewFaceImage.Dispose();
                        pbNewFaceImage.Image = null;
                    }
                    pbNewFaceImage.Dispose();
                    pbNewFaceImage = null;
                }
                if (pbOldImage != null)
                {
                    //04-03-2011 : Commperss
                    if (pbOldImage.Image != null)
                    {
                        pbOldImage.Dispose();
                        pbOldImage.Image = null;
                    }
                    pbOldImage.Dispose();
                    pbOldImage = null;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

        #region " Form Load "

        private void frmUpdatePatientImage_Load(object sender, EventArgs e)
        {
            gloCardScanning ogloCardScanning = new gloCardScanning(_DatabaseConnectionString);

            try
            {
                if (_OldImage != null)//04-03-2011 : Commperss
                {
                    _OldImage.Dispose();
                    _OldImage = null;
                }
                _OldImage = ogloCardScanning.GetOldPhoto(_patientid);

                pbNewFaceImage.Image = _PatientNewfaceImage;

                if (_OldImage != null)
                {
                    pbOldImage.Image = _OldImage;
                }
                else
                {
                    SetGenderPhoto(); // set default blank photo for patient 
                }
               



            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //04-03-2011 : Commperss
                if (ogloCardScanning != null)
                {
                    ogloCardScanning.Dispose();
                    ogloCardScanning = null;
                }
            }

        }
        #endregion

        #region" Methods "

        //public getOldImage()
        //{
        //    try
        //    {
        //         if ((dt.Rows[0]["iPhoto"] != DBNull.Value))
        //                {
        //                    System.Drawing.Image ilogo;
        //                    Byte[] arrPicture = (Byte[])dt.Rows[0]["iPhoto"];
        //                    System.IO.MemoryStream ms = new System.IO.MemoryStream(arrPicture);
        //                    ilogo = Image.FromStream(ms);
        //                    oPatient.DemographicsDetail.PatientPhoto = ilogo;
        //                }

        //    }
        //    catch (Exception ex ){}
        //    finally {}
        //}

        public void SetGenderPhoto()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            DataTable dt = null;
            //string _strSQL = "";//04-03-2011 : Commperss
            //Int64 UpdateID = default(Int64);
            //string UpdateLocation = "";
            try
            {
                if (oDB.Connect(false))
                {
                    String strQuery = "SELECT sGender FROM Patient where nPatientID= " + _patientid + "";
                    oDB.Retrive_Query(strQuery, out dt);

                    if ((dt != null))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if ((dt.Rows[0]["sGender"] != DBNull.Value))
                            {
                                switch (dt.Rows[0]["sGender"].ToString())
                                {
                                    case "Female":
                                        pbOldImage.Image = global::gloCardScanning.Properties.Resources.FemalePatient;

                                        break;
                                    case "Male":
                                        pbOldImage.Image = global::gloCardScanning.Properties.Resources.MalePatient;
                                        break;
                                    //case "Other":
                                    //    pbOldImage.BackgroundImage = "";
                                    //    break;
                                }
                            }

                        }
                    }
                    strQuery = null;
                }//if//04-03-2011 : Commperss
                //oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }//04-03-2011 : Commperss
                if (dt != null) { dt.Dispose(); dt = null; }//04-03-2011 : Commperss

            }            
        }

        #endregion



    }
}
