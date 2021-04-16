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
    class UserFileConfiguration : IEntityTypeConfiguration<UserFile>
    {
        public void Configure(EntityTypeBuilder<UserFile> builder)
        {
            builder.HasKey(x => new { x.Id });

            builder.Property(x => x.FileName)
                .HasMaxLength(Contracts.UserFiles.FileNameMaxLength)
                .IsRequired();

            builder.Property(x => x.StoredFileName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CreatedDateTime)
                .IsRequired();
        }
    }
}
