using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Contract;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Run
{
    public class UITestRunWriteEventConfiguration : IEntityTypeConfiguration<UITestRunWriteEvent>
    {
        public void Configure(EntityTypeBuilder<UITestRunWriteEvent> builder)
        {
            builder.ToTable(nameof(UITestRunWriteEvent));

            Configurations.BuildUITestRunUIElementEvent(builder, x => x.WriteEvents);

            builder.Property(x => x.Input)
                .IsRequired()
                .HasColumnType($"VARCHAR({Contracts.UIEvents.InputMaxLength})")
                .HasDefaultValue(string.Empty);

            builder.Property(x => x.GenerateUnique)
                .IsRequired();
        }
    }
}
