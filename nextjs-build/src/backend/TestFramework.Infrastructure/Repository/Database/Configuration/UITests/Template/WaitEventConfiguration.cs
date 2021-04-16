using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration
{
    public class WaitEventConfiguration : IEntityTypeConfiguration<WaitEvent>
    {
        public void Configure(EntityTypeBuilder<WaitEvent> builder)
        {
            builder.ToTable(nameof(WaitEvent));

            builder.Property(x => x.Ticks)
                .IsRequired();
        }
    }
}
