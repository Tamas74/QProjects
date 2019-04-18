using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using gloUIControlLibrary.Classes;

namespace ChargeRules
{
    public class BusinessOperation : IDisposable
    {
        #region "Constructor, Distructor and Dispose"

        public BusinessOperation()
        { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BusinessOperation()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //Release all the managed resources
            }
        }  

        #endregion "Constructor, Distructor and Dispose"

        public List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> EvaluateRules(List<Claim> listClaim, bool UseParallelProcessing = false, bool IsServiceCall=false)
        {
            List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> triggeredRules = null;
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            string ExecutionDetails = string.Empty;

            try
            {
                triggeredRules = new List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo>();

                if (UseParallelProcessing)
                {
                    stopWatch.Start();
                    triggeredRules = ParallelEvaluationOfRules(listClaim,null,IsServiceCall);
                    stopWatch.Stop();
                }
                else
                {
                    stopWatch.Start();
                    triggeredRules = SequentialEvaluationRules(listClaim,null,IsServiceCall);
                    stopWatch.Stop();
                }

                ExecutionDetails = "ForEach time in milliseconds: " + stopWatch.ElapsedMilliseconds.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stopWatch != null) { stopWatch = null; }
                ExecutionDetails = null;
            }

            return triggeredRules;
        }

        public List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> TestRules(List<Claim> listClaim, Int64 ruleId = 0, bool UseParallelProcessing = false)
        {
            List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> triggeredRules = null;
            List<ChargeRules.Rule> listRules = null;
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            string ExecutionDetails = string.Empty;
            ClsRuleEngine objClsRuleEngine = null;
            BusinessObjects businessObjects = null;
            DataSet dsRules = null;

            try
            {
                triggeredRules = new List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo>();
                businessObjects = new BusinessObjects();
                objClsRuleEngine = new ClsRuleEngine();

                if (ruleId > 0)
                { dsRules = objClsRuleEngine.GetRules(ruleId, true); }
                else
                { dsRules = objClsRuleEngine.GetRules(0, true); }

                listRules = businessObjects.GetRules(dsRules); 

                if (businessObjects != null) { businessObjects.Dispose(); businessObjects = null; }

                if (UseParallelProcessing)
                {
                    stopWatch.Start();
                    triggeredRules = ParallelEvaluationOfRules(listClaim,listRules);
                    stopWatch.Stop();
                }
                else
                {
                    stopWatch.Start();
                    triggeredRules = SequentialEvaluationRules(listClaim,listRules);
                    stopWatch.Stop();
                }

                ExecutionDetails = "ForEach time in milliseconds: " + stopWatch.ElapsedMilliseconds.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(objClsRuleEngine != null) {objClsRuleEngine.Dispose(); objClsRuleEngine = null;}
                if (listRules != null) { listRules.Clear(); listRules = null; }
                if (dsRules != null)
                { if (dsRules.Tables != null) { dsRules.Tables.Clear(); } dsRules.Dispose(); dsRules = null; }
                if (stopWatch != null) { stopWatch = null; }
                ExecutionDetails = null;
            }

            return triggeredRules;
        }

        private List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> ParallelEvaluationOfRules(List<Claim> listClaim, List<ChargeRules.Rule> listInputRule = null, bool ServiceCall = false)
        {
            List<ChargeRules.Rule> listRules = null;
            //RuleEngine ruleEngine = null;
            string finalExpression = String.Empty;
            bool finalExpressionResult = false;
            List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> triggeredRules = null;

            BusinessObjects businessObject = null;
            bool isRuleEvaluationFailed = false;
            string ruleFailureDetails = string.Empty;
            string sAUSID = string.Empty;
            string sRuleSource = string.Empty;
            try
            {
                businessObject = new BusinessObjects();
                if (ServiceCall)
                {
                    sAUSID = listClaim[0].ClaimFacility.AUSID;
                    sRuleSource = "Global";
                }
                if (listInputRule != null)
                { listRules = listInputRule; }
                else
                { listRules = ChargeRules.RulesRepository.GetRules(ServiceCall, sAUSID); }

                if (listRules != null && listRules.Count > 0 && listClaim != null && listClaim.Count > 0)
                {
                    triggeredRules = new List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo>();

                    //ruleEngine = new RuleEngine();

                    #region "Using Parallel ForEach"
                    
                    System.Threading.Tasks.Parallel.ForEach(listRules, ruleItem =>
                    {
                        if (ruleItem != null && ruleItem.RuleConditions != null && ruleItem.RuleConditions.Count > 0)
                        {
                            isRuleEvaluationFailed = false;
                            ruleFailureDetails = string.Empty;

                            foreach (Claim claimItem in listClaim)
                            {
                                foreach (RuleCondition ruleConditionItem in ruleItem.RuleConditions)
                                {
                                    try
                                    {

                                        ruleConditionItem.ConditionResult = CompileAndValidateCondition(ruleConditionItem, claimItem);
                                        
                                        //var ruleConditionFunction = ruleEngine.CompileRule<Claim>(ruleConditionItem);
                                        //ruleConditionItem.ConditionResult = ruleConditionFunction(claimItem);
                                        //ruleConditionFunction = null;

                                        //If all conditions in this rule are "AND" and if we are encountered with the first false
                                        //condition do not evaluate further conditions coz the final outcome will be false.
                                        //If all conditions in this rule are "Or" and if we are encountered with the first true
                                        //condition do not evaluate further conditions coz the final outcome will be true.
                                        if (ruleItem.AndOnlyRuleConditions == true && ruleConditionItem.ConditionResult == false)
                                        {
                                            //TODO ShortCircuitConditionEvaluationFalse;
                                        }
                                        else if (ruleItem.OrOnlyRuleConditions == true && ruleConditionItem.ConditionResult == true)
                                        {
                                            //TODO: ShortCircuitConditionEvaluationTrue;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        isRuleEvaluationFailed = true;

                                        //Skip this rule here and report for rule evaluation error. Move evaluation on new rule
                                        triggeredRules.Add(new gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo(ruleItem.RuleId, ruleItem.RuleName, ruleItem.RuleMessage, gloUIControlLibrary.Classes.ClaimRules.RuleType.FailedRuleEvaluation, ruleItem.RuleCategory,sRuleSource));

                                        //Audit log Rule Id, Patient Id and Claim Item Details with Claim#
                                        ruleFailureDetails = "Rule: " + ruleItem.RuleId + " evaluation against Claim# " + claimItem.ClaimNumber + ", Patient: " + Convert.ToString(claimItem.PatientId) + " for rule condition " + ruleConditionItem.ConditionIndex + " failed.";
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleEvaluation, gloAuditTrail.ActivityType.EvaluateRule, ruleFailureDetails, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        ruleFailureDetails = string.Empty;

                                        //Exception Log
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);

                                        break; //Take next rule for evaluation, foreach (RuleCondition ruleConditionItem in ruleItem.RuleConditions)
                                    }
                                }

                                if (isRuleEvaluationFailed == false)
                                {
                                    finalExpression = String.Empty;
                                    finalExpressionResult = false;

                                    finalExpression = BuildFinalExpression(ruleItem.RuleConditions, ruleItem.RuleExpression.Clone().ToString());
                                    finalExpressionResult = finalExpression.ParseExpression();

                                    if (finalExpressionResult == true)
                                    {
                                        //add this rule to triggered rule list and break the claim item foreach loop
                                        triggeredRules.Add(new gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo(ruleItem.RuleId, ruleItem.RuleName, ruleItem.RuleMessage, ruleItem.RuleTypeInfo, ruleItem.RuleCategory,sRuleSource));
                                        break; //foreach (Claim claimItem in listClaim)
                                    }
                                    else
                                    { ruleItem.ResetConditionResults(); }
                                }
                                else { break; } //foreach (Claim claimItem in listClaim) 
                            }
                        }
                    });

                    #endregion "Using Parallel ForEach"
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (listRules != null) { listRules.Clear(); listRules = null; }
                if (listClaim != null) { listClaim.Clear(); listClaim = null; }
                //if (ruleEngine != null) { ruleEngine.Dispose(); }
                if (businessObject != null) { businessObject.Dispose(); businessObject = null; }
            }


            return triggeredRules;
        }

        private List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> SequentialEvaluationRules(List<Claim> listClaim, List<ChargeRules.Rule> listInputRule = null,bool ServiceCall=false)
        {
            List<ChargeRules.Rule> listRules = null;
            //RuleEngine ruleEngine = null;
            string finalExpression = String.Empty;
            bool finalExpressionResult = false;
            List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo> triggeredRules = null;

            BusinessObjects businessObject = null;
            bool isRuleEvaluationFailed = false;
            string ruleFailureDetails = string.Empty;
            string sAUSID = string.Empty;
            string sRuleSource = string.Empty;
            try
            {
                businessObject = new BusinessObjects();
                if (ServiceCall)
                {
                    sAUSID = listClaim[0].ClaimFacility.AUSID;
                    sRuleSource = "Global";
                }
                if (listInputRule != null)
                { listRules = listInputRule; }
                else
                { listRules = ChargeRules.RulesRepository.GetRules(ServiceCall, sAUSID); }

                if (listRules != null && listRules.Count > 0 && listClaim != null && listClaim.Count > 0)
                {
                    triggeredRules = new List<gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo>();

                    //ruleEngine = new RuleEngine();

                    #region "Using normal ForEach"
                    
                    foreach (ChargeRules.Rule ruleItem in listRules)
                    {
                        if (ruleItem != null && ruleItem.RuleConditions != null && ruleItem.RuleConditions.Count > 0)
                        {
                            isRuleEvaluationFailed = false;
                            ruleFailureDetails = string.Empty;

                            foreach (Claim claimItem in listClaim)
                            {
                                foreach (RuleCondition ruleConditionItem in ruleItem.RuleConditions)
                                {
                                    try
                                    {
                                        ruleConditionItem.ConditionResult = CompileAndValidateCondition(ruleConditionItem, claimItem);

                                        //var ruleConditionFunction = ruleEngine.CompileRule<Claim>(ruleConditionItem);
                                        //ruleConditionItem.ConditionResult = ruleConditionFunction(claimItem);
                                        //ruleConditionFunction = null;

                                        //If all conditions in this rule are "AND" and if we are encountered with the first false
                                        //condition do not evaluate further conditions coz the final outcome will be false.
                                        //If all conditions in this rule are "Or" and if we are encountered with the first true
                                        //condition do not evaluate further conditions coz the final outcome will be true.
                                        if (ruleItem.AndOnlyRuleConditions == true && ruleConditionItem.ConditionResult == false)
                                        {
                                            //TODO ShortCircuitConditionEvaluationFalse;
                                        }
                                        else if (ruleItem.OrOnlyRuleConditions == true && ruleConditionItem.ConditionResult == true)
                                        {
                                            //TODO: ShortCircuitConditionEvaluationTrue;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        isRuleEvaluationFailed = true;

                                        //Skip this rule here and report for rule evaluation error. Move evaluation on new rule
                                        triggeredRules.Add(new gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo(ruleItem.RuleId, ruleItem.RuleName, ruleItem.RuleMessage, gloUIControlLibrary.Classes.ClaimRules.RuleType.FailedRuleEvaluation, ruleItem.RuleCategory,sRuleSource));

                                        //Audit log Rule Id, Patient Id and Claim Item Details with Claim#
                                        ruleFailureDetails = "Rule: " + ruleItem.RuleId + " evaluation against Claim# " + claimItem.ClaimNumber + ", Patient: " + Convert.ToString(claimItem.PatientId) + " for rule condition " + ruleConditionItem.ConditionIndex + " failed.";
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ChargeRule, gloAuditTrail.ActivityCategory.ChargeRuleEvaluation, gloAuditTrail.ActivityType.EvaluateRule, ruleFailureDetails, 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                                        ruleFailureDetails = string.Empty;

                                        //Exception Log
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);

                                        break; //Take next rule for evaluation, foreach (RuleCondition ruleConditionItem in ruleItem.RuleConditions)
                                    }

                                }

                                if (isRuleEvaluationFailed == false)
                                {
                                    finalExpression = String.Empty;
                                    finalExpressionResult = false;

                                    finalExpression = BuildFinalExpression(ruleItem.RuleConditions, ruleItem.RuleExpression.Clone().ToString());
                                    finalExpressionResult = finalExpression.ParseExpression();

                                    if (finalExpressionResult == true)
                                    {
                                        //add this rule to triggered rule list and break the claim item foreach loop
                                        triggeredRules.Add(new gloUIControlLibrary.Classes.ClaimRules.TriggeredRuleInfo(ruleItem.RuleId, ruleItem.RuleName, ruleItem.RuleMessage, ruleItem.RuleTypeInfo, ruleItem.RuleCategory,sRuleSource));
                                        break; //foreach (Claim claimItem in listClaim)
                                    }
                                    else
                                    {
                                        ruleItem.ResetConditionResults();
                                    }
                                }
                                else { break; } //foreach (Claim claimItem in listClaim) 
                            }
                        }
                    }

                    #endregion "Using normal ForEach"
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (listRules != null) { listRules.Clear(); listRules = null; }
                if (listClaim != null) { listClaim.Clear(); listClaim = null; }
                //if (ruleEngine != null) { ruleEngine.Dispose(); }
                if (businessObject != null) { businessObject.Dispose(); businessObject = null; }
            }


            return triggeredRules;
        }

        private string BuildFinalExpression(List<RuleCondition> ruleConditionResult, string ruleExpression)
        {

            try
            {
                int conditionindexforcontinue = 0;
                if (ruleConditionResult != null && ruleConditionResult.Count > 0)
                {
                    foreach (RuleCondition conditionItem in ruleConditionResult.OrderByDescending(rc => rc.ConditionIndex))
                    {



                        if (conditionindexforcontinue != conditionItem.ConditionIndex)
                        {
                            conditionindexforcontinue = conditionItem.ConditionIndex;

                            var groupedMultiplevalues = from c in ruleConditionResult.AsEnumerable()
                                                        where c.ConditionIndex == conditionItem.ConditionIndex
                                                        group c by c.ConditionIndex into grp
                                                        where grp.Count() > 1
                                                        select grp.Key;


                            if (groupedMultiplevalues.Any())
                            {
                                var MultiplevaluesConditionResult = (List<ChargeRules.RuleCondition>)null;
                                var MultiplevaluesWithSameConditionIndex = (List<ChargeRules.RuleCondition>)null;


                                string sInternalPredicate = EvaluatePredicate(ruleConditionResult, ruleExpression, conditionItem);

                                if (conditionItem.Operator == ChargeRules.Operator.Exists)
                                {
                                    MultiplevaluesConditionResult = (from MultipleValueConditions in ruleConditionResult.AsEnumerable()
                                                                     where MultipleValueConditions.ConditionIndex == conditionItem.ConditionIndex &&
                                                                         MultipleValueConditions.ConditionResult == Convert.ToBoolean(1)
                                                                     select MultipleValueConditions).ToList<ChargeRules.RuleCondition>();
                                    MultiplevaluesWithSameConditionIndex = (from MultipleValueConditions in ruleConditionResult.AsEnumerable()
                                                                     where MultipleValueConditions.ConditionIndex == conditionItem.ConditionIndex 
                                                                     select MultipleValueConditions).ToList<ChargeRules.RuleCondition>();

                                    if (sInternalPredicate == "Or")//OrExists
                                    {
                                        if (MultiplevaluesConditionResult.Any())
                                        {
                                            ruleExpression = ruleExpression.Replace("" + (conditionItem.ConditionIndex) + "", "T");
                                        }
                                        else
                                        {
                                            ruleExpression = ruleExpression.Replace("" + (conditionItem.ConditionIndex) + "", "F");
                                        }
                                    }
                                    else if (sInternalPredicate == "And")//AndExists
                                    {
                                        if (MultiplevaluesConditionResult.Count == MultiplevaluesWithSameConditionIndex.Count)
                                        {
                                            ruleExpression = ruleExpression.Replace("" + (conditionItem.ConditionIndex) + "", "T");
                                        }
                                        else
                                        {
                                            ruleExpression = ruleExpression.Replace("" + (conditionItem.ConditionIndex) + "", "F");
                                        }
                                    }
                                }
                                else
                                {
                                    MultiplevaluesConditionResult = (from MultipleValueConditions in ruleConditionResult.AsEnumerable()
                                                                     where MultipleValueConditions.ConditionIndex == conditionItem.ConditionIndex &&
                                                                         MultipleValueConditions.ConditionResult == Convert.ToBoolean(0)
                                                                     select MultipleValueConditions).ToList<ChargeRules.RuleCondition>();

                                    MultiplevaluesWithSameConditionIndex = (from MultipleValueConditions in ruleConditionResult.AsEnumerable()
                                                                            where MultipleValueConditions.ConditionIndex == conditionItem.ConditionIndex
                                                                            select MultipleValueConditions).ToList<ChargeRules.RuleCondition>();

                                    if (sInternalPredicate == "Or")//OrNotExists
                                    {
                                        if (MultiplevaluesConditionResult.Any())
                                        {
                                            ruleExpression = ruleExpression.Replace("" + (conditionItem.ConditionIndex) + "", "F");
                                        }
                                        else
                                        {
                                            ruleExpression = ruleExpression.Replace("" + (conditionItem.ConditionIndex) + "", "T");
                                        } 
                                    }
                                    else if (sInternalPredicate == "And")//AndNotExists
                                    {
                                        if (MultiplevaluesConditionResult.Count == MultiplevaluesWithSameConditionIndex.Count)
                                        {
                                            ruleExpression = ruleExpression.Replace("" + (conditionItem.ConditionIndex) + "", "F");
                                        }
                                        else
                                        {
                                            ruleExpression = ruleExpression.Replace("" + (conditionItem.ConditionIndex) + "", "T");
                                        } 
                                    }
                                }


                            }
                            else
                            {
                                ruleExpression = ruleExpression.Replace("" + (conditionItem.ConditionIndex) + "", conditionItem.ConditionResult ? "T" : "F");
                            }
                        }

                    }
                }
                ruleExpression = ruleExpression.Replace("T", "1").Replace("F", "0").Replace("And", "*").Replace("Or", "+");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ruleExpression;
        }

        private static string EvaluatePredicate(List<RuleCondition> ruleConditionResult, string ruleExpression, RuleCondition conditionItem)
        {
            string sExpr = ruleExpression.Replace("(", "").Replace(")", "").Replace("And", "*").Replace("Or", "+").Replace(" ", "");
            string str = "";
            if (sExpr.Length > 1)
            {
                if (conditionItem.ConditionIndex == 1)
                {
                    str = sExpr.Substring(sExpr.IndexOf(conditionItem.ConditionIndex.ToString()), 1);
                }
                else
                {
                    str = sExpr.Substring(sExpr.IndexOf(conditionItem.ConditionIndex.ToString()) - 1, 1);
                }
            }
            string sExistType = string.Empty;
            if (str == "*")
            {
                sExistType = "And";
            }
            else if (str == "+")
            {
                sExistType = "Or";
            }
            var MultiplevaluesPredicate = (ChargeRules.RuleCondition)null;
            MultiplevaluesPredicate = (from MultipleValueConditions in ruleConditionResult.AsEnumerable()
                                       where MultipleValueConditions.ConditionIndex == conditionItem.ConditionIndex &&
                                               MultipleValueConditions.Predicate != sExistType
                                       select MultipleValueConditions).LastOrDefault<ChargeRules.RuleCondition>();
            string sInternalPredicate;
            if (MultiplevaluesPredicate != null)
            {
                sInternalPredicate = MultiplevaluesPredicate.Predicate;
            }
            else
            {
                sInternalPredicate = sExistType;
            }
            return sInternalPredicate;
        }

        private bool CompileAndValidateCondition(RuleCondition rulecondition, Claim claimitem)
        {
            RuleEngine ruleEngine = null;
            bool conditionResult = false;
            Insurance findInsurance = null;
            bool isSpecialCaseProperty = false;
     //       bool isCurrentRepsonsiblePartyEvaluationOnly = false;

            try
            {

                //Set this property ChargeEditValidateAllClaimInsurances = 'TRUE' to verify insurance name, payer id etc against all claim insurance(s)
                //else set it to ChargeEditValidateAllClaimInsurances = 'FALSE' to evaluate only against the current responsible insurance 
                //(i.e first party on claim)
                //<<---------gloGlobal.gloPMGlobal.ChargeEditValidateAllClaimInsurances;----->
                //


                ruleEngine = new RuleEngine();
                findInsurance = new Insurance();
                isSpecialCaseProperty = false;

                if (gloGlobal.gloPMGlobal.ChargeEditValidateAllClaimInsurances == true)
                {
                    //special case to find the value on rule in a list for following properties
                    switch (rulecondition.PropertyName.Trim().ToUpper())
                    {
                        case "INSURANCEPAYERID":
                            { findInsurance.InsurancePayerID = Convert.ToString(rulecondition.Value); isSpecialCaseProperty = true; }
                            break;
                        case "INSURANCEPLANNAME":
                            { findInsurance.InsurancePlanName = Convert.ToString(rulecondition.Value); isSpecialCaseProperty = true; }
                            break;
                        case "INSURANCEPLANTYPE":
                            { findInsurance.InsurancePlanType = Convert.ToString(rulecondition.Value); isSpecialCaseProperty = true; }
                            break;
                        case "INSURANCEREPORTINGCATEGORY":
                            { findInsurance.InsuranceReportingCategory = Convert.ToString(rulecondition.Value); isSpecialCaseProperty = true; }
                            break;
                        case "INSURANCECOMPANYNAME":
                            { findInsurance.InsuranceCompanyName = Convert.ToString(rulecondition.Value); isSpecialCaseProperty = true; }
                            break;
                        case "INSURANCECOMPANYREPORTINGCATEGORY":
                            { findInsurance.InsuranceCompanyReportingCategory = Convert.ToString(rulecondition.Value); isSpecialCaseProperty = true; }
                            break;
                        default:
                            findInsurance = null;
                            isSpecialCaseProperty = false;
                            break;
                    }
                }

                if (isSpecialCaseProperty == true)
                {
                    if (rulecondition.Operator == Operator.Equal)
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<Insurance>(rulecondition.PropertyName, Operator.FoundIn, claimitem.InsuranceList);
                        conditionResult = ruleConditionFunction(findInsurance);
                        ruleConditionFunction = null;
                    }
                    else if (rulecondition.Operator == Operator.NotEqual)
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<Insurance>(rulecondition.PropertyName, Operator.FoundIn, claimitem.InsuranceList);
                        conditionResult = !ruleConditionFunction(findInsurance);
                        ruleConditionFunction = null;
                    }
                }
                else
                {
                    if (rulecondition.Operator == Operator.Exists && rulecondition.PropertyName == "CPTCode")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<CPT_Code>(rulecondition.PropertyName, Operator.FoundIn, claimitem.CPTCodes);

                        CPT_Code cptCode = new CPT_Code();
                        cptCode.CPTCode = Convert.ToString(rulecondition.Value);

                        conditionResult = ruleConditionFunction(cptCode);

                        cptCode = null;

                        ruleConditionFunction = null;
                    }
                    else if (rulecondition.Operator == Operator.NotExists && rulecondition.PropertyName == "CPTCode")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<CPT_Code>(rulecondition.PropertyName, Operator.FoundIn, claimitem.CPTCodes);

                        CPT_Code cptCode = new CPT_Code();
                        cptCode.CPTCode = Convert.ToString(rulecondition.Value);

                        conditionResult = !ruleConditionFunction(cptCode);

                        cptCode = null;

                        ruleConditionFunction = null;
                    }

                         /// exists operator for ClaimModifier property  
                    else if (rulecondition.Operator == Operator.Exists && rulecondition.PropertyName == "ClaimModifier")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<Claim_Modfier>(rulecondition.PropertyName, Operator.FoundIn, claimitem.ClaimModfiers);

                        Claim_Modfier ClaimModfier = new Claim_Modfier();
                        ClaimModfier.ClaimModifier = Convert.ToString(rulecondition.Value);

                        conditionResult = ruleConditionFunction(ClaimModfier);

                        ClaimModfier = null;

                        ruleConditionFunction = null;
                    }
                    ///  /// NotExists operator for ClaimModifier property   
                    else if (rulecondition.Operator == Operator.NotExists && rulecondition.PropertyName == "ClaimModifier")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<Claim_Modfier>(rulecondition.PropertyName, Operator.FoundIn, claimitem.ClaimModfiers);

                        Claim_Modfier ClaimModfier = new Claim_Modfier();
                        ClaimModfier.ClaimModifier = Convert.ToString(rulecondition.Value);

                        conditionResult = !ruleConditionFunction(ClaimModfier);

                        ClaimModfier = null;

                        ruleConditionFunction = null;
                    }


                        /// exists operator for rendering provider name property  
                    else if (rulecondition.Operator == Operator.Exists && rulecondition.PropertyName == "RenderingProviderName")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<RenderringProvider_Name>(rulecondition.PropertyName, Operator.FoundIn, claimitem.RenderringProviderNames);

                        RenderringProvider_Name RenderringProviderName = new RenderringProvider_Name();
                        RenderringProviderName.RenderingProviderName = Convert.ToString(rulecondition.Value);

                        conditionResult = ruleConditionFunction(RenderringProviderName);

                        RenderringProviderName = null;

                        ruleConditionFunction = null;
                    }
                    /// NotExists operator for rendering provider name property  
                    else if (rulecondition.Operator == Operator.NotExists && rulecondition.PropertyName == "RenderingProviderName")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<RenderringProvider_Name>(rulecondition.PropertyName, Operator.FoundIn, claimitem.RenderringProviderNames);

                        RenderringProvider_Name RenderringProviderName = new RenderringProvider_Name();
                        RenderringProviderName.RenderingProviderName = Convert.ToString(rulecondition.Value);

                        conditionResult = !ruleConditionFunction(RenderringProviderName);

                        RenderringProviderName = null;

                        ruleConditionFunction = null;
                    }

                          /// exists operator for rendering provider NPI property  
                    else if (rulecondition.Operator == Operator.Exists && rulecondition.PropertyName == "RenderingProviderNPI")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<RenderringProvider_NPI>(rulecondition.PropertyName, Operator.FoundIn, claimitem.RenderringProviderNPIs);

                        RenderringProvider_NPI RenderringProviderNPI = new RenderringProvider_NPI();
                        RenderringProviderNPI.RenderingProviderNPI = Convert.ToString(rulecondition.Value);

                        conditionResult = ruleConditionFunction(RenderringProviderNPI);

                        RenderringProviderNPI = null;

                        ruleConditionFunction = null;
                    }
                    /// NotExists operator for rendering provider NPI property  
                    else if (rulecondition.Operator == Operator.NotExists && rulecondition.PropertyName == "RenderingProviderNPI")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<RenderringProvider_NPI>(rulecondition.PropertyName, Operator.FoundIn, claimitem.RenderringProviderNPIs);

                        RenderringProvider_NPI RenderringProviderNPI = new RenderringProvider_NPI();
                        RenderringProviderNPI.RenderingProviderNPI = Convert.ToString(rulecondition.Value);

                        conditionResult = !ruleConditionFunction(RenderringProviderNPI);

                        RenderringProviderNPI = null;

                        ruleConditionFunction = null;
                    }
                    /// exists operator for ClaimDiagnosis property  
                    else if (rulecondition.Operator == Operator.Exists && rulecondition.PropertyName == "ClaimDiagnosis")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<Claim_Diagnosis>(rulecondition.PropertyName, Operator.FoundIn, claimitem.ClaimDiagnosis);

                        Claim_Diagnosis ClaimDiagnosis = new Claim_Diagnosis();
                        ClaimDiagnosis.ClaimDiagnosis = Convert.ToString(rulecondition.Value);
                        conditionResult = ruleConditionFunction(ClaimDiagnosis);
                        ClaimDiagnosis = null;
                        ruleConditionFunction = null;
                    }
                    /// NotExists operator for ClaimDiagnosis property   
                    else if (rulecondition.Operator == Operator.NotExists && rulecondition.PropertyName == "ClaimDiagnosis")
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<Claim_Diagnosis>(rulecondition.PropertyName, Operator.FoundIn, claimitem.ClaimDiagnosis);

                        Claim_Diagnosis ClaimDiagnosis = new Claim_Diagnosis();
                        ClaimDiagnosis.ClaimDiagnosis = Convert.ToString(rulecondition.Value);
                        conditionResult = !ruleConditionFunction(ClaimDiagnosis);
                        ClaimDiagnosis = null;
                        ruleConditionFunction = null;
                    }
                    else
                    {
                        var ruleConditionFunction = ruleEngine.CompileRule<Claim>(rulecondition);
                        conditionResult = ruleConditionFunction(claimitem);
                        ruleConditionFunction = null;
                    }                    
                }

            }
            catch (Exception ex)
            {
                conditionResult = false;
                throw ex;
            }
            finally
            {
                if (ruleEngine != null) { ruleEngine.Dispose(); }
                //Note: Do not dispose "rulecondition" and "claimitem" objects here
            }

            return conditionResult;
        }
    }

    internal class BusinessObjects : IDisposable
    {
        #region "Constructor, Distructor and Dispose"

        public BusinessObjects()
        { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BusinessObjects()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //Release all the managed resources
            }
        }

        #endregion "Constructor, Distructor and Dispose"
        
        public List<ChargeRules.Rule> GetRules(DataSet dsRules = null)
        {
            List<ChargeRules.Rule> rulesInfo = new List<ChargeRules.Rule>();
            ChargeRules.Rule chargeRuleItem = null;
            ChargeRules.RuleCondition chargeRuleCondition = null;
            ClsRuleEngine objClsRuleEngine = null;
            DataSet _dsRules = null;
            List<string> dateTimePropertyNames = null;

            try
            {
                dateTimePropertyNames = new List<string>();
                dateTimePropertyNames.Add("HospitalizationFromDOS");
                dateTimePropertyNames.Add("HospitalizationToDOS");
                dateTimePropertyNames.Add("ChargeFromDOS");
                dateTimePropertyNames.Add("ChargeToDOS");
                dateTimePropertyNames.Add("ClaimDate");
                dateTimePropertyNames.Add("OtherClaimDate");

                objClsRuleEngine = new ClsRuleEngine();

                if (dsRules != null && dsRules.Tables != null && dsRules.Tables.Count > 0)
                { _dsRules = dsRules.Copy(); }
                else
                { _dsRules = objClsRuleEngine.GetRules(); }

                if (_dsRules != null && _dsRules.Tables != null && _dsRules.Tables.Count > 0)
                {
                    foreach (DataRow ruleMasterRow in _dsRules.Tables["RuleMaster"].Rows)
                    {
                        chargeRuleItem = new Rule();
                        chargeRuleItem.RuleId = Convert.ToInt64(ruleMasterRow["nRuleID"]);
                        chargeRuleItem.RuleName = Convert.ToString(ruleMasterRow["sRuleName"]);
                        chargeRuleItem.RuleMessage = Convert.ToString(ruleMasterRow["sErrorMessage"]);
                        chargeRuleItem.RuleExpression = Convert.ToString(ruleMasterRow["sExpression"]);
                        chargeRuleItem.RuleTypeInfo = ((gloUIControlLibrary.Classes.ClaimRules.RuleType)Convert.ToInt32(ruleMasterRow["nRuleType"]));
                        chargeRuleItem.AndOnlyRuleConditions = false;
                        chargeRuleItem.OrOnlyRuleConditions = false;

                        if (ruleMasterRow.Table.Columns.Contains("sRuleCategory"))
                        {
                            chargeRuleItem.RuleCategory = Convert.ToString(ruleMasterRow["sRuleCategory"]);
                        }
                        else
                        {
                            chargeRuleItem.RuleCategory = "Clinic Edits";
                        }

                        //Convert.ToString(ruleMasterRow["sRuleDescription"]); 
                        //Convert.ToInt32(ruleMasterRow["nEvaluationLogic"]); 

                        foreach (DataRow ruleCondition in _dsRules.Tables["RuleConditions"].Select("nRuleID = " + chargeRuleItem.RuleId + ""))
                        {
                            

                            chargeRuleCondition = new RuleCondition(
                                Convert.ToString(ruleCondition["sPropertyName"]),
                                ((Operator)Enum.Parse(typeof(Operator), Convert.ToString(ruleCondition["sOperatorDisplayText"]))),
                                Convert.ToString(ruleCondition["sValue"]).Trim(),
                                Convert.ToInt32(ruleCondition["nConditionIndex"]),
                                Convert.ToString(ruleCondition["sPredicate"])
                                );

                            if (dateTimePropertyNames.Any(str => chargeRuleCondition.PropertyName.Equals(str)) == true
                                                                && Convert.ToString(chargeRuleCondition.Value).Trim() == "")
                            {
                                chargeRuleCondition.Value = System.DateTime.MinValue.ToString();
                            }

                            chargeRuleItem.RuleConditions.Add(chargeRuleCondition);
                            chargeRuleCondition = null;
                        }

                        #region " Finding AndOrOnly Conditions "

                        if (chargeRuleItem.RuleConditions != null && chargeRuleItem.RuleConditions.Count > 1)
                        {
                            //Setting Rule Master Object flag if all conditions are "And" or "Or" 
                            var AndOrOnly = (from rc in chargeRuleItem.RuleConditions
                                             where rc.ConditionIndex > 1
                                             group rc by rc.Predicate into rcGroup
                                             select new { ConditionKey = rcGroup.Key, ConditionCount = rcGroup.Count() }).ToList();

                            if (AndOrOnly != null && AndOrOnly.Count == 1)
                            {
                                if (AndOrOnly[0].ConditionKey == "And")
                                { chargeRuleItem.AndOnlyRuleConditions = true; }
                                else if (AndOrOnly[0].ConditionKey == "Or")
                                { chargeRuleItem.OrOnlyRuleConditions = true; }
                            }
                        }

                        #endregion " Finding AndOrOnly Conditions "

                        rulesInfo.Add(chargeRuleItem);
                        chargeRuleItem = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dateTimePropertyNames != null)
                {
                    dateTimePropertyNames.Clear();
                    dateTimePropertyNames = null;
                }

                if (_dsRules != null)
                {
                    if (_dsRules.Tables != null) { _dsRules.Tables.Clear(); }
                    _dsRules.Dispose();
                    _dsRules = null;
                }

                if (objClsRuleEngine != null) { objClsRuleEngine.Dispose(); objClsRuleEngine = null; }
                if (chargeRuleCondition != null) { chargeRuleCondition = null; }
                if (chargeRuleItem != null) { chargeRuleItem.Dispose(); chargeRuleItem = null; }

                //Do not dispose "dsRules" incoming parameter, it would clear the cache
            }

            return rulesInfo;
        }
    }
}
