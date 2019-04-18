using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloPatientPortal.UserControls;

namespace gloPatientPortal
{
    public partial class frmHealthForm : Form
    {
        #region Variable declaration
        string _strConnectionString = string.Empty;
        string _strDMSconnectionstring = string.Empty;
        long _nLoginID;
        UcHealthFormGrp objUcHealthFormGrp = null;
        UcHealthFrmQueAns objUcHealthFrmQueAns = null;
        UcHealthFormBinding objUcHealthFrmFormBinding = null;
        UcHealthFormAddEdit ObjUcHealthFormAddEdit = null;
        // Unique Id of Patient form 
        long _nPFListId = 0;


        #endregion

        #region Constractor
        public frmHealthForm()
        {
            InitializeComponent();
        }

        public frmHealthForm(string strConnectionString, long nLoginID,string strDMSconnectionstring)
        {
            InitializeComponent();
            _strConnectionString = strConnectionString;
            _nLoginID = nLoginID;
            _strDMSconnectionstring = strDMSconnectionstring;
        }
        #endregion

        #region Events
        private void frmHealthForm_Load(object sender, EventArgs e)
        {
            //ts_Groups_Click(null, null);
            ts_BuildForm_Click(null, null);
        }

        public void SetModifyEnabled(bool enableValue)
        {
            ts_Modify.Enabled = enableValue;
        }

