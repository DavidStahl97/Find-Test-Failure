using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Run
{
    public class UITestRunUIElementConfiguration : IEntityTypeConfiguration<UITestRunUIElement>
    {
        public void Configure(EntityTypeBuilder<UITestRunUIElement> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Name)
                .HasMaxLength(UITestRunUIElement.NameMaxLength)
                .IsRequired();

            builder.Property(x => x.FindByMethod)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.FindBy)
                .IsRequired();
        }
    }
}
