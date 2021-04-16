using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Run
{
    static class Configurations
    {
        public static void BuildUITestRunUIElementEvent<T>(EntityTypeBuilder<T> builder,
            Expression<Func<UITestRunUIElement, IEnumerable<T>>> navigationExpression)
            where T : UITestRunUIElementEvent
        {
            builder.Property(x => x.WaitForUIElement)
                .IsRequired();

            builder.Property(x => x.UseDefaultWaitForUIElement)
                .IsRequired();

            builder.HasOne(x => x.UIElement)
                .WithMany(navigationExpression)
                .HasForeignKey(x => x.UITestRunUIElementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
