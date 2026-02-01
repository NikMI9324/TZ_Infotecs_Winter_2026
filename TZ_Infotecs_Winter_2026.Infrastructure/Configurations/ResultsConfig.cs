using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TZ_Infotecs_Winter_2026.Domain.Entities;

namespace TZ_Infotecs_Winter_2026.Infrastructure.Configurations
{
    internal sealed class ResultsConfig : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.ToTable("results");

            builder.HasKey(r => r.FileName);

            #region Property Names
            builder.Property(r => r.FileName)
                .HasColumnName("file_name");

            builder.Property(r => r.MinimalDate)
                .HasColumnName("minimal_date")
                .HasColumnType("timestamp with time zone");

            builder.Property(r => r.MedianValueDefinition)
                .HasColumnName("median_value_definition");

            builder.Property(r => r.TimeDeltaSeconds)
                .HasColumnName("time_delta_seconds");

            builder.Property(r => r.AverageExecutionTime)
                .HasColumnName("avg_execution_time");

            builder.Property(r => r.MinValueDefinition)
                .HasColumnName("min_value");

            builder.Property(r => r.MaxValueDefinition)
                .HasColumnName("max_value");

            builder.Property(r => r.AverageValueDefinition)
                .HasColumnName("avg_value");
            #endregion


            #region Indexes

            builder.HasIndex(r => r.MinimalDate)
                .HasDatabaseName("idx_results_min_date");

            builder.HasIndex(r => r.AverageValueDefinition)
                .HasDatabaseName("idx_results_avg_value");

            builder.HasIndex(r => r.AverageExecutionTime)
                .HasDatabaseName("idx_results_execution_time");
            #endregion

        }
    }
}
