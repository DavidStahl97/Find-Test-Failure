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
    public class UITestRunMoveToUIElementEventConfiguration : IEntityTypeConfiguration<UITestRunMoveToUIElementEvent>
    {
        public void Configure(EntityTypeBuilder<UITestRunMoveToUIElementEvent> builder)
        {
            builder.ToTable(nameof(UITestRunMoveToUIElementEvent));
            Configurations.BuildUITestRunUIElementEvent(builder, x => x.MoveToUIElements);
        }
    }
}
