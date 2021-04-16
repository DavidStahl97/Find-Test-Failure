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
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.HasOne(x => x.UITestRunEvent)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.UITestRunEventId);
        }
    }
}
