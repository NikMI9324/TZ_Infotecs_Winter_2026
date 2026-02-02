using System;
using System.Collections.Generic;
using System.Text;

namespace TZ_Infotecs_Winter_2026.Application.Dtos
{
    public record ResultFilterDto
    {
        public string? FileName { get; init; }

        public DateTime? MinimalDateFrom { get; init; }
        public DateTime? MinimalDateTo { get; init; }
        
        public double? AvgExecTimeFrom { get; init; }       
        public double? AvgExecTimeTo { get; init; }

        public double? AvgValueFrom {  get; init; }
        public double? AvgValueTo { get; init; }

    }
}
