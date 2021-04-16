using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Template
{
    public class ClickAtPositionEventConfiguration : IEntityTypeConfiguration<ClickAtPositionEvent>
    {
        public void Configure(EntityTypeBuilder<ClickAtPositionEvent> builder)
        {
            builder.ToTable(nameof(ClickAtPositionEvent));
        }
    }
}
