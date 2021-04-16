using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration
{
    public class UITestRunEventConfiguration : IEntityTypeConfiguration<UITestRunEvent>
    {
        public void Configure(EntityTypeBuilder<UITestRunEvent> builder)
        {
            builder.ToTable(nameof(UITestRunEvent));

            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Name)
                .HasMaxLength(UITestRunEvent.NameMaxLength)
                .IsRequired();

            builder.Property(x => x.State)
                .IsRequired();

            builder.HasOne(x => x.RunCase)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.UIRunCase)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
