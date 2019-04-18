using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ChargeRules
{
    public class RuleEngine
    {

        #region "Constructor, Distructor and Dispose"

        public RuleEngine()
        { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RuleEngine()
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

        public Func<T, bool> CompileRule<T>(RuleCondition rule)
        {
            try
            {
                if (string.IsNullOrEmpty(rule.PropertyName))
                {
                    ExpressionBuilder expressionBuilder = new ExpressionBuilder();
                    var param = Expression.Parameter(typeof(T));
                    Expression expression = expressionBuilder.BuildExpression<T>(rule.Operator, rule.Value, param);
                    Func<T, bool> func = Expression.Lambda<Func<T, bool>>(expression, param).Compile();
                    return func;
                }
                else
                {
                    ExpressionBuilder expressionBuilder = new ExpressionBuilder();
                    var param = Expression.Parameter(typeof(T));
                    Expression expression = expressionBuilder.BuildExpression<T>(rule.PropertyName, rule.Operator, rule.Value, param);
                    Func<T, bool> func = Expression.Lambda<Func<T, bool>>(expression, param).Compile();
                    return func;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return null;
            }
        }

        public Func<T, bool> CompileRule<T>(string propertyName,Operator ruleOperator, List<T> values)
        {
            try
            {
                ExpressionBuilder expressionBuilder = new ExpressionBuilder();
                var param = Expression.Parameter(typeof(T));
                Tuple<Expression, ParameterExpression> expression =
                    expressionBuilder.BuildExpression<T>(propertyName, ruleOperator, param, values);
                Func<T, bool> compiledExpression = Expression.Lambda<Func<T, bool>>(
                    expression.Item1, expression.Item2).Compile();
                return compiledExpression;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return null;
            }
        }
    }
}
