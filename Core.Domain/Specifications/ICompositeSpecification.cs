using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Specifications
{
    public interface ICompositeSpecification<T> : ISpecification<T>
    {
        ISpecification<T> Left { get; }

        ISpecification<T> Right { get; }
    }
}
