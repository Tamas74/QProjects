using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace gloCommunity.Classes
{
    class ClsCardioVascularComm : IDisposable
    {
        // To detect redundant calls
        private bool disposedValue = false;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO:  free managed resources when explicitly called
                }

                // TODO: free shared unmanaged resources
            }
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        private System.DateTime _dtproceduredate;
        private long _nCatheterizationID = 0;
        private string _sCPTCode = "";
        private string _sPhysicianName = "";
        private long _nGroupID = 0;
        private Int64 _nPatientID = 0;
        private Int64 _nVisitID = 0;
        private Int64 _nClinicID = 0;
        private Int64 _nexamid = 0;
        private string _sPeak = "";
        private string _sTestType = "";
        private string _sProcedures = "";
        private string _sInterventionType = "";
        private string _SRaPressure = "";
        private string _sLaPressure = "";
        private string _sWaveMean = "";
        private string _sRPulmonary = "";
        private string _sLPulmonary = "";
        private string _sWedge = "";
        private string _sRV = "";
        private string _sLV = "";
        private string _sEarlyDiastolic = "";
        private string _sEndDiastolic = "";
        private string _sDiastolic = "";
        private string _sMean = "";
        private string _sPaPressure = "";
        private string _sAoPressure = "";
        private string _sIVc = "";
        private string _sSvc = "";
        private string _sRASaturations = "";
        private string _sRVSAturations = "";
        private string _sLASaturations = "";
        private string _sPASaturations = "";
        private string _sLVSaturations = "";
        private string _sAortic = "";
        private string _sCardiacIndex = "";
        private string _sCardiacOutput = "";
        private string _sAvo2difference = "";
        private string _sshuntFraction = "";
        private string _sLVEjectionFraction = "";
        private string _sLVDiastolicVol = "";
        private string _sLVSystolicVol = "";
        private string _sRVEjectionFraction = "";
        private string _sRVSystolicVol = "";
        private string _sRVDiostolicVol = "";

        private string _sNarrativeSummary = "";
        public System.DateTime dtproceduredate
        {
            get { return _dtproceduredate; }
            set { _dtproceduredate = value; }
        }

        public long nCatheterizationID
        {
            get { return _nCatheterizationID; }
            set { _nCatheterizationID = value; }
        }

        public long nGroupID
        {
            get { return _nGroupID; }
            set { _nGroupID = value; }
        }

        public string sCPTCode
        {
            get { return _sCPTCode; }
            set { _sCPTCode = value; }
        }

        public string sPhysicianName
        {
            get { return _sPhysicianName; }
            set { _sPhysicianName = value; }
        }

        public Int64 nPatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public Int64 nexamid
        {
            get { return _nexamid; }
            set { _nexamid = value; }
        }

        public Int64 nVisitID
        {
            get { return _nVisitID; }
            set { _nVisitID = value; }
        }

        public Int64 nClinicID
        {
            get { return _nClinicID; }
            set { _nClinicID = value; }
        }


        public string sPeak
        {
            get { return _sPeak; }
            set { _sPeak = value; }
        }

        public string sTestType
        {
            get { return _sTestType; }
            set { _sTestType = value; }
        }

        public string sProcedures
        {
            get { return _sProcedures; }
            set { _sProcedures = value; }
        }

        public string sInterventionType
        {
            get { return _sInterventionType; }
            set { _sInterventionType = value; }
        }

        public string sRaPressure
        {
            get { return _SRaPressure; }
            set { _SRaPressure = value; }
        }

        public string sLaPressure
        {
            get { return _sLaPressure; }
            set { _sLaPressure = value; }
        }


        public string sWaveMean
        {
            get { return _sWaveMean; }
            set { _sWaveMean = value; }
        }


        public string sRPulmonary
        {
            get { return _sRPulmonary; }
            set { _sRPulmonary = value; }
        }

        public string sLPulmonary
        {
            get { return _sLPulmonary; }
            set { _sLPulmonary = value; }
        }

        public string sWedge
        {
            get { return _sWedge; }
            set { _sWedge = value; }
        }


        public string sRV
        {
            get { return _sRV; }
            set { _sRV = value; }
        }

        public string sLV
        {
            get { return _sLV; }
            set { _sLV = value; }
        }

        public string sEarlyDiastolic
        {
            get { return _sEarlyDiastolic; }
            set { _sEarlyDiastolic = value; }
        }

        public string sEndDiastolic
        {
            get { return _sEndDiastolic; }
            set { _sEndDiastolic = value; }
        }

        public string sDiastolic
        {
            get { return _sDiastolic; }
            set { _sDiastolic = value; }
        }

        public string sMean
        {
            get { return _sMean; }
            set { _sMean = value; }
        }

        public string sPaPressure
        {
            get { return _sPaPressure; }
            set { _sPaPressure = value; }
        }


        public string sAoPressure
        {
            get { return _sAoPressure; }
            set { _sAoPressure = value; }
        }

        public string sIVc
        {
            get { return _sIVc; }
            set { _sIVc = value; }
        }

        public string sSvc
        {
            get { return _sSvc; }
            set { _sSvc = value; }
        }

        public string sRASaturations
        {
            get { return _sRASaturations; }
            set { _sRASaturations = value; }
        }

        public string sRVSAturations
        {
            get { return _sRVSAturations; }
            set { _sRVSAturations = value; }
        }

        public string sLASaturations
        {
            get { return _sLASaturations; }
            set { _sLASaturations = value; }
        }


        public string sPASaturations
        {
            get { return _sPASaturations; }
            set { _sPASaturations = value; }
        }

        public string sLVSaturations
        {
            get { return _sLVSaturations; }
            set { _sLVSaturations = value; }
        }


        public string sAortic
        {
            get { return _sAortic; }
            set { _sAortic = value; }
        }

        public string sCardiacIndex
        {
            get { return _sCardiacIndex; }
            set { _sCardiacIndex = value; }
        }


        public string sCardiacOutput
        {
            get { return _sCardiacOutput; }
            set { _sCardiacOutput = value; }
        }

        public string sAvo2difference
        {
            get { return _sAvo2difference; }
            set { _sAvo2difference = value; }
        }

        public string sshuntFraction
        {
            get { return _sshuntFraction; }
            set { _sshuntFraction = value; }
        }


        public string sLVEjectionFraction
        {
            get { return _sLVEjectionFraction; }
            set { _sLVEjectionFraction = value; }
        }

        public string sLVDiastolicVol
        {
            get { return _sLVDiastolicVol; }
            set { _sLVDiastolicVol = value; }
        }

        public string sLVSystolicVol
        {
            get { return _sLVSystolicVol; }
            set { _sLVSystolicVol = value; }
        }


        public string sRVEjectionFraction
        {
            get { return _sRVEjectionFraction; }
            set { _sRVEjectionFraction = value; }
        }

        public string sRVSystolicVol
        {
            get { return _sRVSystolicVol; }
            set { _sRVSystolicVol = value; }
        }


        public string sRVDiostolicVol
        {
            get { return _sRVDiostolicVol; }
            set { _sRVDiostolicVol = value; }
        }

        public string sNarrativeSummary
        {
            get { return _sNarrativeSummary; }
            set { _sNarrativeSummary = value; }
        }


    }


    public class Cls_CardioVasculars : CollectionBase, IDisposable
    {

        // To detect redundant calls
        private bool disposedValue = false;

        //Remove Item at specified index
        public void Remove(int index)
        {
            // Check to see if there is a widget at the supplied index.
            if (index > Count - 1 | index < 0)
            {
                // If no object exists, a messagebox is shown and the operation is 
                // cancelled.
                //System.Windows.Forms.MessageBox.Show("Index not valid!")
            }
            else
            {
                // Invokes the RemoveAt method of the List object.
                List.RemoveAt(index);
            }
        }
        // This line declares the Item property as ReadOnly, and 
        // declares that it will return a SentFax object.
        //public ClsCardioVascularComm Item(int index)
        //{
        //    // The appropriate item is retrieved from the List object and 
        //    // explicitly cast to the SentFax type, then returned to the 
        //    // caller.
        //    get{return ((ClsCardioVascularComm)List[index]);}
        //}
        // Restricts to SentFax types, items that can be added to the collection.
        //public void Add(ClsCardioVascularComm oConditionfield)
        //{
        //    // Invokes Add method of the List object to add a SentFax.
        //    List.Add(oConditionfield);
        //}
        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: free managed resources when explicitly called
                }

                // TODO: free shared unmanaged resources
            }
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        //protected override void Finalize()
        //{
        //    base.Finalize();
        //}

        public Cls_CardioVasculars()
            : base()
        {
        }

        public void SetListBoxToolTip(ListBox LstBox, C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1, Point Position)
        {
            /// ''''''''''''' by Ujwala Atre to give tooltip to Listbox
            Point MousePositionInClientCoords = LstBox.PointToClient(Position);
            int indexUnderTheMouse = LstBox.IndexFromPoint(MousePositionInClientCoords);
            if (indexUnderTheMouse > -1)
            {
                string s = LstBox.Items[indexUnderTheMouse].ToString();
                Graphics g = LstBox.CreateGraphics();
                if (g.MeasureString(s, LstBox.Font).Width > LstBox.ClientRectangle.Width)
                {
                    C1SuperTooltip1.SetToolTip(LstBox, s);
                }
                else
                {
                    C1SuperTooltip1.SetToolTip(LstBox, "");
                }
                g.Dispose();
            }
            /// ''''''''''''' by Ujwala Atre to give tooltip to Listbox
        }

        public void ExpandAll(C1.Win.C1FlexGrid.C1FlexGrid C1Grd)
        {
            /// ''''''''''''' by Ujwala Atre to Expand All Nodes in c1tree - as on 20101029
            int i = 0;
            try
            {
                for (i = 0; i <= C1Grd.Rows.Count - 1; i++)
                {
                    C1.Win.C1FlexGrid.Node nd = C1Grd.Rows[i].Node;
                    if ((nd != null))
                    {
                        if (nd.Level == 0)
                        {
                            nd.Expanded = true;
                        }
                    }
                }

            }
            catch //(Exception ex)
            {
            }
            /// ''''''''''''' by Ujwala Atre to Expand All Nodes in c1tree - as on 20101029
        }
    }
}
