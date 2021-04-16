using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Template
{
    public class ClearContentEventConfiguration : IEntityTypeConfiguration<ClearContentEvent>
    {
        public void Configure(EntityTypeBuilder<ClearContentEvent> builder)
        {
            builder.ToTable(nameof(ClearContentEvent));
            Configurations.BuildUIElementEvent(builder, x => x.ClearContentEvents);
        }
    }
}
