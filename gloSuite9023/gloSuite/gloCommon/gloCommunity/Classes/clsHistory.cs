using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace gloCommunity.Classes
{
   public class clsHistory
    {
        //public Histories oHistories;
        public DataTable FetchData(long CategoryId)
        {
            DataBaseLayer oDB = new DataBaseLayer();
            DBParameter oParamater = default(DBParameter);
            DataTable oResultTable = new DataTable();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nCategoryId";
                oParamater.Value = CategoryId;// "Template";
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oResultTable = oDB.GetDataTable("GC_viewHistory_MST");

                if ((oResultTable != null))
                {
                    return oResultTable;
                }
                return oResultTable;
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120104
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in FetchData of clsHistory "  + "\n" + ex.Message.ToString(), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oResultTable;
            }
            finally
            {
                oDB.Dispose();
                oDB = null;
            }
        }

        public long InsertHistory(string sCatDescription, string sHistoryDescription, string sComments, string sCategoryType, string sConceptID, string sSnomedDescription, string sSnomedDefination, string sCPT, string sHistoryType, string sICD9)
        {
            long Id = 0;

            DBParameter oParamater = default(DBParameter);
            DataBaseLayer oDB = new DataBaseLayer();
            try
            {
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCatDescription";
                oParamater.Value = sCatDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sHistoryDescription";
                oParamater.Value = sHistoryDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sComments";
                oParamater.Value = sComments;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCategoryType";
                oParamater.Value = sCategoryType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.Decimal;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@nClinicID";
                oParamater.Value = clsGeneral.gClinicID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sConceptID";
                oParamater.Value = sConceptID;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sSnomedDescription";
                oParamater.Value = sSnomedDescription;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sSnomedDefination";
                oParamater.Value = sSnomedDefination;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                //Added new cloumn in History master on 20121003
                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sICD9";
                oParamater.Value = sICD9;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sCPT";
                oParamater.Value = sCPT;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;

                oParamater = new DBParameter();
                oParamater.DataType = SqlDbType.VarChar;
                oParamater.Direction = ParameterDirection.Input;
                oParamater.Name = "@sHistoryType";
                oParamater.Value = sHistoryType;
                oDB.DBParametersCol.Add(oParamater);
                oParamater = null;
                //End Added new cloumn in History master

                string strQuery = "GC_CategoryHistory_Insert";
                Id = Convert.ToInt64(oDB.GetDataValue(strQuery));
                return Id;
            }
            catch
            {
                //commented by kanchan on 20120104
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.None, "gloCommunity:-Error in FetchData of clsHistory " + "\n" + Id.ToString (), 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR); 
                return Id;
            }
        }

        public class History : IDisposable
        {
            #region "Constructor & Distructor"

            public History()
            {
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~History()
            {
                Dispose(false);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }
            #endregion

            #region "Private Variables"

            private string _sCategoryName = "";
            private string _sType = "";
            private string _sDescription = "";
            private string _sComments = "";
            private string _sConceptID = "";
            private string _sDescriptionID = "";
            private string _sSnoMedID = "";
            private string _sSnomedDescription = "";
            private string _sTranID1 = "";
            private string _sTranID2 = "";
            private string _sTranID3 = "";
            private string _sICD9 = "";
            private string _sSnomedDefination = "";
            private bool _Select = false;
            private string _sSpecialty = "";
            private string _sCPTCode = "";
            private string _sHistoryType = "";
            #endregion

            #region "Property Procedures"

            //1.CategoryName
            public string CategoryName
            {
                get { return _sCategoryName; }
                set { _sCategoryName = value; }
            }

            //2.Type
            public string Type
            {
                get { return _sType; }
                set { _sType = value; }
            }

            //3.Description
            public string Description
            {
                get { return _sDescription; }
                set { _sDescription = value; }
            }

            //4.Comments
            public string Comments
            {
                get { return _sComments; }
                set { _sComments = value; }
            }

            //5.ConceptID
            public string ConceptID
            {
                get { return _sConceptID; }
                set { _sConceptID = value; }
            }

            //6.DescriptionID
            public string DescriptionID
            {
                get { return _sDescriptionID; }
                set { _sDescriptionID = value; }
            }

            //7.SnoMedID
            public string SnoMedID
            {
                get { return _sSnoMedID; }
                set { _sSnoMedID = value; }
            }

            //8.SnomedDescription
            public string SnomedDescription
            {
                get { return _sSnomedDescription; }
                set { _sSnomedDescription = value; }
            }

            //9.TranID1
            public string TranID1
            {
                get { return _sTranID1; }
                set { _sTranID1 = value; }
            }

            //10.TranID2
            public string TranID2
            {
                get { return _sTranID2; }
                set { _sTranID2 = value; }
            }

            //11.TranID3
            public string TranID3
            {
                get { return _sTranID3; }
                set { _sTranID3 = value; }
            }

            //12.ICD9
            public string ICD9
            {
                get { return _sICD9; }
                set { _sICD9 = value; }
            }

            //13.SnomedDefination
            public string SnomedDefination
            {
                get { return _sSnomedDefination; }
                set { _sSnomedDefination = value; }
            }

            //14.Select
            public bool Select
            {
                get { return _Select; }
                set { _Select = value; }
            }

            //15.Specialty
            public string Specialty
            {
                get { return _sSpecialty; }
                set { _sSpecialty = value; }
            }

            //Added new cloumn in History master on 20121003
            //16.CPTCode
            public string CPTCode
            {
                get { return _sCPTCode; }
                set { _sCPTCode = value; }
            }

            //17.HistoryType
            public string HistoryType
            {
                get { return _sHistoryType; }
                set { _sHistoryType = value; }
            }
            //End Added new cloumn in History master.
            #endregion
        }

        public class Histories : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public Histories()
            {
                _innerlist = new ArrayList();

            }

            private bool disposed = false;

            public void Dispose()
            {

                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }


            ~Histories()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(History item)
            {
                _innerlist.Add(item);
            }

     
            public bool Remove(History item)
            {
                bool result = false;
               

                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public History this[int index]
            {
                get
                { return (History)_innerlist[index]; }
            }

            public bool Contains(History item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(History item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(History[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }
    }


}
