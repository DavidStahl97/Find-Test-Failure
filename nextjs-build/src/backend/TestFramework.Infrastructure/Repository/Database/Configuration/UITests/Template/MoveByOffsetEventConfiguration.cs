using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Template
{
    public class MoveByOffsetEventConfiguration : IEntityTypeConfiguration<MoveByOffsetEvent>
    {
        public void Configure(EntityTypeBuilder<MoveByOffsetEvent> builder)
        {
            builder.ToTable(nameof(MoveByOffsetEvent));

            builder.Property(x => x.OffsetX)
                .IsRequired();

            builder.Property(x => x.OffsetY)
                .IsRequired();
        }
    }
}
