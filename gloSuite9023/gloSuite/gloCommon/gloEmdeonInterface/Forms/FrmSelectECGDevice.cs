using System;
using System.Drawing;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;

namespace gloEmdeonInterface.Forms
{
    public partial class FrmSelectECGDevice : Form
    {

        private bool bIsCardioScinceDeviceEnabled = false;
        private bool bIsWelchAllynEcgDeviceEnabled = false;
        private bool bIsMidmarkecgDeviceEnabled = false;

        //fonts 
        private Font fonnt9TahomaRegular = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        private Font fonnt9TahomaBoldFace = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);


        public ClsWelchAllynECGLayer.DeviceType GetSelectedDeviceType
        { get; set; }

        public FrmSelectECGDevice(Boolean _bIsCardioScinceDeviceEnabled, Boolean _bIsWelchAllynEcgDeviceEnabled, Boolean _bIsMidmarkecgDeviceEnabled)
        {
            InitializeComponent();

            bIsCardioScinceDeviceEnabled = _bIsCardioScinceDeviceEnabled;
            bIsWelchAllynEcgDeviceEnabled = _bIsWelchAllynEcgDeviceEnabled;
            bIsMidmarkecgDeviceEnabled = _bIsMidmarkecgDeviceEnabled;

        }

        private void FrmSelectECGDevice_Load(object sender, EventArgs e)
        {
            try
            {
                RdoCardiacScienseECGDevice.Visible = bIsCardioScinceDeviceEnabled;
                RdoMidmarkECGDevice.Visible = bIsMidmarkecgDeviceEnabled;
                RdoWelchAllynECGDevice.Visible = bIsWelchAllynEcgDeviceEnabled;

                if (bIsCardioScinceDeviceEnabled && bIsMidmarkecgDeviceEnabled && bIsWelchAllynEcgDeviceEnabled)
                {
                    RdoCardiacScienseECGDevice.Location = new System.Drawing.Point(63, 51);
                    RdoMidmarkECGDevice.Location = new System.Drawing.Point(63, 86);
                    RdoWelchAllynECGDevice.Location = new System.Drawing.Point(63, 120);

                }
                else if (bIsCardioScinceDeviceEnabled && bIsMidmarkecgDeviceEnabled && !bIsWelchAllynEcgDeviceEnabled)
                {
                    RdoCardiacScienseECGDevice.Location = new System.Drawing.Point(63, 51);
                    RdoMidmarkECGDevice.Location = new System.Drawing.Point(63, 86);
                    RdoWelchAllynECGDevice.Location = new System.Drawing.Point(63, 120);

                }
                else if (bIsCardioScinceDeviceEnabled && !bIsMidmarkecgDeviceEnabled && bIsWelchAllynEcgDeviceEnabled)
                {
                    RdoCardiacScienseECGDevice.Location = new System.Drawing.Point(63, 51);
                    RdoMidmarkECGDevice.Location = new System.Drawing.Point(63, 120);
                    RdoWelchAllynECGDevice.Location = new System.Drawing.Point(63, 86);
                }
                else if (!bIsCardioScinceDeviceEnabled && bIsMidmarkecgDeviceEnabled && bIsWelchAllynEcgDeviceEnabled)
                {
                    RdoCardiacScienseECGDevice.Location = new System.Drawing.Point(63, 120);
                    RdoMidmarkECGDevice.Location = new System.Drawing.Point(63, 51);
                    RdoWelchAllynECGDevice.Location = new System.Drawing.Point(63, 86);
                }
            }
            catch (Exception)
            {
            }

        }

        private void ts_ViewButtons_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                 if (tlbHealthCentrix.Name == e.ClickedItem.Name)
                {
                    if (RdoCardiacScienseECGDevice.Checked)
                    {

                        GetSelectedDeviceType = ClsWelchAllynECGLayer.DeviceType.HeartCentrixECGDevice;
                        this.Close();

                    }
                    else if (RdoWelchAllynECGDevice.Checked)
                    {

                        GetSelectedDeviceType = ClsWelchAllynECGLayer.DeviceType.WelchAllynECGDevice;
                        this.Close();

                    }
                    else if (RdoMidmarkECGDevice.Checked)
                    {

                        GetSelectedDeviceType = ClsWelchAllynECGLayer.DeviceType.MidMarkECGDevice;
                        this.Close();

                    }


                }
                else if (e.ClickedItem.Name == ts_btnClose.Name)
                {

                    GetSelectedDeviceType = ClsWelchAllynECGLayer.DeviceType.NoDeviceSelected;
                    this.Close();

                }
            }
            catch (Exception)
            {

            }

        }

        private void RdoCardiacScienseECGDevice_CheckedChanged(object sender, EventArgs e)
        {

            if (RdoCardiacScienseECGDevice.Checked)
            { toggleCaption(); }

        }

        private void RdoWelchAllynECGDevice_CheckedChanged(object sender, EventArgs e)
        {

            if (RdoWelchAllynECGDevice.Checked)
            { toggleCaption(); }

        }

        private void RdoMidmarkECGDevice_CheckedChanged(object sender, EventArgs e)
        {
            if (RdoMidmarkECGDevice.Checked)
            { toggleCaption(); }

        }

        private void toggleCaption()
        {
            try
            {
                if (RdoCardiacScienseECGDevice.Checked)
                {
                    RdoCardiacScienseECGDevice.Font = fonnt9TahomaBoldFace;
                    RdoMidmarkECGDevice.Font = fonnt9TahomaRegular;
                    RdoWelchAllynECGDevice.Font = fonnt9TahomaRegular;
                    tlbHealthCentrix.Text = "Order ECG Test";
                    tlbHealthCentrix.ToolTipText = "Order ECG Test";

                }
                else if (RdoWelchAllynECGDevice.Checked)
                {
                    RdoCardiacScienseECGDevice.Font = fonnt9TahomaRegular;
                    RdoMidmarkECGDevice.Font = fonnt9TahomaRegular;
                    RdoWelchAllynECGDevice.Font = fonnt9TahomaBoldFace;
                    tlbHealthCentrix.Text = "Start ECG Test";
                    tlbHealthCentrix.ToolTipText = "Start ECG Test";
                }
                else if (RdoMidmarkECGDevice.Checked)
                {
                    RdoCardiacScienseECGDevice.Font = fonnt9TahomaRegular;
                    RdoMidmarkECGDevice.Font = fonnt9TahomaBoldFace;
                    RdoWelchAllynECGDevice.Font = fonnt9TahomaRegular;
                    tlbHealthCentrix.Text = "Start ECG Test";
                    tlbHealthCentrix.ToolTipText = "Start ECG Test";
                }
            }
            catch (Exception)
            {
            }


        }


    }
}
