using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Run
{
    public class UITestRunWaitEventConfiguration : IEntityTypeConfiguration<UITestRunWaitEvent>
    {
        public void Configure(EntityTypeBuilder<UITestRunWaitEvent> builder)
        {
            builder.ToTable(nameof(UITestRunWaitEvent));

            builder.Property(x => x.Ticks)
                .IsRequired();
        }
    }
}
