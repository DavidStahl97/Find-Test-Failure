using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Run
{
    public class UITestRunMoveByEventOffsetConfiguration : IEntityTypeConfiguration<UITestRunMoveByOffsetEvent>
    {
        public void Configure(EntityTypeBuilder<UITestRunMoveByOffsetEvent> builder)
        {
            builder.ToTable(nameof(UITestRunMoveByOffsetEvent));

            builder.Property(x => x.OffsetX)
                .IsRequired();

            builder.Property(x => x.OffsetY)
                .IsRequired();
        }
    }
}
