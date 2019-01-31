using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;


namespace SEAL
{
    public class SealExpression : ExpressionVisitor {

		Expression start_exp;

		public SealExpression(Expression e) {
			start_exp = e;
		}

        public Expression Modify(Expression expression)
        {
            return Visit(expression);
        }

        public static int NumMults(Expression e) {
            switch (e)
            {
                case LambdaExpression lam:
                    return NumMults(lam.Body);
                case BinaryExpression bin:
                    {
                        int count = 0;
                        if (bin.NodeType == ExpressionType.Multiply)
                        {
                            count = 1;
                        }
                        int c1 = NumMults(bin.Left);
                        int c2 = NumMults(bin.Right);
                        return count + c1 + c2;
                    }
                case UnaryExpression unary:
                    {
                        return NumMults(unary.Operand);
                    }
                default:
                    return 0;
            }
        }

        public static Expression RenameSingleParam(Expression e, ParameterExpression oldParam, ParameterExpression newParam)
        {
            switch (e)
            {
                case LambdaExpression lam:
                    List<ParameterExpression> paramList = new List<ParameterExpression>();
                    for (int i=0; i< lam.Parameters.Count; i++)
                    {
                        ParameterExpression current = lam.Parameters[i];
                        if (current.Name == oldParam.Name)
                        {
                            ParameterExpression replace = Expression.Parameter(current.Type, newParam.Name);
                            paramList.Add(replace);
                        }
                        else
                        {
                            paramList.Add(current);
                        }
                    }
                    ReadOnlyCollection<ParameterExpression> parameters = new ReadOnlyCollection<ParameterExpression>(paramList);
                    Expression body = RenameSingleParam(lam.Body, oldParam, newParam);
                    return Expression.Lambda(lam.Type, body, false, parameters);
                case ParameterExpression param:
                    if (param.Name == oldParam.Name)
                    {
                        return newParam;
                    }
                    return param;
                case BinaryExpression bin:
                    Expression left = RenameSingleParam(bin.Left, oldParam, newParam);
                    Expression right = RenameSingleParam(bin.Right, oldParam, newParam);
                    return Expression.MakeBinary(bin.NodeType, left, right);
                case UnaryExpression unary:
                    return RenameSingleParam(unary.Operand, oldParam, newParam);
                default:
                    return e;
            }
        }

        public static Expression<TDelegate2> ReplaceWithCall<TDelegate2>(LambdaExpression e) where TDelegate2 : class
        {
            switch (e)
            {
                case LambdaExpression lam:
                    List<ParameterExpression> paramList = new List<ParameterExpression>();
                    ParameterExpression evalExpr = Expression.Parameter(typeof(Evaluator), "eval");
                    paramList.Add(evalExpr);
                    ParameterExpression relinExpr = Expression.Parameter(typeof(RelinKeys), "relin");
                    paramList.Add(relinExpr);
                    Dictionary<ParameterExpression, ParameterExpression> paramDict= new Dictionary<ParameterExpression, ParameterExpression>();
                    for (int i = 0; i < lam.Parameters.Count; i++)
                    {
                        ParameterExpression current = lam.Parameters[i];
                        ParameterExpression newParam = Expression.Parameter(typeof(Ciphertext), current.Name);
                        paramDict[current] = newParam;
                        paramList.Add(newParam);
                    }
                    Expression newBody = ReplaceWithCall2(lam.Body, evalExpr, relinExpr, paramDict);
                    ReadOnlyCollection<ParameterExpression> parameters = new ReadOnlyCollection<ParameterExpression>(paramList);
                    return Expression.Lambda<TDelegate2>(newBody, false, parameters);
                default:
                    throw new Exception ("cant get here");
            }
        }

        public static Expression ReplaceWithCall2(Expression e, ParameterExpression evalExpr, ParameterExpression relinExpr,
                                                  Dictionary<ParameterExpression, ParameterExpression> paramDict)
        {
            switch (e)
            {
                case LambdaExpression lam:
                    throw new Exception("cant get here");
                case ParameterExpression param:
                    return paramDict[param];
                case MethodCallExpression methodcall:
                    if (methodcall.Method.Name == "R")
                    {
                        Expression ciph = null;
                        for (int i=0; i<methodcall.Arguments.Count; i++)
                        {
                            ciph = ReplaceWithCall2(methodcall.Arguments[i], evalExpr, relinExpr, paramDict);
                        }
                        Expression relinCall = Expression.Call(typeof(Evaluator), "SimpleRelin", null, evalExpr, relinExpr, ciph);
                        return relinCall;
                    }
                    throw new Exception("not R method call");
                case BinaryExpression bin:
                    Expression left = ReplaceWithCall2(bin.Left, evalExpr, relinExpr, paramDict);
                    Expression right = ReplaceWithCall2(bin.Right, evalExpr, relinExpr, paramDict);
                    if (bin.NodeType == ExpressionType.Add)
                    {
                        Expression addCall = Expression.Call(typeof(Evaluator), "SimpleAdd", null, evalExpr, left, right);
                        return addCall;
                    }
                    if (bin.NodeType == ExpressionType.Multiply)
                    {
                        Expression multCall = Expression.Call(typeof(Evaluator), "SimpleMult", null, evalExpr, left, right);
                        return multCall;
                    }
                    throw new Exception("not add or mult");
                default:
                    return e;
            }
        }
    }
}