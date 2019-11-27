using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Global.Specifications
{
    public class ExpressionSpecification<T> : Specification<T>
    {
        private Expression<Func<T, bool>> expression;

        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this.expression = expression;
        }

        public override Expression<Func<T, bool>> GetExpression()
        {
            return this.expression;
        }
    }
}
