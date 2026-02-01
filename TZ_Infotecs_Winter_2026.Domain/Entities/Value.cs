using System;
using System.Collections.Generic;
using System.Text;

namespace TZ_Infotecs_Winter_2026.Domain.Entities
{
    public class Value
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public DateTime Date { get; set; }
        public int ExecutionTime { get; set; }
        public double ValueDefinition { get; set; }
    }
}
