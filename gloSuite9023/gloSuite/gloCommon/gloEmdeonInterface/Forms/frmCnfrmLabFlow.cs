using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloEmdeonInterface.Forms
{
    public partial class frmCnfrmLabFlow : Form
    {

       
        private string _dataBaseConnectionString = string.Empty;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string lab_flow_confirm = string.Empty;
        public String LabFlowConfirm
        {
            get
            { return lab_flow_confirm; }

            set
            { lab_flow_confirm = value; }

        }
       
        public frmCnfrmLabFlow()
        {
            //added by madan on 20100622
            if (appSettings != null)
            {
                _dataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);                
            }

            InitializeComponent();
        }
        

        private void tlbbtn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbLabOrder.Checked == true)
                {
                    LabFlowConfirm = rbLabOrder.Tag.ToString();
                }
                else if (rbRecordResults.Checked == true)
                {
                    LabFlowConfirm = rbRecordResults.Tag.ToString();
                }
                else if (rbTask.Checked == true)
                {
                    LabFlowConfirm = rbTask.Tag.ToString();
                }

                if (chk_LabStatus.Checked ==true)
                {
                    if (lab_flow_confirm !="" && lab_flow_confirm.Length >0)
                    {
                        UpdateProviderUsage(lab_flow_confirm);
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private bool UpdateProviderUsage(string _ProviderUsage)
        {
            bool _blnResult = false;
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            gloDatabaseLayer.DBParameters objDbParameters = new gloDatabaseLayer.DBParameters();
            int _result = 0;           
            
                try
                {
                    objDbLayer.Connect(false);
                    objDbParameters.Clear();
                    objDbParameters.Add("@SettingsName", "GLOLAB PROVIDER USAGE", ParameterDirection.Input, SqlDbType.VarChar);
                    objDbParameters.Add("@SettingsValue", _ProviderUsage, ParameterDirection.Input, SqlDbType.VarChar);
                    objDbParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.BigInt);
                    objDbParameters.Add("@nUserID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    objDbParameters.Add("@nUserClinicFlag", 1, ParameterDirection.Input, SqlDbType.Int);

                    _result = objDbLayer.Execute("gsp_UpdateSettings", objDbParameters);
                    
                    if (_result < 0)
                        _blnResult = false;
                    else if (_result >= 0)
                        _blnResult = true;
                    else
                        _blnResult = false;

                    objDbLayer.Disconnect();
                    //objDbParameters.Dispose();

                }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnResult = false;
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                if (objDbParameters != null)
                {
                    objDbParameters.Dispose();
                }
                _result = 0;
            }

            return _blnResult;
        }

        private void btn_task_Click(object sender, EventArgs e)
        {
            LabFlowConfirm = btn_task.Tag.ToString().ToUpper();
            this.Close();
        }

        private void btn_LabOrder_Click(object sender, EventArgs e)
        {
            LabFlowConfirm = btn_LabOrder.Tag.ToString().ToUpper();
            this.Close();
        }

        private void btn_RecordResults_Click(object sender, EventArgs e)
        {
            lab_flow_confirm = btn_RecordResults.Tag.ToString().ToUpper();
            this.Close();
        }

        private void frmCnfrmLabFlow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chk_perform.Checked==true)
            {
                if (lab_flow_confirm != "" && lab_flow_confirm.Length > 0)
                {
                    UpdateProviderUsage(lab_flow_confirm);
                    Classes.clsEmdeonGeneral.gloLab_Provider_Usage = lab_flow_confirm;
                }                
            }
        }
        
    }
}
