﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Global.Specifications
{
    public class AndNotSpecification<T> : CompositeSpecification<T>
    {
        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right) : base(left, right) { }

        public override Expression<Func<T, bool>> GetExpression()
        {
            var bodyNot = Expression.Not(Right.GetExpression().Body);
            var bodyNotExpression = Expression.Lambda<Func<T, bool>>(bodyNot, Right.GetExpression().Parameters);

            return Left.GetExpression().And(bodyNotExpression);
        }
    }
}
