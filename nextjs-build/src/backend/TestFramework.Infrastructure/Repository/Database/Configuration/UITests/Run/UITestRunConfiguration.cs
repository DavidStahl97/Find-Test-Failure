using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Infrastructure.Repository.Database.Configuration
{
    public class UITestRunConfiguration : IEntityTypeConfiguration<UITestRun>
    {
        public void Configure(EntityTypeBuilder<UITestRun> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Start)
                .IsRequired();

            builder.Property(x => x.State)
                .IsRequired();
        }
    }
}
