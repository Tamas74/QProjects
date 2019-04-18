using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;  
namespace gloRxHub
{
    public enum FormularyCheck
    {
        eRxHistory=0,
        eFormulary=1,
        eBenefits=2
    }
    
    public class ClsFormularyCheck : IDisposable
    {
        
        //private int mProductExclusionCount = 0;
        
        private FormularyCheck eFormularyType;

        public FormularyCheck FormularyType
        {
            get { return eFormularyType; }
            set { eFormularyType = value; }
        }
	
        private ClsRxH_271Master oRxH_271Master;

        public ClsRxH_271Master RxH_271MasterObject
        {
            get { return oRxH_271Master; }
            set { oRxH_271Master = value; }
        }
	
        private string sNDCCode = "";

        private string sFormularyStatus = "";

        public string FormularyStatus
        {
            get { return sFormularyStatus; }
            set { sFormularyStatus = value; }
        }
        public string NDCCode
        {
            get { return sNDCCode; }
            set { sNDCCode = value; }
        }
	
        public int ProductExclusionCount
        {
            get { return ProductExclusionCount; }
            set { ProductExclusionCount = value; }
        }
        private bool disposedValue = false;
        // To detect redundant calls 

        // IDisposable 



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
                // TODO: free managed resources when explicitly called 
            }

            // TODO: free shared unmanaged resources 
            this.disposedValue = true;
        }

        
        // This code added by Visual Basic to correctly implement the disposable pattern. 
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ClsFormularyCheck(ClsRxH_271Master objRxH_271Master, string NDCCode)
        {
            oRxH_271Master = objRxH_271Master;
            CheckFormulary();
            
        }
        private void CheckFormulary()
        {
            SqlConnection conn;
            SqlCommand cmd;
            string strQuery = "";
            System.Data.DataTable dt= new System.Data.DataTable(); 
            try
            {
                conn = new SqlConnection(ClsgloRxHubGeneral.ConnectionString);
                
                cmd = new SqlCommand();
                conn.Open();
                cmd.Connection = conn;
                 
                strQuery = "select count(*) from RxH_Coverage_DE_DTL where sCoverageID =  " + "'" + oRxH_271Master.CoverageId + "'" + " and sServiceID1='" + sNDCCode + "'";
                cmd.CommandType = System.Data.CommandType.Text; 
                cmd.CommandText = strQuery;
                object oResult = cmd.ExecuteScalar();
                //Retrieve formulary Status
                if (oResult == null)
                {
                    ProductExclusionCount = 0;
                    strQuery = "Select isnull(sServiceID1,'')as NDCCode,isnull(sFormularyStatus,'')as FormularyStatus,dbo.RxH_GetFormularyDescription(isnull(sFormularyStatus,'')) as FormularyDescription,isnull(sRelativecost,'') as RelativeCost from RxH_FormularyStatus_DTL where sFormularyID = " + "'" + oRxH_271Master.FormularyListId + "'" + " and sServiceID1='" + sNDCCode + "'";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = strQuery;
                    dt.Load(cmd.ExecuteReader());
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            sFormularyStatus = dt.Rows[0]["FormularyDescription"].ToString();
                        }
                    }
                    else
                    {
                        strQuery = "Select dbo.RxH_GetFormularyDescription(isnull(sNLPrescriptionBrandFStatus,'')) as NLPrescriptionBrandFStatus,dbo.RxH_GetFormularyDescription(isnull(sNLPrescriptionGenericFStatus,'')) as NLPrescriptionGenericFStatus,dbo.RxH_GetFormularyDescription(isnull(sNLBrandCounterFStatus,'')) as NLBrandCounterFStatus,dbo.RxH_GetFormularyDescription(isnull(sNLGenericCounterFStatus,'')) as NLGenericCounterFStatus,dbo.RxH_GetFormularyDescription(isnull(sNLSuppliesFStatus,'')) as NLSuppliesFStatus  from RxH_FormularyStatus_DTL where sFormularyID = " + "'" + oRxH_271Master.FormularyListId + "'";
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = strQuery;
                        dt.Load(cmd.ExecuteReader());
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                sFormularyStatus = "NLPrescriptionBrandFStatus: " + dt.Rows[0]["NLPrescriptionBrandFStatus"].ToString() + Environment.NewLine;
                                sFormularyStatus = sFormularyStatus + "NLPrescriptionGenericFStatus: " + dt.Rows[0]["NLPrescriptionGenericFStatus"].ToString() + Environment.NewLine;
                                sFormularyStatus = sFormularyStatus + "NLBrandCounterFStatus: " + dt.Rows[0]["NLBrandCounterFStatus"].ToString() + Environment.NewLine;
                                sFormularyStatus = sFormularyStatus + "NLGenericCounterFStatus: " + dt.Rows[0]["NLGenericCounterFStatus"].ToString() + Environment.NewLine;
                                sFormularyStatus = sFormularyStatus + "NLSuppliesFStatus: " + dt.Rows[0]["NLSuppliesFStatus"].ToString() + Environment.NewLine;
                            }
                        }
                    }
                }
                else
                {
                    ProductExclusionCount = 1;
                }
                
            }
            catch (ClsRxHubDBLayerException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }
            catch (ClsRxHubInterfaceException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ClsRxHubInterfaceException oRxHubInterfaceException = new ClsRxHubInterfaceException(ex.ToString());

            }
            finally
            {

            }
        }
    }

}