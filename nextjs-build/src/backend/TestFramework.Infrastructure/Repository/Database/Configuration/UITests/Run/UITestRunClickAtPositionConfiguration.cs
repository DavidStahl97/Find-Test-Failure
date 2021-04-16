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
    public class UITestRunClickAtPositionConfiguration : IEntityTypeConfiguration<UITestRunClickAtPositionEvent>
    {
        public void Configure(EntityTypeBuilder<UITestRunClickAtPositionEvent> builder)
        {
            builder.ToTable(nameof(UITestRunClickAtPositionEvent));
        }
    }
}
