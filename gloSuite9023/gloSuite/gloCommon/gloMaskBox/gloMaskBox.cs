using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace gloMaskControl
{
    public enum gloMaskType
    {
        Other = 0,
        SSN = 1,
        Phone = 3,
        Mobile = 4,
        //Sandip Darade 20090907
        //Added Fax
        Fax = 5,
        Date = 6,
        Pager=7
    }

    public partial class gloMaskBox : UserControl
    {
        #region " Constructor "
        public gloMaskBox()
        {
            InitializeComponent();
            txtMaskBox.Mask = "(000)000-0000";
            #region " Retrieve MessageBoxCaption from AppSettings "
            //Sandip Darade 2009090428
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }
        #endregion

        #region " Private Variables "
        private gloMaskType _MaskType = gloMaskType.Phone;
        private String _MessageBoxCaption = String.Empty;
        private bool _bIncludeLiteralsAndPrompts = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion

        #region " Public Properties "
        public gloMaskType MaskType
        {
            get { return _MaskType; }
            set
            {
                _MaskType = value;
                switch (_MaskType)
                {
                    case gloMaskType.SSN:
                        txtMaskBox.Mask = "000-00-0000";
                        //MessageBoxCaption = "SSN";
                        break;
                    //case gloMaskType.Pager:
                    //    txtMaskBox.Mask = "000-00-0000";
                    //    //MessageBoxCaption = "SSN";
                    //    break;
                    case gloMaskType.Phone:
                        txtMaskBox.Mask = "(000)000-0000";
                        //MessageBoxCaption = "Phone";
                        break;
                    case gloMaskType.Other:
                        txtMaskBox.Mask = "";
                        //MessageBoxCaption = "Other";
                        break;
                    case gloMaskType.Mobile:
                        txtMaskBox.Mask = "(000)000-0000";
                        //MessageBoxCaption = "Mobile";
                        break;
                    case gloMaskType.Date:
                        txtMaskBox.Mask = "00/00/0000";
                        break;
                    case gloMaskType.Pager:
                        txtMaskBox.Mask = "(000)000-0000";
                        //MessageBoxCaption = "Mobile";
                        break;
                }
            }
        }
        //public String MessageBoxCaption
        //{
        //    get { return _MessageBoxCaption; }
        //    set { _MessageBoxCaption = value; }
        //}
       
        public override String Text
        {
            get
            {
                if (_bIncludeLiteralsAndPrompts == false)
                {
                    txtMaskBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    return txtMaskBox.Text;
                }
                else
                {
                    txtMaskBox.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    return txtMaskBox.Text;
                }
            }
            set { txtMaskBox.Text = value; }
        }

    
        public bool IncludeLiteralsAndPrompts
        {
            get { return _bIncludeLiteralsAndPrompts; }
            set { _bIncludeLiteralsAndPrompts = value; }
        }
        public bool MaskFull
        {
            get { return txtMaskBox.MaskFull; }
        }
        public bool ReadOnly
        {
            get
            {
                return txtMaskBox.ReadOnly;

            }
            set { txtMaskBox.ReadOnly = value; }
        }
        public bool IsValidated
        {
            get
            {
                if (txtMaskBox.Text.Length != 0 && txtMaskBox.MaskFull == false)
                {
                    if (_AllowValidate == true)
                    {
                        if (ErrorMessageInvoked != null)
                        {
                            ErrorMessageInvoked(null, null);
                        }
                        MessageBox.Show(_MaskType + " details are incomplete.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMaskBox.Focus();
                    }
                    return false;
                }
                else if (txtMaskBox.Text.Length != 0 && _MaskType == gloMaskType.Date)
                {                    
                    txtMaskBox.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    try
                    { 
                        DateTime _dt = Convert.ToDateTime(txtMaskBox.Text);
                        txtMaskBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        return true; 
                    }
                    catch
                    {
                        txtMaskBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        MessageBox.Show("Enter valid date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtMaskBox.Focus();
                        return false;
                    }                    
                }
                else
                {
                    return true;
                }
            }
        }
        //Add new property  for manual validation 
        bool _AllowValidate = true;
        public bool AllowValidate
        {
            get
            {
                return _AllowValidate;
            }
            set
            {
                _AllowValidate = value;
            }
        }
        #endregion

        public delegate void ErrorMessage(object sender, EventArgs e);
        public event ErrorMessage ErrorMessageInvoked;

        #region " Public Methods "
        public void Clear()
        {
            txtMaskBox.Clear();
        }
        #endregion

        private void txtMaskBox_Leave(object sender, EventArgs e)
        {
            if (IsValidated == true) { }
        }

        private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            else
            {
                ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                
                int index = ((MaskedTextBox)sender).GetCharIndexFromPosition(new Point(e.X, e.Y))-1;

                if (index + 2 == ((MaskedTextBox)sender).TextLength)
                {
                    ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    string str = ((MaskedTextBox)sender).Text.Replace("_", " ").Trim();
                    ((MaskedTextBox)sender).SelectionStart = str.TrimEnd(new Char[] { '-', ')', '(', ' ' }).Length;
                    str = null;
                }
                else
                {
                    if (index >= 0)
                    {
                        string strt = ((MaskedTextBox)sender).Text.Substring(index, 1).TrimEnd(new Char[] { '_', '-', ')', '(', ' ' });


                        if (((MaskedTextBox)sender).GetCharFromPosition(new Point(e.X, e.Y)).ToString().TrimEnd(new Char[] { '_', '-', ')', '(', ' ' }) == string.Empty && strt == string.Empty)
                        //if (strt == string.Empty)
                        {
                            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            string str = ((MaskedTextBox)sender).Text.Replace("_", " ").Trim();
                            ((MaskedTextBox)sender).SelectionStart = str.TrimEnd(new Char[] { '-', ')', '(', ' ' }).Length;
                            str = null;
                        }
                        strt = null;
                    }
                }

                ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            }
        }

        private void txtMaskBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // GLO2011-0013690 : Select and Type Text in Modify Patient
            // Commented the Key press code, as it is not seems to be functioning as expected.

            ////Sandip Darade 20091230
            ////bug ID 4409
            //if (MaskType == gloMaskType.Phone || MaskType == gloMaskType.Mobile)
            //{


            //    txtMaskBox.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace("_", "");

            //    if (((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //    {
            //        if (txtMaskBox.Text.Trim().Length > 3 && txtMaskBox.Text.Trim().Length <= 6)
            //        {
            //            txtMaskBox.SelectionStart = txtMaskBox.Text.Trim().Length + 2;

            //        }
            //        else if (txtMaskBox.Text.Trim().Length > 6)
            //        {
            //            txtMaskBox.SelectionStart = txtMaskBox.Text.Trim().Length + 3;
            //        }
            //        else
            //        {
            //            txtMaskBox.SelectionStart = txtMaskBox.Text.Trim().Length + 1;

            //        }
            //    }
            //}
        }

        //public delegate void TextChanged(object sender, EventArgs e);
        //public event TextChanged TextChange;
        //private void txtMaskBox_TextChanged(object sender, EventArgs e)
        //{
        //    TextChange(sender, e);
        //}

        public void removeLeaveEvent()
        {
            this.txtMaskBox.Leave -= txtMaskBox_Leave;
        }

        public void addLeaveEvent()
        {
            this.txtMaskBox.Leave -= txtMaskBox_Leave;
        }
    }
}