using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Contract;
using TestFramework.Domain;

namespace TestFramework.Infrastructure.Repository.Database.Configuration
{
    public class HealthCheckConfiguration : IEntityTypeConfiguration<HealthCheck>
    {
        public void Configure(EntityTypeBuilder<HealthCheck> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Contracts.HealthChecks.NameMaxLength);

            builder.Property(x => x.Healthy)
                .IsRequired();

            builder.Property(x => x.Url)
                .IsRequired();
        }
    }
}
