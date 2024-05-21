using System;
using Domain.Enums;

namespace Application.DTOs
{
    public class HistoryGoalDto
    {
        public CategoryFinancialGoalType Category { get; set; }
        public decimal ActualValue { get; set; }
        public decimal TargetValue { get; set; }
        public DateTime TargetDate { get; set; }
        public string Description { get; set; }
        public string PercentageProgress { get; set; }
    }
}

