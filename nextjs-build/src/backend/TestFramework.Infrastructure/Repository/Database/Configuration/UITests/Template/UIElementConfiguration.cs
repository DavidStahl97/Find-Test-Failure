using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Contract;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Infrastructure.Repository.Database.Configuration
{
    class UIElementConfiguration : IEntityTypeConfiguration<UIElement>
    {
        public void Configure(EntityTypeBuilder<UIElement> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Name)
                .HasMaxLength(Contracts.UIElements.NameMaxLength)
                .IsRequired();

            builder.Property(x => x.FindByMethod)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.FindBy)
                .IsRequired();

            builder.HasOne(x => x.Page)
                .WithMany(x => x.UIElements)
                .HasForeignKey(x => x.PageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
