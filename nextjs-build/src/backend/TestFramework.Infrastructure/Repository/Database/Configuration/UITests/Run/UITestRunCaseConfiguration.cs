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
    public class UITestRunCaseConfiguration : IEntityTypeConfiguration<UITestRunCase>
    {
        public void Configure(EntityTypeBuilder<UITestRunCase> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.StartUrl)
                .IsRequired();

            builder.Property(x => x.State)
                .IsRequired();

            builder.Property(x => x.Browser)
                .IsRequired();

            builder.Property(x => x.DefaultWaitForUIElement)
                .IsRequired();

            builder.HasOne(x => x.TestRun)
                .WithMany(x => x.TestCases)
                .HasForeignKey(x => x.UITestRunId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
