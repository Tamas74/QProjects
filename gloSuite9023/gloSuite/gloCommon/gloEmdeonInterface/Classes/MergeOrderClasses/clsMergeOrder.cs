using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using gloEmdeonInterface.Classes.MergeOrderClasses;

namespace gloEmdeonInterface.Classes
{
    public class clsMergeOrder : IDisposable
    {
        #region "Attributes Properties and Constructors"
        
        #region "Attributes"
        private clsGloOrder objSourceOrder = null;
        private clsGloOrder objTargetOrder = null;
        private clsMergeOrderDBLayer xMergeDBLayer = null;
        private clsReplaceTests_TVPBuilder xRenameTest = null;

        [DefaultValue(false)]
        private bool SourceOrTargetOrderChanged { get; set; }

        [DefaultValue(false)]
        private bool PreviewScreenDisplayed { get; set; }

        #region "Enumerables and Dictionary"
        Dictionary<UInt64, clsGloOrder> dictionaryOrders = null;

        IEnumerable<clsGloOrder> enumOrders = null;
        IEnumerable<clsGloTest> enumTests = null;
        IEnumerable<clsGloResult> enumResults = null;
        IEnumerable<clsGloResultAttachment> enumAttachments = null;
        IEnumerable<clsGloResultDetail> enumResultDetail = null;

        IEnumerable<clsGloTest> enumMatchedTest = null;
        IEnumerable<clsGloResult> enumMatchedResult = null;
        IEnumerable<clsGloResultAttachment> enumMatchedTestDocument = null;
        IEnumerable<clsGloResultDetail> enumMatchedResultDetail = null;
        #endregion

        #endregion

        #region "Properties"
        [DefaultValue(null)]
        public clsGloOrder SourceOrder { get { return this.objSourceOrder; } set { this.objSourceOrder = value; } }

        [DefaultValue(null)]
        public clsGloOrder TargetOrder { get { return this.objTargetOrder; } set { this.objTargetOrder = value; } }

        [DefaultValue(false)]
        public bool IsTestsRenaming { get; set; }
      
        public string ConnectionString { get; set; }

        [DefaultValue(false)]
        private bool MergeStarted { get; set; }
        #endregion

        #region "Constructors"

        #region "Secondary Constructor"
        public clsMergeOrder(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            dictionaryOrders = new Dictionary<UInt64, clsGloOrder>();
        }
        #endregion

