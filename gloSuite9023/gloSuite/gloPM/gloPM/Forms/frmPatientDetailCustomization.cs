using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloGlobal;

namespace gloPM
{
    public partial class frmPatientDetailCustomization : Form
    {
        #region Constructors

        public frmPatientDetailCustomization()
        {
            InitializeComponent();
        }

        public frmPatientDetailCustomization(string connectionstring)
        {
            InitializeComponent();
        }

        #endregion

        #region Private Variables

        TreeNode PrevNode;

        #endregion

        public enum FormName1
        {
            None = 0,
            Schedule = 1,
            Billing = 2,
            Temp = 3
        }

        /// <summary>
        /// Anil 20070105
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void frmPatientDetailCustomization_Load(object sender, EventArgs e)
        {
            try
            {
                Fill_TreeModule();
                Fill_CheckListModule();
                trvModule.Nodes[0].Expand();
                trvModule.SelectedNode = trvModule.Nodes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// To fill the treeview with modules in the database table "PatientControl_DTL"
        /// </summary>
        public void Fill_TreeModule()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            try
            {
                for (Int32 i = 1; i <= Enum.GetValues(typeof(gloPatientStripControl.FormName)).Length - 1; i++)
                {
                    trvModule.Nodes.Add(Enum.GetValues(typeof(gloPatientStripControl.FormName)).GetValue(i).ToString());
                }

                string _strQuery = "";
                _strQuery = "Select nModule,sInfo from PatientControl_DTL Order By nModule ";                
                DataTable dt = new DataTable();
                oDB.Connect(false);
                oDB.Retrive_Query(_strQuery, out dt);

                for (int i = 0; i <= trvModule.GetNodeCount(false) - 1; i++)
                {
                    string str = "";
                    if (dt != null)
                    {
                        for (int j = 0; j <= dt.Rows.Count - 1; j++)
                        {
                            //if treeview Node is equal to value present in datatable for cheklist then fill str
                            if (Convert.ToInt32(dt.Rows[j]["nModule"]) == i + 1)
                            {
                                if (str == "")
                                    str = dt.Rows[j]["sInfo"].ToString();
                                else
                                    str = str + ", " + dt.Rows[j]["sInfo"];
                            }
                        }
                    }

                    trvModule.Nodes[i].Tag = str;
                }
                oDB.Disconnect();


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Dispose();    
            }

        }

        /// <summary>
        /// To fill checklist with the fields/details to be shown on the patient strip control
        /// </summary>
        public void Fill_CheckListModule()
        {
            try
            {
                for (Int32 i = 0; i <= Enum.GetValues(typeof(gloPatientStripControl.PatientInfo)).Length - 1; i++)
                {
                    chklistModule.Items.Add(Enum.GetValues(typeof(gloPatientStripControl.PatientInfo)).GetValue(i).ToString());
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

        
        private void trvModule_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //set previous node cheked item to collection
            if (PrevNode!=null)
                PrevNode.Tag = SetCollections();
        }

        public string SetCollections()
        {
            string str = "";
            try
            {
                for (int i = 0; i <= chklistModule.Items.Count - 1; i++)
                {
                    if ((Boolean)chklistModule.GetItemChecked(i) == true)
                    {
                        if (str == "")
                            str = i.ToString();
                        else
                            str = str + ", " + i.ToString();
                    }
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return str;
          }

        private void trvModule_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
            if (trvModule.SelectedNode != null)
            {
                
                SetValueChecked(trvModule.SelectedNode.Tag.ToString());
                PrevNode = trvModule.SelectedNode;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

        public void SetValueChecked(string str)
        {
            try
            {
                //set checked value for chklistBox item
                string[] strSplit;

                for (int j = 0; j <= chklistModule.Items.Count - 1; j++)
                {
                    chklistModule.SetItemChecked(j, false);
                }

                if (str != "")
                {
                    strSplit = str.Split(',');

                    for (int i = 0; i <= strSplit.Length - 1; i++)
                    {
                        for (int j = 0; j <= chklistModule.Items.Count - 1; j++)
                        {
                            if (Convert.ToUInt16(strSplit[i]) == j)
                                chklistModule.SetItemChecked(j, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }
        public void SaveControlInfo(Int32 nModule, string sInfo)
        {
            try
            {
            string []   strinfosave ;
            string strDeleteQry ;
            string  strInsertQry;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dt=new DataTable();
            strinfosave = sInfo.Split(',');

            ////Delete previous data from dataTable for selected Node
            strDeleteQry = "Delete PatientControl_DTL where nModule = " + nModule + "";
            oDB.Connect(false);
            oDB.Execute_Query(strDeleteQry ) ; 
            oDB.Disconnect();

            if (sInfo != "")
            {
             //Fill new data for selected Node
                for (int i = 0;i<=strinfosave.Length - 1;i++)
                {
                    strInsertQry = "Insert into PatientControl_DTL (nModule,sInfo) values(" + nModule + " , '" + strinfosave[i] + "' )";
                   // strInsertQry = "UPDATE PatientControl_DTL SET nModule =" + nModule + " , sInfo ='" + strinfosave[i] + "' WHERE (nModule = " + nModule + ")";
                    oDB.Connect(false);
                    //dt=new DataTable();
                    oDB.Execute_Query(strInsertQry) ; 
                    oDB.Disconnect();
                }
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // To Save the Last Selected Patient Details in tag of the Form
                TreeViewCancelEventArgs etrv = null;
                trvModule_BeforeSelect(sender, etrv);

                //save checked item to database
                for (Int32 i = 0; i <= trvModule.GetNodeCount(false) - 1; i++)
                {
                    SaveControlInfo(i + 1, trvModule.Nodes[i].Tag.ToString());
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
            this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}