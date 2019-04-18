using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gloUIControlLibrary.Classes.ClaimRules;

namespace ChargeRules
{
    //public enum RuleType
    //{ 
    //    Warning = 1,
    //    Information =2,
    //    Error = 3,
    //    None = 0
    //}

    public class RuleCondition : ICloneable
    {
        private bool propertySet = false;
        public string PropertyName { get; set; }
        public Operator Operator { get; set; }
        public object Value { get; set; }        
        public string ProcessingRule { get; set; }
        public bool ConditionResult { get; set; }
        public int ConditionIndex { get; set; }
        public string Version { get; set; }
        public string Predicate { get; set; }

        public Int32 PropertyID { get; set; }
        public string PropertyDisplayName { get; set; }
        public string OperatorDisplayText { get; set; }

        public string sValue { get; set; }

        public RuleCondition(string propertyName, Operator operator_, object value)
            : this()
        {
            this.Operator = operator_;
            this.Value = value;
            this.PropertyName = propertyName;
            this.ConditionResult = false;
            if (!string.IsNullOrEmpty(propertyName))
                this.propertySet = true;
        }

        public RuleCondition(string propertyName, Operator operator_, object value, int index)
            : this()
        {
            this.Operator = operator_;
            this.Value = value;
            this.PropertyName = propertyName;
            this.ConditionResult = false;
            this.ConditionIndex = index;
            if (!string.IsNullOrEmpty(propertyName))
                this.propertySet = true;
        }

        public RuleCondition(string propertyName, Operator operator_, object value, int index, string predicate)
            : this()
        {
            this.Operator = operator_;
            this.Value = value;
            this.PropertyName = propertyName;
            this.ConditionResult = false;
            this.ConditionIndex = index;
            if (!string.IsNullOrEmpty(propertyName))
                this.propertySet = true;
            this.Predicate = predicate;
        }

        public RuleCondition()
        {
            this.PropertyName = "";
            this.Operator = ChargeRules.Operator.Equal;
            this.Value = null;
            this.ProcessingRule = "";
            this.ConditionResult = false;
            this.ConditionIndex = 0;
            this.Version = "";
            this.Predicate = "";
            this.PropertyID = 0;
            this.PropertyDisplayName = "";
            this.OperatorDisplayText = "";
            this.sValue = "";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Rule : IDisposable, ICloneable
    {
        #region "Constructor, Distructor and Dispose"

        public Rule()
        { 
            RuleConditions = new List<RuleCondition>();
            this.RuleId = 0;
            this.RuleName = "";
            this.RuleMessage = "";
            this.RuleTypeInfo = RuleType.None;
            this.RuleExpression = "";
            this.RuleDescription = "";
            this.EvaluationLogic = 0;
            this.Version = "";
            this.RuleCategory = "";
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Rule()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //Release all the managed resources
                if (this.RuleConditions != null)
                { this.RuleConditions.Clear(); this.RuleConditions = null; }
            }
        }  

        #endregion "Constructor, Distructor and Dispose"

        public Int64 RuleId { get; set; }
        public string RuleName { get; set; }
        public string RuleMessage { get; set; }
        public RuleType RuleTypeInfo { get; set; }
        public List<RuleCondition> RuleConditions { get; set; }
        public string RuleExpression { get; set; }
        public string RuleDescription { get; set; }
        public Int32 EvaluationLogic { get; set; }
        public string Version { get; set; }
        public bool AndOnlyRuleConditions { get; set; }
        public bool OrOnlyRuleConditions { get; set; }
        public string RuleCategory { get; set; }

        public void ResetConditionResults()
        {
            foreach (RuleCondition condition in this.RuleConditions)
            {
                if (condition.ConditionResult) { condition.ConditionResult = false; }
            }
        }


        public object Clone()
        {
            Rule cloneRule = (Rule)this.MemberwiseClone();
            foreach (RuleCondition rc in this.RuleConditions)
            {
                cloneRule.RuleConditions.Add((RuleCondition)rc.Clone());
            }
            return cloneRule;
        }
    }
}
