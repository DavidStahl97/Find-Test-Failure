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

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Template
{
    public class WriteEventConfiguration : IEntityTypeConfiguration<WriteEvent>
    {
        public void Configure(EntityTypeBuilder<WriteEvent> builder)
        {
            builder.ToTable(nameof(WriteEvent));

            Configurations.BuildUIElementEvent(builder, x => x.WriteEvents);
            
            builder.Property(x => x.GenerateUnique)
                .IsRequired();

            builder.Property(x => x.Input)
                .IsRequired()
                .HasDefaultValue(string.Empty)
                .HasColumnType($"VARCHAR({Contracts.UIEvents.InputMaxLength})");
        }
    }
}
