﻿// The following code was generated by Microsoft Visual Studio 2005.
// The test owner should check each test for validity.
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using gloReports;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

namespace gloPMUnitTesting
{
    /// <summary>
    ///This is a test class for gloReports.frmRpt_FinancialSummary and is intended
    ///to contain all gloReports.frmRpt_FinancialSummary Unit Tests
    ///</summary>
    [TestClass()]
    public class frmRpt_FinancialSummaryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for getCloseDate ()
        ///</summary>
        [DeploymentItem("gloReports.dll")]
        [TestMethod()]
        public void getCloseDateTest()
        {
            string databaseconnectionstring = null; // TODO: Initialize to an appropriate value

            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting(System.Windows.Forms.Application.StartupPath);
            if (oSettings.GetDatabaseSettings_Registry() == true)
            {
                databaseconnectionstring = oSettings.DBConnectionString;
            }

            frmRpt_FinancialSummary target = new frmRpt_FinancialSummary(databaseconnectionstring);

            gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor accessor = new gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor(target);

            string expected = "Error in Returning Date.";
            string actual;

            actual = accessor.getCloseDate();

            Assert.AreNotEqual(expected, actual, "gloReports.frmRpt_FinancialSummary.getCloseDate did not return the expected value.");
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }


        /// <summary>
        ///A test for getClinicName ()
        ///</summary>
        [DeploymentItem("gloReports.dll")]
        [TestMethod()]
        public void getClinicNameTest()
        {

            string databaseconnectionstring = null; // TODO: Initialize to an appropriate value

            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting(System.Windows.Forms.Application.StartupPath);
            if (oSettings.GetDatabaseSettings_Registry() == true)
            {
                databaseconnectionstring = oSettings.DBConnectionString;
            }
            frmRpt_FinancialSummary target = new frmRpt_FinancialSummary(databaseconnectionstring);

            gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor accessor = new gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor(target);

            string expected = "Error in Returning Clinic Name.";
            string actual;

            actual = accessor.getClinicName();

            Assert.AreNotEqual(expected, actual, "gloReports.frmRpt_FinancialSummary.getClinicName did not return the expected value.");

            //Assert.Inconclusive("Verify the correctness of this test method.");
        }



