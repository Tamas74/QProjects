using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentBook
{
    public partial class frmMstClinicalInstruction : Form
    {
        Int64 nID = 0;
        public frmMstClinicalInstruction(Int64 nId)
        {
            InitializeComponent();
            nID = nId;
            if (nID > 0)
            {
                DisplayClinicalInstruction(nID);
            }

        }

        private void DisplayClinicalInstruction(Int64 nId)
        {
            DataTable _dtMstTable=null;
            try
            {
                using (ClsClinicalInstruction objClinicalInstruction = new ClsClinicalInstruction())
                {
                    _dtMstTable = objClinicalInstruction.GetClinicalInstruction(nId);
                }
                if (_dtMstTable != null)
                {
                    if (_dtMstTable.Rows.Count > 0)
                    {
                        txtInstruction.Text = _dtMstTable.Rows[0]["Instruction"].ToString();
                        txtInstructionDesc.Text = _dtMstTable.Rows[0]["Instruction Description"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_dtMstTable != null) { _dtMstTable.Dispose(); _dtMstTable = null; }
            }
        }


        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveClinicalInstruction())
                {
                    using (ClsClinicalInstruction objClinicalInstruction = new ClsClinicalInstruction())
                    {
                        objClinicalInstruction.SaveClinicalInstruction(nID, txtInstruction.Text.ToString(), txtInstructionDesc.Text.ToString());
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private Boolean SaveClinicalInstruction()
        {
            Boolean _Result = false;
            try
            {               
                if (txtInstruction.Text.Trim() == "")
                {
                    MessageBox.Show("Enter the Instruction. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInstruction.Select();
                    _Result= false;
                }
                else if (txtInstructionDesc.Text.Trim() == "")
                {
                    MessageBox.Show("Enter the Instruction Description. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInstructionDesc.Select();
                    _Result= false;
                }
                else
                    _Result= true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return _Result;
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveClinicalInstruction())
                {
                    using (ClsClinicalInstruction objClinicalInstruction = new ClsClinicalInstruction())
                    {
                        objClinicalInstruction.SaveClinicalInstruction(nID, txtInstruction.Text.ToString(), txtInstructionDesc.Text.ToString());
                    }
                    ResetFormData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ResetFormData()
        {
            nID = 0;
            txtInstruction.Text = "";
            txtInstructionDesc.Text = "";
        }




    }
}