        #region "Primary Constructor"
        public clsMergeOrder(UInt64 SourceOrderID, UInt64 TargetOrderID, string ConnectionString)
            : this(ConnectionString)
        {
            try
            {
                if (SourceOrderID == TargetOrderID)
                { throw new Exception("Source Order and Target Order cannot be the same. Merge operation aborted."); }
                else if (SourceOrderID == 0 || TargetOrderID == 0)
                { throw new Exception("Source\\Target Order ID cannot be 0. Merge operation aborted."); }
                
                this.FillOrdersFromDB(SourceOrderID, TargetOrderID);

                if (!this.dictionaryOrders.ContainsKey(SourceOrderID) || !this.dictionaryOrders.ContainsKey(TargetOrderID))
                {
                    //this.SourceOrder = dictionaryOrders[SourceOrderID];
                    //this.TargetOrder = dictionaryOrders[TargetOrderID];
                    throw new Exception("Either Source Order or Target Order not found. Merge operation aborted.");
                }
                //else { throw new Exception("Either Source Order or Target Order not found. Merge operation aborted."); }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #endregion

        #region "Events"

        public delegate void MergeFired(object sender);
        public event MergeFired mergeFired;

        protected virtual void MergeOrderFired(object sender)
        { if (mergeFired != null) { mergeFired(sender); } }
        #endregion

        #endregion

        #region "Replace Tests"

        #region "Private Functions"

        private void ReplaceTest(Int32 TestHashCodeToReplace, Int32 ReplacingTestHashCode)
        {
            try
            {
                if (this.TargetOrder.ContainsKey(TestHashCodeToReplace))
                {
                    clsGloTest xReplacingTest = TargetOrder[TestHashCodeToReplace];
                    TargetOrder.Remove(TestHashCodeToReplace);

                    xReplacingTest.TestID = SourceOrder[ReplacingTestHashCode].TestID;
                    xReplacingTest.TestName = SourceOrder[ReplacingTestHashCode].TestName;
                    xReplacingTest.LOINCCode = SourceOrder[ReplacingTestHashCode].LOINCCode;

                    ReplaceTestDocument(xReplacingTest);

                    if (xReplacingTest.Values.Any())
                    { 
                        foreach (clsGloResult xResult in xReplacingTest.Values)
                        {
                            List<clsGloResultDetail> listDetail = xResult.Values.ToList();

                            foreach (clsGloResultDetail xDetail in listDetail)
                            {
                                if (xResult.ContainsKey(xDetail.GetHashCode())) {xResult.Remove(xDetail.GetHashCode());}

                                xDetail.labotd_TestID = xReplacingTest.TestID;
                                xDetail.labotd_TestName = xReplacingTest.TestName;
                                xDetail.labotd_LOINCCode = xReplacingTest.LOINCCode;

                                //xDetail.labotr_TestID = xReplacingTest.TestID;
                                xDetail.labotr_TestName = xReplacingTest.TestName;                                

                                xDetail.labotrd_TestName = xReplacingTest.TestName;
                                xDetail.labotrd_LOINCID = xReplacingTest.LOINCCode;

                                if (!xResult.ContainsKey(xDetail.GetHashCode())) { xResult.Add(xDetail.GetHashCode(), xDetail) ;}

                            }
                            listDetail.Clear();
                            listDetail = null;
                        }
                    }

                    TargetOrder.Add(xReplacingTest.Key, xReplacingTest);
                    xReplacingTest = null;
                }
            }
            catch (Exception ex) { throw ex; }           
        }

        //private void ReplaceTestResult(clsGloResult xReplacingResult)
        //{ }

        private void ReplaceTestDocument(clsGloTest xReplacingTest)
        {
            try
            {
                if (xReplacingTest.DictionaryDocuments != null && xReplacingTest.DictionaryDocuments.Any())
                {
                    List<clsGloResultAttachment> xDocument = xReplacingTest.DictionaryDocuments.Values.ToList();

                    foreach (clsGloResultAttachment doc in xDocument)
                    {
                        xReplacingTest.DictionaryDocuments.Remove(doc.AttachmentID);
                        doc.TestID = xReplacingTest.TestID;
                        xReplacingTest.DictionaryDocuments.Add(doc.AttachmentID, doc);
                    }

                    xDocument.Clear();
                    xDocument = null;
                }
            }
            catch (Exception ex) { throw ex; }
            
        }

        private void ReplaceResult(clsGloTest xReplacingTest)
        {
            List<clsGloResult> listResults = null;
            try
            {
                listResults = xReplacingTest.Values.ToList();

                foreach (clsGloResult xReplacingResult in listResults)
                {
                    ReplaceResultDetail(xReplacingResult, xReplacingTest.TestID, xReplacingTest.TestName);

                    // Remove Result from Test
                    xReplacingTest.Remove(xReplacingResult.GetHashCode());
                    xReplacingResult.TestID = xReplacingTest.TestID;
                    xReplacingTest.Add(xReplacingResult.GetHashCode(), xReplacingResult);
                    // Add changed Result in Test

                }
            }
            catch (Exception ex) { throw ex; }
            finally 
            { 
                listResults.Clear();
                listResults = null;
            }

            
        }

        private void ReplaceResultDetail(clsGloResult xReplacingResult, UInt64 TestID, string ReplacingTestName)
        {
            List<clsGloResultDetail> listResultDetails = null;

            try
            {
                listResultDetails = xReplacingResult.Values.ToList();

                foreach (clsGloResultDetail xReplacingResultDetail in listResultDetails)
                {

                    xReplacingResult.Remove(xReplacingResultDetail.GetHashCode());

                    xReplacingResultDetail.labotd_TestID = TestID;

                    xReplacingResultDetail.labotrd_TestName = ReplacingTestName;
                    xReplacingResultDetail.labotr_TestName = ReplacingTestName;
                    xReplacingResultDetail.labotd_TestName = ReplacingTestName;

                    xReplacingResult.Add(xReplacingResultDetail.GetHashCode(), xReplacingResultDetail);
                }
            }
            catch (Exception ex) { throw ex; }
            finally 
            { 
                listResultDetails.Clear();
                listResultDetails = null;
            }
           
        }
      
        private Int32 GetTestKey(bool IsSource, UInt64 TestID)
        {
            Int32 Hash = 0;

            try
            {
                if (IsSource)
                { Hash = SourceOrder.Values.First(p => p.TestID == TestID).Key; }
                else
                { Hash = TargetOrder.Values.First(p => p.TestID == TestID).Key; }
                return Hash;
            }
            catch (Exception ex) { throw ex; }                       
        }

        #endregion

        #region "Public Functions"

        public void ResetReplacingTestObject()
        {
            try
            {
                if (this.xRenameTest != null && this.xRenameTest.RenamingTests != null)
                {
                    this.xRenameTest.RenamingTests.Clear();                  
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void ReplaceTargetTest(UInt64 SourceTestID, UInt64 TargetTestID)
        { ReplaceTargetTest(GetTestKey(true, SourceTestID), GetTestKey(false, TargetTestID)); }

        public void ReplaceTargetTest(int SourceTestHashCode, int TargetTestHashCode)
        {
            try
            {
                if (TargetOrder.ContainsKey(SourceTestHashCode)) { throw new ReplacementTestException("Replacing Test is already present in Target Order."); }
                if (SourceOrder.ContainsKey(TargetTestHashCode)) { throw new ReplacementTestException("Source Order already has Test that will be replaced."); }

                if (SourceOrder.ContainsKey(SourceTestHashCode) && TargetOrder.ContainsKey(TargetTestHashCode))
                {
                    if (this.xRenameTest == null) { this.xRenameTest = new clsReplaceTests_TVPBuilder(); }

                    xRenameTest.AddRow(SourceOrder[SourceTestHashCode], TargetOrder[TargetTestHashCode]);

                    clsGloTest xReplacingTest = TargetOrder[TargetTestHashCode];

                    ReplaceTest(TargetTestHashCode, SourceTestHashCode);
                    ReplaceResult(xReplacingTest);

                    xReplacingTest = null;

                }
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
        
        #endregion

        #region "Preview Merge"
               
        public DataSet BuildPreviewDataSet()
        {
            try
            {
                if (!TargetOrder.IsMerged)
                { BeginMerge(); }

                DataSet PreviewDataSet = null;

                using (clsPreviewMergedOrder_UserControlBinder userControlBinder = new clsPreviewMergedOrder_UserControlBinder(TargetOrder))
                { PreviewDataSet = userControlBinder.BuildPreviewDataSet().Copy(); }
                
                return PreviewDataSet;
                
            }
            catch (Exception ex) { throw ex; }

            
        }
      
        #endregion

        #region "Merge Operations"

        #region "Merge"

        public void ExecuteMerge()
        {
            try
            {
                if (!MergeStarted) { BeginMerge(); }
                EndMerge();
            }
            catch (Exception Ex) { throw Ex; }
        }

        public void BeginMerge()
        {
            {
                try
                {
                    if (this.SourceOrder != null && this.TargetOrder != null)
                    {
                        if (this.SourceOrder.OrderID != this.TargetOrder.OrderID)
                        {
                            TemplateChecks();
                            MergeOrderLevelDetails();
                            MergeSimilarTests();
                            MergeDifferentTest();

                            this.MergeStarted = true;
                            TargetOrder.IsMerged = true;
                            //ExecuteDatabaseUpdateCall();
                        }
                        else { throw new Exception("Source Order and Target Order cannot be the same. Merge aborted."); }
                    }
                    else { throw new Exception("Either Source Order or Target Order is not set. Merge aborted."); }
                }
                catch (Exception Ex) { throw Ex; }
            }
        }

        public void EndMerge()
        {
            if (MergeStarted) { ExecuteDatabaseUpdateCall(); }
            else { throw new Exception("Merge Operation not started yet."); }
        }

        
        #endregion

        #region "Template Logic"

        // If Tests that are matched both have Templates associated with them
        // then we do not allow the Merge operation to continue as
        // individual Word files cannot be merged.

        private void TemplateChecks()
        {            
            List<clsGloTest> lstTemplateTests = new List<clsGloTest>();

            try
            {
                foreach (clsGloTest xTest in clsMergeOrderLINQ.GetMatchedTests(SourceOrder, TargetOrder).Where(Test => SourceOrder[Test.Key].HasTemplate && TargetOrder[Test.Key].HasTemplate))
                {lstTemplateTests.Add(xTest);}
                
                if (lstTemplateTests.Any())
                {
                    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                    stringBuilder.AppendLine("The following Test(s) have Templates associated with them: ");
                    stringBuilder.AppendLine();

                    foreach (clsGloTest xTest in lstTemplateTests) { stringBuilder.AppendLine("Test Name: " + xTest.TestName); }

                    lstTemplateTests.Clear();
                    lstTemplateTests = null;

                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("Template merging is not allowed.");                    

                    throw new TemplateException(stringBuilder.ToString());
                }                
            }
            catch (Exception Ex) { throw Ex; }
            finally { lstTemplateTests = null; }
        }

        #endregion

        #region "Merge Tests"
        
        private void MergeSimilarTests()
        {
            // Tests are matched by both the TestID and the TestName
            // This is done by using a Hash of the TestID and the TestName

            // For each Test that is matched the Results of the Source Order
            // are shifted into the Target Order.

            try
            {
                if (SourceOrder == null || TargetOrder == null) { throw new Exception("Either Source Order or Target Order is not set."); }

                foreach (clsGloTest xMatchedTest in clsMergeOrderLINQ.GetMatchedTests(SourceOrder, TargetOrder))
                {
                    MergeAttachments(xMatchedTest);
                    MergeResults(xMatchedTest);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void MergeDifferentTest()
        {
            IEnumerable<clsGloTest> enumUnmatchedTests = null;

            // For all Tests that are in Source Order but not
            // in Target Order:
            
            // 1. Clone the Test (which will clone all its Results and other children            
            // 2. Set Target Order ID and the IsShifting flag to True
            // and add it in the Target Order

            try
            {
                enumUnmatchedTests = SourceOrder.Values.Except(TargetOrder.Values, new clsGloTestComparer());

                if (enumUnmatchedTests.Any())
                {
                    foreach (clsGloTest _xGloTest in enumUnmatchedTests)
                    {

                        if (!TargetOrder.ContainsKey(_xGloTest.Key))
                        {
                            clsGloTest _xTest = clsGloTest.CloneTest(_xGloTest);
                            _xTest.TargetOrderID = TargetOrder.OrderID;
                            _xTest.IsShifting = true;

                            TargetOrder.Add(_xGloTest.Key, _xTest);
                            _xTest = null;
                        }
                    }
                }
            }
            catch (Exception Ex) { throw Ex; }
            finally { enumUnmatchedTests = null; }
        }

        //private void MergeTestLevelDetails(xGloTest xMatchedTest)
        //{
        //    if (xMatchedTest.Instruction.Any() && TargetOrder[xMatchedTest.ValidatedTestName].Instruction.Any())
        //    {
        //        string sSourceInstruction = xMatchedTest.Instruction;
        //        string sTargetInstruction = TargetOrder[xMatchedTest.ValidatedTestName].Instruction;

        //        StringBuilder sInstructionBuilder = new StringBuilder();

        //        sInstructionBuilder.AppendLine(sSourceInstruction);
        //        sInstructionBuilder.Append(sTargetInstruction);

        //        xMatchedTest.Instruction = sInstructionBuilder.ToString();

        //        sInstructionBuilder.Clear();

        //        sInstructionBuilder = null;
        //        sSourceInstruction = null;
        //        sTargetInstruction = null;

        //    }
        //}

        #endregion

        #region "Merge Order Level Details"

        private void MergeOrderLevelDetails()
        {            
            TargetOrder.ExternalCode = SourceOrder.ExternalCode;
            TargetOrder.IsAcknowledged = SourceOrder.IsAcknowledged;

            if (TargetOrder.OrderStatus == "" || TargetOrder.OrderStatus == null)
            { TargetOrder.OrderStatus = "Sent"; }                        
        }

        #endregion

        #region "Merging of 1..N Relationships of Test"

        private void MergeAttachments(clsGloTest xMatchedTest)
        {
            try
            {

                // Result Documents that are present in both Source and Target Tests
                // have to be removed from the Source Test. Result Documents are 
                // matched by the AttachmentID
                foreach (clsGloResultAttachment _xGloAttachment in clsMergeOrderLINQ.GetMatchedAttachments(SourceOrder, TargetOrder, xMatchedTest))
                {
                    SourceOrder[xMatchedTest.Key].HasAttachmentsForRemoval = true;
                    SourceOrder[xMatchedTest.Key].DictionaryDocuments[_xGloAttachment.AttachmentID].IsDeleting = true;
                }

                // Result Documents that are present in Source Test but not in Target Test
                // have to be added in the Target Test
                foreach (clsGloResultAttachment _xGloAttachment in clsMergeOrderLINQ.GetUnmatchedAttachments(SourceOrder, TargetOrder, xMatchedTest))
                {
                    if (!TargetOrder[xMatchedTest.Key].DictionaryDocuments.ContainsKey(_xGloAttachment.AttachmentID))
                    {
                        clsGloResultAttachment _aGloAttachment = new clsGloResultAttachment(_xGloAttachment);

                        UInt64 nMaxAttachmentNumber = 0;

                        if (TargetOrder[xMatchedTest.Key].DictionaryDocuments.Any())
                        { nMaxAttachmentNumber = TargetOrder[xMatchedTest.Key].DictionaryDocuments.Values.Max(p => p.AttachmentNumber); }

                        _aGloAttachment.AttachmentNumber = Convert.ToUInt64(nMaxAttachmentNumber + 1);
                        _aGloAttachment.IsShifting = true;

                        clsGloTest xTargetTest = TargetOrder[xMatchedTest.Key];
                        xTargetTest.HasAttachmentsForInsertion = true;
                        xTargetTest.DictionaryDocuments.Add(_aGloAttachment.AttachmentID, _aGloAttachment);

                        xTargetTest = null;
                        _aGloAttachment = null;

                    }
                }
            }
            catch (Exception ex) { throw ex; }

            
        }

        private void MergeResults(clsGloTest xMatchedTest)
        {
            try
            {
                // For each Result in Source Test 
                // 1. Set the TargetOrderID to the Target OrderID
                
                // 2. If Target Test has already has Results then
                //    its Maximum Result Number will be 3. So the newly
                //    added Result's Target Result Number should be 4 onwards

                // 3. TargetResultName should be 'Result-' and the Result Number

                // 4. IsUpdating flag is used while generating TVPs for final
                //    database call.

                // 5. Finally this newly generated Result is cloned and
                //    added into the Target Order.

                foreach (clsGloResult xIterator in xMatchedTest.Values)
                {
                    // 1.
                    xIterator.TargetOrderID = TargetOrder[xMatchedTest.Key].OrderID;

                    // 2.
                    if (TargetOrder[xMatchedTest.Key].Values.Any())
                    { xIterator.TargetResultNumber = Convert.ToUInt16(TargetOrder[xMatchedTest.Key].GetMaxResultNo() + 1); }
                    else
                    { xIterator.TargetResultNumber = 1; }

                    // 3.
                    xIterator.TargetResultName = "Result-" + Convert.ToString(xIterator.TargetResultNumber);
                    
                    // 4.
                    xIterator.IsUpdating = true;

                    // 5.
                    TargetOrder[xMatchedTest.Key].Add(xIterator.GetHashCode(), clsGloResult.CloneResult(xIterator));
                }
            }
            catch (Exception ex) { throw ex; }            
        }

      
        #endregion

        #endregion

        #region "Database Update Call"
        private void ExecuteDatabaseUpdateCall()
        {
            try
            {
                if (this.xMergeDBLayer != null)
                {
                    xMergeDBLayer.Build_TVPs();
                    xMergeDBLayer.InsertAttachmentIntoDataTable(SourceOrder, TargetOrder);
                    xMergeDBLayer.InsertTestIntoDataTable(TargetOrder);

                    if (this.xRenameTest != null && this.xRenameTest.RenamingTests != null && this.xRenameTest.RenamingTests.AsEnumerable().Any())
                    { xMergeDBLayer.RenamingTests = xRenameTest.RenamingTests; }

                    xMergeDBLayer.ExecuteUpdate(SourceOrder.OrderID, TargetOrder.OrderID);
                    MergeOrderFired(this);
                }
            }
            catch (Exception ex) { throw ex; }

            

        }
        #endregion

        #region "Object Building"
      
        private void Add_Tests_In_Order(clsGloOrder Order)
        {
            try
            {
                enumMatchedTest = enumTests.Where(p => p.OrderID == Order.OrderID);
                //Add_Tests_In_Order_From_Enumeration(Order, enumMatchedTest);
                foreach (clsGloTest OrderTest in enumMatchedTest)
                {
                    UInt64 nOrderTestID = OrderTest.TestID;

                    if (!Order.ContainsKey(OrderTest.Key))
                    {
                        Add_Result_In_Test(OrderTest);
                        Add_Attachment_In_Test(OrderTest);

                        Order.Add(OrderTest.Key, OrderTest);
                    }
                }

                enumMatchedTest = null;
            }
            catch (Exception Ex) { throw Ex; }

        }

        #region "Add object in Test"

        private void Add_Result_In_Test(clsGloTest Test)
        {            
            try
            {
                enumMatchedResult = enumResults.Where(p => p.OrderID == Test.OrderID && p.TestID == Test.TestID);

                foreach (clsGloResult matchedResult in enumMatchedResult)
                {
                    if (!Test.ContainsKey(matchedResult.GetHashCode()))
                    {
                        InsertResultDetailIntoResult(matchedResult);
                        Test.Add(matchedResult.GetHashCode(), matchedResult);
                    }
                }
            }
            catch { throw new Exception("Error occured while adding Result Detail in Result."); }
            finally { enumMatchedResult = null; }
        }

        private void Add_Attachment_In_Test(clsGloTest xTest)
        {            
            try
            {
                enumMatchedTestDocument = enumAttachments.Where(p => p.TestID == xTest.TestID && p.OrderID == xTest.OrderID);
                if (enumMatchedTestDocument.Any())
                {
                    foreach (clsGloResultAttachment xGloAttachment in enumMatchedTestDocument)
                    {
                        if (!xTest.DictionaryDocuments.ContainsKey(xGloAttachment.AttachmentID))
                        { xTest.DictionaryDocuments.Add(xGloAttachment.AttachmentID, xGloAttachment); }
                    }
                }
            }
            catch { throw new Exception("Error occured while adding Attachment in Test."); }
            finally { enumMatchedTestDocument = null; }
        }

        #endregion

        #region "Add Result Detail in Result"

        private void InsertResultDetailIntoResult(clsGloResult gloResult)
        {

            try
            {
                enumMatchedResultDetail = enumResultDetail.Where(p => p.labom_OrderID == gloResult.OrderID && p.labotd_TestID == gloResult.TestID && p.labotr_TestResultNumber == gloResult.ResultNumber);

                foreach (clsGloResultDetail detailResult in enumMatchedResultDetail)
                {
                    if (!gloResult.ContainsKey(detailResult.GetHashCode())) 
                    { gloResult.Add(detailResult.GetHashCode(), detailResult); }
                }            

            }
            catch (Exception ex) { throw ex; }
            

          
        }

        #endregion

        //private void InsertDiagnosisIntoTest(xGloTest xTest)
        //{
        //    enumMatchedDiag = enumDiagnosis.Where(p => p.OrderID == xTest.OrderID && p.TestID == xTest.TestID);

        //    if (enumMatchedDiag.Any())
        //    {
        //        foreach (xGloDiagnosis xDiagnosis in enumMatchedDiag)
        //        {xTest.ListDiagnosis.Add(xDiagnosis);}
        //    }
        //}

        #endregion

        #region "Fill Order"
        public void FillOrdersFromDB(UInt64 SourceOrderID, UInt64 TargetOrderID)
        {
            try
            {
                if (this.xMergeDBLayer != null)
                { this.xMergeDBLayer.LoadOrders(SourceOrderID, TargetOrderID, this.ConnectionString); }
                else
                { this.xMergeDBLayer = new clsMergeOrderDBLayer(SourceOrderID, TargetOrderID, this.ConnectionString); }

                if (this.SourceOrder != null)
                {
                    SourceOrder.Dispose();
                    SourceOrder.Clear();
                }

                if (this.TargetOrder != null)
                {
                    TargetOrder.Dispose();
                    TargetOrder.Clear();
                }

                if (this.dictionaryOrders != null && this.dictionaryOrders.Any()) { dictionaryOrders.Clear(); }

                enumOrders = this.xMergeDBLayer.Orders.AsEnumerable().Select(p => new clsGloOrder(p));
                enumTests = this.xMergeDBLayer.Tests.AsEnumerable().Select(p => new clsGloTest(p));
                
                enumResults = this.xMergeDBLayer.Results.AsEnumerable().Select(p => new clsGloResult(p));
                enumResultDetail = this.xMergeDBLayer.ResultDetail.AsEnumerable().Select(p => new clsGloResultDetail(p));
                
                enumAttachments = this.xMergeDBLayer.Attachments.AsEnumerable().Select(p => new clsGloResultAttachment(p));
             

                foreach (clsGloOrder Order in enumOrders)
                {
                    if (!dictionaryOrders.ContainsKey(Order.OrderID))
                    {
                        clsGloOrder insertedOrder = new clsGloOrder(Order);

                        Add_Tests_In_Order(insertedOrder);
                        dictionaryOrders.Add(insertedOrder.OrderID, insertedOrder);

                        insertedOrder = null;
                    }
                }

                if (this.dictionaryOrders.ContainsKey(SourceOrderID)) { this.SourceOrder = dictionaryOrders[SourceOrderID]; }
                if (this.dictionaryOrders.ContainsKey(TargetOrderID)) { this.TargetOrder = dictionaryOrders[TargetOrderID]; }
            }
            catch (Exception Ex) { throw Ex; }


        }
        #endregion

        //#region "Build Source and Target Order Object"

        //public void LoadSourceOrder(UInt64 SourceOrderID)
        //{
        //    try
        //    {
        //        //if (this.SourceOrder != null && this.SourceOrder.OrderID != SourceOrderID)
        //        //{
        //        if (this.dictionaryOrders.ContainsKey(SourceOrderID)) 
        //        {
        //            this.dictionaryOrders.Remove(SourceOrderID);
        //            this.SourceOrder.Dispose();
        //            this.SourceOrder = null;
        //        }
                                        
        //        //}

        //        if (this.SourceOrder == null) 
        //        { 
        //            this.FillOrdersFromDB(SourceOrderID, 0);
        //            this.SourceOrTargetOrderChanged = true;
        //        }
        //    }
        //    catch (Exception Ex) { throw Ex; }
        //}

        //public void LoadTargetOrder(UInt64 TargetOrderID)
        //{
        //    try
        //    {
        //        //if (this.TargetOrder != null && this.TargetOrder.OrderID != TargetOrderID)
        //        //{
        //        if (this.dictionaryOrders.ContainsKey(TargetOrderID)) 
        //            {
        //                this.dictionaryOrders.Remove(TargetOrderID);
        //                this.TargetOrder.Dispose();
        //                this.TargetOrder = null;
        //            }
                    
        //        //}
        //        if (this.TargetOrder == null) 
        //        { 
        //            this.FillOrdersFromDB(0, TargetOrderID);
        //            this.SourceOrTargetOrderChanged = true;
        //        }
        //    }
        //    catch (Exception Ex) { throw Ex; }
        //}

        //#endregion

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
                    if (this.xMergeDBLayer != null)
                    {
                        this.xMergeDBLayer.Dispose();
                        this.xMergeDBLayer = null;
                    }
                    
                    if (this.dictionaryOrders != null)
                    {
                        if (this.dictionaryOrders.Any())
                        {
                            foreach (clsGloOrder xOrder in dictionaryOrders.Values.AsParallel())
                            { xOrder.Dispose(); }
                        }

                        this.dictionaryOrders.Clear();
                        this.dictionaryOrders = null;
                    }

                    if (this.SourceOrder != null)
                    {
                        SourceOrder.Dispose();
                        SourceOrder = null;
                    }

                    if (this.TargetOrder != null)
                    {
                        TargetOrder.Dispose();
                        TargetOrder = null;
                    }

                    enumOrders = null;
                    enumTests = null;
                    enumResults = null;
                    enumAttachments = null;
                    enumResultDetail = null;

                    enumMatchedTest = null;
                    enumMatchedResult = null;
                    enumMatchedTestDocument = null;
                    enumMatchedResultDetail = null;
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

}



