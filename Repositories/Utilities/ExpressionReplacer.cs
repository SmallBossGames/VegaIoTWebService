using System;
using System.Linq.Expressions;

namespace VegaIoTApi.Repositories.Utilities
{
    public class ExpressionReplacer : ExpressionVisitor
    {
        private readonly Func<Expression, Expression> replacer;

        public ExpressionReplacer(Func<Expression, Expression> replacer)
        {
            this.replacer = replacer;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(replacer(node));
        }

        public static T ReplaceParameter<T>(T expr, ParameterExpression toReplace, ParameterExpression replacement)
            where T : Expression
        {
            var replacer = new ExpressionReplacer(e => e == toReplace ? replacement : e);
            return (T)replacer.Visit(expr);
        }
    }
}