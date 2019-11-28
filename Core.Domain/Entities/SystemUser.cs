using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    public partial class SystemUser : AggregateRoot<SystemUser, int>
    {
        public override void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            throw new NotImplementedException();
        }
    }
}