        /// <summary>
        ///A test for hideYear (ReportDocument, string, string)
        ///</summary>
        [DeploymentItem("gloReports.dll")]
        [TestMethod()]
        public void hideYearTest()
        {
            string databaseconnectionstring = null; // TODO: Initialize to an appropriate value

            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting(System.Windows.Forms.Application.StartupPath);
            if (oSettings.GetDatabaseSettings_Registry() == true)
            {
                databaseconnectionstring = oSettings.DBConnectionString;
            }

            frmRpt_FinancialSummary target = new frmRpt_FinancialSummary(databaseconnectionstring);

            gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor accessor = new gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor(target);

            Rpt_FinancialSummaryNone_withoutDay oRpt_FinancialSummaryNone_withoutDay = new Rpt_FinancialSummaryNone_withoutDay();
            ReportDocument rptObj = oRpt_FinancialSummaryNone_withoutDay; // TODO: Initialize to an appropriate value

            string startDate = "11/11/2000"; // TODO: Initialize to an appropriate value

            string endDate = "11/11/2010"; // TODO: Initialize to an appropriate value

            bool expected = false;
            bool actual;

            actual = accessor.hideYear(rptObj, startDate, endDate);

            Assert.AreNotEqual(expected, actual, "gloReports.frmRpt_FinancialSummary.hideYear did not return the expected value.");
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for passParametres (ReportDocument, string, string, string, DataSet, string, string, string)
        ///</summary>
        [DeploymentItem("gloReports.dll")]
        [TestMethod()]
        public void passParametresTest()
        {
            string databaseconnectionstring = null; // TODO: Initialize to an appropriate value

            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting(System.Windows.Forms.Application.StartupPath);
            if (oSettings.GetDatabaseSettings_Registry() == true)
            {
                databaseconnectionstring = oSettings.DBConnectionString;
            }
            frmRpt_FinancialSummary target = new frmRpt_FinancialSummary(databaseconnectionstring);

            gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor accessor = new gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor(target);

            Rpt_FinancialSummaryNone_withoutDay oRpt_FinancialSummaryNone_withoutDay = new Rpt_FinancialSummaryNone_withoutDay();
            ReportDocument rptObj = oRpt_FinancialSummaryNone_withoutDay; // TODO: Initialize to an appropriate value

            string reportType = "Financial Summary"; // TODO: Initialize to an appropriate value

            string breakBy = "None"; // TODO: Initialize to an appropriate value

            string clinic = "Clinic Name"; // TODO: Initialize to an appropriate value

            DataSet ds = new DataSet(); // TODO: Initialize to an appropriate value
            ds.Tables.Add("dtparam");
            string startDate = "11/11/2000"; // TODO: Initialize to an appropriate value

            string endDate = "11/11/2010"; // TODO: Initialize to an appropriate value

            string userName = "Admin"; // TODO: Initialize to an appropriate value

            bool expected = false;
            bool actual;

            actual = accessor.passParametres(rptObj, reportType, breakBy, clinic, ds, startDate, endDate, userName);

            Assert.AreNotEqual(expected, actual, "gloReports.frmRpt_FinancialSummary.passParametres did not return the expected value.");
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }



        /// <summary>
        ///A test for FillAgingReport (string, string, string, string)
        ///</summary>
        [DeploymentItem("gloReports.dll")]
        [TestMethod()]
        public void FillAgingReportTest()
        {
            Int32 counter = -1;
            string databaseconnectionstring = null; // TODO: Initialize to an appropriate value

            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting(System.Windows.Forms.Application.StartupPath);
            if (oSettings.GetDatabaseSettings_Registry() == true)
            {
                databaseconnectionstring = oSettings.DBConnectionString;
            }
            frmRpt_FinancialSummary target = new frmRpt_FinancialSummary(databaseconnectionstring);

            gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor accessor = new gloPMUnitTesting.gloReports_frmRpt_FinancialSummaryAccessor(target);
            DataTable myDatatable = new DataTable();
            myDatatable.Columns.Add("charges");
            myDatatable.Columns.Add("WriteOff");
            myDatatable.Columns.Add("Adjustment");
            myDatatable.Columns.Add("InsurancePayment");
            myDatatable.Columns.Add("PatientPayment");
            myDatatable.Columns.Add("NegativePayment");
            myDatatable.Columns.Add("ChargeUnit");


            DataTable actual=null;
            DataTable myExpected=null;
            DataTable expected = new DataTable();
            DataTable actual1 = new DataTable();
            DataTable actual2 = new DataTable();
            //*************1st datatable***************************

            string stratdate = "01/01/2010"; // TODO: Initialize to an appropriate value

            string endDate = "02/28/2010"; // TODO: Initialize to an appropriate value

            string sProvider = null; // TODO: Initialize to an appropriate value

            string sFacility = null; // TODO: Initialize to an appropriate value

            actual1 = accessor.FillAgingReport(stratdate, endDate, sProvider, sFacility);
            if (actual1.Rows.Count > 0)
            {
                myDatatable.Rows.Add();
                counter = counter + 1;
                for (int i = 0; i <= actual1.Rows.Count - 1; i++)
                {

                    myDatatable.Rows[counter]["charges"] = Convert.ToDecimal((myDatatable.Rows[0]["charges"].ToString() == "" ? 0 : myDatatable.Rows[0]["charges"])) + Convert.ToDecimal((actual1.Rows[i]["charges"].ToString() == "" ? Convert.ToDecimal(0) : actual1.Rows[i]["charges"]));
                    myDatatable.Rows[counter]["WriteOff"] = Convert.ToDecimal((myDatatable.Rows[0]["WriteOff"].ToString() == "" ? 0 : myDatatable.Rows[0]["WriteOff"])) + Convert.ToDecimal((actual1.Rows[i]["WriteOff"].ToString() == "" ? 0 : actual1.Rows[i]["WriteOff"]));
                    myDatatable.Rows[counter]["Adjustment"] = Convert.ToDecimal((myDatatable.Rows[0]["Adjustment"].ToString() == "" ? 0 : myDatatable.Rows[0]["Adjustment"])) + Convert.ToDecimal((actual1.Rows[i]["Adjustment"].ToString() == "" ? 0 : actual1.Rows[i]["Adjustment"]));
                    myDatatable.Rows[counter]["InsurancePayment"] = Convert.ToDecimal((myDatatable.Rows[0]["InsurancePayment"].ToString() == "" ? 0 : myDatatable.Rows[0]["InsurancePayment"])) + Convert.ToDecimal((actual1.Rows[i]["InsurancePayment"].ToString() == "" ? 0 : actual1.Rows[i]["InsurancePayment"]));
                    myDatatable.Rows[counter]["PatientPayment"] = Convert.ToDecimal((myDatatable.Rows[0]["PatientPayment"].ToString() == "" ? 0 : myDatatable.Rows[0]["PatientPayment"])) + Convert.ToDecimal((actual1.Rows[i]["PatientPayment"].ToString() == "" ? 0 : actual1.Rows[i]["PatientPayment"]));
                    myDatatable.Rows[counter]["NegativePayment"] = Convert.ToDecimal((myDatatable.Rows[0]["NegativePayment"].ToString() == "" ? 0 : myDatatable.Rows[0]["NegativePayment"])) + Convert.ToDecimal((actual1.Rows[i]["NegativePayment"].ToString() == "" ? 0 : actual1.Rows[i]["NegativePayment"]));
                    myDatatable.Rows[counter]["ChargeUnit"] = Convert.ToInt32((myDatatable.Rows[0]["ChargeUnit"].ToString() == "" ? 0 : myDatatable.Rows[0]["ChargeUnit"])) + Convert.ToInt32((actual1.Rows[i]["ChargeUnit"].ToString() == "" ? 0 : actual1.Rows[i]["ChargeUnit"]));
                }
            }
            myDatatable.AcceptChanges();
            //*************2nd datatable***************************

            string stratdate1 = "03/01/2010"; // TODO: Initialize to an appropriate value

            string endDate1 = "04/30/2010"; // TODO: Initialize to an appropriate value

            string sProvider1 = null; // TODO: Initialize to an appropriate value

            string sFacility1 = null; // TODO: Initialize to an appropriate value

            actual2 = accessor.FillAgingReport(stratdate1, endDate1, sProvider1, sFacility1);
            if (actual2.Rows.Count > 0)
            {
                myDatatable.Rows.Add();
                counter = counter + 1;
                for (int i = 0; i <= actual2.Rows.Count - 1; i++)
                {
                    myDatatable.Rows[counter]["charges"] = Convert.ToDecimal((myDatatable.Rows[1]["charges"].ToString() == "" ? 0 : myDatatable.Rows[1]["charges"])) + Convert.ToDecimal((actual2.Rows[i]["charges"].ToString() == "" ? 0 : actual2.Rows[i]["charges"]));
                    myDatatable.Rows[counter]["WriteOff"] = Convert.ToDecimal((myDatatable.Rows[1]["WriteOff"].ToString() == "" ? 0 : myDatatable.Rows[1]["WriteOff"])) + Convert.ToDecimal((actual2.Rows[i]["WriteOff"].ToString() == "" ? 0 : actual2.Rows[i]["WriteOff"]));
                    myDatatable.Rows[counter]["Adjustment"] = Convert.ToDecimal((myDatatable.Rows[1]["Adjustment"].ToString() == "" ? 0 : myDatatable.Rows[1]["Adjustment"])) + Convert.ToDecimal((actual2.Rows[i]["Adjustment"].ToString() == "" ? 0 : actual2.Rows[i]["Adjustment"]));
                    myDatatable.Rows[counter]["InsurancePayment"] = Convert.ToDecimal((myDatatable.Rows[1]["InsurancePayment"].ToString() == "" ? 0 : myDatatable.Rows[1]["InsurancePayment"])) + Convert.ToDecimal((actual2.Rows[i]["InsurancePayment"].ToString() == "" ? 0 : actual2.Rows[i]["InsurancePayment"]));
                    myDatatable.Rows[counter]["PatientPayment"] = Convert.ToDecimal((myDatatable.Rows[1]["PatientPayment"].ToString() == "" ? 0 : myDatatable.Rows[1]["PatientPayment"])) + Convert.ToDecimal((actual2.Rows[i]["PatientPayment"].ToString() == "" ? 0 : actual2.Rows[i]["PatientPayment"]));
                    myDatatable.Rows[counter]["NegativePayment"] = Convert.ToDecimal((myDatatable.Rows[1]["NegativePayment"].ToString() == "" ? 0 : myDatatable.Rows[1]["NegativePayment"])) + Convert.ToDecimal((actual2.Rows[i]["NegativePayment"].ToString() == "" ? 0 : actual2.Rows[i]["NegativePayment"]));
                    myDatatable.Rows[counter]["ChargeUnit"] = Convert.ToInt32((myDatatable.Rows[1]["ChargeUnit"].ToString() == "" ? 0 : myDatatable.Rows[1]["ChargeUnit"])) + Convert.ToInt32((actual2.Rows[i]["ChargeUnit"].ToString() == "" ? 0 : actual2.Rows[i]["ChargeUnit"]));
                }
            }
            myDatatable.AcceptChanges();
            //*************Final datatable***************************
            if (myDatatable.Rows.Count > 0)
            {
                myDatatable.Rows.Add();
                counter = counter + 1;
                for (int i = 0; i <= myDatatable.Rows.Count - 1; i++)
                {
                    myDatatable.Rows[counter]["charges"] = Convert.ToDecimal((myDatatable.Rows[0]["charges"].ToString() == "" ? 0 : myDatatable.Rows[0]["charges"])) + Convert.ToDecimal((myDatatable.Rows[1]["charges"].ToString() == "" ? 0 : myDatatable.Rows[1]["charges"]));
                    myDatatable.Rows[counter]["WriteOff"] = Convert.ToDecimal((myDatatable.Rows[0]["WriteOff"].ToString() == "" ? 0 : myDatatable.Rows[0]["WriteOff"])) + Convert.ToDecimal((myDatatable.Rows[1]["WriteOff"].ToString() == "" ? 0 : myDatatable.Rows[1]["WriteOff"]));
                    myDatatable.Rows[counter]["Adjustment"] = Convert.ToDecimal((myDatatable.Rows[0]["Adjustment"].ToString() == "" ? 0 : myDatatable.Rows[0]["Adjustment"])) + Convert.ToDecimal((myDatatable.Rows[1]["Adjustment"].ToString() == "" ? 0 : myDatatable.Rows[1]["Adjustment"]));
                    myDatatable.Rows[counter]["InsurancePayment"] = Convert.ToDecimal((myDatatable.Rows[0]["InsurancePayment"].ToString() == "" ? 0 : myDatatable.Rows[0]["InsurancePayment"])) + Convert.ToDecimal((myDatatable.Rows[1]["InsurancePayment"].ToString() == "" ? 0 : myDatatable.Rows[1]["InsurancePayment"]));
                    myDatatable.Rows[counter]["PatientPayment"] = Convert.ToDecimal((myDatatable.Rows[0]["PatientPayment"].ToString() == "" ? 0 : myDatatable.Rows[0]["PatientPayment"])) + Convert.ToDecimal((myDatatable.Rows[1]["PatientPayment"].ToString() == "" ? 0 : myDatatable.Rows[1]["PatientPayment"]));
                    myDatatable.Rows[counter]["NegativePayment"] = Convert.ToDecimal((myDatatable.Rows[0]["NegativePayment"].ToString() == "" ? 0 : myDatatable.Rows[0]["NegativePayment"])) + Convert.ToDecimal((myDatatable.Rows[1]["NegativePayment"].ToString() == "" ? 0 : myDatatable.Rows[1]["NegativePayment"]));
                    myDatatable.Rows[counter]["ChargeUnit"] = Convert.ToInt32((myDatatable.Rows[0]["ChargeUnit"].ToString() == "" ? 0 : myDatatable.Rows[0]["ChargeUnit"])) + Convert.ToInt32((myDatatable.Rows[1]["ChargeUnit"].ToString() == "" ? 0 : myDatatable.Rows[1]["ChargeUnit"]));
                }
            }
            //if (myDatatable.Rows.Count == Convert.ToInt32(3))
            //{
            //    myDatatable.Rows[0].Delete();
            //    myDatatable.Rows[0].Delete();
            //    counter = counter - 2;
            //}
            //else if (myDatatable.Rows.Count == Convert.ToInt32(2))
            //{
            //    myDatatable.Rows[0].Delete();
            //    counter = counter - 1;
            //}
            string stratdate2 = "01/01/2010"; // TODO: Initialize to an appropriate value

            string endDate2 = "04/30/2010"; // TODO: Initialize to an appropriate value

            string sProvider2 = null; // TODO: Initialize to an appropriate value

            string sFacility2 = null; // TODO: Initialize to an appropriate value



            expected = accessor.FillAgingReport(stratdate2, endDate2, sProvider2, sFacility2);
            DataTable myDatatable1 = new DataTable();
            myDatatable1.Columns.Add("charges");
            myDatatable1.Columns.Add("WriteOff");
            myDatatable1.Columns.Add("Adjustment");
            myDatatable1.Columns.Add("InsurancePayment");
            myDatatable1.Columns.Add("PatientPayment");
            myDatatable1.Columns.Add("NegativePayment");
            myDatatable1.Columns.Add("ChargeUnit");
            if (expected.Rows.Count > 0)
            {
                myDatatable1.Rows.Add();
                //counter = counter + 1;
                //for (int i = 0; i <= expected.Rows.Count - 1; i++)
                //{

                //    myDatatable.Rows[counter]["charges"] = Convert.ToDecimal((myDatatable.Rows[3]["charges"].ToString() == "" ? 0 : myDatatable.Rows[3]["charges"])) + Convert.ToDecimal((expected.Rows[i]["charges"].ToString() == "" ? 0 : expected.Rows[i]["charges"]));
                //    myDatatable.Rows[counter]["WriteOff"] = Convert.ToDecimal((myDatatable.Rows[3]["WriteOff"].ToString() == "" ? 0 : myDatatable.Rows[3]["WriteOff"])) + Convert.ToDecimal((expected.Rows[i]["WriteOff"].ToString() == "" ? 0 : expected.Rows[i]["WriteOff"]));
                //    myDatatable.Rows[counter]["Adjustment"] = Convert.ToDecimal((myDatatable.Rows[3]["Adjustment"].ToString() == "" ? 0 : myDatatable.Rows[3]["Adjustment"])) + Convert.ToDecimal((expected.Rows[i]["Adjustment"].ToString() == "" ? 0 : expected.Rows[i]["Adjustment"]));
                //    myDatatable.Rows[counter]["InsurancePayment"] = Convert.ToDecimal((myDatatable.Rows[3]["InsurancePayment"].ToString() == "" ? 0 : myDatatable.Rows[3]["InsurancePayment"])) + Convert.ToDecimal((expected.Rows[i]["InsurancePayment"].ToString() == "" ? 0 : expected.Rows[i]["InsurancePayment"]));
                //    myDatatable.Rows[counter]["PatientPayment"] = Convert.ToDecimal((myDatatable.Rows[3]["PatientPayment"].ToString() == "" ? 0 : myDatatable.Rows[3]["PatientPayment"])) + Convert.ToDecimal((expected.Rows[i]["PatientPayment"].ToString() == "" ? 0 : expected.Rows[i]["PatientPayment"]));
                //    myDatatable.Rows[counter]["NegativePayment"] = Convert.ToDecimal((myDatatable.Rows[3]["NegativePayment"].ToString() == "" ? 0 : myDatatable.Rows[3]["NegativePayment"])) + Convert.ToDecimal((expected.Rows[i]["NegativePayment"].ToString() == "" ? 0 : expected.Rows[i]["NegativePayment"]));
                //    myDatatable.Rows[counter]["ChargeUnit"] = Convert.ToInt32((myDatatable.Rows[3]["ChargeUnit"].ToString() == "" ? 0 : myDatatable.Rows[3]["ChargeUnit"])) + Convert.ToInt32((expected.Rows[i]["ChargeUnit"].ToString() == "" ? 0 : expected.Rows[i]["ChargeUnit"]));
                //}
               

                for (int i = 0; i <= expected.Rows.Count - 1; i++)
                {

                    myDatatable1.Rows[0]["charges"] = Convert.ToDecimal((myDatatable1.Rows[0]["charges"].ToString() == "" ? 0 : myDatatable1.Rows[0]["charges"])) + Convert.ToDecimal((expected.Rows[i]["charges"].ToString() == "" ? 0 : expected.Rows[i]["charges"]));
                    myDatatable1.Rows[0]["WriteOff"] = Convert.ToDecimal((myDatatable1.Rows[0]["WriteOff"].ToString() == "" ? 0 : myDatatable1.Rows[0]["WriteOff"])) + Convert.ToDecimal((expected.Rows[i]["WriteOff"].ToString() == "" ? 0 : expected.Rows[i]["WriteOff"]));
                    myDatatable1.Rows[0]["Adjustment"] = Convert.ToDecimal((myDatatable1.Rows[0]["Adjustment"].ToString() == "" ? 0 : myDatatable1.Rows[0]["Adjustment"])) + Convert.ToDecimal((expected.Rows[i]["Adjustment"].ToString() == "" ? 0 : expected.Rows[i]["Adjustment"]));
                    myDatatable1.Rows[0]["InsurancePayment"] = Convert.ToDecimal((myDatatable1.Rows[0]["InsurancePayment"].ToString() == "" ? 0 : myDatatable1.Rows[0]["InsurancePayment"])) + Convert.ToDecimal((expected.Rows[i]["InsurancePayment"].ToString() == "" ? 0 : expected.Rows[i]["InsurancePayment"]));
                    myDatatable1.Rows[0]["PatientPayment"] = Convert.ToDecimal((myDatatable1.Rows[0]["PatientPayment"].ToString() == "" ? 0 : myDatatable1.Rows[0]["PatientPayment"])) + Convert.ToDecimal((expected.Rows[i]["PatientPayment"].ToString() == "" ? 0 : expected.Rows[i]["PatientPayment"]));
                    myDatatable1.Rows[0]["NegativePayment"] = Convert.ToDecimal((myDatatable1.Rows[0]["NegativePayment"].ToString() == "" ? 0 : myDatatable1.Rows[0]["NegativePayment"])) + Convert.ToDecimal((expected.Rows[i]["NegativePayment"].ToString() == "" ? 0 : expected.Rows[i]["NegativePayment"]));
                    myDatatable1.Rows[0]["ChargeUnit"] = Convert.ToInt32((myDatatable1.Rows[0]["ChargeUnit"].ToString() == "" ? 0 : myDatatable1.Rows[0]["ChargeUnit"])) + Convert.ToInt32((expected.Rows[i]["ChargeUnit"].ToString() == "" ? 0 : expected.Rows[i]["ChargeUnit"]));
                }
                //myDatatable1.Rows.Add();
                //    myDatatable1.Rows[1]["charges"] = Convert.ToDecimal(0);
                //    myDatatable1.Rows[1]["WriteOff"] = Convert.ToDecimal(0);
                //    myDatatable1.Rows[1]["Adjustment"] = Convert.ToDecimal(0);
                //    myDatatable1.Rows[1]["InsurancePayment"] =Convert.ToDecimal(0);
                //    myDatatable1.Rows[1]["PatientPayment"] =Convert.ToDecimal(0);
                //    myDatatable1.Rows[1]["NegativePayment"] =Convert.ToDecimal(0);
                //    myDatatable1.Rows[1]["ChargeUnit"] = Convert.ToDecimal(0);
                
            }

            if (myDatatable.Rows.Count > 0)
            {
                for (int i = 0; i<=myDatatable.Columns.Count - 1; i++)
                {
                    if (Convert.ToDecimal(myDatatable.Rows[myDatatable.Rows.Count - 1].ItemArray[i]) != Convert.ToDecimal(myDatatable1.Rows[myDatatable1.Rows.Count - 1].ItemArray[i]))
                    {
                        actual = new DataTable();
                        actual.Rows.Add();
                    }
                }
            }


          

            Assert.AreEqual(myExpected, actual, "gloReports.frmRpt_FinancialSummary.FillAgingReport did not return the expected value.");
                    
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }


}