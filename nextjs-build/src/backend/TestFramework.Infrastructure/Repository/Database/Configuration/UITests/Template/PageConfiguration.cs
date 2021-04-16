using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Contract;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Template
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Name)
                .HasMaxLength(Contracts.UIPages.NameMaxLength)
                .IsRequired();
        }
    }
}
