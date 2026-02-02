using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZ_Infotecs_Winter_2026.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    file_name = table.Column<string>(type: "text", nullable: false),
                    time_delta_seconds = table.Column<double>(type: "double precision", nullable: false),
                    minimal_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    avg_execution_time = table.Column<double>(type: "double precision", nullable: false),
                    avg_value = table.Column<double>(type: "double precision", nullable: false),
                    median_value_definition = table.Column<double>(type: "double precision", nullable: false),
                    max_value = table.Column<double>(type: "double precision", nullable: false),
                    min_value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_results", x => x.file_name);
                });

            migrationBuilder.CreateTable(
                name: "values",
                columns: table => new
                {
                    value_id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    execution_time = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_values", x => x.value_id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_results_avg_value",
                table: "results",
                column: "avg_value");

            migrationBuilder.CreateIndex(
                name: "idx_results_execution_time",
                table: "results",
                column: "avg_execution_time");

            migrationBuilder.CreateIndex(
                name: "idx_results_min_date",
                table: "results",
                column: "minimal_date");

            migrationBuilder.CreateIndex(
                name: "idx_values_date",
                table: "values",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "idx_values_file_name",
                table: "values",
                column: "file_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "results");

            migrationBuilder.DropTable(
                name: "values");
        }
    }
}
