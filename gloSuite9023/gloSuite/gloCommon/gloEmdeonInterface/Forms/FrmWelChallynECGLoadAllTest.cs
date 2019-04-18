using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;

namespace gloEmdeonInterface.Forms
{
    public partial class FrmWelChallynECGLoadAllTest : Form
    {
        #region "Declaration"
        private const Int16 COL_Select = 0;
        private const Int16 COL_TestID = 1;
        private const Int16 COL_ECGID = 2;
        private const Int16 COL_TestDateTime = 3;
        private const Int16 COL_Stutus= 4;
        private string gloConnectionString = string.Empty;
        private long nPatientID = 0;
        private long nClinicID = 1;
        private int _NoOfTestSelected = 0;

        private CcBase.ITests ALLTest = null;
        public string[] SelectedTest=new string[0] ;

        public int NoOfTestSelected
        {
            get
            {
                return _NoOfTestSelected;
            }
            set
            {
                _NoOfTestSelected = value;
            }
        }
        
        #endregion

        public FrmWelChallynECGLoadAllTest(CcBase.ITests LoadTest, long PatinetID,long ClinicID, string gloEMRconnectionstring)
        {
            InitializeComponent();
            ALLTest = LoadTest;
            nPatientID = PatinetID;
            nClinicID = ClinicID;
            gloConnectionString = gloEMRconnectionstring;
            SelectedTest = new string[ALLTest.Count];   
        }

        private void FrmWelChallynECGLoadAllTest_Load(object sender, EventArgs e)
        {
            try
            {
                gloPatient.gloPatient.GetWindowTitle(this, nPatientID, gloConnectionString, "gloEMR");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            gloEmdeonCommon.gloC1FlexStyle.Style(c1WelchAllynTest, false);
            DesignGrid();
            FillTest();
           
        }

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString().Trim().ToUpper())
            {

                case "PROCESSTEST":
                    {
                        ProcessRecored();
                        this.Close();  
                        break; 
                    }

                case "CLOSE":
                    {
                        this.Close();  
                        break;
                    }

            }



        }

        private void DesignGrid()
        {
            try
            {
              //  c1WelchAllynTest.Clear();
                c1WelchAllynTest.DataSource = null;
                c1WelchAllynTest.Clear();

                c1WelchAllynTest.Cols.Count =5;
                c1WelchAllynTest.Rows.Count = 1;
                c1WelchAllynTest.Rows.Fixed = 1;

                // set visibility of column
                c1WelchAllynTest.Cols[COL_Select].Visible = true;
                c1WelchAllynTest.Cols[COL_TestID].Visible = false;
                c1WelchAllynTest.Cols[COL_ECGID].Visible = false;
                c1WelchAllynTest.Cols[COL_TestDateTime].Visible = true;
                c1WelchAllynTest.Cols[COL_Stutus].Visible = true;
        
                // set column type
                c1WelchAllynTest.Cols[COL_Select].DataType = typeof(bool);
                c1WelchAllynTest.AllowEditing = true;

                // set column editing
                c1WelchAllynTest.Cols[COL_Select].AllowEditing = true;
                c1WelchAllynTest.Cols[COL_TestID].AllowEditing = false;
                c1WelchAllynTest.Cols[COL_ECGID].AllowEditing = false;
                c1WelchAllynTest.Cols[COL_TestDateTime].AllowEditing = false;
                c1WelchAllynTest.Cols[COL_Stutus].AllowEditing = false;
            
                //set Heading
                c1WelchAllynTest.SetData(0, COL_Select, "Select");
                c1WelchAllynTest.SetData(0, COL_TestID, "Test ID");
                c1WelchAllynTest.SetData(0, COL_ECGID, "ECG ID");
                c1WelchAllynTest.SetData(0, COL_TestDateTime, "ECG Test Date");
                c1WelchAllynTest.SetData(0, COL_Stutus, "Import Status");
            
                // set width
                c1WelchAllynTest.Cols[COL_Select].Width = 50;
                c1WelchAllynTest.Cols[COL_TestID].Width =0;
                c1WelchAllynTest.Cols[COL_ECGID].Width = 0;
                c1WelchAllynTest.Cols[COL_TestDateTime].Width =250;
                c1WelchAllynTest.Cols[COL_Stutus].Width = 300;
                

                c1WelchAllynTest.ExtendLastCol = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, "error in design grid " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }

        }

