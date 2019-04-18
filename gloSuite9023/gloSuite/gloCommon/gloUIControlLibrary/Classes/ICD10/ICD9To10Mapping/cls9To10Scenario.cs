using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloUIControlLibrary.Classes.ICD10.ICD9To10Mapping
{
    public class ICD9To10MappingContainer : IDisposable
    {
        public string ICD9Code { get; set; }
        public List<ICDMappingScenario> ScenarioList { get; set; }

        public ICD9To10MappingContainer()
        { this.ScenarioList = new List<ICDMappingScenario>(); }

        public void GenerateStructure(List<ICD9To10Mapping> ListOfMappings)
        {
            if (ListOfMappings.Count > 0)
            { 
                int totalScenarioCount = ListOfMappings.Max(x => x.Scenario);
                try
                {
                                
                   for (int i = 0; i <= totalScenarioCount; i++)
                    {
                        ICDMappingScenario ToAddScenario = new ICDMappingScenario(i);
                        foreach (ICD9To10Mapping element in ListOfMappings.Where(p => p.Scenario == i))
                        {

                            if (element.Scenario == ToAddScenario.ScenarioID)
                            {
                                ICDMappingOr ORObject = new ICDMappingOr(element.choiceList);
                                if (!ToAddScenario.ORNodeList.Exists(p => p.UniqueID == element.choiceList))
                                {
                                    ToAddScenario.ORNodeList.Add(ORObject);
                                    ORObject.MyScenario = ToAddScenario;
                                }
                                ORObject = null;
                            }

                        }
                        if (ToAddScenario.ORNodeList.Count != 0)
                        { this.ScenarioList.Add(ToAddScenario); }
                        else
                        { ToAddScenario.Dispose(); }

                        ToAddScenario = null;
                  
                    }
                
                    foreach (ICDMappingScenario ScenarioItem in this.ScenarioList)
                    {
                        foreach (ICDMappingOr ORitem in ScenarioItem.ORNodeList)
                        {
                            foreach (ICD9To10Mapping codeItem in ListOfMappings.Where(p => p.Scenario == ScenarioItem.ScenarioID && p.choiceList == ORitem.UniqueID))
                            {                                                    
                                if (!ORitem.iCodeList.Exists(p => p.ICD10code == codeItem.ICD10code))
                                {
                                    ORitem.iCodeList.Add(codeItem);
                                    codeItem.MyOR = ORitem;
                                }                          
                            }
                        }

                    }

                }
                catch (Exception Ex)
                { LogException.ExceptionLog(Ex.ToString(), true); }
            }
        }

        public void Dispose()
        {
            if (this.ScenarioList != null)
            {
                foreach (ICDMappingScenario element in this.ScenarioList)
                { element.Dispose(); }

                this.ScenarioList.Clear();
                this.ScenarioList = null;
            }
        }
    }

    public class ICDMappingScenario : IDisposable
    {
        #region Properties
        public int ScenarioID { get; set; }        
        public List<ICDMappingOr> ORNodeList { get; set; }
        #endregion

        #region Constructor
        public ICDMappingScenario(int ScenarioID)
        {
            this.ScenarioID = ScenarioID;
            ORNodeList = new List<ICDMappingOr>();
        }
        #endregion

        #region Functions
        public bool IsOrFirst(ICDMappingOr OrObject)
        {
            bool returned = false;

            ICDMappingOr or = this.ORNodeList.FirstOrDefault(p => p.UniqueID == OrObject.UniqueID);

            if (or != null)
            {
                if (ORNodeList.IndexOf(or) == 0)
                { returned = true; }
            }

            return returned;
        }
        #endregion

        #region Disposal
        public void Dispose()
        {
            if (this.ORNodeList != null)
            {
                foreach (ICDMappingOr element in this.ORNodeList)
                { element.Dispose(); }

                this.ORNodeList.Clear();
                this.ORNodeList = null;
            }
        } 
        #endregion
    }

    public class ICDMappingOr : IDisposable
    {
        #region Properties
        public int UniqueID { get; set; }
        public ICDMappingScenario MyScenario { get; set; }        
        public List<ICD9To10Mapping> iCodeList { get; set; }     
        public bool IsFirst
        { get { return MyScenario.IsOrFirst(this); } }
        #endregion

        #region Constructor
        public ICDMappingOr(int UniqueID)
        {
            this.UniqueID = UniqueID;
            iCodeList = new List<ICD9To10Mapping>();
        }
        #endregion

        #region Functions
        public bool IsICDFirst(ICD9To10Mapping ICDCode)
        {
            bool returned = false;

            ICD9To10Mapping or = this.iCodeList.FirstOrDefault(p => p.ICD10code == ICDCode.ICD10code);

            if (or != null)
            {
                if (iCodeList.IndexOf(or) == 0)
                { returned = true; }
            }

            return returned;
        }
        #endregion

        #region Disposal
        public void Dispose()
        {
            if (this.iCodeList != null)
            {
                foreach (ICD9To10Mapping element in this.iCodeList)
                { element.Dispose(); }

                this.iCodeList.Clear();
                this.iCodeList = null;
            }

            this.MyScenario = null;
        }
        #endregion
    }

    public class ICD9To10Mapping : IDisposable
    {
        #region Properties
        public string ICD9code { get; set; }
        public string ICD10code { get; set; }        
        public string Description { get; set; }

        public string Flag { get; set; }
        public string ICD9Decimalcode { get; set; }
        public string Approximate { get; set; }

        public int NoMap { get; set; }
        public int Combination { get; set; }
        public int Scenario { get; set; }

        public int choiceList { get; set; }
        public string DisplayRepresentation
        { get { return this.ICD10CodeWithDecimal + " " + this.Description; } }
        public string ICD10CodeWithDecimal
        {
            get
            {
                if (this.ICD10code.Length >= 4 && !this.ICD10code.Contains("."))
                { return this.ICD10code.Insert(3, "."); }
                else
                { return this.ICD10code; }
            }
        }

        public ICDMappingOr MyOR { get; set; }

        public bool IsICDFirst
        { 
            get             
            {
                if (this.MyOR != null)
                { return this.MyOR.IsICDFirst(this); }
                else
                { return false; }
                
            } 
        }
        #endregion

        #region Disposal
        public void Dispose() { this.MyOR = null; }
        #endregion
    }
}
