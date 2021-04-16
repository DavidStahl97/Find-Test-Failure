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
    public class ClearContentEventConfiguraiton : IEntityTypeConfiguration<UITestRunClearContentEvent>
    {
        public void Configure(EntityTypeBuilder<UITestRunClearContentEvent> builder)
        {
            builder.ToTable(nameof(UITestRunClearContentEvent));
            Configurations.BuildUITestRunUIElementEvent(builder, x => x.ClearContentEvents);
        }
    }
}
