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
using gloRxPatientSaving;
using System.Data;

namespace gloPatientSavingMessage
{
    /// <summary>
    /// Interaction logic for UC_30_90DayDispense.xaml
    /// </summary>
    public partial class UC_alternative : UserControl
    {
        public UC_alternative(PatSavOpportunityAlternativeMed_Dtl oAlternative)
        {
            InitializeComponent();
        }

        private void rbAlternate_Click(object sender, RoutedEventArgs e)
        {
            gloGlobal.DIB.DrugDetails dtDrugInfo = null;
            gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest oDrugInformation = new gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest();
            try
            {
                if (lblQualifier2.Text.ToString() == "ND")
                {
                    dtDrugInfo = oDrugInformation.GetDrugInfoFromNDCCode(lblDrugDBCode1.Text.ToString());
                }
                else if (lblQualifier2.Text.ToString() == "MG")
                {
                    dtDrugInfo = oDrugInformation.GetDrugInfoFromGPI(lblDrugDBCode1.Text.ToString(), lblDrugDescription2.Text.ToString());
                }
                else if (lblQualifier2.Text.ToString() == "SBD" || lblQualifier2.Text.ToString() == "SCD" || lblQualifier2.Text.ToString() == "GPK" || lblQualifier2.Text.ToString() == "BPK")
                {
                    dtDrugInfo = oDrugInformation.GetDrugInfoFromRxNorm(lblDrugDBCode1.Text.ToString());
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(" Drug match not found. Either select another alternative or send disposition message with appropriate disposition code.", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                if (dtDrugInfo != null)
                {
                    System.Windows.Forms.MessageBox.Show(" Drug match not found. Either select another alternative or send disposition message with appropriate disposition code.", "gloEMR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
            catch //(Exception ex)
            {
                return;
            }
            
        }

        private void rbAlternate_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        } 
    }
}
