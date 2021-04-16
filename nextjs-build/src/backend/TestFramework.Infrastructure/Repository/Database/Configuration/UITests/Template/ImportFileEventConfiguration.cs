using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Template
{
    class ImportFileEventConfiguration : IEntityTypeConfiguration<ImportFileEvent>
    {
        public void Configure(EntityTypeBuilder<ImportFileEvent> builder)
        {
            builder.ToTable(nameof(ImportFileEvent));
            Configurations.BuildUIElementEvent(builder, x => x.ImportFileEvents);

            builder.HasOne(x => x.UserFile)
                .WithMany(x => x.ImportFileEvents)
                .HasForeignKey(x => x.UserFileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
