using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloCommon;

namespace gloOffice
{
    public partial class frmAddDropDown : Form
    {
        #region Variable Declaration

        private ArrayList m_arrList;
        private string m_Title;
        public int width;

        #endregion

        #region Properties

        public ArrayList GetDropdownItems
        {
            get { return m_arrList; }
            set { m_arrList = value; }
        }
        public string GetDropdownTitle
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        #endregion

        #region Constructor

        public frmAddDropDown()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Load

        private void frmAddDropDown_Load(object sender, EventArgs e)
        {
            //'Add default item
                if (m_arrList == null)
                {
                    lstItems.Items.Add("Choose an item");
                }
                else
                {
                    //'If modifying th exisitng load the drop doen items in the list
                    FillDdlItems();
                }
                //'enable the diabled buttons
                EnableBtns();
                //'If more than one items available select the first item by default
                if ((lstItems.Items.Count > 0))
                {
                    lstItems.SelectedIndex = 0;
                }
                //' Bind the title to text box if exists
                if (!string.IsNullOrEmpty(m_Title))
                {
                    txtTitle.Text = m_Title;
                }
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                // This method actually sets the order all the way down the control hierarchy.
                tom.SetTabOrder(scheme);
        }

        #endregion


        #region Form Control Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string strValue = txtItem.Text.Trim();
            //'check for empty text
            if (!string.IsNullOrEmpty(strValue))
            {
                //' check duplicate items
                if (!IsDuplicate(strValue))
                {
                    strValue = txtItem.Text.Trim();
                    //'Add item to the list
                    lstItems.Items.Add(strValue);
                    //'clear the textbox and set focus
                    txtItem.Text = string.Empty;
                    txtItem.Focus();

                }
                else
                {
                    MessageBox.Show("An entry with the same name already exists - each entry must specify a unique entry.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Blank entry cannot be added.", "gloPM",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if ((lstItems.SelectedItem != null))
            {
                //'Get the selected item from the list
                txtItem.Text = lstItems.SelectedItem.ToString();
                lstItems.Items.Remove(lstItems.SelectedItem);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if ((lstItems.SelectedItem != null))
                {
                    //'Remove the selected item from the list
                    lstItems.Items.Remove(lstItems.SelectedItem);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Remove, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                //'check If item is selected
                if ((lstItems.SelectedItem != null))
                {
                    if (lstItems.SelectedIndex != lstItems.Items.Count - 1)
                    {
                        //'if the selected iten is not the last item
                        Int16 intindex = default(Int16);
                        intindex =Convert.ToInt16(lstItems.SelectedIndex);
                        string str = null;
                        str = lstItems.SelectedItem.ToString();
                        //'get the selected item, store it and remove from the list
                        lstItems.Items.RemoveAt(lstItems.SelectedIndex);
                        //'insert it in one position down and select it again
                        lstItems.Items.Insert(intindex + 1, str);
                        lstItems.SelectedItem = lstItems.Items[intindex + 1];
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if ((lstItems.SelectedItem != null))
                {
                    if (lstItems.SelectedIndex != 0)
                    {
                        //'if the selected iten is not the first item
                        Int16 intindex = default(Int16);
                        intindex = Convert.ToInt16(lstItems.SelectedIndex);
                        string str = null;
                        str = lstItems.SelectedItem.ToString();
                        //'get the selected item, store it and remove from the list
                        lstItems.Items.RemoveAt(lstItems.SelectedIndex);
                        //'insert it in one position up and select it again
                        lstItems.Items.Insert(intindex - 1, str);
                        lstItems.SelectedItem = lstItems.Items[intindex - 1];

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.Message);
            }

        }

        private void btnAdd_MouseHover(object sender, EventArgs e)
        {

            btnAdd.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongYellow;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongButton;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnModify_MouseHover(object sender, EventArgs e)
        {
            btnModify.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongYellow;
            btnModify.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btnModify_MouseLeave(object sender, EventArgs e)
        {
            btnModify.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongButton;
            btnModify.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemove_MouseHover(object sender, EventArgs e)
        {
            btnRemove.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongYellow;
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnRemove_MouseLeave(object sender, EventArgs e)
        {
            btnRemove.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongButton;
            btnRemove.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btnUp_MouseHover(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongYellow;
            btnUp.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnUp_MouseLeave(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongButton;
            btnUp.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongYellow;
            btnDown.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloOffice.Properties.Resources.Img_LongButton;
            btnDown.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void txtItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            //'Check for enter key

            {
                if (e.KeyChar == 13)
                {
                    System.EventArgs erg = null;
                    if (txtItem.Text.Length > 0)
                    {
                        //'If txt is not empty then add the value
                        btnAdd_Click(sender, erg);
                    }
                    else
                    {
                        //'If txt is empty then add the entire data into db
                        AddDropDown();
                    }
                }
                else
                {
                    //'If not enter key and txt is not empty, then enable the add button
                    if (txtItem.Text.Length > 0)
                    {
                        btnAdd.Enabled = true;
                    }
                }
            }

        }

        private void lstItems_MouseDown(object sender, MouseEventArgs e)
        {
            EnableBtns();
        }

        private void tlsDropdown_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "Ok":
                    AddDropDown();
                    break;
                case "Cancel":
                    this.Close();
                    break;
            }

        }

        #endregion

        #region Form Methods


        /// To enable the buttons for user
        private void EnableBtns()
        {
            btnRemove.Enabled = true;
            btnUp.Enabled = true;
            btnDown.Enabled = true;
            btnModify.Enabled = true;
        }

       
        ///To disable the buttons for validation
        private void DisableBtns()
        {
            btnRemove.Enabled = false;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
            btnModify.Enabled = false;
        }

        private bool IsDuplicate(string strName)
        {

            for (Int32 i = 0; i <= lstItems.Items.Count - 1; i++)
            {
                //'Check whether the same item is avilable in the list 
                if (lstItems.Items[i].ToString() == strName)
                {
                    //' same value exists
                    return true;
                }
            }
            return false;
        }

       /// To fill the field values in the list box
       
        private void FillDdlItems()
        {
            //'Clear the list box and then add the field value to the list
            lstItems.Items.Clear();
            for (Int32 _inc = 0; _inc <= m_arrList.Count - 1; _inc++)
            {
                lstItems.Items.Add(m_arrList[_inc]);
            }
        }

        private void AddDropDown()
        {
            //'Clear  the arraylist before adding
            if (m_arrList == null)
            {
                m_arrList = new ArrayList();
            }
            else
            {
                m_arrList.Clear();
            }
            //'Add the list items into arraylist for further purpose
            for (Int32 i = 0; i <= lstItems.Items.Count - 1; i++)
            {
                m_arrList.Add(lstItems.Items[i].ToString());
            }
            m_Title = txtTitle.Text.Trim();
            this.Close();
        }

        #endregion

        private void lstItems_MouseMove(object sender, MouseEventArgs e)
        {
            //try
            //{

            //    ListBox lstBox;
            //    lstBox = (ListBox)sender;

            //    if (lstItems.SelectedItem != null)
            //    {
            //        if (getWidthofListItems(Convert.ToString(lstItems.Items[lstItems.SelectedIndex]), lstItems) >= lstItems.Width - 10)
            //        {
            //            //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
            //            string temp = Convert.ToString(lstItems.Items[lstItems.SelectedIndex]);
            //            toolTip1.SetToolTip(lstItems, Convert.ToString(lstItems.Items[lstItems.SelectedIndex]));
            //        }
            //        else
            //        {
            //            toolTip1.SetToolTip(lstItems, "");
            //            toolTip1.Hide(lstItems);
            //        }
            //    }
            //    else
            //    {
            //        toolTip1.SetToolTip(lstItems, "");
            //        toolTip1.Hide(lstItems);
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            //    Ex = null;
            //}  


            try
            {
                ListBox objListBox = (ListBox)sender;
                int itemIndex = -1;
                if (lstItems.Items.Count>0)
                {
                    if (objListBox.ItemHeight != 0)
                    {
                        itemIndex = e.Y / objListBox.ItemHeight;
                        itemIndex += objListBox.TopIndex;
                    }
                    if ((itemIndex >= 0) && (itemIndex  <= lstItems.Items.Count - 1))
                    {
                        if (itemIndex <= lstItems.Items.Count - 1)
                        {
                            if (getWidthofListItems(Convert.ToString(lstItems.Items[itemIndex]), lstItems) >= lstItems.Width)
                            {
                                toolTip1.SetToolTip(objListBox, lstItems.Items[itemIndex].ToString());
                            }
                            else
                            {
                                toolTip1.Hide(objListBox);
                            }
                        }
                        else
                        {
                            toolTip1.Hide(objListBox);
                        }
                    }
                    else
                    {
                        toolTip1.Hide(objListBox);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private int getWidthofListItems(string _text, ListBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
   
    }
}
