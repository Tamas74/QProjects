using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloSnoMed
{
    public partial class gloSearchTextBox : TextBox
    {
        public event SearchFiredEventHandler SearchFired;
        public delegate void SearchFiredEventHandler();

        System.DateTime _CurrentTime;

        private void oTimer_Tick(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(this.Text.Trim()))
            {
                if (DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100)
                {
                    oTimer.Stop();
                    if (SearchFired != null)
                    {
                        SearchFired();
                    }
                }

            }
            else
            {
                oTimer.Stop();
                if (SearchFired != null)
                {
                    SearchFired();
                }
            }

        }

        private void gloSearchTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //'Added on 20140221-To disable enter while searching in textbox
            if (e.KeyValue  ==(int) Keys.Enter)
            {
                return;
            }
            _CurrentTime = DateTime.Now;
            oTimer.Stop();
            oTimer.Interval = 700;
            oTimer.Enabled = true;
        }

        private void gloSearchTextBox_TextChanged(object sender, System.EventArgs e)
        {
       
            if (oTimer.Enabled == false)
            {
                oTimer.Stop();
                oTimer.Enabled = true;
            }
        }
        public gloSearchTextBox()
        {
            InitializeComponent(); 
            TextChanged += gloSearchTextBox_TextChanged;
            KeyDown += gloSearchTextBox_KeyDown;
        }

       
    }
}
