using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Global.Specifications
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }

        public override Expression<Func<T, bool>> GetExpression()
        {
            return Left.GetExpression().Or(Right.GetExpression());
        }
    }
}
