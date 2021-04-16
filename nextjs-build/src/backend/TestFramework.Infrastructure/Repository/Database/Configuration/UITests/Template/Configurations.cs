using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Template
{
    static class Configurations
    {
        public static void BuildUIElementEvent<T>(EntityTypeBuilder<T> builder, 
            Expression<Func<UIElement, IEnumerable<T>>> navigationExpression)
            where T : UIElementEvent
        {
            builder.HasOne(x => x.UIElement)
                .WithMany(navigationExpression)
                .HasForeignKey(x => x.UIElementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.WaitForUIElement)
                .IsRequired();

            builder.Property(x => x.UseDefaultWaitForUIElement)
                .IsRequired();
        }
    }
}
