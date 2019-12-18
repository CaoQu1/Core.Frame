using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Global.Specifications
{
    public class ExpressionSpecification<T> : Specification<T>
    {
        private Expression<Func<T, bool>> expression;

        public Expression<Func<T, bool>> Expression
        {
            get
            {
                if (expression == null)
                {
                    expression = t => true;
                }
                return expression;
            }
            set
            {
                expression = value;
            }
        }

        public ExpressionSpecification()
        { }

        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this.Expression = expression;
        }

        public override Expression<Func<T, bool>> GetExpression()
        {
            return this.Expression;
        }
    }
}
