using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace ChargeRules
{
    internal class ExpressionBuilder
    {
        public Expression BuildExpression<T>(Operator ruleOperator, object value, ParameterExpression parameterExpression)
        {
            try
            {
                ExpressionType expressionType = new ExpressionType();
                var leftOperand = parameterExpression;
                var rightOperand = Expression.Constant(Convert.ChangeType(value, typeof(T)));
                var expressionTypeValue = (ExpressionType)expressionType.GetType().GetField(Enum.GetName(typeof(Operator), ruleOperator)).GetValue(ruleOperator);
                return CastBuildExpression(expressionTypeValue, value, leftOperand, rightOperand);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return null;
            }
        }

        public Expression BuildExpression<T>(string propertyName, Operator ruleOperator, object value, ParameterExpression parameterExpression)
        {
            try
            {
                ExpressionType expressionType = new ExpressionType();
                var leftOperand = MemberExpression.Property(parameterExpression, propertyName);
                var rightOperand = Expression.Constant(Convert.ChangeType(value, value.GetType()));
                FieldInfo fieldInfo = expressionType.GetType().GetField(Enum.GetName(typeof(Operator), ruleOperator));
                var expressionTypeValue = (ExpressionType)fieldInfo.GetValue(ruleOperator);
                return CastBuildExpression(expressionTypeValue, value, leftOperand, rightOperand);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return null;
            }

        }

        public Tuple<Expression, ParameterExpression> BuildExpression<T>(string propertyName, Operator ruleOperator,
            ParameterExpression parameterExpression, List<T> values)
        {
            try
            {
                ParameterExpression listExpression = Expression.Parameter(typeof(List<T>));
                ParameterExpression counterExpression = Expression.Parameter(typeof(int));
                ParameterExpression toExpression = Expression.Parameter(typeof(int));
                ParameterExpression arrayExpression = Expression.Parameter(typeof(T[]));
                ParameterExpression valueExpression = Expression.Parameter(typeof(T));
                ParameterExpression checkExpression = Expression.Parameter(typeof(T));
                ParameterExpression returnExpression = Expression.Parameter(typeof(bool));
                MemberExpression memberExpression = MemberExpression.Property(parameterExpression, propertyName);
                Expression expression = memberExpression.Expression;
                var type = memberExpression.Type;
                ParameterExpression propertyExpression = Expression.Parameter(type);
                ParameterExpression localPropertyExpression = Expression.Parameter(type);

                LabelTarget breakLabel = Expression.Label();
                PropertyInfo result = typeof(List<T>).GetProperty("Count");
                MethodInfo toArray = typeof(List<T>).GetMethod("ToArray");
                var toArrayName = toArray.Name;
                MethodInfo getGetMethod = result.GetGetMethod();
                ConstantExpression constantExpression = Expression.Constant(true);
                if (ruleOperator == Operator.NotFoundIn)
                {
                    constantExpression = Expression.Constant(false);
                }
                Expression loop = Expression.Block(
                    new ParameterExpression[] { toExpression, arrayExpression, valueExpression, counterExpression, 
                returnExpression, propertyExpression, localPropertyExpression, listExpression },
                    Expression.Assign(listExpression, Expression.Constant(values)),
                    Expression.Assign(toExpression, Expression.Call(listExpression, getGetMethod)),
                    Expression.Assign(arrayExpression, Expression.Call(listExpression, toArray)),
                    Expression.Assign(propertyExpression, MemberExpression.Property(checkExpression, propertyName)),
                    Expression.Loop(
                        Expression.IfThenElse(
                            Expression.LessThan(counterExpression, toExpression),
                            Expression.Block(
                                Expression.Assign(valueExpression, Expression.ArrayAccess(arrayExpression, counterExpression)),
                                Expression.Assign(localPropertyExpression, Expression.Property(valueExpression, propertyName)),
                                Expression.IfThen(
                                    Expression.Equal(propertyExpression, localPropertyExpression),
                                    Expression.Block(Expression.Assign(returnExpression, constantExpression),
                                        Expression.Break(breakLabel))),
                                Expression.Assign(Expression.ArrayAccess(arrayExpression, counterExpression), checkExpression),
                                Expression.PostIncrementAssign(counterExpression)),
                            Expression.Break(breakLabel)
                            ), breakLabel
                        ),
                        Expression.And(returnExpression, constantExpression)
                    );
                return new Tuple<Expression, ParameterExpression>(Expression.Block(loop), checkExpression);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return null;
            }
        }

        private Expression CastBuildExpression(ExpressionType expressionTypeValue, object value, Expression leftOperand, ConstantExpression rightOperand)
        {
            try
            {
                if (leftOperand.Type == rightOperand.Type)
                {
                    return Expression.MakeBinary(expressionTypeValue, leftOperand, rightOperand);
                }
                else if (CanChangeType(value, leftOperand.Type))
                {
                    if (rightOperand.Type != typeof(bool))
                    {
                        rightOperand = Expression.Constant(Convert.ChangeType(value, leftOperand.Type));
                    }
                    else
                    {
                        leftOperand = Expression.Constant(Convert.ChangeType(value, rightOperand.Type));
                    }
                    return Expression.MakeBinary(expressionTypeValue, leftOperand, rightOperand);
                }
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return null;
            }
        }

        private bool CanChangeType(object sourceType, Type targetType)
        {
            try
            {
                Convert.ChangeType(sourceType, targetType);
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return false;
            }
        }
    }
}
