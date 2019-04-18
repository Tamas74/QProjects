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
using System.Windows.Controls.Primitives;
using gloRxPatientSaving;
using System.Data;

namespace gloPatientSavingMessage
{
    /// <summary>
    /// Interaction logic for UC_GenericAlternative.xaml
    /// </summary>
    public partial class UC_GenericAlternative : UserControl
    {
        public UC_GenericAlternative(PatSavOpportunity_Mst oPatSavOppMST)
        {
            InitializeComponent();
            BindData(oPatSavOppMST);
            
        }

        private void BindData(PatSavOpportunity_Mst oPatSavOppMST)
        {
            StringBuilder _Header = null;
            clsProcessLayer objProcessLayer=new clsProcessLayer ();
            try 
	        {	        
		    
            _Header = new StringBuilder();
            _Header.Append("Opportunity : $" + oPatSavOppMST.AnnualCostSavings + " - " + oPatSavOppMST.OrgRxDrugDesc);
            _Header.Append(" - " + oPatSavOppMST.OrgRxDrugQty + " Qty - " + oPatSavOppMST.OrgRxDaysSupply + " days - ");
            _Header.Append(oPatSavOppMST.OrgRxPhBusinessName);
            txtOppHeader.Text = _Header.ToString();
            lblOppHeader.Tag = oPatSavOppMST.PSOppID;
            lblGenAlternative.Text = "$" + oPatSavOppMST.AnnualCostSavings + " Annually";
            lblWrittenDate.Text = oPatSavOppMST.OrgRxWrittenDate.Value.ToShortDateString();
            lblLastFillDate.Text = oPatSavOppMST.OrgRxLastFillDate.Value.ToShortDateString();
            lblProductCode.Text=oPatSavOppMST.OrgRxDrugCode ;
            lblQualifier.Text=oPatSavOppMST.OrgRxDrugQlfr ;
            lblDrugDescription.Text = oPatSavOppMST.OrgRxDrugDesc;
            lblOpportunityType.Text = oPatSavOppMST.OpportunityType;
            ExpOpportunity.IsExpanded = true;

            var oAlternative = (from r in oPatSavOppMST.PatSavOpportunityAlternativeMed_Dtls select r);
          //  bool IsDrugMatch = false;
            foreach (PatSavOpportunityAlternativeMed_Dtl obj in oAlternative)
            {
                UC_alternative _OppControl = new UC_alternative(obj);
                _OppControl.DataContext = obj;
                _OppControl.lblAltrHeader.Content = "Alternate : " + obj.AltMedDrugDesc + " - " + obj.AltMedDrugCode + " (" + obj.AltMedDrugQlfr + ")";
                _OppControl.txtAltrHeader.Text = _Header.ToString();
                _OppControl.lblAltrHeader.Tag = obj.PSOppAltMedID;
                _OppControl.rbAlternate.GroupName = "grp_" + oPatSavOppMST.OpportunityID;
               if(obj.bIsSelected !=null)
                   _OppControl.rbAlternate.IsChecked = obj.bIsSelected;
               else
                   _OppControl.rbAlternate.IsChecked = false;
                stkGenOpportunity.Children.Add(_OppControl);

                if (IsMedCodeMatch(obj.AltMedDrugCode, obj.AltMedDrugQlfr, obj.AltMedDrugDesc) == true)
                {
                //    IsDrugMatch = true;
                }
            }
            if (lblOpportunityType.Text.ToLower() == "generic_alternative")
            {
                //if (IsDrugMatch == false)
                //{
                //    lblDisposition_Code.Content = "05";
                //    txbDisposition_Des.Text = "Opportunity declined because message contains invalid information";
                //    updateTableforDisposition();
                //    this.IsEnabled = false;
                //}
            }
            else
            {
                IsMedicaionNDCAvailable();
            }
           
            if (oPatSavOppMST.DispositionCode != null)
            {
                txbDisposition_Des.Text = objProcessLayer.getCodeDesc("Disposition", oPatSavOppMST.DispositionCode);
                DisposePnl.Visibility = System.Windows.Visibility.Visible;
                this.IsEnabled = false;
            }
	        }
	        catch (Exception)
	        {
		        return ;
	        }
            finally 
            {objProcessLayer=null ;}
           
            
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (lblOpportunityType.Text.ToLower() == "generic_alternative")
            {
                UIElement OBJUIElement = null;
                UC_alternative _OppControl = null;
                bool IsChecked = false;

                for (int i = 0; i < stkGenOpportunity.Children.Count; i++)
                {
                    try { OBJUIElement = stkGenOpportunity.Children[i]; }
                    catch (Exception) { }

                    if (OBJUIElement != null && (OBJUIElement.GetType() == typeof(UC_alternative)))
                    {
                        _OppControl = (UC_alternative)OBJUIElement;

                        if (_OppControl.rbAlternate.IsChecked.Value)
                        {
                            IsChecked = true;
                            i = stkGenOpportunity.Children.Count;
                            lblDisposition_Code.Tag = _OppControl.lblAltrHeader.Tag;
                        }
                    }
                }

                if (!IsChecked)
                {
                    MessageBox.Show("Please Select Alternative", "gloEMR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            lblDisposition_Code.Content = "01";
            txbDisposition_Des.Text = "Opportunity Accepted";
            updateTableforDisposition();
            this.IsEnabled = false;
        }

        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            DenyPopUp.Placement = PlacementMode.MousePoint ;
            DenyPopUp.IsOpen = true;
            
            
            if (lblOpportunityType.Text.ToLower() == "generic_alternative")
            {
                UIElement OBJUIElement = null;
                UC_alternative _OppControl = null;
                for (int i = 0; i < stkGenOpportunity.Children.Count; i++)
                {
                    try { OBJUIElement = stkGenOpportunity.Children[i]; }
                    catch (Exception) { }

                    if (OBJUIElement != null && (OBJUIElement.GetType() == typeof(UC_alternative)))
                    {
                        _OppControl = (UC_alternative)OBJUIElement;
                        _OppControl.rbAlternate.IsChecked  = false;
                    }
                }
            }
            
            #region "binding denypanel from table"
            //clsDataInsertionLayer oDataDataInsertionLayer = new clsDataInsertionLayer();
            //IQueryable<PatSav_Code> oCode = oDataDataInsertionLayer.getCode("Disposition Code");
            //foreach (PatSav_Code  obj in oCode)
            //{
            //    RadioButton rb1 = new RadioButton();
            //    rb1.Tag = obj.sCodeValue;
            //    rb1.Content = obj.sCodeDesc;
            //    rb1.;
            //    DenyPanel.Children.Add(rb1);
            //}
            #endregion
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            DenyPopUp.IsOpen = false;
            lblDisposition_Code.Content = ((RadioButton)sender).Tag;
            txbDisposition_Des.Text = ((RadioButton)sender).Content.ToString();
            updateTableforDisposition();
            this.IsEnabled = false;
        }

        public void updateTableforDisposition()
        {
            decimal _PSOppID=0;
            PatSavOpportunity_Mst _opp = null;
            PatSavOpportunityAlternativeMed_Dtl _alter = null;
            try 
	        {
	            //Update Opportunity disposition code
		        if (lblOppHeader.Tag == null )
                   return; 
                
                decimal.TryParse(lblOppHeader.Tag.ToString() ,out _PSOppID);

                if (_PSOppID <=0)
                     return;

                _opp = (from result in PatSavGeneral.objDataContext.PatSavOpportunity_Msts where result.PSOppID == _PSOppID select result).FirstOrDefault();
 
               if  (_opp!=null)
               {
                   DisposePnl.Visibility = System.Windows.Visibility.Visible;
                   _opp.DispositionCode =Convert.ToString(lblDisposition_Code.Content).Trim();
                   _opp.DispositionDate = DateTime.Now.Date;
                   PatSavGeneral.objDataContext.SubmitChanges();
                   PatSavGeneral.objDataContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
               }

                //Update Alternative selection
               decimal.TryParse(lblDisposition_Code.Tag.ToString(), out _PSOppID);

               if (_PSOppID <= 0)
                   return;

               _alter = (from result in PatSavGeneral.objDataContext.PatSavOpportunityAlternativeMed_Dtls where result.PSOppAltMedID == _PSOppID select result).FirstOrDefault();

               if (_alter != null)
               {
                   _alter.bIsSelected = true;
                   PatSavGeneral.objDataContext.SubmitChanges();
                   PatSavGeneral.objDataContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
               }
	        }
	        catch (Exception)
	        {
                return;
	        }
        
        }

        private void DenyPopUp_MouseLeave(object sender, MouseEventArgs e)
        {
           DenyPopUp.IsOpen = false;
        }

        private void IsMedicaionNDCAvailable(bool _IsDrudCodeMatch=false)
        {
            if (IsMedCodeMatch(lblProductCode.Text, lblQualifier.Text, lblDrugDescription.Text) == false)
            {
                lblDisposition_Code.Content = "05";
                txbDisposition_Des.Text = "Opportunity declined because message contains invalid information";
                updateTableforDisposition();
                this.IsEnabled = false;
                return;
            }

           
        }

        private bool IsMedCodeMatch(string _Code,string _Qualifier,string _Drug)
        {
            bool _result = false;
            gloGlobal.DIB.DrugDetails dtDrugInfo = null;
            gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest oDrugInformation = new gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest();
            try
            {
                if (_Qualifier == "ND")
                {
                    dtDrugInfo = oDrugInformation.GetDrugInfoFromNDCCode(_Code);
                }
                else if (_Qualifier == "MG")
                {
                    dtDrugInfo = oDrugInformation.GetDrugInfoFromGPI(_Code, _Drug);
                }
                else if (_Qualifier == "SBD" || _Qualifier == "SCD" || _Qualifier == "GPK" || _Qualifier == "BPK")
                {
                    dtDrugInfo = oDrugInformation.GetDrugInfoFromRxNorm(_Code);
                }

                if (dtDrugInfo != null)
                {
                    _result = true;
                }
            }
            catch //(Exception ex)
            {
                return false;
            }
            return _result;
        }

       
    }
}
