using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Contract;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Infrastructure.Repository.Database.Configuration
{
    public class UITestCaseConfiguration : IEntityTypeConfiguration<UITestCase>
    {
        public void Configure(EntityTypeBuilder<UITestCase> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Name)
                .HasMaxLength(Contracts.UITestCases.NameMaxLength)
                .IsRequired();

            builder.Property(x => x.StartUrl)
                .IsRequired();

            builder.Property(x => x.DefaultWaitForUIElement)
                .IsRequired();
        }
    }
}
