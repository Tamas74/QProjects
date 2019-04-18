using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using gloBilling.Properties;

namespace gloBilling
{
    class CFormFlickering : IDisposable
    {

        Form form;
        Panel oPanel = null;

        int FreezeCount = 0;

        public CFormFlickering(Form form, bool freezeIt)
        {
            this.form = form;
            if (freezeIt) this.Freeze();
        }


        public void Freeze()
        {
            if (++FreezeCount > 1)
                return;
            Rectangle rect = form.ClientRectangle;

            oPanel = new Panel();
            oPanel.BackColor = Color.FromArgb(207, 224, 248);
            oPanel.Width = rect.Width;
            oPanel.Height = rect.Height;


            //PictureBox oPictureBox = new PictureBox();
            //oPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            //Image img = new Bitmap("C:\\Tulips.JPG");

            //oPictureBox.Image = img;
            //oPictureBox.Width = rect.Width;
            //oPictureBox.Height = rect.Height;

            //oPanel.Controls.Add(oPictureBox);
            //oPictureBox.BringToFront();

            //Label oLable = new Label();
            //oLable.Text = "Please Wait While Loading......";
            //oLable.Font = new Font("Tahoma", 9, FontStyle.Bold);
            //oLable.TextAlign=ContentAlignment.MiddleCenter;
            //oLable.Width = rect.Width;
            //oLable.Height = rect.Height;
            //oLable.Dock = DockStyle.Fill;
            //oPanel.Controls.Add(oLable);

            form.Controls.Add(oPanel);
            oPanel.BringToFront();
        }


        public void Unfreeze(bool force)
        {

            if (FreezeCount == 0)
                return;

            FreezeCount -= 1;

            if (force)
                FreezeCount = 0;

            if (FreezeCount > 0)
                return;

            form.Controls.Remove(oPanel);

            oPanel.Dispose();

            oPanel = null;

        }

        public bool IsFrozen
        {
            get { return (FreezeCount > 0); }
        }

        void IDisposable.Dispose()
        {
            this.Unfreeze(true);
        }

    }

}
