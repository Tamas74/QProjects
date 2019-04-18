using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace gloEmdeonInterface.Classes.MergeOrderClasses
{

    #region "Preview Merged Order Object Builder"

    public class clsPreviewMergedOrder_ObjectBuilder : IDisposable
    {

        #region "Properties"

        public clsGloOrder OrderToDisplay { get; set; }

        #endregion

        #region "Constructors"
        public clsPreviewMergedOrder_ObjectBuilder() { }
        public clsPreviewMergedOrder_ObjectBuilder(clsGloOrder OrderToPreview) { this.OrderToDisplay = clsGloOrder.Clone(OrderToPreview); }
        #endregion

        #region "Functions"
        public clsGloOrder PreviewMergedOrder() { return this.PreviewMergedOrder(OrderToDisplay); }

        private void AddChanged_ResultDetail(clsGloResult xChangedResult, clsGloResultDetail xResultDetail)
        {
            clsGloResultDetail xChangedDetail = clsGloResultDetail.CloneResultDetail(xResultDetail);

            xChangedDetail.labom_OrderID = xChangedResult.OrderID;
            xChangedDetail.labotr_TestResultNumber = xChangedResult.ResultNumber;
            xChangedDetail.labotr_TestResultName = xChangedResult.ResultName;

            xChangedResult.Remove(xResultDetail.GetHashCode());
            xChangedResult.Add(xChangedDetail.GetHashCode(), xChangedDetail);

            xChangedDetail = null;
        }

        private void AddChanged_Result(clsGloTest ChangedTest, clsGloResult xResult)
        {
            clsGloResult xChangedResult = clsGloResult.CloneResult(xResult);
            xChangedResult.OrderID = ChangedTest.OrderID;
            xChangedResult.ResultNumber = xResult.TargetResultNumber;

            List<clsGloResultDetail> xResultDetailList = xChangedResult.Values.ToList();

            foreach (clsGloResultDetail xResultDetail in xResultDetailList)
            { AddChanged_ResultDetail(xChangedResult, xResultDetail); }

            xResultDetailList.Clear();
            xResultDetailList = null;

            ChangedTest.Remove(xResult.GetHashCode());

            if (!ChangedTest.ContainsKey(xChangedResult.GetHashCode()))
            { ChangedTest.Add(xChangedResult.GetHashCode(), xChangedResult); }

            xChangedResult = null;
        }

        public clsGloOrder PreviewMergedOrder(clsGloOrder xOrder)
        {
            clsGloOrder xPreviewOrder = new clsGloOrder();

            foreach (clsGloTest xTest in xOrder.Values)
            {
                clsGloTest xChangedTest = clsGloTest.CloneTest(xTest);

                if (xTest.TargetOrderID != 0) { xChangedTest.OrderID = xTest.TargetOrderID; }

                List<clsGloResult> xResultList = xChangedTest.Values.ToList();

                foreach (clsGloResult xResult in xResultList)
                { AddChanged_Result(xChangedTest, xResult); }

                xResultList.Clear();
                xResultList = null;

                xPreviewOrder.Add(xChangedTest.Key, xChangedTest);

            }
            return xPreviewOrder;
        }

        #region "IDisposable Support"
        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (this.OrderToDisplay != null)
                    {
                        this.OrderToDisplay.Dispose();
                        this.OrderToDisplay = null;
                    }

                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //Protected Overrides Sub Finalize()
        //    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #endregion
    }

    #endregion

    #region "Binder to User Control Class"

    public class clsPreviewMergedOrder_UserControlBinder : IDisposable
    {

        #region "Properties"

        public clsGloOrder OrderToBePreviewed { get; set; }

        public DataSet UserControlBindingDataSet { get; set; }

        #endregion

        #region "Constructor"

        public clsPreviewMergedOrder_UserControlBinder(clsGloOrder OrderToBePreviewed)
        { this.OrderToBePreviewed = OrderToBePreviewed; }

        #endregion

        #region "Build Datatable and load data"

        private void BuildPreviewDataTable(DataTable PreviewDataTable)
        {
            var _1 = PreviewDataTable.Columns;

            _1.Add(new DataColumn("labom_OrderID", System.Type.GetType("System.UInt64")));
            _1.Add(new DataColumn("labotd_TestID", System.Type.GetType("System.UInt64")));
            _1.Add(new DataColumn("labotr_TestResultNumber", System.Type.GetType("System.UInt16")));
            _1.Add(new DataColumn("ResultLineNo", System.Type.GetType("System.UInt16")));
            _1.Add(new DataColumn("TargetOrderID", System.Type.GetType("System.UInt64")));
            _1.Add(new DataColumn("TargetResultNumber", System.Type.GetType("System.UInt16")));
            _1.Add(new DataColumn("labotr_TestID", System.Type.GetType("System.UInt64")));
            _1.Add(new DataColumn("labom_PatientID", System.Type.GetType("System.Int64")));
            _1.Add(new DataColumn("labotd_TestName", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_ResultNameID", System.Type.GetType("System.Int64")));
            _1.Add(new DataColumn("labotrd_ResultName", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labom_VisitID", System.Type.GetType("System.Int64")));
            _1.Add(new DataColumn("labotd_LineNo", System.Type.GetType("System.Int64")));
            _1.Add(new DataColumn("labom_TransactionDate", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotd_TestStatus", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotd_SpecimenSource", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotd_SpecimenConditionDisp", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotd_TestType", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotd_LOINCCode", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotr_TestResultName", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotr_IsFinished", System.Type.GetType("System.Int32")));
            _1.Add(new DataColumn("labotr_TestName", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotr_TestResultDateTime", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_TestSpecimenCollectionDateTime", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_ResultValue", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_ResultUnit", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_ResultRange", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_ResultType", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_AbnormalFlag", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_IsFinished", System.Type.GetType("System.Int32")));
            _1.Add(new DataColumn("labotrd_LOINCID", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_TestName", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_LabFacilityName", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_LabFacilityStreetAddress", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_LabFacilityCity", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_LabFacilityState", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_LabFacilityZipCode", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_ResultComment", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_specificResultRefRange", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_TestResultNumber", System.Type.GetType("System.Int32")));
            _1.Add(new DataColumn("labotrd_ResultDateTime", System.Type.GetType("System.String")));
            _1.Add(new DataColumn("labotrd_ResultLineNo", System.Type.GetType("System.Int32")));
            _1.Add(new DataColumn("labotd_DMSID", System.Type.GetType("System.Int64")));
            _1.Add(new DataColumn("sCPTCode", System.Type.GetType("System.String")));
        }

        private DataRow LoadRow(DataRow Row, clsGloResultDetail xResultDetail)
        {
            Row["labom_OrderID"] = xResultDetail.labom_OrderID;
            Row["labom_PatientID"] = xResultDetail.labom_PatientID;
            Row["labotd_TestID"] = xResultDetail.labotd_TestID;
            Row["labotr_TestID"] = xResultDetail.labotr_TestID;
            Row["labotd_TestName"] = xResultDetail.labotd_TestName;
            Row["labotr_TestResultNumber"] = xResultDetail.labotr_TestResultNumber;
            Row["labotrd_ResultLineNo"] = xResultDetail.ResultLineNo;
            Row["labotrd_ResultNameID"] = xResultDetail.labotrd_ResultNameID;
            Row["labotrd_ResultName"] = xResultDetail.labotrd_ResultName;
            Row["labom_VisitID"] = xResultDetail.labom_VisitID;
            Row["labotd_LineNo"] = xResultDetail.labotd_LineNo;
            Row["labom_TransactionDate"] = xResultDetail.labom_TransactionDate;
            Row["labotd_TestStatus"] = xResultDetail.labotd_TestStatus;
            Row["labotd_SpecimenSource"] = xResultDetail.labotd_SpecimenSource;
            Row["labotd_SpecimenConditionDisp"] = xResultDetail.labotd_SpecimenConditionDisp;
            Row["labotd_TestType"] = xResultDetail.labotd_TestType;
            Row["labotd_LOINCCode"] = xResultDetail.labotd_LOINCCode;
            Row["labotr_TestResultName"] = xResultDetail.labotr_TestResultName;
            Row["labotr_IsFinished"] = xResultDetail.labotr_IsFinished;
            Row["labotr_TestName"] = xResultDetail.labotr_TestName;
            Row["labotr_TestResultDateTime"] = xResultDetail.labotr_TestResultDateTime;
            Row["labotrd_TestSpecimenCollectionDateTime"] = xResultDetail.labotrd_TestSpecimenCollectionDateTime;
            Row["labotrd_ResultValue"] = xResultDetail.labotrd_ResultValue;
            Row["labotrd_ResultUnit"] = xResultDetail.labotrd_ResultUnit;
            Row["labotrd_ResultRange"] = xResultDetail.labotrd_ResultRange;
            Row["labotrd_ResultType"] = xResultDetail.labotrd_ResultType;
            Row["labotrd_AbnormalFlag"] = xResultDetail.labotrd_AbnormalFlag;
            Row["labotr_IsFinished"] = xResultDetail.labotr_IsFinished;
            Row["labotrd_LOINCID"] = xResultDetail.labotrd_LOINCID;
            Row["labotrd_TestName"] = xResultDetail.labotrd_TestName;
            Row["labotrd_LabFacilityName"] = xResultDetail.labotrd_LabFacilityName;
            Row["labotrd_LabFacilityStreetAddress"] = xResultDetail.labotrd_LabFacilityStreetAddress;
            Row["labotrd_LabFacilityCity"] = xResultDetail.labotrd_LabFacilityCity;
            Row["labotrd_LabFacilityState"] = xResultDetail.labotrd_LabFacilityState;
            Row["labotrd_LabFacilityZipCode"] = xResultDetail.labotrd_LabFacilityZipCode;
            Row["labotrd_ResultComment"] = xResultDetail.labotrd_ResultComment;
            Row["labotrd_specificResultRefRange"] = xResultDetail.labotrd_specificResultRefRange;
            Row["labotrd_TestResultNumber"] = xResultDetail.labotrd_TestResultNumber;
            Row["labotrd_ResultDateTime"] = xResultDetail.labotrd_ResultDateTime;
            Row["labotrd_ResultLineNo"] = xResultDetail.labotrd_ResultLineNo;
            Row["labotd_DMSID"] = xResultDetail.labotd_DMSID;
            Row["sCPTCode"] = xResultDetail.sCPTCode;

            return Row;
        }

        private DataRow LoadNoResultsRow(DataRow Row, clsGloTest xTest)
        {
            Row["labom_OrderID"] = xTest.OrderID;
            Row["labom_PatientID"] = 0;
            Row["labotd_TestID"] = xTest.TestID;
            Row["labotr_TestID"] = 0;
            Row["labotd_TestName"] = xTest.TestName;
            Row["labotr_TestResultNumber"] = 0;
            Row["labotrd_ResultLineNo"] = 0;
            Row["labotrd_ResultNameID"] = 0;
            Row["labotrd_ResultName"] = null;
            Row["labom_VisitID"] = OrderToBePreviewed.VisitID;
            Row["labotd_LineNo"] = xTest.LineNo;
            Row["labom_TransactionDate"] = OrderToBePreviewed.TransactionDate;
            Row["labotd_TestStatus"] = xTest.TestStatus;
            Row["labotd_SpecimenSource"] = null;
            Row["labotd_SpecimenConditionDisp"] = null;
            Row["labotd_TestType"] = null;
            Row["labotd_LOINCCode"] = xTest.LOINCCode;
            Row["labotr_TestResultName"] = null;
            Row["labotr_IsFinished"] = "0";
            Row["labotr_TestName"] = null;
            Row["labotr_TestResultDateTime"] = null;
            Row["labotrd_TestSpecimenCollectionDateTime"] = null;
            Row["labotrd_ResultValue"] = null;
            Row["labotrd_ResultUnit"] = null;
            Row["labotrd_ResultRange"] = null;
            Row["labotrd_ResultType"] = null;
            Row["labotrd_AbnormalFlag"] = null;
            Row["labotr_IsFinished"] = 0;
            Row["labotrd_LOINCID"] = null;
            Row["labotrd_TestName"] = null;
            Row["labotrd_LabFacilityName"] = null;
            Row["labotrd_LabFacilityStreetAddress"] = null;
            Row["labotrd_LabFacilityCity"] = null;
            Row["labotrd_LabFacilityState"] = null;
            Row["labotrd_LabFacilityZipCode"] = null;
            Row["labotrd_ResultComment"] = null;
            Row["labotrd_specificResultRefRange"] = null;
            Row["labotrd_TestResultNumber"] = "0";
            Row["labotrd_ResultDateTime"] = null;
            Row["labotrd_ResultLineNo"] = "0";
            Row["labotd_DMSID"] = "0";
            Row["sCPTCode"] = null; 

            return Row;
        }

        #endregion

        #region "Build Preview Dataset"

        public DataSet BuildPreviewDataSet()
        {
            UserControlBindingDataSet = new DataSet();
            DataTable dtPreview = new DataTable("PreviewDataTable");
            DataRow dRow = null;

            UserControlBindingDataSet.Tables.Add(dtPreview);
            BuildPreviewDataTable(dtPreview);

            clsGloOrder xPreviewOrder = null;

            using (clsPreviewMergedOrder_ObjectBuilder xPreview = new clsPreviewMergedOrder_ObjectBuilder(OrderToBePreviewed))
            { xPreviewOrder = xPreview.PreviewMergedOrder(); }

            // Ashish revised below code for optimization
            // on 4-Apr-2014

            foreach (clsGloTest xTest in xPreviewOrder.Values.Where(p => !p.Values.Any()))
            {
                dRow = dtPreview.NewRow();
                dtPreview.Rows.Add(LoadNoResultsRow(dRow, xTest));
                dRow = null;
            }

            foreach (clsGloResultDetail xResultDetail in clsMergeOrderLINQ.GetAllResultDetails(xPreviewOrder))
            {
                dRow = dtPreview.NewRow();
                dtPreview.Rows.Add(LoadRow(dRow, xResultDetail));
                dRow = null;
            }
           
            return this.UserControlBindingDataSet;
        }

        #endregion

        #region "IDisposable Support"
        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    if (this.UserControlBindingDataSet != null)
                    {
                        this.UserControlBindingDataSet.Clear();
                        this.UserControlBindingDataSet.Dispose();
                        this.UserControlBindingDataSet = null;
                    }

                    this.OrderToBePreviewed = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //Protected Overrides Sub Finalize()
        //    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    #endregion

}
