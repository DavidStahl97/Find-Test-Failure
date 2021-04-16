using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Contract;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Infrastructure.Repository.Database.Configuration.UITests.Run
{
    class UITestRunImportFileEventConfiguration : IEntityTypeConfiguration<UITestRunImportFileEvent>
    {
        public void Configure(EntityTypeBuilder<UITestRunImportFileEvent> builder)
        {
            builder.ToTable(nameof(UITestRunImportFileEvent));
            Configurations.BuildUITestRunUIElementEvent(builder, x => x.ImportFileEvents);

            builder.Property(x => x.FileName)
                .HasMaxLength(Contracts.UserFiles.FileNameMaxLength)
                .IsRequired();

            builder.Property(x => x.StoredFileName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
