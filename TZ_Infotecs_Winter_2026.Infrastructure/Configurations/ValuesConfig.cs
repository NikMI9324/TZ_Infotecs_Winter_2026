using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TZ_Infotecs_Winter_2026.Domain.Entities;

namespace TZ_Infotecs_Winter_2026.Infrastructure.Configurations
{
    internal sealed class ValuesConfig : IEntityTypeConfiguration<Value>
    {
        public void Configure(EntityTypeBuilder<Value> builder)
        {
            builder.ToTable("values");

            builder.HasKey(v => v.Id);

            #region Property Names
            builder.Property(v => v.Id)
                .HasColumnName("value_id")
                .IsRequired();

            builder.Property(v => v.FileName)
                .HasColumnName("file_name")
                .HasMaxLength(255);

            builder.Property(v => v.Date)
                .HasColumnName("date")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.Property(v => v.ExecutionTime)
                .HasColumnName("execution_time")
                .IsRequired();

            builder.Property(v => v.ValueDefinition)
                .HasColumnName("value")
                .IsRequired();
            #endregion

            #region Indexes

            builder.HasIndex(v => v.FileName)
                .HasDatabaseName("idx_values_file_name");

            builder.HasIndex(v => v.Date)
                .HasDatabaseName("idx_values_date");

            #endregion
        }
    }
}