        private void ts_Groups_Click(object sender, EventArgs e)
        {
            if (ValidateToolStrip())
            {
               
                try
                {
                    if (objUcHealthFormGrp == null)
                    {
                        if (objUcHealthFrmQueAns != null)
                        {
                            pnlMain.Controls.Remove(objUcHealthFrmQueAns);
                            objUcHealthFrmQueAns.Dispose();
                            objUcHealthFrmQueAns = null;
                        }
                        if (objUcHealthFrmFormBinding != null)
                        {
                            pnlMain.Controls.Remove(objUcHealthFrmFormBinding);
                            objUcHealthFrmFormBinding.Dispose();
                            objUcHealthFrmFormBinding = null;
                        }
                        if (ObjUcHealthFormAddEdit != null)
                        {
                            pnlMain.Controls.Remove(ObjUcHealthFormAddEdit);
                            ObjUcHealthFormAddEdit.Dispose();
                            ObjUcHealthFormAddEdit = null;
                        }
                        this.Text = "Online Patient Form - Groups";
                        objUcHealthFormGrp = new UcHealthFormGrp(_strConnectionString, _nLoginID);
                        objUcHealthFormGrp.BringToFront();
                        objUcHealthFormGrp.Dock = DockStyle.Fill;
                        pnlMain.Controls.Add(objUcHealthFormGrp);
                        objUcHealthFormGrp.IsNewModify = false;
                        ts_Save.Visible = false;
                        ts_SaveandPreview.Visible = false;
                        ts_New.Visible = true;
                        ts_Modify.Visible = true;
                        ts_Publish.Visible = false;
                        ts_close.Visible = false;

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void ts_Questions_Click(object sender, EventArgs e)
        {
            if (ValidateToolStrip())
            {
                try
                {
                    if (objUcHealthFrmQueAns == null)
                    {
                        if (objUcHealthFormGrp != null)
                        {
                            pnlMain.Controls.Remove(objUcHealthFormGrp);
                            objUcHealthFormGrp.Dispose();
                            objUcHealthFormGrp = null;
                        }
                        if (objUcHealthFrmFormBinding != null)
                        {
                            pnlMain.Controls.Remove(objUcHealthFrmFormBinding);
                            objUcHealthFrmFormBinding.Dispose();
                            objUcHealthFrmFormBinding = null;
                        }
                        if (ObjUcHealthFormAddEdit != null)
                        {
                            pnlMain.Controls.Remove(ObjUcHealthFormAddEdit);
                            ObjUcHealthFormAddEdit.Dispose();
                            ObjUcHealthFormAddEdit = null;
                        }
                        this.Text = "Online Patient Form - Questions";
                        objUcHealthFrmQueAns = new UcHealthFrmQueAns(_strConnectionString, _nLoginID);
                        objUcHealthFrmQueAns.BringToFront();
                        objUcHealthFrmQueAns.Dock = DockStyle.Fill;
                        //objUcHealthFrmQueAns.pnlAns.Visible = false;
                        pnlMain.Controls.Add(objUcHealthFrmQueAns);
                        ts_Save.Visible = false;
                        ts_SaveandPreview.Visible = false;
                        ts_New.Visible = true;
                        ts_Modify.Visible = true;
                        ts_Publish.Visible = false;
                        ts_close.Visible = false;

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private bool ValidateToolStrip()
        {
            DialogResult result;
            if (objUcHealthFormGrp != null)
            {
                if (objUcHealthFormGrp.IsNewModify)
                {
                    result=MessageBox.Show("Any change(s) done will be lost.\nDo you want to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DialogResult.Yes == result)
                    {
                        objUcHealthFormGrp.pnlGroup.Dock = DockStyle.None;
                        objUcHealthFormGrp.c1Group.Enabled = true;
                        objUcHealthFormGrp.IsNewModify = false;
                        objUcHealthFormGrp.ClearAll();
                        return true;
                    }
                    else
                        return false;
                }
            }
            //if (objUcHealthFrmFormBinding != null)
            //{
            //    //if (objUcHealthFrmFormBinding.IsNewModify)
            //    // {
            //    result = MessageBox.Show("Any change(s) done will be lost.\nDo you want to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //    if (DialogResult.Yes == result)
            //        return true;
            //    else
            //        return false;
            //    // }
            //}
            if (ObjUcHealthFormAddEdit != null)
            {
                if (ObjUcHealthFormAddEdit.IsNewModify)
                {
                    result = MessageBox.Show("Any change(s) done will be lost.\nDo you want to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DialogResult.Yes == result)
                        return true;
                    else
                        return false;
                }
            }
            if (objUcHealthFrmQueAns != null)
            {
                if (objUcHealthFrmQueAns.IsNewModify)
                {
                    result = MessageBox.Show("Any change(s) done will be lost.\nDo you want to continue?", "gloEMRAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DialogResult.Yes == result)
                    {
                        objUcHealthFrmQueAns.pnlGroup.Dock = DockStyle.None;
                        objUcHealthFrmQueAns.c1Question.Enabled = true;
                        objUcHealthFrmQueAns.IsNewModify = false;
                        objUcHealthFrmQueAns.ClearAll();
                        return true;
                    }
                    else
                        return false;
                }
            }
            return true;
        }

        private void ts_Answer_Click(object sender, EventArgs e)
        {

        }

        private void ts_BuildForm_Click(object sender, EventArgs e)
        {
            if (ValidateToolStrip())
            {
                try
                {
                    if (objUcHealthFrmFormBinding == null)
                    {
                        if (objUcHealthFormGrp != null)
                        {
                            pnlMain.Controls.Remove(objUcHealthFormGrp);
                            objUcHealthFormGrp.Dispose();
                            objUcHealthFormGrp = null;                           
                        }

                        if (objUcHealthFrmQueAns != null)
                        {
                            pnlMain.Controls.Remove(objUcHealthFrmQueAns);
                            objUcHealthFrmQueAns.Dispose();
                            objUcHealthFrmQueAns = null;
                        }
                        if (ObjUcHealthFormAddEdit != null)
                        {
                            pnlMain.Controls.Remove(ObjUcHealthFormAddEdit);
                            ObjUcHealthFormAddEdit.Dispose();
                            ObjUcHealthFormAddEdit = null;
                        }
                        this.Text = "Online Patient Forms";

                        objUcHealthFrmFormBinding = new UcHealthFormBinding(_strConnectionString, _nLoginID,_strDMSconnectionstring);
                        objUcHealthFrmFormBinding.BringToFront();
                        objUcHealthFrmFormBinding.Dock = DockStyle.Fill;
                        pnlMain.Controls.Add(objUcHealthFrmFormBinding);
                        
                    }
                    else
                    {
                        objUcHealthFrmFormBinding.Show();
                        objUcHealthFrmFormBinding.panel1.Show();
                        objUcHealthFrmFormBinding.pnlSearch.Show();
                        objUcHealthFrmFormBinding.panel3.Show();
                        objUcHealthFrmFormBinding.FillHealthForms();

                    }

                    ts_Save.Visible = false;
                    ts_SaveandPreview.Visible = false;
                    ts_New.Visible = true;
                    ts_Modify.Visible = true;
                    ts_Publish.Visible = false;
                    ts_close.Visible = false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHealthForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (objUcHealthFormGrp != null)
            {
                pnlMain.Controls.Remove(objUcHealthFormGrp);
                objUcHealthFormGrp.Dispose();
                objUcHealthFormGrp = null;
            }
            if (objUcHealthFrmQueAns != null)
            {
                pnlMain.Controls.Remove(objUcHealthFrmQueAns);
                objUcHealthFrmQueAns.Dispose();
                objUcHealthFrmQueAns = null;
            }
            if (objUcHealthFrmFormBinding != null)
            {
                pnlMain.Controls.Remove(objUcHealthFrmFormBinding);
                objUcHealthFrmFormBinding.Dispose();
                objUcHealthFrmFormBinding = null;
            }
            if (ObjUcHealthFormAddEdit != null)
            {
                pnlMain.Controls.Remove(ObjUcHealthFormAddEdit);
                ObjUcHealthFormAddEdit.Dispose();
                ObjUcHealthFormAddEdit = null;
            }
        }

        private void ts_New_Click(object sender, EventArgs e)
        {
            if (objUcHealthFormGrp != null)
            {
                objUcHealthFormGrp.AddNew();
                objUcHealthFormGrp.IsNewModify = true;
            }
            else if (objUcHealthFrmQueAns != null)
            {
                objUcHealthFrmQueAns.AddNew();
                objUcHealthFrmQueAns.IsNewModify = true;
            }
            else if (objUcHealthFrmFormBinding != null)
            {
                pnlMain.Controls.Remove(objUcHealthFrmFormBinding);
                objUcHealthFrmFormBinding.Dispose();
                objUcHealthFrmFormBinding = null;
                try
                {
                    if (ObjUcHealthFormAddEdit == null)
                    {
                        ObjUcHealthFormAddEdit = new UcHealthFormAddEdit(_strConnectionString, _nLoginID,_strDMSconnectionstring);
                        ObjUcHealthFormAddEdit.BringToFront();
                        ObjUcHealthFormAddEdit.Dock = DockStyle.Fill;
                        pnlMain.Controls.Add(ObjUcHealthFormAddEdit);
                        ObjUcHealthFormAddEdit.FormAddEdit(0, false);
                        ObjUcHealthFormAddEdit.Show();
                        ObjUcHealthFormAddEdit.IsNewModify = true;

                    }
                    else
                    {
                        ObjUcHealthFormAddEdit.ResetControls();
                        ObjUcHealthFormAddEdit.Show();
                        ObjUcHealthFormAddEdit.IsNewModify = true; ;
                    }
                    ts_Save.Visible = true;
                    ts_SaveandPreview.Visible = false;
                    ts_New.Visible = false;
                    ts_Modify.Visible = false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void ts_Modify_Click(object sender, EventArgs e)
        {
            if (objUcHealthFormGrp != null)
            {
                if (objUcHealthFormGrp.ShowGroup() > 0)
                { 
                objUcHealthFormGrp.ts_Save.Text = "&Save";
                objUcHealthFormGrp.c1Group.Enabled = false;
                objUcHealthFormGrp.IsNewModify = true;
                }
                else
                {
                    MessageBox.Show("No record exists.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (objUcHealthFrmQueAns != null)
            {
                if (objUcHealthFrmQueAns.BindQuestion() > 0)
                {
                    objUcHealthFrmQueAns.DesignAssociatedAnswersGrid();
                    objUcHealthFrmQueAns.BindAssociatedAnswers();
                    objUcHealthFrmQueAns.ts_Save.Text = "&Save";
                    objUcHealthFrmQueAns.c1Question.Enabled = false;
                    objUcHealthFrmQueAns.IsNewModify = true;
                }
                else
                {
                    MessageBox.Show("No record exists.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                
            }
            else if (objUcHealthFrmFormBinding != null)
            {
                _nPFListId = objUcHealthFrmFormBinding.GetHealthFormId();
                if (_nPFListId != 0)
                {

               
                objUcHealthFrmFormBinding.Dispose();
                objUcHealthFrmFormBinding = null;
                try
                {
                    if (ObjUcHealthFormAddEdit == null)
                    {

                        ObjUcHealthFormAddEdit = new UcHealthFormAddEdit(_strConnectionString, _nLoginID,_strDMSconnectionstring);
                        ObjUcHealthFormAddEdit.BringToFront();
                        ObjUcHealthFormAddEdit.Dock = DockStyle.Fill;
                        pnlMain.Controls.Add(ObjUcHealthFormAddEdit);
                        ObjUcHealthFormAddEdit.FormAddEdit(_nPFListId, true);
                        ObjUcHealthFormAddEdit.IsNewModify = true;
                    }
                    else
                    {
                        ObjUcHealthFormAddEdit.FormAddEdit(_nPFListId, true);
                        ObjUcHealthFormAddEdit.Show();
                        ObjUcHealthFormAddEdit.IsNewModify = true;
                    }
                    ts_Save.Visible = true;
                    ts_SaveandPreview.Visible = true;
                    ts_New.Visible = false;
                    ts_Modify .Visible = false ;
                    ts_close.Visible = true;

                }
                catch (Exception)
                {
                    throw;
                }

                }
                else
                {
                    MessageBox.Show("No record exists.", "gloEMRAdmin", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        #endregion

        private void ts_Save_Click(object sender, EventArgs e)
        {
            if (ObjUcHealthFormAddEdit !=null)
                if (ObjUcHealthFormAddEdit.Save())
                {
                    ts_SaveandPreview.Visible = true;
                    ts_close.Visible = true;
                }
            if (objUcHealthFrmFormBinding != null)
                if (objUcHealthFrmFormBinding.SaveHealthform())
                {
                    ts_SaveandPreview.Visible = true;
                    ts_close.Visible = true;
                }
        }

        private void ts_Publish_Click(object sender, EventArgs e)
        {
            if (ObjUcHealthFormAddEdit != null)
                if (!ObjUcHealthFormAddEdit.Publish())
                    return;
            if (objUcHealthFrmFormBinding != null)
                if (!objUcHealthFrmFormBinding.PublishHealthform())
                    return;
            ts_Save.Visible = true;
            ts_SaveandPreview.Visible = true;
            ts_Publish.Visible = false;
        }

        private void ts_close_Click(object sender, EventArgs e)
        {
            if (ValidateToolStrip())
            {
                try
                {
                    if (objUcHealthFrmFormBinding == null)
                    {

                        if (objUcHealthFormGrp != null)
                        {
                            pnlMain.Controls.Remove(objUcHealthFormGrp);
                            objUcHealthFormGrp.Dispose();
                            objUcHealthFormGrp = null;
                        }

                        if (objUcHealthFrmQueAns != null)
                        {
                            pnlMain.Controls.Remove(objUcHealthFrmQueAns);
                            objUcHealthFrmQueAns.Dispose();
                            objUcHealthFrmQueAns = null;
                        }
                        if (ObjUcHealthFormAddEdit != null)
                        {
                            pnlMain.Controls.Remove(ObjUcHealthFormAddEdit);
                            ObjUcHealthFormAddEdit.Dispose();
                            ObjUcHealthFormAddEdit = null;
                        }
                        this.Text = "Online Patient Forms";
                        objUcHealthFrmFormBinding = new UcHealthFormBinding(_strConnectionString, _nLoginID, _strDMSconnectionstring);
                        objUcHealthFrmFormBinding.BringToFront();
                        objUcHealthFrmFormBinding.Dock = DockStyle.Fill;
                        pnlMain.Controls.Add(objUcHealthFrmFormBinding);
                        ts_Save.Visible = false;
                        ts_SaveandPreview.Visible = false;
                        ts_New.Visible = true;
                        ts_Modify.Visible = true;
                        ts_close.Visible = false;
                        ts_Publish.Visible = false;
                    }
                    else
                    {
                        objUcHealthFrmFormBinding.Show();
                        objUcHealthFrmFormBinding.panel1.Show();
                        objUcHealthFrmFormBinding.pnlSearch.Show();
                        objUcHealthFrmFormBinding.panel3.Show();
                        objUcHealthFrmFormBinding.FillHealthForms();
                        ts_Save.Visible = false;
                        ts_SaveandPreview.Visible = false;
                        ts_New.Visible = true;
                        ts_Modify.Visible = true;
                        ts_close.Visible = false;
                        ts_Publish.Visible = false;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            //ts_Save.Visible = false;
            //ts_SaveandPreview.Visible = false;
            //ts_Publish.Visible = false;
            //ts_close.Visible = false;
            //ts_New.Visible = true;
            //ts_Modify.Visible = true;
        }

        private void ts_SaveandPreview_Click(object sender, EventArgs e)
        {
            if (ObjUcHealthFormAddEdit != null)

                if (!ObjUcHealthFormAddEdit.SaveandPreview())
                {
                    return;
                }
            if (objUcHealthFrmFormBinding != null)
                if (!objUcHealthFrmFormBinding.SaveandPreviewHealthform())
                    return;
            ts_Save.Visible = false;
            ts_SaveandPreview.Visible = false;
            ts_Publish.Visible = true;
            ts_close.Visible = true;

        }

        public void setpfmodifybtns()
        {
            ts_Save.Visible = true;
            ts_SaveandPreview.Visible = true;
            ts_New.Visible = false;
            ts_Modify.Visible = false;
            ts_close.Visible = true;
        }


    }
}
