using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
namespace gloEDocumentV3
{
    partial class TextBox : Form
    {
        public TextBox()
        {
            InitializeComponent();
            myStringBld = new StringBuilder();
                
        }
        public TextBox(StringBuilder parmString)
        {
            InitializeComponent();
            myStringBld = new StringBuilder();
            myStringBld = parmString;
            Txt_AnnotText.Text = myStringBld.ToString();

        }
        StringBuilder myStringBld = null;

        public StringBuilder MyStringBld
        {
            get { return myStringBld; }
            set { myStringBld = value; }
        }
        Color _cColor;

        public Color CColor
        {
            get { return _cColor; }
            set { _cColor = value; }
        }
        Font _fFont = null;

        public Font FFont
        {
            get { return _fFont; }
            set { _fFont = value; }
        }

        private void frmaddText_Load(object sender, EventArgs e)
        {
            this.Select();
            this.BringToFront();
        }

        private void frmaddText_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MyStringBld.Append(textBox1.Text);
        }

        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            if (Txt_AnnotText.Text == "")
            {
                MessageBox.Show("Please enter the text.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MyStringBld.Remove(0, myStringBld.Length);
            MyStringBld.Append (Txt_AnnotText.Text);
            if (FFont==null)
            {
                CColor = Txt_AnnotText.ForeColor;
                FFont = Txt_AnnotText.Font;
            }
            this.Close();
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Font get_Txt_AnnotText_Font()
        {
            return Txt_AnnotText.Font;
        }

        private void tlb_FontSelect_Click(object sender, EventArgs e)
        {
            myFontDialog objmyFontDialog = new myFontDialog(ref Txt_AnnotText);            
            CColor = Txt_AnnotText.ForeColor;
            FFont = Txt_AnnotText.Font;
            objmyFontDialog = null;
        }

        
     
    }
}


public class myFontDialog
{
    FontDialog dlgFont = null;
  
    public myFontDialog(ref TextBox oTextBox)
    {
        dlgFont = new FontDialog();
        dlgFont.ShowColor = true;
        dlgFont.ShowApply = true;
        if (dlgFont.ShowDialog(oTextBox.Parent) == DialogResult.OK)
        {
            oTextBox.Font = dlgFont.Font;
            oTextBox.ForeColor = dlgFont.Color;
           
        }
        dlgFont.Dispose();
        dlgFont = null;

    }
   
 
}