        private void FillTest()
        {
            CcBase.ITest Test = null;
            CcBase.IPersistent2 IPt = null;
            DataTable dtECGID = null;
            long ECGID = 0;
            try
            {
              
                dtECGID = ClsWelchAllynECGLayerGenral.Retrive_ECGID(nClinicID, nPatientID, gloConnectionString); 
                for (int RowCnt = 0; RowCnt <= ALLTest.Count; RowCnt++)
                {
                    Test = null;
                    Test = ALLTest[RowCnt];
                    if (Test.TestType == CcBase.TestType.ttECG)
                    {
                        ECGID = 0;
                        c1WelchAllynTest.Rows.Add();
                        IPt = (CcBase.IPersistent2)Test;
                        ECGID =ClsWelchAllynECGLayerGenral.Get_ECGID(dtECGID, IPt.GetIdAsString());
                        c1WelchAllynTest.SetData(RowCnt + 1, COL_Select, 0);
                        c1WelchAllynTest.SetData(RowCnt + 1, COL_TestID, IPt.GetIdAsString());
                        c1WelchAllynTest.SetData(RowCnt + 1, COL_ECGID, ECGID);
                        c1WelchAllynTest.SetData(RowCnt + 1, COL_TestDateTime, Test.DateTime.ToString());
                        if (ECGID <= 0)
                        {
                            c1WelchAllynTest.SetData(RowCnt + 1, COL_Stutus, "Add");
                        }
                        else
                        {
                            c1WelchAllynTest.SetData(RowCnt + 1, COL_Stutus, "Update");
                        }
                    } // end of test type
                } // end of for
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, "error while filling values into grid " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
              Test = null;
             
            }
          

        }

        private bool ProcessRecored()
        {
            bool _ProcessRecored = false;
            bool IsSelected = false;
            string TestID = string.Empty;
            CcBase.ITest Test = null;
            try
            {
                for (int i = 1; i < c1WelchAllynTest.Rows.Count  ; i++)
                {                
                        Test = null;
                        IsSelected = false;
                        bool.TryParse(Convert.ToString(c1WelchAllynTest.GetData(i, COL_Select)), out IsSelected);
                        TestID = Convert.ToString(c1WelchAllynTest.GetData(i, COL_TestID));
                        if (IsSelected && TestID.Length > 0)
                        {
                          Test = ClsWelchAllynECGLayerGenral.Retrive_Test(TestID, ALLTest);
                         
                            if (Test != null)
                            {
                                SelectedTest[i - 1] = TestID;
                                NoOfTestSelected = NoOfTestSelected + 1;
                                _ProcessRecored = true;
                            }
                        } // end if
                }//end for

               } //end of try
              catch (Exception ex)
               {
                   gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General,  ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                   ex = null;
               }
               finally
               {
                   TestID = string.Empty;
               }
             
           
            return _ProcessRecored;
        }

        private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkSelectAll.Focused)
                {

                    //if (ChkSelectAll.Checked)
                    //{
                    for (int i = 1; i < c1WelchAllynTest.Rows.Count; i++)
                    {
                        c1WelchAllynTest.SetData(i, COL_Select, ChkSelectAll.Checked);
                    }

                    //}


                }

            }
            catch (Exception)
            {
            }
       
        }

        private void c1WelchAllynTest_Click(object sender, EventArgs e)
        {
            
            bool Result = false;
            try
            {
                for (int i = 1; i < c1WelchAllynTest.Rows.Count ; i++)
                {
                    Result = false;
                    if (bool.TryParse(Convert.ToString(c1WelchAllynTest.GetData(i, COL_Select)), out Result))
                    {
                        ChkSelectAll.Checked = Result;
                        if (!Result)
                            break;
                        
                   }
                    
                }

            }
            catch (Exception)
            { }
            finally
            {
                Result = false;
            }
          
        }

        

      

    }
}
