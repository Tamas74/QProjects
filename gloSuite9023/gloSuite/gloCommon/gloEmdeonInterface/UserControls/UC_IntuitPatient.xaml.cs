using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using gloDatabaseLayer; 


namespace gloEmdeonInterface.UserControls
{
    /// <summary>
    /// Interaction logic for UC_IntuitPatient.xaml
    /// </summary>
    public partial class UC_IntuitPatient : UserControl
    {
        #region "Private Varribles"

        private string ConString = string.Empty;

        private DataGrid innerDataGrid;

        private List<IntuitePatient> lIntuitePatient;

        private List<Trscationdetails> lTrscationdetails;

        private IEnumerable<IntuitePatient> dgPatientIteamSource = null;

        private Int64 nTempVarriable=0;

        private bool bRedundantEvent = false;

        public System.Windows.Forms.Form FormsWindow { get; set; }

        #endregion

        #region "Forms Events"

        public UC_IntuitPatient(string sConnectionString)
        {
            bRedundantEvent = !bRedundantEvent;
            InitializeComponent();
            ConString = sConnectionString;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FillDataGrid();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error);
                ex = null;
            }
            finally
            {
                bRedundantEvent = !bRedundantEvent;
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow.GetRowContainingElement((Expander)sender).DetailsVisibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure); 
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error);
                ex = null;
            }


        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridRow.GetRowContainingElement((Expander)sender).DetailsVisibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure); 
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error);
                ex = null;
            }


        }

        private void dgPatient_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
        {
            try
            {
                innerDataGrid = e.DetailsElement as DataGrid;

                if (e.Row.DetailsVisibility == Visibility.Collapsed)
                {
                    innerDataGrid.ItemsSource = null;
                }
                else
                {
                    nTempVarriable = ((IntuitePatient)e.Row.Item).MemberID;
                    innerDataGrid.ItemsSource = RetriveCFFHistory(nTempVarriable);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error);
                ex = null;
            }
            finally
            {
                nTempVarriable = 0;
                innerDataGrid = null;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bRedundantEvent = !bRedundantEvent;
                Chkselect.IsChecked = false;
                lblSearchFor.Content = "Patient Code :";
                txtSerarchFor.Text = "";
                FillDataGrid();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error);
                ex = null;
            }
            finally
            {
                bRedundantEvent = !bRedundantEvent;     
            }

        }

        private void btnGenrateQueue_Click(object sender, RoutedEventArgs e)
        {
            DataTable dtResult = null;
            DataRow Dr = null;
            try
            {
                dgPatientIteamSource = (IEnumerable<IntuitePatient>)dgPatient.ItemsSource;

                if (dgPatientIteamSource.Count() <= 0)
                {
                    MessageBox.Show("None of the patient found activated with the intuit portal.", "gloEMR", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //get all checked patient from grid
                dgPatientIteamSource = (from Result in dgPatientIteamSource where Result.Sel == true select Result).ToList();

                //if no patient selected then give warning message
                if (dgPatientIteamSource.Count() <= 0)
                {
                    MessageBox.Show("Select at least one patient to download Custom Forms.", "gloEMR", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    //get all patient in datatable
                    dtResult = new DataTable();
                    dtResult.Columns.Add("PatientID", typeof(Int64));
                    dtResult.Columns.Add("MemberID", typeof(Int64));
                    dtResult.Columns.Add("nQuestionId", typeof(Int64));
                    dtResult.Columns.Add("nTransId", typeof(Int64));

                    foreach (IntuitePatient item in dgPatientIteamSource)
                    {
                        Dr = dtResult.NewRow();
                        Dr["PatientID"] = item.PatientId;
                        Dr["MemberID"] = item.MemberID;
                        Dr["nQuestionId"] = 0;
                        Dr["nTransId"] = 0;
                        dtResult.Rows.Add(Dr);
                    }

                    //Call genrate  message Queue method to genrate Queue for selected patient
                    GenrateMessageQueue(dtResult);

                    MessageBox.Show("Selected patient records are added to queue for downloading Form Data.", "gloEMR", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error);
                ex = null;
            }
            finally
            {
                if (dtResult != null)
                {
                    dtResult.Dispose();
                    dtResult = null;
                }
                Dr = null;
                dgPatientIteamSource = null;
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {              
                this.FormsWindow.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure); 
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error);
                ex = null; 
            }

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (bRedundantEvent)
                return;

            try
            {               
                bRedundantEvent = !bRedundantEvent;

                dgPatient.BeginEdit();
 
                dgPatientIteamSource = (IEnumerable<IntuitePatient>)dgPatient.ItemsSource;

                foreach (IntuitePatient objIntpat in dgPatientIteamSource)
                {
                    objIntpat.Sel = Chkselect.IsChecked.Value;
                }

                dgPatient.CommitEdit(DataGridEditingUnit.Row, true); 

                dgPatient.Items.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                dgPatientIteamSource = null;
                bRedundantEvent = !bRedundantEvent;
            }

        }

        private void txtSerarchFor_TextChanged(object sender, TextChangedEventArgs e)
        {
            ImgBrowseClose.Visibility = Visibility.Visible;

            if (bRedundantEvent)
                return;
            try
            {
                if (txtSerarchFor.Text.Trim().Length <= 0)
                {
                    dgPatient.ItemsSource = lIntuitePatient;
                }
                else
                {
                    switch (lblSearchFor.Content.ToString().Trim().ToLower())
                    {
                        case "patient code :":
                            dgPatient.ItemsSource = (from Result in lIntuitePatient
                                                     where Result.PatientCode.ToLower().Contains(txtSerarchFor.Text.Trim().ToLower())
                                                     select Result).ToList();
                            break;
                        case "patient name :":
                            dgPatient.ItemsSource = (from Result in lIntuitePatient
                                                     where Result.PatientName.ToLower().Contains(txtSerarchFor.Text.Trim().ToLower())
                                                     select Result).ToList();
                            break;
                        case "gender :":
                            dgPatient.ItemsSource = (from Result in lIntuitePatient
                                                     where Result.Gender.ToLower().Contains(txtSerarchFor.Text.Trim().ToLower())
                                                     select Result).ToList();
                            break;
                        case "date of birth :":
                            dgPatient.ItemsSource = (from Result in lIntuitePatient
                                                     where Result.Dob.ToString("MM/dd/yyyy").ToLower().Contains(txtSerarchFor.Text.Trim().ToLower())
                                                     select Result).ToList();
                            break;
                        default:
                            dgPatient.ItemsSource = lIntuitePatient;
                            break;
                    }

                }
                dgPatient.Items.Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error);
                ex = null;
            }

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            

            try
            {
                

                bRedundantEvent = !bRedundantEvent;

                txtSerarchFor.Text = string.Empty;

                dgPatient.ItemsSource = lIntuitePatient;

                dgPatient.Items.Refresh();

                

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButton.OK, MessageBoxImage.Error);
                ex = null;
            }
            finally
            {
                bRedundantEvent = !bRedundantEvent;
            }

        }

        private void columnHeader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                nTempVarriable = ((DataGridColumnHeader)sender).DisplayIndex;

                if (nTempVarriable != 0 && nTempVarriable != 5)
                    lblSearchFor.Content = ((DataGridColumnHeader)sender).Content.ToString() + " :";


            }
            catch { }
            finally { nTempVarriable = 0; }

        }

        private void ChkSel_Checked(object sender, RoutedEventArgs e)
        {
            if (bRedundantEvent)
                return;

            CheckBox chk = null;          
            try
            {               

                bRedundantEvent = !bRedundantEvent;

                dgPatientIteamSource = (IEnumerable<IntuitePatient>)dgPatient.ItemsSource;

                if (dgPatientIteamSource == null)
                    return;

                chk = (CheckBox)sender;

                nTempVarriable = (from Result in dgPatientIteamSource where Result.Sel == chk.IsChecked select Result).Count();


                if (nTempVarriable == dgPatientIteamSource.Count())
                {
                    Chkselect.IsChecked = chk.IsChecked;
                }
                else if (Chkselect.IsChecked.Value == true && chk.IsChecked == false)
                {
                    Chkselect.IsChecked = chk.IsChecked;
                }     

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
               dgPatientIteamSource = null;
               chk = null;
               nTempVarriable = 0;
               bRedundantEvent = !bRedundantEvent;
            }
        }
       
        private void dgPatient_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
        }
 
        #endregion


        #region "Form Methods and functions"

        private void FillDataGrid()
        {

            DBLayer oDBLayer = null;
            DataTable dtResult = null;
            try
            {
                oDBLayer = new DBLayer(ConString);
                oDBLayer.Connect(false);
                oDBLayer.Retrive("gsp_FusionGetAllHF", out dtResult);  
                oDBLayer.Disconnect();

                lIntuitePatient = (from result in dtResult.AsEnumerable()
                                   select new IntuitePatient(false,
                                                            Convert.ToInt64(result["MemberID"].ToString()),
                                                            Convert.ToInt64(result["nPatientID"].ToString()),
                                                            Convert.ToString(result["sPatientCode"]),
                                                            Convert.ToString(result["PName"]),
                                                            Convert.ToString(result["sGender"]),
                                                            Convert.ToDateTime(result["dtDOB"]))).ToList();
                //get only distnct patient from list
                lIntuitePatient = lIntuitePatient.GroupBy(i => i.PatientId).Select(group => group.First()).ToList();
                //bind patient data grid
                dgPatient.ItemsSource = lIntuitePatient;

                //get details recoreds for each trscation
                Int32 icnt = 1;
                lTrscationdetails = (from result in dtResult.AsEnumerable()
                                     where result["dtDownloaded"] != DBNull.Value
                                     select new Trscationdetails(
                                                 icnt++,
                                                 Convert.ToInt64(result["MemberID"]),
                                                 Convert.ToDateTime(result["dtDownloaded"]),
                                                 result["CustomFormCCFID"].ToString(),
                                                 result["FormName"].ToString(),
                                                 result["nTranType"].ToString())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                if (dtResult != null)
                {
                    dtResult.Dispose();
                    dtResult = null;
                }
            }
        }

        private void GenrateMessageQueue(DataTable dtpatient)
        {
            DBLayer oDBLayer = null;
            DBParameters oDBParameters = null;
            try
            {
                oDBParameters = new DBParameters();
                oDBParameters.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@dtDateTimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@tvpMapData", dtpatient, ParameterDirection.Input, SqlDbType.Structured);    
                oDBLayer = new DBLayer(ConString);
                oDBLayer.Connect(false);
                oDBLayer.Execute("gsp_FusionINUpAllHFMsgQ", oDBParameters); 
                oDBLayer.Disconnect();              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
            }
        }

        private List<Trscationdetails> RetriveCFFHistory(Int64 nMemberID)
        {
            List<Trscationdetails> IResult = null;
            Int32 Icnt = 1;
            try
            {
                IResult = (from result in lTrscationdetails
                           where result.nmemberID == nMemberID
                           orderby result.dtDownloaded descending
                           select new Trscationdetails(Icnt++,
                                                        result.nmemberID,
                                                        result.dtDownloaded,
                                                        result.CustomFormID,
                                                        result.sFormName,
                                                        result.TrscationType)).ToList();
            }
            catch { }
            return IResult;

        }
        
        public void UserControl_Closing()
        {
            try
            {
                if (lIntuitePatient != null)
                {
                    lIntuitePatient.Clear();
                    lIntuitePatient = null;
                }
                if (lTrscationdetails != null)
                {
                    lTrscationdetails.Clear(); 
                    lTrscationdetails = null;
                }
                innerDataGrid = null;
                dgPatientIteamSource = null;
            }
            catch { }
        }

        #endregion  

      
     
       

    }

    public class IntuitePatient
    {
        public bool Sel { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 MemberID { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }

        public IntuitePatient(bool Sel, Int64 MemberID, Int64 PatientId, string PatientCode, string PatientName, string Gender, DateTime Dob)
        {
            this.Sel = Sel;
            this.MemberID = MemberID;
            this.PatientId = PatientId;
            this.PatientCode = PatientCode;
            this.PatientName = PatientName;
            this.Gender = Gender;
            this.Dob = Dob;
        }


    }

    public class Trscationdetails
    {
        public Int32 SrNo { get; set; }
        public Int64 nmemberID { get; set; }
        public DateTime dtDownloaded { get; set; }
        public string CustomFormID { get; set; }
        public string sFormName { get; set; }
        public string TrscationType { get; set; }

        public Trscationdetails(Int32 SrNo, Int64 nmemberID, DateTime dtDownloaded, string CustomFormID, string sFormName, string TrscationType)
        {
            this.SrNo = SrNo;
            this.nmemberID = nmemberID;
            this.dtDownloaded = dtDownloaded;
            this.CustomFormID = CustomFormID;
            this.sFormName = sFormName;
            this.TrscationType = TrscationType;

        }

    }

}
