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
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration
{
    class UIEventConfiguration : IEntityTypeConfiguration<UIEvent>
    {
        public void Configure(EntityTypeBuilder<UIEvent> builder)
        {
            builder.ToTable(nameof(UIEvent));

            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Name)
                .HasMaxLength(Contracts.UIEvents.NameMaxLength)
                .IsRequired();

            builder.Property(x => x.UITestCaseId)
                .IsRequired();

            builder.Property(x => x.Step)
                .IsRequired();
        }
    }
}
